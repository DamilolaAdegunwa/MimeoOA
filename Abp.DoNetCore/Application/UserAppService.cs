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
        public async Task<bool> CreateUserAsync(UserInput input)
        {
            var userEntities = await this.userRepository.GetAllListAsync(item => item.AccountEmail == input.AccountEmail);
            if (userEntities.Count > 0)
            {
                throw new ArgumentException($"The user {input.AccountEmail} have been exist");
            }
            var userEntity = Mapper.Map<UserInput, User>(input);
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
        public async Task<bool> UpdateUserAsync(UserInput input)
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

        public async Task<UserInput> RemoveUserAsync(Guid id)
        {
            var result = await this.userRepository.UpdateAsync(id, item => { item.IsDeleted = true; return userRepository.UpdateAsync(item); });

            return Mapper.Map<User, UserInput>(result);
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<UserInput> GetUserById(Guid id)
        {
            return Mapper.Map<User, UserInput>(await this.userRepository.GetAsync(id));
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<IEnumerable<UserInput>> GetUsers(int pageIndex, int pageSize)
        {
            var users = this.userRepository.GetAll().Take(pageIndex * pageSize).Skip(pageSize * (pageIndex - 1)).ToList();
            List<UserInput> userInputs = new List<UserInput>();
            users.ForEach(item => userInputs.Add(Mapper.Map<User, UserInput>(item)));
            return userInputs;
        }
        /// <summary>
        /// It should be returned this current user entity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AuthorizationOfUser(UserInput input)
        {
            var users = await this.userRepository.GetAllListAsync(item => item.AccountEmail == input.AccountEmail || item.AccountCode == input.AccountCode || item.AccountPhone == input.AccountPhone);
            if (users.Count > 0)
            {
                //validate the password
                var password = users.FirstOrDefault().Password;
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
