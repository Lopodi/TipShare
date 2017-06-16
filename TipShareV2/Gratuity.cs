using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TipShareV2
{
    public class Gratuity

    {
        private DateTime cldShiftDate;

        protected void cldShiftDate_SelectionChanged(object sender, EventArgs e)
        {

            DateTime shiftDate = cldShiftDate;
            SqlConnection conn = null;

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

        }
}