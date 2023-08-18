namespace Mc2.CrudTest.Shared.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BankAccountNumber { get; set; }
    public GenderEnum Gender { get; set; }
}
