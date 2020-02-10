using System.Linq;
using System.Collections.Generic;
using System;
using DayProgress.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DayProgress.Repo
{
    public class ProgressEntryRepo
    {
        private string _dbConnString;
        private IConfiguration _config;
        private const int _maxContentLengthToDisplay = 250;
        public ProgressEntryRepo(IConfiguration config){            
            _config = config;
            _dbConnString = _config.GetValue<string>("dbConnectionString");
        }
        
        public IEnumerable<ProgressEntry> GetProgressEntries(IEnumerable<int> tagsIds, int daysBeforeToday = 7){
            var result = new List<ProgressEntry>();
            string query = "select * from ProgressEntries where WhenCreated >= @since";
            using(var conn = new MySqlConnection(_dbConnString)){
                conn.Open();
                var command = new MySqlCommand(query, conn);
                var since = DateTime.Now.AddDays(-daysBeforeToday).ToString("yyyy-MM-dd");
                command.Parameters.AddWithValue("@since", since);
                using (var reader = command.ExecuteReader())
                {  
                    while (reader.Read())  
                    {  
                        Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));  
                        var pe = new ProgressEntry(){
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            WhenCreated = reader.GetDateTime(reader.GetOrdinal("WhenCreated"))
                        };

                        if (!string.IsNullOrWhiteSpace(pe.Content) && pe.Content.Length > _maxContentLengthToDisplay)
                            pe.Content = pe.Content.Substring(0, _maxContentLengthToDisplay) + "...";

                        result.Add(pe);
                    }  
                }  
            }

            return result;
        }

        public void CreateProgressEntry(ProgressEntry entry){
            string query = "insert into ProgressEntries (Content) values (@content)";
            using (var conn = new MySqlConnection(_dbConnString))
            {
                conn.Open();
                var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@content", entry.Content);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteProgressEntry(int id)
        {
            string query = "delete from ProgressEntries where Id = @id";
            using (var conn = new MySqlConnection(_dbConnString))
            {
                conn.Open();
                var command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateProgressEntry(){
            throw new NotImplementedException();
        }

        
    }
}