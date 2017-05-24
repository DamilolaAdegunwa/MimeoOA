using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Domain
{
   public class RolePermission:Entity
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime? CreateTime { get; set; }
        public int CreateByUserId { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int ModifyByUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
