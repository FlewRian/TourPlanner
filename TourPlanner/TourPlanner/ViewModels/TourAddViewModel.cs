using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Logger;
using TourPlanner.Model;

namespace TourPlanner.ViewModels
{
    public class TourAddViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private string _name;
        private string _description;
        private string _start;
        private string _end;
        private int _distance;
        private Window _window;

        private ITourPlannerFactory _tourPlannerFactory;
        private MainViewModel _mainViewModel;
        private Tour _newTour;

        public ICommand SaveTourCommand => new RelayCommand(SaveTour);
        public ICommand CancelTourAddCommand => new RelayCommand(CancelAddTour);

        public TourAddViewModel(Window window, MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
            this._window = window;
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

        private void SaveTour(object obj)
        {
            _log.Debug("Save Tour klicked");
            if (this.Name != null && this.Description != null && this.Start != null && this.End != null)
            {
                _newTour = _tourPlannerFactory.AddNewItem(this.Name, this.Description, this.Start, this.End,
                    this.Distance);
                _mainViewModel.searchUcViewModel.Items.Add(_newTour);
                _log.Info("Tour could be saved");
            }
            else
            {
                _log.Warn("Tour could not be saved");
                MessageBox.Show("Tour couldn´t be added!", "Tour Add Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            _window.Close();
            MessageBox.Show("Tour successfully added!", "Tour Add", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelAddTour(object obj)
        {
            _log.Debug("Cancel Add Tour klicked");
            _window.Close();
            MessageBox.Show("Tour add was canceled!", "Tour Add", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
