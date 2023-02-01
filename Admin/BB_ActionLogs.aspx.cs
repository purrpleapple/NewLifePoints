using LifePoints.Admin.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifePoints.Admin
{
    public partial class BB_ActionLogs : System.Web.UI.Page
    {
        Database_Connections db = new Database_Connections();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Convert.ToBoolean(Session["LOGIN"]))
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                LifePoints.Database.account bb = Session["ACCOUNT"] as LifePoints.Database.account;
                //Set Username
                username.InnerText = bb.ACC_EMAIL;

                PopulateActionLogsGridView();
            }
        }

        //Displays data to GridView named ActionLogs
        public void PopulateActionLogsGridView()
        {
            DataTable data = db.GetBloodBankLogsTableData();
            if(data != null )
            {
                NoDataMsg.Attributes.Add("display", "none");
                TableContainer.Attributes.Add("display", "");
                ActionLogs.DataSource = null;
                ActionLogs.DataSource = data;
                ActionLogs.DataBind();
            }
            else
            {
                NoDataMsg.Attributes.Add("display", "");
                TableContainer.Attributes.Add("display", "none");
            }
        }

        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {

            Session.Clear();
            Session.RemoveAll();
            Server.Transfer("~/Default.aspx");
        }
    }
}