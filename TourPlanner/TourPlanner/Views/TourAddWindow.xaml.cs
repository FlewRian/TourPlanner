using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaktionslogik für TourAddWindow.xaml
    /// </summary>
    public partial class TourAddWindow : Window
    {
        public TourAddWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = new TourAddViewModel(this, mainViewModel);
        }
    }
}
