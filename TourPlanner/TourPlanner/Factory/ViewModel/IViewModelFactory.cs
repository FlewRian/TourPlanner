using System.Windows;

namespace TourPlanner.Factory.ViewModel
{
    public interface IViewModelFactory
    {
        object CreateViewModel(DependencyObject sender);
    }
}
