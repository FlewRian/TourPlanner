using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Logger;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer.PostgresSqlServer
{
    public class TourPostgresDAO : ITourDAO
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private const string SQL_FIND_BY_ID = "SELECT * FROM \"tours\" WHERE \"id\"=@id;";
        private const string SQL_GET_ALL_TOURS = "SELECT * FROM \"tours\";";
        private const string SQL_INSERT_NEW_TOUR = "INSERT INTO \"tours\" (\"name\", \"description\", \"start\", \"end\", \"distance\", \"imagepath\") " +
                                                   "VALUES (@name, @description, @start, @end, @distance, @imagepath) " +
                                                   "RETURNING \"id\";";
        private const string SQL_DELETE_TOUR = "DELETE FROM \"tours\" WHERE \"id\"=@id;";
        private const string SQL_EDIT_TOUR = "UPDATE \"tours\" SET " +
                                           "\"end\"=@end, \"name\"=@name, \"start\"=@start, " +
                                           "\"description\"=@description, \"distance\"=@distance, \"imagepath\"=@imagepath  " +
                                           "WHERE \"id\"=@id RETURNING \"id\";";

        
        private IDatabase _database;

        public TourPostgresDAO()
        {
            this._database = DALFactory.GetDatabase();
        }

        public TourPostgresDAO(IDatabase database)
        {
            this._database = database;
        }

        public Tour AddNewItem(string name, string description, string start, string end, int distance, string imagePath)
        {
            DbCommand insertCommand = _database.CreateCommand(SQL_INSERT_NEW_TOUR);
            _database.DefineParameter(insertCommand, "@name", DbType.String, name);
            _database.DefineParameter(insertCommand, "@description", DbType.String, description);
            _database.DefineParameter(insertCommand, "@start", DbType.String, start);
            _database.DefineParameter(insertCommand, "@end", DbType.String, end);
            _database.DefineParameter(insertCommand, "@distance", DbType.Int32, distance);
            _database.DefineParameter(insertCommand, "@imagepath", DbType.String, imagePath);

            return FindById(_database.ExecuteScalar(insertCommand));
        }
        public Tour FindById(int itemId)
        {
            DbCommand findCommand = _database.CreateCommand(SQL_FIND_BY_ID);
            _database.DefineParameter(findCommand, "@id", DbType.Int32, itemId);

            IEnumerable<Tour> tours = QueryTourFromDatabase(findCommand);
            return tours.FirstOrDefault();
        }

        public IEnumerable<Tour> GetTours()
        {
            DbCommand toursCommand = _database.CreateCommand(SQL_GET_ALL_TOURS);
            return QueryTourFromDatabase(toursCommand);
        }

        private IEnumerable<Tour> QueryTourFromDatabase(DbCommand command)
        {
            List<Tour> tourList = new List<Tour>();

            using (IDataReader reader = _database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourList.Add(new Tour(
                        (int)reader["id"],
                        (string)reader["name"],
                        (string)reader["description"],
                        (string)reader["start"],
                        (string)reader["end"],
                        (int)reader["distance"],
                        (string)reader["ImagePath"]
                    ));
                }
            }

            return tourList;
        }

        public void DeleteTour(Tour tour)
        {
            DbCommand deleteCommand = _database.CreateCommand(SQL_DELETE_TOUR);
            _database.DefineParameter(deleteCommand, "@id", DbType.Int32, tour.Id);
            _log.Info("Delete Tour");
            _database.ExecuteScalar(deleteCommand);
        }

        public Tour EditTour(Tour currentTour, string newName, string newDescription, string newStart, string newEnd, int newDistance, string tourImagePath)
        {
            DbCommand editCommand = _database.CreateCommand(SQL_EDIT_TOUR);
            _database.DefineParameter(editCommand, "@name", DbType.String, newName);
            _database.DefineParameter(editCommand, "@start", DbType.String, newStart);
            _database.DefineParameter(editCommand, "@end", DbType.String, newEnd);
            _database.DefineParameter(editCommand, "@description", DbType.String, newDescription);
            _database.DefineParameter(editCommand, "@distance", DbType.Int32, newDistance);
            _database.DefineParameter(editCommand, "@imagepath", DbType.String, tourImagePath);
            _database.DefineParameter(editCommand, "@id", DbType.Int32, currentTour.Id);

            _log.Info("Tour Update");

            return FindById(_database.ExecuteScalar(editCommand));
        }
    }
}
