using AssetVariation.Domain.Core.Interfaces;
using AssetVariation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AssetVariation.Infra.Data.Context
{
    public sealed class AssetVariationContext: DbContext
    {
        public AssetVariationContext()
        {

        }

        public AssetVariationContext(DbContextOptions<AssetVariationContext> options):base(options)
        {

        }

        public DbSet<AssetEntity> AssetEntities { get; set; }
        public DbSet<TraddingFloorEntity> TraddingFloorEntities { get; set; }

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssetVariationContext).Assembly);
        }       

    }
}
