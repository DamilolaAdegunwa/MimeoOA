using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.DoNetCore.Domain
{
    [Table("MO_User")]
    public class User:Entity
    {
        public string Name { get; set; }

    }
}
