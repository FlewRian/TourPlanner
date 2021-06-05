using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.Report;
using TourPlanner.Model;

namespace TourPlanner.Test
{
    class ReportTest
    {
        private Tour _tour;
        private TourLog _tourLog;
        private List<TourLog> _logList;
        private ITourPlannerReport _tourPlannerReport;
        private string _imagePath =
            "C:\\Users\\Flori\\source\\repos\\SWE2_Repos\\TourPlanner\\Test\\TourImage_1.jpg";
        private string _expectedFileName = @"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\ReportTest.pdf";

        [SetUp]
        public void Setup()
        {
            _tour = new Tour(1, "Tour1", "Test Description", "Start", "End", 7, _imagePath);
            _tourLog = new TourLog(1,"testlog", "das ist ein log fuer tour1", "einfache Strecke macht echt spaß", "zu fuß", "01.01.1234", 1, 23, 1, 9);
            _logList = new List<TourLog>() {_tourLog};
        }

        [Test]
        public void GenerateReport_Cancelled()
        {
            _expectedFileName = "";

            var saveDialog = new Mock<ISaveFile>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            _tourPlannerReport = new TourPlannerReport(saveDialog.Object);

            _logList = new List<TourLog>();

            bool erg = _tourPlannerReport.GenerateReportPDF(_tour, _logList, false);

            saveDialog.Verify();
            Assert.False(erg);
        }

        [Test]
        public void GenerateReport_1Tour_NoLogs()
        {
            var saveDialog = new Mock<ISaveFile>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            _tourPlannerReport = new TourPlannerReport(saveDialog.Object);

            _logList = new List<TourLog>();

            bool erg = _tourPlannerReport.GenerateReportPDF(_tour, _logList, false);

            saveDialog.Verify();
            Assert.True(erg);
            Assert.True(File.Exists(_expectedFileName));

            if(File.Exists(_expectedFileName))
                File.Delete(_expectedFileName);
        }

        [Test]
        public void GenerateSummery_Cancelled()
        {
            _expectedFileName = "";

            var saveDialog = new Mock<ISaveFile>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            _tourPlannerReport = new TourPlannerReport(saveDialog.Object);

            _logList = new List<TourLog>();

            bool erg = _tourPlannerReport.GenerateReportPDF(_tour, _logList, true);

            saveDialog.Verify();
            Assert.False(erg);
        }

        [Test]
        public void GenerateSummery_1Log()
        {
            _expectedFileName = @"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\ReportTest.pdf";

            var saveDialog = new Mock<ISaveFile>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            _tourPlannerReport = new TourPlannerReport(saveDialog.Object);

            _logList = new List<TourLog>() {_tourLog};

            bool erg = _tourPlannerReport.GenerateReportPDF(_tour, _logList, false);

            saveDialog.Verify();
            Assert.True(erg);
            Assert.True(File.Exists(_expectedFileName));

            if(File.Exists(_expectedFileName))
                File.Delete(_expectedFileName);
        }

        [Test]
        public void GenerateSummery_3Logs()
        {
            _expectedFileName = @"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\ReportTest.pdf";

            var saveDialog = new Mock<ISaveFile>();

            saveDialog.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialog.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            _tourPlannerReport = new TourPlannerReport(saveDialog.Object);

            _logList = new List<TourLog>() {_tourLog, _tourLog, _tourLog};

            bool erg = _tourPlannerReport.GenerateReportPDF(_tour, _logList, false);

            saveDialog.Verify();
            Assert.True(erg);
            Assert.True(File.Exists(_expectedFileName));

            if(File.Exists(_expectedFileName))
                File.Delete(_expectedFileName);
        }
    }
}
