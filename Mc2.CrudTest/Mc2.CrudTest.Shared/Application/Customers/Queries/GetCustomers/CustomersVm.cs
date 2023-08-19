using Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomer;

namespace Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomers;

public class CustomersVm
{
    public IReadOnlyCollection<CustomerDto> Lists { get; init; } = Array.Empty<CustomerDto>();
}
