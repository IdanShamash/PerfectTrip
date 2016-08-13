using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models.GeneticAlgo
{
    public class IndividualPathEqualityComparer : EqualityComparer<IndividualPath>
    {
        public override bool Equals(IndividualPath ip1, IndividualPath ip2)
        {
            for (int i = 0; i < ip1.PathEvents.Count; i++)
            {
                if (!(ip1.PathEvents.Values.ElementAt(i).Equals(ip2.PathEvents.Values.ElementAt(i))))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(IndividualPath ip)
        {
            int hCode = 0;

            foreach (EventBL eb in ip.PathEvents.Values)
            {
                hCode += eb.GetHashCode();
            }

            return hCode;
        }
    }
}