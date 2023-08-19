using Mc2.CrudTest.Presentation.Shared.Customers;
using Mc2.CrudTest.Presentation.Shared.Data;

namespace Mc2.CrudTest.Presentation.Client.Services.Customers
{
    public interface ICustomerService
    {
        Task<PagedResult<Customer>> GetPeople(string? name, string page);
        Task<Customer> GetCustomer(int id);

        Task DeleteCustomer(int id);

        Task AddCustomer(Customer customer);

        Task UpdateCustomer(Customer customer);
    }
}
