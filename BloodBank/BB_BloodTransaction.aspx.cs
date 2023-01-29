using BloodBank.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifePoints
{
	public partial class BB_BloodTransaction : System.Web.UI.Page
	{
        private Database_Connections db = new Database_Connections();
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!Convert.ToBoolean(Session["LOGIN"]))
            {
                Response.Redirect("~/Default.aspx");
            }

            if(!Page.IsPostBack)
            {
                LifePoints.Database.account bb = Session["ACCOUNT"] as LifePoints.Database.account;
                //Set Username
                username.InnerText = bb.ACC_EMAIL;
                PopulateDropDown();
                PopulateRequestBloodGrid();
                GetUnreadNotif();
            }
		}

        protected void NotificationNavList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewNotif")
            {
                string id = e.CommandArgument.ToString();
                Session["IsViewing"] = true;
                Session["NTF_ID"] = id;
                Response.Redirect("~/BloodBank/BB_Notification.aspx");
            }
        }


        private void PopulateRequestBloodGrid()
        {
            string stat = RequestStatus.SelectedValue;
            string query = string.Format(@"select BREQ_ID, BREQ_UACC_ID, BREQ_JSON_SURVEY_FORM, BREQ_REQ_STATUS, BREQ_DATE, BREQ_VISIT_DATE,
                                            if(BREQ_SURVEY_STATUS = false && BREQ_REQ_STATUS = true, 'PENDING', 
                                            if(BREQ_SURVEY_STATUS = true && BREQ_REQ_STATUS = true, 'APPROVED', 
                                            if(BREQ_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BREQ_SURVEY_STATUS,
                                            if(BREQ_BLOOD_STATUS = false && BREQ_REQ_STATUS = true, 'PENDING', 
                                            if(BREQ_BLOOD_STATUS = true && BREQ_REQ_STATUS = true, 'APPROVED', 
                                            if(BREQ_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BREQ_BLOOD_STATUS
                                             from blood_request");
            switch(stat)
            {
                case "0":
                    query += " order by BREQ_DATE desc;";
                    break;
                case "1":
                    query += " where (BREQ_SURVEY_STATUS=false and BREQ_REQ_STATUS=true) or (BREQ_BLOOD_STATUS=false and BREQ_REQ_STATUS=true) order by BREQ_DATE desc;";
                    break;
                case "2":
                    query += " where BREQ_BLOOD_STATUS=true and BREQ_REQ_STATUS=true order by BREQ_DATE desc;";
                    break;
                case "3":
                    query += " where BREQ_REQ_STATUS=false order by BREQ_DATE desc;";
                    break;
            }


            DataTable dt = db.GetBloodTransactionTableData(query);
            if (dt != null)
            {
                GridUserBloodRequest.DataSource = null;
                GridUserBloodRequest.DataSource = dt;
                GridUserBloodRequest.DataBind();
            }
        }

        private void PopulateDonationBloodGrid()
        {
            string stat = RequestStatus.SelectedValue;
            string query = string.Format(@"select BD_ID, BD_UACC_ID, BD_JSON_SURVEY_FORM, BD_REQ_STATUS, BD_DATE, BD_VISIT_DATE,
                                            if(BD_SURVEY_STATUS = false && BD_REQ_STATUS = true, 'PENDING', 
                                            if(BD_SURVEY_STATUS = true && BD_REQ_STATUS = true, 'APPROVED', 
                                            if(BD_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BD_SURVEY_STATUS,
                                            if(BD_BLOOD_STATUS = false && BD_REQ_STATUS = true, 'PENDING', 
                                            if(BD_BLOOD_STATUS = true && BD_REQ_STATUS = true, 'APPROVED', 
                                            if(BD_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BD_BLOOD_STATUS
                                             from blood_donation");
            switch (stat)
            {
                case "0":
                    query += " order by BD_DATE desc;";
                    break;
                case "1":
                    query += " where (BD_SURVEY_STATUS=false and BD_REQ_STATUS=true) or (BD_BLOOD_STATUS=false and BD_REQ_STATUS=true) order by BD_DATE desc;";
                    break;
                case "2":
                    query += " where BD_BLOOD_STATUS=true and BD_REQ_STATUS=true order by BD_DATE desc;";
                    break;
                case "3":
                    query += " where BD_REQ_STATUS=false order by BD_DATE desc;";
                    break;
            }


            DataTable dt = db.GetuserBloodDonation(query);
            if (dt != null)
            {
                GridUserBloodDonation.DataSource = null;
                GridUserBloodDonation.DataSource = dt;
                GridUserBloodDonation.DataBind();
            }
        }

        protected void GridUserBloodRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridUserBloodRequest.SelectedRow;
            string id = row.Cells[0].Text;

            LifePoints.Database.blood_request br = db.SearchBloodRequest(id);
            if (br != null)
            {
                Session["BloodRequest"] = br;
                Response.Redirect("~/BloodBank/BB_Request_Survey.aspx");
            }
        }

        public void PopulateDropDown()
        {
            RequestStatus.Items.Clear();

            RequestStatus.Items.Insert(0, new ListItem("All", "0"));
            RequestStatus.Items.Insert(1, new ListItem("Pending", "1"));
            RequestStatus.Items.Insert(2, new ListItem("Approved", "2"));
            RequestStatus.Items.Insert(3, new ListItem("Rejected", "3"));

            TableView.Items.Insert(0, new ListItem("Blood Requests", "0"));
            TableView.Items.Insert(1, new ListItem("Blood Donation", "1"));
        }

        protected void RequestStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateRequestBloodGrid();
        }

        protected void SearchBloodRequest_Click(object sender, EventArgs e)
        {
            TableView_SelectedIndexChanged(sender, e);
            string query = "";
            DataTable dt = new DataTable();
            int x = TableView.SelectedIndex;
            switch(x)
            {
                case 0:
                    query = string.Format(@"select BREQ_ID, BREQ_UACC_ID, BREQ_JSON_SURVEY_FORM, BREQ_REQ_STATUS, BREQ_DATE,BREQ_VISIT_DATE,
                                            if(BREQ_SURVEY_STATUS = false && BREQ_REQ_STATUS = true, 'PENDING', 
                                            if(BREQ_SURVEY_STATUS = true && BREQ_REQ_STATUS = true, 'APPROVED', 
                                            if(BREQ_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BREQ_SURVEY_STATUS,
                                            if(BREQ_BLOOD_STATUS = false && BREQ_REQ_STATUS = true, 'PENDING', 
                                            if(BREQ_BLOOD_STATUS = true && BREQ_REQ_STATUS = true, 'APPROVED', 
                                            if(BREQ_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BREQ_BLOOD_STATUS
                                             from blood_request where BREQ_UACC_ID={0};", SearchRequest.Text);
                    dt = db.GetBloodTransactionTableData(query);
                    if (dt != null)
                    {
                        GridUserBloodRequest.DataSource = null;
                        GridUserBloodRequest.DataSource = dt;
                        GridUserBloodRequest.DataBind();
                        SearchRequest.Text = "";
                    }
                    else
                    {
                        Response.Write(string.Format("<script>alert('No Blood Request Transaction was Found on Record with User ID : {0}')</script>", SearchRequest.Text));
                    }
                    break;
                case 1:
                    query = string.Format(@"select BD_ID, BD_UACC_ID, BD_JSON_SURVEY_FORM, BD_REQ_STATUS, BD_DATE,BD_VISIT_DATE,
                                            if(BD_SURVEY_STATUS = false && BD_REQ_STATUS = true, 'PENDING', 
                                            if(BD_SURVEY_STATUS = true && BD_REQ_STATUS = true, 'APPROVED', 
                                            if(BD_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BD_SURVEY_STATUS,
                                            if(BD_BLOOD_STATUS = false && BD_REQ_STATUS = true, 'PENDING', 
                                            if(BD_BLOOD_STATUS = true && BD_REQ_STATUS = true, 'APPROVED', 
                                            if(BD_REQ_STATUS = false, 'REJECTED', 'REJECTED'))) as BD_BLOOD_STATUS
                                             from blood_donation where BD_UACC_ID={0};", SearchRequest.Text);
                    dt = db.GetuserBloodDonation(query);
                    if (dt != null)
                    {
                        GridUserBloodDonation.DataSource = null;
                        GridUserBloodDonation.DataSource = dt;
                        GridUserBloodDonation.DataBind();
                        SearchRequest.Text = "";
                    }
                    else
                    {
                        Response.Write(string.Format("<script>alert('No Blood Donation Transaction was Found on Record with User ID : {0}')</script>", SearchRequest.Text));
                    }
                    break;
            }

            
        }

        protected void TableView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int table = TableView.SelectedIndex;
            switch (table)
            {
                case 0:
                    GridUserBloodRequest.Style.Add("display", "");
                    GridUserBloodDonation.Style.Add("display", "none");
                    HeadingText.InnerText = "Blood Request Transactions";
                    PopulateRequestBloodGrid();
                    break;
                case 1:
                    GridUserBloodRequest.Style.Add("display", "none");
                    GridUserBloodDonation.Style.Add("display", "");
                    HeadingText.InnerText = "Blood Donation Transactions";
                    PopulateDonationBloodGrid();
                    break;
            }
        }

        protected void GridUserBloodDonation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridUserBloodDonation.SelectedRow;
            string id = row.Cells[0].Text;

            blood_donation bd = db.SearchBloodDonation(id);
            if (bd != null)
            {

                Session["BloodDonation"] = bd;
                Response.Redirect("~/BloodBank/Donor_Survey_ViewOnly.aspx");
            }
        }

        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {

            Session.Clear();
            Session.RemoveAll();
            Server.Transfer("~/Default.aspx");
        }


        private void GetUnreadNotif()
        {
            LifePoints.Database.account bb = Session["ACCOUNT"] as LifePoints.Database.account;

            //Get Unread COunt
            string query = string.Format(@"select count(*) from notifications where NTF_RECEIVER_ID={0} and NTF_STATUS=false;", bb.ACC_ID);
            int count = db.GetUnreadNotificationCount(query);

            if (count <= 9)
            {
                UnreadCount.InnerText = count.ToString();
            }
            else
            {
                UnreadCount.InnerText = "9+";
            }
            Debug.Print("Unread Count : " + count);

            query = string.Format(@"select * from notifications where NTF_RECEIVER_ID={0} order by NTF_STATUS, NTF_DATE desc", bb.ACC_ID);
            List<notifications> nList = db.GetNotifications(query);
            if (nList != null && nList[0].NTF_ID != null)
            {
                List<notifications> unread = nList.Where(x => x.NTF_STATUS == false).Select(g => g).ToList();
                if (unread != null)
                {
                    int rows = 0;
                    if (count > 5)
                    {
                        rows = 5;
                    }
                    else
                    {
                        rows = unread.Count;
                    }
                    List<notifications> newUnread = new List<notifications>();
                    for (int i = 0; i < rows; i++)
                    {
                        newUnread.Add(unread[i]);
                    }

                    NotificationNavList.DataSource = null;
                    NotificationNavList.DataSource = newUnread;
                    NotificationNavList.DataBind();
                }
            }
        }
    }
}