using LifePoints.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifePoints
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (Email.CssClass.Contains("is-invalid"))
            {
                Response.Write("<script>alert('Email Address Is Invalid')</script>");
            }
            else if (Password.CssClass.Contains("is-invalid"))
            {
                Response.Write("<script>alert('Password Must be Atleast 8 characters long')</script>");
            }
            else
            {
                string email = Email.Text;
                string pword = Password.Text;

                Database_Connection db = new Database_Connection();

                account acc = db.LoginUser(email, pword);
                if (acc != null)
                {
                    if (acc.ACC_ID != null)
                    {
                        Session["LOGIN"] = true;
                        Session["ACCOUNT"] = acc;

                        if (acc.ACC_TYPE != "3")
                        {
                            switch (acc.ACC_TYPE)
                            {
                                case "1": //Admin
                                    Response.Redirect("~/Admin/BB_Dashboard.aspx");
                                    break;
                                case "2": //BloodBank
                                    Response.Redirect("~/BloodBank/BB_Dashboard.aspx");
                                    break;
                            }
                        }
                        else
                        {
                            user_info ui = db.GetUserInfo(acc.ACC_ID);

                            if (ui != null)
                            {
                                if (ui.UI_ID != null)
                                {
                                    Session["USER_INFO"] = ui; 
                                    
                                    //User
                                    Response.Redirect("~/USER_BLOGPOST.aspx");
                                }
                            }
                        }
                            
                    }
                    else
                    {
                        Response.Write("<script>alert('Email/Password Mismatch. Try Again.')</script>");
                    }
                }

            }
        }

    }
}