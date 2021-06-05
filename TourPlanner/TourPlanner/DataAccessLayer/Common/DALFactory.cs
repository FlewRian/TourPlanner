using System;
using System.Configuration;
using System.Reflection;
using TourPlanner.DataAccessLayer.DAO;

namespace TourPlanner.DataAccessLayer.Common
{
    public class DALFactory
    {
        public static string assemblyName;
        private static Assembly dalAssembley;
        private static IDatabase database;

        static DALFactory()
        {
            assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            dalAssembley = Assembly.Load(assemblyName!);
        }

        public static IDatabase GetDatabase()
        {
            if (database == null)
            {
                database = CreateDatabase();
            }
            return database;
        }

        private static IDatabase CreateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgresSqlConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        private static IDatabase CreateDatabase(string connectionString)
        {
            string dataBaseClasseName = assemblyName + ".BusinessLayer.PostgresSqlServer.Database";
            Type dbClass = dalAssembley.GetType(dataBaseClasseName);

            return Activator.CreateInstance(dbClass, new object[] {connectionString}) as IDatabase;
        }
        
        public static ITourDAO CreateTourDAO()
        {
            string className = assemblyName + ".BusinessLayer.PostgresSqlServer.TourPostgresDAO";
            Type tourType = dalAssembley.GetType(className);

            return Activator.CreateInstance(tourType) as ITourDAO;
        } 
        
        public static ITourLogDAO CreateTourLogDAO()
        {
            string className = assemblyName + ".BusinessLayer.PostgresSqlServer.TourLogPostgresDAO";
            Type tourLogType = dalAssembley.GetType(className);

            return Activator.CreateInstance(tourLogType) as ITourLogDAO;
        }
    }
}
