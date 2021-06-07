using AssetVariation.Domain.Core.Interfaces.Repository;
using AssetVariation.Domain.Entities;
using AssetVariation.Infra.Dto;
using AssetVariation.Service.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AssetVariation.Service.Services
{
    public class BaseService<TModel, TEntity> : IServiceBase<TModel> where TModel : BaseDto where TEntity : BaseEntity
    {
        private readonly IMapper mapper;
        private readonly IRepositoryBase<TEntity> repository;

        public BaseService(IMapper mapper,
                           IRepositoryBase<TEntity> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<TModel> Add(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.Id > 0)
                throw new ArgumentException($"Operação não permitida, não é possível adicionar uma entidade que já existe!({nameof(TEntity)})!");

            var domain = mapper.Map<TEntity>(model);

            if (!domain.Validate())
                throw new WarningException(string.Join("!", domain.Errors));

            await repository.Add(domain);         

            return mapper.Map<TModel>(domain);
        }

        public async Task<bool> AddRange(ICollection<TModel> model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.Count == 0)
                throw new ArgumentException("Lista vazia");

            var domain = mapper.Map<ICollection<TModel>,List<TEntity>>(model);
          
            domain = domain.Where(w => w.Errors.Count == 0)?.ToList();

            if (!domain.Any())
                return false;

            var result = await repository.AddRange(domain);
            return result == domain.Count;
        }

        public async Task<ICollection<TModel>> GetAll()
        {
            var result = await repository.GetAll();
            return mapper.Map<ICollection<TModel>>(result);
        }

        public async Task<TModel> GetById(long id)
        {
            if (id == 0)
                return null;

            var result = await repository.GetById(id);
            return mapper.Map<TModel>(result);
        }

        public async Task<bool> Remove(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var result = repository.GetById(model.Id);

            if (result == null)
                throw new ArgumentException($"Operação não permitida, a entidade que deseja remover não foi localizada!");

            return await  repository.Remove(mapper.Map<TEntity>(model));
          
        }

        public async Task<ICollection<TModel>> Select(TModel criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            var result = await repository.Select(mapper.Map<TEntity>(criteria));
            return mapper.Map<ICollection<TModel>>(result);
        }

        public async Task<TModel> Update(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.Id == 0)
                throw new ArgumentException($"Operação não permitida, não é possível alterar uma entidade que ainda não foi cadastrada!({nameof(TEntity)})!");

            var result = await repository.GetById(model.Id);

            if (result == null)
                throw new ArgumentException($"Operação não permitida, a entidade que deseja alterar não foi localizada!");

            var domain = mapper.Map<TEntity>(model);

            if (!domain.Validate())
                throw new WarningException(string.Join("!", domain.Errors));

            var domainResult = await repository.UpdateAsync(result, domain);

            return domainResult != null ? mapper.Map<TModel>(domainResult) : null;
          
        }
    }
}