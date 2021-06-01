using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Logger;
using TourPlanner.Model;

namespace TourPlanner.ViewModels
{
    public class SearchUcViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        public string searchName;
        public ITourPlannerFactory tourPlannerFactory;

        private MainViewModel _mainViewModel;

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

        public SearchUcViewModel()
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            InitListbox();
        }

        public SearchUcViewModel(MainViewModel mainViewModel)
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            InitListbox();
            this._mainViewModel = mainViewModel;
        }

        private void InitListbox()
        {
            Items = new ObservableCollection<Tour>();
            _log.Debug("Tour Listbox was initialized");
            FillListBox();
        }

        public void FillListBox()
        {
            _log.Debug("Tour Collection gets filled");
            foreach (Tour item in this.tourPlannerFactory.GetItems())
            {
                Items.Add(item);
            }
        }

        private void Search(object commandParameter)
        {
            _log.Debug("Search klicked");
            IEnumerable foundItems = tourPlannerFactory.Search(SearchName); 
            Items.Clear();
            foreach (Tour item in foundItems)
            {
                Items.Add(item);
            }
        }

        private void Clear(object commandParameter)
        {
            _log.Debug("Reset klicked");
            Items.Clear();
            SearchName = ""; 
            FillListBox();
        }
    }
}
