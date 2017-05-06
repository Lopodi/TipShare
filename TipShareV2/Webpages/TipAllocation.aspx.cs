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
    public partial class TipAllocation : System.Web.UI.Page
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
            lblShiftDate.Text = cldShiftDate.SelectedDate.ToString();
            lblShiftDate.Visible = true;
            cldShiftDate.Visible = false;

        }
        //Once calendar data changed or selected, change labele next to button to equal calendar date and 
        // change cldShiftDate.Visible = false;



        protected void btnSaveLunch_Click(object sender, EventArgs e)
        {
            string employee = ddlLunchServer.Text;
            double grossSales = double.Parse(txtLunchGrossSales.Text);
            double tipAllocPercent = double.Parse(txtLunchTipAlloc.Text);
            double tipsEarned = double.Parse(txtLunchGrossTips.Text);
            double tipPoolAlloc = grossSales * tipAllocPercent / 100;

            string lunchTipPoolCalc = lblLunchTipPoolCalc.Text = tipPoolAlloc.ToString("C");

            SqlConnection conn = null;

                string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                conn = new SqlConnection(connString);

                try
                {
               
                var query = String.Format("INSERT INTO [Gratuity] ([GrossSales], [TipsEarned]" +
                    ", [TipPercentAllocated], [TipsAllocated], [EmployeeID], [PayPeriodID], [ShiftID], " +
                    "[DateCreated], [UserID]) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, '{7}', {8})",
                    grossSales, tipsEarned, tipAllocPercent, tipPoolAlloc, ddlLunchServer.SelectedValue, 
                    1, 1, DateTime.Now, 1);
            
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

     

        /* protected void txtLunchSupportHours_TextChanged(object sender, EventArgs e)
         {
             double lunchHoursWorked = double.Parse(txtLunchSupportHours.Text);
             double lunchTipCalc = double.Parse(lblLunchSupportTipCalc.Text);


             SqlConnection conn = null;
             string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
             conn = new SqlConnection(connString);

             try
             {                 
                 var query = String.Format("INSERT INTO [Gratuity] ([HoursWorked], [TipsAllocated}" +
                     "[EmployeeID], [PayPeriodID], [ShiftID], [DateCreated] [UserID]) " +
                     "VALUES({0}, {1}, {2}, {3}, {4}, {5}', {6})",
                     lunchHoursWorked, lunchTipCalc, 3, 1, 1, DateTime.Now, 1);
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
     */



    }
}