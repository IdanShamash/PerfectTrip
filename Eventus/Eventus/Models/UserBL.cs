using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eventus.Models.DAL;

namespace Eventus.Models
{
    public class UserBL
    {
        public String username { get; set; }

        public static List<EventBL> GetHistory(String user)
        {
            return SavedEvent.GetEvents(user);
        }
    }
}