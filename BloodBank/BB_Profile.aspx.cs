using LifePoints.BloodBank.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifePoints
{
    public partial class BB_Profile : System.Web.UI.Page
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
                Profile_Username.Value = bb.ACC_EMAIL;
                GetUnreadNotif();
            }
        }

        //Update Info (this is from the button)
        protected void UpdateInfo_Click(object sender, EventArgs e)
        {
            string uname = username.InnerText;
            string npword = Profile_NPassword.Value;
            string opword = Profile_OPassword.Value;

            bool isSuccess = false;
            if (npword == "" || opword == "")
            {
                //Missing Fields
                Response.Write("<script>alert('Missing Fields.')</script>");
            }
            else
            {
                int res = db.UpdateProfileInfo(uname, npword, opword);
                //1 if Success
                //-2 if Old Password is not the one on record
                //-1 if Database
                switch (res)
                {
                    case 1:
                        Response.Write("<script>alert('Profile Password Updated Successfully.')</script>");
                        isSuccess = true;
                        break;
                    case -2:
                        Response.Write("<script>alert('Old Profile Password does not match the one on record.')</script>");
                        break;
                    case -1:
                        Response.Write("<script>alert('Database Error.')</script>");
                        break;
                }

            }


            if (isSuccess)
            {
                Session.Clear();
               Response.Redirect("~/Default.aspx");
            }
        }

        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {

            Session.Clear();
            Session.RemoveAll();
            Session["LOGIN"] = false;
            Response.Redirect("~/Default.aspx");
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