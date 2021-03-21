using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SWE2_TourPlanner;

namespace TourPlanner
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand AddTourCommand => new RelayCommand(AddTour);

        public void AddTour(object obj)
        {
            Debug.WriteLine("AddTour klicked");
            TourAddWindowFactory.CreateWindow();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}