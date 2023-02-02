using LifePoints.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifePoints
{
    public partial class USER_CHAT : System.Web.UI.Page
    {
        Database_Connection db = new Database_Connection();
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
                Debug.Print("Username : " + Username.InnerText);
                GetUnreadNotif();
                PopulateUserConvo();
            }
        }
        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {

            Session.Clear();
            Session.RemoveAll();
            Server.TransferRequest("~/Default.aspx");
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

        public void PopulateUserConvo()
        {
            account acc = Session["ACCOUNT"] as account;
            DataTable dt = db.GetUserConvo(acc.ACC_ID);
            if(dt != null)
            {
                UserConvo.DataSource = null;
                UserConvo.DataSource = dt;
                UserConvo.DataBind();
            }
        }

        protected void StartConvoBtn_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                account acc = Session["ACCOUNT"] as account;
                string email = UserEmail.Text;
                string id = acc.ACC_ID;

                if(string.Equals(email, acc.ACC_EMAIL, StringComparison.OrdinalIgnoreCase))
                {
                    Response.Write("<script>alert('Should not used own email. Try Again.')</script>");
                    UserEmail.Text = "";
                }
                else
                {
                    int[] res = db.InsertUserConvo(email, id);
                    switch (res[0])
                    {
                        case -1:
                            //Database Error
                            Response.Write("<script>alert('Database Error. Try Again')</script>");
                            break;
                        case -2:
                            //Email not foun
                            Response.Write("<script>alert('Email Address Not Found on Record. Try Again')</script>");
                            break;
                        case -3:
                            //Convo ALready Exists
                            Response.Write("<script>alert('Conversation with the User already exists.')</script>");
                            UserEmail.Text = "";
                            break;
                        case 1:
                            //Success Inserting User Convo
                            Response.Write("<script>alert('Conversation with the User added successfully. You may now message each other.')</script>");
                            UserEmail.Text = "";
                            PopulateUserConvo();
                            break;
                    }
                }
            }
        }

        protected void UserConvo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "ViewMessages")
            {
                string[] info = e.CommandArgument.ToString().Split(new char[] { ',' });
                string cv_id = info[0];
                string name = info[1];

                ConvoReceiver.InnerText = name;
                Session["CV_ID"] = cv_id.ToString();
                MessageInbox.Style.Add("display", "");
                PopulateMessageInbox();

            }
        }

        public void PopulateMessageInbox()
        {
            string cv_id = Session["CV_ID"] as string;
            account acc = Session["ACCOUNT"] as account;
            DataTable dt = db.GetUserMessage(acc.ACC_ID, cv_id);
            if(dt != null)
            {
                UserMessage.DataSource = null;
                UserMessage.DataSource = dt;
                UserMessage.DataBind();
            }
        }

        protected void SendBtn_Click(object sender, EventArgs e)
        {
            string cv_id = Session["CV_ID"] as string;
            account acc = Session["ACCOUNT"] as account;
            string msg = Message.Text;

            if(db.InsertUserMessage(cv_id, msg, acc.ACC_ID))
            {
                Message.Text = "";
                PopulateMessageInbox();
            }
        }
    }
}