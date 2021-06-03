using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Logger;
using TourPlanner.Model;

namespace TourPlanner.ViewModels
{
    public class SearchUcViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private string _searchName;
        private ITourPlannerFactory _tourPlannerFactory;

        private MainViewModel _mainViewModel;

        private ICommand _searchCommand;
        private ICommand _clearCommand;
        private ICommand _exportJsonCommand;
        private ICommand _importJsonCommand;
        public ICommand SearchCommand => _searchCommand ??= new RelayCommand(Search);
        public ICommand ClearCommand => _clearCommand ??= new RelayCommand(Clear);
        public ICommand ExportJsonCommand => _exportJsonCommand ??= new RelayCommand(ExportJson);
        public ICommand ImportJsonCommand => _importJsonCommand ??= new RelayCommand(ImportJson);
        public ICommand GeneratePDFReportCommand => new RelayCommand(GeneratePDFReport);
        public ICommand GenerateSummaryCommand => new RelayCommand(GenerateSummary);
        
        public ObservableCollection<Tour> Items { get; set; }
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                if (_searchName != value)
                {
                    _searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName));
                }
            }
        }

        public SearchUcViewModel()
        {
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
            InitListbox();
        }

        public SearchUcViewModel(MainViewModel mainViewModel)
        {
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
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
            foreach (Tour item in this._tourPlannerFactory.GetItems())
            {
                Items.Add(item);
            }
        }

        private void Search(object commandParameter)
        {
            _log.Debug("Search klicked");
            IEnumerable foundItems = _tourPlannerFactory.Search(SearchName); 
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

        private void ExportJson(object obj)
        {
            _log.Debug("Export Json klicked");
            if (_tourPlannerFactory.JsonExport())
            {
                _log.Info("Json was successfully exported.");
                MessageBox.Show("Json successfully exported!", "JSON Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _log.Warn("Json Export failed");
                var dialog = MessageBox.Show("An unexpected error occurred during Json Export!", "JSON Export", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ImportJson(object obj)
        {
            _log.Debug("Import Json klicked");
            if (_tourPlannerFactory.JsonImport())
            {
                _log.Info("Json was successfully imported");
                FillListBox();
                MessageBox.Show("Json successfully imported!", "JSON Import", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _log.Warn("Json Import failed");
                var dialog = MessageBox.Show("An unexpected error occurred during Json Import!", "JSON Import", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GeneratePDFReport(object obj)
        {
            _log.Debug("Generate Report klicked");
            if (_mainViewModel.tourListUcViewModel.CurrentItem != null)
            {
                if (_tourPlannerFactory.GenerateReportPDF(_mainViewModel.tourListUcViewModel.CurrentItem, _mainViewModel.tourInfoUcViewModel.TourLogs))
                {
                    _log.Info("PDF Report was successfully generated");
                    MessageBox.Show("PDF Report successfully generated.", "Report Generator", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    _log.Warn("PDF Report could not be generated");
                    MessageBox.Show("An unexpected error occurred while creating the report!", "Report Generator",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                _log.Debug("No Tour was selected, could not generate a report");
                MessageBox.Show("No Tour selected", "Report Generator",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateSummary(object obj)
        {
            _log.Debug("Generate Summary klicked");
            if (_tourPlannerFactory.GenerateSummary(_mainViewModel.tourListUcViewModel.CurrentItem))
            {
                _log.Info("PDF Summary was successfully generated");
                MessageBox.Show("PDF Summary successfully generated.", "Summary Generator", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                _log.Warn("PDF Summary could not be generated");
                MessageBox.Show("An unexpected error occurred while creating the summary!", "Summary Generator",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
