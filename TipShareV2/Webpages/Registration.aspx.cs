using System;
using System.Collections.Generic;
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

            lblConfirm.Text = "Your request has been submitted for approval. You will receive a confirmation email" +
                " once your request has been reviewed.";

        }


    }

}