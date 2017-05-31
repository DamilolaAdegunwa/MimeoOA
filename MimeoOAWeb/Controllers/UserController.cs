using Abp.DoNetCore.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimeoOAWeb.Controllers
{
    [Route("api/[controller]")]
    public class UserController:Controller
    {
        private readonly IUserAppService userAppService;
       public UserController(IUserAppService userAppService)
        {
            this.userAppService = userAppService;
        }


        [HttpGet("{id}")]
        public bool CreateUser()
        {
            this.userAppService.CreateUser(new Abp.DoNetCore.Application.Dtos.UserCreateInput {AccountCode="test",AccountEmail="test@mimeo.com",AccountPhone="12345678",Password="test" });
            return false;
        }
    }
}
