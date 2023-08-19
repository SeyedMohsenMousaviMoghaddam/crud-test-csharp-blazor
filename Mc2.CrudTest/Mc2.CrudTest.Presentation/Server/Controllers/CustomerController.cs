using Mc2.CrudTest.Shared.Application.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Commands.DeleteCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Commands.UpdateCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomers;
using Mc2.CrudTest.Shared.Application.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {        
        [HttpGet]
        public async Task<ActionResult<PagedResult<CustomerDto>>> GetCustomers(ISender sender,string page,string? name)
        {
            return await sender.Send(new GetCustomersQuery(int.Parse(page),name));
        }
        [HttpGet("byId")]
        public async Task<ActionResult<CustomerDto>> GetCustomerQueryById(ISender sender, int id)
        {
            return await sender.Send(new GetCustomerQueryById(id));
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomer(ISender sender, CreateCustomerCommand command)
        {
            return await sender.Send(command);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCustomer(ISender sender, int id, UpdateCustomerCommand command)
        {
            if (id != command.Id) return BadRequest();
            await sender.Send(command);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer(ISender sender, int id)
        {
            await sender.Send(new DeleteCustomerCommand(id));
            return Ok();
        }
    }
}