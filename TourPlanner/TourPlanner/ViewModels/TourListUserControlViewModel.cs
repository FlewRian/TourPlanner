using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Factory.Window;
using TourPlanner.Model;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    public class TourListUserControlViewModel : ViewModelBase
    {
        private Tour _currentItem;
        private MainViewModel _mainViewModel;

        private readonly IWindowFactory _windowFactoryAddTour = new TourAddWindowFactory();
        public ICommand AddTourCommand => new RelayCommand(AddTour);
        public ICommand EditTourCommand => new RelayCommand(EditTour);
        public ICommand DeleteTourCommand => new RelayCommand(DeleteTour);

        public Tour CurrentItem
        {
            get
            {
                return _currentItem;
            }
            set
            {
                if ((_currentItem != value) && (value != null))
                {
                    _currentItem = value;
                    Debug.WriteLine("CurrentItem was changed");
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                }   
            }
        }

        public TourListUserControlViewModel()
        {
            
        }

        public TourListUserControlViewModel(MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
        }

        public void AddTour(object obj)
        {
            Debug.WriteLine("AddTour klicked");
            Window view = _windowFactoryAddTour.GetWindow(_mainViewModel);
            //TourAddWindow view = new TourAddWindow(_mainViewModel);
            view.Show();
        }

        public void EditTour(object obj)
        {
            Debug.WriteLine("EditTour klicked");
            //Window view = _windowFactoryAddTour.GetWindow();
            TourEditWindow view = new TourEditWindow(_mainViewModel);
            view.Show();
        }

        private void DeleteTour(object obj)
        {
            Debug.WriteLine("DeleteTour klicked");
        }
    }
}
