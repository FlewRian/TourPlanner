using System.Windows;
using TourPlanner.Factory.Window;
using TourPlanner.ViewModels;

namespace TourPlanner.Factory.ViewModel
{
    public partial class TourListViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            IWindowFactory windowFactoryAddTour = new TourAddWindowFactory();
            TourListViewModel vm = new TourListViewModel(windowFactoryAddTour);
            return vm;
        }
    }
}
