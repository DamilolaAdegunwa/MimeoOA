using Abp.DoNetCore.Application.Dtos;
using Abp.DoNetCore.Application.Dtos.Users;
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

        Task<bool> AddNewRoleAsync(RoleDataTransferObject roleInfo);

        Task<RoleDataTransferObject> UpdateRoleAsync(RoleDataTransferObject roleInfo);

        Task<bool> RemoveRoleAsync(Guid roleId);
    }
}
