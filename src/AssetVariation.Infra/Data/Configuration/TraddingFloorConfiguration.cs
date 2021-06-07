using AssetVariation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetVariation.Infra.Data.Configuration
{
    public class TraddingFloorConfiguration : IEntityTypeConfiguration<TraddingFloorEntity>
    {
        public void Configure(EntityTypeBuilder<TraddingFloorEntity> builder)
        {
            builder.ToTable("TraddingFloor");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.OperationDate).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.Value).HasColumnType("DECIMAL(18,4)").IsRequired();
            builder.Property(p => p.Variation).HasColumnType("DECIMAL(18,2)").IsRequired();
            builder.Property(p => p.CreateAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();      
            builder.Property(p => p.AssetId);

        }
    }
}
