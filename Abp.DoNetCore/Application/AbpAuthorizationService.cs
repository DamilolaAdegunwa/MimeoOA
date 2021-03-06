﻿using Abp.Domain.Repositories;
using Abp.DoNetCore.Application.Dtos;
using Abp.DoNetCore.Common;
using Abp.DoNetCore.Domain;
using Abp.Json;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Abp.DoNetCore.Application
{
    public class AbpAuthorizationService : IAbpAuthorizationService
    {
        private readonly IUserAppService userAppService;
        private readonly JwtIssuerOptions _jwtOptions;
        public AbpAuthorizationService(IUserAppService userAppService, IOptions<JwtIssuerOptions> jwtOptions)
        {
            this.userAppService = userAppService;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<ApplicationUser> AuthorizationUser(ApplicationUser userInfo)
        {
            var userInfoCorrect = await userAppService.AuthorizationOfUser(userInfo);
            if (!userInfoCorrect)
            {
                throw new ArgumentException($"Invalid user: {userInfo.AccountCode} {userInfo.AccountEmail} {userInfo.AccountPhone} or password {userInfo.Password}");
            }
            userInfo.MimeoToken = await ProductedMimeoOAToken(userInfo.AccountEmail);
            return userInfo;
        }

        private async Task<string> ProductedMimeoOAToken(string userName)
        {
            var identity = await GetClaimsIdentity(userName);
            var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, userName),
        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
        identity.FindFirst("MimeoUser") };
            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
            // Serialize and return the response
            //var response = new
            //{
            //    access_token = encodedJwt,
            //    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            //};

            //var json = JsonSerializationHelper.Seialize(response);

            //return json;
        }
        private long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[] {
                    new Claim("MimeoUser","test")
                });
            }
            return null;
        }
    }
}
