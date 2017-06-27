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

        public int ConnectionString { get; private set; }

        // set sql connection to be used throughout application -->

        SqlConnection connTipAlloc = null;

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
            gvLunchSupportAlloc.Visible = false;
            gvLunchSupportTipsEarned.Visible = true;

            DataTable dtLunchServers = new DataTable();
            string connStringServers = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connStringServers);

            //stored procedure
            SqlCommand cmdTipAllocationServer = new SqlCommand("spServerTipsAllocation", connTipAlloc);
            cmdTipAllocationServer.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftDateBeginServerParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdTipAllocationServer.Parameters.Add(shiftDateBeginServerParameter);

            SqlParameter shiftDateEndServerParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdTipAllocationServer.Parameters.Add(shiftDateEndServerParameter);

            SqlParameter shiftServerParameter = new SqlParameter("@Shift", "Lunch");
            cmdTipAllocationServer.Parameters.Add(shiftServerParameter);

            try

            {
                connTipAlloc.Open();
                SqlDataReader drServerTipsAllocation = cmdTipAllocationServer.ExecuteReader(CommandBehavior.CloseConnection);
                dtLunchServers.Load(drServerTipsAllocation);
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }

            // bind the appropriate SQL results to your gridview control -->

            gvLunchTipAlloc.DataSource = dtLunchServers;
            gvLunchTipAlloc.DataBind();

            //Step 1.1 --> select shift date for which to enter gross sales and gratuities & display existings 
            //SUPPORT STAFF results for that day -->

            gvLunchSupportAlloc.Visible = true;

            //SqlConnection connTipsAllocated = null;
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (connTipAlloc = new SqlConnection(connString))
            {

                DataTable gridTableSupportStaff = new DataTable();

                //stored procedure
                SqlCommand cmdTipAllocation = new SqlCommand("spSupportTipsAllocation", connTipAlloc);
                cmdTipAllocation.CommandType = CommandType.StoredProcedure;

                SqlParameter shiftDateBeginParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
                cmdTipAllocation.Parameters.Add(shiftDateBeginParameter);

                SqlParameter shiftDateEndParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
                cmdTipAllocation.Parameters.Add(shiftDateEndParameter);

                SqlParameter shiftParameter = new SqlParameter("@Shift", "Lunch");
                cmdTipAllocation.Parameters.Add(shiftParameter);

                try

                {

                    connTipAlloc.Open();
                    SqlDataReader drTipsAlloc = cmdTipAllocation.ExecuteReader(CommandBehavior.CloseConnection);
                    gridTableSupportStaff.Load(drTipsAlloc);

                }

                catch (Exception ex)

                {
                    Response.Write("Error occured" + ex.Message);
                }

                finally
                {
                    connTipAlloc.Close();

                }

                //bind appropriate SQL results to gridview control -->

                gvLunchSupportTipsEarned.DataSource = gridTableSupportStaff;
                gvLunchSupportTipsEarned.DataBind();
                gvLunchSupportAlloc.Visible = false;
                gvLunchSupportTipsEarned.Visible = true;

            }
            //Sum tip pool and dipslay for selected date -->

            SqlCommand cmdTipPool;
            connTipAlloc = new SqlConnection(connString);

            string lunchTipPoolTotal = lblLunchTipPoolTotalCalc.Text;

            string tipPoolTotal = ("SELECT SUM (TipsAllocated) FROM Gratuity " +
                "WHERE ShiftDate ='" + shiftDate.ToShortDateString() + "'");

            try
            {

                cmdTipPool = new SqlCommand(tipPoolTotal, connTipAlloc);
                connTipAlloc.Open();
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
                connTipAlloc.Close();

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
            double grossSales, tipAllocPercent, tipsEarned, tipPoolAlloc;
            string lunchTipPoolCalc, lunchTipPoolTotal;

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

           // SqlConnection conn = null;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connString);

            try
            {
                //stored procedure -->
                SqlCommand cmdInsertServerRecord = new SqlCommand("spInsertServerRecord", connTipAlloc);
                cmdInsertServerRecord.CommandType = CommandType.StoredProcedure;

                SqlParameter GrossSalesParameter = new SqlParameter("@GrossSales", grossSales);
                cmdInsertServerRecord.Parameters.Add(GrossSalesParameter);

                SqlParameter TipsEarnedParameter = new SqlParameter("@TipsEarned", tipsEarned);
                cmdInsertServerRecord.Parameters.Add(TipsEarnedParameter);

                SqlParameter TipsAllocPercentParamter = new SqlParameter("@TipsAllocPercent", tipAllocPercent);
                cmdInsertServerRecord.Parameters.Add(TipsAllocPercentParamter);

                SqlParameter TipPoolAllocParameter = new SqlParameter("@TipPoolAlloc", tipPoolAlloc);
                cmdInsertServerRecord.Parameters.Add(TipPoolAllocParameter);

                SqlParameter EmployeeParamter = new SqlParameter("@Employee", ddlLunchServer.SelectedValue);
                cmdInsertServerRecord.Parameters.Add(EmployeeParamter);

                SqlParameter ShiftParameter = new SqlParameter("@Shift", "Lunch");
                cmdInsertServerRecord.Parameters.Add(ShiftParameter);

                SqlParameter ShiftDateParameter = new SqlParameter("@ShiftDate", cldShiftDate.SelectedDate);
                cmdInsertServerRecord.Parameters.Add(ShiftDateParameter);

                SqlParameter DateCreatedParameter = new SqlParameter("@DateCreated", DateTime.Now);
                cmdInsertServerRecord.Parameters.Add(DateCreatedParameter);

                SqlParameter UserCreatedParameter = new SqlParameter
                    ("@UserID", int.Parse(Session["userIDCookie"].ToString()));
                cmdInsertServerRecord.Parameters.Add(UserCreatedParameter);

                SqlParameter CreatedByParameter = new SqlParameter ("@CreatedBy", "System");
                cmdInsertServerRecord.Parameters.Add(CreatedByParameter);

                //var query = String.Format("INSERT INTO [Gratuity] ([GrossSales], [TipsEarned]," +
                //    "[TipPercentAllocated], [TipsAllocated], [EmployeeID], [Shift], " +
                //    "[ShiftDate], [DateCreated], [UserID], [CreatedBy]) " +
                //    "VALUES({0}, {1}, {2}, {3}, {4}, '{5}', '{6}', '{7}', {8}, '{9}')",
                //    grossSales, tipsEarned, tipAllocPercent, tipPoolAlloc, ddlLunchServer.SelectedValue,
                //    "Lunch", cldShiftDate.SelectedDate, DateTime.Now,
                //    int.Parse(Session["userIDCookie"].ToString()), "System");

                //SqlCommand cmd = new SqlCommand(query, connTipAlloc);
                connTipAlloc.Open();
                cmdInsertServerRecord.ExecuteNonQuery();
            }
            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }


            DataTable gridTable = new DataTable();
            gvLunchTipAlloc.DataSource = gridTable;
            gvLunchTipAlloc.DataBind();

            // continue to display gridview for the selected date, appended for new row -->

            DataTable dtLunchServers = new DataTable();
            string connStringServers = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connStringServers);

            //stored procedure -->
            SqlCommand cmdTipAllocationServer = new SqlCommand("spServerTipsAllocation", connTipAlloc);
            cmdTipAllocationServer.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftDateBeginServerParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdTipAllocationServer.Parameters.Add(shiftDateBeginServerParameter);

            SqlParameter shiftDateEndServerParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdTipAllocationServer.Parameters.Add(shiftDateEndServerParameter);

            SqlParameter shiftServerParameter = new SqlParameter("@Shift", "Lunch");
            cmdTipAllocationServer.Parameters.Add(shiftServerParameter);

            try

            {
                connTipAlloc.Open();
                SqlDataReader drServerTipsAllocation = cmdTipAllocationServer.ExecuteReader(CommandBehavior.CloseConnection);
                dtLunchServers.Load(drServerTipsAllocation);
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }

            // bind the appropriate SQL results to your gridview control -->

            gvLunchTipAlloc.DataSource = dtLunchServers;
            gvLunchTipAlloc.DataBind();

            //Sum tip pool and set up gridview -->

            SqlCommand cmdTipPool;

            lunchTipPoolTotal = lblLunchTipPoolTotalCalc.Text;

            string tipPoolTotal = ("SELECT SUM (TipsAllocated) FROM Gratuity " +
                "WHERE ShiftDate ='" + shiftDate.ToShortDateString() + "'");

            try
            {
                cmdTipPool = new SqlCommand(tipPoolTotal, connTipAlloc);
                connTipAlloc.Open();
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
                connTipAlloc.Close();

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

            //SqlConnection conn = null;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connString);

            var query = String.Format("INSERT INTO [Gratuity] ([EmployeeID], [Shift], " +
                    "[ShiftDate], [HoursWorked], [DateCreated], [UserID], [CreatedBy]) " +
                    "VALUES({0}, '{1}', '{2}', {3}, '{4}', {5}, '{6}')",
                    ddlLunchSupport.SelectedValue, "Lunch", cldShiftDate.SelectedDate, hours, DateTime.Now,
                    int.Parse(Session["userIDCookie"].ToString()), "System");

            using (SqlCommand cmd = new SqlCommand(query, connTipAlloc))
            {

                try
                {

                    connTipAlloc.Open();
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)

                {
                    Response.Write("Error occured" + ex.Message);
                }

                finally
                {
                    connTipAlloc.Close();

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
                    string displayServers= ("SELECT Gratuity.GratuityID, Gratuity.EmployeeID, Gratuity.UserID, " +
                    "Gratuity.HoursWorked, Gratuity.TipsEarnedSupport," +
                    "Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, " +
                    "Gratuity.DateCreated FROM Gratuity INNER JOIN Employee " +
                    "ON Gratuity.EmployeeID = Employee.EmployeeID " +
                    "WHERE Gratuity.HoursWorked IS NOT NULL AND " +
                    "Gratuity.ShiftDate ='" + shiftDate.ToShortDateString() + "'");

                    SqlCommand cmdServers = new SqlCommand(displayServers, connTipAlloc);
                    connTipAlloc.Open();
                    lblLunchSupportTipAlloc.Text = Convert.ToString(cmd.ExecuteReader(CommandBehavior.CloseConnection));
                    SqlDataReader drHours = cmdServers.ExecuteReader(CommandBehavior.CloseConnection);
                    gridTableSupportStaff.Load(drHours);
                }

                catch (Exception ex)

                {
                    Response.Write("Error occured" + ex.Message);
                }

                finally
                {
                    connTipAlloc.Close();

                }

                gvLunchSupportAlloc.DataSource = gridTableSupportStaff;
                gvLunchSupportAlloc.DataBind();
                gvLunchSupportAlloc.Visible = true;
                gvLunchSupportTipsEarned.Visible = false;
            }
        }

        protected void btnAllocateTips_Click(object sender, EventArgs e)

        {
            // define variables
            DateTime shiftDate;

            //initialize variables
            shiftDate = cldShiftDate.SelectedDate;

            //SqlConnection connTipsAllocated = null;
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (connTipAlloc = new SqlConnection(connString))
            {

                DataTable gridTableSupportStaff = new DataTable();

                //stored procedure
                SqlCommand cmdTipAllocation = new SqlCommand("spSupportTipsAllocation", connTipAlloc);
                cmdTipAllocation.CommandType = CommandType.StoredProcedure;

                SqlParameter shiftDateBeginParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
                cmdTipAllocation.Parameters.Add(shiftDateBeginParameter);

                SqlParameter shiftDateEndParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
                cmdTipAllocation.Parameters.Add(shiftDateEndParameter);

                SqlParameter shiftParameter = new SqlParameter("@Shift", "Lunch");
                cmdTipAllocation.Parameters.Add(shiftParameter);

                try
                {
                    connTipAlloc.Open();
                    SqlDataReader drHours = cmdTipAllocation.ExecuteReader(CommandBehavior.CloseConnection);
                    gridTableSupportStaff.Load(drHours);
                }
                // above, data is pulling in the rows and columns as identifed in the sqldatareader and loading comand in "try"

                catch (Exception ex)

                {
                    Response.Write("Error occured" + ex.Message);
                }

                finally
                {
                    connTipAlloc.Close();

                }

                gvLunchSupportTipsEarned.DataSource = gridTableSupportStaff;
                gvLunchSupportTipsEarned.DataBind();
                gvLunchSupportAlloc.Visible = false;
                gvLunchSupportTipsEarned.Visible = true;
            }

          
        }
    }
        
}




      



    

    
