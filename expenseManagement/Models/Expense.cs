using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenseManagement.Models
{
    public class Expense
    {
        public Expense()
        {
            this.UserExpenses = new HashSet<UserExpense>();
        }
        [Key]
        public Guid ExpenseId { get; set; }

        public ExpenseGenericDetail ExpenseGenericDetail { get; set; }

        public ICollection<UserExpense> UserExpenses { get; set; }
        //public string UserExpId { get; set; }
        //[ForeignKey("UserExpId")]
        //public virtual UserExpense UserExpense { get; set; }

        //public string ExpenseDoerId { get; set; }
        //[ForeignKey("ExpenseDoerId")]
        public  ApplicationUser ExpenseDoer { get; set; }
    }
}
