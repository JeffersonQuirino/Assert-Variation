using AssetVariation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetVariation.Infra.Data.Configuration
{
    public class AssetConfiguration : IEntityTypeConfiguration<AssetEntity>
    {
        public void Configure(EntityTypeBuilder<AssetEntity> builder)
        {
            builder.ToTable("Asset");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Active);
            builder.Property(p => p.CreateAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.HasIndex(i => i.Name).IsUnique(true);

            builder.HasMany(p => p.TraddingFloors)
             .WithOne(p => p.Asset)
             .HasForeignKey(p=> p.AssetId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
