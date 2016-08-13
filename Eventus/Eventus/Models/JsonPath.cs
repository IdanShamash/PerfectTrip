using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.GeneticAlgo
{
    public class JsonPath
    {
        public Dictionary<String, EventBL> PathEvents { get; set; }
        public Double Score { get; set; }

        public JsonPath(IndividualPath ip)
        {
            this.PathEvents = new Dictionary<String, EventBL>();

            foreach (EventSpecification spec in ip.PathEvents.Keys)
            {
                this.PathEvents.Add(spec.Identifier.ToString(), ip.PathEvents[spec]);
            }

            this.Score = ip.Score;
        }
    }
}