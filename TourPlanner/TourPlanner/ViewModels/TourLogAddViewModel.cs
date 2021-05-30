using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.ViewModels
{
    class TourLogAddViewModel : ViewModelBase
    {
        private string _name;
        private string _description;
        private string _report;
        private string _vehicle;
        private string _datetime;
        private decimal _distance;
        private decimal _totaltime;
        private int _rating;
        private int _tourId;

        private Tour _currentTour;
        private Window _window;
        private ITourPlannerFactory _tourPlannerFactory;
        private MainViewModel _mainViewModel;
        private TourLog _newTourLog;

        public ICommand SaveTourLogCommand => new RelayCommand(SaveTourLog);
        public ICommand CancelAddTourLogCommand => new RelayCommand(CancelTourLogAdd);

        public TourLogAddViewModel(Tour currentTour, Window window, MainViewModel mainViewModel)
        {
            this._currentTour = currentTour;
            this.TourId = this._currentTour.Id;
            this._mainViewModel = mainViewModel;
            this._window = window;
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
        }

        public int TourId
        {
            get
            {
                return _tourId;
            }
            set
            {
                _tourId = value;
                RaisePropertyChangedEvent(nameof(TourId));
            }
        }


        public string Name
        {
            get {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChangedEvent(nameof(Name));
            }
        }
        public string Description
        {
            get {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChangedEvent(nameof(Description));
            }
        }
        public string Report
        {
            get {
                return _report;
            }
            set
            {
                _report = value;
                RaisePropertyChangedEvent(nameof(Report));
            }
        }
        public string Vehicle
        {
            get {
                return _vehicle;
            }
            set
            {
                _vehicle = value;
                RaisePropertyChangedEvent(nameof(Vehicle));
            }
        }
        public string DateTime
        {
            get {
                return _datetime;
            }
            set
            {
                _datetime = value;
                RaisePropertyChangedEvent(nameof(DateTime));
            }
        }
        public decimal Distance 
        {
            get {
                return _distance;
            }
            set
            {
                _distance = value;
                RaisePropertyChangedEvent(nameof(Distance));
            }
        }
        public decimal TotalTime
        {
            get {
                return _totaltime;
            }
            set
            {
                if (value == 0)
                {
                    value = 0.001m;
                }
                _totaltime = value;
                RaisePropertyChangedEvent(nameof(TotalTime));
            }
        }
        public int Rating
        {
            get {
                return _rating;
            }
            set
            {
                _rating = value;
                RaisePropertyChangedEvent(nameof(Rating));
            }
        }

        private void SaveTourLog(object obj)
        {
            Debug.WriteLine("Save TourLog klicked");
            _newTourLog = _tourPlannerFactory.AddNewTourLog(this.Name, this.Description, this.Report, this.Vehicle, this.DateTime, this.TourId, this.Distance, this.TotalTime, this.Rating);
            _mainViewModel.tourInfoUcViewModel.TourLogs.Add(_newTourLog);
            _window.Close();
        }

        private void CancelTourLogAdd(object obj)
        {
            Debug.WriteLine("Cancel klicked");
            _window.Close();
        }
    }
}
