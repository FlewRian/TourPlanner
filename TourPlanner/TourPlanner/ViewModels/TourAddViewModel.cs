using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer.PostgresSqlServer;
using TourPlanner.Model;

namespace TourPlanner.ViewModels
{
    public class TourAddViewModel : ViewModelBase
    {
        private string _name;
        private string _description;
        private string _start;
        private string _end;
        private Window _window;

        private TourPostgresDAO tourPostgresDao;
        private MainViewModel _mainViewModel;
        private Tour _newTour;

        public ICommand SaveTourCommand => new RelayCommand(SaveTour);
        public ICommand CancelAddTourCommand => new RelayCommand(CancelAddTour);

        public TourAddViewModel(Window window, MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
            this._window = window;
            this.tourPostgresDao = new TourPostgresDAO();
        }

        public TourAddViewModel()
        {
            this.tourPostgresDao = new TourPostgresDAO();
        }

        public TourAddViewModel(Window window)
        {
            this._window = window;
            this.tourPostgresDao = new TourPostgresDAO();
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


        private void SaveTour(object obj)
        {
            Debug.WriteLine("Save Tour klicked");
            _newTour = tourPostgresDao.AddNewItem(this.Name, this.Description, this.Start, this.End);
            _mainViewModel.searchUcViewModel.Items.Add(_newTour);
            _window.Close();
        }

        private void CancelAddTour(object obj)
        {
            Debug.WriteLine("Cancel klicked");
            _window.Close();
        }
    }
}
