using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Models.Dto;
using expenseManagement.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace expenseManagement.Controllers
{
    [Route("api/[controller]")]
    public class UpiController : Controller
    {
        private readonly ApplicationDbContext _db;
        ResponseDto _response;
        private readonly IUpiRepository _upiRepository;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly RefreshTokenValidator _refreshTokenValidator;
        //private readonly IRefreshTokenRepository _refreshTokenRepository;
        //private readonly AccessTokenGenerator _accessTokenGenerator;
        //private readonly RefreshTokenGenerator _refreshTokenGenerator;


        public UpiController(ApplicationDbContext db,
            //UserManager<ApplicationUser> userManager,
            //RoleManager<IdentityRole> roleManager, 
            //SignInManager<ApplicationUser> signInManager,
            //AccessTokenGenerator accessTokenGenerator,
            //RefreshTokenGenerator refreshTokenGenerator,
            //RefreshTokenValidator refreshTokenValidator,
            //IRefreshTokenRepository refreshTokenRepository
              IUpiRepository upiRepository
            )
        {
            _db = db;
            this._response = new ResponseDto();
            _upiRepository = upiRepository;
            //_userManager = userManager;
            //_roleManager = roleManager;
            //_signInManager = signInManager;
            //_accessTokenGenerator = accessTokenGenerator;
            //_refreshTokenGenerator = refreshTokenGenerator;
            //_refreshTokenValidator = refreshTokenValidator;
            //_refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost]
        [Route("AddUpi")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<object> AddUpi([FromBody]AddUpiDto upi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = User.FindFirstValue("id");
                    var isAdded = await _upiRepository.AddUpi(upi,user);
                    if (!isAdded)
                    {
                        _response.IsSuccess = false;
                        _response.Result = BadRequest();
                        _response.DisplayMessage = "Error in adding Upi";
                    }
                    else
                    {
                        _response.Result = Ok();
                        _response.DisplayMessage = "Upi Added Successfully";
                    }
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Result = BadRequest(ModelState);
                    _response.DisplayMessage = "Error in adding Upi";
                    //return BadRequest(ModelState);
                }


            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("", ex.Message);
                //return BadRequest(ModelState);
                _response.IsSuccess = false;
                _response.Result = BadRequest(ModelState);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;

        }



        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetUpis")]
        public async Task<object> GetALLSkill()
        {
            try
            {
                var user = User.FindFirstValue("id");
                var result = await _upiRepository.GetUpis(user);
                if (result == null)
                {
                    _response.Result = BadRequest();
                    _response.DisplayMessage = "User does not exists";

                }
                
                _response.Result = Ok(result);
                _response.DisplayMessage = "Sucessfully fetched all the users with Upis";
            }
            catch (Exception ex)
            {
                _response.Result = BadRequest(ModelState);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

    }
}
