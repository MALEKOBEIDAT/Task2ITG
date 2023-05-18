using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITG_Tsk2
{
    public partial class WebForm1 : System.Web.UI.Page
    {


        #region Properties

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["RowDeleted"] != null && (bool)Session["RowDeleted"])
            {
                // Display alert indicating row was deleted
                string script = "alert('Row deleted successfully.');";
                ClientScript.RegisterStartupScript(this.GetType(), "DeleteAlert", script, true);

                // Reset the flag in Session
                Session["RowDeleted"] = false;
            }

            if (!IsPostBack)
            {

                //GetUsers();
                UsersClass obj1 = new UsersClass();
                obj1.Get(Gv1);



            }



        }
        #endregion

        #region Methods
        public List<UsersClass> GetUsers()
        {
            List < UsersClass > liUsersClass=new List<UsersClass>();
            return liUsersClass;


        }
        #endregion

        #region Handler



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            UsersClass obj1 = new UsersClass();

            obj1.AddUsers(Gv1, txtAddName.Text, txtAddUserName.Text, Convert.ToInt32(DDG.SelectedValue), Convert.ToInt32(DDU.SelectedValue), DateTime.Parse(txtBirth.Text), 1);
            obj1.Get(Gv1);
            Response.Redirect("Users.aspx");

            // Register the JavaScript code to display the alert message
            string script = "alert('User added successfully!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UserAddedScript", script, true);
        }




      

        protected void Gv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            UsersClass obj1 = new UsersClass();
            int index = e.RowIndex;

            Label lblid = Gv1.Rows[index].FindControl("lblUID") as Label;
            obj1.User_id = Convert.ToInt32(lblid.Text);

            obj1.Delete();

            // Set a flag in Session to indicate a row has been deleted
            Session["RowDeleted"] = true;

            Response.Redirect("Users.aspx");
        }



        protected void Gv1_RowEditing1(object sender, GridViewEditEventArgs e)
        {
          
            UsersClass obj1 = new UsersClass();



            Gv1.EditIndex = e.NewEditIndex;
            obj1.Get(Gv1);


        }

        protected void Gv1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0 && index < Gv1.Rows.Count)
            {
                TextBox name = Gv1.Rows[index].FindControl("txtN") as TextBox;
                string username = ((TextBox)Gv1.Rows[index].FindControl("txtUserN")).Text;

                DropDownList ddlgender = (DropDownList)Gv1.Rows[index].FindControl("DDG");
                int genderid = Convert.ToInt32(ddlgender.SelectedValue);

                DropDownList ddlUserType = (DropDownList)Gv1.Rows[index].FindControl("DDUT");
                int usertypeid = Convert.ToInt32(ddlUserType.SelectedValue);

                string birthOfDate = ((TextBox)Gv1.Rows[index].FindControl("txtDateOf")).Text;

                DropDownList ddLanguge = (DropDownList)Gv1.Rows[index].FindControl("DDL");
                int languageid = Convert.ToInt32(ddLanguge.SelectedValue);

                TextBox txtDateOf = (TextBox)Gv1.Rows[index].FindControl("txtDateOf");
                DateTime date_of_birth;
                if (DateTime.TryParse(txtDateOf.Text, out date_of_birth))
                {
                    // Date parsing succeeded, you can use the "date_of_birth" variable
                }
                else
                {
                    // Date parsing failed, handle the error accordingly
                    throw new Exception("Date parsing failed ");
                }

                UsersClass obj = new UsersClass();

                if (Gv1.DataKeys[index] != null)
                {
                    int user_id = Convert.ToInt32(Gv1.DataKeys[index].Value); // Assuming the user_id is stored in the DataKeys collection of the grid view

                    obj.User_id = user_id;
                    obj.Name = name.Text;
                    obj.UserName = username;
                    obj.gender_id = genderid;
                    obj.usertype_id = usertypeid;
                    obj.date_of_birth = date_of_birth.Day;
                    obj.language_id = languageid;

                    obj.Edit(name.Text, username, genderid, usertypeid, date_of_birth, languageid);
                    Gv1.EditIndex = -1;
                    obj.Get(Gv1);

                    // Show an alert message after successful editing
                    string script = "alert('Row updated successfully.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateSuccess", script, true);
                }
                else
                {
                    // Handle the case where the DataKeys value is null or not found
                    throw new Exception("The index is Null");
                }
            }
            else
            {
                // Handle the case where the index is out of range
                throw new Exception("The index is out of range");
            }
        }



        protected void Gv1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {

            UsersClass obj = new UsersClass();

            Gv1.EditIndex = -1;
            obj.Get(Gv1);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            UsersClass obj1 = new UsersClass();

            obj1.Search(Gv1, txtSearch.Text);


        }
        protected void Gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != Gv1.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }


            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Normal))
            {
                LinkButton btnDelete = (LinkButton)e.Row.Cells[0].Controls[2]; 
                btnDelete.CssClass = "btn btn-danger";
                
            }



            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Normal))
            {
                LinkButton btnDelete = (LinkButton)e.Row.Cells[0].Controls[0]; 
                btnDelete.CssClass = "btn btn-warning";
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblRowCount = (Label)e.Row.FindControl("lblRowCount");
                if (lblRowCount != null)
                {
                    lblRowCount.Text = "Total Rows: " + Gv1.Rows.Count.ToString();
                }
            }


        }
        protected void Gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsersClass obj1 = new UsersClass();
            obj1.Get(Gv1);
            Gv1.PageIndex = e.NewPageIndex;
            Gv1.DataBind();

        }
        protected void Gv1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Gv1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = e.SortDirection;

            // Fetch the data from your data source (e.g., database)
            DataTable dataTable = GetDataTableFromDataSource();

            // Apply sorting
            DataView dataView = new DataView(dataTable);
            dataView.Sort = sortExpression + (sortDirection == SortDirection.Ascending ? " ASC" : " DESC");

            // Bind the sorted data back to the GridView
            Gv1.DataSource = dataView;
            Gv1.DataBind();
        }
        private DataTable GetDataTableFromDataSource()
        {
            // Retrieve data from your data source and return it as a DataTable
            // Replace this with your actual data retrieval logic
            var con = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;

            using (SqlConnection con1 = new SqlConnection(con))
            {
                con1.Open();

                using (SqlCommand cmd = new SqlCommand("GetUser", con1))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        protected void Gv1_RowCreated1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                LinkButton btnEdit = new LinkButton();
                btnEdit.Text = "Action";
                btnEdit.CssClass = "btn btn-primary";
                e.Row.Cells[0].Controls.Add(btnEdit);

                
            }
        }

        #endregion
    }


}
