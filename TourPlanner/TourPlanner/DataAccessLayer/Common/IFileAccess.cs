using System.Collections.Generic;
using System.IO;

namespace TourPlanner.DataAccessLayer.Common
{
    public interface IFileAccess
    {
        int CreateNewTourFile();
        IEnumerable<FileInfo> SearchFiles(string searchTerm);
        IEnumerable<FileInfo> GetAllFiles();
    }
}
