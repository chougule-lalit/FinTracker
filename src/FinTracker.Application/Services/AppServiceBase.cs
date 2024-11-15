using AutoMapper;
using FinTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Services
{
    public abstract class AppServiceBase
    {
        protected IMapper Mapper { get; }
        protected ICurrentUser CurrentUser { get; }

        protected AppServiceBase(
            IMapper mapper,
            ICurrentUser currentUser)
        {
            Mapper = mapper;
            CurrentUser = currentUser;
        }

        protected virtual TDestination MapTo<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        protected virtual void MapTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            Mapper.Map(source, destination);
        }

        protected virtual IQueryable<TDestination> MapTo<TDestination>(IQueryable source)
        {
            return Mapper.ProjectTo<TDestination>(source);
        }

        protected virtual void CheckPermission(string permissionName)
        {
            // Implement permission checking logic
        }

        protected virtual async Task CheckPermissionAsync(string permissionName)
        {
            await Task.CompletedTask; // Placeholder for actual implementation
        }
    }
}
