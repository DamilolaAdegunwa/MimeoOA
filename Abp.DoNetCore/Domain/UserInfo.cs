using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Domain
{
    public class UserInfo : Entity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string CardType { get; set; }
        public int CardNo { get; set; }
        public string Phone { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public DateTime? birthDate { get; set; }
        public string ExtendInfo { get; set; }
    }
}
