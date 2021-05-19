using System.Diagnostics;
using System.Windows.Input;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    public class TourInfoUcViewModel
    {
        private MainViewModel _mainViewModel;
        public ICommand AddTourLogCommand => new RelayCommand(AddTourLog);
        public ICommand EditTourLogCommand => new RelayCommand(EditTourLog);
        public ICommand DeleteTourLogCommand => new RelayCommand(DeleteTourLog);

        public TourInfoUcViewModel(MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
        }

        private void AddTourLog(object obj)
        {
            Debug.WriteLine("AddTourLog klicked");
            //Window view = _windowFactoryAddTour.GetWindow();
            TourLogAddWindow view = new TourLogAddWindow();
            view.Show();
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
        }
    }
}
