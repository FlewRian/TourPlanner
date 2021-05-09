using System;
using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourPlannerFactory
    {
        IEnumerable<Tour> GetItems();
        IEnumerable<Tour> Search(string itemName, bool caseSensitive = false);
        Tour CreateTour(string name, string description, string start, string end);
        TourLog CreateTourLog(string name, string description, string report, string vehicle, DateTime dateTime,
            int tourId, double distance, double totalTime, int rating);
    }
}
