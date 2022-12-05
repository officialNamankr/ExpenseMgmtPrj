using System.ComponentModel.DataAnnotations;

namespace expenseManagement.Models
{
    public class ExpenseGenericDetail
    {
        [Key]
        public Guid ExpenseGDId { get; set; }
        public string Title { get; set; }
        public int TotalValue { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
