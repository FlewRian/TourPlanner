using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer
{
    class Database : IDataAccess
    {
        private string connectionString;
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
