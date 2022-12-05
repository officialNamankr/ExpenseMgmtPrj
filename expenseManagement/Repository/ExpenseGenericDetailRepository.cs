using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Repository.IRepository;

namespace expenseManagement.Repository
{
    public class ExpenseGenericDetailRepository : IExpenseGenericDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public ExpenseGenericDetailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ExpenseGenericDetail> AddGenericDetail(ExpenseGenericDetail egd)
        {
            var result = await _db.ExpensesGenericDetails.AddAsync(egd);
            await _db.SaveChangesAsync();
            return egd;
        }

        public async Task<ExpenseGenericDetail> GetGenericDetailById(string id)
        {
            var result = await _db.ExpensesGenericDetails.FindAsync(id);
            return result;
        }

        
    }
}
