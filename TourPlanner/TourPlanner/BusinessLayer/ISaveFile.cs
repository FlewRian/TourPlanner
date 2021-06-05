namespace TourPlanner.BusinessLayer {
    public interface ISaveFile
    {
        string Filter { get; set; }
        bool? ShowDialog();
        string FileName { get; set; }
    }
}
