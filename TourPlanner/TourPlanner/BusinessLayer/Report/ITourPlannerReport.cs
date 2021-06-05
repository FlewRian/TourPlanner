using System;
using System.Collections.Generic;
using log4net.Util.TypeConverters;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer.Report
{
    public interface ITourPlannerReport
    {
        public bool GenerateReportPDF(Tour tour, IEnumerable<TourLog> tourLogs, bool logSummery);
    }
}
