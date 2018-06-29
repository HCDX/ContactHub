using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Reflection;
using IMDLL;
using System.Web.Services;
using AjaxControlToolkit;
using System.Diagnostics;
using System.Data.OleDb;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Web.Mail;
using System.Threading;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using System.Net;
using System.Drawing;

public partial class Addusers : System.Web.UI.Page
{
    private DataTable d = new DataTable();
    Users objUsers = new Users();
    IMDLL.Profile objProfile = new Profile();
    clsEmail objEmail = new clsEmail();

    protected static int l;
    private DataTable detail_dt = new DataTable("");
    string Strsearch = "";
    protected static string HDnIDUserID;

    protected void Page_Load(object sender, EventArgs e)
    {
        PagesAccess objPagesAccess = new PagesAccess();
        if (objPagesAccess.CheckAccess(Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath), Session[RunningCache.UserProfile].ToString()) == true)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

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
                    bindUserActif();
                    bindgrid();
                    bindProfile();
                }

                if (this.Edit_global.Visible) this.Edit_global.Visible = false;
                bindgrid();
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                execTimeLit.Text = String.Format("{0:00} minute(s), {1:00} seconde(s), {2:00} milliseconde(s)", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            }
            catch (Exception ex)
            {
                if (this.global_success.Visible) this.global_success.Visible = false;
                this.global_error.Visible = true;
                this.global_error_msg.Text = Mains.Constant.GENERAL_ERR;
            }
        }

    }

    protected void bindgrid()
    {

        try
        {
            this.global_error.Visible = false;
            DataTable Dt = new DataTable();
            if (string.IsNullOrEmpty(Strsearch) == false)
            {

                Dt = objUsers.GetUsers(Strsearch);
                Strsearch = "";
                if (Dt.Rows.Count == 0)
                {
                    no_data.Visible = true;
                    NoDatalbl.Text = "No data found";
                    d.Columns.Clear();
                    d.Rows.Clear();
                    EntGridView.DataSource = d;
                    EntGridView.DataBind();
                }

            }
            else Dt = objUsers.GetUsers(" where UserActif=1 order by  UserFullName");

            l = Dt.Rows.Count;
            EntGridView.DataSource = Dt;
            EntGridView.DataBind();
            EntGridView.Visible = true;
            no_data.Visible = false;
            upCrudGrid.Update();


        }
        catch (Exception ex)
        {
            if (this.global_success.Visible) this.global_success.Visible = false;
            this.global_error.Visible = true;
            this.global_error_msg.Text = Mains.Constant.GENERAL_ERR;
        }
    }

    protected void EntGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Retrieve row
        GridViewRow row = e.Row;
    }

    protected void EntGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            EntGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
        }
    }

    protected void EntGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            bindgrid();
            EntGridView.PageIndex = e.NewPageIndex;
            EntGridView.DataBind();
            upCrudGrid.Update();

        }
        catch (Exception ex)
        {
            EntGridView.Controls.Add(new LiteralControl("An error occured; please try again, " + ex));
        }
    }

    protected void EntGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            int index = Convert.ToInt32(e.CommandArgument) % EntGridView.PageSize;

            string HDnID = ((HiddenField)EntGridView.Rows[index].FindControl("UserID")).Value;
            string UserFullName = ((Label)EntGridView.Rows[index].FindControl("UserFullName")).Text;
            string UserOrganization = ((Label)EntGridView.Rows[index].FindControl("UserOrganization")).Text;
            string UserTitle = ((Label)EntGridView.Rows[index].FindControl("UserTitle")).Text;
            string Username = ((Label)EntGridView.Rows[index].FindControl("Username")).Text;
            string UserMail = ((Label)EntGridView.Rows[index].FindControl("UserMail")).Text;
            string UserPhone = ((Label)EntGridView.Rows[index].FindControl("UserPhone")).Text;
            string UserAddress = ((Label)EntGridView.Rows[index].FindControl("UserAddress")).Text;
            string UserActif = ((Label)EntGridView.Rows[index].FindControl("UserActif")).Text;
            string ProfileName = ((Label)EntGridView.Rows[index].FindControl("ProfileName")).Text;
            string UserNote = ((Label)EntGridView.Rows[index].FindControl("UserNote")).Text;

            //This user is mixing with the User session ID
            HDnIDUserID = HDnID;

            if (e.CommandName.Equals("view"))
            {
                try
                {
                    txtUsername.ReadOnly = true;
                    txtUserMail.ReadOnly = true;

                    txtUserFullName.Text = UserFullName;
                    txtUserOrganization.Text = UserOrganization;
                    txtUserTitle.Text = UserTitle;
                    txtUsername.Text = Username;
                    txtUserMail.Text = UserMail;
                    txtUserPhone.Text = UserPhone;
                    txtUserAddress.Text = UserAddress;

                    DataTable dtActive = objUsers.GetActiveStatus(" where ActiveStatusName='" + UserActif + "'");
                    if (dtActive.Rows.Count > 0) ddlUserActif.SelectedValue = dtActive.Rows[0]["ActiveStatusID"].ToString();

                    DataTable dtProfile = objProfile.GetProfile(" where ProfileName='" + ProfileName + "'");
                    if (dtActive.Rows.Count > 0) ddlProfile.SelectedValue = dtProfile.Rows[0]["ProfileID"].ToString();

                    txtUserNote.Text = UserNote;

                    BtnSave.Text = "Update";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#addEdiModal').modal('show');");
                    sb.Append(@"</script>");
                    ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addEdiModalScript", sb.ToString(), false);
                    upEdit.Update();
                }
                catch (Exception ex)
                {

                }
            }

            if (e.CommandName.Equals("deleting"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
                upDel.Update();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            if (!string.IsNullOrEmpty(txtUserMail.Text.Trim()) && IsValid(txtUserMail.Text.Trim()))
            {
                string motif = string.Empty;

                string strmsg = "";

                objUsers.UserID = 0;

                objUsers.UserFullName = txtUserFullName.Text.Trim();
                objUsers.UserOrganization = txtUserOrganization.Text.Trim();
                objUsers.UserTitle = txtUserTitle.Text.Trim();
                objUsers.Username = txtUsername.Text.Trim();
                objUsers.UserMail = txtUserMail.Text.Trim();
                objUsers.UserPhone = txtUserPhone.Text.Trim();
                objUsers.UserAddress = txtUserAddress.Text.Trim();
                objUsers.UserProfile = int.Parse(ddlProfile.SelectedValue);
                if (int.Parse(ddlUserActif.SelectedValue) == 1)
                {
                    objUsers.UserActif = true;
                }
                else
                {
                    objUsers.UserActif = false;
                }

                string Newpwd = GetUniqueKey(6);
                objUsers.UserPassword = Newpwd;
                objUsers.UserNote = txtUserNote.Text.Trim();
                objUsers.LastModifiedBy = int.Parse(Session[RunningCache.UserID].ToString());
                objUsers.LastModifiedDate = DateTime.Now;

                if (BtnSave.Text.ToUpper() == "Save".ToUpper())
                {
                    DataTable dtUser = objUsers.GetUsers(" where Username='" + txtUsername.Text.Trim()+"'");
                    if (dtUser.Rows.Count > 0)
                    {
                        if (this.global_success.Visible) this.global_success.Visible = false;
                        this.global_error.Visible = true;
                        this.global_error_msg.Text = "This username already exist in our system. Please choose another one.";
                        UpdatePanel.Update();
                    }
                    else
                    {
                        objUsers.SaveUsers(objUsers, out strmsg);
                        if (this.global_error.Visible) this.global_error.Visible = false;
                        this.global_success.Visible = true;
                        this.global_success_msg.Text = "The user is successfully saved into the system.";
                        UpdatePanel.Update();
                        SendEmail(txtUserMail.Text.Trim(), "ContactHub Login Details", txtUserFullName.Text.Trim(), txtUsername.Text.Trim(), objUsers.UserPassword);
                        SendEmail("ble@unhcr.org", "ContactHub Login Details", txtUserFullName.Text.Trim(), txtUsername.Text.Trim(), objUsers.UserPassword);
                    }
                }


                else if (BtnSave.Text.ToUpper() == "UPDATE".ToUpper())
                {
                    objUsers.UserID = int.Parse(HDnIDUserID.ToString());
                    objUsers.AdminUpdateUsers(objUsers, out strmsg);
                    if (this.global_error.Visible) this.global_error.Visible = false;
                    this.global_success.Visible = true;
                    this.global_success_msg.Text = "The user is successfully updated into the system.";
                    UpdatePanel.Update();
                }

                upCrudGrid.Update();
                bindgrid();
            }
            else
            {
                if (this.global_success.Visible) this.global_success.Visible = false;
                this.global_error.Visible = true;
                this.global_error_msg.Text = "Please enter a valid email.";
                UpdatePanel.Update();
            }
        }

        catch (Exception ex)
        {
        }

        sb.Append(@"<script type='text/javascript'>");

        sb.Append("$('#addEdiModal').modal('hide');");

        sb.Append(@"</script>");

        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {

            if (!(objUsers.DeleteUsers(HDnIDUserID.ToString())))
            {
                if (this.global_success.Visible) this.global_success.Visible = false;
                this.global_error.Visible = true;
                this.global_error_msg.Text = Mains.Constant.FAIL_CRUD;
                UpdatePanel.Update();
                upCrudGrid.Update();
                upDel.Update();
                bindgrid();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('hide');");
                sb.Append(@"</script>");
                ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
                return;
            }
            else
            {
                if (this.global_error.Visible) this.global_error.Visible = false;
                this.global_success.Visible = true;
                this.global_success_msg.Text = Mains.Constant.SUCCESS_DELETE;
                UpdatePanel.Update();
                upDel.Update();
                upCrudGrid.Update();
                bindgrid();

            }

        }

        catch (Exception ex)
        {
        }

        sb.Append(@"<script type='text/javascript'>");

        sb.Append("$('#deleteModal').modal('hide');");

        sb.Append(@"</script>");

        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);

    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string search_value = ClsTools.Text_Validator(txtSearch.Text);
            Strsearch = "  WHERE UserActif=1 and UserFullName LIKE  '%" + search_value + "%'    ORDER BY UserFullName";
            bindgrid();
        }

        catch (Exception ex)
        {
        }

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        txtUserFullName.ReadOnly = false;
        txtUserOrganization.ReadOnly = false;
        txtUserTitle.ReadOnly = false;
        txtUsername.ReadOnly = false;
        txtUserMail.ReadOnly = false;
        txtUserPhone.ReadOnly = false;
        txtUserAddress.ReadOnly = false;
        txtUserNote.ReadOnly = false;

        txtUserFullName.Text = string.Empty;
        txtUserOrganization.Text = string.Empty;
        txtUserTitle.Text = string.Empty;
        txtUsername.Text = string.Empty;
        txtUserMail.Text = string.Empty;
        txtUserPhone.Text = string.Empty;
        txtUserAddress.Text = string.Empty;
        ddlUserActif.Text = string.Empty;
        txtUserNote.Text = string.Empty;

        BtnSave.Text = "Save";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addEdiModal').modal('show');");
        sb.Append(@"</script>");
        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addEdiModalScript", sb.ToString(), false);
        upEdit.Update();
    }

    protected void bindUserActif()
    {
        DataTable dt = objUsers.GetActiveStatus("  ");

        DataRow dr = dt.NewRow();
        dt.Rows.InsertAt(dr, 0);
        dt.AcceptChanges();

        ddlUserActif.DataSource = dt;
        ddlUserActif.DataTextField = "ActiveStatusName";
        ddlUserActif.DataValueField = "ActiveStatusID";
        ddlUserActif.DataBind();

    }

    protected void bindProfile()
    {
        DataTable dt = objProfile.GetProfile("  where ProfileName is not null order by ProfileName");

        DataRow dr = dt.NewRow();
        dr[1] = "Choose a Profile";
        dt.Rows.InsertAt(dr, 0);
        dt.AcceptChanges();

        ddlProfile.DataSource = dt;
        ddlProfile.DataTextField = "ProfileName";
        ddlProfile.DataValueField = "ProfileID";
        ddlProfile.DataBind();

    }

    public static string GetUniqueKey(int maxSize)
    {
        char[] chars = new char[62];
        chars = "abcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        data = new byte[maxSize];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(maxSize);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }

    protected void SendEmail(string emailAddress, String Title, string fullname, string username, string password)
    {
        SmtpClient smtp = new SmtpClient();

        string MailTemplate = Server.MapPath("~/EmailTemplates/users.htm");

        string template = string.Empty;
        if (File.Exists(MailTemplate))
        {
            template = File.ReadAllText(MailTemplate);
        }
        template = template.Replace("<%strUserFullName%>", fullname);
        template = template.Replace("<%strUserName%>", username);
        template = template.Replace("<%strPassword%>", password);

        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

        msg.From = new MailAddress("lebbeia@unhcr.org", "Inter-Agency Coordination Lebanon");

        msg.To.Add(emailAddress);
        msg.Subject = Title;

        String logo = Server.MapPath("~/EmailTemplates/images/interagency.jpg");
        System.Net.Mail.AlternateView htmlView = AlternateView.CreateAlternateViewFromString(template, null, "text/html");
        LinkedResource pic1 = new LinkedResource(logo, MediaTypeNames.Image.Jpeg);
        pic1.ContentId = "logo";
        htmlView.LinkedResources.Add(pic1);
        msg.AlternateViews.Add(htmlView);
        objEmail.EmailConfig(template, msg);

    }

    public bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

}