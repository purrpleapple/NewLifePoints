using LifePoints.Database;
using System;
using System.Collections.Generic;
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
                        switch (acc.ACC_TYPE)
                        {
                            case "1": //Admin
                                Response.Write("<script>alert('Admin')</script>");
                                break;
                            case "2": //BloodBank
                                Response.Write("<script>alert('BloodBank')</script>");
                                break;
                            case "3": //User
                                Response.Write("<script>alert('User')</script>");
                                break;
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