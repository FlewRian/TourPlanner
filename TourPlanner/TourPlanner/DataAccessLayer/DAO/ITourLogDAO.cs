using System;
using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface ITourLogDAO
    {
        TourLog FindById(int logId);
        TourLog AddNewTourLog(string name, string description, string report, string vehicle, string dateTime, int tourId, decimal distance, decimal totalTime, int rating);
        IEnumerable<TourLog> GetLogsForTour(int tourId);
        IEnumerable<TourLog> GetTourLogs(Tour tour);
        public void DeleteTourLog(TourLog tourLog);
    }
}
