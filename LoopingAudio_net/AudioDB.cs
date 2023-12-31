﻿using System.Data;
using System.Configuration;
using System.Collections.Generic;
using Dapper;
using System.Data.SQLite;
using System.Linq;

namespace LoopingAudio_net
{
    internal class AudioDB
    {
        private readonly string connectionString;
        internal AudioDB()
        {
            //the final piece of the puzzle is to figure out why the connection string is null
            //the project will be officially complete when this happens.
            //app.config is null on client machines. Probably because it isn't included
            //when I included a "config file" on github, the app still complained about it being null

            //My solution is to probably use the publish wizard...

            //SOLUTION
            //CD-ROM... meant removable media
            //deploy the app to a flash drive, then go to client computer and click "Set Up" there...
            connectionString = ConfigurationManager.ConnectionStrings["songs"].ConnectionString;
        }

        internal void SetUp()
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                string checkTable = "SELECT name FROM sqlite_master WHERE type='table' AND name='Music'";

                IEnumerable<string> table = connection.Query<string>(checkTable);
                string tableName = table.FirstOrDefault();
                if (!string.IsNullOrEmpty(tableName) && tableName == "Music")
                    return;

                string createTable = "CREATE TABLE \"Music\" " +
                    "(\r\n\t\"Name\"\tTEXT,\r\n\t\"StartPoint\"\tTEXT," +
                    "\r\n\t\"EndPoint\"\tTEXT,\r\n\t\"Song\"\tBLOB\r\n)";

                connection.Execute(createTable);
            }
        }

        internal List<string> GetSongList()
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                string sql = "SELECT Name FROM Music";
                return connection.Query<string>(sql).ToList();
            }
        }

        internal Music GetSongData(string name)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                string sql = "SELECT * FROM Music " +
                    $"WHERE Name = @Name " +
                    $"LIMIT 1";
                var parameters = new { Name = name };
                return connection.QuerySingleOrDefault<Music>(sql, parameters);
            }
        }

        internal void InsertOrUpdateSong(in Music music)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                string sql = $"SELECT EXISTS(SELECT Name FROM Music WHERE Name = @Name);";
                var parms = new { music.Name };
                int transaction = connection.QuerySingleOrDefault<int>(sql, parms, null);
                if(transaction == 1) // exist, update table
                {
                    sql = "UPDATE Music SET StartPoint = @StartPoint, " +
                        "EndPoint = @EndPoint WHERE Name = @Name";
                    var parameters = new { music.StartPoint, music.EndPoint, music.Name};
                    connection.Execute(sql, parameters, null, 10);

                    //TODO: Find out why the startpoint value is "0". The error occurs in the 
                    //Execute function for some reason.

                    //FIXED, I messed up my SQL syntax, for SET multiple values, there should be
                    //commas to seperate them, not AND
                }
                else //doesn't exist, insert table
                {
                    sql = "INSERT INTO Music " +
                        "VALUES (@Name, @StartPoint, @EndPoint, @Song)";
                    var parameters = new { music.Name, music.StartPoint, music.EndPoint, music.Song };
                    connection.Execute(sql, parameters, null, 10);
                }            
            }
        }

        internal void DeleteSong(string name)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                string sql = "DELETE FROM Music WHERE Name = @Name";
                var parameters = new { Name = name };
                connection.Execute(sql, parameters, null, 10);
            }
        }
    }
}
