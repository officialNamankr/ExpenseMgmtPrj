using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Models.Dto;
using expenseManagement.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenseManagement.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseGenericDetailController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ResponseDto _response;
        private IExpenseGenericDetailRepository _expenseGenericDetialRepository;

        public ExpenseGenericDetailController(ApplicationDbContext db,
            ResponseDto response,
            IExpenseGenericDetailRepository expenseGenericDetialRepository)
        {
            _db = db;
            _response = response;
           _expenseGenericDetialRepository = expenseGenericDetialRepository;

        }


        [HttpGet]
        [Route("GetExpenseGenericDetailyId")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ResponseDto> GetExpenseGenericDetailyId(string id)
        {
            try
            {
                var result = await _expenseGenericDetialRepository.GetGenericDetailById(id);
                if (result == null)
                {
                    _response.IsSuccess = false;
                    _response.Result = BadRequest();
                    _response.DisplayMessage = "Expense generic detial does not exists";

                }
                else
                {
                    _response.Result = Ok(result);
                    _response.DisplayMessage = "Sucessfully fetched the data";

                }

            }
            catch (Exception ex)
            {
                _response.Result = BadRequest(ModelState);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [Route("AddExpenseKeeper")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<object> AddExpenseKeeper([FromBody] AddExpenseGenericDetailDto Exp)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    
                    var newExpKeep = new ExpenseGenericDetail()
                    {
                       Description = Exp.Description,
                       ExpenseDate = Exp.ExpenseDate,
                       Title = Exp.Title,
                       TotalValue = Exp.TotalValue

                    };
                    var isAdded = await _expenseGenericDetialRepository.AddGenericDetail(newExpKeep);

                    _response.Result = Ok();
                    _response.DisplayMessage = "Expense Genric Detail Added Successfully";
                }
                //else
                //{
                //    _response.IsSuccess = false;
                //    _response.Result = BadRequest(ModelState);
                //    _response.DisplayMessage = "Error in adding ExpenseKeeper";
                //    //return BadRequest(ModelState);
                //}


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


    }
}
