using LifePoints.Admin.Database;
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

namespace LifePoints.Admin
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


        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("BB_BloodTransaction.aspx");
        }

        

        protected void BtnLogout_ServerClick(object sender, EventArgs e)
        {

            Session.Clear();
            Session.RemoveAll();
            Server.Transfer("~/Default.aspx");
        }



    }
}