using System.Configuration;
using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner.Factory.ViewModel
{
    class TourAddViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            TourAddViewModel vm = new TourAddViewModel();
            return vm;
        }
    }
}
