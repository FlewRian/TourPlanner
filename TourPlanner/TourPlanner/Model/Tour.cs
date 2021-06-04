using System.IO;

namespace TourPlanner.Model
{
    public class Tour
    {
        private string _imagePath;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int Distance { get; set; }
        public string ImagePath
        {
            get
            {
                if (_imagePath == null || _imagePath.Equals(""))
                    return @"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\TourPlanner\TourPlanner\Images\No_Image_Icon.png";
                if(File.Exists(_imagePath))
                    return _imagePath;
                return @"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\TourPlanner\TourPlanner\Images\No_Image_Icon.png";
            }
            set => _imagePath = value;
        }

        public Tour(int id, string name, string description, string start, string end, int distance, string imagePath)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Start = start;
            this.End = end;
            this.Distance = distance;
            this.ImagePath = imagePath;
        }

        public bool TourHasImage()
        {
            if(ImagePath.Equals(@"C:\Users\Flori\source\repos\SWE2_Repos\TourPlanner\TourPlanner\TourPlanner\Images\No_Image_Icon.png"))
                return false;
            return true;
        }

    }
}
