using Abp.DoNetCore;
using Abp.DoNetCore.Application;
using Abp.DoNetCore.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimeoOAWeb.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAbpAuthorizationService authorizationService;
        public AuthController(IAbpAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        [HttpGet("getToken")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken(ApplicationUser user)
        {
            return Ok(await this.authorizationService.GetToken(user));
        }
    }
}
