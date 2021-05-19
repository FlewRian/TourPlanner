using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner.Factory.Window
{
    class TourEditWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow(MainViewModel mainViewModel)
        {
            TourEditWindow view = new TourEditWindow(mainViewModel);
            return view;
        }
    }
}
