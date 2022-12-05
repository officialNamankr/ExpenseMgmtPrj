using expenseManagement.Models;
using expenseManagement.Models.Dto;

namespace expenseManagement.Repository.IRepository
{
    public interface IExpenseKeeperRepository
    {
        Task<ExpenseKeeper> GetExpenseKeeperById(string id);
        Task<ExpenseKeeper> GetExpenseKeeperById(Guid id);
        Task<ExpenseKeeper> AddExpenseKeeper(ExpenseKeeper ExpKeep);

        Task<ExpenseKeeper> AddMembersToExpenseKeeper(AddMembersTokeeper ExpKeep);

        Task<ExpenseKeeper> AddExpensesToExpenseKeeper(AddExpenseDto Exp);

        Task<ExpenseKeeper> DeleteExpenseInKeeper(DeleteExpenseInKeeperDto exp);
    }
}
