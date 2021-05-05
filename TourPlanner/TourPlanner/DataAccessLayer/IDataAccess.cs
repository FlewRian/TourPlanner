using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer
{
    interface IDataAccess
    {
        public List<MediaItem> GetItems();
    }
}
