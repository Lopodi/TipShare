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

        protected void BtnSaveLunch_Click(object sender, EventArgs e)
        {
            double grossSales = double.Parse(txtLunchGrossSalesTest.Text);
            double tipAllocPercent = double.Parse(txtLunchTipAllocTest.Text);
            double tipsEarned = double.Parse(txtLunchGrossTipsTest.Text);
            double tipPoolAlloc = grossSales * tipAllocPercent / 100;
            string lunchTipPoolCalc = lblLunchTipPoolCalcTest.Text = tipPoolAlloc.ToString("C");

            SqlCommand cmd;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            string sql = "SELECT EmployeeID FROM Employee WHERE [NAME] = ddlLunchServer ";
            try
            {
                conn.Open(); cmd = new SqlCommand(sql, conn);
                Int32 value = Convert.ToInt32(cmd.ExecuteScalar()); cmd.Dispose();
                Response.Write("Returned TweetId: " + value.ToString());
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            NewMethod();

            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                var query = String.Format("INSERT INTO [Gratuity] ([GrossSales], [TipsEarned]" +
                    ", [TipPercentAllocated], [TipsAllocated], [EmployeeID], [PayPeriodID], [ShiftID], [DateCreated], " +
                    "[UserID]) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, '{7}', {8})",
                    grossSales, tipsEarned, tipAllocPercent, tipPoolAlloc, 2, 1, 1, DateTime.Now, 1);
                conn.Open();
                new SqlCommand(query, conn).ExecuteNonQuery();
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

        private static void NewMethod()
        {
            SqlConnection conn = null;
        }

        /* protected void txtLunchSupportHours_TextChanged(object sender, EventArgs e)
         {
             double lunchHoursWorked = double.Parse(txtLunchSupportHours.Text);
             double lunchTipCalc = double.Parse(lblLunchSupportTipCalc.Text);


             SqlConnection conn = null;

             try
             {
                 string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                 conn = new SqlConnection(connString);
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