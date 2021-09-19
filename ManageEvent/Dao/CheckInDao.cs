using ManageEvent.Dto;
using ManageEvent.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dao
{
    public class CheckInDao
    {
        public List<CheckIn> GetAll(int eventId)
        {
            SqlConnection connection = Connection.createConnection();
            List<CheckIn> list = null;
            CheckIn checkIn;
            string query = "SELECT name, email, other, status, id " +
                "FROM tblCheckIn " +
                "WHERE eventId = @eventId and status = 1";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@eventId", eventId);
            try
            {
                list = new List<CheckIn>();
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    checkIn = new CheckIn();
                    checkIn.Name = dataReader.GetString(0);
                    checkIn.Email = dataReader.GetString(1);
                    checkIn.Other = dataReader.GetString(2);
                    checkIn.Status = dataReader.GetBoolean(3);
                    checkIn.Id = dataReader.GetInt32(4);
                    checkIn.EventId = eventId;
                    list.Add(checkIn);
                }
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        public bool Create(CheckIn checkIn)
        {
            SqlConnection connection = Connection.createConnection();
            int check = 0;
            string query = "INSERT INTO dbo.tblCheckIn(eventId, name, email, other, status) " +
                "Values(@eventId, @name, @email, @other, 0)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@eventId", checkIn.EventId);
            cmd.Parameters.AddWithValue("@name", checkIn.Name);
            cmd.Parameters.AddWithValue("@email", checkIn.Email);
            cmd.Parameters.AddWithValue("@other", checkIn.Other);
            try
            {
                connection.Open();
                check = cmd.ExecuteNonQuery();
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
            SqlCommand cmd = new SqlCommand("DELETE FROM tblCheckIn WHERE id=@id", connection);
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
        public bool Update(CheckIn checkIn)
        {
            SqlConnection connection = Connection.createConnection();
            SqlCommand cmd = new SqlCommand("UPDATE tblCheckIn SET name=@name, email=@email, other=@other WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("@id", checkIn.Id);
            cmd.Parameters.AddWithValue("@name", checkIn.Name);
            cmd.Parameters.AddWithValue("@email", checkIn.Email);
            cmd.Parameters.AddWithValue("@other", checkIn.Other);
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

        public bool CheckIn(int id)
        {
            SqlConnection connection = Connection.createConnection();
            SqlCommand cmd = new SqlCommand("UPDATE tblCheckIn SET status=1 WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("@id", id);
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
