using Abp.DoNetCore;
using Abp.DoNetCore.Application;
using Abp.DoNetCore.Application.Dtos;
using Abp.DoNetCore.Application.Dtos.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimeoOAWeb.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserAppService userAppService;
        private readonly IAbpAuthorizationService abpAuthorizationService;
        public UserController(IUserAppService userAppService,IAbpAuthorizationService abpAuthorizationService)
        {
            this.userAppService = userAppService;
            this.abpAuthorizationService = abpAuthorizationService;
            var request = this.Request;
        }

        [HttpPost("signUp")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser userInput)
        {
            return Ok(await this.userAppService.CreateUserAsync(userInput));

        }
        [HttpPut("update")]
        [Authorize(Policy = "MimeoOA")]
        public async Task<IActionResult> UpdateUser([FromBody] ApplicationUser userInput)
        {
            return Ok(await this.userAppService.UpdateUserAsync(userInput));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] ApplicationUser userInput)
        {
            //return Ok(await this.userAppService.AuthorizationOfUser(userInput));
            return Ok(await abpAuthorizationService.AuthorizationUser(userInput));
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "MimeoOA")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            return Ok(await this.userAppService.RemoveUserAsync(id));
        }

        [HttpGet("getUser/{id}")]
        [Authorize(Policy = "MimeoOA")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            return Ok(await this.userAppService.GetUserById(id));
        }

        [HttpGet("getUsers/{pageIndex}/{pageSize}")]
        [Authorize(Policy = "MimeoOA")]
        public async Task<IActionResult> GetUsers(int pageIndex, int pageSize)
        {
            return Ok(await this.userAppService.GetUsers(pageIndex, pageSize));
        }

        [HttpPost("addNewRole/{id}")]
        [Authorize(Policy = "MimeoOA")]
        public async Task<IActionResult> AddRole(RoleDataTransferObject roleInfos)
        {
            return Ok();
        }
    }
}
