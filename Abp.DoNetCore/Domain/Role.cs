using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Domain
{

    public class Role : Entity
    {
        public Guid DepartMentId { set; get; }
        public DateTime? CreateTime { set; get; }
        public Guid CreateByUserId { set; get; }
        public DateTime? ModifyTime { set; get; }
        public Guid ModifyByUserId { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public bool IsDeleted { set; get; }
    }
}
