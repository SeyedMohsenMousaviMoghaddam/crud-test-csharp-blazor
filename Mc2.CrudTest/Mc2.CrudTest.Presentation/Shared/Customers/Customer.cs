namespace Mc2.CrudTest.Presentation.Shared.Customers
{
    public class Customer
    {
        public int Id { get; init; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? BankAccountNumber { get; set; }
        public byte Gender { get; set; }
        public bool IsDeleted { get; set; }

    }
}