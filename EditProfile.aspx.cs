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
using System.Configuration;
using System.Data.Odbc;
using System.Web.Security;
using IMDLL;

public partial class EditProfile : System.Web.UI.Page
{
    public string _userFullName = string.Empty,
                 _userProfile = string.Empty,
                 _userLastLogin = string.Empty,
                StrToday = string.Empty,
                StrMonth = string.Empty;

    private static string usr;

    Users objUsers = new Users();
    AreaInterest objAreaInterest = new AreaInterest();
    SectorInterest objSectorInterest = new SectorInterest();

    protected void Page_Load(object sender, EventArgs e)
    {
        PagesAccess objPagesAccess = new PagesAccess();
        if (objPagesAccess.CheckAccess(Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath), Session[RunningCache.UserProfile].ToString()) == true)
        {
            try
            {
                CultureInfo ci = new CultureInfo("en-GB");

                var format = "dd, MMMM  yyyy";

                ClientScript.GetPostBackEventReference(this, string.Empty);

                if (Request.Form["__EVENTTARGET"] == "Disconnect")
                {
                    Disconnect();
                }

                if (Session[RunningCache.UserLogin] == null)
                {
                    Disconnect();
                }

                if (!IsPostBack)
                {
                    if (HttpContext.Current.Session["AlertOn"] != null)
                    {
                        if ((Boolean)HttpContext.Current.Session["AlertOn"] == true)
                        {
                            if (this.global_error.Visible) this.global_error.Visible = false;
                            this.global_success.Visible = true;
                            this.global_success_msg.Text = Mains.Constant.SUCCESS_INSERT;
                        }
                        HttpContext.Current.Session["AlertOn"] = null;
                    }

                    var ss = Session[RunningCache.UserLogin];
                    if (ss != null)
                    {
                        if (!string.IsNullOrEmpty(Session[RunningCache.UserLogin].ToString()))
                        {
                            usr = Session[RunningCache.UserLogin].ToString();
                            _userFullName = Session[RunningCache.UserFullName].ToString();
                            _userProfile = Session[RunningCache.ProfileName].ToString();
                            filldata();
                        }
                    }
                    else
                    {
                        Disconnect();
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }

    }
    private void Disconnect()
    {
        try
        {
            FormsAuthentication.SignOut();
            usr = string.Empty;
            Session[RunningCache.UserLogin] = null;
            HttpContext.Current.Response.Redirect("login.aspx", false);

        }
        catch (Exception ex)
        {
        }


    }

    protected void filldata()
    {
        DataTable dt = objUsers.GetUsers(" where UserID=" + Session[RunningCache.UserID].ToString());
        if (dt.Rows.Count > 0)
        {
            txtUserFullName.Text = dt.Rows[0]["UserFullName"].ToString();
            txtUserOrganization.Text = dt.Rows[0]["UserOrganization"].ToString();
            txtUserTitle.Text = dt.Rows[0]["UserTitle"].ToString();
            txtUsername.Text = dt.Rows[0]["Username"].ToString();
            txtUserMail.Text = dt.Rows[0]["UserMail"].ToString();
            txtUserPhone.Text = dt.Rows[0]["UserPhone"].ToString();
            txtUserAddress.Text = dt.Rows[0]["UserAddress"].ToString();
            txtProfile.Text = dt.Rows[0]["ProfileName"].ToString();
            txtUserNote.Text = dt.Rows[0]["UserNote"].ToString();

        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {


            string strmsg = "";

            objUsers.UserFullName = txtUserFullName.Text.Trim();
            objUsers.UserOrganization = txtUserOrganization.Text.Trim();
            objUsers.UserTitle = txtUserTitle.Text.Trim();
            objUsers.UserMail = txtUserMail.Text.Trim();
            objUsers.UserPhone = txtUserPhone.Text.Trim();
            objUsers.UserAddress = txtUserAddress.Text.Trim();
            objUsers.UserNote = txtUserNote.Text.Trim();
            objUsers.LastModifiedBy = int.Parse(Session[RunningCache.UserID].ToString());
            objUsers.LastModifiedDate = DateTime.Now;

            objUsers.UserID = int.Parse(Session[RunningCache.UserID].ToString());
            objUsers.UserUpdateUsers(objUsers, out strmsg);
            this.global_success.Visible = true;
            this.global_success_msg.Text = "You have successfully updated your information into the system.";
            UpdatePanel.Update();

        }

        catch (Exception ex)
        {
        }


    }


    protected void BtnChangePwd_Click(object sender, EventArgs e)
    {
        if (txtUserPasswordNew.Text.Trim() == txtUserPasswordConfirm.Text.Trim())
        {
            objUsers.CustomUpdateUsers(" UserPassword='" + txtUserPasswordConfirm.Text.Trim() + "' WHERE UserID=" + Session[RunningCache.UserID].ToString());
        }
        else
        {
            if (this.global_success.Visible) this.global_error.Visible = false;
            this.global_error.Visible = true;
            this.global_error_msg.Text = "Please type the same password.";
        }
    }

   

}