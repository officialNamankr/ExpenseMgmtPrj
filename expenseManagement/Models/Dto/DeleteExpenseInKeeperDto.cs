namespace expenseManagement.Models.Dto
{
    public class DeleteExpenseInKeeperDto
    {
        public Guid ExpenseKeeperId { get; set; }
        public Guid ExpenseId { get; set; }
    }
}
