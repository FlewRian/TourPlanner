﻿using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner.Factory.Window
{
    class TourAddWindowFactory : IWindowFactory
    {
        public System.Windows.Window GetWindow(MainViewModel mainViewModel)
        {
            TourAddWindow view = new TourAddWindow(mainViewModel);
            return view;
        }
    }
}
