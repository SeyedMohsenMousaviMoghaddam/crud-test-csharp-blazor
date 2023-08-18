using System.Reflection;
using Mc2.CrudTest.Shared.Application.Common.Interfaces;
using Mc2.CrudTest.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Shared.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
