using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Common;

namespace TourPlanner.DataAccessLayer.FileServer
{
    class FileAccess : IFileAccess
    {
        private string filePath;

        public FileAccess(string filePath)
        {
            this.filePath = filePath;
        }

        private IEnumerable<FileInfo> GetFileInfos(string startFolder, int tourId)
        {
            DirectoryInfo dir = new DirectoryInfo(startFolder);
            return dir.GetFiles("*" + tourId + ".png", SearchOption.AllDirectories);
        }

        private string GetFullPath(string fileName)
        {
            return Path.Combine(filePath, fileName);
        }

        

        public IEnumerable<FileInfo> GetAllFiles()
        {
            throw new NotImplementedException();
        }

        public int CreateNewTourFile()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileInfo> SearchFiles(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
