using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingList.Models
{
    // DEMO 0: DebuggerDisplay
    [DebuggerDisplay("{Title}")]
    public class Book
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty("pages", NullValueHandling = NullValueHandling.Ignore)]
        public string Pages { get; set; }

        [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
        public int Year { get; set; }

        [JsonProperty("imageLink", NullValueHandling = NullValueHandling.Ignore)]
        public string Cover { get; set; }

        [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore)]
        public string Link { get; set; }

        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        public int TimesRead { get; set; }

        public DateTime LastReadDate { get; set; }

        public string FinishedBookString()
        {
            // DEMO 3: ReturnValue, Step Into Specific, Format Spec - Add ToShortDateString() to LastReadDate; 
            // Remove ToShortDateString() to include the time book was finished
            return "last finished " + this.Title + " on " + LastReadDate.ToShortDateString();
        }
    }
}
