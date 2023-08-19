using Mc2.CrudTest.Shared.Application.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Commands.DeleteCustomer;
using Mc2.CrudTest.Shared.Domain.Entities;

namespace Mc2.CrudTest.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class DeleteCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCustomerId()
    {
        var command = new DeleteCustomerCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCustomer()
    {
        var listId = await SendAsync(new CreateCustomerCommand
        {
            Firstname = "New List"
        });

        await SendAsync(new DeleteCustomerCommand(listId));

        var list = await FindAsync<Customer>(listId);

        list.Should().BeNull();
    }
}
