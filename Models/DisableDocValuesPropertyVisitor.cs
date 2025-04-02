using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    public class DisableDocValuesPropertyVisitor:NoopPropertyVisitor
    {
        public override void Visit(IBooleanProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
        {
            type.DocValues = false;
        }

        public override void Visit(INumberProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
        {
            type.DocValues = false;
        }
    }
}
