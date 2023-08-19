
namespace Mc2.CrudTest.Application.FunctionalTests.Customers.Queries;

using Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomers;
using Mc2.CrudTest.Shared.Domain.Entities;
using static Testing;

public class GetTodosTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        var query = new GetCustomersQuery(1,null);

        var result = await SendAsync(query);

    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await AddAsync(new Customer
        {
            Firstname = "Shopping"
        });

        var query = new GetCustomersQuery(1,null);

        var result = await SendAsync(query);

        //result.Should().HaveCount(1);
        //result.First().Items.Should().HaveCount(7);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetCustomersQuery(1, null);

        var action = () => SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
