using System;
using System.Collections.Generic;

namespace DayProgress.Data
{
    public class ProgressEntry
    {
        public int Id {get; set;}
        public string Content {get; set;}
        public string ContentToShow
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Content))
                    return string.Empty;
                return Content.Length <= 150 ? Content : Content.Substring(0, 150) + "...";
            }
        }
        public DateTime WhenCreated {get; set;}
        public IEnumerable<ProgressTag> Tags {get; set;}
    }
}