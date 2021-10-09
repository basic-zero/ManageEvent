using ManageEvent.Dto;
using ManageEvent.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dao
{
    public class EventAttendeesDao
    {
       
        
        public List<EventAttendees> GetAllEventAttendees(int groupID)
        {
            SqlConnection connection = Connection.createConnection();
            List<EventAttendees> list = null;
            EventAttendees eventAttendees;
            string query = "SELECT name, email, other, status, groupID, id " +
                "FROM tblEventAttendees " +
                "WHERE groupID = @groupID AND status = 1";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@groupID", groupID);
            try
            {
                list = new List<EventAttendees>();
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    eventAttendees = new EventAttendees();
                    eventAttendees.Name = dataReader.GetString(0);
                    eventAttendees.Email = dataReader.GetString(1);
                    eventAttendees.Other = dataReader.GetString(2);
                    eventAttendees.Status = dataReader.GetBoolean(3);
                    eventAttendees.GroupId = dataReader.GetInt32(4);
                    eventAttendees.Id = dataReader.GetInt32(5);
                    list.Add(eventAttendees);
                }
            }
            finally 
            { 
                connection.Close();
            }
            return list;
        }
        public bool Create(EventAttendees eventAttendees)
        {
            SqlConnection connection = Connection.createConnection();
            int check = 0;
            string query = "INSERT INTO dbo.tblEventAttendees(groupID, name, email, other, status) " +
                "Values(@groupID, @name, @email, @other, 1)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@groupID", eventAttendees.GroupId);
            cmd.Parameters.AddWithValue("@name", eventAttendees.Name);
            cmd.Parameters.AddWithValue("@email", eventAttendees.Email);
            cmd.Parameters.AddWithValue("@other", eventAttendees.Other);
            try
            {
                connection.Open();
                check = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {
                connection.Close();
            }
            return check == 1;
        }
        public bool DeleteById(int Id)
        {
            SqlConnection connection = Connection.createConnection();
            SqlCommand cmd = new SqlCommand("UPDATE tblEventAttendees SET status='False' WHERE id=@id", connection);
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
        public bool Update(EventAttendees eventAttendees)
        {
            SqlConnection connection = Connection.createConnection();
            SqlCommand cmd = new SqlCommand("UPDATE tblEventAttendees SET name=@name, email=@email, other=@other WHERE id=@id;", connection);
            cmd.Parameters.AddWithValue("@id", eventAttendees.Id);
            cmd.Parameters.AddWithValue("@name", eventAttendees.Name);
            cmd.Parameters.AddWithValue("@email", eventAttendees.Email);
            cmd.Parameters.AddWithValue("@other", eventAttendees.Other);
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
