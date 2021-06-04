using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Moq;
using TourPlanner.BusinessLayer.PostgresSqlServer;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Model;

namespace TourPlanner.Test
{
    public class TourTests
    {
        private TourPostgresDAO _tourDao = new TourPostgresDAO();
        private Tour _tour;
        private string _imagePath =
            "C:\\Users\\Flori\\source\\repos\\SWE2_Repos\\TourPlanner\\Test\\TourImage_1.jpg";

        [SetUp]
        public void Setup()
        {
            _tour = new Tour(1, "Tour1", "Test Description", "Start", "End", 7, _imagePath);
        }

        [Test]
        public void TourDataCheck()
        {
            Assert.AreEqual("Tour1", _tour.Name);
            Assert.AreEqual("Test Description", _tour.Description);
            Assert.AreEqual("Start", _tour.Start);
            Assert.AreEqual("End", _tour.End);
            Assert.AreEqual(7, _tour.Distance);
        }

        [Test]
        public void TourHasImage()
        {
            Assert.True(_tour.TourHasImage());
        }

        [Test]
        public void TourHasNoImageEmptyString()
        {
            _tour.ImagePath = "";
            Assert.False(_tour.TourHasImage());
        }

        [Test]
        public void TourHasNoImageNull()
        {
            _tour.ImagePath = null;
            Assert.False(_tour.TourHasImage());
        }

        [Test]
        public void CreateTourWithDb()
        {
            Tour tour = _tourDao.AddNewItem("Tour1", "Test Description", "Start", "End", 7, _imagePath);
            Assert.AreEqual("Tour1", tour.Name);
            Assert.AreEqual("Test Description", tour.Description);
            Assert.AreEqual("Start", tour.Start);
            Assert.AreEqual("End", tour.End);
            Assert.AreEqual(7, tour.Distance);
        }


        [Test]
        public void TestTourEdit()
        {
            Tour tour = _tourDao.AddNewItem("Tour1", "Test Description", "Start", "End", 7, _imagePath);
            Tour newTour = _tourDao.EditTour(tour, "NewName", "NewDescription", "newStart", "newEnd", 99, _imagePath);
            Assert.AreEqual("NewName", newTour.Name);
            Assert.AreEqual("NewDescription", newTour.Description);
            Assert.AreEqual("newStart", newTour.Start);
            Assert.AreEqual("newEnd", newTour.End);
            Assert.AreEqual(99, newTour.Distance);
        }
    }

    
}