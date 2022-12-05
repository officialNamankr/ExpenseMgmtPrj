using expenseManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace expenseManagement.DbContexts
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<Upi> Upis { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<UserExpense> UserExpenses { get; set; }
        public DbSet<ExpenseGenericDetail> ExpensesGenericDetails { get; set; }

        public DbSet<ExpenseKeeper> ExpensesKeepers { get; set; }

    }
} 
