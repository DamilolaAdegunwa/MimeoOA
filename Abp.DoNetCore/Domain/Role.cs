using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Domain
{

    public class Role : Entity
    {
        public int DepartMentId { set; get; }
        public DateTime? CreateTime { set; get; }
        public int CreateByUserId { set; get; }
        public int ModifyTime { set; get; }
        public DateTime? ModifyByUserId { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public bool IsDeleted { set; get; }
    }
}
