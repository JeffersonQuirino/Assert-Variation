using AssetVariation.Domain.Core.Interfaces.Repository;
using AssetVariation.Domain.Entities;
using AssetVariation.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetVariation.Infra.Data.Repository
{
    public class TraddingFloorRepository : RepositoryBase<TraddingFloorEntity>, ITraddingFloorRepository
    {
        private readonly AssetVariationContext assetVariationContext;
        public TraddingFloorRepository(AssetVariationContext assetVariationContext) : base(assetVariationContext)
        {
            this.assetVariationContext = assetVariationContext;
        }
        public override async Task<ICollection<TraddingFloorEntity>> Select(TraddingFloorEntity criteria)
        {
            return await assetVariationContext.TraddingFloorEntities
                                              .Where(w => criteria.AssetId == 0 || criteria.AssetId == w.AssetId).ToListAsync();
        }      
    }
}