using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Model;

namespace TourPlanner.DataAccessLayer.PostgresSQL
{
    public class TourPostgresDAO : ITourDAO
    {
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"Tours\" WHERE \"Id\"=@Id;";
        private const string SQL_GET_ALL_TOURS = "SELECT * FROM public.\"Tours\";";
        private const string SQL_INSERT_NEW_TOUR = "INSERT INTO public.\"Tours\" (\"Name\", \"Description\", \"Start\", \"End\") " +
                                                   "VALUES (@Name, @Description, @Start, @End) " +
                                                   "RETURNING \"Id\";";

        
        private IDatabase database;

        public TourPostgresDAO()
        {
            this.database = DALFactory.GetDatabase();
        }

        public TourPostgresDAO(IDatabase database)
        {
            this.database = database;
        }

        public Tour AddNewItem(string name, string description, string start, string end)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_TOUR);
            database.DefineParameter(insertCommand, "@Name", DbType.String, name);
            database.DefineParameter(insertCommand, "@Name", DbType.String, description);
            database.DefineParameter(insertCommand, "@Name", DbType.String, start);
            database.DefineParameter(insertCommand, "@Name", DbType.String, end);

            return FindById(database.ExecuteScalar(insertCommand));
        }
        public Tour FindById(int itemId)
        {
            DbCommand findCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findCommand, "@Id", DbType.Int32, itemId);

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
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (string)reader["Description"],
                        (string)reader["Start"],
                        (string)reader["End"]
                    ));
                }
            }

            return tourList;
        }
    }
}
