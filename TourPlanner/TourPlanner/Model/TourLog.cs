using System;

namespace TourPlanner.Model
{
    public class TourLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Report { get; set; }
        public string Vehicle { get; set; }
        public DateTime DateTime { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public double Distance { get; set; }
        public double TotalTime { get; set; }
        public double Speed { get; set; }
        public int Rating { get; set; }

        public TourLog(int id, string name, string description, string report, string vehicle, DateTime dateTime, int tourId, double distance, double totalTime, int rating)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Report = report;
            this.Vehicle = vehicle;
            this.DateTime = dateTime;
            this.TourId = tourId;
            this.Distance = distance;
            this.TotalTime = totalTime;
            this.Speed = Math.Round(distance / totalTime, 2);
            this.Rating = rating;
        }
    }
}
