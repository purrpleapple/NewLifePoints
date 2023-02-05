using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LifePoints.Database;

namespace LifePoints
{
    public partial class USER_BECOMEADONOR : System.Web.UI.Page
    {
        private Database_Connection db = new Database_Connection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                user_info ua = Session["USER_INFO"] as user_info;
                Username.InnerText = ua.UI_FNAME + " " + ua.UI_LNAME;
                PopulateDonationBloodGrid();
                GetUnreadNotif();
            }
        }

        private void PopulateDonationBloodGrid()
        {
            user_info ua = Session["USER_INFO"] as user_info;

            DataTable dt = db.GetuserBloodDonation(ua.UI_ID);

            if (dt != null)
            {
                GridUserBloodDonation.DataSource = null;
                GridUserBloodDonation.DataSource = dt;
                GridUserBloodDonation.DataBind();
            }
        }

        protected void GridUserBloodDonation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridUserBloodDonation.SelectedRow;
            string id = row.Cells[0].Text;

            blood_donation br = db.SearchBloodDonation(id);
            if (br != null)
            {

                Session["BloodDonation"] = br;
                Response.Redirect("~/USER_DONOR_SURVEY_FORM_VIEW.aspx");
            }
        }

        protected void NotificationNavList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewNotif")
            {
                string id = e.CommandArgument.ToString();
                Session["IsViewing"] = true;
                Session["NTF_ID"] = id;
                Response.Redirect("~/USER_NOTIFICATION.aspx");
            }
        }

        private void GetUnreadNotif()
        {
            user_info ua = Session["USER_INFO"] as user_info;

            //Get Unread COunt
            string query = string.Format(@"select count(*) from notifications where NTF_RECEIVER_ID={0} and NTF_STATUS=false;", ua.UI_ID);
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
            if (count > 0)
            {
                query = string.Format(@"select * from notifications where NTF_RECEIVER_ID={0} order by NTF_STATUS, NTF_DATE desc", ua.UI_ID);
                List<notifications> nList = db.GetNotifications(query);
                try
                {
                    if (nList != null)
                    {
                        if (nList[0].NTF_ID != null)
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
                catch (Exception ex)
                {
                    Debug.Print("No Notification : " + ex.Message);
                }
            }
        }

        protected void BloodDonor_Click(object sender, EventArgs e)
        {
            Session["Input"] = true;

            user_info ua = Session["USER_INFO"] as user_info;

            blood_donation br = new blood_donation();

            br.BD_UACC_ID = ua.UI_ID;

            int res = db.ClickDonationrequest(br);
            Debug.Print("Result After DB : " + res);
            switch (res)
            {
                case 1: //Success
                    Response.Redirect("~/USER_DONOR_SURVEY_FORM.aspx");
                    break;
                case -2:
                    Response.Write("<script>alert('You are only allowed to donate again after 3 months.')</script>");
                    break;
                case -1:
                    Response.Write("<script>alert('You have already made a request. Wait till the process is completed.')</script>");
                    break;
            }

        }


        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {

            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("~/Default.aspx");
        }

    }
}