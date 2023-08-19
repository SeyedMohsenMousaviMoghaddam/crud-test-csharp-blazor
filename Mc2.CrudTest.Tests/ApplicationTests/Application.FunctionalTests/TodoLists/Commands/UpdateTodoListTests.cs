
namespace Mc2.CrudTest.Application.FunctionalTests.Customers.Commands;

using Mc2.CrudTest.Shared.Application.Common.Exceptions;
using Mc2.CrudTest.Shared.Application.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Shared.Application.Customers.Commands.UpdateCustomer;
using Mc2.CrudTest.Shared.Domain.Entities;
using static Testing;

public class UpdateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCustomerId()
    {
        var command = new UpdateCustomerCommand { Id = 99, Firstname = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var listId = await SendAsync(new CreateCustomerCommand
        {
            Firstname = "New List"
        });

        await SendAsync(new CreateCustomerCommand
        {
            Firstname = "Other List"
        });

        var command = new UpdateCustomerCommand
        {
            Id = listId,
            Firstname = "Other List"
        };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("'Title' must be unique.");
    }

    [Test]
    public async Task ShouldUpdateCustomer()
    {

        var listId = await SendAsync(new CreateCustomerCommand
        {
            Firstname = "New List"
        });

        var command = new UpdateCustomerCommand
        {
            Id = listId,
            Firstname = "Updated List Title"
        };

        await SendAsync(command);

        var list = await FindAsync<Customer>(listId);

        list.Should().NotBeNull();
        list!.Firstname.Should().Be(command.Firstname);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be("");
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
