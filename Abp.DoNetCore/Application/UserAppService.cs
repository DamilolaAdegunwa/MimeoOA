using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.DoNetCore.Application.Dtos;
using Abp.DoNetCore.Domain;
using Abp.Utilities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.DoNetCore.Application
{
    public class UserAppService : DomainService, IUserAppService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserInfo> userInfoRepository;
        private readonly IRepository<Role> roleRepository;
        private readonly IRepository<UserRole> userRoleRepository;
        private readonly IRepository<RolePermission> rolePermissionRepository;
        private readonly IRepository<Permission> permissionRepository;

        public UserAppService(IRepository<User> userRepository, IRepository<UserInfo> userInfoRepository, IRepository<Role> roleRepository, IRepository<UserRole> userRoleRepository, IRepository<RolePermission> rolePermissionRepository, IRepository<Permission> permissionRepository)
        {
            this.userRepository = userRepository;
            this.userInfoRepository = userInfoRepository;
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;
            this.rolePermissionRepository = rolePermissionRepository;
            this.permissionRepository = permissionRepository;
        }
        public async Task<bool> CreateUserAsync(ApplicationUser input)
        {
            var userEntities = await this.userRepository.GetAllListAsync(item => item.AccountEmail == input.AccountEmail);
            if (userEntities.Count > 0)
            {
                throw new ArgumentException($"The user {input.AccountEmail} have been exist");
            }
            var userEntity = Mapper.Map<ApplicationUser, User>(input);
            userEntity.Id = Guid.NewGuid();
            userEntity.LastLoginIP = "127.0.0.1";
            userEntity.CreateTime = DateTime.UtcNow;
            userEntity.LastLoginTime = DateTime.UtcNow;
            userEntity.Password = HashUtility.CreateHash(input.Password);
            var result = await this.userRepository.InsertAsync(userEntity);
            if (result != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateUserAsync(ApplicationUser input)
        {
            var userEntitie = await this.userRepository.GetAllListAsync(item => item.AccountEmail == input.AccountEmail);
            if (userEntitie.Count <= 0)
            {
                return false;
            }
            var result = await this.userRepository.UpdateAsync(userEntitie.FirstOrDefault());
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public async Task<ApplicationUser> RemoveUserAsync(Guid id)
        {
            var result = await this.userRepository.UpdateAsync(id, item => { item.IsDeleted = true; return userRepository.UpdateAsync(item); });

            return Mapper.Map<User, ApplicationUser>(result);
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<ApplicationUser> GetUserById(Guid id)
        {
            return Mapper.Map<User, ApplicationUser>(await this.userRepository.GetAsync(id));
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<IEnumerable<ApplicationUser>> GetUsers(int pageIndex, int pageSize)
        {
            var users = this.userRepository.GetAll().Take(pageIndex * pageSize).Skip(pageSize * (pageIndex - 1)).ToList();
            List<ApplicationUser> userInputs = new List<ApplicationUser>();
            users.ForEach(item => userInputs.Add(Mapper.Map<User, ApplicationUser>(item)));
            return userInputs;
        }
        [UnitOfWork(IsDisabled = true)]
        private async Task<User> ValidateLoginUser(ApplicationUser input)
        {
            var users = await this.userRepository.GetAllListAsync(item => item.AccountEmail == input.AccountEmail || item.AccountCode == input.AccountCode || item.AccountPhone == input.AccountPhone);
            if (users.Count>0)
            {
                return users.FirstOrDefault();
            }
            return null;
        }
        /// <summary>
        /// It should be returned this current user entity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AuthorizationOfUser(ApplicationUser input)
        {
            var haveLoginUser = await ValidateLoginUser(input);
            if (haveLoginUser!=null)
            {
                //validate the password
                var password = haveLoginUser.Password;
                var checkResult = HashUtility.ValidatePassword(input.Password, password);
                if (checkResult)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
