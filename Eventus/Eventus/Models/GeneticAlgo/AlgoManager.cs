using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eventus.Models;
using System.Collections;

namespace Eventus.Models.GeneticAlgo
{
    public class AlgoManager
    {
        const int GENERATIONS = 10;

        public Population Population { get; set; }
        public List<IndividualPath> BestPaths { get; set; }
        public EventsContainer EventsContainer { get; set; }

        public int SizeBestPaths = 3;
        
        private IndividualPathEqualityComparer ipEqualityComparer; 

        public AlgoManager(EventsContainer ec)
        {
            this.EventsContainer = ec;
            this.Population = new Population(this.EventsContainer);
            this.ipEqualityComparer = new IndividualPathEqualityComparer();
            this.ChooseBestPathsFromPopulation();
            this.Run();
        }

        public void Run()
        {
            for (int i = 0; i < GENERATIONS; i++)
            {
                this.Population = new Population(this.Population);
                this.ChooseBestPathsFromPopulation();
            }
            
        }

        public void UpdateBestPaths()
        {
            if (this.BestPaths == null)
            {
                this.BestPaths = new List<IndividualPath>();
            }

            for (int i = 0; i < this.SizeBestPaths; i++)
            {
                this.BestPaths.Add(this.EventsContainer.ComposeRandomPath());
            }
        }

        private void ChooseBestPathsFromPopulation()
        {
            if (this.BestPaths == null)
            {
                this.BestPaths = new List<IndividualPath>();
                this.Population.Individuals.Sort();
                int index = 0;

                while (this.BestPaths.Count < this.SizeBestPaths && index < Population.POP_SIZE)
                {
                    IndividualPath ip = this.Population.Individuals.ElementAt(index);

                    if (!(this.BestPaths.Contains(ip, this.ipEqualityComparer)))
                    {
                        this.BestPaths.Add(ip);
                    }
                    
                    index++;
                }
            }

            else
            {
                List<IndividualPath> updatedBestPaths = new List<IndividualPath>();
                IndividualPath ip;

                for (int i = 0; i < Population.POP_SIZE; i++)
                {
                    ip = this.Population.Individuals.ElementAt(i);

                    if (ip.Score > this.BestPaths.ElementAt(this.BestPaths.Count - 1).Score)
                    {
                        if (!(this.BestPaths.Contains(ip, this.ipEqualityComparer)))
                        {
                            IndividualPath newInstance = new IndividualPath(ip.PathEvents);
                            newInstance.Score = ip.Score;
                            this.BestPaths.RemoveAt(this.BestPaths.Count - 1);
                            this.BestPaths.Add(newInstance);
                            this.BestPaths.Sort();
                        }  
                    }
                }
            }

            this.BestPaths.Sort();
        }
    }
}