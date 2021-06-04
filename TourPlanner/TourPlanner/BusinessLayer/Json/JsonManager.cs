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
        public bool JsonExport(IEnumerable<Tour> tours, IEnumerable<TourLog> tourLogs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();  
            saveFileDialog.Filter = "Json Files (*.json) | *.json";
            saveFileDialog.ShowDialog();
            if (!saveFileDialog.FileName.Equals(""))
            {
                Tour_LogJson data = new Tour_LogJson(tours, tourLogs);
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(saveFileDialog.FileName, json);
                return true;
            }
            return false;
        }

        public Tour_LogJson JsonImport()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();  
            openFileDialog.Title = "Open a JSON TourPlanner File you want to import";  
            openFileDialog.Filter = "Json Files (*.json) | *.json";
            openFileDialog.ShowDialog();  
            if (!openFileDialog.FileName.Equals("")) {  
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                string json = streamReader.ReadToEnd();  
                streamReader.Close();
                return JsonConvert.DeserializeObject<Tour_LogJson>(json);
            }
            return null;
        }
    }
}
