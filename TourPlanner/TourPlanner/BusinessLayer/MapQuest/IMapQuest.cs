namespace TourPlanner.BusinessLayer
{
    public interface IMapQuest
    {
        public string LoadImage(string fromLocation, string toLocation);
        public bool DoesLocationExist(string location);
    }
}