using LifePoints.Database;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LifePoints
{
    public partial class USER_REGISTER : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                PopulatreDropDown();
            }
        }

        public void PopulatreDropDown()
        {
            string[] bloodType = new string[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

            Bloodtype.Items.Insert(0, new ListItem("Select Blood Type", ""));
            int i = 1;
            foreach(string type in bloodType)
            {
                Bloodtype.Items.Insert(i++, new ListItem(type, type));
            }

            Gender.Items.Insert(0, new ListItem("Male", "1"));
            Gender.Items.Insert(1, new ListItem("Female", "0"));
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            if(LName.CssClass.Contains("is-invalid") || FName.CssClass.Contains("is-invalid") || MName.CssClass.Contains("is-invalid") || Bloodtype.SelectedIndex == 0 ||
                DOB.CssClass.Contains("is-invalid") || Bloodtype.CssClass.Contains("is-invalid") || Street.CssClass.Contains("is-invalid") ||
                Baranggay.CssClass.Contains("is-invalid") || City.CssClass.Contains("is-invalid") || Province.CssClass.Contains("is-invalid") ||
                Zip.CssClass.Contains("is-invalid") || Home.CssClass.Contains("is-invalid") || Mobile.CssClass.Contains("is-invalid") ||
                Email.CssClass.Contains("is-invalid") || Password.CssClass.Contains("is-invalid") || RepeatPassword.CssClass.Contains("is-invalid"))
            {
                Response.Write("<script>alert('Please make sure all inputs are valid.')</script>");
            }
            else
            {
                Database_Connection db = new Database_Connection();
                //Register Account
                account acc = new account();
                acc.ACC_EMAIL = Email.Text;
                acc.ACC_PASSWORD = Password.Text;
                int res = db.RegisterAccount(acc);
                switch (res)
                {
                    case -1:
                        break;
                    case -2:
                        break;
                    default: //Success
                        acc.ACC_ID = res.ToString();


                        //Insert User Info
                        user_info ui = new user_info();
                        ui.UI_ID = acc.ACC_ID;
                        ui.UI_LNAME = LName.Text;
                        ui.UI_FNAME = FName.Text;
                        ui.UI_MNAME = MName.Text;
                        ui.UI_GENDER = Gender.SelectedValue == "1";
                        ui.UI_DOB = DOB.Text;

                        //Convert Address to Json
                        user_info_address ad = new user_info_address();
                        ad.street = Street.Text;
                        ad.baranggay = Baranggay.Text;
                        ad.city = City.Text;
                        ad.province = Province.Text;
                        ad.zip = Zip.Text;

                        ui.UI_ADDRESS = MySqlHelper.EscapeString(JsonConvert.SerializeObject(ad));

                        if(db.InsertUserInfo(ui))
                        {
                            string query = string.Format(@"insert into activity_logs(ACT_DESCRIPTION, ACT_UACC_ID, ACT_UNAME)
                                            select concat('User ', UI_FNAME, ' ', UI_LNAME, ' Register'), {0}, '{1}' from user_info
                                            where UI_ID={2};", ui.UI_ID, ui.UI_FNAME + " " + ui.UI_LNAME, ui.UI_ID);


                            bool logs = db.InsertToUserLogs(query);
                            if (logs)
                            {
                                Response.Write("<script>alert('Register Successful. Proceed with Login')</script>");
                                Response.Redirect("~/Default.aspx");
                            }
                        }
                        break;
                }
            }
        }

        private bool CheckInput()
        {
            bool res = false;

            res = (DOB.CssClass.Contains("is-invalid"));
            res = (Bloodtype.CssClass.Contains("is-invalid"));
            res = (Street.CssClass.Contains("is-invalid"));
            res = (Baranggay.CssClass.Contains("is-invalid"));
            res = (City.CssClass.Contains("is-invalid"));
            res = (Province.CssClass.Contains("is-invalid"));
            res = (Zip.CssClass.Contains("is-invalid"));
            res = (Home.CssClass.Contains("is-invalid"));
            res = (Mobile.CssClass.Contains("is-invalid"));
            res = (Email.CssClass.Contains("is-invalid"));
            res = (Password.CssClass.Contains("is-invalid"));
            res = (RepeatPassword.CssClass.Contains("is-invalid"));

            return res;
        }
    }
}