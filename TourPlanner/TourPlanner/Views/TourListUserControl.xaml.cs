using System.Windows.Controls;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaktionslogik für TouListUserControl.xaml
    /// </summary>
    public partial class TourListUserControl : UserControl
    {
        public TourListUserControl(TourListUserControlViewModel tourListUcViewModel)
        {
            InitializeComponent();
            this.DataContext = tourListUcViewModel;
        }

        public TourListUserControl()
        {
            InitializeComponent();
        }
    }
}
