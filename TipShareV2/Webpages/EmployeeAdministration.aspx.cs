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
        private SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
              
            SqlConnection conn = null;
            DataTable gridTable = new DataTable();

            Button addEmployee = btnAddEmployee;
            Button saveEdits = btnSaveEmployeeEdit;
            Button yes = btnSaveEmployeeYes;
            Button no = btnSaveEmployeeNo;

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                conn = new SqlConnection(connString);
                var query = "SELECT LastName, FirstName, EmployeeStatus FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                gridTable.Load(dr);

            }
            catch (Exception ex)
            {
                // handle error here
                Response.Write(" Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnSaveEmployeeEdit_Click(object sender, EventArgs e)
        {

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

    }
}