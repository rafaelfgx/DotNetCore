using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public sealed class FakeEntityChildConfiguration : IEntityTypeConfiguration<FakeEntityChild>
    {
        public void Configure(EntityTypeBuilder<FakeEntityChild> builder)
        {
            builder.ToTable(nameof(FakeEntityChild));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.HasOne(x => x.FakeEntity).WithMany(x => x.FakeEntityChild).HasForeignKey(x => x.FakeEntityId);
        }
    }
}
