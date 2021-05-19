using System.Windows.Controls;
using TourPlanner.ViewModels;


namespace TourPlanner.Views
{
    /// <summary>
    /// Interaktionslogik für TourInfoUserControl.xaml
    /// </summary>
    public partial class TourInfoUserControl : UserControl
    {
        public TourInfoUserControl(TourInfoUcViewModel tourInfoUcViewModel)
        {
            InitializeComponent();
            this.DataContext = tourInfoUcViewModel;
        }

        public TourInfoUserControl()
        {
            InitializeComponent();
        }
    }
}
