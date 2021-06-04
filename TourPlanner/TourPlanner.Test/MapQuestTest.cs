using NUnit.Framework;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.MapQuest;

namespace TourPlanner.Test
{
    class MapQuestTest
    {
        private IMapQuest _mapQuest;

        [SetUp]
        public void Setup()
        {
            _mapQuest = new MapQuest();
        }
        
        [Test]
        public void TestDoesLocationExistSuccess()
        {
            string location = "Wien";
            bool result = _mapQuest.DoesLocationExist(location);
            Assert.True(result);
        }

        [Test]
        public void TestDoesLocationExistFail()
        {
            bool result = _mapQuest.DoesLocationExist("");
            Assert.False(result);
        }
        
    }
}
