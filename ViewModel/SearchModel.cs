using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ESDemo.ViewModel
{
    public class SearchModel
    {
        public string Keyword { get; set; }
        public int District { get; set; }
        public IEnumerable<int> Category { get; set; }
        public IEnumerable<int> Status { get; set; }

        public int PageIndex { get; set; } = 1;

        public GeoLocation Top_Left { get; set; }
        public GeoLocation Bottom_Right { get; set; }
    }
}
