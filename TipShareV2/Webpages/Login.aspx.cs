using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TipShareV2
{
    public partial class Login : System.Web.UI.Page
    {
        public int ConnectionString { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEnterEmail.Text;
            string password = txtEnterPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblLoginError.Text = "All fields are required";
                return;
            }
          
            SqlConnection conn = null;
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);

            try

            {
                //  alternate code --> string query = 
                // "SELECT UserID FROM [User] WHERE Email='" + email + "' AND UserPassword ='" + password +"'";

                var query = String.Format("SELECT UserID FROM [User] WHERE Email = '{0}' AND " +
                    "UserPassword = '{1}'", email, password); 

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                Int32 userID = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();

                if (userID < 1)
                {
                    lblLoginError.Text = "Please enter valid login credentials";
                    return;
                }
                else
                // store database value in session cookie

                Session["userIDCookie"] = userID.ToString();

             //  if (Session["userIDCookie"] == null)
             //  {
             //      lblLoginError.Text = "Please enter valid login credentials";
             //       return;
             //   }

                // redirect to landing page
                // put redirect in try block incase anything fails, it will not redirect, but 74 on will
                // be executed & connection closed
                          
                 Response.Redirect("TipAllocation.aspx");
                
            }

            catch (Exception ex)

            {
             Response.Write("Error occured" + ex.Message);
             lblLoginError.Text = "Please enter valid login credentials";

            }

            finally

            {
                conn.Close();
            }

           
        }
    }
}