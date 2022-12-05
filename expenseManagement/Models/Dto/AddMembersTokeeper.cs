namespace expenseManagement.Models.Dto
{
    public class AddMembersTokeeper
    {
        public Guid Id { get; set; }
        public List<AddingUsersDto> Members { get; set; }
    }
}
