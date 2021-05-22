using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        
        private IDatabase database;

        public TourPostgresDAO()
        {
            this.database = DALFactory.GetDatabase();
        }

        public TourPostgresDAO(IDatabase database)
        {
            this.database = database;
        }

        public Tour AddNewItem(string name, string description, string start, string end, int distance)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_TOUR);
            database.DefineParameter(insertCommand, "@name", DbType.String, name);
            database.DefineParameter(insertCommand, "@description", DbType.String, description);
            database.DefineParameter(insertCommand, "@start", DbType.String, start);
            database.DefineParameter(insertCommand, "@end", DbType.String, end);
            database.DefineParameter(insertCommand, "@distance", DbType.Int32, distance);

            return FindById(database.ExecuteScalar(insertCommand));
        }
        public Tour FindById(int itemId)
        {
            DbCommand findCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findCommand, "@id", DbType.Int32, itemId);

            IEnumerable<Tour> tours = QueryTourFromDatabase(findCommand);
            return tours.FirstOrDefault();
        }

        public IEnumerable<Tour> GetTours()
        {
            DbCommand toursCommand = database.CreateCommand(SQL_GET_ALL_TOURS);
            
            
            return QueryTourFromDatabase(toursCommand);
        }

        private IEnumerable<Tour> QueryTourFromDatabase(DbCommand command)
        {
            List<Tour> tourList = new List<Tour>();

            using (IDataReader reader = database.ExecuteReader(command))
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
    }
}
