using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner.Factory.ViewModel
{
    public partial class SearchViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            SearchViewModel vm = new SearchViewModel();
            return vm;
        }
    }
}
