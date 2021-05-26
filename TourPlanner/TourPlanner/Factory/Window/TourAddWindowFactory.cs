using TourPlanner.Model;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner.Factory.Window
{
    public class TourAddWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow(MainViewModel mainViewModel)
        {
            TourAddWindow view = new TourAddWindow(mainViewModel);
            return view;
        }

        public System.Windows.Window GetWindow(MainViewModel mainViewModel, Tour currentTour)
        {
            TourAddWindow view = new TourAddWindow(mainViewModel);
            return view;
        }
    }
}
