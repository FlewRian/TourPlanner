using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Factory.Window;
using TourPlanner.Model;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    public class TourListUserControlViewModel : ViewModelBase
    {
        private Tour _currentItem;
        private MainViewModel _mainViewModel;
        private ITourPlannerFactory _tourPlannerFactory;
        
        private readonly IWindowFactory _windowFactoryAddTour = new TourAddWindowFactory();
        private readonly IWindowFactory _windowFactoryEditTour = new TourEditWindowFactory();
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

                    _mainViewModel.tourInfoUcViewModel.FillTourLogListBox(_currentItem);
                }   
            }
        }

        public TourListUserControlViewModel()
        {
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
        }

        public TourListUserControlViewModel(MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
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
            if (CurrentItem != null)
            {
                Window view = _windowFactoryEditTour.GetWindow(_mainViewModel, CurrentItem);
                //TourEditWindow view = new TourEditWindow(_mainViewModel, CurrentItem);
                view.Show();
            }
            else Debug.WriteLine("No Tour selected");
        }

        private void DeleteTour(object obj)
        {
            Debug.WriteLine("DeleteTour klicked");
            if (CurrentItem != null)
            {
                _tourPlannerFactory.DeleteTour(CurrentItem);
                CurrentItem = null;
                _mainViewModel.searchUcViewModel.Items.Remove(CurrentItem);
                //Logs.Clear();
                //FillTourListBox();
            }
            else Debug.WriteLine("no Tour selected");

        }
    }
}
