using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer
{
    public class MediaItemDAO
    {
        private IDataAccess dataAccess;

        public MediaItemDAO()
        {
            dataAccess = new Database();
        }

        public List<MediaItem> GetItems()
        {
            return dataAccess.GetItems();
        }
    }
}
