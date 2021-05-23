using TourPlanner.Model;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner.Factory.Window
{
    class TourEditWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow(MainViewModel mainViewModel, Tour currentTour)
        {
            TourEditWindow view = new TourEditWindow(mainViewModel, currentTour);
            return view;
        }

        public System.Windows.Window GetWindow(MainViewModel mainViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
