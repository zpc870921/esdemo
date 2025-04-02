using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    [ElasticsearchType(Name="doc")]
    public class StoIndex
    {
        [Keyword(Name="id")]
        public string Id { get; set; }

        [Text(Name ="title")]
        public string Title { get; set; }

        [Date(Name ="post_Date")]
        public DateTime Post_Date { get; set; }

        [Number(NumberType.Integer,Name ="view_cnt")]
        public int View_CntZ { get; set; }
    }

    
    public class NewsInfo
    {
        public string ArticleID { get; set; }

        
        public string Content { get; set; }

       
        public long Follower_Num { get; set; }

       
        public bool Hidden { get; set; }

        
        public DateTime Post_Date { get; set; }

        
        public string Sub_Title { get; set; }

       
        public string[] Tag { get; set; }

        
        public long Tag_Cnt { get; set; }

        
        public string Title { get; set; }

        
        public long UserID { get; set; }

        
        public long View_Cnt { get; set; }
    }


    [ElasticsearchType(Name ="article")]
    public class Article
    {
        [Keyword(Name ="articleID")]
        public string ArticleID { get; set; }

        [Text(Name="content")]
        public string Content { get; set; }

        [Number(NumberType.Long,Name ="follower_num")]
        public long Follower_Num { get; set; }

        [Boolean(Name ="hidden")]
        public bool Hidden { get; set; }

        [Date(Name = "post_Date")]
        public DateTime Post_Date { get; set; }

        [Text(Name ="sub_title")]
        public string Sub_Title { get; set; }

        [Text(Name ="tag")]
        public string[] Tag { get; set; }

        [Number(NumberType.Long,Name ="tag_cnt")]
        public long Tag_Cnt { get; set; }

        [Text(Name ="title")]
        public string Title { get; set; }

        [Number(NumberType.Long,Name ="userID")]
        public long UserID { get; set; }

        [Number(NumberType.Long,Name ="view_cnt")]
        public long View_Cnt { get; set; }
    }
}
