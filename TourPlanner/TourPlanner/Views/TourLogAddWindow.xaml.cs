using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner.Model;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaktionslogik für TourLogAddWindow.xaml
    /// </summary>
    public partial class TourLogAddWindow : Window
    {
        public TourLogAddWindow(Tour currentTour, MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = new TourLogAddViewModel(currentTour,this, mainViewModel);
        }
    }
}
