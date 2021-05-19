using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public SearchUcViewModel searchUcViewModel { get; set; }
        public TourInfoUcViewModel tourInfoUcViewModel { get; set; }
        public TourListUserControlViewModel tourListUcViewModel { get; set; }
        public SearchUserControl searchUserControl;
        public TourListUserControl tourListUserControl;
        public TourInfoUserControl tourInfoUserControl;
        private MainViewModel _mainViewModel { get; set; }
        

        public MainViewModel()
        {
            this.searchUcViewModel = new SearchUcViewModel(this);
            this.tourInfoUcViewModel = new TourInfoUcViewModel(this);
            this.tourListUcViewModel = new TourListUserControlViewModel(this);

            this.searchUserControl = new SearchUserControl(searchUcViewModel);
            this.tourListUserControl = new TourListUserControl(tourListUcViewModel);
            this.tourInfoUserControl = new TourInfoUserControl(tourInfoUcViewModel);
        }

    }

}