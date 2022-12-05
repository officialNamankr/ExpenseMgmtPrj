using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Models.Dto;
using expenseManagement.Models.Response;
using expenseManagement.Repository.IRepository;
using expenseManagement.Services.TokenGenerators;
using expenseManagement.Services.TokenValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace expenseManagement.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        

        public AuthenticationController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,
            AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, 
            RefreshTokenValidator refreshTokenValidator, IRefreshTokenRepository refreshTokenRepository


            )
        {
            _db = db;
            this._response = new ResponseDto();
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;

            
        }


        [HttpPost]
        [Route("Register")]
        public async Task<object> Register([FromBody] RegisterViewDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Name = model.Name,
                        Addedon = DateTime.Now

                    };
                    var IsUserNamePresent = await _db.Users
                        .AnyAsync(u => u.UserName == user.UserName);
                    var IsEmailPresent = await _db.Users.AnyAsync(u => u.Email == user.Email);
                    var IsPhoneRegistered = await _db.Users.AnyAsync(u => u.PhoneNumber.Equals(user.PhoneNumber));
                    if (IsUserNamePresent)
                    {
                        _response.IsSuccess = false;
                        _response.Result = Conflict();
                        _response.DisplayMessage = "UserName Already Present";
                    }
                    else if (IsEmailPresent)
                    {
                        _response.IsSuccess = false;
                        _response.Result = Conflict();
                        _response.DisplayMessage = "Email Already Present";
                    }
                    else if (IsPhoneRegistered)
                    {
                        _response.IsSuccess = false;
                        _response.Result = Conflict();
                        _response.DisplayMessage = "Phone Number Already Present";

                    }
                    else
                    {
                        var result = await _userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            //var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                            //EmailHelper emailHelper = new EmailHelper();
                            //bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                            //if (emailResponse)
                            //{
                            //    _response.IsSuccess = true;
                            //    _response.Result = Ok();
                            //    _response.DisplayMessage = "Email sent successfullt";
                            //}
                            //else
                            //{
                            //    _response.IsSuccess = false;
                            //    _response.Result = BadRequest();
                            //    _response.DisplayMessage = "Failed to send email";
                            //}

                            _response.IsSuccess = true;
                            _response.Result = Ok();
                            _response.DisplayMessage = "User Successfully Registered";


                        }
                    }

                }
                else
                {
                    _response.IsSuccess = false;
                    //_response.ErrorMessages = new List<string>() { ex.ToString() };
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<object> Login([FromBody] LoginViewDto model)
        {

            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.Result = BadRequest();
                _response.DisplayMessage = "Invalid model state";
                return _response;
            }
            else
            {
                ApplicationUser user = await _db.Users.Where(u => u.Email.Equals(model.Email)).FirstOrDefaultAsync();
                if (user == null)
                {
                    _response.IsSuccess = false;
                    _response.Result = Unauthorized();
                    _response.DisplayMessage = "User Does not Exist";
                    return _response;
                }
                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isCorrectPassword)
                {
                    _response.IsSuccess = false;
                    _response.Result = Unauthorized();
                    _response.DisplayMessage = "Password Incorrect";
                    return _response;
                }

                var accessToken = _accessTokenGenerator.GenerateToken(user);
                string refreshToken = _refreshTokenGenerator.GenerateToken();

                RefreshToken newRefreshToken = new RefreshToken()
                {
                    Token = refreshToken,
                    UserId = user.Id,
                };
                await _refreshTokenRepository.Create(newRefreshToken);
                _response.Result = Ok(new AuthenticatedUserResponse()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                });

                _response.DisplayMessage = "Logged In successfully";
                return (_response);

            }

        }


        [HttpDelete]
        [Route("Logout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<object> Logout()
        {

            var userId = User.FindFirstValue("id");


            if (userId == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "first login in to access";
                _response.Result = Unauthorized();
                return _response;
            }
            await _refreshTokenRepository.DeleteAll(userId);

            _response.IsSuccess = true;
            _response.DisplayMessage = "Logged out Successfully";
            _response.Result = NoContent();

            return _response;
        }

    }
}
