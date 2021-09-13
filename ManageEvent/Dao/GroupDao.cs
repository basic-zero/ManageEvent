using ManageEvent.Dto;
using ManageEvent.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dao {
    public class GroupDao {
        //get list group by userId and name of group
        public List<Group> getGroupList(int userId, String search) {
            SqlConnection connection = Connection.createConnection();
            List<Group> groups = null;
            String query = "SELECT id, name, description, status From tblGroup Where userId = @userId AND name like @search AND status = 1 ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@search", "%" + search + "%");
            try {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                groups = new List<Group>();
                while (dataReader.Read()) {
                    Group newGroup = new Group();
                    newGroup.Id = dataReader.GetInt32(0);
                    newGroup.Name = dataReader.GetString(1);
                    newGroup.Description = dataReader.GetString(2);
                    newGroup.Status = dataReader.GetBoolean(3);
                    newGroup.UserId = userId;
                    groups.Add(newGroup);
                }
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                connection.Close();
            }
            return groups;
        }
        
        public bool Update(Group group) {
            bool check = false;
            SqlConnection connection = Connection.createConnection();
            String query = "Update tblGroup Set name = @name , description = @description Where id = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@name", group.Name);
            cmd.Parameters.AddWithValue("@description", group.Description);
            try {
                connection.Open();
                if (cmd.ExecuteNonQuery() > 0) {
                    check = true;
                }
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                connection.Close();
            }
            return check;   
        }
        
        public bool Create(Group group) {
            bool check = false;
            SqlConnection connection = Connection.createConnection();
            String query = "Insert into tblGroup(name,description,status,userId) Values(@name,@description,@status,@userId)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@name", group.Name);
            cmd.Parameters.AddWithValue("@description", group.Description);
            cmd.Parameters.AddWithValue("@status", group.Status);
            cmd.Parameters.AddWithValue("@userId", group.UserId);
            try {
                connection.Open();
                if (cmd.ExecuteNonQuery() > 0) {
                    check = true;
                }
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                connection.Close();
            }
            return check;
        }

        public bool DeleteById(int groupID) {
            bool check = false;
            SqlConnection connection = Connection.createConnection();
            String query = "Update tblGroup Set status = 0 Where id = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", groupID);
            try {
                connection.Open();
                if (cmd.ExecuteNonQuery() > 0) {
                    check = true;
                }
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                connection.Close();
            }
            return check;
        }



    }
}
