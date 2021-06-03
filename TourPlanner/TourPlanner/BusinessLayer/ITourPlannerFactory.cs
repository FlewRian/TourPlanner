using System;
using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourPlannerFactory
    {
        IEnumerable<Tour> GetItems();
        IEnumerable<TourLog> GetTourLogs(Tour tour);
        IEnumerable<Tour> Search(string itemName, bool caseSensitive = false);
        IEnumerable<TourLog> SearchTourLog(string itemName, Tour currentTour, bool caseSensitive = false);
        Tour CreateTour(string name, string description, string start, string end, int distance);
        TourLog CreateTourLog(string name, string description, string report, string vehicle, string dateTime,
            int tourId, decimal distance, decimal totalTime, int rating);
        void DeleteTour(Tour tour, string imagePath);
        void DeleteTourLog(TourLog tourlog);
        Tour AddNewItem(string name, string description, string start, string end, int distance);
        TourLog AddNewTourLog(string name, string description, string report, string vehicle, string dateTime,
            int tourId, decimal distance, decimal totalTime, int rating);
        Tour EditTour(Tour currentTour, string newName, string newDescription, string newStart, string newEnd, int newDistance);

        TourLog EditTourLog(TourLog currentTourLog, string name, string description, string report,
            string vehicle, string dateTime, int tourId, decimal distance, decimal totalTime, int rating);
        bool GenerateReportPDF(Tour currentTour, IEnumerable<TourLog> tourLogs);
        bool GenerateSummary(Tour currentTour);

    }
}
