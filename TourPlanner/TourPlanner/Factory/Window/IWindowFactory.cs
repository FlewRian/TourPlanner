using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Factory.Window
{
    public interface IWindowFactory
    {
        public System.Windows.Window GetWindow(MainViewModel mainViewModel, Tour currentTour);
        public System.Windows.Window GetWindow(MainViewModel mainViewModel);
        public System.Windows.Window GetWindow(MainViewModel mainViewModel, TourLog currentTourLog);
    }
}
