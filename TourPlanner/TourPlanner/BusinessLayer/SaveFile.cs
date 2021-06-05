using System;
using Microsoft.Win32;
using TourPlanner.BusinessLayer.Json;

namespace TourPlanner.BusinessLayer {
    class SaveFile : ISaveFile {

        private SaveFileDialog _saveFile;

        public string Filter { 
            get => _saveFile.Filter;
            set => _saveFile.Filter = value;
        }

        public string FileName
        {
            get => _saveFile.FileName;
            set => _saveFile.FileName = value;
        }

        public SaveFile()
        {
            _saveFile = new SaveFileDialog();
        }
        public bool? ShowDialog()
        {
            return _saveFile.ShowDialog();
        }

        
    }
}
