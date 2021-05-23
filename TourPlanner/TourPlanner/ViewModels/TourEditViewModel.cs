using System.Diagnostics;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Model;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    class TourEditViewModel : ViewModelBase
    {
        private TourEditWindow _tourEditWindow;
        private MainViewModel _mainViewModel;
        private Tour _currentTour;
        private string _name;
        private string _description;
        private string _start;
        private string _end;
        private int _distance;

        private ITourPlannerFactory _tourPlannerFactory;

        public ICommand EditTourCommand => new RelayCommand(EditTour);
        public ICommand CancelEditTourCommand => new RelayCommand(CancelEditTour);

        public TourEditViewModel(TourEditWindow tourEditWindow, MainViewModel mainViewModel, Tour currentTour)
        {
            this._mainViewModel = mainViewModel;
            this._tourEditWindow = tourEditWindow;
            this._currentTour = currentTour;
            this.Name = this._currentTour.Name;
            this.Description = this._currentTour.Description;
            this.Start = this._currentTour.Start;
            this.End = this._currentTour.End;
            this.Distance = this._currentTour.Distance;

            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
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
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChangedEvent(nameof(Description));
            }
        }

        public string Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                RaisePropertyChangedEvent(nameof(Start));
            }
        }

        public string End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                RaisePropertyChangedEvent(nameof(End));
            }
        }

        public int Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value;
                RaisePropertyChangedEvent(nameof(Distance));
            }
        }

        private void EditTour(object commandParameter)
        {

            Debug.WriteLine("Update Tour klicked");
            Tour tour = _tourPlannerFactory.EditTour(_currentTour, Name, Description, Start, End, Distance);
            if (tour != null)
            {
                _mainViewModel.searchUcViewModel.Items.Remove(_currentTour);
                _mainViewModel.searchUcViewModel.Items.Add(tour);
            }
            _tourEditWindow.Close();
        }

        private void CancelEditTour(object obj)
        {
            Debug.WriteLine("Cancel klicked");
            _tourEditWindow.Close();
        }
    }
}
