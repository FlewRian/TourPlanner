using TourPlanner.Factory.ViewModel;
using TourPlanner.Views;

namespace TourPlanner.Factory.Window
{
    class TourAddWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow()
        {
            TourAddWindow view = new TourAddWindow();
            return view;
        }
    }
}
