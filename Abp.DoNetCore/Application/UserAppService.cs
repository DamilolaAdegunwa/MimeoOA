using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.DoNetCore.Application.Dtos;
using Abp.DoNetCore.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void CreateUser(UserCreateInput input)
        {
            var temp = Mapper.Map<UserCreateInput, User>(input);
            //this.userRepository.Insert(new User { AccountCode = "test123", AccountEmail = "test@mimeo.com", AccountPhone = "12345678", CreateTime = DateTime.UtcNow, LastLoginIP = "127.0.0.1", LastLoginTime = DateTime.UtcNow, Password = "123456", Status = "Active" });
        }
    }
}
