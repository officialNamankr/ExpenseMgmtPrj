using expenseManagement.Models;
using expenseManagement.Models.Dto;

namespace expenseManagement.Repository.IRepository
{
    public interface IExpenseRepository
    {
        Task<Expense> GetExpenseByID(Guid id);
        Task<bool>  DeleteExpense(Guid id);
    }
}
