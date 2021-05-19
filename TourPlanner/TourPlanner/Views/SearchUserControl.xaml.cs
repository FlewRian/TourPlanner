using System.Windows.Controls;
using TourPlanner.ViewModels;


namespace TourPlanner.Views
{
    /// <summary>
    /// Interaktionslogik für SearchUserControl.xaml
    /// </summary>
    public partial class SearchUserControl : UserControl
    {
        public SearchUserControl(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = new SearchUcViewModel(mainViewModel);
        }

        public SearchUserControl(SearchUcViewModel searchUcViewModel)
        {
            InitializeComponent();
            this.DataContext = searchUcViewModel;
        }

        public SearchUserControl()
        {
            InitializeComponent();
        }
    }
}
