using expenseManagement.Models;
using expenseManagement.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace expenseManagement.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class EmailController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ResponseDto _response;

        public EmailController(UserManager<ApplicationUser> userManager, ResponseDto response)
        {
            _userManager = userManager;
            _response = response;
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<ResponseDto> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.Result = NotFound();
                _response.DisplayMessage = "User not found";

            }
            else
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _response.IsSuccess = true;
                    _response.Result = Ok(result);
                    _response.DisplayMessage = "Email Confirmer Sucessfully";
                }
                _response.IsSuccess = false;
                _response.Result = NotFound();
                _response.DisplayMessage = "Email Could not be verified. Please Try Again!!!";
            }

            return _response;

        }
    }
}
