using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.DAL
{
    public partial class Location
    {
        //public int ID { get; set; }
        //public String Name { get; set; }

        public Location(int ID, String name)
        {
            this.Id = ID;
            this.Name = name;
            this.GeoHorizontal = 0;
            this.GeoVertical = 0;
        }

        public static List<Location> GetLocations()
        {
            List<Location> locs;

            DBEventusDataContext db = new DBEventusDataContext();
            var q = from data in db.Locations
                    select data;

            locs = new List<Location>(q);

            return locs;
        }
    }
}