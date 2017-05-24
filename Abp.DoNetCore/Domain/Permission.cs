using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Domain
{
   public class Permission:Entity
    {
        public int Name { get; set; }
        public DateTime? CreateTime { get; set; }
        public int CreateByUserId { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int ModifyByUserId { get; set; }
        public int Status { get; set; }
        public string ProrityLevel { get; set; }
    }
}
