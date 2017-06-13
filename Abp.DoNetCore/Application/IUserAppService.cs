using Abp.DoNetCore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abp.DoNetCore.Application
{
    public interface IUserAppService
    {
        Task<bool> CreateUserAsync(UserInput input);

        Task<bool> UpdateUserAsync(UserInput input);

        Task<UserInput> RemoveUserAsync(Guid id);

        Task<UserInput> GetUserById(Guid id);

        Task<IEnumerable<UserInput>> GetUsers(int pageIndex,int pageSize);
    }
}
