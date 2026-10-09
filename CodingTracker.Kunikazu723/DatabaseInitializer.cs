using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Kunikazu723
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;
        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute("""
                CREATE TABLE IF NOT EXISTS CodingSessions (
                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                  StartTime TEXT UNIQUE NOT NULL,
                  EndTime TEXT UNIQUE NOT NULL
                )
                """);
        }
    }
}
