using Mc2.CrudTest.Shared.Application.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Commands.DeleteCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Commands.UpdateCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomers;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {        
        [HttpGet]
        public async Task<CustomersVm> GetCustomers(ISender sender)
        {
            return await sender.Send(new GetCustomersQuery());
        }
        [HttpPost]
        public async Task<int> CreateCustomer(ISender sender, CreateCustomerCommand command)
        {
            return await sender.Send(command);
        }
        [HttpPut]
        public async Task<IResult> UpdateCustomer(ISender sender, int id, UpdateCustomerCommand command)
        {
            if (id != command.Id) return Results.BadRequest();
            await sender.Send(command);
            return Results.Ok();
        }
        [HttpDelete]
        public async Task<IResult> DeleteCustomer(ISender sender, int id)
        {
            await sender.Send(new DeleteCustomerCommand(id));
            return Results.Ok();
        }
    }
}