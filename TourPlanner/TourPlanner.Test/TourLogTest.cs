using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TourPlanner.BusinessLayer.PostgresSqlServer;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Model;

namespace TourPlanner.Test
{
    class TourLogTest
    {
        private TourPostgresDAO _tourDao = new TourPostgresDAO();
        private ITourLogDAO _tourLogDao;
        private TourLog _tourLog;
        private string _imagePath =
            "C:\\Users\\Flori\\source\\repos\\SWE2_Repos\\TourPlanner\\Test\\TourImage_1.jpg";

        [SetUp]
        public void Setup()
        {
           _tourLogDao = new TourLogPostgresDAO();
           _tourLog = new TourLog(1,"testlog", "das ist ein log fuer tour1", "einfache Strecke macht echt spaß", "zu fuß", "01.01.1234", 1, 23, 1, 9);
        }

        [Test]
        public void TourLogDataCheck()
        {
            Assert.AreEqual("testlog", _tourLog.Name);
            Assert.AreEqual("das ist ein log fuer tour1", _tourLog.Description);
            Assert.AreEqual("einfache Strecke macht echt spaß", _tourLog.Report);
            Assert.AreEqual("zu fuß", _tourLog.Vehicle);
            Assert.AreEqual("01.01.1234", _tourLog.DateTime);
            Assert.AreEqual(1, _tourLog.TourId);
            Assert.AreEqual(23, _tourLog.Distance);
            Assert.AreEqual(1, _tourLog.TotalTime);
            Assert.AreEqual(9, _tourLog.Rating);
        }

        [Test]
        public void TourLogCreateWithDb()
        {
            Tour tour = _tourDao.AddNewItem("Tour1", "Test Description", "Start", "End", 7, _imagePath);
            TourLog tourLog = _tourLogDao.AddNewTourLog("testlog", "das ist ein log fuer tour1",
                "einfache Strecke macht echt spaß", "zu fuß", "01.01.1234", tour.Id, 23, 1, 9);
            Assert.AreEqual("testlog", tourLog.Name);
            Assert.AreEqual("das ist ein log fuer tour1", tourLog.Description);
            Assert.AreEqual("einfache Strecke macht echt spaß", tourLog.Report);
            Assert.AreEqual("zu fuß", tourLog.Vehicle);
            Assert.AreEqual("01.01.1234", tourLog.DateTime);
            Assert.AreEqual(tour.Id, tourLog.TourId);
            Assert.AreEqual(23, tourLog.Distance);
            Assert.AreEqual(1, tourLog.TotalTime);
            Assert.AreEqual(9, tourLog.Rating);
        }

        [Test]
        public void TourLogEdit()
        {
            Tour tour = _tourDao.AddNewItem("Tour1", "Test Description", "Start", "End", 7, _imagePath);
            TourLog tourLog = _tourLogDao.AddNewTourLog("testlog", "das ist ein log fuer tour1",
                "einfache Strecke macht echt spaß", "zu fuß", "01.01.1234", tour.Id, 23, 1, 9);
            TourLog newTourLog = _tourLogDao.EditTourLog(tourLog, "NewName", "NewDescription", "newReport", "newVehicle", "01.01.1111", tour.Id, 1, 1, 1);
            Assert.AreEqual("NewName", newTourLog.Name);
        }
    }
}
