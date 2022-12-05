using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Models.Dto;
using expenseManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace expenseManagement.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _db;
        public ExpenseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> DeleteExpense(Guid id)
        {
            var expense = await _db.Expenses.Where(e => e.ExpenseId.Equals(id)).FirstOrDefaultAsync();
            if(expense == null)
            {
                return false;
            }
            _db.Expenses.Remove(expense);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Expense> GetExpenseByID(Guid id)
        {
            var exp = await _db.Expenses.Where(e => e.ExpenseId.Equals(id))
                .Include(g => g.ExpenseGenericDetail)
                .Include(u => u.UserExpenses)
                .FirstOrDefaultAsync();
            return exp;
        }
    }
}
