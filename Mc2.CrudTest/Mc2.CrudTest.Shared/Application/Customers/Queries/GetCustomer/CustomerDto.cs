using Mc2.CrudTest.Shared.Domain.Entities;

namespace Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomer;

public class CustomerDto
{
    public CustomerDto()
    {
    }

    public int Id { get; init; }

    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BankAccountNumber { get; set; }
    public byte Gender { get; set; }


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
