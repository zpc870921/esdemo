using System;
using System.Collections.Generic;
using System.Text;
using Nest;
using Newtonsoft.Json;

namespace ESDemo.Models
{
    class Precedence
    {
        [Text(Name = "renamedIgnoresNest")]
        [PropertyName("renamedIgnoresJsonProperty"), JsonProperty("renamedIgnoresJsonProperty")]
        public string RenamedOnConnectionSettings { get; set; }

        [Text(Name = "nestAtt")]
        [PropertyName("nestProp"), JsonProperty("jsonProp")]
        public string NestAttribute { get; set; }

        [PropertyName("nestProp"), JsonProperty("jsonProp")]
        public string NestProperty { get; set; }

        [JsonProperty("jsonProp")]
        public string JsonProperty { get; set; }

        [PropertyName("dontaskme"), JsonProperty("dontaskme")]
        public string AskSerializer { get; set; }

        public string DefaultFieldNameInferrer { get; set; }
    }
}
