using Eventus.Models.GeneticAlgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models
{
    public class EventsContainer
    {
        //public List<List<EventBL>> AllEvents { get; set; }
        public Dictionary<EventSpecification, List<EventBL>> AllEvents;
        public static Random Rand = new Random(); 

        public EventsContainer()
        {
            this.AllEvents = new Dictionary<EventSpecification, List<EventBL>>();
        }

        // creates random chromozed individual
        public IndividualPath ComposeRandomPath()
        {
            //IndividualPath ip = new IndividualPath();
            Dictionary<EventSpecification, EventBL> pe = new Dictionary<EventSpecification, EventBL>();

            foreach (EventSpecification spec in this.AllEvents.Keys)
            {
                List<EventBL> lse = this.AllEvents[spec];
                int r = Rand.Next(lse.Count);

                pe.Add(spec, lse[r]);
                //ip.PathEvents.Add(spec, lse[r]);
            }

            IndividualPath ip = new IndividualPath(pe); // if error delete this line

            return ip;
        }

        public EventBL GetRandomEventForSpec(EventSpecification spec)
        {
            int chosen = Rand.Next(this.AllEvents[spec].Count);
            return this.AllEvents[spec].ElementAt<EventBL>(chosen);
        }
    }
}