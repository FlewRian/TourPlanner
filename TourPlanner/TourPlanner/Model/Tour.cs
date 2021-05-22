using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int Distance { get; set; }

        public Tour(int id, string name, string description, string start, string end, int distance)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Start = start;
            this.End = end;
            this.Distance = distance;
        }
    }
}
