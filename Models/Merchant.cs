using System;
using System.Collections.Generic;
using System.Text;
using Nest;
using Newtonsoft.Json;

namespace ESDemo.Models
{
    
    public class Merchant
    {
        public int Id { get; set; }

        [Text(Analyzer ="ik_max_word",SearchAnalyzer ="ik_max_word")]
        public string Name { get; set; }

        public int District { get; set; }

        [GeoPoint]
        public GeoLocation Location { get; set; }

        public int Status { get; set; }

        public int Category { get; set; }

        public DateTime CreateDate { get; set; }

        [Ignore]
        public double Distance { get; set; }

        public string Address { get; set; }

        public int Level { get; set; }

        [Keyword]
        public string Img { get; set; }
    }
}
