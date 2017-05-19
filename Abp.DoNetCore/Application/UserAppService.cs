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
        private readonly IRepository<User> userRepository;

        public UserAppService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void CreateUser()
        {
            this.userRepository.Insert(new User { Id =1, Name = "test" });
        }
    }
}
