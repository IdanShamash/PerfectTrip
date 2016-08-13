using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.GeneticAlgo
{
    public class Population
    {
        public const int POP_SIZE = 100;

        public List<IndividualPath> Individuals { get; set; }

        private static Population currPop;

        private static Population oldPop = null;

        private static Random Rand = new Random();

        private int totalPopulationScore = 0;

        private double worstScore;

        private void NormalizeScores()
        {
            int    normalizationDirection = 0;
            double normalizationDelta;

            this.Individuals.Sort();
            this.worstScore = this.Individuals.ElementAt(POP_SIZE - 1).Score;

            if (this.worstScore != 0)
            {
                if (this.worstScore < 0)
                    normalizationDirection = 1;

                else if (this.worstScore > 0)
                    normalizationDirection = -1;

                foreach (IndividualPath ip in this.Individuals)
                {
                    normalizationDelta = (normalizationDirection * worstScore) + 1;
                    ip.Score += normalizationDelta;
                    this.totalPopulationScore += (int)Math.Ceiling(normalizationDelta); ;
                }                
            }
        }

        public Population(EventsContainer ec)
        {
            this.totalPopulationScore = 0;
            this.Individuals = new List<IndividualPath>();
            this.GenerateIndividuals(ec);
            this.NormalizeScores();
        }

        private Population()
        {
            this.Individuals = new List<IndividualPath>();
        }

        public Population(Population p)
        {
            this.totalPopulationScore = 0;
            this.Individuals = new List<IndividualPath>();
            int[] propArr = this.CreateProbabilityArray(p);

            for (int i = 0; i < POP_SIZE / 2; i++)
            {
                int r = Rand.Next(propArr.Length - 1);
                IndividualPath ip = p.Individuals.ElementAt(propArr[r]);

                if (!(this.Individuals.Contains(ip)))
                {
                    this.Individuals.Add(ip);
                    this.totalPopulationScore += (int)Math.Ceiling(ip.Score);
                }

                else i--;
            }

            for (int i = 0; i < POP_SIZE / 2; i++)
            {
                IndividualPath parent1 = p.Individuals.ElementAt(Rand.Next(POP_SIZE));
                IndividualPath parent2 = p.Individuals.ElementAt(Rand.Next(POP_SIZE));

                Dictionary<EventSpecification, EventBL> childPath = new Dictionary<EventSpecification,EventBL>();

                EventSpecification keyChromozom;
                EventBL            valChromozom;

                for (int j = 0; j < parent1.PathEvents.Count; j++)
                {
                    if (j % 2 == 0)
                    {
                        keyChromozom = parent1.PathEvents.ElementAt(j).Key;
                        valChromozom = parent1.PathEvents[keyChromozom];
                    }
                    else
                    {
                        keyChromozom = parent2.PathEvents.ElementAt(j).Key;
                        valChromozom = parent2.PathEvents[keyChromozom];
                    }

                    childPath.Add(keyChromozom, valChromozom);    
                }

                if (false) // if muation
                {
                    //randomize a spec
                    //get the spec's eventBL from container
                    //insert to dictionary[spec] <- eventBL
                }

                IndividualPath ip = new IndividualPath(childPath);

                this.Individuals.Add(ip);
                this.totalPopulationScore += (int)Math.Ceiling(ip.Score);
            }

            this.NormalizeScores();
        }

        private void GenerateIndividuals(EventsContainer ec)
        {
            IndividualPath ip;
            double scoreAggregator = 0;

            for (int i = 0; i < POP_SIZE; i++)
            {
                ip = ec.ComposeRandomPath();
                this.Individuals.Add(ip);
                scoreAggregator += (int)Math.Ceiling(ip.Score);
            }

            this.totalPopulationScore = (int)Math.Ceiling(scoreAggregator);
        }

        private int[] CreateProbabilityArray(Population p)
        {
            int[] probArr = new int[p.totalPopulationScore];
            int index = 0;

            foreach (IndividualPath ip in p.Individuals)
            {
                int i;

                for (i = 0; i < (int)Math.Ceiling(ip.Score); i++)
                {
                    probArr[index + i] = p.Individuals.IndexOf(ip);
                }

                index += i;
            }

            if (index < probArr.Length)
            {
                Array.Resize<int>(ref probArr, index);
            }

            return probArr;
        }
    }
}