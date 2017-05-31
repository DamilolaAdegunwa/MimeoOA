﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Abp.Collections.Extensions;

namespace Abp.AutoMapper
{
    public class AutoMapToAttribute : AutoMapAttributeBase
    {
        public MemberList MemberList { get; set; } = MemberList.Source;
        public AutoMapToAttribute(params Type[] targetTypes):base(targetTypes)
        {

        }
        public AutoMapToAttribute(MemberList memberList, params Type[] targetTypes)
           : this(targetTypes)
        {
            MemberList = memberList;
        }
        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            if (TargetTypes.IsNullOrEmpty())
            {
                return;
            }

            foreach (var targetType in TargetTypes)
            {
                configuration.CreateMap(type, targetType, MemberList);
            }
        }
    }
}
