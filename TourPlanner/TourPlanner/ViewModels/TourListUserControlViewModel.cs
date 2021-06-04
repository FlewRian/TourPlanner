using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Factory.Window;
using TourPlanner.Logger;
using TourPlanner.Model;

namespace TourPlanner.ViewModels
{
    public class TourListUserControlViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

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
                    _log.Debug("CurrentItem was changed");
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
            _log.Debug("AddTour klicked");
            Window view = _windowFactoryAddTour.GetWindow(_mainViewModel);
            view.Show();
            CurrentItem = null;
        }

        public void EditTour(object obj)
        {
            _log.Debug("EditTour klicked");
            if (CurrentItem != null)
            {
                Window view = _windowFactoryEditTour.GetWindow(_mainViewModel, CurrentItem);
                view.Show();
            }
            else 
            {
                _log.Warn("No Tour selected");
                MessageBox.Show("No Tour selected!", "Tour Edit", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteTour(object obj)
        {
            _log.Debug("DeleteTour klicked");
            if (CurrentItem != null)
            {
                if (CurrentItem.TourHasImage())
                {
                    _tourPlannerFactory.DeleteTour(CurrentItem, CurrentItem.ImagePath);
                    CurrentItem.ImagePath = null;
                    _mainViewModel.searchUcViewModel.Items.Remove(CurrentItem);
                    _mainViewModel.tourInfoUcViewModel.TourLogs.Clear();
                    CurrentItem = null;
                }
            }
            else 
            {
                _log.Warn("No Tour selected");
                MessageBox.Show("No Tour selected!", "Tour Delete", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
