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


public partial class Unsubscription : System.Web.UI.Page
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



                if (string.IsNullOrEmpty(Request.QueryString["vid"]) == false)
                {
                    Session[RunningCache.InterestCode] = Request.QueryString["vid"];
                    DataTable dtInterest = objInterest.GetInterest(" where InterestCode='" + Session[RunningCache.InterestCode].ToString().Trim() + "'");
                    if (dtInterest.Rows.Count > 0)
                    {
                        Session[RunningCache.InterestID] = dtInterest.Rows[0]["InterestID"].ToString();
                        filldata(Session[RunningCache.InterestID].ToString());
                        bindgrid(Session[RunningCache.InterestCode].ToString());
                    }
                }


            }

            StrMonth = DateTime.Now.ToString("MMMM yyyy", ci).ToUpper();
            StrToday = DateTime.Now.ToString(format, ci).ToUpper();
            if (this.global_success.Visible) this.global_success.Visible = false;

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

    protected void bindgrid(string InterestCode)
    {
        try
        {
            DataTable Dt = new DataTable();
            Dt = objInterest.GetInterest(" where InterestCode='" + InterestCode + "'");
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
                UpdAdd.Update();
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
            bindgrid(Session[RunningCache.InterestID].ToString());
            EntGridView.PageIndex = e.NewPageIndex;
            EntGridView.DataBind();

        }
        catch (Exception ex)
        {
            EntGridView.Controls.Add(new LiteralControl("An error occured; please try again, " + ex));
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
                upDel.Update();
                bindgrid(Session[RunningCache.InterestID].ToString());
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
                upDel.Update();
                bindgrid(Session[RunningCache.InterestID].ToString());

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

    protected void BtnUnsubscribe_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#deleteModal').modal('show');");
        sb.Append(@"</script>");
        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        upDel.Update();
    }

    protected void filldata (string InterestID)
    {
        DataTable dtInterest = objInterest.GetInterest(" where InterestID=" + InterestID);
        if (dtInterest.Rows.Count>0)
        {
            DataTable dtSubscriber = objSubscriber.GetSubscriber(" where SubscriberID="+ dtInterest.Rows[0]["InterestSubscriber"].ToString());
            txtSubFullName.Text = dtSubscriber.Rows[0]["SubscriberFullName"].ToString();
            txtSubEmail.Text = dtSubscriber.Rows[0]["SubscriberEmail"].ToString();
            txtSubTitle.Text = dtSubscriber.Rows[0]["SubscriberTitle"].ToString();
            txtSubOrganization.Value = dtSubscriber.Rows[0]["SubscriberOrganization"].ToString();
            txtSubContact.Text = dtSubscriber.Rows[0]["SubscriberContact"].ToString();

          
        }
    }
}