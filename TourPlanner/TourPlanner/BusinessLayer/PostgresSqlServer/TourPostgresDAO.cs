﻿using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer.PostgresSqlServer
{
    public class TourPostgresDAO : ITourDAO
    {
        /*private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"Tours\" WHERE \"Id\"=@Id;";
        private const string SQL_GET_ALL_TOURS = "SELECT * FROM public.\"Tours\";";
        private const string SQL_INSERT_NEW_TOUR = "INSERT INTO public.\"Tours\" (\"Name\", \"Description\", \"Start\", \"End\") " +
                                                   "VALUES (@Name, @Description, @Start, @End) " +
                                                   "RETURNING \"Id\";";*/

        private const string SQL_FIND_BY_ID = "SELECT * FROM \"tours\" WHERE \"id\"=@id;";
        private const string SQL_GET_ALL_TOURS = "SELECT * FROM \"tours\";";
        private const string SQL_INSERT_NEW_TOUR = "INSERT INTO \"tours\" (\"name\", \"description\", \"start\", \"end\", \"distance\") " +
                                                   "VALUES (@name, @description, @start, @end, @distance) " +
                                                   "RETURNING \"id\";";
        private const string SQL_DELETE_TOUR = "DELETE FROM \"tours\" WHERE \"id\"=@id;";
        private const string SQL_EDIT_TOUR = "UPDATE \"tours\" SET " +
                                           "\"end\"=@end, \"name\"=@name, \"start\"=@start, " +
                                           "\"description\"=@description, \"distance\"=@distance " +
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

        public Tour AddNewItem(string name, string description, string start, string end, int distance)
        {
            DbCommand insertCommand = _database.CreateCommand(SQL_INSERT_NEW_TOUR);
            _database.DefineParameter(insertCommand, "@name", DbType.String, name);
            _database.DefineParameter(insertCommand, "@description", DbType.String, description);
            _database.DefineParameter(insertCommand, "@start", DbType.String, start);
            _database.DefineParameter(insertCommand, "@end", DbType.String, end);
            _database.DefineParameter(insertCommand, "@distance", DbType.Int32, distance);

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
                        (int)reader["distance"]
                    ));
                }
            }

            return tourList;
        }

        public void DeleteTour(Tour tour)
        {
            DbCommand deleteCommand = _database.CreateCommand(SQL_DELETE_TOUR);
            _database.DefineParameter(deleteCommand, "@id", DbType.Int32, tour.Id);
            Debug.WriteLine("Tour deleted");
            _database.ExecuteScalar(deleteCommand);
        }

        public Tour EditTour(Tour currentTour, string newName, string newDescription, string newStart, string newEnd, int newDistance)
        {
            DbCommand editCommand = _database.CreateCommand(SQL_EDIT_TOUR);
            _database.DefineParameter(editCommand, "@name", DbType.String, newName);
            _database.DefineParameter(editCommand, "@start", DbType.String, newStart);
            _database.DefineParameter(editCommand, "@end", DbType.String, newEnd);
            _database.DefineParameter(editCommand, "@description", DbType.String, newDescription);
            _database.DefineParameter(editCommand, "@distance", DbType.Int32, newDistance);
            _database.DefineParameter(editCommand, "@id", DbType.Int32, currentTour.Id);

            return FindById(_database.ExecuteScalar(editCommand));
        }
    }
}