using BloodBank.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifePoints
{
    public partial class BB_Dashboard : System.Web.UI.Page
    {
        private Database_Connections db = new Database_Connections();
        public string[] Labels { get; set; }
        public int[] Data { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Convert.ToBoolean(Session["LOGIN"]))
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Page.IsPostBack)
            {

                Debug.Print("LOGIN : " + Session["LOGIN"].ToString());
                Debug.Print("LOGIN : " + Session["ACCOUNT"].ToString());
                LifePoints.Database.account bb = Session["ACCOUNT"] as LifePoints.Database.account;
                //Set Username
                username.InnerText = bb.ACC_EMAIL;
                PopulateDashboardObjects();
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
                Response.Redirect("~/BB_Notification.aspx");
            }
        }

        public void PopulateDashboardObjects()
        {
            //Populate Number of Users
            string query = "select count(*) as USER_COUNT from account where ACC_STATUS=true and ACC_TYPE=3;";
            TotalNumberUser.InnerText = db.GetCount(query).ToString();

            query = "select count(distinct BREQ_UACC_ID) as REQ_COUNT from blood_request join account on ACC_ID=BREQ_UACC_ID where ACC_STATUS=true and ACC_TYPE=3;";
            NumberRequestor.InnerText = db.GetCount(query).ToString();

            query = "select count(distinct BD_UACC_ID) as DON_COUNT from blood_donation join account on ACC_ID=BD_UACC_ID where (BD_SURVEY_STATUS = true and BD_BLOOD_STATUS = true) and BD_REQ_STATUS=true and ACC_STATUS=true and ACC_TYPE=3;";
            NumberDonor.InnerText = db.GetCount(query).ToString();


            //Populate Transaction NUmbers
            query = @"select ((select count(*) from blood_donation where (BD_SURVEY_STATUS = false or BD_BLOOD_STATUS = false) and BD_REQ_STATUS=true)
                        + (select count(*) from blood_request where (BREQ_SURVEY_STATUS = false or BREQ_BLOOD_STATUS = false) and BREQ_REQ_STATUS=true)) as Total;";
            TotalNumberTransaction.InnerText = db.GetCount(query).ToString();

            query = @"select count(*) from blood_request where (BREQ_SURVEY_STATUS = false or BREQ_BLOOD_STATUS = false) and BREQ_REQ_STATUS=true;";
            NumberRequestTransaction.InnerText = db.GetCount(query).ToString();

            query = @"select count(*) from blood_donation where (BD_SURVEY_STATUS = false or BD_BLOOD_STATUS = false) and BD_REQ_STATUS=true;";
            NumberDonationTransaction.InnerText = db.GetCount(query).ToString();

            object sender = new object();
            EventArgs e = new EventArgs();
            PieOption_SelectedIndexChanged(sender, e);


        }

        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("~/Default.aspx");
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
            if (nList != null)
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

        protected void PieOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] transactions = new int[3];
            string[] lbl = new string[3];
            string query = "";

            lbl[0] = "Completed";
            lbl[1] = "Rejected";
            lbl[2] = "Pending";

            switch (PieOption.SelectedValue)
            {
                case "0":
                    //Blood Requests
                    PieTitle.InnerText = "Requests";
                    query = @"select count(*) as Total from blood_request where (BREQ_SURVEY_STATUS = true and BREQ_BLOOD_STATUS = true) and BREQ_REQ_STATUS=true;";
                    transactions[0] = db.GetCount(query);

                    query = @"select count(*) as Total from blood_request where BREQ_REQ_STATUS=false;";
                    transactions[1] = db.GetCount(query);

                    query = @"select count(*) as Total from blood_request where (BREQ_SURVEY_STATUS = false or BREQ_BLOOD_STATUS = false) and BREQ_REQ_STATUS=true;";
                    transactions[2] = db.GetCount(query);
                    break;
                case "1":
                    //Blood Donations
                    PieTitle.InnerText = "Donations";
                    query = @"select count(*) as Total from blood_donation where (BD_SURVEY_STATUS = true and BD_BLOOD_STATUS = true) and BD_REQ_STATUS=true;";
                    transactions[0] = db.GetCount(query);

                    query = @"select count(*) as Total from blood_donation where BD_REQ_STATUS=false;";
                    transactions[1] = db.GetCount(query);

                    query = @"select count(*) as Total from blood_donation where (BD_SURVEY_STATUS = false or BD_BLOOD_STATUS = false) and BD_REQ_STATUS=true;";
                    transactions[2] = db.GetCount(query);
                    break;
            }

            Labels = lbl;
            Data = transactions;
        }
    }
}