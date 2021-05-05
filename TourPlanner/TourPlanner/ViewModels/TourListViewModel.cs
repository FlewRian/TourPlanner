using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Factory.Window;
using TourPlanner.Model;

namespace TourPlanner.ViewModels
{
    public class TourListViewModel : ViewModelBase
    {
        public IMediaItemFactory mediaItemFactory;
        private MediaItem currentItem;
        private readonly IWindowFactory _windowFactoryAddTour;
        public ICommand AddTourCommand => new RelayCommand(AddTour);

        public ObservableCollection<MediaItem> Items { get; set; }

        public string searchName;
        private ICommand searchCommand;
        private ICommand clearCommand;
        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);
        public ICommand ClearCommand => clearCommand ??= new RelayCommand(Clear);

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
        public MediaItem CurrentItem
        {
            get
            {
                return currentItem;
            }
            set
            {
                if ((currentItem != value) && (value != null))
                {
                    currentItem = value;
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                }   
            }
        }

        public TourListViewModel(IWindowFactory windowFactoryAddTour)
        {
            _windowFactoryAddTour = windowFactoryAddTour;
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

        public void AddTour(object obj)
        {
            Debug.WriteLine("AddTour klicked");
            Window view = _windowFactoryAddTour.GetWindow();
            view.Show();
        }

        public new event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Search(object commandParameter)
        {
            IEnumerable foundItems = this.mediaItemFactory.Search(SearchName); 
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
