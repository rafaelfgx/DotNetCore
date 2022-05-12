using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCore.EntityFrameworkCore.Tests;

public sealed class PhoneConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.ToTable(nameof(Phone));

        builder.HasKey(phone => phone.Id);

        builder.Property(phone => phone.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Property(phone => phone.Number).IsRequired().HasMaxLength(20);

        builder.HasOne(phone => phone.Customer).WithMany(customer => customer.Phones).HasForeignKey(phone => phone.CustomerId);
    }
}
