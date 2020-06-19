using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public sealed class FakeEntityConfiguration : IEntityTypeConfiguration<FakeEntity>
    {
        public void Configure(EntityTypeBuilder<FakeEntity> builder)
        {
            builder.ToTable(nameof(FakeEntity));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Surname).IsRequired().HasMaxLength(100);

            builder.OwnsOne(x => x.FakeValueObject, y =>
            {
                y.Property(x => x.Property1).HasColumnName(nameof(FakeEntity.FakeValueObject.Property1)).IsRequired().HasMaxLength(100);
                y.Property(x => x.Property2).HasColumnName(nameof(FakeEntity.FakeValueObject.Property2)).IsRequired().HasMaxLength(200);
            });

            builder.HasMany(x => x.FakeEntityChild).WithOne(x => x.FakeEntity).HasForeignKey(x => x.FakeEntityId);
        }
    }
}
