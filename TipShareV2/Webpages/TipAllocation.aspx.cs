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
        private object gridTable;

        public int ConnectionString { get; private set; }

//Assign userID upon login -->

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

//Step 1 --> select shift date for which to enter gross sales and gratuities & display existings 
//results for that day -->

        protected void cldShiftDate_SelectionChanged(object sender, EventArgs e)
        {
            DateTime shiftDate = cldShiftDate.SelectedDate;
            btnShiftDate.Text = "Shift Date: " + shiftDate.ToShortDateString();
            gvLunchTipAlloc.Visible = true;

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
                "WHERE Gratuity.GrossSales IS NOT NULL AND " +
                "Gratuity.ShiftDate ='" + shiftDate.ToShortDateString() + "'");

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

//Step 1.1 --> select shift date for which to enter gross sales and gratuities & display existings 
//SUPPORT STAFF results for that day -->

            gvLunchSupportAlloc.Visible = true;

            SqlConnection connSupportStaff = null;
            DataTable gridTableSupportStaff = new DataTable();

            try

            {
                string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connSupportStaff = new SqlConnection(connString);
                string query = ("SELECT Gratuity.GratuityID, Gratuity.EmployeeID, Gratuity.UserID, " +
                "Gratuity.HoursWorked, Gratuity.TipsEarnedSupport," +
                "Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, " +
                "Gratuity.DateCreated FROM Gratuity INNER JOIN Employee " +
                "ON Gratuity.EmployeeID = Employee.EmployeeID " +
                "WHERE Gratuity.HoursWorked IS NOT NULL AND " +
                "Gratuity.ShiftDate ='" + shiftDate.ToShortDateString() + "'");

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                gridTableSupportStaff.Load(dr);
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

            gvLunchSupportAlloc.DataSource = gridTableSupportStaff;
            gvLunchSupportAlloc.DataBind();

//Sum tip pool and dipslay for selected date -->

            SqlCommand cmdTipPool;

            string lunchTipPoolTotal = lblLunchTipPoolTotalCalc.Text;

            string tipPoolTotal = ("SELECT SUM (TipsAllocated) FROM Gratuity " +
                "WHERE ShiftDate ='" + shiftDate.ToShortDateString() + "'");

            try
            {

                cmdTipPool = new SqlCommand(tipPoolTotal, conn);
                conn.Open();
                Int32 tipPool = Convert.ToInt32(cmdTipPool.ExecuteScalar());
                cmdTipPool.Dispose();
                lblLunchTipPoolTotalCalc.Text = tipPool.ToString("C");
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                conn.Close();

            }


        }

//Step 2 --> Enter new record in database for gross sales and gratuities earned on shift date -->

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

            //insert new row into Tipshare database

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
            }
            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                conn.Close();

            }


            DataTable gridTable = new DataTable();
            gvLunchTipAlloc.DataSource = gridTable;
            gvLunchTipAlloc.DataBind();

// continue to display gridview for the selected date, appended for new row -->

            try
            {

                string display = ("SELECT Gratuity.GratuityID, Gratuity.EmployeeID, Gratuity.UserID, " +
                    "Gratuity.GrossSales, Gratuity.TipsEarned, Gratuity.TipsAllocated, " +
                    "Gratuity.TipPercentAllocated, Gratuity.TipPercentContributed, Gratuity.HoursWorked, " +
                    "Gratuity.Shift, Gratuity.ShiftDate, Gratuity.CreatedBy, Gratuity.LastUpdateDate, " +
                    "Gratuity.LastCreatedBy, Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, " +
                    "Gratuity.DateCreated FROM Gratuity INNER JOIN Employee " +
                    "ON Gratuity.EmployeeID = Employee.EmployeeID " +
                    "WHERE Gratuity.GrossSales IS NOT NULL AND " +
                    "Gratuity.ShiftDate ='" + shiftDate.ToShortDateString() + "'");

                SqlCommand cmdDisplay = new SqlCommand(display, conn);
                conn.Open();
                SqlDataReader dr = cmdDisplay.ExecuteReader(CommandBehavior.CloseConnection);
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

//Sum tip pool and set up gridview -->

            SqlCommand cmdTipPool;

            lunchTipPoolTotal = lblLunchTipPoolTotalCalc.Text;

            string tipPoolTotal = ("SELECT SUM (TipsAllocated) FROM Gratuity " +
                "WHERE ShiftDate ='" + shiftDate.ToShortDateString() + "'");

            try
            {
                cmdTipPool = new SqlCommand(tipPoolTotal, conn);
                conn.Open();
                Int32 tipPool = Convert.ToInt32(cmdTipPool.ExecuteScalar());
                cmdTipPool.Dispose();
                lblLunchTipPoolTotalCalc.Text = tipPool.ToString("C");
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                conn.Close();

            }

        }

//Step 2 --> assign hours to support staff

        protected void SaveLunchHours(object sender, EventArgs e)
        {
            //double.TryParse --> will try to convert to a number, but if it fails, it would do something
            //else (define)

            //defining variables outside of try block, so they can be viewed and called elsewhere 
            //in this method

            DateTime shiftDate;
            string employee;
            double hours;

            try
            {
                //above, we are defining the variables. here, we are initializing them

                shiftDate = cldShiftDate.SelectedDate;
                employee = ddlLunchSupport.Text;
                hours = double.Parse(txtLunchSupportHours.Text);

                if (string.IsNullOrEmpty(employee))

                //check for null or empty employee only on if statement b/c it is only variable above
                //that does not try to perform an action (convert, calculate). Cannot take action on a 
                //null field and will "automatically" error out 

                {
                   //CHANGE TO LUNCH SUPPORT ERROR! lblLunchServerError.Text = "All fields are required";
                    return;
                }

            }
            catch (Exception)
            {

                //CHANGE TO LUNCH SUPPORT ERROR! lblLunchServerError.Text = "All fields are required";
                return;

            }

            //CHANGE TO LUNCH SUPPORT ERROR!lblLunchServerError.Text = "";

//insert new row into Tipshare database for hours worked by support staff -->

            SqlConnection conn = null;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);

            try
            {

                var query = String.Format("INSERT INTO [Gratuity] ([EmployeeID], [Shift], " +
                    "[ShiftDate], [HoursWorked], [DateCreated], [UserID], [CreatedBy]) " +
                    "VALUES({0}, '{1}', '{2}', {3}, '{4}', {5}, '{6}')",
                    ddlLunchSupport.SelectedValue, "Lunch", cldShiftDate.SelectedDate, hours, DateTime.Now,
                    int.Parse(Session["userIDCookie"].ToString()), "System");

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                conn.Close();

            }

            DataTable gridTableSupportStaff = new DataTable();
            gvLunchSupportAlloc.DataSource = gridTableSupportStaff;
            gvLunchSupportAlloc.DataBind();

// display support gridview after saved -->

            SqlConnection connSupportStaff = null;

            try

            {
                string connStringSupport = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connSupportStaff = new SqlConnection(connStringSupport);
                string query = ("SELECT Gratuity.GratuityID, Gratuity.EmployeeID, Gratuity.UserID, " +
                "Gratuity.HoursWorked, Gratuity.TipsEarnedSupport," +
                "Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, " +
                "Gratuity.DateCreated FROM Gratuity INNER JOIN Employee " +
                "ON Gratuity.EmployeeID = Employee.EmployeeID " +
                "WHERE Gratuity.HoursWorked IS NOT NULL AND " +
                "Gratuity.ShiftDate ='" + shiftDate.ToShortDateString() + "'");

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                lblLunchSupportTipAlloc.Text = Convert.ToString(cmd.ExecuteReader(CommandBehavior.CloseConnection));
                //gridTableSupportStaff.Load(dr);
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

          //  gvLunchSupportAlloc.DataSource = gridTableSupportStaff;
          //  gvLunchSupportAlloc.DataBind();


            //    lblLunchServerError.Text = "All fields are required";
            //    return;

            //}

        }

        protected void btnAllocateTips_Click(object sender, EventArgs e)
        {

            //Allocate tips to support staff -->
            DateTime shiftDate;
            shiftDate = cldShiftDate.SelectedDate;

            DataTable gridTableSupportStaff = new DataTable();
            gvLunchSupportAlloc.DataSource = gridTableSupportStaff;
            gvLunchSupportAlloc.DataBind();

            SqlConnection conn = null;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);

            try
            {

                string display = ("SELECT MAX(e.FirstName) +' ' + MAX(e.LastName) AS EmployeeName," +
                    "SUM (HoursWorked) HoursWorked, " +
                    "CONVERT(DECIMAL(18, 2), (SUM(Hoursworked) / (SELECT SUM(HoursWorked) " +
                    "   FROM Gratuity WHERE ShiftDate = '" + shiftDate.ToShortDateString() + "') " +
                    "   * (SELECT SUM(TipsAllocated) FROM Gratuity " +
                    "   WHERE ShiftDate = '" + shiftDate.ToShortDateString() + "')))" +
                    "AS TipsEarnedSupport" +
                    "FROM Gratuity g" +
                    "   INNER JOIN Employee e" +
                    "   ON g.EmployeeID = e.EmployeeID" +
                    "WHERE HoursWorked IS NOT NULL AND " +
                    "ShiftDate = '" + shiftDate.ToShortDateString() + "'" +
                    "GROUP BY e.EmployeeID");

                SqlCommand cmd = new SqlCommand(display, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                gridTableSupportStaff.Load(dr);
                Response.Write(dr);

                // SqlCommand cmd = new SqlCommand(query, conn);
                //conn.Open();
                //cmd.ExecuteReader();
            }
            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                conn.Close();

            }
            gvLunchSupportAlloc.DataSource = gridTableSupportStaff;
            gvLunchSupportAlloc.DataBind();

 //Continue to display support staff after tip allocation processed -->

            gvLunchSupportAlloc.Visible = true;


            //DateTime shiftDate = cldShiftDate.SelectedDate;
            SqlConnection connSupportStaff = null;
            //DataTable gridTableSupportStaff = new DataTable();

            try

            {
                connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connSupportStaff = new SqlConnection(connString);
                string query = ("SELECT Gratuity.GratuityID, Gratuity.EmployeeID, Gratuity.UserID, " +
                "Gratuity.HoursWorked, Gratuity.TipsEarnedSupport," +
                "Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, " +
                "Gratuity.DateCreated FROM Gratuity INNER JOIN Employee " +
                "ON Gratuity.EmployeeID = Employee.EmployeeID " +
                "WHERE Gratuity.HoursWorked IS NOT NULL AND " +
                "Gratuity.ShiftDate ='" + shiftDate.ToShortDateString() + "'");

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                gridTableSupportStaff.Load(dr);
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

            gvLunchSupportAlloc.DataSource = gridTableSupportStaff;
            gvLunchSupportAlloc.DataBind();


        }
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



    

    
