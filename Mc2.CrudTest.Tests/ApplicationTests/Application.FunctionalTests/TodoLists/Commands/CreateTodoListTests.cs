
namespace Mc2.CrudTest.Application.FunctionalTests.Customers.Commands;

using Mc2.CrudTest.Shared.Application.Common.Exceptions;
using Mc2.CrudTest.Shared.Application.Customers.Commands.CreateCustomer;
using Mc2.CrudTest.Shared.Domain.Entities;
using static Testing;

public class CreateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateCustomerCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateCustomerCommand
        {
            Firstname = "Shopping"
        });

        var command = new CreateCustomerCommand
        {
            Firstname = "Shopping"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCustomer()
    {
        var command = new CreateCustomerCommand
        {
            Firstname = "Tasks"
        };

        var id = await SendAsync(command);

        var list = await FindAsync<Customer>(id);

        list.Should().NotBeNull();
        list!.Firstname.Should().Be(command.Firstname);
        list.CreatedBy.Should().Be("TestUser");
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
