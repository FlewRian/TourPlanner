using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Factory.Window;

namespace TourPlanner.ViewModels
{
    public class TourListViewModel : ViewModelBase
    {
        private readonly IWindowFactory _windowFactoryAddTour;
        public ICommand AddTourCommand => new RelayCommand(AddTour);

        public TourListViewModel(IWindowFactory windowFactoryAddTour)
        {
            _windowFactoryAddTour = windowFactoryAddTour;
        }

        public void AddTour(object obj)
        {
            Debug.WriteLine("AddTour klicked");
            Window view = _windowFactoryAddTour.GetWindow();
            view.Show();
        }

        public new event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
