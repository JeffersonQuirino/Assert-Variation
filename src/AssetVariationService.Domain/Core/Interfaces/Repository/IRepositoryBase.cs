using AssetVariation.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetVariation.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity entity);

        Task<int> AddRange(ICollection<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity, TEntity entityChange);

        Task<bool> Remove(TEntity entity);

        Task<ICollection<TEntity>> GetAll();

        Task<ICollection<TEntity>> Select(TEntity criteria);

        Task<TEntity> GetById(long id);
    }
}
