using expenseManagement.Models;

namespace expenseManagement.Repository.IRepository
{
    public interface IUserExpenseRepository
    {
        Task<List<UserExpense>> ExpenseByKeeperId (string keeperId);

    }
}
