using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.DAL
{
    public partial class Event
    {
        //public int ID { get; set; }
        //public int LocationID { get; set; }
        //public int TypeID { get; set; }
        //public DateTime Date { get; set; }
        //public String Title { get; set; }
        //public Decimal Price { get; set; }
        //public String Coin { get; set; }

        public static List<EventBL> GetEvents(DateTime sDate, DateTime eDate, String loc)
        {
            return null;
        }

        public static List<EventBL> GetEvents(DateTime sDate, DateTime eDate, String loc, List<EventSpecification> evSpecs)
        {
        //    List<EventBL> desired;
        //    DBEventusDataContext ctx = new DBEventusDataContext();
        //    var q = from data in ctx.Events
        //            select new
        //            {
        //                col1 = data.ID,
        //                col2 = "a",
        //                col3 = "a",
        //                col4 = data.Date,
        //                col5 = "a",
        //                col6 = 2.0,
        //                col7 = "a"
        //            };

        //    desired = (List<EventBL>)(q);
            
        //    return desired;
        //    //EventBL e1 = new EventBL();
        //    //e1.Location = "brazil";
        //    //e1.Type = "music";            
        //    //e1.Date = new DateTime(2016, 5, 30);
        //    //e1.Title = "rihana";
        //    //e1.Price = 3;
        //    //e1.Coin = "Peso";
        //    //e1.ID = 1;

            return new List<EventBL>() { };
        }

        public static List<EventBL> GetEvents(string sDate, string eDate, string loc, EventSpecification spec)
        {
            DateTime dtStart = new DateTime(1, 1, 1);
            DateTime dtEnd   = new DateTime(9999, 9, 9);
            List<EventBL> EventResults = new List<EventBL>();

            DBEventusDataContext db = new DBEventusDataContext();

            if (sDate != null && sDate != "")
                dtStart = DateTime.Parse(sDate);

            if (eDate != null && eDate != "")
                dtEnd = DateTime.Parse(eDate);

            var q2 =
               from data in db.Events
               join loc1 in db.Locations on data.LocationID equals loc1.Id
               join type in db.EventTypes on data.TypeID equals type.ID
               where (data.Date >= dtStart) && (data.Date <= dtEnd) && 
                     (loc == null || loc == "" || loc == "CA" ||loc1.Name == loc) && 
                     type.Name == spec.Type && (spec.FreeText == "" || data.Title.Contains(spec.FreeText))
               select new {id = data.ID, locID = loc1.Id, locName = loc1.Name, typeID = type.ID, typeName = type.Name, date = data.Date, title = data.Title, price = data.Price, coin = data.Coin };

            foreach (var item in q2)
            {
                Location l = new Location(item.locID, item.locName);
                EventType t = new EventType(item.typeID, item.typeName);
                EventBL eb = new EventBL(item.id, l, t, item.date, item.title, item.price, item.coin);

                EventResults.Add(eb);
            }

            return EventResults;
        }
    }
}