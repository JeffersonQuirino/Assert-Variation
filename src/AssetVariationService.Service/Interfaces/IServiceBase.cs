using AssetVariation.Infra.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetVariation.Service.Interfaces
{
    public interface IServiceBase<TModel> where TModel : BaseDto
    {
        Task<TModel> Add(TModel model);

        Task<bool> AddRange(ICollection<TModel> models);

        Task<TModel> Update(TModel model);

        Task<bool> Remove(TModel model);

        Task<ICollection<TModel>> GetAll();

        Task<ICollection<TModel>> Select(TModel criteria);

        Task<TModel> GetById(long id);
    }
}
