using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.Json;
using TourPlanner.Model;

namespace TourPlanner.Test
{
    class JsonTest
    {
        private Tour _tour;
        private TourLog _tourLog;
        private string _imagePath =
            "C:\\Users\\Flori\\source\\repos\\SWE2_Repos\\TourPlanner\\Test\\TourImage_1.jpg";
        private string _expectedFileName = @"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\TestExportTest.json";
        private IJsonManager _jsonManager;
        private List<Tour> _tourList;
        private List<TourLog> _logList;

        [SetUp]
        public void Setup()
        {
            _tour = new Tour(1, "Tour1", "Test Description", "Start", "End", 7, _imagePath);
            _tourLog = new TourLog(1,"testlog", "das ist ein log fuer tour1", "einfache Strecke macht echt spaß", "zu fuß", "01.01.1234", 1, 23, 1, 9);
            _tourList = new List<Tour>() {_tour};
            _logList = new List<TourLog>() {_tourLog};
        }

        [Test]
        public void JsonExport_isTrue()
        {
            var saveDialogMock = new Mock<ISaveFile>();
            var openDialogMock = new Mock<IOpenFile>();

            saveDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            openDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();


            _jsonManager = new JsonManager(saveDialogMock.Object,openDialogMock.Object);

            bool erg = _jsonManager.JsonExport(_tourList, _logList);

            saveDialogMock.Verify();
            Assert.True(erg);
            Assert.True(File.Exists(_expectedFileName));

            if(File.Exists(_expectedFileName))
                File.Delete(_expectedFileName);
        }

        [Test]
        public void JsonExport_NoFilename_Cancelled()
        {
            _expectedFileName = "";

            var saveDialogMock = new Mock<ISaveFile>();
            var openDialogMock = new Mock<IOpenFile>();

            saveDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            openDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            _jsonManager = new JsonManager(saveDialogMock.Object,openDialogMock.Object);

            bool erg = _jsonManager.JsonExport(_tourList, _logList);

            saveDialogMock.Verify();
            Assert.False(erg);
            Assert.False(File.Exists(_expectedFileName));
        }

        [Test]
        public void JsonImport_1Tour_1TourLog()
        {
            _expectedFileName = @"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\Test\TestJsonImport2.json";

            var saveDialogMock = new Mock<ISaveFile>();
            var openDialogMock = new Mock<IOpenFile>();

            saveDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            openDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();


            _jsonManager = new JsonManager(saveDialogMock.Object,openDialogMock.Object);

            Tour_LogJson erg = _jsonManager.JsonImport();

            //assert
            openDialogMock.Verify();
            Assert.True(erg.Tours.ToList().Count == 1);
            Assert.True(erg.TourLogs.ToList().Count == 1);
        }

        [Test]
        public void JsonImport_2Tours_3TourLogs()
        {
            _expectedFileName = @"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\Test\TestJsonImport.json";

            var saveDialogMock = new Mock<ISaveFile>();
            var openDialogMock = new Mock<IOpenFile>();

            saveDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            openDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();


            _jsonManager = new JsonManager(saveDialogMock.Object,openDialogMock.Object);

            Tour_LogJson erg = _jsonManager.JsonImport();

            openDialogMock.Verify();
            Assert.True(erg.Tours.ToList().Count == 2);
            Assert.True(erg.TourLogs.ToList().Count == 3);
        }

        [Test]
        public void JsonImport_NoFilename_Cancelled()
        {
            _expectedFileName = "";

            var saveDialogMock = new Mock<ISaveFile>();
            var openDialogMock = new Mock<IOpenFile>();

            saveDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            saveDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();

            openDialogMock.Setup(x => x.ShowDialog()).Returns(true).Verifiable();
            openDialogMock.Setup(x => x.FileName).Returns(_expectedFileName).Verifiable();


            _jsonManager = new JsonManager(saveDialogMock.Object,openDialogMock.Object);

            Tour_LogJson erg = _jsonManager.JsonImport();

            openDialogMock.Verify();
            Assert.Null(erg);
        }
    }
}
