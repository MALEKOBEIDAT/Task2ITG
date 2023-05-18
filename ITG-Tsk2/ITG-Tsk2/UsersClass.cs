using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.UI;


namespace ITG_Tsk2
{
    public class UsersClass
    {
        public int User_id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int gender_id { get; set; }
        public int usertype_id { get; set; }
        public int date_of_birth { get; set; }
        public int language_id { get; set; }
        public string gender_name { get; set; }
        public string language_name { get; set; }
        public string usertype_name { get; set; }



        public void Login(string username, string password)
        {
            var con = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;

            using (SqlConnection con1 = new SqlConnection(con))
            {
                con1.Open();

                using (SqlCommand cmd = new SqlCommand("Login2"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Connection = con1;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            HttpContext.Current.Response.Redirect("Users.aspx");
                        }
                        else
                        {
                            // Incorrect credentials, redirect to another page or display an error message


                            Console.WriteLine("Invalid");

                            
                        }
                    }
                }
            }
        }
        public void Get(GridView Gv1 )
        {


            var con = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;

            using (SqlConnection con1 = new SqlConnection(con))
            {

                con1.Open();
                using (SqlCommand cmd = new SqlCommand("GetUser"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                      
                        cmd.Connection = con1;
                        da.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        
                            da.Fill(dt);
                            Gv1.DataSource = dt;

                            
                            Gv1.DataBind();


                        
                    }
                }

            }






        }
        public void Delete()
        {
            var con = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con1 = new SqlConnection(con))
            {
                con1.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteUser"))
                {
                    cmd.Connection = con1;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", User_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Edit( string name, string username, int genderid, int usertypeid, DateTime date_ofbirth, int languageid)
        {
            var con = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(con))

            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", User_id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@genderid", genderid);



                    command.Parameters.AddWithValue("@usertypeid", usertypeid);
                    command.Parameters.AddWithValue("@date_ofbirth", date_ofbirth);
                    command.Parameters.AddWithValue("@languageid", languageid);


                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddUsers(GridView Gv1, string name, string username, int genderid, int usertypeid, DateTime date_ofbirth, int languageid)
        {
            var con = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con1 = new SqlConnection(con))
            {
                con1.Open();
                // Check if the username already exists
                using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE username = @username", con1))
                {
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int usernameCount = (int)checkCmd.ExecuteScalar();

                    if (usernameCount > 0)
                    {
                        // Username already exists, handle the error or display a message
                        // For example, you can throw an exception or show an error message to the user
                        throw new Exception("Username already exists. Please choose a different username.");
                     
                    }
                }

                using (SqlCommand cmd = new SqlCommand("AddUser"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con1;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@genderid", genderid);
                    cmd.Parameters.AddWithValue("@usertypeid", usertypeid);
                    cmd.Parameters.AddWithValue("@date_ofbirth", date_ofbirth);
                    cmd.Parameters.AddWithValue("@languageid", languageid);

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            Gv1.DataSource = dt;
                            Gv1.DataBind();
                        }
                    }
                }
            }
        }
        public void Search(GridView Gv1, string name)
        {

            var con = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;

            using (SqlConnection con1 = new SqlConnection(con))
            {

                con1.Open();
                using (SqlCommand cmd = new SqlCommand("Searchs1"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", name);

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {

                        cmd.Connection = con1;
                        da.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            Gv1.DataSource = dt;
                            Gv1.DataBind();


                        }

                    }
                }

            }






        }

    }
}