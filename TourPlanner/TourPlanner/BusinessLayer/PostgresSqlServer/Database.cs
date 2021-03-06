using System;
using System.Data;
using System.Data.Common;
using Npgsql;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.Logger;

namespace TourPlanner.BusinessLayer.PostgresSqlServer
{
    public class Database : IDatabase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();
        private string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbCommand CreateCommand(string genericCommandText)
        {
            return new NpgsqlCommand(genericCommandText);
        }

        public int DeclareParameter(DbCommand command, string name, DbType type)
        {
            try
            {
                if (!command.Parameters.Contains(name))
                {
                    int index = command.Parameters.Add(new NpgsqlParameter(name, type));
                    return index;
                }

                throw new ArgumentException($"Parameter {name} already exists");
            }
            catch (ArgumentException ex)
            {
                _log.Error("ArgumentException", ex);
                return -1;
            }
        }

        public void DefineParameter(DbCommand command, string name, DbType type, object value)
        {
            int index = DeclareParameter(command, name, type);
            command.Parameters[index].Value = value;
        }

        public void SetParameter(DbCommand command, string name, object value)
        {
            try
            {
                if (command.Parameters.Contains(name))
                {
                    command.Parameters[name].Value = value;
                }

                throw new ArgumentException($"Parameter {0} does not exist.", name);
            }
            catch (ArgumentException ex)
            {
                _log.Error("ArgumentException", ex);
            }
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            command.Connection = CreateConnection();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteScalar(DbCommand command)
        {
            command.Connection = CreateConnection();
            return Convert.ToInt32(command.ExecuteScalar());
        }

        private DbConnection CreateConnection()
        {
            DbConnection connection = new NpgsqlConnection(this.connectionString);
            connection.Open();

            return connection;
        }
    }
}
