using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    class TourEditViewModel
    {
        private TourEditWindow _tourEditWindow;
        private MainViewModel _mainViewModel;
        public TourEditViewModel(TourEditWindow tourEditWindow, MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
            this._tourEditWindow = tourEditWindow;
        }
    }
}
