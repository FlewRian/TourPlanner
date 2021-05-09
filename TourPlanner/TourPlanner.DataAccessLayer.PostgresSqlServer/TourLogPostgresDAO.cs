using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer.PostgresSQL
{
    public class TourLogPostgresDAO : ITourLogDAO
    {
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"TourLogs\" WHERE \"Id\"=@Id;";
        private const string SQL_FIND_BY_TOURID = "SELECT * FROM public.\"TourLogs\" WHERE \"TourId\"=@TourId;";
        private const string SQL_INSERT_NEW_TOURLOG =
            "INSERT INTO public.\"TourLogs\" (\"Name\", \"Description\", \"Report\", \"Vehicle\", \"DateTime\", \"TourId\", \"Distance\", \"TotalTime\", \"Rating\") " +
            "VALUES (@Name, @Description, @Report, @Vehicle, @DateTime, @TourId, @Distance, @TotalTime, @Rating);";

        private IDatabase database;
        private ITourDAO tourDAO;

        public TourLogPostgresDAO()
        {
            this.database = DALFactory.GetDatabase();
            this.tourDAO = DALFactory.CreateTourDAO();
        }

        public TourLogPostgresDAO(IDatabase database, ITourDAO tourDao)
        {
            this.database = database;
            this.tourDAO = tourDao;
        }

        public TourLog AddNewTourLog(string name, string description, string report, string vehicle, DateTime dateTime, int tourId, double distance, double totalTime, int rating)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_TOURLOG);
            database.DefineParameter(insertCommand, "@Name", DbType.String, name);
            database.DefineParameter(insertCommand, "@Description", DbType.String, description);
            database.DefineParameter(insertCommand, "@Report", DbType.String, report); 
            database.DefineParameter(insertCommand, "@Vehicle", DbType.String, vehicle);
            database.DefineParameter(insertCommand, "@DateTime", DbType.String, dateTime.ToString());
            database.DefineParameter(insertCommand, "@TourId", DbType.Int32, tourId);
            database.DefineParameter(insertCommand, "@Distance", DbType.Double, distance);
            database.DefineParameter(insertCommand, "@TotalTime", DbType.Double, totalTime);
            database.DefineParameter(insertCommand, "@Rating", DbType.Int32, rating);

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public TourLog FindById(int logId)
        {
            DbCommand findCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findCommand, "@Id", DbType.Int32, logId);

            IEnumerable<TourLog> tourlogList = QueryMediaLogsFromDb(findCommand);
            return tourlogList.FirstOrDefault();
        }

        public IEnumerable<TourLog> GetLogsForTour(int tourId)
        {
            DbCommand getLogsCommand = database.CreateCommand(SQL_FIND_BY_TOURID);
            database.DefineParameter(getLogsCommand, "@TourId", DbType.Int32, tourId);

            return QueryMediaLogsFromDb(getLogsCommand);
        }

        private IEnumerable<TourLog> QueryMediaLogsFromDb(DbCommand command)
        {
            List<TourLog> tourLogList = new List<TourLog>();

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourLogList.Add(new TourLog(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (string)reader["Description"],
                        (string)reader["Report"],
                        (string)reader["Vehicle"],
                        DateTime.Parse(reader["DateTime"].ToString()),
                        (int)reader["TourId"],
                        (int)reader["Distance"],
                        (int)reader["TotalTime"],
                        (int)reader["Rating"]
                    ));
                }
            }

            return tourLogList;
        }
    }
}
