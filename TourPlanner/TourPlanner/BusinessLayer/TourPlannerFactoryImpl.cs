﻿using System;
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
        public Tour CreateTour(string name, string description, string start, string end, int distance)
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.AddNewItem(name, description, start, end, distance);
        }

        public TourLog CreateTourLog(string name, string description, string report, string vehicle, string dateTime,
            int tourId, decimal distance, decimal totalTime, int rating)
        {
            ITourLogDAO tourLogDAO = DALFactory.CreateTourLogDAO();
            return tourLogDAO.AddNewTourLog(name, description, report, vehicle, dateTime, tourId, distance, totalTime,
                rating);
        }

        public void DeleteTour(Tour tour)
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
            return tourDao.AddNewItem(name, description, start, end, distance);
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
            return tourDao.EditTour(currentTour, newName, newDescription, newStart, newEnd, newDistance);
        }
    }
}
    