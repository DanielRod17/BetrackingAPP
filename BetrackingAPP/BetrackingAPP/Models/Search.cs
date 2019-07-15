using System;
using System.Collections.Generic;
using System.Text;

namespace BetrackingAPP.Models
{
    class Searches
    {
        public Search[] timecard { get; set; }
    }

    public class Search
    {
        public string key { get; set; }
        public string name { get; set; }
    }
}
