using System.Collections.Generic;

namespace TourPlanner.Model
{
    class Tour_LogJson
    {
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<TourLog> TourLogs { get; set; }
        public Tour_LogJson(IEnumerable<Tour> tours, IEnumerable<TourLog> tourLogs)
        {
            this.Tours = tours;
            this.TourLogs = tourLogs;
        }
    }
}
