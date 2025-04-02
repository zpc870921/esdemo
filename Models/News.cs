using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    [ElasticsearchType(Name="news")]
    public class News
    {
        [Keyword(Name ="Id")]
        public int Id { get; set; }

        [Text(Name="title")]
        public string Title { get; set; }

        [Text(Name = "content")]
        public string Content { get; set; }

        public override string ToString()
        {
            return $"Id:{this.Id},title:{this.Title},content:{this.Content}";
        }
    }
}
