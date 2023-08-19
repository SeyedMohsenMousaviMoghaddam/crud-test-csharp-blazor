using Mc2.CrudTest.Presentation.Shared.Customers;
using Mc2.CrudTest.Presentation.Shared.Data;
using Mc2.CrudTest.Presentation.Shared.Services;

namespace Mc2.CrudTest.Presentation.Client.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private IHttpService _httpService;

        public CustomerService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<PagedResult<Customer>> GetPeople(string? name, string page)
        {
            return await _httpService.Get<PagedResult<Customer>>("api/Customer?page=" + page + (name != null && name.Trim() != "" ? ("&name=" + name) : ""));
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _httpService.Get<Customer>($"api/customer/{id}");
        }

        public async Task DeleteCustomer(int id)
        {
            await _httpService.Delete($"api/customer/{id}");
        }

        public async Task AddCustomer(Customer customer)
        {
            await _httpService.Post($"api/customer", customer);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await _httpService.Put($"api/customer", customer);
        }
    }
}
