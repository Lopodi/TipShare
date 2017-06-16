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
    public partial class UserAdministration : System.Web.UI.Page
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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["userIDCookie"] = null;
            Response.Redirect("Login.aspx");
        }

        /*
        protected void gvUserAdmin_RowUpdating(object sender, EventArgs e)
        {

            SqlConnection conn = null;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);

            try
            {
                var query = String.Format("UPDATE [User] ([UserStatus], [LastUpdateDate], [LastUpdatedBy]) " +
                    "VALUES('{0}', {1}, {2})",
                ((DropDownList)gvUserAdmin.FindControl("ddlUserStatus")).SelectedValue, DateTime.Now,
                int.Parse(Session["userIDCookie"].ToString()));

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();

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

    




            //UPDATE[User]
            //SET UserStatus = 'Pending Approval'
            //WHERE UserID = 3;
            //  }
        }
        */
    }
}