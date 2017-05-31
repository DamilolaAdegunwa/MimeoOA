using Abp.DoNetCore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Application
{
    public interface IUserAppService
    {
        void CreateUser(UserCreateInput input);
    }
}
