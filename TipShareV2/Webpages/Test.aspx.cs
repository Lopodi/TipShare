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
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //int grossSales = int.Parse(txtGrossSales.Text);
            //double tipAllocPercent = double.Parse(txtTipAllocPercent.Text);
            //double tipsEarned = double.Parse(txtTipsEarned.Text);
            //double tipPoolAlloc = grossSales * tipAllocPercent / 100;

            //lblTipPool.Text = tipPoolAlloc.ToString("C");

          
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmdEEID = new SqlCommand("SELECT EmployeeID FROM Employee " +
            "WHERE FirstName = ' AND LastName = 'Blo'", conn);

            try
            {
                conn.Open();
                int EmployeeID = (int)cmdEEID.ExecuteScalar();
                Response.Write(EmployeeID);

                //string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //conn = new SqlConnection(connString);
                
                //var query = String.Format("INSERT INTO [Gratuity] ([GrossSales], [TipsEarned]" +
                //    ", [TipPercentAllocated], [TipsAllocated], [EmployeeID], [PayPeriodID], [ShiftID], [DateCreated], " +
                //    "[UserID]) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, '{7}', {8})", 
                //    grossSales, tipsEarned, tipAllocPercent, tipPoolAlloc, 2, 1, 1, DateTime.Now, 1);
                //SqlCommand cmd = new SqlCommand(query, conn);
                //conn.Open();
                //cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // handle error here
                Response.Write(" Error: " +ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
    }
}