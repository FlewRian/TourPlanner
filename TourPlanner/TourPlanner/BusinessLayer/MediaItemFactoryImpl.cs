using System;
using System.Collections.Generic;
using System.Linq;
using TourPlanner.DataAccessLayer;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    internal class MediaItemFactoryImpl : IMediaItemFactory
    {
        private MediaItemDAO mediaItemDAO = new MediaItemDAO();
        public IEnumerable<MediaItem> GetItems()
        {
            return mediaItemDAO.GetItems();
        }

        public IEnumerable<MediaItem> Search(string itemName, bool caseSensitive = false)
        {
            IEnumerable<MediaItem> items = GetItems();

            if (caseSensitive)
            {
                return items.Where(x => x.Name.Contains(itemName));
            }

            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }
    }
}
    