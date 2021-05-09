using System;
using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface ITourLogDAO
    {
        TourLog FindById(int logId);
        TourLog AddNewTourLog(string name, string description, string report, string vehicle, DateTime dateTime, int tourId, double distance, double totalTime, int rating);
        IEnumerable<TourLog> GetLogsForTour(int tourId);
    }
}
