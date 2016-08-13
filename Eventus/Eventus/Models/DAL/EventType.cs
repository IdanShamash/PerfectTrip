using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.DAL
{
    public partial class EventType
    {
        //public int ID { get; set; }
        //public String Name { get; set; }

        public EventType(int id, String name)
        {
            this.ID = id;
            this.Name = name;
        }

        public static List<EventType> GetTypes()
        {
            List<EventType> types;

            DBEventusDataContext db = new DBEventusDataContext();
            var q = from data in db.EventTypes
                    select data;

            types = new List<EventType>(q);

            return types;
        }
    }
}