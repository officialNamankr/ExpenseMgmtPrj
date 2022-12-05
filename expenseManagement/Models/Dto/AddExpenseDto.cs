namespace expenseManagement.Models.Dto
{
    public class AddExpenseDto
    {
        public Guid ExpenseKeeperId { get; set; }
        public AddExpenseGenericDetailDto ExpenseGenericDetail { get; set; }

        public ICollection<AddUserExpenseDto> UserExpenses { get; set; }
    
        public string ExpenseDoerId { get; set; }
    }
}
