namespace expenseManagement.Models.Dto
{
    public class AddExpenseKeeperDto
    {
        public string Title { get; set; }

        public List<AddingUsersDto> KeeperUsers { get; set; }

        public List<AddExpToKeeperDto> Expenses { get; set; }

        public string Description { get; set; }
    }
}
