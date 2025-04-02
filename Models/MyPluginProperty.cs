using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    public class MyPluginProperty : IProperty
    {
        public MyPluginProperty(string name,string language)
        {
            this.Name = name;
            this.Language = language;
            this.Numeric = true;
        }
        public PropertyName Name { get; set; }
        public string Type{ get; set; }="my_plugin_property";
        public IDictionary<string, object> LocalMetadata{ get; set; }

        [PropertyName("language")]
        public string Language { get; set; }

        [PropertyName("numeric")]
        public bool Numeric { get; set; }
    }
}
