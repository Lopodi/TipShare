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
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string Email = txtEmail.Text;
            string ConfirmEmail = txtConfirmEmail.Text;
            string Password = txtPassword.Text;
            string ConfirmPassword = txtConfirmPassword.Text;
            string FirstName = txtFirstName.Text;
            string LastName = txtLastName.Text;

            if (string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(ConfirmEmail) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(ConfirmPassword) ||
                string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName))
            {
                lblNullError.Text = "All fields are required";
                return;
            }
            lblNullError.Text = "";

            if (Email != ConfirmEmail)
            {
                lblEmailMismatchError.Text = "The email accounts you entered do not match";
                return;
            }
            lblEmailMismatchError.Text = "";

            if (!Email.Contains("@") || !Email.Contains("."))
            {
                lblEmailInvalidError.Text = "Please enter a valid email address";
                return;
            }
            lblEmailInvalidError.Text = "";

            if (Password.Length < 6)
            {
                lblPasswordLengthError.Text = "Passwords must contain as least 6 characters";
                return;
            }
            lblPasswordLengthError.Text = "";

            if (Password != ConfirmPassword)
            {
                lblPasswordMismatchError.Text = "The password does not match the confirm password";
                return;
            }
            lblPasswordMismatchError.Text = "";

            SqlConnection conn = null;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);

            try
            {

                var query = String.Format("INSERT INTO [User] ([FirstName], [LastName]," +
                    "[Email], [UserPassword], [UserStatus], [StatusDate], [CreatedBy], [DateCreated]) " +
                    "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    FirstName, LastName, Email, Password, "Pending Approval", DateTime.Now, "System", DateTime.Now);

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();

                pnlRegister.Visible = false;
                lblConfirm.Text = "Success! Your request has been submitted for approval. " +
                    "The system administrator will contact you " +
                    "once your request has been reviewed.";

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


    }

}