using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner.Factory.Window
{
    class TourLogEditWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow(MainViewModel mainViewModel, Tour currentTour)
        {
            throw new NotImplementedException();
        }

        public System.Windows.Window GetWindow(MainViewModel mainViewModel)
        {
            throw new NotImplementedException();
        }

        public System.Windows.Window GetWindow(MainViewModel mainViewModel, TourLog currentTourLog)
        {
            TourLogEditWindow view = new TourLogEditWindow(mainViewModel, currentTourLog);
            return view;
        }
    }
}
