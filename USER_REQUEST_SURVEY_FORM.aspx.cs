using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LifePoints.Database;
using System.IO;

namespace LifePoints
{
    public partial class USER_REQUEST_SURVEY_FORM : System.Web.UI.Page
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
                PopulateDropDown();
                GetUnreadNotif();
                UserRd.Items.Insert(0, new ListItem("Yes", "1"));
                UserRd.Items.Insert(1, new ListItem("No", "0"));
                UserRd.SelectedValue = "1";
                if (!Convert.ToBoolean(Session["Input"]))
                {
                    PopulateFormInputs();

                    Option.Style.Add("display", "none");
                    Survey.Style.Add("display", "");
                    Upload.Style.Add("display", "none");
                    DConsent.Style.Add("display", "");
                }
                else
                {
                    
                    PopulateFormInputsUserYes();
                    Option.Style.Add("display", "");
                    Survey.Style.Add("display", "none");
                    DConsent.Style.Add("display", "none");
                    Upload.Style.Add("display", "");
                }
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
                Response.Redirect("~/USER_NOTIFICATION.aspx");
            }
        }
        protected void SubmitSurvey_Click(object sender, EventArgs e)
        {
            if (Consent.HasFile)
            {
                string fileExtension = Path.GetExtension(Consent.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                bool isAllowed = false;
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if(fileExtension == allowedExtensions[i])
                    {
                        isAllowed = true;
                    }
                }

                if (isAllowed)
                {
                    try
                    {
                        account acc = Session["ACCOUNT"] as account;
                        string currentFileName = Path.GetFileNameWithoutExtension(Consent.FileName);
                        string currentFileExtension = Path.GetExtension(Consent.FileName);

                        string filename = string.Format("{0}_Request_{1}{2}", acc.ACC_ID, DateTime.Now.ToString("dd-MM-yy"), currentFileExtension);
                        Consent.SaveAs(Server.MapPath("~/Uploads/") + filename);

                        request_survey_form rq = new request_survey_form();
                        rq.lname = LName.Text;
                        rq.fname = FName.Text;
                        rq.mname = MName.Text;
                        rq.gender = Gender.SelectedValue;
                        rq.dob = DOB.Text;
                        rq.bloodtype = Bloodtype.SelectedValue;
                        rq.city = City.Text;
                        rq.street = Street.Text;
                        rq.province = Province.Text;
                        rq.barangay = Baranggay.Text;
                        rq.zip = Zip.Text;
                        rq.homenum = Home.Text;
                        rq.mobilenum = Mobile.Text;
                        rq.email = Email.Text;
               
                        rq.demand_date = Demand_date.Text;
                        rq.no_blood = No_blood.Text;

                        user_info ua = Session["USER_INFO"] as user_info;

                        blood_request br = new blood_request();
                        br.BREQ_JSON_SURVEY_FORM = JsonConvert.SerializeObject(rq);
                        br.BREQ_CONSENT = filename;
                        br.BREQ_UACC_ID = ua.UI_ID;

                        br.BREQ_DEMAND_DATE = Demand_date.Text;
                        br.BREQ_BLOOD_TYPE = Bloodtype.Text;
                        br.BREQ_NO_BLOOD = No_blood.Text;

                        if (db.InsertBloodrequest(br))
                        {
                            string query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('User ', UI_FNAME, ' ', UI_LNAME, ' Submitted Blood Request'), {0}, '{1}' from user_info
                                            where UI_ID={2};", ua.UI_ID, "User", ua.UI_ID);
                            bool logs = db.InsertToUserLogs(query);

                            //Send Notification
                            string sbj = string.Format("User {0} submitted a Blood Request", ua.UI_ID);
                            string msg = MySqlHelper.EscapeString(string.Format(@"User {0} ( {1} {2} ) Submitted Blood Request.", ua.UI_ID, ua.UI_FNAME, ua.UI_LNAME));
                            query = string.Format(@"insert into notifications(NTF_SUBJECT, NTF_MESSAGE, NTF_RECEIVER_ID, NTF_SENDER_ID) 
                                                values('{0}', '{1}', 1, {2})", sbj, msg, ua.UI_ID);
                            if (!db.InsertToNotification(query))
                            {
                                Debug.Print("Notification was not sent.");
                            }

                            //Successfullu Inserted
                            Response.Write("<script>alert('Successfully Submitted Blood Request Form and is Pending for approval.')</script>");
                            Server.Transfer("~/USER_REQUEST_A_BLOOD.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('You have already made a request. Wait till the process is completed.')</script>");

                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Print("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
                    }
                }
                else
                {
                    Response.Write("<script>alert('Uploaded File is Invalid. Should be image type (jpg, jpeg, png, gif).')</script>");
                }


            }



            

        }

        private void PopulateFormInputs()
        {
            blood_request br = Session["BloodRequest"] as blood_request;
            request_survey_form rq = JsonConvert.DeserializeObject<request_survey_form>(br.BREQ_JSON_SURVEY_FORM);

            Option.Style.Add("display", "");
            Survey.Style.Add("display", "");

            LName.Text = rq.lname;
            FName.Text = rq.fname;
            MName.Text = rq.mname;
            Gender.SelectedValue = rq.gender;
            DOB.Text = rq.dob;
            Bloodtype.SelectedValue = rq.bloodtype;
            City.Text = rq.city;
            Street.Text = rq.street;
            Province.Text = rq.province;
            Baranggay.Text = rq.barangay;
            Zip.Text = rq.zip;
            Home.Text = rq.homenum;
            Mobile.Text = rq.mobilenum;
            Email.Text = rq.email;
            No_blood.Text = rq.no_blood;
            Demand_date.Text = rq.demand_date;
            Debug.Print("~/Uploads/" + br.BREQ_CONSENT);
            DoctorsConsent.ImageUrl = "~/Uploads/" + br.BREQ_CONSENT;

            Session["BREQ_ID"] = br.BREQ_ID;
            Session["Upload"] = br.BREQ_CONSENT;

            UpdateSurvey.Visible = (!br.BREQ_SURVEY_STATUS && br.BREQ_REQ_STATUS) ? true : false;

            DisableInputs();
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
            Province.Enabled = false;
            Baranggay.Enabled = false;
            Zip.Enabled = false;
            Home.Enabled = false;
            Mobile.Enabled = false;
            Email.Enabled = false;
            No_blood.Enabled = false;
            Demand_date.Enabled = false;

            BackButton.Visible = true;
            SubmitSurvey.Visible = false;
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USER_REQUEST_A_BLOOD.aspx");
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
            Server.Transfer("~/Default.aspx");
        }

        protected void UserRd_CheckedChanged(object sender, EventArgs e)
        {
            if(UserRd.SelectedValue == "1")
            {
                Option.Style.Add("display", "");
                Survey.Style.Add("display", "");
                PopulateFormInputsUserYes();
            }
            else
            {
                Option.Style.Add("display", "");
                Survey.Style.Add("display", "");
                ClearInput();
            }
        }

        private void PopulateFormInputsUserYes()
        {
            account acc = Session["ACCOUNT"] as account;
            user_info ui = Session["USER_INFO"] as user_info;
            user_info_address ua = JsonConvert.DeserializeObject<user_info_address>(ui.UI_ADDRESS);


            Option.Style.Add("display", "");
            Survey.Style.Add("display", "none");

            LName.Text = ui.UI_LNAME;
            FName.Text = ui.UI_FNAME;
            MName.Text = ui.UI_MNAME;
            Gender.SelectedValue = ui.UI_GENDER ? "1" : "0";
            DOB.Text = ui.UI_DOB;
            Bloodtype.SelectedValue = ui.UI_BTYPE;
            City.Text = ua.city;
            Street.Text = ua.street;
            Province.Text = ua.province;
            Baranggay.Text = ua.baranggay;
            Zip.Text = ua.zip;
            Home.Text = ui.UI_HOME;
            Mobile.Text = ui.UI_MOBILE;
            Email.Text = acc.ACC_EMAIL;

        }
        private void ClearInput()
        {
            LName.Text = "";
            FName.Text = "";
            MName.Text = "";
            Gender.SelectedValue = null;
            DOB.Text = "";
            Bloodtype.SelectedValue = "";
            City.Text = "";
            Street.Text = "";
            Province.Text = "";
            Baranggay.Text = "";
            Zip.Text = "";
            Home.Text = "";
            Mobile.Text = "";
            Email.Text = "";
            Demand_date.Text = "";
            No_blood.Text = "";
        }

        protected void UpdateSurvey_Click(object sender, EventArgs e)
        {
            EnableInputs();

            Option.Style.Add("display", "none");
            Survey.Style.Add("display", "");
            DConsent.Style.Add("display", "none");
            Upload.Style.Add("display", "");


            UpdateSurvey.Visible = false;
            UpdateBtn.Visible = true;
            Cancel.Visible = true;
        }

        private void EnableInputs()
        {
            LName.Enabled = true;
            FName.Enabled = true;
            MName.Enabled = true;
            DOB.Enabled = true;
            Bloodtype.Enabled = true;
            Gender.Enabled = true;
            Street.Enabled = true;
            Province.Enabled = true;
            Baranggay.Enabled = true;
            Zip.Enabled = true;
            Home.Enabled = true;
            Mobile.Enabled = true;
            Email.Enabled = true;
            No_blood.Enabled = true;
            Demand_date.Enabled = true;
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {

            bool allowed = true;
            bool isNew = false;
            string filename = Session["Upload"] as string;
            if (Consent.HasFile)
            {
                string fileExtension = Path.GetExtension(Consent.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                bool isAllowed = false;
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        isAllowed = true;
                    }
                }

                if (isAllowed)
                {
                    try
                    {
                        account acc = Session["ACCOUNT"] as account;
                        string currentFileName = Path.GetFileNameWithoutExtension(Consent.FileName);
                        string currentFileExtension = Path.GetExtension(Consent.FileName);

                        filename = "";
                        filename = string.Format("{0}_Request_{1}{2}", acc.ACC_ID, DateTime.Now.ToString("dd-MM-yy"), currentFileExtension);
                        Consent.SaveAs(Server.MapPath("~/Uploads/") + filename);
                        isNew = true;
                        
                    }
                    catch (Exception ex)
                    {
                        Debug.Print("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
                    }
                }
                else
                {
                    allowed = false;
                    Response.Write("<script>alert('Uploaded File is Invalid. Should be image type (jpg, jpeg, png, gif).')</script>");
                }
            }
                

            if(allowed)
            {
                request_survey_form rq = new request_survey_form();
                rq.lname = LName.Text;
                rq.fname = FName.Text;
                rq.mname = MName.Text;
                rq.gender = Gender.SelectedValue;
                rq.dob = DOB.Text;
                rq.bloodtype = Bloodtype.SelectedValue;
                rq.city = City.Text;
                rq.street = Street.Text;
                rq.province = Province.Text;
                rq.barangay = Baranggay.Text;
                rq.zip = Zip.Text;
                rq.homenum = Home.Text;
                rq.mobilenum = Mobile.Text;
                rq.email = Email.Text;

                rq.demand_date = Demand_date.Text;
                rq.no_blood = No_blood.Text;

                user_info ua = Session["USER_INFO"] as user_info;

                blood_request br = new blood_request();
                br.BREQ_JSON_SURVEY_FORM = JsonConvert.SerializeObject(rq);
                FileInfo f = new FileInfo("~/Uploads/" + Session["Upload"].ToString());
                if(f.Exists)
                {
                    f.Delete();
                }
                br.BREQ_ID = Session["BREQ_ID"] as string;
                br.BREQ_CONSENT = filename;
                br.BREQ_UACC_ID = ua.UI_ID;

                br.BREQ_DEMAND_DATE = Demand_date.Text;
                br.BREQ_BLOOD_TYPE = Bloodtype.Text;
                br.BREQ_NO_BLOOD = No_blood.Text;

                if (db.UpdateBloodRequest(br))
                {
                    string query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('User ', UI_FNAME, ' ', UI_LNAME, ' Updated Blood Request ( {0} )'), {1}, '{2}' from user_info
                                            where UI_ID={3};", br.BREQ_ID, ua.UI_ID, "User", ua.UI_ID);
                    db.InsertToUserLogs(query);

                    

                    //Successfullu Inserted
                    Response.Write("<script>alert('Successfully Updated Blood Request Form and is Pending for approval.')</script>");
                    Server.Transfer("~/USER_REQUEST_A_BLOOD.aspx");
                }
                else
                {
                    Response.Write("<script>alert('You have already made a request. Wait till the process is completed.')</script>");

                }
            }



        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            PopulateFormInputs();


            Option.Style.Add("display", "none");
            Survey.Style.Add("display", "");
            Upload.Style.Add("display", "none");
            DConsent.Style.Add("display", "");

            UpdateBtn.Visible = false;
            Cancel.Visible = false;
        }
    }
}