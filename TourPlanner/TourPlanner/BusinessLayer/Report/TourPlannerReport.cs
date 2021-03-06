using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.Win32;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer.Report
{
    public class TourPlannerReport : ITourPlannerReport
    {
        private ISaveFile _saveFile;

        public TourPlannerReport(ISaveFile saveFile)
        {
            _saveFile = saveFile;
        }

        public bool GenerateReportPDF(Tour currentTour, IEnumerable<TourLog> tourLogs, bool logSummary)
        { 
            _saveFile.Filter = "PDF Files (*.pdf) | *.pdf"; //Here you can filter which all files you wanted allow to open  
            _saveFile.ShowDialog();
            if (!_saveFile.FileName.Equals(""))
            {
                var htmlToPdf = new HtmlToPdf();
                var pdf = htmlToPdf.RenderHtmlAsPdf(ConvertToHTML(currentTour, tourLogs, logSummary));
                pdf.SaveAs(_saveFile.FileName);
                return true;
            }

            return false;
        }

        private string ConvertToHTML(Tour currentTour, IEnumerable<TourLog> tourLogs, bool logSummary)
        {
            string htmlReportText;
            int tourLogCount = 0;
            decimal totalDistance = 0;
            decimal totalTime = 0;
            bool printAllLogs = true;

            switch (logSummary)
            {
                case true:      //Report from all Logs
                    htmlReportText = @"<html>"+ 
                                     "<head>" +
                                     "<title>All TourLogs Summary</title>" +
                                     "</head>";

                    foreach (var log in tourLogs)
                    {
                        tourLogCount++;
                        totalDistance += log.Distance;
                        totalTime += log.TotalTime;
                    }

                    htmlReportText = @"<h1>Total Tour Log Count: " + tourLogCount + "</h1>" +
                                     "<b> Total Distance: " + totalDistance + " km<br>" +
                                     "<b> Total Time: " + totalTime + " h<br>";
                                     

                    if (printAllLogs)
                    {
                        foreach (var log in tourLogs)
                        {
                            htmlReportText += @"<h3>Logname: " + log.DateTime + "</h3>" +
                                              "<b> Description: </b> " + log.Description + "<br>" +
                                              "<b> Report: </b> " + log.Report + "<br>" +
                                              "<b> Vehicle: </b> " + log.Vehicle + "<br>" +
                                              "<b> Date: </b> " + log.DateTime + "<br>" +
                                              "<b> Distance: </b> " + log.Distance + " km<br>" +
                                              "<b> Totaltime: </b> " + log.TotalTime + " h<br>" +
                                              "<b> Rating: </b> " + log.Rating + "<br>" +
                                              "<b> Tour ID: </b> " + log.TourId + "<br>";
                        }
                    }
                    htmlReportText += "</body></html>";

                    return htmlReportText;

                case false:     //Report from one Tour with all Logs
                    htmlReportText = @"<html>"+ 
                                            "<head>" +
                                            "<title>TourPlanner Tour Report</title>" +
                                            "</head>";

                    htmlReportText += @"<body>"+
                                      "<h1>Tourname: " + currentTour.Name + "</h1>" +
                                      "<b> Start: </b> " + currentTour.Start + "<br>" +
                                      "<b> End: </b> " + currentTour.End + "<br>" +
                                      "<b> Distance: </b> " + currentTour.Distance + " km<br>" +
                                      "<b> Description: </b> " + currentTour.Description + "<br>" +
                                      "<img src='" + currentTour.ImagePath + "' alt='TourImage' width='500'>" +
                                      "<h2>All TourLogs from Tour: " + currentTour.Name + "</h2>";

                    foreach (var log in tourLogs)
                    {
                        if (log.TourId == currentTour.Id)
                        {
                            htmlReportText += @"<h3>Logname: " + log.DateTime + "</h3>" +
                                              "<b> Description: </b> " + log.Description + "<br>" +
                                              "<b> Report: </b> " + log.Report + "<br>" +
                                              "<b> Vehicle: </b> " + log.Vehicle + "<br>" +
                                              "<b> Date: </b> " + log.DateTime + "<br>" +
                                              "<b> Distance: </b> " + log.Distance + " km<br>" +
                                              "<b> Totaltime: </b> " + log.TotalTime + " h<br>" +
                                              "<b> Rating: </b> " + log.Rating + "<br>" +
                                              "</body></html>";
                        }
                    }

                    return htmlReportText;
            }
        }
    }
}
