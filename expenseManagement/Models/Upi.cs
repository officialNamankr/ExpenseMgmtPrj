using System.ComponentModel.DataAnnotations.Schema;

namespace expenseManagement.Models
{
    public class Upi
    {
        public Guid Id { get; set; }
        //public string UserId { get; set; }
        //[ForeignKey("UserId")]

        public  ApplicationUser User { get; set; }

        public string UpiId { get; set; }
    }
}
