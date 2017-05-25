using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TipShareV2.Webpages
{

    public partial class EmployeeAdministration : System.Web.UI.Page
    {

        public DataSourceOperation UpdateQuery { get; set; }

        private SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userIDCookie"] != null)
            {
                Response.Write(Session["userIDCookie"]);
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            sdsEmployee.InsertParameters["FirstName"].DefaultValue = 
                ((TextBox)gvEmployee.FooterRow.FindControl("txtFirstName")).Text;

            sdsEmployee.InsertParameters["LastName"].DefaultValue =
                ((TextBox)gvEmployee.FooterRow.FindControl("txtLastName")).Text;

            sdsEmployee.InsertParameters["EmployeeStatus"].DefaultValue =
                ((DropDownList)gvEmployee.FooterRow.FindControl("ddlEmployeeStatus")).SelectedValue;

            sdsEmployee.Insert();

        }
        

       // SqlConnection conn = null;
      //  DataTable employee = new DataTable();








        //private void btnAddEmployee_Click(object sender, EventArgs e)
        //{
        //    sdsEmployee.InsertParameters["LastName"].DefaultValue = GridView.FooterRow.FindControl.
        //}



        protected void gvEmployee_RowUpdating(object sender, EventArgs e)
        {
            SqlConnection conn = null;

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                conn = new SqlConnection(connString);
                var query = String.Format("UPDATE [Employee] ([LastName], [FirstName], [Status], " +
                    "[LastUpdatedDate], [LastUpdatedBy], [UserID]) VALUES({0}, '{1}', '{2}', '{3}', '{4}', {5})",
                    gvEmployee, gvEmployee, gvEmployee, DateTime.Now, "System", 
                    int.Parse(Session["userIDCookie"].ToString()));
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write(" Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["userIDCookie"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void gvEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}




        /* SqlConnection conn = null;

         try
         {
             string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
             conn = new SqlConnection(connString);
             var query = String.Format("UPDATE [Employee] ([LastName], [FirstName], [Status]) " +
                 "VALUES({0}, {1}, {2})");
             SqlCommand cmd = new SqlCommand(query, conn);
             conn.Open();
             cmd.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
             Response.Write(" Error: " + ex.Message);
         }
         finally
         {
             conn.Close();
         } */

    



    
