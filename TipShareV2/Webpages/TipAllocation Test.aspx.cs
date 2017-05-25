using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TipShareV2.Webpages
{
    public partial class TipAllocationTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShiftDate_Click(object sender, EventArgs e)
        {
            cldShiftDate.Visible = true;
        }

        protected void cldShiftDate_SelectionChanged(object sender, EventArgs e)
        {
            DateTime shiftDate = cldShiftDate.SelectedDate;
            btnShiftDate.Text = "Shift Date: " + shiftDate.ToShortDateString();

            //lblShiftDate.Text = cldShiftDate.SelectedDate.ToString();
            //lblShiftDate.Visible = true;
            cldShiftDate.Visible = false;
        }


        protected void btnSaveLunch_Click(object sender, EventArgs e)
        {
            //double.TryParse --> will try to convert to a number, but if it fails, it would do something
            //else (define)

            //defining variables outside of try block, so they can be viewed and called elsewhere 
            //in this method

            DateTime shiftDate;
            string employee;
            double grossSales;
            double tipAllocPercent;
            double tipsEarned;
            double tipPoolAlloc;
            string lunchTipPoolCalc;

            try
            {
                //above, we are defining the variables. here, we are initializing them

                shiftDate = cldShiftDate.SelectedDate;
                employee = ddlLunchServer.Text;
                grossSales = double.Parse(txtLunchGrossSales.Text);
                tipAllocPercent = double.Parse(txtLunchTipAlloc.Text);
                tipsEarned = double.Parse(txtLunchGrossTips.Text);
                tipPoolAlloc = grossSales * tipAllocPercent / 100;

                lunchTipPoolCalc = lblLunchTipPoolCalc.Text = tipPoolAlloc.ToString("C");

                if (string.IsNullOrEmpty(employee))

                //check for null or empty employee only on if statement b/c it is only variable above
                //that does not try to perform an action (convert, calculate). Cannot take action on a 
                //null field and will "automatically" error out 

                {
                    lblLunchServerError.Text = "All fields are required";
                    return;
                }

            }
            catch (Exception)
            {

                lblLunchServerError.Text = "All fields are required";
                return;

            }



            lblLunchServerError.Text = "";



            SqlConnection conn = null;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);

            try
            {

                var query = String.Format("INSERT INTO [Gratuity] ([GrossSales], [TipsEarned]," +
                    "[TipPercentAllocated], [TipsAllocated], [EmployeeID], [Shift], " +
                    "[ShiftDate], [DateCreated], [UserID], [CreatedBy]) " +
                    "VALUES({0}, {1}, {2}, {3}, {4}, '{5}', '{6}', '{7}', {8}, '{9}')",
                    grossSales, tipsEarned, tipAllocPercent, tipPoolAlloc, ddlLunchServer.SelectedValue,
                    "Lunch", cldShiftDate.SelectedDate, DateTime.Now,
                    int.Parse(Session["userIDCookie"].ToString()), "Test Page");

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

        }

        protected void btnAddNewLunchServer_Click(object sender, EventArgs e)
        {
            // int newServerLine = 0;
            // newServerLine += 1;
            // Response.Write(newServerLine);
            //problem is that it never loops back through? Need a foreach?

            DropDownList ddlNewLunchServer = new DropDownList();
            TextBox txtNewLunchGrossSales = new TextBox();
            TextBox txtNewGrossTipsEarned = new TextBox();
            TextBox txtNewTipAllocPercent = new TextBox();
            TextBox txtNewTipPoolCalc = new TextBox();
            Button btnNewSaveLunch = new Button();

            pnlLunchServer.Controls.Add(ddlNewLunchServer);
            pnlLunchServer.Controls.Add(txtNewLunchGrossSales);



            //, txtNewLunchGrossSales);, txtNewGrossTipsEarned, txtNewTipAllocPercent, txtNewTipPoolCalc, btnNewSaveLunch);
        }


    }    
    
}