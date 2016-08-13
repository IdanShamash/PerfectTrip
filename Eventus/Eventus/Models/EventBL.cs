using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eventus.Models.DAL;

namespace Eventus.Models
{
    public class EventBL : IEquatable<EventBL>
    {
        public int      ID {    get; set; }
       // public String   Location { get; set; }
       // public String   Type { get; set; }
        public Location Location { get; set; }
        public EventType Type { get; set; }
        public DateTime Date { get; set; }
        public String   Title { get; set; }
        public Decimal  Price { get; set; }
        public String   Coin { get; set; }
        public string stringDate { get; set; }

        public EventBL()
        {

        }

        public override bool Equals(Object obj)
        {
            EventBL ev = (EventBL)obj;

            return (this.Equals(ev));
        }

        public bool Equals(EventBL ev)
        {
            if (this.ID == ev.ID)
                return true;

            else return false;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

        public EventBL(int id, Location loc, EventType type, DateTime date, String title, Decimal price, String coin)
        {
            this.ID = id;
            this.Location = loc;
            this.Type = type;
            this.Date = date;
            this.Title = title;
            this.Price = price;
            this.Coin = coin;
            this.stringDate = date.ToShortDateString();
        }

        //public static List<EventBL> doSearch(DateTime dateIn, DateTime dateOut, String loc)
        //{
        //    List<EventBL> lEves = Event.GetEvents(dateIn, dateOut, loc); // Replacement for WWW search

        //    return lEves;
        //}

        //public static List<EventBL> doSearch(DateTime dateIn, DateTime dateOut, String loc, List<EventSpecification> specs)
        //{
        //    List<EventBL> resEvents = new List<EventBL>();
        //    List<EventBL> lEves = Event.GetEvents(dateIn, dateOut, loc, specs); // Replacement for WWW search

        //    return resEvents;
        //}
    }
}