using AutoMapper;
using FinTracker.Application.Contracts.Dtos;
using FinTracker.Domain.Data;
using FinTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Application.Services
{
    public abstract class CrudAppServiceBase<TEntity, TEntityDto, TCreateInput, TUpdateInput> : AppServiceBase
    where TEntity : class, IEntity
    where TEntityDto : class
    where TCreateInput : class
    where TUpdateInput : class
    {
        protected readonly IRepository<TEntity> Repository;

        protected CrudAppServiceBase(
            IRepository<TEntity> repository,
            IMapper mapper,
            ICurrentUser currentUser)
            : base(mapper, currentUser)
        {
            Repository = repository;
        }

        public virtual async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await Repository.GetByIdAsync(id);
            return MapTo<TEntityDto>(entity);
        }

        public virtual async Task<PagedResultDto<TEntityDto>> GetListAsync(PagedRequestDto input)
        {
            var query = Repository.GetAll();
            var totalCount = await query.CountAsync();

            query = ApplySorting(query);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();
            var dtos = entities.Select(MapTo<TEntityDto>).ToList();

            return new PagedResultDto<TEntityDto>
            {
                TotalCount = totalCount,
                Items = dtos
            };
        }

        public virtual async Task<TEntityDto> CreateAsync(TCreateInput input)
        {
            var entity = MapTo<TEntity>(input);
            await Repository.AddAsync(entity);
            return MapTo<TEntityDto>(entity);
        }

        public virtual async Task<TEntityDto> UpdateAsync(Guid id, TUpdateInput input)
        {
            var entity = await Repository.GetByIdAsync(id);
            MapTo(input, entity);
            await Repository.UpdateAsync(entity);
            return MapTo<TEntityDto>(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await Repository.DeleteAsync(id);
        }

        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query)
        {
            // Implement default sorting logic here
            return query;
        }

        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, PagedRequestDto input)
        {
            return query.Skip(input.SkipCount).Take(input.MaxResultCount);
        }
    }
}
