using expenseManagement.Models;
using expenseManagement.Models.Dto;

namespace expenseManagement.Repository.IRepository
{
    public interface IUpiRepository
    {
        Task<bool> AddUpi(AddUpiDto upi, string userId);
        Task<List<ApplicationUser>> GetUpis(string userId);
    }
}
