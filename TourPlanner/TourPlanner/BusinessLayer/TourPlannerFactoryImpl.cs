using System.Collections.Generic;
using System.Linq;
using TourPlanner.BusinessLayer.Json;
using TourPlanner.BusinessLayer.Report;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    internal class TourPlannerFactoryImpl : ITourPlannerFactory
    {

        public IEnumerable<Tour> GetItems()
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            return tourDao.GetTours();
        }

        public IEnumerable<TourLog> GetTourLogs(Tour tour)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.GetTourLogs(tour);
        }

        public IEnumerable<Tour> Search(string itemName, bool caseSensitive = false)
        {
            IEnumerable<Tour> items = GetItems();

            if (caseSensitive)
            {
                return items.Where(x => x.Name.Contains(itemName));
            }

            if (itemName == null)
            {
                return items;
            }
            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }

        public IEnumerable<TourLog> SearchTourLog(string itemName, Tour currentTour, bool caseSensitive = false)
        {
            IEnumerable<TourLog> items = GetTourLogs(currentTour);

            if (caseSensitive)
            {
                return items.Where(x => x.Name.Contains(itemName));
            }

            if (itemName == null)
            {
                return items;
            }
            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }

        public Tour CreateTour(string name, string description, string start, string end, int distance)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            IMapQuest mapQuest = new MapQuest.MapQuest();
            string imagePath = mapQuest.LoadImage(start, end);
            return tourDao.AddNewItem(name, description, start, end, distance, imagePath);
        }

        public TourLog CreateTourLog(string name, string description, string report, string vehicle, string dateTime,
            int tourId, decimal distance, decimal totalTime, int rating)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.AddNewTourLog(name, description, report, vehicle, dateTime, tourId, distance, totalTime,
                rating);
        }

        public void DeleteTour(Tour tour, string imagePath)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();

            tourDao.DeleteTour(tour);
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            tourLogDao.DeleteTourLog(tourLog);
        }

        public Tour AddNewItem(string name, string description, string start, string end, int distance)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            IMapQuest mapQuest = new MapQuest.MapQuest();
            string imagePath = mapQuest.LoadImage(start, end);
            return tourDao.AddNewItem(name, description, start, end, distance, imagePath);
        }

        public TourLog AddNewTourLog(string name, string description, string report, string vehicle, string dateTime, int tourId,
            decimal distance, decimal totalTime, int rating)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.AddNewTourLog(name, description, report, vehicle, dateTime, tourId, distance, totalTime, rating);
        }

        public Tour EditTour(Tour currentTour, string newName, string newDescription, string newStart, string newEnd, int newDistance)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            IMapQuest mapQuest = new MapQuest.MapQuest();
            string imagePath = mapQuest.LoadImage(newStart, newEnd);
            return tourDao.EditTour(currentTour, newName, newDescription, newStart, newEnd, newDistance, imagePath);
        }

        public TourLog EditTourLog(TourLog currentTourLog, string name, string description, string report,
            string vehicle, string dateTime, int tourId, decimal distance, decimal totalTime, int rating)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.EditTourLog(currentTourLog, name, description, report, vehicle, dateTime, tourId,
                distance, totalTime, rating);
        }

        public bool GenerateReportPDF(Tour currentTour, IEnumerable<TourLog> tourLogs)
        {
            TourPlannerReport report = new TourPlannerReport(new SaveFile());
            return report.GenerateReportPDF(currentTour, tourLogs, false);
        }

        public bool GenerateSummary(Tour currentTour)
        {
            TourPlannerReport report = new TourPlannerReport(new SaveFile());
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return report.GenerateReportPDF(currentTour, tourLogDao.GetAllTourLogs(), true);
        }

        public bool JsonExport()
        {
            IJsonManager jsonManager = new JsonManager(new SaveFile(),new OpenFile());
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            ITourDAO tourDao = DALFactory.CreateTourDAO();

            IEnumerable<Tour> tours = tourDao.GetTours();
            IEnumerable<TourLog> tourLogs = tourLogDao.GetAllTourLogs();

            return jsonManager.JsonExport(tours, tourLogs);
        }

        public bool JsonImport()
        {
            IJsonManager jsonManager = new JsonManager(new SaveFile(),new OpenFile());
            Tour_LogJson jsonData = jsonManager.JsonImport();

            if (jsonData != null)
            {
                foreach (var tour in jsonData.Tours)
                {
                    Tour newTour = AddNewItem(tour.Name, tour.Description, tour.Start, tour.End, tour.Distance);
                    foreach (var tourLog in jsonData.TourLogs)
                    {
                        if (tour.Id == tourLog.TourId)
                        {
                            AddNewTourLog(tourLog.Name, tourLog.Description, tourLog.Report, tourLog.Vehicle,
                                tourLog.DateTime, newTour.Id, tourLog.Distance, tourLog.TotalTime, tourLog.Rating);
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
    