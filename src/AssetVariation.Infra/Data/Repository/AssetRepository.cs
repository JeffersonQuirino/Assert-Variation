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
    public class AssetRepository:RepositoryBase<AssetEntity>,IAssetRepository
    {
        private readonly AssetVariationContext assetVariationContext;

        public AssetRepository(AssetVariationContext assetVariationContext) :base(assetVariationContext)
        {           
            this.assetVariationContext = assetVariationContext;
        }

        public override async Task<ICollection<AssetEntity>> Select(AssetEntity criteria) 
        {
            return await assetVariationContext.AssetEntities                                            
                                              .Where(w => (criteria.Id == 0 || criteria.Id == w.Id) &&
                                                          (string.IsNullOrEmpty(criteria.Name) || criteria.Name.ToUpper().Trim() == w.Name.ToUpper().Trim()) &&
                                                          (criteria.CreateAt == DateTime.MinValue || criteria.CreateAt.Date == w.CreateAt.Date)
                                                    )
                                              .Include(i=> i.TraddingFloors)
                                              .AsNoTracking()
                                              .ToListAsync();                                                             
        }
    }
}