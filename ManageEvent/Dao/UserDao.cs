using ManageEvent.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dao
{
    public class UserDao
    {
        public bool Authentication(string token)
        {
            SqlConnection connection = Connection.createConnection();
            string query = "select name from tblUser where token=@token";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@token", token);
            bool result = false;
            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                result = dataReader.Read();
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

    }
}
