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
    public class TourInfoUcViewModel : ViewModelBase
    {
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
                    Debug.WriteLine("CurrentItem was changed");
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
            Debug.WriteLine("TourLogliste erstellt");
        }

        public void FillTourLogListBox(Tour tour)
        {
            Debug.WriteLine("TourLog Collection wird befüllt.");
            if (tour != null)
            {
                TourLogs.Clear();
                foreach (var log in this._tourPlannerFactory.GetTourLogs(tour))
                {
                    TourLogs.Add(log);
                }
            }
            else
                Debug.WriteLine("es wurde keine Tour ausgewählt");

        }

        private void AddTourLog(object obj)
        {
            if (_mainViewModel.tourListUcViewModel.CurrentItem != null)
            {
                Debug.WriteLine("AddTourLog klicked");
                //TourLogAddWindow view = new TourLogAddWindow(_mainViewModel.tourListUcViewModel.CurrentItem, _mainViewModel);
                Window view = _windowFactoryAddTourLog.GetWindow(_mainViewModel, _mainViewModel.tourListUcViewModel.CurrentItem);
                view.Show();
            }
            else
            {
                Debug.WriteLine("No Tour selected");
            }
        }

        private void EditTourLog(object obj)
        {
            Debug.WriteLine("EditTourLog klicked");
            //Window view = _windowFactoryAddTour.GetWindow();
            TourLogEditWindow view = new TourLogEditWindow();
            view.Show();
        }

        private void DeleteTourLog(object obj)
        {
            Debug.WriteLine("DeleteTourLog klicked");
            if (CurrentTourLog != null)
            {
                _tourPlannerFactory.DeleteTourLog(CurrentTourLog);
                CurrentTourLog = null;
                TourLogs.Remove(CurrentTourLog);
                //Logs.Clear();
                //FillTourListBox();
            }
            else Debug.WriteLine("no TourLog selected");
        }
    }
}
