using FinTracker.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Contracts
{
    public interface ICrudAppService<TEntityDto, TCreateInput, TUpdateInput>
    where TEntityDto : class
    where TCreateInput : class
    where TUpdateInput : class
    {
        Task<TEntityDto> GetAsync(Guid id);
        Task<PagedResultDto<TEntityDto>> GetListAsync(PagedRequestDto input);
        Task<TEntityDto> CreateAsync(TCreateInput input);
        Task<TEntityDto> UpdateAsync(Guid id, TUpdateInput input);
        Task DeleteAsync(Guid id);
    }
}
