using Abp.DoNetCore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abp.DoNetCore.Application
{
    public interface IUserAppService
    {
        void CreateUser(UserCreateInput input);

      Task<bool> UPdateUser(UserCreateInput input);
    }
}
