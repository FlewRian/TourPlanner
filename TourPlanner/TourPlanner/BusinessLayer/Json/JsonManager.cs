using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer.Json
{
    public class JsonManager : IJsonManager
    {
        private ISaveFile _saveFileDialog;
        private IOpenFile _openFileDialog;
        public JsonManager(ISaveFile saveFileDialog, IOpenFile openFileDialog)
        {
            _saveFileDialog = saveFileDialog;
            _openFileDialog = openFileDialog;
        }
        public bool JsonExport(IEnumerable<Tour> tours, IEnumerable<TourLog> tourLogs)
        {
            _saveFileDialog.Filter = "Json Files (*.json) | *.json";
            _saveFileDialog.ShowDialog();
            if (!_saveFileDialog.FileName.Equals(""))
            {
                Tour_LogJson data = new Tour_LogJson(tours, tourLogs);
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(_saveFileDialog.FileName, json);
                return true;
            }
            return false;
        }

        public Tour_LogJson JsonImport()
        {
            _openFileDialog.Title = "Open a JSON TourPlanner File you want to import";  
            _openFileDialog.Filter = "Json Files (*.json) | *.json";
            _openFileDialog.ShowDialog();  
            if (!_openFileDialog.FileName.Equals("")) {  
                StreamReader streamReader = new StreamReader(_openFileDialog.FileName);
                string json = streamReader.ReadToEnd();  
                streamReader.Close();
                return JsonConvert.DeserializeObject<Tour_LogJson>(json);
            }
            return null;
        }
    }
}
