using expenseManagement.DbContexts;
using expenseManagement.Models;
using expenseManagement.Models.Dto;
using expenseManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace expenseManagement.Repository
{
    public class ExpenseKeeperRepository : IExpenseKeeperRepository
    {
        private readonly ApplicationDbContext _db;
        public ExpenseKeeperRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ExpenseKeeper> AddExpenseKeeper(ExpenseKeeper ExpKeep)
        {
            await _db.ExpensesKeepers.AddAsync(ExpKeep);
            await _db.SaveChangesAsync();
            var expenseKeeper = await _db.ExpensesKeepers.Where(u => u.KeeperId.Equals(ExpKeep.KeeperId)).FirstOrDefaultAsync();
            if (expenseKeeper == null)
            {
                return null;
            }
            var user = await _db.Users.FindAsync(ExpKeep.ExpenseKeeperCreator);
            expenseKeeper.KepperUsers.Add(user);
            
            await _db.SaveChangesAsync();

            return ExpKeep;
        }

        public async Task<ExpenseKeeper> AddExpensesToExpenseKeeper(AddExpenseDto Exp)
        {
            var uExpList = new List<UserExpense>();
            foreach (var ue in Exp.UserExpenses)
            {
                var usr = await _db.Users.FirstOrDefaultAsync( u => u.Id.Equals(ue.UserId));
                var newUE = new UserExpense()
                {
                    User = usr,
                    Value = ue.Value
                };
                uExpList.Add(newUE);
            }
            var expenseDoer = await _db.Users.FirstOrDefaultAsync(u => u.Id.Equals(Exp.ExpenseDoerId));
            var expenseGenericDetail = new ExpenseGenericDetail()
            {
                Title = Exp.ExpenseGenericDetail.Title,
                TotalValue = Exp.ExpenseGenericDetail.TotalValue,
                Description = Exp.ExpenseGenericDetail.Description,
                ExpenseDate = Exp.ExpenseGenericDetail.ExpenseDate,
            };
            var newExp = new Expense()
            {
                ExpenseGenericDetail = expenseGenericDetail,
                UserExpenses = uExpList,
                ExpenseDoer = expenseDoer
            };
            var expKeep = await _db.ExpensesKeepers.FirstOrDefaultAsync(e => e.KeeperId.Equals(Exp.ExpenseKeeperId));
            if(expKeep != null)
            {
                expKeep.Expenses.Add(newExp);
            }
            
            await _db.SaveChangesAsync();
            return expKeep;
        }

        public async Task<ExpenseKeeper> AddMembersToExpenseKeeper(AddMembersTokeeper ExpKeep)
        {
            var expenseKeeper = await _db.ExpensesKeepers.Where(u => u.KeeperId.Equals(ExpKeep.Id)).FirstOrDefaultAsync();
            if (expenseKeeper == null)
            {
                return null;
            }
            foreach(var ek in ExpKeep.Members)
            {
                var mem = await _db.Users.FirstOrDefaultAsync(u => u.Id.Equals(ek.Id));
                expenseKeeper.KepperUsers.Add(mem);
            }
            await _db.SaveChangesAsync();
            return expenseKeeper;

        }

        public async Task<ExpenseKeeper> DeleteExpenseInKeeper(DeleteExpenseInKeeperDto exp)
        {
            var expesne = await  _db.Expenses.FindAsync(exp.ExpenseId);
            var expenseKeeper = await _db.ExpensesKeepers.FindAsync(exp.ExpenseKeeperId);
            if(expesne == null ||expenseKeeper == null)
            {
                return null;
            }
            expenseKeeper.Expenses.Remove(expesne);
            await _db.SaveChangesAsync();
            _db.Expenses.Remove(expesne);
            await _db.SaveChangesAsync();
            return expenseKeeper;
        }

        public async Task<ExpenseKeeper> GetExpenseKeeperById(Guid id)
        {
            var result = await _db.ExpensesKeepers
                .Include(u => u.KepperUsers)
                .Include(e => e.Expenses)
                .ThenInclude(us => us.UserExpenses)
                .Include(e => e.Expenses)
                .ThenInclude(gd => gd.ExpenseGenericDetail)
                .Where(x => x.KeeperId.Equals(id)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ExpenseKeeper> GetExpenseKeeperById(string id)
        {
            var eK = await _db.ExpensesKeepers.Include(u => u.KepperUsers).Include(e => e.Expenses).Where(x => x.KeeperId.Equals(id)).FirstOrDefaultAsync();
            return eK;
            
        }
    }
}
