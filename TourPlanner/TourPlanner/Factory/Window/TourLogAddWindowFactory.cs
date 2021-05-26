using System;
using TourPlanner.Model;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner.Factory.Window
{
    public class TourLogAddWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow(MainViewModel mainViewModel, Tour currentTour)
        {
            TourLogAddWindow view = new TourLogAddWindow(currentTour, mainViewModel);
            return view;
        }

        public System.Windows.Window GetWindow(MainViewModel mainViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
