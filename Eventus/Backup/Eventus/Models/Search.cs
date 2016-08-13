using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models
{
    public partial class Search
    {
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public List<Event> desiredEvents { get; set; }

        public void doSearch()
        {
            this.desiredEvents = Event.GetEvents(this.dateStart, this.dateEnd);
        }

        public static List<Event> doSearch(DateTime dateIn, DateTime dateOut)
        {
            return Event.GetEvents(dateIn, dateOut);
        }
    }
}