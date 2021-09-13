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
        SqlConnection connection = Connection.createConnection();
        SqlCommand cmd;
        public List<EventAttendees> GetAllEventAttendees(int groupID)
        {
            List<EventAttendees> list = null;
            EventAttendees eventAttendees;
            string query = "SELECT te.name, te.email, te.other, te.status, te.groupID, te.id " +
                "FROM tblEventAttendees te " +
                "WHERE te.groupID = @groupID";
            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@groupID", groupID);
            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                list = new List<EventAttendees>();
                while (dataReader.Read())
                {
                    eventAttendees = new EventAttendees();
                    eventAttendees.Name = dataReader.GetString(0);
                    eventAttendees.Email = dataReader.GetString(1);
                    eventAttendees.Other = dataReader.GetString(2);
                    eventAttendees.Status = dataReader.GetBoolean(3);
                    eventAttendees.GroupId = dataReader.GetInt32(4);
                    list.Add(eventAttendees);
                }
            }
            catch(Exception e)
            { 
                e.ToString();
            }
            finally 
            { 
                connection.Close();
            }
            return list;
        }
        public bool Create(EventAttendees eventAttendees)
        {
            int check = 0;
            string query = "INSERT INTO tblEventAttendees(groupID, name,email, other, status) " +
                "VALUES(@groupID, @name, @email, @other, 0) ";
            cmd = new SqlCommand(query, connection);
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
            cmd = new SqlCommand("UPDATE tblEventAttendees SET status='True' WHERE id=@id", connection);
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
