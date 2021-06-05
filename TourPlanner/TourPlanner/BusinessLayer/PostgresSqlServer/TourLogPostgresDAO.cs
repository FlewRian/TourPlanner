using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Logger;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer.PostgresSqlServer
{
    public class TourLogPostgresDAO : ITourLogDAO
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private const string SQL_FIND_BY_ID = "SELECT * FROM \"tourlogs\" WHERE \"id\"=@id;";

        private const string SQL_FIND_BY_TOURID = "SELECT * FROM \"tourlogs\" WHERE \"tourid\"=@tourid;";

        private const string SQL_GET_ALL_LOGS = "SELECT * FROM \"tourlogs\";";

        private const string SQL_INSERT_NEW_TOURLOG =
            "INSERT INTO \"tourlogs\" (\"name\", \"description\", \"report\", \"vehicle\", \"datetime\", \"tourid\", \"distance\", \"totaltime\", \"rating\") " +
            "VALUES (@name, @description, @report, @vehicle, @datetime, @tourid, @distance, @totaltime, @rating) " +
            "RETURNING \"id\";";

        private const string SQL_DELETE_TOURLOG = "DELETE FROM \"tourlogs\" WHERE \"id\"=@id;";

        private const string SQL_EDIT_TOURLOG = "UPDATE \"tourlogs\" SET " +
                                             "\"name\"=@name, \"description\"=@description, \"report\"=@report, \"vehicle\"=@vehicle, " +
                                             "\"datetime\"=@datetime, \"tourid\"=@tourid, \"distance\"=@distance, \"totaltime\"=@totaltime, \"rating\"=@rating " +
                                             "WHERE \"id\"=@id RETURNING \"id\";";

        private IDatabase _database;

        public TourLogPostgresDAO()
        {
            this._database = DALFactory.GetDatabase();
        }

        public TourLog AddNewTourLog(string name, string description, string report, string vehicle, string dateTime, int tourId, decimal distance, decimal totalTime, int rating)
        {
            DbCommand insertCommand = _database.CreateCommand(SQL_INSERT_NEW_TOURLOG);
            _database.DefineParameter(insertCommand, "@name", DbType.String, name);
            _database.DefineParameter(insertCommand, "@description", DbType.String, description);
            _database.DefineParameter(insertCommand, "@report", DbType.String, report); 
            _database.DefineParameter(insertCommand, "@vehicle", DbType.String, vehicle);
            _database.DefineParameter(insertCommand, "@datetime", DbType.String, dateTime);
            _database.DefineParameter(insertCommand, "@tourid", DbType.Int32, tourId);
            _database.DefineParameter(insertCommand, "@distance", DbType.Decimal, distance);
            _database.DefineParameter(insertCommand, "@totaltime", DbType.Decimal, totalTime);
            _database.DefineParameter(insertCommand, "@rating", DbType.Int32, rating);

            return FindById(_database.ExecuteScalar(insertCommand));
        }

        public TourLog FindById(int logId)
        {
            DbCommand findCommand = _database.CreateCommand(SQL_FIND_BY_ID);
            _database.DefineParameter(findCommand, "@id", DbType.Int32, logId);

            IEnumerable<TourLog> tourlogList = QueryTourLogsFromDb(findCommand);
            return tourlogList.FirstOrDefault();
        }

        public IEnumerable<TourLog> GetLogsForTour(int tourId)
        {
            DbCommand getLogsCommand = _database.CreateCommand(SQL_FIND_BY_TOURID);
            _database.DefineParameter(getLogsCommand, "@tourid", DbType.Int32, tourId);

            return QueryTourLogsFromDb(getLogsCommand);
        }

        public IEnumerable<TourLog> GetTourLogs(Tour tour)
        {
            int tourid = tour.Id;
            return GetLogsForTour(tourid);
        }

        public IEnumerable<TourLog> GetAllTourLogs()
        {
            DbCommand getAllLogsCommand = _database.CreateCommand(SQL_GET_ALL_LOGS);
            return QueryTourLogsFromDb(getAllLogsCommand);
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            DbCommand deleteCommand = _database.CreateCommand(SQL_DELETE_TOURLOG);
            _database.DefineParameter(deleteCommand, "@id", DbType.Int32, tourLog.Id);
            _log.Info("TourLog deleted");
            _database.ExecuteScalar(deleteCommand);
        }

        public TourLog EditTourLog(TourLog currentTourLog, string name, string description, string report,
            string vehicle, string dateTime, int tourId, decimal distance, decimal totalTime, int rating)
        {
            DbCommand editCommand = _database.CreateCommand(SQL_EDIT_TOURLOG);
            _database.DefineParameter(editCommand, "@name", DbType.String, name);
            _database.DefineParameter(editCommand, "@description", DbType.String, description);
            _database.DefineParameter(editCommand, "@report", DbType.String, report); 
            _database.DefineParameter(editCommand, "@vehicle", DbType.String, vehicle);
            _database.DefineParameter(editCommand, "@datetime", DbType.String, dateTime);
            _database.DefineParameter(editCommand, "@tourid", DbType.Int32, tourId);
            _database.DefineParameter(editCommand, "@distance", DbType.Decimal, distance);
            _database.DefineParameter(editCommand, "@totaltime", DbType.Decimal, totalTime);
            _database.DefineParameter(editCommand, "@rating", DbType.Int32, rating);
            _database.DefineParameter(editCommand, "@id", DbType.Int32, currentTourLog.Id);

            _log.Info("TourLog updated");

            return FindById(_database.ExecuteScalar(editCommand));
        }

        private IEnumerable<TourLog> QueryTourLogsFromDb(DbCommand command)
        {
            List<TourLog> tourLogList = new List<TourLog>();

            using (IDataReader reader = _database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourLogList.Add(new TourLog(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (string)reader["Description"],
                        (string)reader["Report"],
                        (string)reader["Vehicle"],
                       (string)reader["DateTime"],
                        (int)reader["TourId"],
                        (decimal)reader["Distance"],
                        (decimal)reader["TotalTime"],
                        (int)reader["Rating"]
                    ));
                }
            }

            return tourLogList;
        }
    }
}
