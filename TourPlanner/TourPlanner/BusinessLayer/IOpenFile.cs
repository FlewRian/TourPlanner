namespace TourPlanner.BusinessLayer {
    public interface IOpenFile {
        string Filter { get; set; }
        string FileName { get; set; }
        string Title { get; set; }
        bool? ShowDialog();
        
    }
}
