using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    [ElasticsearchType(Name = "question")]
    public class question
    {
        public JoinField join { get; set; }
        [Text(Name = "body")]
        public string Body { get; set; }

        [Text(Name ="title")]
        public string Title { get; set; }

        [Keyword(Name ="tags")]
        public string[] Tags { get; set; }
    }

    [ElasticsearchType(Name="answer")]
    public class answer
    {
        public JoinField join { get; set; }

        [Text(Name ="owner")]
        public Owner Owner { get; set; }

        [Text(Name ="body")]
        public string Body { get; set; }
        
        [Date(Name= "creation_date")]
        public  DateTime creation_date { get; set; }
    }

    public class Owner
    {
        public string location { get; set; }
        public string display_name { get; set; }
        public int id { get; set; }
    }
}
