using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LifePoints.Database;

namespace LifePoints
{
    public partial class USER_PROFILE1 : System.Web.UI.Page
    {
        private Database_Connection db = new Database_Connection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Convert.ToBoolean(Session["LOGIN"]))
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                user_info ua = Session["USER_INFO"] as user_info;
                Username.InnerText = ua.UI_FNAME + " " + ua.UI_LNAME;
                Display();
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
                Response.Redirect("~/USER_NOTIFICATION.aspx");
            }
        }
        public void Display()
        {
            Session["Input"] = true;
            user_info ua = Session["USER_INFO"] as user_info;
            account acc = Session["ACCOUNT"] as account;


            UPD_F.Text = ua.UI_FNAME;
            UPD_M.Text = ua.UI_MNAME;
            UPD_L.Text = ua.UI_LNAME;
            UPD_EMAIL.Text = acc.ACC_EMAIL;
            UPD_PASS.Text = acc.ACC_PASSWORD;
            UPD_RPASS.Text = acc.ACC_PASSWORD;
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            //    Session["Input"] = true;
            //    user_info ua= Session["USER_INFO"] as user_info;
            //    user_info ub = new user_info();
            //    account acc = Session["ACCOUNT"] as account;

            //    string npword = UPD_PASS.Text;
            //    string rpword = UPD_RPASS.Text;
            //    string email = acc.ACC_EMAIL;

            //    bool isSuccess = false;
            //    if (npword == "" || rpword == "")
            //    {
            //        //Missing Fields
            //        Response.Write("<script>alert('Missing Fields.')</script>");
            //    }
            //    else if(npword!=rpword){
            //        Response.Write("<script>alert('Password Does Not Match.')</script>");
            //    }
            //    else {

            //    ub.UI_ID = ua.UI_ID;
            //    ub.UI_FNAME = UPD_F.Text;
            //    ub.UI_MNAME = UPD_M.Text;
            //    ub.UI_LNAME = UPD_L.Text;
            //    ub.UACC_EMAIL = UPD_EMAIL.Text;
            //    ub.UACC_PASSWORD = UPD_PASS.Text;


            //        int res = db.UpdateUserAccount(ub,email);
            //        //1 if Success

            //        //-1 if Database
            //        switch (res)
            //        {
            //            case 1:
            //                Response.Write("<script>alert('Profile Updated Successfully.')</script>");
            //                isSuccess = true;
            //                break;
            //            case -2:
            //                Response.Write("<script>alert('Email already exist')</script>");
            //                break;

            //            case -1:
            //                Response.Write("<script>alert('Database Error.')</script>");
            //                break;
            //        }

            //    }
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


        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {

            Session.Clear();
            Session.RemoveAll();
            Session["LOGIN"] = false;
            Response.Redirect("~/Default.aspx");
        }

    }
}

