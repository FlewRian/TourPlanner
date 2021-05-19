﻿using TourPlanner.ViewModels;

namespace TourPlanner.Factory.Window
{
    public interface IWindowFactory
    {
        public System.Windows.Window GetWindow(MainViewModel mainViewModel);
    }
}
