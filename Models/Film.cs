using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    [ElasticsearchType(Name="doc")]
    public class Film
    {
        [Text(Name="title")]
        public string Title { get; set; }

        [Keyword(Name ="category")]
        public string Category { get; set; }

        [Keyword(Name = "tags")]
        public string[] Tags { get; set; }

        [Number(NumberType.Integer,Name="rate")]
        public int Rate { get; set; }

        [Keyword(Name = "actors")]
        public string[] Actors { get; set; }

        public override string ToString()
        {
            return $"title:{this.Title},category:{this.Category}";
        }
    }
}
