using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Eventus.Models.DAL;

namespace Eventus.Context
{
    public class EventContext : DbContext
    {
        public DbSet<Event>      Events { get; set; }
        public DbSet<EventType>  EventTypes { get; set; }
        public DbSet<SavedEvent> SavedEvents { get; set; }
        public DbSet<Location>   Locations { get; set; }
        public DbSet<User>       Users { get; set; }
    }
}