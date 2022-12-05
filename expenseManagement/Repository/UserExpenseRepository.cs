using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Repository.IRepository;

namespace expenseManagement.Repository
{
    public class UserExpenseRepository : IUserExpenseRepository
    {
        private readonly ApplicationDbContext _db;
        public UserExpenseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<List<UserExpense>> ExpenseByKeeperId(string keeperId)
        {
            throw new NotImplementedException();
        }
        //public Task<List<UserExpense>> ExpenseByKeeperId(string keeperId)
        //{
        //    var result  = _db.UserExpenses.Where
        //}
    }
}
