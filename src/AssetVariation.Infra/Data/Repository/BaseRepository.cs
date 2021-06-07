using AssetVariation.Domain.Core.Interfaces;
using AssetVariation.Domain.Core.Interfaces.Repository;
using AssetVariation.Domain.Entities;
using AssetVariation.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetVariation.Infra.Data.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        private readonly AssetVariationContext assetVariationContext;
        private DbSet<TEntity> dbSet;

        public RepositoryBase(AssetVariationContext assetVariationContext)
        {
            this.assetVariationContext = assetVariationContext;
            this.dbSet = assetVariationContext.Set<TEntity>();
        }      

        public async Task<TEntity> Add(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await assetVariationContext.SaveChangesAsync();
            return entity;
        }

        public async Task<int> AddRange(ICollection<TEntity> entities)
        {           
            await dbSet.AddRangeAsync(entities);
            return await assetVariationContext.SaveChangesAsync();
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            return await assetVariationContext.SaveChangesAsync() > 0;
        }

        public abstract Task<ICollection<TEntity>> Select(TEntity criteria);

        public async Task<TEntity> UpdateAsync(TEntity entity,TEntity entityChange)
        {           
            assetVariationContext.Entry(entity).CurrentValues.SetValues(entityChange);         
            return await assetVariationContext.SaveChangesAsync() > 0 ? entityChange :null;           
        }       
    }
}