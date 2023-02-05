using LifePoints.Admin.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifePoints.Admin
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
        private void PopulateFormInputs()
        {
            blood_request br = Session["BloodRequest"] as blood_request;
            LifePoints.Database.request_survey_form rq = JsonConvert.DeserializeObject<LifePoints.Database.request_survey_form>(br.BREQ_JSON_SURVEY_FORM);
            Debug.Print(br.BREQ_JSON_SURVEY_FORM);

            LName.Text = rq.lname;
            FName.Text = rq.fname;
            MName.Text = rq.mname;
            Gender.SelectedValue = rq.gender;
            DOB.Text = rq.dob;
          
            City.Text = rq.city;
            Street.Text = rq.street;
            Province.Text = rq.province;
            Baranggay.Text = rq.barangay;
            Zip.Text = rq.zip;
            Home.Text = rq.homenum;
            Mobile.Text = rq.mobilenum;
            Email.Text = rq.email;


            Bloodtype.SelectedValue = rq.bloodtype;
            No_blood.Text = rq.no_blood;
            Demand_date.Text = rq.demand_date;

            Debug.Print("~/Uploads/" + br.BREQ_CONSENT);
            DoctorsConsent.ImageUrl = "~/Uploads/" + br.BREQ_CONSENT;

            DisableInputs();

            if (!br.BREQ_REQ_STATUS)
            {
                SurveyGroup.Style.Add("display", "none");
                BloodGroup.Style.Add("display", "none");
            }
            else if (!br.BREQ_SURVEY_STATUS)
            {
                SurveyGroup.Style.Add("display", "");
                BloodGroup.Style.Add("display", "none");
            }
            else if (!br.BREQ_BLOOD_STATUS)
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
            Response.Redirect("~/Admin/BB_BloodTransaction.aspx");
        }

       
        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {

            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("~/Default.aspx");
        }
    }
}