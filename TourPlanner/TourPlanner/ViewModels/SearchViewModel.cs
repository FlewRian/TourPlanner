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
        public ITourPlannerFactory tourPlannerFactory;

        private ICommand searchCommand;
        private ICommand clearCommand;
        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);
        public ICommand ClearCommand => clearCommand ??= new RelayCommand(Clear);

        public ObservableCollection<Tour> Items { get; set; }
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
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            InitListbox();
        }

        private void InitListbox()
        {
            Items = new ObservableCollection<Tour>();
            FillListBox();
        }

        public void FillListBox()
        {
            foreach (Tour item in this.tourPlannerFactory.GetItems())
            {
                Items.Add(item);
            }
        }



        private void Search(object commandParameter)
        {
            IEnumerable foundItems = tourPlannerFactory.Search(SearchName); 
            Items.Clear();
            foreach (Tour item in foundItems)
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
