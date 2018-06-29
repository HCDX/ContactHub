using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;
using System.Threading;
using System.Web.Mail;

using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;

using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using System.Globalization;

using System.Net;
using System.Drawing;
using IMDLL;

public partial class Login : System.Web.UI.Page
{
    ModulePageAccess objModulePageAccess = new ModulePageAccess();
    Users objUsers = new Users();
    IMDLL.Profile objProfile = new Profile();
    clsEmail objEmail = new clsEmail();
    clsVariables objVariables = new clsVariables();

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void BtnLogin_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(txtUsername.Text) == true || string.IsNullOrEmpty(txtPassword.Text) == true)
        {

            errorSummaryLiteral.Text = "Kindly login with your username and password";
            errorSummaryBox.Visible = true;
        }

        else
        {
            DataTable Dt = objUsers.GetUsers(" where  Username ='" + txtUsername.Text + "'  and  UserPassword ='" + txtPassword.Text + "' AND UserActif=1");
            if (Dt.Rows.Count > 0)
            {
                Session[RunningCache.UserID] = Dt.Rows[0]["UserID"].ToString();
                Session[RunningCache.UserFullName] = Dt.Rows[0]["UserFullName"].ToString();
                Session[RunningCache.UserLogin] = Dt.Rows[0]["UserName"].ToString();
                Session[RunningCache.UserProfile] = Dt.Rows[0]["UserProfile"].ToString();

                //Get the profile of the user
                DataTable dtProfile = objProfile.GetProfile(" where  ProfileID =" + Session[RunningCache.UserProfile].ToString());
                if (dtProfile.Rows.Count > 0)
                {

                    Session[RunningCache.ProfileName] = dtProfile.Rows[0]["ProfileName"].ToString();
                    DataTable DtModulePageAccess = objModulePageAccess.GetModulePageAccess(" where ProfileID=" + Session[RunningCache.UserProfile].ToString());
                    if (DtModulePageAccess.Rows.Count > 0)
                    {
                        Session[RunningCache.DtModulePageAccess] = DtModulePageAccess;
                        Response.Redirect("default.aspx", false);
                    }
                    else
                    {
                        errorSummaryLiteral.Text = "You don't have the profile to access this page, please contact your administrator";
                        errorSummaryBox.Visible = true;
                        Response.Redirect("Register.aspx", false);
                    }
                }
                else
                {
                    errorSummaryLiteral.Text = "You don't have any profile. Please contact your administrator";
                    errorSummaryBox.Visible = true;
                }




            }
            else
            {
                errorSummaryLiteral.Text = "Your username or password is not correct";
                errorSummaryBox.Visible = true;
            }
        }
    }

    
    protected void BtnSubscribe_Click(object sender, EventArgs e)
    {
        Response.Redirect("Subscription.aspx", false);
    }

    protected void BtnForgottenPwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Subscription.aspx", false);
    }

}