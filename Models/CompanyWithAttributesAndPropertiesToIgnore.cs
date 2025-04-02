using System;
using System.Collections.Generic;
using System.Text;
using Nest;
using Newtonsoft.Json;

namespace ESDemo.Models
{
    [ElasticsearchType(Name ="company")]
    public class CompanyWithAttributesAndPropertiesToIgnore
    {
        public string Name { get; set; }

        [Text(Ignore =true)]
        public string PropertyToIgnore { get; set; }

        
        public string AnotherToIgnore { get; set; }

        [Ignore, JsonIgnore]
        public string JsonIngoreProperty { get; set; }
    }
}
