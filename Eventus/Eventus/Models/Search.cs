using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eventus.Models.DAL;
using Eventus.Models.GeneticAlgo;

namespace Eventus.Models
{
    public class Search
    {
        public String startDate { get; set; }
        public String endDate { get; set; }
        public String location { get; set; }

        public string[] EventType { get; set; }
        public string[] freeText { get; set; }
        public int[] Weight { get; set; }

        public List<EventSpecification> specs { get; set; }

        public Search()
        {
            int i;
        }
        public void makeSpecifications()
        {
            this.specs = new List<EventSpecification>();

            for (int i = 0; i < this.EventType.Length; i++ )
            {
                try
                {
                    this.specs.Add(new EventSpecification(i, this.EventType[i], this.freeText[i]));
                }
                catch (Exception ex)
                {
                    if (ex is NullReferenceException || ex is IndexOutOfRangeException)
                    {
                        this.specs.Add(new EventSpecification(i, this.EventType[i]));
                    }
                    else
                    {
                        throw;
                    }
                }                
            }
        }

        public  List<EventBL> doSearch(DateTime dateIn, DateTime dateOut, String loc)
        {
            List<EventBL> lEves = Event.GetEvents(dateIn, dateOut, loc); // Replacement for WWW search

            return lEves;
        }

        public EventsContainer doSearch(DateTime dateIn, DateTime dateOut, String loc, List<EventSpecification> specs)
        {
            EventsContainer ec = new EventsContainer();
            
            //Dictionary<int, List<EventBL>> d = new Dictionary<int,List<EventBL>>();
            //int id = 0;

            foreach (EventSpecification spec in specs)
            {
                List<EventBL> lEves = Event.GetEvents(dateIn, dateOut, loc, specs); // change to one spec    
                ec.AllEvents.Add(spec,lEves);
                //d.Add(id, lEves);
                //id++;
            }

            return ec;
        }

        public List<IndividualPath> doSearch()
        {
            if (this.specs == null) this.makeSpecifications();
            EventsContainer ec = new EventsContainer();

            foreach (EventSpecification spec in specs)
            {
                List<EventBL> lEves = Event.GetEvents(this.startDate, this.endDate, this.location, spec); // change to one spec    
                ec.AllEvents.Add(spec, lEves);
            }

            ec.ChooseMaximums();
            GeneticAlgo.AlgoManager alg = new GeneticAlgo.AlgoManager(ec);

            return alg.BestPaths;
        }

        //private List<EventBL> prepareData(List<Event> evds)
        //{
        //    foreach (Event ev in evds)
        //    {
        //        EventBL e = new EventBL();
        //        e.Date = ev.Date;
        //        //e.Location = ev.
        //        //this.resEvents.Add(e);
        //    }

        //    return null;
        //}

        public bool SaveEvents(String username, List<EventBL> le)
        {
            return SavedEvent.SaveEvents(username, le);
        }
    }
}