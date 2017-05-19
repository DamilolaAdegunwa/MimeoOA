using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.DoNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Application
{
    public class UserAppService:IUserAppService
    {
        //private readonly 
        private readonly IRepository<User,Guid> userRepository;

        public UserAppService(IRepository<User, Guid> userRepository)
        {
            this.userRepository = userRepository;
        }
    }
}
