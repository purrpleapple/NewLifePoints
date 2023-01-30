using BloodBank.Database;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LifePoints
{
    public partial class Donor_Survey_ViewOnly : System.Web.UI.Page
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
                PopulateSurveyForm();
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
                Response.Redirect("BB_Notification.aspx");
            }
        }

        public void PopulateSurveyForm()
        {
            //Basic/Personal Information
            blood_donation bd = Session["BloodDonation"] as blood_donation;
            DonorSurvey ds = JsonConvert.DeserializeObject<DonorSurvey>(bd.BD_JSON_SURVEY_FORM);

            //Basic/Personal Information
            ViewState["familyname"] = ds.personalInfo.Lname;
            ViewState["firstname"] = ds.personalInfo.Fname;
            ViewState["middlename"] = ds.personalInfo.Mname;
            ViewState["gender"] = ds.personalInfo.Gender;
            ViewState["dob"] = ds.personalInfo.Dob;
            ViewState["street"] = ds.personalInfo.Street;
            ViewState["barangay"] = ds.personalInfo.Barangay;
            ViewState["city"] = ds.personalInfo.City; ;
            ViewState["province"] = ds.personalInfo.Province;
            ViewState["zip"] = ds.personalInfo.Zip;
            ViewState["homenum"] = ds.personalInfo.Home;
            ViewState["mobilenum"] = ds.personalInfo.Mobile;
            ViewState["email"] = ds.personalInfo.Email;

            //Response.Write("<script>alert('" + ds.healthAssessment.N16d + "')</script>");
            //
            rd11y.Checked = (string.Equals(ds.healthAssessment.N11, "yes", StringComparison.OrdinalIgnoreCase));
            rd11n.Checked = (string.Equals(ds.healthAssessment.N11, "no", StringComparison.OrdinalIgnoreCase));

            rd12y.Checked = (string.Equals(ds.healthAssessment.N12, "yes", StringComparison.OrdinalIgnoreCase));
            rd12n.Checked = (string.Equals(ds.healthAssessment.N13, "no", StringComparison.OrdinalIgnoreCase));

            rd13y.Checked = (string.Equals(ds.healthAssessment.N13, "yes", StringComparison.OrdinalIgnoreCase));
            rd13n.Checked = (string.Equals(ds.healthAssessment.N13, "no", StringComparison.OrdinalIgnoreCase));

            rd14y.Checked = (string.Equals(ds.healthAssessment.N14, "yes", StringComparison.OrdinalIgnoreCase));
            rd14n.Checked = (string.Equals(ds.healthAssessment.N14, "no", StringComparison.OrdinalIgnoreCase));

            rd15y.Checked = (string.Equals(ds.healthAssessment.N15, "yes", StringComparison.OrdinalIgnoreCase));
            rd15n.Checked = (string.Equals(ds.healthAssessment.N15, "no", StringComparison.OrdinalIgnoreCase));

            rd16ay.Checked = (string.Equals(ds.healthAssessment.N16a, "yes", StringComparison.OrdinalIgnoreCase));
            rd16an.Checked = (string.Equals(ds.healthAssessment.N16a, "no", StringComparison.OrdinalIgnoreCase));

            rd16by.Checked = (string.Equals(ds.healthAssessment.N16b, "yes", StringComparison.OrdinalIgnoreCase));
            rd16bn.Checked = (string.Equals(ds.healthAssessment.N16b, "no", StringComparison.OrdinalIgnoreCase));
                    
            rd16cy.Checked = (string.Equals(ds.healthAssessment.N16c, "yes", StringComparison.OrdinalIgnoreCase));
            rd16cn.Checked = (string.Equals(ds.healthAssessment.N16c, "no", StringComparison.OrdinalIgnoreCase));

            rd16dy.Checked = (string.Equals(ds.healthAssessment.N16d, "yes", StringComparison.OrdinalIgnoreCase));
            rd16dn.Checked = (string.Equals(ds.healthAssessment.N16d, "no", StringComparison.OrdinalIgnoreCase));

            rd17y.Checked = (string.Equals(ds.healthAssessment.N17, "yes", StringComparison.OrdinalIgnoreCase));
            rd17n.Checked = (string.Equals(ds.healthAssessment.N17, "no", StringComparison.OrdinalIgnoreCase));

            rd18ay.Checked = (string.Equals(ds.healthAssessment.N18a, "yes", StringComparison.OrdinalIgnoreCase));
            rd18an.Checked = (string.Equals(ds.healthAssessment.N18a, "no", StringComparison.OrdinalIgnoreCase));

            rd18by.Checked = (string.Equals(ds.healthAssessment.N18b, "yes", StringComparison.OrdinalIgnoreCase));
            rd18bn.Checked = (string.Equals(ds.healthAssessment.N18b, "no", StringComparison.OrdinalIgnoreCase));

            rd19ay.Checked = (string.Equals(ds.healthAssessment.N19a, "yes", StringComparison.OrdinalIgnoreCase));
            rd19an.Checked = (string.Equals(ds.healthAssessment.N19a, "no", StringComparison.OrdinalIgnoreCase));

            rd19by.Checked = (string.Equals(ds.healthAssessment.N19b, "yes", StringComparison.OrdinalIgnoreCase));
            rd19bn.Checked = (string.Equals(ds.healthAssessment.N19b, "no", StringComparison.OrdinalIgnoreCase));

            rd19cy.Checked = (string.Equals(ds.healthAssessment.N19c, "yes", StringComparison.OrdinalIgnoreCase));
            rd19cn.Checked = (string.Equals(ds.healthAssessment.N19c, "no", StringComparison.OrdinalIgnoreCase));

            rd110y.Checked = (string.Equals(ds.healthAssessment.N110, "yes", StringComparison.OrdinalIgnoreCase));
            rd110n.Checked = (string.Equals(ds.healthAssessment.N110, "no", StringComparison.OrdinalIgnoreCase));

            rd111y.Checked = (string.Equals(ds.healthAssessment.N111, "yes", StringComparison.OrdinalIgnoreCase));
            rd111n.Checked = (string.Equals(ds.healthAssessment.N111, "no", StringComparison.OrdinalIgnoreCase));

            rd112y.Checked = (string.Equals(ds.healthAssessment.N112, "yes", StringComparison.OrdinalIgnoreCase));
            rd112n.Checked = (string.Equals(ds.healthAssessment.N112, "no", StringComparison.OrdinalIgnoreCase));

            rd113y.Checked = (string.Equals(ds.healthAssessment.N113, "yes", StringComparison.OrdinalIgnoreCase));
            rd113n.Checked = (string.Equals(ds.healthAssessment.N113, "no", StringComparison.OrdinalIgnoreCase));

            rd114ay.Checked = (string.Equals(ds.healthAssessment.N114a, "yes", StringComparison.OrdinalIgnoreCase));
            rd114an.Checked = (string.Equals(ds.healthAssessment.N114a, "no", StringComparison.OrdinalIgnoreCase));

            rd114by.Checked = (string.Equals(ds.healthAssessment.N114b, "yes", StringComparison.OrdinalIgnoreCase));
            rd114bn.Checked = (string.Equals(ds.healthAssessment.N114b, "no", StringComparison.OrdinalIgnoreCase));

            ViewState["rd115"] = ds.healthAssessment.N115;

            //
            rd21y.Checked = (string.Equals(ds.riskAssessment.N21, "yes", StringComparison.OrdinalIgnoreCase));
            rd21n.Checked = (string.Equals(ds.riskAssessment.N21, "no", StringComparison.OrdinalIgnoreCase));

            rd22y.Checked = (string.Equals(ds.riskAssessment.N22, "yes", StringComparison.OrdinalIgnoreCase));
            rd22n.Checked = (string.Equals(ds.riskAssessment.N22, "no", StringComparison.OrdinalIgnoreCase));

            switch (ds.riskAssessment.N23)
            {
                case "voluntary":
                    rd23VOL.Checked = (string.Equals(ds.riskAssessment.N23, "voluntary", StringComparison.OrdinalIgnoreCase));
                    break;
                case "employment":
                    rd23EMP.Checked = (string.Equals(ds.riskAssessment.N23, "employment", StringComparison.OrdinalIgnoreCase));
                    break;
                case "insurance":
                    rd23INS.Checked = (string.Equals(ds.riskAssessment.N23, "insurance", StringComparison.OrdinalIgnoreCase));
                    break;
                case "medadvice":
                    rd23MED.Checked = (string.Equals(ds.riskAssessment.N23, "medadvice", StringComparison.OrdinalIgnoreCase));
                    break;
                default:
                    ViewState["rd23"] = ds.riskAssessment.N23;
                    break;
            }

            rd24y.Checked = (string.Equals(ds.riskAssessment.N24, "yes", StringComparison.OrdinalIgnoreCase));
            rd24n.Checked = (string.Equals(ds.riskAssessment.N24, "no", StringComparison.OrdinalIgnoreCase));

            rd25y.Checked = (string.Equals(ds.riskAssessment.N25, "yes", StringComparison.OrdinalIgnoreCase));
            rd25n.Checked = (string.Equals(ds.riskAssessment.N25, "no", StringComparison.OrdinalIgnoreCase));

            rd26y.Checked = (string.Equals(ds.riskAssessment.N26, "yes", StringComparison.OrdinalIgnoreCase));
            rd26n.Checked = (string.Equals(ds.riskAssessment.N26, "no", StringComparison.OrdinalIgnoreCase));

            rd27ay.Checked = (string.Equals(ds.riskAssessment.N27a, "yes", StringComparison.OrdinalIgnoreCase));
            rd27an.Checked = (string.Equals(ds.riskAssessment.N27a, "no", StringComparison.OrdinalIgnoreCase));

            rd27by.Checked = (string.Equals(ds.riskAssessment.N27b, "yes", StringComparison.OrdinalIgnoreCase));
            rd27bn.Checked = (string.Equals(ds.riskAssessment.N27b, "no", StringComparison.OrdinalIgnoreCase));

            rd27cy.Checked = (string.Equals(ds.riskAssessment.N27c, "yes", StringComparison.OrdinalIgnoreCase));
            rd27cn.Checked = (string.Equals(ds.riskAssessment.N27c, "no", StringComparison.OrdinalIgnoreCase));

            rd28y.Checked = (string.Equals(ds.riskAssessment.N28, "yes", StringComparison.OrdinalIgnoreCase));
            rd28n.Checked = (string.Equals(ds.riskAssessment.N28, "no", StringComparison.OrdinalIgnoreCase));

            rd29y.Checked = (string.Equals(ds.riskAssessment.N29, "yes", StringComparison.OrdinalIgnoreCase));
            rd29n.Checked = (string.Equals(ds.riskAssessment.N29, "no", StringComparison.OrdinalIgnoreCase));

            rd210y.Checked = (string.Equals(ds.riskAssessment.N210, "yes", StringComparison.OrdinalIgnoreCase));
            rd210n.Checked = (string.Equals(ds.riskAssessment.N210, "no", StringComparison.OrdinalIgnoreCase));

            rd211y.Checked = (string.Equals(ds.riskAssessment.N211, "yes", StringComparison.OrdinalIgnoreCase));
            rd211n.Checked = (string.Equals(ds.riskAssessment.N211, "no", StringComparison.OrdinalIgnoreCase));


            if (!bd.BD_REQ_STATUS)
            {
                SurveyGroup.Style.Add("display", "none");
                BloodGroup.Style.Add("display", "none");
            }
            else if (!bd.BD_SURVEY_STATUS)
            {
                SurveyGroup.Style.Add("display", "");
                BloodGroup.Style.Add("display", "none");
            }
            else if (!bd.BD_BLOOD_STATUS)
            {
                SurveyGroup.Style.Add("display", "none");
                BloodGroup.Style.Add("display", "");
            }

        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("BB_BloodTransaction.aspx");
        }

        private void UserRequestSurveyResponse(bool res)
        {
            blood_donation bd = Session["BloodDonation"] as blood_donation;
            LifePoints.Database.account bb = Session["ACCOUNT"] as LifePoints.Database.account;
            string query = "";
            DateTime vDate = DateTime.Now.AddDays(2);
            if (res)
            {
                query = string.Format(@"update blood_donation set BD_SURVEY_STATUS={0}, BD_VISIT_DATE='{1}' where BD_ID={2}", res, vDate, bd.BD_ID);
                if (db.UpdateBloodRequestStatus(query))
                {
                    //Create Login Logs
                    string description = string.Format("{0} Accepted User {1} ( ","BloodBank", bd.BD_UACC_ID);
                    query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('{0}', UI_FNAME, ' ', UI_LNAME, ' ) Initial Blood Donation Request Form'), {1}, '{2}' from account
                                            where ACC_ID={3};", description, bb.ACC_ID, "BloodBank", bd.BD_UACC_ID);

                    Debug.Print(query);
                    bool x = db.InsertBloodBankLogs(query);
                    //If Not Successfully Inserted Logs
                    if (!x)
                    {
                        Debug.Print("BloodBank Logs Not Inserted");
                    }

                    //Send Notification
                    string sbj = "Blood Donation Form Accepted";
                    string msg = MySqlHelper.EscapeString(string.Format(@"Your Request ID {0}
Your request has been approved you may now proceed to claim your request.
                                                    
Your request has been approved you may now proceed to donate.
Please bring the following with you: {1}
Any valid ID

*Note: Show your Request ID to the bloodbank.", bd.BD_ID, vDate));
                    query = string.Format(@"insert into notifications(NTF_SUBJECT, NTF_MESSAGE, NTF_RECEIVER_ID, NTF_SENDER_ID) 
                                                values('{0}', '{1}', {2}, {3})", sbj, msg, bd.BD_UACC_ID, bb.ACC_ID);
                    if (!db.InsertToNotification(query))
                    {
                        Debug.Print("Notification was not sent.");
                    }

                    //Success
                    Response.Write(string.Format("<script>alert('User {0} blood donation survey was successfully approved.')</script>", bd.BD_UACC_ID));

                    SurveyGroup.Style.Add("display", "none");
                    BloodGroup.Style.Add("display", "");
                }
            }
            else
            {
                query = string.Format(@"update blood_donation set BD_SURVEY_STATUS=false, BD_BLOOD_STATUS=false, BD_REQ_STATUS={0} where BD_ID={1}", res, bd.BD_ID);
                if (db.UpdateBloodRequestStatus(query))
                {//Create Login Logs
                    string description = string.Format("{0} Rejected User {1} ( ", "BloodBank", bd.BD_UACC_ID);
                    query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('{0}', UI_FNAME, ' ', UI_LNAME, ' ) Initial Blood Donation Request Form'), {1}, '{2}' from account
                                            where ACC_ID={3};", description, bb.ACC_ID, "BloodBank", bd.BD_UACC_ID);

                    Debug.Print(query);
                    bool x = db.InsertBloodBankLogs(query);
                    //If Not Successfully Inserted Logs
                    if (!x)
                    {
                        Debug.Print("BloodBank Logs Not Inserted");
                    }

                    //Send Notification
                    string sbj = "Blood Donation Form Accepted";
                    string msg = MySqlHelper.EscapeString(string.Format(@"Your Request ID {0}
Your request has been rejected", bd.BD_ID));
                    query = string.Format(@"insert into notifications(NTF_SUBJECT, NTF_MESSAGE, NTF_RECEIVER_ID, NTF_SENDER_ID) 
                                                values('{0}', '{1}', {2}, {3})", sbj, msg, bd.BD_UACC_ID, bb.ACC_ID);
                    if (!db.InsertToNotification(query))
                    {
                        Debug.Print("Notification was not sent.");
                    }

                    //Success
                    Response.Write(string.Format("<script>alert('User {0} blood request survey was successfully rejected.')</script>", bd.BD_UACC_ID));

                    SurveyGroup.Style.Add("display", "none");
                    BloodGroup.Style.Add("display", "none");
                }
            }
        }


        private void UserRequestBloodResponse(bool res)
        {
            blood_donation bd = Session["BloodDonation"] as blood_donation;
            LifePoints.Database.account bb = Session["ACCOUNT"] as LifePoints.Database.account;
            string query = "";

            if (res)
            {
                query = string.Format(@"update blood_donation set BD_BLOOD_STATUS={0} where BD_ID={1}", res, bd.BD_ID);
                if (db.UpdateBloodRequestStatus(query))
                {//Create Login Logs
                    string description = string.Format("{0} Accepted User {1} ( ", "BloodBank", bd.BD_UACC_ID);
                    query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('{0}', UI_FNAME, ' ', UI_LNAME, ' ) Final  Blood Donation Request Form'), {1}, '{2}' from account
                                            where ACC_ID={3};", description, bb.ACC_ID, "BloodBank", bd.BD_UACC_ID);

                    Debug.Print(query);
                    bool x = db.InsertBloodBankLogs(query);
                    //If Not Successfully Inserted Logs
                    if (!x)
                    {
                        Debug.Print("BloodBank Logs Not Inserted");
                    }
                    //Success
                    Response.Write(string.Format("<script>alert('User {0} blood donation was successfully approved.')</script>", bd.BD_ID));

                    SurveyGroup.Style.Add("display", "none");
                    BloodGroup.Style.Add("display", "none");
                }
            }
            else
            {
                query = string.Format(@"update blood_donation set BD_BLOOD_STATUS=false, BD_REQ_STATUS={0} where BD_ID={1}", res, bd.BD_ID);
                if (db.UpdateBloodRequestStatus(query))
                {//Create Login Logs
                    string description = string.Format("{0} Rejected User {1} ( ", "BloodBank", bd.BD_UACC_ID);
                    query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('{0}', UI_FNAME, ' ', UI_LNAME, ' ) Final Blood Donation Request Form'), {1}, '{2}' from account
                                            where ACC_ID={3};", description, bb.ACC_ID, "BloodBank", bd.BD_UACC_ID);

                    Debug.Print(query);
                    bool x = db.InsertBloodBankLogs(query);
                    //If Not Successfully Inserted Logs
                    if (!x)
                    {
                        Debug.Print("BloodBank Logs Not Inserted");
                    }
                    //Success
                    Response.Write(string.Format("<script>alert('User {0} blood donation was successfully rejected.')</script>", bd.BD_ID));

                    SurveyGroup.Style.Add("display", "none");
                    BloodGroup.Style.Add("display", "none");
                }
            }
        }

        protected void ApproveSurveyBtn_Click(object sender, EventArgs e)
        {
            UserRequestSurveyResponse(true);
        }

        protected void RejectSurveyBtn_Click(object sender, EventArgs e)
        {
            UserRequestSurveyResponse(false);
        }

        protected void ApproveBloodBtn_Click(object sender, EventArgs e)
        {
            UserRequestBloodResponse(true);
        }

        protected void RejectBloodBtn_Click(object sender, EventArgs e)
        {
            UserRequestBloodResponse(false);
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