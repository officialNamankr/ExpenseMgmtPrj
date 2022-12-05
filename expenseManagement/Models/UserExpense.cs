using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenseManagement.Models
{
    public class UserExpense
    {
        [Key]
        public Guid UserExpId { get; set; }
        
        public  ApplicationUser User { get; set; }

        public int Value { get; set; }

    }
}
