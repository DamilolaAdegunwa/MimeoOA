using Abp.DoNetCore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abp.DoNetCore.Application
{
    public interface IUserAppService
    {
        Task<bool> CreateUserAsync(ApplicationUser input);

        Task<bool> UpdateUserAsync(ApplicationUser input);

        Task<ApplicationUser> RemoveUserAsync(Guid id);

        Task<ApplicationUser> GetUserById(Guid id);

        Task<IEnumerable<ApplicationUser>> GetUsers(int pageIndex,int pageSize);

        Task<bool> AuthorizationOfUser(ApplicationUser input);
    }
}
