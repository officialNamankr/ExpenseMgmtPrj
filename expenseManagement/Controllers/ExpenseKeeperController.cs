using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Models.Dto;
using expenseManagement.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace expenseManagement.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseKeeperController : Controller
    {
        private readonly ApplicationDbContext _db;
        private  ResponseDto _response;
        private IExpenseKeeperRepository _expesneKeeperRepository;

        public ExpenseKeeperController(ApplicationDbContext db,
        
            IExpenseKeeperRepository expenseKeeperRepository)
        {
            _db = db;
            this._response = new ResponseDto();
            _expesneKeeperRepository = expenseKeeperRepository;

        }

        [HttpGet]
        [Route("GetKeeperById")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ResponseDto> GetKeeperById([FromBody]Guid id)
        {
            try
            {
                var result = await _expesneKeeperRepository.GetExpenseKeeperById(id);
                if (result == null)
                {
                    _response.IsSuccess = false;
                    _response.Result = BadRequest();
                    _response.DisplayMessage = "ExpenseKeeper does not exists";

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
        public async Task<object> AddExpenseKeeper([FromBody] AddExpenseKeeperDto ExpKeep)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue("id");
                    var user = await _db.Users.FindAsync(userId);
                    var members = new List<ApplicationUser>();
                    var exps = new List<Expense>();
                    foreach(var usr in ExpKeep.KeeperUsers)
                    {
                        var u = await _db.Users.FirstOrDefaultAsync(u => u.Id.Equals(usr.Id));
                        members.Add(u);
                    }
                    //foreach(var e in ExpKeep.Expenses)
                    //{
                    //    //var ex = await _db.Expenses.FirstOrDefaultAsync(u => u.ExpenseId.Equals(e.Id));
                    //    exps.Add(ex);
                    //}

                    var newExpKeep = new ExpenseKeeper()
                    {
                        Description = ExpKeep.Description,
                        Title = ExpKeep.Title,
                        StartDate = DateTime.Now,
                        ExpenseKeeperCreator = user.Id,
                        //KepperUsers = members,
                        //Expenses = exps
                    };
                    
                    var isAdded = await _expesneKeeperRepository.AddExpenseKeeper(newExpKeep);
                    
                        _response.Result = Ok(isAdded);
                        _response.DisplayMessage = "ExpenseKeeper Added Successfully";
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


        [HttpPut]
        [Route("AddMembersToKeeper")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<object> AddMembersToExpenseKeeper([FromBody] AddMembersTokeeper ExpKeep)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _expesneKeeperRepository.AddMembersToExpenseKeeper(ExpKeep);

                    _response.Result = Ok(result);
                    _response.DisplayMessage = "Members  Added Successfully";
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


        [HttpPut]
        [Route("AddExpenseToKeeper")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<object> AddExpensesToExpenseKeeper([FromBody] AddExpenseDto Exp)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _expesneKeeperRepository.AddExpensesToExpenseKeeper(Exp);

                    _response.Result = Ok(result);
                    _response.DisplayMessage = "Expesne  Added Successfully";
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



        [HttpDelete]
        [Route("DeleteExpenseInKeeper")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ResponseDto> DeleteExpsene([FromBody] DeleteExpenseInKeeperDto exp) 
        {
            try
            {
                var res = await _expesneKeeperRepository.DeleteExpenseInKeeper(exp);
                _response.Result = NoContent();
                if (res != null)
                {
                    _response.DisplayMessage = "successfully Deleted Expense";
                }
                else
                {
                    _response.DisplayMessage = "Expense Not Found or Keeper not found";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


    }
}
