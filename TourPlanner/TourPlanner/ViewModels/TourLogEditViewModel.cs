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

namespace TourPlanner.ViewModels
{
    class TourLogEditViewModel : ViewModelBase
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

        private TourLog _currentTourLog;
        private Window _window;
        private ITourPlannerFactory _tourPlannerFactory;
        private MainViewModel _mainViewModel;
        private TourLog _newTourLog;

        public ICommand UpdateTourLogCommand => new RelayCommand(UpdateTourLog);
        public ICommand CancelEditTourLogCommand => new RelayCommand(CancelTourLogEdit);

        public TourLogEditViewModel(TourLog currentTourLog, Window window, MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
            this._window = window;
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
            this._currentTourLog = currentTourLog;
            this.TourId = this._currentTourLog.TourId;
            this.Name = this._currentTourLog.Name;
            this.Description = this._currentTourLog.Description;
            this.Report = this._currentTourLog.Report;
            this.Vehicle = this._currentTourLog.Vehicle;
            this.DateTime = this._currentTourLog.DateTime;
            this.Distance = this._currentTourLog.Distance;
            this.TotalTime = this._currentTourLog.TotalTime;
            this.Rating = this._currentTourLog.Rating;
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

        private void UpdateTourLog(object obj)
        {
            Debug.WriteLine("Update TourLog klicked");
            TourLog tourLog = _tourPlannerFactory.EditTourLog(this._currentTourLog, this.Name, this.Description, this.Report, this.Vehicle, this.DateTime, this.TourId, this.Distance, this.TotalTime, this.Rating);
            if (tourLog != null)
            {
                _mainViewModel.tourInfoUcViewModel.TourLogs.Remove(_currentTourLog);
                _mainViewModel.tourInfoUcViewModel.TourLogs.Add(tourLog);
            }
            _window.Close();
        }

        private void CancelTourLogEdit(object obj)
        {
            Debug.WriteLine("Cancel klicked");
            _window.Close();
        }
    }
}
