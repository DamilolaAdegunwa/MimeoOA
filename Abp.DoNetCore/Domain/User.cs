using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.DoNetCore.Domain
{
    public class User : Entity
    {
        public string AccountEmail { get; set; }
        public string AccountCode { get; set; }
        public string AccountPhone { get; set; }
        public string Password { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ModifyTime { get; set; }
        public int ModifyByUserId { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginIP { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
