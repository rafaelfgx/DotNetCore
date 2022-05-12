using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCore.EntityFrameworkCore.Tests;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));

        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Property(customer => customer.Code).IsRequired();

        builder.OwnsOne(customer => customer.Name, customerName =>
        {
            customerName.Property(name => name.FirstName).HasColumnName(nameof(Name.FirstName)).IsRequired().HasMaxLength(100);

            customerName.Property(name => name.LastName).HasColumnName(nameof(Name.LastName)).IsRequired().HasMaxLength(250);
        });

        builder.HasMany(customer => customer.Phones).WithOne(phone => phone.Customer).HasForeignKey(phone => phone.CustomerId);
    }
}
