using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner.Factory.ViewModel
{
    public partial class TourInfoViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
           TourInfoViewModel vm = new TourInfoViewModel();
            return vm;
        }
    }
}
