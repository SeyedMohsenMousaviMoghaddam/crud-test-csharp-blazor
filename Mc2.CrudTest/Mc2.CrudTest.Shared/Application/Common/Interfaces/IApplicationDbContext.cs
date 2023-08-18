using Mc2.CrudTest.Shared.Domain.Entities;

namespace Mc2.CrudTest.Shared.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
