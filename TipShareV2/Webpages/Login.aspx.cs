using System;
using System.Collections.Generic;
using System.Configuration;
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

            if (string.IsNullOrEmpty(email))
            {
                lblEmailError.Text = "Please enter your email";
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                lblPasswordError.Text = "Please enter your password ";
                return;
            }

            Response.Redirect("Home.aspx");

            //SqlConnection conn = null;
            //string connString = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
            //conn = new SqlConnection(connString);

            //try

            //{
            //    string query = "SELECT t.TweetMessage, t.TweetDate, '@' + u.ScreenName AS Tweeter 
            //    FROM [Tweet] t INNER JOIN [Users] u ON t.PostedBy = u.UserId"; " +
            //    "SqlCommand cmd = new SqlCommand(query, conn); conn.Open(); " +
            //    "SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); " +
            //    "gridTable.Load(dr); " +
            //}

            //catch (Exception ex)

            //{
            //    // handle error here Response.Write("Error occured" + ex.Message); 

            //}

            //finally

            //{
            //    conn.Close();
        }

            // make database call

            /* if ((email == "yes") && (password == "no")) */
            //{
                
           // }
            /* else
             {
                 lblLoginError.Text = "Please enter valid login credentials"; 
             }*/

       // }
    }
}