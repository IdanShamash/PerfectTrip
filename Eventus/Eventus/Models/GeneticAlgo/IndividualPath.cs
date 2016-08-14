using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.GeneticAlgo
{
    public class IndividualPath : IComparable
    {
        const int TOTAL_COST_FACTOR      = 20;
        const int EXACT_LOCATION_FACTOR  = 25;
        const int DATE_DISTANCE_FACTOR   = 15;
        const int EXACT_DATE_FACTOR      = 40;

        public static int max_cost;
        public static int max_date_distance;

        const int MAX_COST           = 700;
        const int MAX_EXACT_LOCATION = 10;
        const int MAX_DATE_DISTANCE  = 365;
        const int MAX_EXACT_DATE     = 10;

        //const double FACTORS_WEIGHT_NORMALIZATION    = 0.01;
        //const double POSITIVE_SCORE_NORMALIZATION = 100;

        public Dictionary<EventSpecification, EventBL> PathEvents { get; set; }
        public Double Score { get; set; }

/*        public override bool Equals(Object obj)
        {
            IndividualPath ip = (IndividualPath) obj;

            for (int i = 0; i < this.PathEvents.Count; i++)
            {
                if (!(this.PathEvents.Values.ElementAt(i).Equals(ip.PathEvents.Values.ElementAt(i))))
                    return false;
            }

            return true;
        }
 */

        int IComparable.CompareTo(object obj)
        {
            IndividualPath ip = (IndividualPath)obj;

            if (this.Score > ip.Score)
            {
                return -1;
            }
            else if (this.Score < ip.Score)
	        {
                return 1;		 
	        }
            else
            {
                return 0;
            }
        }

        public IndividualPath()
        {
            this.PathEvents = new Dictionary<EventSpecification, EventBL>();
            //this.CalculateScore();
        }
        public IndividualPath (Dictionary<EventSpecification, EventBL> pe)
	    {
            this.PathEvents = pe;
            this.CalculateScore();
	    }

        public void CalculateScore()
        {
            int      dateDistance;
            int      maxExactLocation = 1;
            int      maxExactDate     = 1;
            decimal  totalPrice       = 0;

            
            DateTime maxDate          = new DateTime(1, 1, 1);
            DateTime minDate          = new DateTime(9999, 12, 1);

            Dictionary<String, int> exactLocationCounter = new Dictionary<string, int>(); // implement comparator
            Dictionary<DateTime, int> exactDateCounter   = new Dictionary<DateTime, int>();
            

            foreach (EventSpecification spec in this.PathEvents.Keys)
	        {
		        EventBL currEvent = this.PathEvents[spec];
                totalPrice += currEvent.Price;

                if (exactLocationCounter.ContainsKey(currEvent.Location.Name))
                {
                    if (++exactLocationCounter[currEvent.Location.Name] > maxExactLocation)
                        maxExactLocation = exactLocationCounter[currEvent.Location.Name];

                }
                else 
                    exactLocationCounter.Add(currEvent.Location.Name, 1);

                if (exactDateCounter.ContainsKey(currEvent.Date))
                {
                    if (exactDateCounter[currEvent.Date]++ > maxExactDate)
                        maxExactDate = exactDateCounter[currEvent.Date];

                }
                else
                    exactDateCounter.Add(currEvent.Date, 1);

                if (currEvent.Date < minDate) minDate = currEvent.Date;
                if (currEvent.Date > maxDate) maxDate = currEvent.Date;

	        }

            dateDistance = (maxDate - minDate).Days;

            this.Score = TOTAL_COST_FACTOR * (1 - (double)totalPrice / (IndividualPath.max_cost * this.PathEvents.Count)) +
                         DATE_DISTANCE_FACTOR * (1 - (double)dateDistance / IndividualPath.max_date_distance) +
                         EXACT_DATE_FACTOR * (1 - (double)maxExactDate / this.PathEvents.Count) +
                         EXACT_LOCATION_FACTOR * ((double)maxExactLocation / this.PathEvents.Count); //MAX_EXACT_LOCATION);
        }

     /*   private double NormalizeScore(double score)
        {
            return (score * FACTORS_WEIGHT_NORMALIZATION) + POSITIVE_SCORE_NORMALIZATION;
        }*/

        //public void Mutate
    }
}