using ManageEvent.Dto;
using ManageEvent.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dao {
    public class UserDao {
        public bool Authentication(string token) {
            SqlConnection connection = Connection.createConnection();
            string query = "Select name from tblUser where token=@token";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@token", token);
            bool result = false;
            try {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                result = dataReader.Read();
            } finally {
                connection.Close();
            }
            return result;
        }


        public bool RegisterWithGG(User user, String token) {
            bool check = false;
            SqlConnection connection = Connection.createConnection();
            string query = "Insert into tblUser(Email, Name, Token, Status, Type) Values (@Email,@Name, @Token,@Status, @Type)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Token", token);
            cmd.Parameters.AddWithValue("@Type", user.Type);
            try {
                connection.Open();
                if (cmd.ExecuteNonQuery() == 1) {
                    check = true;
                }
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                connection.Close();
            }
            return check;
        }

        public bool RegisterWithEmailPwd(User user) {
            bool check = false;
            SqlConnection connection = Connection.createConnection();
            string query = "Insert into tblUser(Email, Name, Password, Token, Status, Type) Values(@Email, @Name, @Password, @Token, @Status, @Type)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Token", EncryptionMD5.GetHash(user.Email + "" + user.Password));
            cmd.Parameters.AddWithValue("@Type", user.Type);
            cmd.Parameters.AddWithValue("@Status", 1);
            try {
                connection.Open();
                if (cmd.ExecuteNonQuery() == 1) {
                    check = true;
                }
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                connection.Close();
            }
            return check;
        }

        public string LoginWithEmailPwd(User user) {
            string token = null;
            SqlConnection connection = Connection.createConnection();
            string query = "Select Token From tblUser Where Email = @Email And Password = @Password And Status = 1";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            try {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    token = dataReader.GetString(0);
                }
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                connection.Close();
            }
            return token;

        }


    }


}
