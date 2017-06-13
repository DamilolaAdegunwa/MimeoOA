using Abp.AutoMapper;
using Abp.DoNetCore.Common;
using Abp.DoNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Application.Dtos
{
    [AutoMap(typeof(User))]
    public class UserInput
    {
        public string AccountEmail { get; set; }
        public string AccountCode { get; set; }
        public string AccountPhone { get; set; }
        public string Password { get; set; }
        public UserActiveStatus Status { get; set; }
    }
}
