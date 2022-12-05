using expenseManagement.Models;

namespace expenseManagement.Repository.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByToken(string refreshToken);
        Task Create(RefreshToken refreshToken);
        Task Delete(Guid id);

        Task DeleteAll(string id);
    }
}
