using Microsoft.Win32;
using TourPlanner.BusinessLayer.Json;

namespace TourPlanner.BusinessLayer {
    class OpenFile : IOpenFile
    {
        private OpenFileDialog _openFile;

        public string Filter
        {
            get => _openFile.Filter;
            set => _openFile.Filter = value;
        }

        public string FileName
        {
            get => _openFile.FileName;
            set => _openFile.FileName = value;
        }

        public string Title
        {
            get => _openFile.Title;
            set => _openFile.Title = value;
        }

        public OpenFile()
        {
            _openFile = new OpenFileDialog();
        }

        public bool? ShowDialog()
        {
            return _openFile.ShowDialog();
        }

        
    }
}
