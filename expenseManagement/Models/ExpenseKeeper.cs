using System.ComponentModel.DataAnnotations;

namespace expenseManagement.Models
{
    public class ExpenseKeeper
    {
        public ExpenseKeeper()
        {
            this.KepperUsers = new HashSet<ApplicationUser>();
            this.Expenses = new HashSet<Expense>();
        }
        [Key]
        public Guid KeeperId { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }   

        public virtual ICollection<ApplicationUser> KepperUsers { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }

        public string Description { get; set; }

        public string ExpenseKeeperCreator { get; set; }

    }
}
