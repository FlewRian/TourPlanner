using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.GetTours();
        }

        public IEnumerable<TourLog> GetTourLogs(Tour tour)
        {
            ITourLogDAO tourLogDAO = DALFactory.CreateTourLogDAO();
            return tourLogDAO.GetTourLogs(tour);
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
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            IMapQuest mapQuest = new MapQuest();
            string imagePath = mapQuest.LoadImage(start, end);
            return tourDAO.AddNewItem(name, description, start, end, distance, imagePath);
        }

        public TourLog CreateTourLog(string name, string description, string report, string vehicle, string dateTime,
            int tourId, decimal distance, decimal totalTime, int rating)
        {
            ITourLogDAO tourLogDAO = DALFactory.CreateTourLogDAO();
            return tourLogDAO.AddNewTourLog(name, description, report, vehicle, dateTime, tourId, distance, totalTime,
                rating);
        }

        public void DeleteTour(Tour tour, string imagePath)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();

            if (!imagePath.Equals(""))
            {
                /*Image tempImage = Image.FromFile(imagePath);
                tempImage.Dispose();
                File.Delete(imagePath);*/
            }

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
            IMapQuest mapQuest = new MapQuest();
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
            IMapQuest mapQuest = new MapQuest();
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
            TourPlannerReport report = new TourPlannerReport();
            return report.GenerateReportPDF(currentTour, tourLogs, false);
        }

        public bool GenerateSummary(Tour currentTour)
        {
            TourPlannerReport report = new TourPlannerReport();
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return report.GenerateReportPDF(currentTour, tourLogDao.GetAllTourLogs(), true);
        }
    }
}
    