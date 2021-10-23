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
            string query = "Select name from tblUser where token like @token";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@token", "%"+token);
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




        public bool RegisterWithEmailPwd(User user) {
            bool check = false;
            SqlConnection connection = Connection.createConnection();
            string query = "Insert into tblUser(email, name, password, token, status, type) Values(@Email, @Name, @Password, @Token, @Status, @Type)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Token", EncryptionMD5.GetHash(user.Email + "" + user.Password));
            cmd.Parameters.AddWithValue("@Type", 1);
            cmd.Parameters.AddWithValue("@Status", 1);
            try {
                connection.Open();
                if (cmd.ExecuteNonQuery() == 1) {
                    check = true;
                }
            } finally {
                connection.Close();
            }
            return check;
        }

        public bool RegisterWithGG(User user, String token) {
            bool check = false;
            SqlConnection connection = Connection.createConnection();
            string query = "Insert into tblUser(email, name, token, status, type) Values (@Email,@Name, @Token,@Status, @Type)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Token", token);
            cmd.Parameters.AddWithValue("@Type", 0);
            cmd.Parameters.AddWithValue("@Status", 1);
            try {
                connection.Open();
                if (cmd.ExecuteNonQuery() == 1) {
                    check = true;
                }
            }  finally {
                connection.Close();
            }
            return check;
        }

        public UserForLogin LoginWithEmailPwd(User user) {
            string token = null;
            UserForLogin userForLogin = new UserForLogin();
            SqlConnection connection = Connection.createConnection();
            try {
                if (checkEmailExist(user.Email)) {
                    if (!checkEmailType(user.Email, true)) {
                        throw new Exception("incorrect type of account");
                    }
                    if (!checkEmailPwd(user.Email, user.Password)) {
                        throw new Exception("incorrect password");
                    }
                } else {
                    throw new Exception("email does not exist");
                }
                connection.Open();
                string query = "Select token, id From tblUser Where  email = @Email And password = @Password And type = @Type And status = @Status";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Type", 1);
                cmd.Parameters.AddWithValue("@Status", 1);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    userForLogin.Token = dataReader.GetString(0);
                    userForLogin.Id = dataReader.GetInt32(1);
                }
            }  finally {
                connection.Close();
            }
            return userForLogin;

        }

        public UserForLogin LoginWithGG(String token) {
            UserForLogin userForLogin = new UserForLogin();
            SqlConnection connection = Connection.createConnection();
            string query = "Select token, id From tblUser Where token=@Token And type = @Type And status = @Status";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Token", token);
            cmd.Parameters.AddWithValue("@Type", 0);
            cmd.Parameters.AddWithValue("@Status", 1);

            bool result = false;
            try {
                if (checkTokenExist(token)) {
                    if (!checkTokenType(token, false)) {
                        throw new Exception("incorrect type of account");
                    }
                } else {
                    throw new Exception("email does not exist");
                }
               
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    userForLogin.Token = dataReader.GetString(0);
                    userForLogin.Id = dataReader.GetInt32(1);
                }
            } finally {
                connection.Close();
            }
            return userForLogin;
        }

        private bool checkEmailType(string email, bool type) {
            bool result = false;
            SqlConnection connection = Connection.createConnection();
            try {
                connection.Open();
                string query = "Select token From tblUser Where email = @Email And status = @Status And type = @Type";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Status", 1);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    result = true;
                }
            } finally {
                connection.Close();
            }
            return result;
        }

        private bool checkTokenType(string token,bool type) {
            bool result = false;
            SqlConnection connection = Connection.createConnection();
            try {
                connection.Open();
                string query = "Select token From tblUser Where token = @Token And status = @Status And type = @Type";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Token", token);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Status", 1);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    result = true;
                }
            } finally {
                connection.Close();
            }
            return result;
        }

        private bool checkTokenExist(string token) {
            bool result = false;
            SqlConnection connection = Connection.createConnection();
            try {
                connection.Open();
                string query = "Select token From tblUser Where token = @Token And status = @Status";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Token", token);
                cmd.Parameters.AddWithValue("@Status", 1);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    result = true;
                }
            } finally {
                connection.Close();
            }
            return result;
        }


        private bool checkEmailPwd(string email, string password) {
            bool result = false;
            SqlConnection connection = Connection.createConnection();
            try {
                connection.Open();
                string query = "Select token From tblUser Where email = @Email And password = @Password";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    result = true;
                }
            } finally {
                connection.Close();
            }
            return result;
        }

        private bool checkEmailExist(string email) {
            bool result = false;
            SqlConnection connection = Connection.createConnection();
            try {
                connection.Open();
                string query = "Select token From tblUser Where email = @Email";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    result = true;
                }
            } finally {
                connection.Close();
            }
            return result;
        }


    }


}
