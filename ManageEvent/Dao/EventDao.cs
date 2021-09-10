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
        public List<Event> getEventList(string userId, string search)
        {
            SqlConnection connection = Connection.createConnection();
            List<Event> events = null;
            string query = "SELECT id,name,description,status FROM dbo.tblEvent WHERE userID=@userName And status!='deleted' And name like @search And status != 'checked in'";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userName", userId);
            cmd.Parameters.AddWithValue("@search", "%" + search + "%");
            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                events = new List<Event>();
                while (dataReader.Read())
                {
                    Event newEvent = new Event();
                    newEvent.Id = dataReader.GetInt32(0);
                    newEvent.Name = dataReader.GetString(1);
                    newEvent.Description = dataReader.GetString(2);
                    newEvent.Status = dataReader.GetString(3);
                    events.Add(newEvent);
                }
}
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                connection.Close();
            }
            return events;
        }
    }
}
