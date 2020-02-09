using System;
using System.Collections.Generic;

namespace DayProgress.Data
{
    public class ProgressEntry
    {
        public int Id {get; set;}
        public string Content {get; set;}
        public DateTime WhenCreated {get; set;}
        public IEnumerable<ProgressTag> Tags {get; set;}
    }
}