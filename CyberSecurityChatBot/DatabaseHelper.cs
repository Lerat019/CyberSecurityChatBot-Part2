using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CyberSecurityChatBot
{
    public class DatabaseHelper
    {
        private string connectionString =
            "Server=localhost;Database=cyberbot_db;Uid=root;Pwd=Root1991;";

        // Tests the database connection
        public bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Adds a new task to the database
        public void AddTask(string title, string description, string reminderDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tasks (Title, Description, ReminderDate) VALUES (@title, @desc, @reminder)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@reminder", reminderDate);
                cmd.ExecuteNonQuery();
            }
        }

        // Retrieves all tasks from the database
        public List<string> GetAllTasks()
        {
            List<string> tasks = new List<string>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Title, Description, ReminderDate, IsCompleted FROM tasks";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string status = reader.GetBoolean(4) ? "[Done]" : "[Pending]";
                    string reminder = reader.IsDBNull(3) ? "No reminder" : reader.GetString(3);
                    tasks.Add($"{reader.GetInt32(0)}. {status} {reader.GetString(1)} - {reader.GetString(2)} | Reminder: {reminder}");
                }
            }
            return tasks;
        }

        // Marks a task as completed
        public void CompleteTask(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE tasks SET IsCompleted = TRUE WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // Deletes a task from the database
        public void DeleteTask(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM tasks WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
