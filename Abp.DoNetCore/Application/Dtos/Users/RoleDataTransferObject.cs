using Abp.AutoMapper;
using Abp.DoNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Application.Dtos.Users
{
    [AutoMap(typeof(Role))]
    public class RoleDataTransferObject
    {
        public Guid Id { get; set; }
        public Guid CreateByUserId { set; get; }
        public Guid ModifyByUserId { get; set; }
        public string Code { set; get; }
        public string Name { set; get; }
        public bool IsDeleted { set; get; }

        public void Mapping(Role roleEntity)
        {
            roleEntity.Code = this.Code;
            roleEntity.Name = this.Name;
            roleEntity.CreateByUserId = this.CreateByUserId;
            roleEntity.ModifyByUserId = this.ModifyByUserId;
            roleEntity.IsDeleted = this.IsDeleted;
        }
    }
}
