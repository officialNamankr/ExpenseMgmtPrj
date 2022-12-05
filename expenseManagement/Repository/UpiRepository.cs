using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Models.Dto;
using expenseManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace expenseManagement.Repository
{
    public class UpiRepository : IUpiRepository
    {
        private readonly ApplicationDbContext _db;
        public UpiRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddUpi(AddUpiDto upi, string userId)
        {
            //var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            //if(user == null)
            //{
            //    return false;
            //}

            var newUpi = new Upi
            {
                //UserId = userId,
                UpiId = upi.UpiId,
            };

            await _db.Upis.AddAsync(newUpi);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ApplicationUser>> GetUpis(string userId)
        {
            var upis = await _db.Users.Where(u => u.Id == userId).Include(u => u.UpiList).ToListAsync();
            return upis;
        }
    }
}
