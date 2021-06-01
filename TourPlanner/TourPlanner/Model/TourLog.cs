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
        public string DateTime { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public decimal Distance { get; set; }
        public decimal TotalTime { get; set; }
        public decimal Speed { get; set; }
        public int Rating { get; set; }

        public TourLog(int id, string name, string description, string report, string vehicle, string dateTime, int tourId, decimal distance, decimal totalTime, int rating)
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
            this.Rating = rating;
            if (totalTime == 0)
            {
                totalTime = 0.001m;
            }
            this.Speed = Math.Round(distance / totalTime, 2);
        }
    }
}
