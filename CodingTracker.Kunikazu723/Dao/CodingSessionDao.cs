using CodingTracker.Kunikazu723.Dao.Interfaces;
using CodingTracker.Kunikazu723.Models;
using CodingTracker.Kunikazu723.Seeders;
using Dapper;
using System.Data.SQLite;

namespace CodingTracker.Kunikazu723.Dao
{
    public class CodingSessionDao : IDao<CodingSession>
    {
        private readonly string _connectionString;

        public CodingSessionDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void DeleteItemById(int id)
        {
            throw new NotImplementedException();
        }

        public List<CodingSession> GetAllItems()
        {
            using var connection = new SQLiteConnection(_connectionString);
            string sql = """
                SELECT * FROM CodingSessions
                """;
            return connection.Query<CodingSession>(sql).ToList();
        }

        public void InsertItem(CodingSession item)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(List<CodingSession> collection)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            connection.Execute("""
                INSERT INTO CodingSessions (StartTime, EndTime)
                VALUES (@StartTime, @EndTime)
                """, collection);
            transaction.Commit();
        }

        public void UpdateItemById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
