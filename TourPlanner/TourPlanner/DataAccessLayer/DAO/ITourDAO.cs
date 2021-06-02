using System;
using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface ITourDAO
    {
        Tour FindById(int itemId);
        Tour AddNewItem(string name, string description, string start, string end, int distance, string imagePath);
        IEnumerable<Tour> GetTours();
        void DeleteTour(Tour tour);
        Tour EditTour(Tour currentTour, string newName, string newDescription, string newStart, string newEnd, int newDistance, string tourImagePath);
    }
}
