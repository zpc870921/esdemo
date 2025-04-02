using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    public class EverythingIsATextPropertyVisitor:NoopPropertyVisitor
    {
        public override IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) =>
            new TextProperty();
    }
}
