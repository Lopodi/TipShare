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
    public partial class TipAllocation : System.Web.UI.Page
    {
        private SqlCommand cmd;

        public int ConnectionString { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //requires user to login. if userID not stored in session, will take them back to login page
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

        protected void btnShiftDate_Click(object sender, EventArgs e)
        {
            cldShiftDate.Visible = true;
        }

            protected void cldShiftDate_SelectionChanged(object sender, EventArgs e)
            {
                DateTime shiftDate = cldShiftDate.SelectedDate;
                btnShiftDate.Text = "Shift Date: " + shiftDate.ToShortDateString();
                gvLunchTipAlloc.Visible = true;

                //lblShiftDate.Text = cldShiftDate.SelectedDate.ToString();
                //lblShiftDate.Visible = true;
                //cldShiftDate.Visible = false; - uncomment if don't want calendar to show until selecting date
                       
                SqlConnection conn = null;
                DataTable gridTable = new DataTable();

                  try

                   {
                       string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                       conn = new SqlConnection(connString);
                    string query = ("SELECT Gratuity.GratuityID, Gratuity.EmployeeID, Gratuity.UserID, " +
                    "Gratuity.GrossSales, Gratuity.TipsEarned, Gratuity.TipsAllocated, " +
                    "Gratuity.TipPercentAllocated, Gratuity.TipPercentContributed, Gratuity.HoursWorked, " +
                    "Gratuity.Shift, Gratuity.ShiftDate, Gratuity.CreatedBy, Gratuity.LastUpdateDate, " +
                    "Gratuity.LastCreatedBy, Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, " +
                    "Gratuity.DateCreated FROM Gratuity INNER JOIN Employee " +
                    "ON Gratuity.EmployeeID = Employee.EmployeeID " +
                    "WHERE Gratuity.ShiftDate ='" + shiftDate.ToShortDateString() + "'"); 

                       SqlCommand cmd = new SqlCommand(query, conn);
                       conn.Open();
                       SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                       gridTable.Load(dr);
                   }

                   catch (Exception ex)

                   {
                       Response.Write("Error occured" + ex.Message);
                   }

                   finally
                   {
                       conn.Close();

                   }

                // bind the appropriate SQL results to your gridview control 

                gvLunchTipAlloc.DataSource = gridTable;
                gvLunchTipAlloc.DataBind();
               
               
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
            string lunchTipPoolTotal;

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
                //lunchTipPoolTotalCalc = lunchTipPoolCalc;
                //How to get the variable to dynamically update with new info?          


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
                    int.Parse(Session["userIDCookie"].ToString()), "System");
            
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();

                gvLunchTipAlloc.DataBind();
                

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

            //Sum tip pool and set up gridview??

            /*

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
                    int.Parse(Session["userIDCookie"].ToString()), "System");

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();

                gvLunchTipAlloc.DataBind();


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

            //}
            //catch (Exception)
            //{

            //    lblLunchServerError.Text = "All fields are required";
            //    return;

            //}

        }

        protected void btnAddNewLunchServer_Click(object sender, EventArgs e)
        {

          phNewLunchServer.Controls.Add(pnlLunchServerLine);   
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
                     "[EmployeeID], [PayPeriodID], [DateCreated] [UserID]) " +
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