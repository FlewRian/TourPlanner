using System.Windows;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaktionslogik für TourLogEditWindow.xaml
    /// </summary>
    public partial class TourLogEditWindow : Window
    {
        public TourLogEditWindow(MainViewModel mainViewModel, TourLog currentTourLog)
        {
            InitializeComponent();
            this.DataContext = new TourLogEditViewModel(currentTourLog, this, mainViewModel);
        }
    }
}
