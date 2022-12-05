namespace expenseManagement.Models.Dto
{
    public class AddExpenseGenericDetailDto
    {
        public string Title { get; set; }
        public int TotalValue { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
