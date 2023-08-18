using Mc2.CrudTest.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Shared.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(t => t.Firstname)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.Lastname)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.DateOfBirth)
            .IsRequired();
        builder.Property(t => t.Email)
            .HasMaxLength(200)
            .IsRequired();        
        
        
        builder.HasIndex(x => new { x.Firstname,x.Lastname,x.DateOfBirth }).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
    }
}
