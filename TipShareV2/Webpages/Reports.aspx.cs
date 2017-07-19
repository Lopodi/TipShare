using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TipShareV2.Webpages
{
    public partial class Reports : System.Web.UI.Page
    
    {
        public int ConnectionString { get; private set; }

// set sql connection to be used throughout application -->

        SqlConnection connTipAlloc = null;

        protected void Page_Load(object sender, EventArgs e)
        {
//requires user to login. if userID not stored in session, will take them back to login page -->

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

        protected void btnRunReport_Click (object sender, EventArgs e)
        {
            DateTime dtBeginDate = cldBeginDate.SelectedDate;
            DateTime dtEndDate = cldEndDate.SelectedDate;
            DataTable dtNetEarnings = new DataTable();

            string connStringServers = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connTipAlloc = new SqlConnection(connStringServers);

            SqlCommand cmdNetEarnings = new SqlCommand("spPayPeriodReport", connTipAlloc);
            cmdNetEarnings.CommandType = CommandType.StoredProcedure;

            SqlParameter paramBeginDate = new SqlParameter("@ShiftDateBegin", dtBeginDate.ToShortDateString());
            cmdNetEarnings.Parameters.Add(paramBeginDate);

            SqlParameter paramEndDate = new SqlParameter("@ShiftDateEnd", dtEndDate.ToShortDateString());
            cmdNetEarnings.Parameters.Add(paramEndDate);

// retreive data -->

            try

            {
                connTipAlloc.Open();
                SqlDataReader drNetEarnings = cmdNetEarnings.ExecuteReader(CommandBehavior.CloseConnection);
                dtNetEarnings.Load(drNetEarnings);
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

            gvTaxableTips.DataSource = dtNetEarnings;
            gvTaxableTips.DataBind();
        }
    }
}