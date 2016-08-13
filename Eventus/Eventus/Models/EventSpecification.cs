using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventus.Models
{
    public class EventSpecification
    {
        public int Identifier { get; set; }
        public String Type { get; set; }
        public String FreeText { get; set; }
        public int Weight { get; set; }

        // Dev only
        public EventSpecification(int id, String type, String freeT) 
        {
            this.Identifier = id;
            this.Type = type;
            this.FreeText = freeT;
            this.Weight = 1; // only in Beta
        }

        public EventSpecification(int id, String type)
        {
            this.Identifier = id;
            this.Type = type;
            this.FreeText = "";
            this.Weight = 1; // only in Beta
        }
    }
}