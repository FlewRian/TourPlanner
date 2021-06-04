using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer.Json
{
    public interface IJsonManager
    {
        public bool JsonExport(IEnumerable<Tour> tours, IEnumerable<TourLog> tourLogs);
        public Tour_LogJson JsonImport();
    }
}
