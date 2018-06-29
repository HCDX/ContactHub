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
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Reflection;
using System.Web.Services;
using AjaxControlToolkit;
using System.Diagnostics;
using System.Threading.Tasks;


public partial class Subscription : System.Web.UI.Page
{
    public string _userFullName = string.Empty,
                 _userProfile = string.Empty,
                 _userLastLogin = string.Empty,
                StrToday = string.Empty,
                StrMonth = string.Empty;

    private DataTable d = new DataTable();
    SectorInterest objSectorInterest = new SectorInterest();
    AreaInterest objAreaInterest = new AreaInterest();
    Interest objInterest = new Interest();
    Subscriber objSubscriber = new Subscriber();
    clsEmail objEmail = new clsEmail();
    FocalPoints objFocalPoints = new FocalPoints();
    Users objUsers = new Users();

    protected static int l;
    private DataTable detail_dt = new DataTable("");
    string Strsearch = "";


    private static string usr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CultureInfo ci = new CultureInfo("en-GB");
            l = 0;
            var format = "dd, MMMM  yyyy";
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

                bindSectorInterest();
                bindAreaInterest();

                if (string.IsNullOrEmpty(Request.QueryString["vid"]) == false)
                {
                    Session[RunningCache.SubscriberEmail] = Request.QueryString["vid"];
                    Session[RunningCache.SubscriberCode] = Request.QueryString["strflags"];
                    filldataSubscriber(Session[RunningCache.SubscriberEmail].ToString(), Session[RunningCache.SubscriberCode].ToString());

                }


            }

            StrMonth = DateTime.Now.ToString("MMMM yyyy", ci).ToUpper();
            StrToday = DateTime.Now.ToString(format, ci).ToUpper();
            if (this.Edit_global.Visible) this.Edit_global.Visible = false;

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            execTimeLit.Text = String.Format("{0:00} minute(s), {1:00} seconde(s), {2:00} milliseconde(s)", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

        }
        catch (Exception ex)
        {
            if (this.global_success.Visible) this.global_success.Visible = false;
            this.global_error.Visible = true;
            this.global_error_msg.Text = ex.ToString();
        }
    }

    protected void bindgrid()
    {
        try
        {
            DataTable Dt = new DataTable();
            Dt = objSubscriber.GetSubscriber(" where SubscriberEmail='" + txtSubEmail.Text.Trim() + "'");
            if (Dt.Rows.Count > 0)
            {
                l = Dt.Rows.Count;
                EntGridView.DataSource = Dt;
                EntGridView.DataBind();
                EntGridView.Visible = true;
                no_data.Visible = false;

            }
            else
            {
                EntGridView.Visible = false;
                no_data.Visible = true;
                upCrudGrid.Update();
            }


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
            string HDnID = ((HiddenField)EntGridView.Rows[index].FindControl("InterestID")).Value;
            string SectorInterestName = ((Label)EntGridView.Rows[index].FindControl("SectorInterestName")).Text;
            string AreaInterestName = ((Label)EntGridView.Rows[index].FindControl("AreaInterestName")).Text;

            Session[RunningCache.InterestID] = HDnID;

            if (e.CommandName.Equals("view"))
            {
                try
                {
                    bindSectorInterest();
                    bindAreaInterest();
                    DataTable dtInterest = objInterest.GetInterest(" where InterestID='" + HDnID + "'");
                    ddlSectorInterest1.SelectedValue = dtInterest.Rows[0]["InterestSector"].ToString();
                    ddlAreaInterest1.SelectedValue = dtInterest.Rows[0]["InterestArea"].ToString();
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
                try
                {

                    if (!(objInterest.DeleteInterest(Session[RunningCache.InterestID].ToString())))
                    {
                        if (this.global_success.Visible) this.global_success.Visible = false;
                        this.global_error.Visible = true;
                        this.global_error_msg.Text = Mains.Constant.FAIL_CRUD;
                        UpdAdd.Update();
                        upCrudGrid.Update();
                        upDel.Update();
                        bindgrid();

                        return;
                    }

                    else
                    {
                        if (this.global_error.Visible) this.global_error.Visible = false;
                        this.global_success.Visible = true;
                        this.global_success_msg.Text = Mains.Constant.SUCCESS_DELETE;
                        UpdAdd.Update();
                        upDel.Update();
                        upCrudGrid.Update();
                        bindgrid();

                    }

                }

                catch (Exception ex)
                {
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {

            if (!(objInterest.DeleteInterest(Session[RunningCache.InterestID].ToString())))
            {
                if (this.global_success.Visible) this.global_success.Visible = false;
                this.global_error.Visible = true;
                this.global_error_msg.Text = Mains.Constant.FAIL_CRUD;
                UpdAdd.Update();
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
                UpdAdd.Update();
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

    protected void BtnSubscribe_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtSubEmail.Text.Trim()) == false)
            {
                if (IsValid(txtSubEmail.Text.Trim()))
                {
                    string strmsg = "";
                    objSubscriber.SubscriberID = 0;
                    objSubscriber.SubscriberFullName = txtSubFullName.Text;
                    objSubscriber.SubscriberEmail = txtSubEmail.Text.Trim();
                    objSubscriber.SubscriberTitle = txtSubTitle.Text;
                    objSubscriber.SubscriberOrganization = txtSubOrganization.Value;
                    objSubscriber.SubscriberContact = txtSubContact.Text;
                    objSubscriber.SubscriberInterest = 0;
                    objSubscriber.LastModifiedBy = txtSubEmail.Text.Trim();
                    objSubscriber.LastModifiedDate = DateTime.Now;
                    objSubscriber.SubscriberAddedDate = DateTime.Now;
                    objSubscriber.SubscriberCode = GetUniqueKey(6);

                    //this datatable and GetSubscriberRaw will list all the email in the raw Subscriver table
                    if (BtnSubscribe.Text.ToUpper().Trim() == "Subscribe".ToUpper().Trim())
                    {
                        DataTable dtSubscriber = objSubscriber.GetSubscriberRaw(" where SubscriberEmail='" + txtSubEmail.Text.Trim() + "'");
                        if (dtSubscriber.Rows.Count > 0)
                            objSubscriber.CustomUpdateSubscriber(" SubscriberCode='" + objSubscriber.SubscriberCode + "' WHERE SubscriberEmail='" + txtSubEmail.Text.Trim() + "'");
                        else
                            Session[RunningCache.SubscriberID] = objSubscriber.SaveSubscriber(objSubscriber, out strmsg);

                        string strURL = "use your hosting web folder/subscription.aspx?from=fu&vid=" + txtSubEmail.Text.Trim() + "&strflags=" + objSubscriber.SubscriberCode;
                        SendEmail(txtSubEmail.Text.Trim(), "ContactHub Subscription", txtSubFullName.Text.Trim(), strURL);

                        if (this.subscription_error.Visible) this.subscription_error.Visible = false;
                        this.subcription_success.Visible = true;
                        this.subcription_success_msg.Text = "An email has been sent to " + txtSubEmail.Text.Trim() + ". Please browse the link to continue.";
                    }

                    else if (BtnSubscribe.Text.ToUpper() == "Confirm".ToUpper())
                    {
                        objSubscriber.SubscriberID = int.Parse(Session[RunningCache.SubscriberID].ToString());
                        objSubscriber.UpdateSubscriberConfirm(objSubscriber, out strmsg);
                        if (this.global_success.Visible) this.global_success.Visible = false;
                        this.subcription_success.Visible = true;
                        this.subcription_success_msg.Text = "Information successfully updated for " + txtSubFullName.Text;
                        UpdAdd.Update();
                        DivGrid.Visible = true;

                        foreach (GridViewRow gvrow in EntGridView.Rows)
                        {
                            try
                            {
                                Label InterestSector = (Label)gvrow.FindControl("SectorInterestName");
                                Label InterestArea = (Label)gvrow.FindControl("AreaInterestName");
                                HiddenField InterestID = (HiddenField)gvrow.FindControl("InterestID");
                                HiddenField SectorID = (HiddenField)gvrow.FindControl("InterestSector");
                                HiddenField AreaID = (HiddenField)gvrow.FindControl("InterestArea");

                                Session[RunningCache.InterestID] = Convert.ToInt32(InterestID.Value);
                                objInterest.CustomUpdateInterest(" InterestStatus=1 WHERE InterestID=" + Session[RunningCache.InterestID].ToString());

                                DataTable dtInterestCode = objInterest.GetInterest(" where InterestID=" + Convert.ToInt32(InterestID.Value));
                                string InterestCode = dtInterestCode.Rows[0]["InterestCode"].ToString();

                                //Getting all the focal point list 
                                DataTable dtFocalPoint = objFocalPoints.GetFocalPoints(" where FocalPointsSector=" + Convert.ToInt32(SectorID.Value) + " and FocalPointsArea=" + Convert.ToInt32(AreaID.Value));
                                for (int i = 0; i < dtFocalPoint.Rows.Count; i++)
                                {
                                    if (dtFocalPoint.Rows[i]["FocalPointsNotification"].ToString() == "True")
                                    {
                                        //Send email to distinct Focal Points
                                        DataTable dtUser = objUsers.GetUsers(" where UserID=" + dtFocalPoint.Rows[i]["FocalPointsUser"].ToString());
                                        string subscriber;
                                        if (string.IsNullOrEmpty(txtSubFullName.Text.Trim()))
                                            subscriber = txtSubEmail.Text.Trim();
                                        else subscriber = txtSubEmail.Text.Trim() + " (" + txtSubFullName.Text.Trim() + ")";

                                        string strURL = "use your hosting web folder/unsubscription.aspx?from=fu&vid=" + InterestCode;
                                        SendNotification(dtUser.Rows[0]["UserMail"].ToString().Trim(), "ContactHub Notification Email", dtFocalPoint.Rows[i]["UserFullName"].ToString(), subscriber, strURL, dtFocalPoint.Rows[i]["SectorInterestName"].ToString(), dtFocalPoint.Rows[i]["AreaInterestName"].ToString());
                                    }

                                }

                            }
                            catch (Exception ex)
                            {
                                if (this.subscription_error.Visible) this.subscription_error.Visible = true;
                                this.subcription_success.Visible = false;
                                this.subscription_error_msg.Text = Mains.Constant.INVALID_EMAIL;
                                UpdAdd.Update();
                            }
                        }


                    }

                }
                else
                {
                    if (this.subcription_success.Visible) this.subcription_success.Visible = false;
                    this.subscription_error.Visible = true;
                    this.subscription_error_msg.Text = "Please enter a valid email address.";
                }
            }
            else
            {
                if (this.subcription_success.Visible) this.subcription_success.Visible = false;
                this.subscription_error.Visible = true;
                this.subscription_error_msg.Text = "Please enter an email address.";
            }

        }

        catch (Exception ex)
        {
        }

    }

    protected void bindSectorInterest()
    {
        DataTable dt = objSectorInterest.GetSectorInterest("  order by SectorInterestName ");
        DataRow dr = dt.NewRow();
        dr[1] = "Choose a Sector";
        dt.Rows.InsertAt(dr, 0);
        dt.AcceptChanges();

        ddlSectorInterest.DataSource = dt;
        ddlSectorInterest.DataTextField = "SectorInterestName";
        ddlSectorInterest.DataValueField = "SectorInterestID";
        ddlSectorInterest.DataBind();

    }

    protected void bindAreaInterest()
    {
        DataTable dt = objAreaInterest.GetAreaInterest("  order by AreaInterestName ");

        DataRow dr = dt.NewRow();
        dr[1] = "Choose an Area";
        dt.Rows.InsertAt(dr, 0);
        dt.AcceptChanges();

        ddlAreaInterest.DataSource = dt;
        ddlAreaInterest.DataTextField = "AreaInterestName";
        ddlAreaInterest.DataValueField = "AreaInterestID";
        ddlAreaInterest.DataBind();

    }

    protected void filldataSubscriber(string email, string code)
    {

        DataTable dt = objSubscriber.GetSubscriberRaw("where SubscriberEmail ='" + email + "' and SubscriberCode='" + code + "'");
        if (dt.Rows.Count > 0)
        {
            Session[RunningCache.SubscriberID] = dt.Rows[0]["SubscriberID"].ToString();
            txtSubFullName.Text = dt.Rows[0]["SubscriberFullName"].ToString();
            txtSubEmail.Text = dt.Rows[0]["SubscriberEmail"].ToString();
            txtSubTitle.Text = dt.Rows[0]["SubscriberTitle"].ToString();
            txtSubOrganization.Value = dt.Rows[0]["SubscriberOrganization"].ToString();
            txtSubContact.Text = dt.Rows[0]["SubscriberContact"].ToString();
            BtnSubscribe.Text = "Confirm";

            DivGrid.Visible = true;
            bindgrid();
            txtSubEmail.ReadOnly = true;
            Session[RunningCache.UserID] = Session[RunningCache.SubscriberID];
        }
        else Response.Redirect("subscription.aspx");

    }

    protected void BtnAddInterest_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.subscription_error.Visible) this.subscription_error.Visible = false;

            if (string.IsNullOrEmpty(ddlAreaInterest.SelectedValue) || string.IsNullOrEmpty(ddlSectorInterest.SelectedValue))
            {
                if (this.global_success.Visible) this.global_success.Visible = false;
                this.global_error.Visible = true;
                this.global_error_msg.Text = "Please select a Sector and an Area";
            }
            else
            {
                DataTable dtSubscriber = objSubscriber.GetSubscriber(" where SubscriberEmail='" + txtSubEmail.Text + "' and InterestSector=" + ddlSectorInterest.SelectedValue + " and InterestArea=" + ddlAreaInterest.SelectedValue);
                if (dtSubscriber.Rows.Count > 0)
                {
                    if (this.global_success.Visible) this.global_success.Visible = false;
                    this.global_error.Visible = true;
                    this.global_error_msg.Text = dtSubscriber.Rows[0]["SectorInterestName"].ToString() + " - " + dtSubscriber.Rows[0]["AreaInterestName"].ToString() + " already exist in your interest list";
                    UpdAdd.Update();
                    upEdit.Update();
                }
                else
                {
                    string strmsg = string.Empty;
                    objInterest.InterestID = 0;
                    objInterest.InterestSector = int.Parse(ddlSectorInterest.SelectedValue);
                    objInterest.InterestArea = int.Parse(ddlAreaInterest.SelectedValue);
                    objInterest.InterestSubscriber = int.Parse(Session[RunningCache.SubscriberID].ToString());
                    objInterest.InterestStatus = true;
                    objInterest.InterestCode = GetUniqueKey(6);
                    objInterest.LastModifiedBy = int.Parse(Session[RunningCache.UserID].ToString());
                    objInterest.LastModifiedDate = DateTime.Now;

                    Session[RunningCache.InterestID] = objInterest.SaveInterest(objInterest, out strmsg);

                    DataTable dt = objInterest.GetInterest(" where InterestID=" + int.Parse(Session[RunningCache.InterestID].ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        if (this.global_error.Visible) this.global_error.Visible = false;
                        this.global_success.Visible = true;
                        this.global_success_msg.Text = dt.Rows[0]["SectorInterestName"].ToString() + " - " + dt.Rows[0]["AreaInterestName"].ToString() + " has been added to your interest list";
                    }

                    UpdAdd.Update();
                    upEdit.Update();

                    upCrudGrid.Update();
                    bindgrid();

                    filldataSubscriber(dtSubscriber.Rows[0]["SubscriberEmail"].ToString(), dtSubscriber.Rows[0]["SubscriberCode"].ToString());

                }
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
            DataTable dtSubscriber = objSubscriber.GetSubscriber(" where SubscriberEmail=" + txtSubEmail.Text + " and InterestStatus=true and InterestSector=" + ddlSectorInterest.SelectedValue + " and InterestArea=" + ddlAreaInterest.SelectedValue);
            if (dtSubscriber.Rows.Count > 0)
            {
                if (this.global_error.Visible) this.global_error.Visible = false;
                this.global_success.Visible = true;
                this.global_success_msg.Text = "This subscription already exist in the Contact List.";
                UpdAdd.Update();
                upEdit.Update();
            }
            else
            {
                string motif = string.Empty;

                string strmsg = "";
                objInterest.InterestID = 0;
                objInterest.InterestSector = int.Parse(ddlSectorInterest.SelectedValue);
                objInterest.InterestArea = int.Parse(ddlAreaInterest.SelectedValue);
                objInterest.InterestSubscriber = int.Parse(Session[RunningCache.SubscriberID].ToString());
                objInterest.LastModifiedBy = 0;
                objInterest.LastModifiedDate = DateTime.Now;
                if (BtnSave.Text.ToUpper() == "Save".ToUpper())
                {
                    objInterest.SaveInterest(objInterest, out strmsg);
                    if (this.global_error.Visible) this.global_error.Visible = false;
                    this.global_success.Visible = true;
                    this.global_success_msg.Text = "The Interest is successfully saved into the system.";
                    UpdAdd.Update();
                }


                else if (BtnSave.Text.ToUpper() == "UPDATE".ToUpper())
                {
                    objInterest.InterestID = int.Parse(Session[RunningCache.InterestID].ToString());
                    objInterest.UpdateInterest(objInterest, out strmsg);
                    this.global_success.Visible = true;
                    this.global_success_msg.Text = "The Interest is successfully updated into the system.";
                    UpdAdd.Update();
                }
            }

            upCrudGrid.Update();
            bindgrid();
            filldataSubscriber(dtSubscriber.Rows[0]["SubscriberEmail"].ToString(), dtSubscriber.Rows[0]["SubscriberCode"].ToString());

        }

        catch (Exception ex)
        {
        }


        sb.Append(@"<script type='text/javascript'>");

        sb.Append("$('#addEdiModal').modal('hide');");

        sb.Append(@"</script>");

        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

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

    protected void SendEmail(string emailAddress, String Title, string subscriber, string strURL)
    {
        SmtpClient smtp = new SmtpClient();

        string MailTemplate = Server.MapPath("~/EmailTemplates/contactlist.htm");

        string template = string.Empty;
        if (File.Exists(MailTemplate))
        {
            template = File.ReadAllText(MailTemplate);
        }

        template = template.Replace("<%strSubscriber%>", subscriber);
        template = template.Replace("<%strURL%>", strURL);

        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

        msg.From = new MailAddress("your email address", "your email address name");

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

    protected void SendNotification(string emailAddress, String Title, string strFocalPoint, string subscriber, string strURL, string strSector, string strArea)
    {
        SmtpClient smtp = new SmtpClient();

        string MailTemplate = Server.MapPath("~/EmailTemplates/notification.htm");

        string template = string.Empty;
        if (File.Exists(MailTemplate))
        {
            template = File.ReadAllText(MailTemplate);
        }

        template = template.Replace("<%strSubscriber%>", subscriber);
        template = template.Replace("<%strURL%>", strURL);
        template = template.Replace("<%strFocalPoint%>", strFocalPoint);
        template = template.Replace("<%strSector%>", strSector);
        template = template.Replace("<%strArea%>", strArea);

        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

        msg.From = new MailAddress("your email address", "your email address name");

        msg.To.Add(emailAddress);
        msg.Subject = Title;

        String logo = Server.MapPath("~/EmailTemplates/images/interagency.jpg");//you can change with your logo
        System.Net.Mail.AlternateView htmlView = AlternateView.CreateAlternateViewFromString(template, null, "text/html");
        LinkedResource pic1 = new LinkedResource(logo, MediaTypeNames.Image.Jpeg);
        pic1.ContentId = "logo";
        htmlView.LinkedResources.Add(pic1);
        msg.AlternateViews.Add(htmlView);
        objEmail.EmailConfig(template, msg);
    }


    public static string GetUniqueKey(int maxSize)
    {
        char[] chars = new char[62];
        // chars ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
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

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}