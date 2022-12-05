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
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private IExpenseRepository _expenseRepository;
        public ExpenseController(ApplicationDbContext db,
            IExpenseRepository expenseRepository
            )
        {
            _db = db;
            _expenseRepository = expenseRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        [Route("getExpenseById/{Id}")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ResponseDto> GetExpenseById(Guid Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _expenseRepository.GetExpenseByID(Id);
                    if (result == null)
                    {
                        _response.IsSuccess = false;
                        _response.Result = NotFound();
                        _response.DisplayMessage = "Expense Not Found";
                    }
                    _response.Result = Ok(result);
                    _response.DisplayMessage = "Successfull fetched the data";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = BadRequest(ModelState);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }


        [HttpDelete]
        [Route("DeleteExpense")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ResponseDto> DeleteSkill([FromBody]Guid id)
        {
            try
            {
                bool res = await _expenseRepository.DeleteExpense(id);
                _response.Result = NoContent();
                if (res)
                {
                    _response.DisplayMessage = "successfully Deleted Expense";
                }
                else
                {
                    _response.DisplayMessage = "Expense Not Found";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


        //[HttpPost]
        //[Route("AddExpense")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<object> AddExpense([FromBody] AddExpenseDto Exp)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = User.FindFirstValue("id");
        //            var isAdded = await _expenseRepository.AddExpense(Exp,user);

        //            _response.Result = Ok();
        //            _response.DisplayMessage = "Expense Added Successfully";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //ModelState.AddModelError("", ex.Message);
        //        //return BadRequest(ModelState);
        //        _response.IsSuccess = false;
        //        _response.Result = BadRequest(ModelState);
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string> { ex.ToString() };
        //    }

        //    return _response;

        //}


    }
}
