using Mc2.CrudTest.Shared.Application.Common.Models;

namespace Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomers;

public class CustomersVm
{
    public IReadOnlyCollection<LookupDto> Genders { get; init; } = Array.Empty<LookupDto>();

    public IReadOnlyCollection<CustomerDto> Lists { get; init; } = Array.Empty<CustomerDto>();
}
