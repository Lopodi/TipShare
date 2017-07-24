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

        //Display existing lunch & dinner server & staff info --> 

            gvLunchTipAlloc.Visible = true;
            gvLunchTipAllocSave.Visible = true;
            gvLunchSupportAlloc.Visible = true;
            gvLunchSupportTipsEarned.Visible = true;

            gvDinnerTipAlloc.Visible = true;
            gvDinnerSupportAlloc.Visible = true;
         // add another gv for dinner support to display allocations?? gvDinnerSupportTipsEarned.Visible = true;

            DataTable dtLunchServers = new DataTable();
            DataTable dtDinnerServers = new DataTable();
            DataTable dtLunchSupportStaff = new DataTable();
            DataTable dtDinnerSupportStaff = new DataTable();

            string connStringServers = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connStringServers);

         //stored procedure for lunch servers -->

            SqlCommand cmdLunchTipAllocationServer = new SqlCommand("spServerTipsAllocation", connTipAlloc);
            cmdLunchTipAllocationServer.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftLunchDateBeginServerParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdLunchTipAllocationServer.Parameters.Add(shiftLunchDateBeginServerParameter);

            SqlParameter shiftLunchDateEndServerParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdLunchTipAllocationServer.Parameters.Add(shiftLunchDateEndServerParameter);

            SqlParameter shiftLunchServerParameter = new SqlParameter("@Shift", "Lunch");
            cmdLunchTipAllocationServer.Parameters.Add(shiftLunchServerParameter);

         //stored procedure for dinner servers -->

            SqlCommand cmdDinnerTipAllocationServer = new SqlCommand("spServerTipsAllocation", connTipAlloc);
            cmdDinnerTipAllocationServer.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftDinnerDateBeginServerParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdDinnerTipAllocationServer.Parameters.Add(shiftDinnerDateBeginServerParameter);

            SqlParameter shiftDinnerDateEndServerParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdDinnerTipAllocationServer.Parameters.Add(shiftDinnerDateEndServerParameter);

            SqlParameter shiftDinnerServerParameter = new SqlParameter("@Shift", "Dinner");
            cmdDinnerTipAllocationServer.Parameters.Add(shiftDinnerServerParameter);

        // stored procedure for lunch support staff -->

            SqlCommand cmdLunchSupportTipAllocation = new SqlCommand("spSupportTipsAllocation", connTipAlloc);
            cmdLunchSupportTipAllocation.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftLunchDateBeginSupportParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdLunchSupportTipAllocation.Parameters.Add(shiftLunchDateBeginSupportParameter);

            SqlParameter shiftLunchDateEndSupportParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdLunchSupportTipAllocation.Parameters.Add(shiftLunchDateEndSupportParameter);

            SqlParameter shiftLunchSupportParameter = new SqlParameter("@Shift", "Lunch");
            cmdLunchSupportTipAllocation.Parameters.Add(shiftLunchSupportParameter);

         // stored procedure for dinner support staff -->

            SqlCommand cmdDinnerSupportTipAllocation = new SqlCommand("spSupportTipsAllocation", connTipAlloc);
            cmdDinnerSupportTipAllocation.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftDinnerDateBeginSupportParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdDinnerSupportTipAllocation.Parameters.Add(shiftDinnerDateBeginSupportParameter);

            SqlParameter shiftDinnerDateEndSupportParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdDinnerSupportTipAllocation.Parameters.Add(shiftDinnerDateEndSupportParameter);

            SqlParameter shiftDinnerSupportParameter = new SqlParameter("@Shift", "Dinner");
            cmdDinnerSupportTipAllocation.Parameters.Add(shiftDinnerSupportParameter);

 // retrieve lunch server data -->
            try

            {
                connTipAlloc.Open();
                SqlDataReader drLunchServerTipsAllocation = cmdLunchTipAllocationServer.ExecuteReader(CommandBehavior.CloseConnection);
                dtLunchServers.Load(drLunchServerTipsAllocation);

            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }
        
// bind the appropriate SQL results to your gridview control lunch server -->

            gvLunchTipAlloc.DataSource = dtLunchServers;
            gvLunchTipAlloc.DataBind();

// retrieve dinner server data -->

            try

            {
                connTipAlloc.Open();
                SqlDataReader drDinnerServerTipsAllocation = cmdDinnerTipAllocationServer.ExecuteReader(CommandBehavior.CloseConnection);
                dtDinnerServers.Load(drDinnerServerTipsAllocation);
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }

            gvDinnerTipAlloc.DataSource = dtDinnerServers;
            gvDinnerTipAlloc.DataBind();

// retrieve lunch support data -->

            try

            {
                connTipAlloc.Open();
                SqlDataReader drLunchSupportTipsAllocation = cmdLunchSupportTipAllocation.ExecuteReader(CommandBehavior.CloseConnection);
                dtLunchSupportStaff.Load(drLunchSupportTipsAllocation);
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }

              gvLunchSupportTipsEarned.DataSource = dtLunchSupportStaff;
              gvLunchSupportTipsEarned.DataBind();

            // retrieve dinner support data -->

            try

            {
                connTipAlloc.Open();
                SqlDataReader drDinnerSupportTipsAllocation = cmdDinnerSupportTipAllocation.ExecuteReader(CommandBehavior.CloseConnection);
                dtDinnerSupportStaff.Load(drDinnerSupportTipsAllocation);
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }

            gvDinnerSupportTipsEarned.DataSource = dtDinnerSupportStaff;
            gvDinnerSupportTipsEarned.DataBind();


            //Sum Lunch tip pool and display for selected date -->

            SqlCommand cmdLunchTipPool;

            string lunchTipPoolTotal = lblLunchTipPoolTotalCalc.Text;

            string lunchTipPoolQuery = ("SELECT SUM (TipsAllocated) FROM Gratuity " +
                "WHERE Shift = '"+ "Lunch" +"' AND ShiftDate ='" + shiftDate.ToShortDateString() + "'");

            try
            {
                cmdLunchTipPool = new SqlCommand(lunchTipPoolQuery, connTipAlloc);
                connTipAlloc.Open();
                Int32 tipPool = Convert.ToInt32(cmdLunchTipPool.ExecuteScalar());
                cmdLunchTipPool.Dispose();
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

         //Sum Dinner tip pool and dipslay for selected date -->

            SqlCommand cmdDinnerTipPool;

            string dinnerTipPoolTotal = lblDinnerTipPool.Text;

            string dinnerTipPoolQuery = ("SELECT SUM (TipsAllocated) FROM Gratuity " +
                "WHERE Shift = '" + "Dinner" + "' AND ShiftDate ='" + shiftDate.ToShortDateString() + "'");

            try
            {

                cmdDinnerTipPool = new SqlCommand(dinnerTipPoolQuery, connTipAlloc);
                connTipAlloc.Open();
                Int32 tipPool = Convert.ToInt32(cmdDinnerTipPool.ExecuteScalar());
                cmdDinnerTipPool.Dispose();
                lblDinnerTipPoolSum.Text = tipPool.ToString("C");
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

 //Step 2 --> Enter new record in database for gross sales and gratuities earned on a lunch shift date -->

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

//insert new row into Tipshare database for lunch shift -->

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
            gvLunchTipAllocSave.DataSource = gridTable;
            gvLunchTipAllocSave.DataBind();

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
                "WHERE Shift = '" + "Lunch" + "' AND ShiftDate ='" + shiftDate.ToShortDateString() + "'");

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

 //insert new row into Tipshare database for hours worked by lunch support staff -->

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connString);

            var query = String.Format("INSERT INTO [Gratuity] ([EmployeeID], [Shift], " +
                    "[ShiftDate], [HoursWorked], [DateCreated], [UserID], [CreatedBy]) " +
                    "VALUES({0}, '{1}', '{2}', {3}, '{4}', {5}, '{6}')",
                    ddlLunchSupport.SelectedValue, "Lunch", cldShiftDate.SelectedDate, hours, DateTime.Now,
                    int.Parse(Session["userIDCookie"].ToString()), "System");

            SqlCommand cmdInsertHours = new SqlCommand(query, connTipAlloc);

            try
            {
                connTipAlloc.Open();
                cmdInsertHours.ExecuteNonQuery();
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

            DataTable dtLunchSupportStaff = new DataTable();
            string connStringSupport = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connStringSupport);

            SqlCommand cmdLunchSupportTipAllocation = new SqlCommand("spSupportTipsAllocation", connTipAlloc);
            cmdLunchSupportTipAllocation.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftLunchDateBeginSupportParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdLunchSupportTipAllocation.Parameters.Add(shiftLunchDateBeginSupportParameter);

            SqlParameter shiftLunchDateEndSupportParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdLunchSupportTipAllocation.Parameters.Add(shiftLunchDateEndSupportParameter);

            SqlParameter shiftLunchSupportParameter = new SqlParameter("@Shift", "Lunch");
            cmdLunchSupportTipAllocation.Parameters.Add(shiftLunchSupportParameter);

            try

            {
                connTipAlloc.Open();
                SqlDataReader drHours = cmdLunchSupportTipAllocation.ExecuteReader(CommandBehavior.CloseConnection);
                dtLunchSupportStaff.Load(drHours);          
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }

            gvLunchSupportTipsEarned.DataSource = dtLunchSupportStaff;
            gvLunchSupportTipsEarned.DataBind();
            gvLunchSupportTipsEarned.Visible = true;
            gvLunchSupportAlloc.Visible = false;
        }

// btnAllocateLunchTips is needed in case a server is added after tips have already been allocated. 
//It will recalculate tip allocation for addition of new server -->

        protected void btnAllocateLunchTips_Click(object sender, EventArgs e)
        {
            // define variables
            DateTime shiftDate;

            //initialize variables
            shiftDate = cldShiftDate.SelectedDate;

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
        //Insert dinner server into database -->

        protected void btnSaveDinner_Click(object sender, EventArgs e)
        {
            //double.TryParse --> will try to convert to a number, but if it fails, it would do something
            //else (define)

            //defining variables outside of try block, so they can be viewed and called elsewhere 
            //in this method

            DateTime shiftDate;
            string employee;
            double grossSales, tipAllocPercent, tipsEarned, tipPoolAlloc;
            string dinnerTipPoolCalc, dinnerTipPoolTotal;

            try
            {
                //above, we are defining the variables. here, we are initializing them

                shiftDate = cldShiftDate.SelectedDate;
                employee = ddlDinnerServer.Text;
                grossSales = double.Parse(txtDinnerGrossSales.Text);
                tipAllocPercent = double.Parse(txtDinnerTipAllocPercent.Text);
                tipsEarned = double.Parse(txtDinnerTipsEarned.Text);
                tipPoolAlloc = grossSales * tipAllocPercent / 100;

                dinnerTipPoolCalc = lblDinnerTipPoolSum.Text = tipPoolAlloc.ToString("C");


                if (string.IsNullOrEmpty(employee))

                //check for null or empty employee only on if statement b/c it is only variable above
                //that does not try to perform an action (convert, calculate). Cannot take action on a 
                //null field and will "automatically" error out 

                {
                    lblDinnerServerError.Text = "All fields are required";
                    return;
                }

            }
            catch (Exception)
            {

                lblDinnerServerError.Text = "All fields are required";
                return;

            }

            lblDinnerServerError.Text = "";

//insert new row into Tipshare database for dinner shift -->

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connString);
            DataTable gtDinnerTipAlloc = new DataTable();

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

                SqlParameter EmployeeParamter = new SqlParameter("@Employee", ddlDinnerServer.SelectedValue);
                cmdInsertServerRecord.Parameters.Add(EmployeeParamter);

                SqlParameter ShiftParameter = new SqlParameter("@Shift", "Dinner");
                cmdInsertServerRecord.Parameters.Add(ShiftParameter);

                SqlParameter ShiftDateParameter = new SqlParameter("@ShiftDate", cldShiftDate.SelectedDate);
                cmdInsertServerRecord.Parameters.Add(ShiftDateParameter);

                SqlParameter DateCreatedParameter = new SqlParameter("@DateCreated", DateTime.Now);
                cmdInsertServerRecord.Parameters.Add(DateCreatedParameter);

                SqlParameter UserCreatedParameter = new SqlParameter
                    ("@UserID", int.Parse(Session["userIDCookie"].ToString()));
                cmdInsertServerRecord.Parameters.Add(UserCreatedParameter);

                SqlParameter CreatedByParameter = new SqlParameter("@CreatedBy", "System");
                cmdInsertServerRecord.Parameters.Add(CreatedByParameter);

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

            gvDinnerTipAlloc.DataSource = gtDinnerTipAlloc;
            gvDinnerTipAlloc.DataBind();

// continue to display dinner gridview for the selected date, appended for new row -->

            DataTable dtDinnerServers = new DataTable();
            string connStringServers = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connStringServers);

            //stored procedure -->
            SqlCommand cmdTipAllocationServer = new SqlCommand("spServerTipsAllocation", connTipAlloc);
            cmdTipAllocationServer.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftDateBeginServerParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdTipAllocationServer.Parameters.Add(shiftDateBeginServerParameter);

            SqlParameter shiftDateEndServerParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdTipAllocationServer.Parameters.Add(shiftDateEndServerParameter);

            SqlParameter shiftServerParameter = new SqlParameter("@Shift", "Dinner");
            cmdTipAllocationServer.Parameters.Add(shiftServerParameter);

            try

            {
                connTipAlloc.Open();
                SqlDataReader drServerTipsAllocation = cmdTipAllocationServer.ExecuteReader(CommandBehavior.CloseConnection);
                dtDinnerServers.Load(drServerTipsAllocation);
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }

            // bind the appropriate SQL results to the gridview control -->

            gvDinnerTipAlloc.DataSource = dtDinnerServers;
            gvDinnerTipAlloc.DataBind();

            //Sum tip pool and set up gridview -->

            SqlCommand cmdTipPool;

            dinnerTipPoolTotal = lblDinnerTipPoolSum.Text;

            string dinnertipPoolTotal = ("SELECT SUM (TipsAllocated) FROM Gratuity " +
                "WHERE Shift = '" + "Dinner" + "' AND ShiftDate ='" + shiftDate.ToShortDateString() + "'");

            try
            {
                cmdTipPool = new SqlCommand(dinnertipPoolTotal, connTipAlloc);
                connTipAlloc.Open();
                Int32 tipPool = Convert.ToInt32(cmdTipPool.ExecuteScalar());
                cmdTipPool.Dispose();
                lblDinnerTipPoolSum.Text = tipPool.ToString("C");
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

//Step 2 --> assign hours to dinner support staff

        protected void btnSaveDinnerHours_Click (object sender, EventArgs e)
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
                employee = ddlDinnerSupportStaff.Text;
                hours = double.Parse(txtDinnerHoursWorked.Text);

                if (string.IsNullOrEmpty(employee))

                //check for null or empty employee only on if statement b/c it is only variable above
                //that does not try to perform an action (convert, calculate). Cannot take action on a 
                //null field and will "automatically" error out 

                {
                    //CHANGE TO DINNER SUPPORT ERROR! lblLunchServerError.Text = "All fields are required";
                    return;
                }

            }
            catch (Exception)
            {

                //CHANGE TO DINNER SUPPORT ERROR! lblLunchServerError.Text = "All fields are required";
                return;

            }

            //CHANGE TO DINNER SUPPORT ERROR!lblLunchServerError.Text = "";

            //insert new row into Tipshare database for hours worked by dinner support staff -->

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connString);

            var query = String.Format("INSERT INTO [Gratuity] ([EmployeeID], [Shift], " +
                    "[ShiftDate], [HoursWorked], [DateCreated], [UserID], [CreatedBy]) " +
                    "VALUES({0}, '{1}', '{2}', {3}, '{4}', {5}, '{6}')",
                    ddlDinnerSupportStaff.SelectedValue, "Dinner", cldShiftDate.SelectedDate, hours, DateTime.Now,
                    int.Parse(Session["userIDCookie"].ToString()), "System");

            SqlCommand cmdInsertHours = new SqlCommand(query, connTipAlloc);

            try
            {
                connTipAlloc.Open();
                cmdInsertHours.ExecuteNonQuery();
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
            gvDinnerSupportAlloc.DataSource = gridTableSupportStaff;
            gvDinnerSupportAlloc.DataBind();

            // display support gridview after saved -->

            DataTable dtDinnerSupportStaff = new DataTable();
            string connStringSupport = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connStringSupport);

            SqlCommand cmdDinnerSupportTipAllocation = new SqlCommand("spSupportTipsAllocation", connTipAlloc);
            cmdDinnerSupportTipAllocation.CommandType = CommandType.StoredProcedure;

            SqlParameter shiftDinnerDateBeginSupportParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
            cmdDinnerSupportTipAllocation.Parameters.Add(shiftDinnerDateBeginSupportParameter);

            SqlParameter shiftDinnerDateEndSupportParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
            cmdDinnerSupportTipAllocation.Parameters.Add(shiftDinnerDateEndSupportParameter);

            SqlParameter shiftDinnerSupportParameter = new SqlParameter("@Shift", "Dinner");
            cmdDinnerSupportTipAllocation.Parameters.Add(shiftDinnerSupportParameter);

            try

            {
                connTipAlloc.Open();
                SqlDataReader drHours = cmdDinnerSupportTipAllocation.ExecuteReader(CommandBehavior.CloseConnection);
                dtDinnerSupportStaff.Load(drHours);
            }

            catch (Exception ex)

            {
                Response.Write("Error occured" + ex.Message);
            }

            finally
            {
                connTipAlloc.Close();

            }

            gvDinnerSupportTipsEarned.DataSource = dtDinnerSupportStaff;
            gvDinnerSupportTipsEarned.DataBind();
            gvDinnerSupportTipsEarned.Visible = true;
            gvDinnerSupportAlloc.Visible = false;
        }

        // btnAllocateLunchTips is needed in case a server is added after tips have already been allocated. 
        //It will recalculate tip allocation for addition of new server -->

        protected void btnDinnerAllocTips_Click (object sender, EventArgs e)
        {
            // define variables
            DateTime shiftDate;

            //initialize variables
            shiftDate = cldShiftDate.SelectedDate;

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

                SqlParameter shiftParameter = new SqlParameter("@Shift", "Dinner");
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

                gvDinnerSupportTipsEarned.DataSource = gridTableSupportStaff;
                gvDinnerSupportTipsEarned.DataBind();
                gvDinnerSupportAlloc.Visible = false;
                gvDinnerSupportTipsEarned.Visible = true;

            }

        }

//Editing lunch servers for a particular day -->

        protected void gvLunchTipAlloc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the edit index
            gvLunchTipAlloc.EditIndex = e.NewEditIndex;
            // Bind Data to gridview
            gvLunchTipAlloc.DataBind();
        }

        protected void gvLunchTipAlloc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Reset the edit index
            gvLunchTipAlloc.EditIndex = -1;
            // Bind Data to gridview
            gvLunchTipAlloc.DataBind();
        }

        protected void gvLunchTipAlloc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            //Retrieve the table from the session object.
            DataTable dtLunchTipAllocUpdate = (DataTable)Session["LunchTipAllocUpdate"];

            DataTable dtLunchServers = new DataTable();
            SqlCommand cmdLunchTipAllocationServer = new SqlCommand();

            //Update the values.
            GridViewRow row = gvLunchTipAlloc.Rows[e.RowIndex];
            dtLunchTipAllocUpdate.Rows[row.DataItemIndex]["GrossSales"] = ((TextBox)(row.Cells[1].Controls[0])).Text;
            dtLunchTipAllocUpdate.Rows[row.DataItemIndex]["TipsEarned"] = ((TextBox)(row.Cells[2].Controls[0])).Text;
            dtLunchTipAllocUpdate.Rows[row.DataItemIndex]["TipPercentAllocated"] = ((TextBox)(row.Cells[3].Controls[0])).Text;

            //Reset the edit index.
            gvLunchTipAlloc.EditIndex = -1;

            //Bind data to the GridView control.
            gvLunchTipAlloc.DataBind();     
        }

//Delete lunch servers from a particular day -->
        protected void gvLunchTipAlloc_DeleteRow(object sender, GridViewDeleteEventArgs e)
        {
            var GratuityID = gvLunchTipAlloc.DataKeys[e.RowIndex].Value;
            DateTime shiftDate = cldShiftDate.SelectedDate;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (connTipAlloc = new SqlConnection(connString))
            {
                string sqlDelete = "Delete from Gratuity where GratuityID = @GratuityID";
                using (SqlCommand cmdDelete = new SqlCommand(sqlDelete, connTipAlloc))
                {
                    cmdDelete.Parameters.AddWithValue("@GratuityID", GratuityID);
                    connTipAlloc.Open();
                    cmdDelete.ExecuteNonQuery();
                    connTipAlloc.Close();
                }

                // continue to display gridview for the selected date, appended for deleted row -->

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
            }

        }

//Editing lunch support for a particular day -->

        protected void gvLunchSupportTipAlloc_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void gvLunchSupportTipAlloc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        protected void gvLunchSupportTipAlloc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        //Delete lunch support from a particular day -->
        protected void gvLunchSupportTipAlloc_DeleteRow(object sender, GridViewDeleteEventArgs e)
        {
            var GratuityID = gvLunchSupportAlloc.DataKeys[e.RowIndex].Value;
            DateTime shiftDate = cldShiftDate.SelectedDate;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (connTipAlloc = new SqlConnection(connString))
            {
                string sqlDelete = "Delete from Gratuity where GratuityID = @GratuityID";
                using (SqlCommand cmdDelete = new SqlCommand(sqlDelete, connTipAlloc))
                {
                    cmdDelete.Parameters.AddWithValue("@GratuityID", GratuityID);
                    connTipAlloc.Open();
                    cmdDelete.ExecuteNonQuery();
                    connTipAlloc.Close();
                }

                // continue to display gridview for the selected date, appended for deleted row -->
                string connStringServers = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connTipAlloc = new SqlConnection(connStringServers);

                //stored procedure -->
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
                gvLunchSupportAlloc.Visible = true;
                //gvLunchSupportTipsEarned.Visible = true;

            }
        }
        //Edit dinner servers for a particular day -->

        protected void gvDinnerTipAlloc_RowEditing(object sender, GridViewEditEventArgs e)
        { }

        protected void gvDinnerTipAlloc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        { }

        protected void gvDinnerTipAlloc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        { }


        //Delete dinner servers from a particular day -->
        protected void gvDinnerTipAlloc_DeleteRow(object sender, GridViewDeleteEventArgs e)
        {
            var GratuityID = gvDinnerTipAlloc.DataKeys[e.RowIndex].Value;
            DateTime shiftDate = cldShiftDate.SelectedDate;

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (connTipAlloc = new SqlConnection(connString))
            {
                string sqlDelete = "Delete from Gratuity where GratuityID = @GratuityID";
                using (SqlCommand cmdDelete = new SqlCommand(sqlDelete, connTipAlloc))
                {
                    cmdDelete.Parameters.AddWithValue("@GratuityID", GratuityID);
                    connTipAlloc.Open();
                    cmdDelete.ExecuteNonQuery();
                    connTipAlloc.Close();
                }

                // continue to display gridview for the selected date, appended for new row -->

                DataTable dtDinnerServers = new DataTable();
                string connStringServers = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connTipAlloc = new SqlConnection(connStringServers);

                //stored procedure -->
                SqlCommand cmdTipAllocationServer = new SqlCommand("spServerTipsAllocation", connTipAlloc);
                cmdTipAllocationServer.CommandType = CommandType.StoredProcedure;

                SqlParameter shiftDateBeginServerParameter = new SqlParameter("@ShiftDateBegin", shiftDate.ToShortDateString());
                cmdTipAllocationServer.Parameters.Add(shiftDateBeginServerParameter);

                SqlParameter shiftDateEndServerParameter = new SqlParameter("@ShiftDateEnd", shiftDate.ToShortDateString());
                cmdTipAllocationServer.Parameters.Add(shiftDateEndServerParameter);

                SqlParameter shiftServerParameter = new SqlParameter("@Shift", "Dinner");
                cmdTipAllocationServer.Parameters.Add(shiftServerParameter);

                try

                {
                    connTipAlloc.Open();
                    SqlDataReader drServerTipsAllocation = cmdTipAllocationServer.ExecuteReader(CommandBehavior.CloseConnection);
                    dtDinnerServers.Load(drServerTipsAllocation);
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
                gvDinnerTipAlloc.DataSource = dtDinnerServers;
                gvDinnerTipAlloc.DataBind();
            }

        }


    }
}
        





      



    

    
