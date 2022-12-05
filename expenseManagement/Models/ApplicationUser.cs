using Microsoft.AspNetCore.Identity;

namespace expenseManagement.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
            this.UpiList = new HashSet<Upi>();
            this.ExpenseKeepers = new HashSet<ExpenseKeeper>();
        }
        public string Name { get; set; }
        public DateTime Addedon { get; set; }

        public virtual ICollection<ExpenseKeeper> ExpenseKeepers { get; set; }

        public virtual ICollection<Upi> UpiList { get; set; }

    }
}
