using expenseManagement.Models;

namespace expenseManagement.Repository.IRepository
{
    public interface IExpenseGenericDetailRepository
    {
        Task<ExpenseGenericDetail> GetGenericDetailById(string id);
        Task<ExpenseGenericDetail> AddGenericDetail(ExpenseGenericDetail egd);
    }
}
