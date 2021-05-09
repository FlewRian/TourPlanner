using System;
using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface ITourDAO
    {
        Tour FindById(int itemId);
        Tour AddNewItem(string name, string description, string start, string end);
        IEnumerable<Tour> GetTours();
    }
}
