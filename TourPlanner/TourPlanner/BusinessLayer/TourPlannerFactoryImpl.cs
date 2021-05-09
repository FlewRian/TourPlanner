using System;
using System.Collections.Generic;
using System.Linq;
using TourPlanner.DataAccessLayer;
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

        public IEnumerable<Tour> Search(string itemName, bool caseSensitive = false)
        {
            IEnumerable<Tour> items = GetItems();

            if (caseSensitive)
            {
                return items.Where(x => x.Name.Contains(itemName));
            }

            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }

        public Tour CreateTour(string name, string description, string start, string end)
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.AddNewItem(name, description, start, end);
        }

        public TourLog CreateTourLog(string name, string description, string report, string vehicle, DateTime dateTime,
            int tourId, double distance, double totalTime, int rating)
        {
            ITourLogDAO tourLogDAO = DALFactory.CreateTourLogDAO();
            return tourLogDAO.AddNewTourLog(name, description, report, vehicle, dateTime, tourId, distance, totalTime,
                rating);
        }
    }
}
    