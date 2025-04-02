using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    public class CustomPropertyMappingProvider:PropertyMappingProvider
    {
        public override IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
        {
           return memberInfo.Name==nameof(Precedence.AskSerializer)?new PropertyMapping { Name="ask"}:base.CreatePropertyMapping(memberInfo);
        }
    }
}
