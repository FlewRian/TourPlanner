using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Factory.Window;
using TourPlanner.Logger;
using TourPlanner.Model;

namespace TourPlanner.ViewModels
{
    public class TourInfoUcViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private MainViewModel _mainViewModel;
        private ITourPlannerFactory _tourPlannerFactory;
        private TourLog _currentTourLog;
        public ICommand AddTourLogCommand => new RelayCommand(AddTourLog);
        public ICommand EditTourLogCommand => new RelayCommand(EditTourLog);
        public ICommand DeleteTourLogCommand => new RelayCommand(DeleteTourLog);
        public ObservableCollection<TourLog> TourLogs { get; set; }

        private readonly IWindowFactory _windowFactoryAddTourLog = new TourLogAddWindowFactory();
        private readonly IWindowFactory _windowFactoryEditTourLog = new TourLogEditWindowFactory();

        public TourLog CurrentTourLog
        {
            get
            {
                return _currentTourLog;
            }
            set
            {
                if ((_currentTourLog != value) && (value != null))
                {
                    _currentTourLog= value;
                    _log.Debug("CurrentItem was changed");
                    RaisePropertyChangedEvent(nameof(CurrentTourLog));
                }   
            }
        }

        public TourInfoUcViewModel(MainViewModel mainViewModel)
        {
            this._tourPlannerFactory = BusinessLayer.TourPlannerFactory.GetInstance();
            this._mainViewModel = mainViewModel;
            InitListbox();
        }

        private void InitListbox()
        {
            TourLogs = new ObservableCollection<TourLog>();
            _log.Debug("TourLog Listbox was initialized");
        }

        public void FillTourLogListBox(Tour tour)
        {
            _log.Debug("TourLog Collection gets filled");
            if (tour != null)
            {
                TourLogs.Clear();
                foreach (var log in this._tourPlannerFactory.GetTourLogs(tour))
                {
                    TourLogs.Add(log);
                }
            }
            else
            {
                _log.Warn("No Tour selected");
                MessageBox.Show("No Tour selected!", "TourLog List Fill", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTourLog(object obj)
        {
            if (_mainViewModel.tourListUcViewModel.CurrentItem != null)
            {
                _log.Debug("AddTourLog klicked");
                Window view = _windowFactoryAddTourLog.GetWindow(_mainViewModel, _mainViewModel.tourListUcViewModel.CurrentItem);
                view.Show();
            }
            else
            {
                _log.Warn("No Tour selected");
                MessageBox.Show("No Tour selected!", "TourLog Add", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditTourLog(object obj)
        {
            if (_currentTourLog != null)
            {
                _log.Debug("EditTourLog klicked");
                Window view = _windowFactoryEditTourLog.GetWindow(_mainViewModel, _currentTourLog);
                view.Show();
            }
            else
            {
                _log.Warn("No TourLog selected");
                MessageBox.Show("No TourLog selected!", "TourLog Edit", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteTourLog(object obj)
        {
            _log.Debug("DeleteTourLog klicked");
            if (CurrentTourLog != null)
            {
                _tourPlannerFactory.DeleteTourLog(CurrentTourLog);
                TourLogs.Remove(CurrentTourLog);
                CurrentTourLog = null;
            }
            else
            {
                _log.Warn("No TourLog selected");
                MessageBox.Show("No TourLog selected!", "TourLog Delete", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
