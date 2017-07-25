using Abp.DoNetCore;
using Abp.DoNetCore.Application;
using Abp.DoNetCore.Application.Dtos;
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
        public UserController(IUserAppService userAppService)
        {
            this.userAppService = userAppService;
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
            return Ok(await this.userAppService.AuthorizationOfUser(userInput));
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

    }
}
