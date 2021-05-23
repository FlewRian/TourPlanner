using System.Windows;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaktionslogik für TourEditWindow.xaml
    /// </summary>
    public partial class TourEditWindow : Window
    {
        public TourEditWindow(MainViewModel mainViewModel, Tour currentTour)
        {
            InitializeComponent();
            this.DataContext = new TourEditViewModel(this, mainViewModel, currentTour);
        }
    }
}
