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

        public void ChooseMaximums()
        {
            IndividualPath.max_cost = 0;

            DateTime minDate = new DateTime(9999, 9, 9);
            DateTime maxDate = new DateTime(1, 1, 1);

            foreach (List<EventBL> lEv in this.AllEvents.Values)
            {
                foreach (EventBL eb in lEv)
                {
                    if (eb.Price > IndividualPath.max_cost)
                    {
                        IndividualPath.max_cost = (int)Math.Ceiling(eb.Price);
                    }

                    if (minDate > eb.Date)
                    {
                        minDate = eb.Date;
                    }

                    if (maxDate < eb.Date)
                    {
                        maxDate = eb.Date;
                    }
                }
            }

            IndividualPath.max_date_distance = (maxDate - minDate).Days;
        }
    }
}