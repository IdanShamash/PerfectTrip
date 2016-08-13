using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Collections;

namespace Eventus.Models
{
    public partial class Event
    {
        public static List<Event> GetEvents(DateTime dateIn, DateTime dateOut)
        {
            List<Event> desired;

            LinQDataContext ctx = new LinQDataContext();
            var queryDesiredEvents = from eve in ctx.Events
                                                    where eve.date >= dateIn && eve.date <= dateOut
                                                    select eve;

            desired = new List<Event>(queryDesiredEvents);

            return desired;
        }
    }
}