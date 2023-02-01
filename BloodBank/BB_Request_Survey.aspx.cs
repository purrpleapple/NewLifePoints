using LifePoints.BloodBank.Database;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifePoints
{
    public partial class BB_Request_Survey : System.Web.UI.Page
    {
        private Database_Connections db = new Database_Connections();
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
                PopulateDropDown();
                PopulateFormInputs();
                GetUnreadNotif();
            }
        }

        public void PopulateDropDown()
        {
            string[] bloodType = new string[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

            Bloodtype.Items.Insert(0, new ListItem("Select Blood Type", ""));
            int i = 1;
            foreach (string type in bloodType)
            {
                Bloodtype.Items.Insert(i++, new ListItem(type, type));
            }

            Gender.Items.Insert(0, new ListItem("Male", "1"));
            Gender.Items.Insert(1, new ListItem("Female", "0"));
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


        private void PopulateFormInputs()
        {
            LifePoints.Database.blood_request br = Session["BloodRequest"] as LifePoints.Database.blood_request;
            LifePoints.Database.request_survey_form rq = JsonConvert.DeserializeObject<LifePoints.Database.request_survey_form>(br.BREQ_JSON_SURVEY_FORM);
            Debug.Print(br.BREQ_JSON_SURVEY_FORM);

            LName.Text = rq.lname;
            FName.Text = rq.fname;
            MName.Text = rq.mname;
            Gender.SelectedValue = rq.gender;
            DOB.Text = rq.dob;


            Bloodtype.SelectedValue = rq.bloodtype;
            No_blood.Text = rq.no_blood;
            Demand_date.Text = rq.demand_date;

            City.Text = rq.city;
            Street.Text = rq.street;
            Province.Text = rq.province;
            Baranggay.Text = rq.barangay;
            Zip.Text = rq.zip;
            Home.Text = rq.homenum;
            Mobile.Text = rq.mobilenum;
            Email.Text = rq.email;
            Debug.Print("~/Uploads/" + br.BREQ_CONSENT);
            DoctorsConsent.ImageUrl = "~/Uploads/" + br.BREQ_CONSENT;

            DisableInputs();

            if(!br.BREQ_REQ_STATUS)
            {
                SurveyGroup.Style.Add("display", "none");
                BloodGroup.Style.Add("display", "none");
            }
            else if (!br.BREQ_SURVEY_STATUS)
            {
                SurveyGroup.Style.Add("display", "");
                BloodGroup.Style.Add("display", "none");
            }
            else if(!br.BREQ_BLOOD_STATUS)
            {
                SurveyGroup.Style.Add("display", "none");
                BloodGroup.Style.Add("display", "");
            }
        }

        private void DisableInputs()
        {
            LName.Enabled = false;
            FName.Enabled = false;
            MName.Enabled = false;
            DOB.Enabled = false;
            Bloodtype.Enabled = false;
            Gender.Enabled = false;
            Street.Enabled = false;
            City.Enabled = false;
            Province.Enabled = false;
            Baranggay.Enabled = false;
            Zip.Enabled = false;
            Home.Enabled = false;
            Mobile.Enabled = false;
            Email.Enabled = false;
            No_blood.Enabled = false;
            Demand_date.Enabled = false;
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BloodBank/BB_BloodTransaction.aspx");
        }

        protected void ApproveSurveyBtn_Click(object sender, EventArgs e)
        {
            UserRequestSurveyResponse(true);
        }

        protected void RejectSurveyBtn_Click(object sender, EventArgs e)
        {
            UserRequestSurveyResponse(false);
        }

        private void UserRequestSurveyResponse(bool res)
        {
            LifePoints.Database.blood_request br = Session["BloodRequest"] as LifePoints.Database.blood_request;
            LifePoints.Database.account bb = Session["ACCOUNT"] as LifePoints.Database.account;
            string query = "";
            DateTime vDate = DateTime.Now.AddDays(2);


            int number = int.Parse(No_blood.Text);
            string type = Bloodtype.Text;
            if (db.BR_UpdateInventory(number, type))
            {
                Response.Write(string.Format("<script>alert('Updated inventory succesfully " + number + "')</script>"));

                if (res)
                {
                    query = string.Format(@"update blood_request set BREQ_SURVEY_STATUS={0}, BREQ_VISIT_DATE='{1}' where BREQ_ID={2}", res, vDate, br.BREQ_ID);
                    Debug.Print(query);
                    if (db.UpdateBloodRequestStatus(query))
                    {

                        //Create Logs
                        string description = string.Format("{0} Accepted User {1} ( ", bb.ACC_EMAIL, br.BREQ_UACC_ID);
                        query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('{0}', UI_FNAME, ' ', UI_LNAME, ') Initial Blood Request Form'), {1}, '{2}' from user_info
                                            where UI_ID={3};", description, bb.ACC_ID, "BloodBank", br.BREQ_UACC_ID);
                        Debug.Print(query);
                        bool x = db.InsertBloodBankLogs(query);
                        //If Not Successfully Inserted Logs
                        if (!x)
                        {
                            Debug.Print("BloodBank Logs Not Inserted");
                        }

                        //Send Notification
                        string sbj = "Blood Request Form Accepted";
                        string msg = MySqlHelper.EscapeString(string.Format(@"Your Request ID {0}
Your request has been approved you may now proceed to claim your request.
                                                    
Please bring the following with you:
Any valid ID
Doctor's consent for blood bag request with Doctor's name and signature
Ice bucket filled with ice
Processing fee: P1,500.00
                                                    
*Please keep in mind that you can only claim your request until the following date: {1}
*Note: Show your Request ID to the bloodbank.", br.BREQ_ID, vDate));
                        query = string.Format(@"insert into notifications(NTF_SUBJECT, NTF_MESSAGE, NTF_RECEIVER_ID, NTF_SENDER_ID) 
                                                values('{0}', '{1}', {2}, {3})", sbj, msg, br.BREQ_UACC_ID, bb.ACC_ID);
                        if (!db.InsertToNotification(query))
                        {
                            Debug.Print("Notification was not sent.");
                        }

                        //Success
                        Response.Write(string.Format("<script>alert('User {0} blood request survey was successfully approved.')</script>", br.BREQ_UACC_ID));

                        SurveyGroup.Style.Add("display", "none");
                        BloodGroup.Style.Add("display", "");

                        Response.Redirect("~/BloodBank/BB_BloodTransaction.aspx");
                    }
                }
                else
                {
                    query = string.Format(@"update blood_request set BREQ_SURVEY_STATUS=false, BREQ_BLOOD_STATUS=false, BREQ_REQ_STATUS={0} where BREQ_ID={1}", res, br.BREQ_ID);
                    if (db.UpdateBloodRequestStatus(query))
                    {
                        //Create Login Logs
                        string description = string.Format("{0} Rejected User {1} ( ", bb.ACC_EMAIL, br.BREQ_UACC_ID);
                        query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('{0}', UI_FNAME, ' ', UI_LNAME, ') Initial Blood Request Form'), {1}, '{2}' from user_info
                                            where UI_ID={3};", description, bb.ACC_ID, "BloodBank", br.BREQ_UACC_ID);

                        Debug.Print(query);
                        bool x = db.InsertBloodBankLogs(query);
                        //If Not Successfully Inserted Logs
                        if (!x)
                        {
                            Debug.Print("BloodBank Logs Not Inserted");
                        }

                        //Send Notification
                        string sbj = "Blood Request Form Rejected";
                        string msg = string.Format(@"Your Request ID {0}
Your request has been rejected.", br.BREQ_ID);
                        query = string.Format(@"insert into notifications(NTF_SUBJECT, NTF_MESSAGE, NTF_RECEIVER_ID, NTF_SENDER_ID) 
                                                values('{0}', '{1}', {2}, {3})", sbj, msg, br.BREQ_UACC_ID, bb.ACC_ID);
                        if (!db.InsertToNotification(query))
                        {
                            Debug.Print("Notification was not sent.");
                        }

                        //Success
                        Response.Write(string.Format("<script>alert('User {0} blood request survey was successfully rejected.')</script>", br.BREQ_UACC_ID));

                        SurveyGroup.Style.Add("display", "none");
                        BloodGroup.Style.Add("display", "none");

                        Response.Redirect("~/BloodBank/BB_BloodTransaction.aspx");
                    }
                }
            }
            else
            {
                Response.Write(string.Format("<script>alert('Error in updating inventory succesfully " + number + " ')</script>"));
            }

        }

        protected void ApproveBloodBtn_Click(object sender, EventArgs e)
        {
            UserRequestBloodResponse(true);
        }

        protected void RejectBloodBtn_Click(object sender, EventArgs e)
        {
            UserRequestBloodResponse(false);
        }

        private void UserRequestBloodResponse(bool res)
        {
            LifePoints.Database.blood_request br = Session["BloodRequest"] as LifePoints.Database.blood_request;
            LifePoints.Database.account bb = Session["ACCOUNT"] as LifePoints.Database.account;
            string query = "";

            if (res)
            {
                query = string.Format(@"update blood_request set BREQ_BLOOD_STATUS={0} where BREQ_ID={1}", res, br.BREQ_ID);
                if (db.UpdateBloodRequestStatus(query))
                {
                    //Create Login Logs
                    string description = string.Format("{0} Accepted User {1} ( ", bb.ACC_EMAIL, br.BREQ_UACC_ID);
                    query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('{0}', UI_FNAME, ' ', UI_LNAME, ' ) Final Blood Request Form'), {1}, '{2}' from user_info
                                            where UI_ID={3};", description, bb.ACC_ID,"BloodBank", br.BREQ_UACC_ID);

                    Debug.Print(query);
                    bool x = db.InsertBloodBankLogs(query);
                    //If Not Successfully Inserted Logs
                    if (!x)
                    {
                        Debug.Print("BloodBank Logs Not Inserted");
                    }
                    //Success
                    Response.Write(string.Format("<script>alert('User {0} blood request was successfully approved.')</script>", br.BREQ_UACC_ID));

                    SurveyGroup.Style.Add("display", "none");
                    BloodGroup.Style.Add("display", "none");
                }
            }
            else
            {
                query = string.Format(@"update blood_request set BREQ_BLOOD_STATUS=false, BREQ_REQ_STATUS={0} where BREQ_ID={1}", res, br.BREQ_ID);
                if (db.UpdateBloodRequestStatus(query))
                {
                    //Create Login Logs
                    string description = string.Format("{0} Rejected User {1} ( ", bb.ACC_EMAIL, br.BREQ_UACC_ID);
                    query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('{0}', UI_FNAME, ' ', UI_LNAME, ' ) Final Blood Request Form'), {1}, '{2}' from user_info
                                            where UI_ID={3};", description, bb.ACC_ID, "BloodBank", br.BREQ_UACC_ID);


                    Debug.Print(query);
                    bool x = db.InsertBloodBankLogs(query);
                    //If Not Successfully Inserted Logs
                    if (!x)
                    {
                        Debug.Print("BloodBank Logs Not Inserted");
                    }
                    //Success
                    Response.Write(string.Format("<script>alert('User {0} blood request was successfully rejected.')</script>", br.BREQ_UACC_ID));

                    SurveyGroup.Style.Add("display", "none");
                    BloodGroup.Style.Add("display", "none");
                }
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