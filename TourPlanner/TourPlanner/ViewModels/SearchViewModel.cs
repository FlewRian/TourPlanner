using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Model;


namespace TourPlanner.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public string searchName;
        public IMediaItemFactory mediaItemFactory;

        private ICommand searchCommand;
        private ICommand clearCommand;
        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);
        public ICommand ClearCommand => clearCommand ??= new RelayCommand(Clear);

        public ObservableCollection<MediaItem> Items { get; set; }
        public string SearchName
        {
            get { return searchName; }
            set
            {
                if (searchName != value)
                {
                    searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName));
                }
            }
        }

        public SearchViewModel()
        {
            this.mediaItemFactory = MediaItemFactory.GetInstance();
            InitListbox();
        }

        private void InitListbox()
        {
            Items = new ObservableCollection<MediaItem>();
            FillListBox();
        }

        public void FillListBox()
        {
            foreach (MediaItem item in this.mediaItemFactory.GetItems())
            {
                Items.Add(item);
            }
        }



        private void Search(object commandParameter)
        {
            IEnumerable foundItems = mediaItemFactory.Search(SearchName); 
            Items.Clear();
            foreach (MediaItem item in foundItems)
            {
                Items.Add(item);
            }
        }

        private void Clear(object commandParameter)
        {
            Items.Clear();
            SearchName = ""; 
            FillListBox();
        }
    }
}
