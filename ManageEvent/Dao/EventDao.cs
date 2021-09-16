using ManageEvent.Dto;
using ManageEvent.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace ManageEvent.Dao
{
    public class EventDao
    {
        public List<Event> GetEventList(int userId, string search)
        {
            SqlConnection connection = Connection.createConnection();
            List<Event> events = null;
            string query = "SELECT id,name,description,status FROM dbo.tblEvent WHERE userID=@userName And status!='deleted' And name like @search And status != 'checked in'";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userName", userId);
            cmd.Parameters.AddWithValue("@search", "%" + search + "%");
            try {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                events = new List<Event>();
                while (dataReader.Read()) {
                    Event newEvent = new Event();
                    newEvent.Id = dataReader.GetInt32(0);
                    newEvent.Name = dataReader.GetString(1);
                    newEvent.Description = dataReader.GetString(2);
                    newEvent.Status = dataReader.GetString(3);
                    events.Add(newEvent);
                }
            }
            finally
            {
                connection.Close();
            }
            return events;
        }


        public bool Create(Event dto)
        {
            SqlConnection connection = Connection.createConnection();
            SqlCommand cmd = new SqlCommand("Insert Into tblEvent(name,description,userId, status) VALUES(@name,@description,@userID,@status);", connection);
            cmd.Parameters.AddWithValue("@name", dto.Name);
            cmd.Parameters.AddWithValue("@description", dto.Description);
            cmd.Parameters.AddWithValue("@userID", dto.UserId);
            cmd.Parameters.AddWithValue("@status", "new");
            int count = 0;
            try
            {
                connection.Open();
                count = cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

            return count == 1;
        }

        public bool DeleteById(int Id)
        {
            SqlConnection connection = Connection.createConnection();
            SqlCommand cmd = new SqlCommand("UPDATE dbo.tblEvent SET status='deleted' WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("@id", Id);
            int count = 0;
            try
            {
                connection.Open();
                count = cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

            return count == 1;
        }

        public bool Update(Event dto)
        {
            SqlConnection connection = Connection.createConnection();
            SqlCommand cmd = new SqlCommand("Update tblEvent set name=@name,description=@description where id=@id;", connection);
            cmd.Parameters.AddWithValue("@id", dto.Id);
            cmd.Parameters.AddWithValue("@name", dto.Name);
            cmd.Parameters.AddWithValue("@description", dto.Description);
            int count = 0;
            try
            {
                connection.Open();
                count = cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

            return count == 1;
        }

    }

}

