using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.DAL
{
    public class SavedEvent
    {
        public int UserID { get; set; }
        public int EventID { get; set; }

        public static List<EventBL> GetEvents(String user) 
        {
            // TODO: Select and Join by user id from history and events 
            return new List<EventBL>() { }; ; 
        }

        public static bool SaveEvents(String user, List<EventBL> evs) 
        {
            // TODO: Add rows to history table by user id & events id
            return false;
        }
    }
}