using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LifePoints.Database;

namespace LifePoints
{
    public partial class USER_BLOGPOST : System.Web.UI.Page
    {
        Database_Connection db = new Database_Connection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Convert.ToBoolean(Session["LOGIN"]))
            {
                Response.Redirect("~/Default.aspx");
            }

            if(!Page.IsPostBack)
            {
                user_info ua = Session["USER_INFO"] as user_info;
                Username.InnerText = ua.UI_FNAME + " " + ua.UI_LNAME;
                Debug.Print("Username : " + Username.InnerText);
                PopulatePosts();
                GetUnreadNotif();
            }
        }

        public void PopulatePosts()
        {
            DataTable dt = db.SampleReapeater();
            if (dt != null)
            {
                BlogPosts.DataSource = null;
                BlogPosts.DataSource = dt;
                BlogPosts.DataBind();
                Session["BlogPosts"] = dt;
            }
        }

        protected void BlogPosts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "ReportPost")
            {
                user_info ua = Session["USER_INFO"] as user_info;

                DataTable dt = Session["BlogPosts"] as DataTable;
                DataRow[] row = dt.Select("BLOG_ID='" + e.CommandArgument + "'");
                
                string reporter = "";

                if (row.Length > 0)
                {
                    reporter = row[0]["BLOG_REPORTER"].ToString();    
                }



                bool shouldReport = false;

                List<string> reporters = new List<string>();

                if (reporter == "")
                {
                    shouldReport = true;
                    reporters.Add(ua.UI_ID);
                }
                else
                {
                    reporters = JsonConvert.DeserializeObject<List<string>>(reporter);

                    string exist = reporters.Find(x => x == ua.UI_ID);
                    if (exist == null && exist != ua.UI_ID)
                    {
                        //Should Report
                        shouldReport = true;
                        reporters.Add(ua.UI_ID);
                    }
                }

                if (shouldReport)
                {
                    if (db.ReportPost(Convert.ToInt32(e.CommandArgument), JsonConvert.SerializeObject(reporters)))
                    {
                        //Insert Logs
                        string query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME) values('User {0} Reported Post {1}', {2}, '{3}');", ua.UI_FNAME, e.CommandArgument, ua.UI_ID, ua.UI_FNAME + " " + ua.UI_LNAME);
                        bool logs = db.InsertToUserLogs(query);
                        if (logs)
                        {
                            //Success
                            PopulatePosts();
                        }
                    }
                    else
                    {
                        //Error
                        Response.Write("<script>alert('Error in Reporting Blog Post.')</script>");
                    }
                }



            }

            if (e.CommandName == "ReplyClick")
            {

                PostPanel.Visible = false;
                MessagePanel.Visible = true;


                DataTable dt = Session["BlogPosts"] as DataTable;
                DataRow[] row = dt.Select("BLOG_ID='" + e.CommandArgument + "'");

                string reporter="";
                int id = 0;

                if (row.Length > 0)
                {
                    reporter = row[0]["BLOG_UACC_EMAIL"].ToString();
                    id = Convert.ToInt32(row[0]["BLOG_UACC_ID"]);
                   
                }

                ReceiverEmail.Text = reporter;
                ReceiverID.Text = id.ToString();

                PopulatemMessage();

            }
        }

        protected void PostBlog_Click(object sender, EventArgs e)
        {
            string content = BlogPostMessage.Text;
            if(content == "")
            {
                Response.Write("<script>alert('Missing Field.')</script>");
            }
            else
            {
                user_info ua = Session["USER_INFO"] as user_info;

                if (ua != null || ua.UI_FNAME != null)
                {
                    int res = db.PostBlogPost(content, ua.UI_ID);
                    if (res != -1)
                    {
                        //Success Insert
                        //Clear Content Field
                        BlogPostMessage.Text = "";

                        //Insert Logs
                        string query = string.Format(@"insert into user_logs(ULOG_EVENT, ULOG_UACC_ID) values('User {0} Posted Post {1}', {2});", ua.UI_FNAME, res, ua.UI_ID);
                        bool logs = db.InsertToUserLogs(query);

                        if (logs)
                        {
                            //Response.Write("<script>alert('Success Logs.')</script>");
                            PopulatePosts();
                        }

                    }
                    else
                    {
                        Response.Write("<script>alert('Error in Inserting Blog Post.')</script>");
                    }

                }
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

            if(count <= 9)
            {
                UnreadCount.InnerText = count.ToString();
            }
            else
            {
                UnreadCount.InnerText = "9+";
            }
            Debug.Print("Unread Count : " + count);

            if(count > 0)
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
            if(e.CommandName == "ViewNotif")
            {
                string id = e.CommandArgument.ToString();
                Session["IsViewing"] = true;
                Session["NTF_ID"] = id;
                Response.Redirect("~/USER_NOTIFICATION.aspx");
            }
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            
            user_info ua = Session["USER_INFO"] as user_info;
            user_info ub = new user_info();

            string emailR = ReceiverID.Text;
            int RecID = Convert.ToInt32(emailR);
            ub.UI_ID = ua.UI_ID;
            ub.UI_FNAME = ua.UI_FNAME;
            string message = Mess.Text;

            if (db.InsertUserMessage(ub,RecID,message))
            {
                //Successfullu Inseryted
               
                Mess.Text = "";
                PopulatemMessage();

            }
            else
            {
                Response.Write("<script>alert("+RecID+"'.'"+ub.UI_ID +"'.'"+message+")</script>");

            }
        }


        public void PopulatemMessage()
        {
            string recid= ReceiverID.Text;
            string emailR = ReceiverEmail.Text;
            int RecID = Convert.ToInt32(recid);

            user_info ua = Session["USER_INFO"] as user_info;
            string emailS = ua.UI_ID;
            int SendID = Convert.ToInt32(emailS);

            DataTable dt = db.MessageReapeater(RecID,SendID,emailR);
            if (dt != null)
            {
                MessageReply.DataSource = null;
                MessageReply.DataSource = dt;
                MessageReply.DataBind();
                Session["BlogPosts"] = dt;
            }
        }

        protected void Close_Click1(object sender, ImageClickEventArgs e)
        {
            PostPanel.Visible = true;
            MessagePanel.Visible = false;
        }
    }
}