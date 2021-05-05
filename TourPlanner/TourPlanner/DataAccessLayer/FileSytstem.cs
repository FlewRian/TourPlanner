using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer
{
    class FileSytstem : IDataAccess
    {
        public List<MediaItem> GetItems()
        {
            return new List<MediaItem>()
            {
                new MediaItem() {Name = "Item1"},
                new MediaItem() {Name = "Item2"},
                new MediaItem() {Name = "Herbert"},
                new MediaItem() {Name = "Test"},
                new MediaItem() {Name = "keine Ahnung"}
            };
        }
    }
}
