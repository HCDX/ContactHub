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
using System.Data.Odbc;
using System.Threading;
using System.Web.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using System.Net;
using System.Drawing;

public partial class ContactList : System.Web.UI.Page
{
    private DataTable d = new DataTable();
    Profile objProfile = new Profile();
    Users objUsers = new Users();
    protected static int l;
    protected static string SectorArea;
    SectorInterest objSectorInterest = new SectorInterest();
    AreaInterest objAreaInterest = new AreaInterest();
    DataTable detail_dt = new DataTable("");
    DataTable dtEmailList = new DataTable("");
    DataTable dtExport;
    string Strsearch = "";

    Subscriber objSubscriber = new Subscriber();
    Interest objInterest = new Interest();

    clsEmail objEmail = new clsEmail();

    SqlConnection con;
    string constr, Query, sqlconn, InterestIDs;

    protected void Page_Load(object sender, EventArgs e)
    {
        PagesAccess objPagesAccess = new PagesAccess();
        if (objPagesAccess.CheckAccess(Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath), Session[RunningCache.UserProfile].ToString()) == true)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                Session[RunningCache.Search] = string.Empty; ;
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

                    ddlSectorInterest.SelectedValue = Session[RunningCache.SectorInterestID].ToString();
                    ddlAreaInterest.SelectedValue = Session[RunningCache.AreaInterestID].ToString();

                    bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());
                    BindSecondaryGrid();


                }

                if (this.Edit_global.Visible) this.Edit_global.Visible = false;
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

    protected void bindgrid(string SectorID, string AreaID)
    {
        try
        {

            if (string.IsNullOrEmpty(Session[RunningCache.Search].ToString()) == false)
            {
                if (SectorID == "0" && AreaID == "0")
                {
                    dtEmailList = objSubscriber.GetSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsUser=" + Session[RunningCache.UserID] + ") AND " + Session[RunningCache.Search].ToString() + " order by SubscriberOrganization,SubscriberFullName asc");

                }
                else if (SectorID != "0" && AreaID == "0")
                {
                    dtEmailList = objSubscriber.GetSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsSector= " + SectorID + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsUser=" + Session[RunningCache.UserID] + ") AND " + Session[RunningCache.Search].ToString() + " order by SubscriberOrganization,SubscriberFullName asc");

                }
                else if (SectorID == "0" && AreaID != "0")
                {
                    dtEmailList = objSubscriber.GetSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsArea=" + AreaID + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ") AND " + Session[RunningCache.Search].ToString() + " order by SubscriberOrganization,SubscriberFullName asc");

                }
                else if (SectorID != "0" && AreaID != "0")
                {
                    dtEmailList = objSubscriber.GetSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsSector= " + SectorID + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsArea=" + AreaID + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ") AND " + Session[RunningCache.Search].ToString() + " order by SubscriberOrganization,SubscriberFullName asc");

                }

                //Strsearch = "";
                if (dtEmailList == null || dtEmailList.Rows.Count == 0)
                {
                    no_data.Visible = true;
                    l = dtEmailList.Rows.Count;
                    NoDatalbl.Text = "No Contact found";
                    d.Columns.Clear();
                    d.Rows.Clear();
                    gvAll.DataSource = d;
                    gvAll.DataBind();
                    upCrudGrid.Update();
                    return;
                }

            }
            else
            {
                if (SectorID == "0" && AreaID == "0")
                {
                    dtEmailList = objSubscriber.GetSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsUser=" + Session[RunningCache.UserID] + ")" + " order by SubscriberOrganization,SubscriberFullName asc");
                }
                else if (SectorID != "0" && AreaID == "0")
                {
                    dtEmailList = objSubscriber.GetSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsSector= " + SectorID + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsUser=" + Session[RunningCache.UserID] + ")" + " order by SubscriberOrganization,SubscriberFullName asc");

                }
                else if (SectorID == "0" && AreaID != "0")
                {
                    dtEmailList = objSubscriber.GetSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsArea=" + AreaID + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ")" + " order by SubscriberOrganization,SubscriberFullName asc");

                }
                else if (SectorID != "0" && AreaID != "0")
                {
                    dtEmailList = objSubscriber.GetSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsSector= " + SectorID + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsArea=" + AreaID + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ")" + " order by SubscriberOrganization,SubscriberFullName asc");

                }
            }

            l = dtEmailList.Rows.Count;
            gvAll.DataSource = dtEmailList;
            Session[RunningCache.dtEmailList] = dtEmailList;
            ViewState["dt"] = dtEmailList;
            gvAll.DataBind();

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

    protected void SubscriberList(string SubscriberEmail)
    {
        try
        {
            DataTable Dt = new DataTable();
            Dt = objSubscriber.GetSubscriber(" where SubscriberEmail='" + SubscriberEmail + "' order by  SectorInterestName");
            if (Dt.Rows.Count > 0)
            {
                divSubscriptList.Visible = true;
                EntGridView.DataSource = Dt;
                EntGridView.DataBind();
                EntGridView.Visible = true;
                no_data.Visible = false;
                upCrudGrid.Update();
            }
            else
            {
                Dt.Rows.Clear();
                EntGridView.DataSource = Dt;
                EntGridView.DataBind();
                upCrudGrid.Update();
                return;
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
            SubscriberList(Session[RunningCache.SubscriberEmail].ToString());
            EntGridView.PageIndex = e.NewPageIndex;
            EntGridView.DataBind();
            upCrudGrid.Update();

        }
        catch (Exception ex)
        {
            EntGridView.Controls.Add(new LiteralControl("An error occured; please try again, " + ex));
        }
    }
    #region    Gridview section 

    protected void gvAll_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            int index = Convert.ToInt32(e.CommandArgument) % gvAll.PageSize;
            string HDnID = ((HiddenField)gvAll.Rows[index].FindControl("SubscriberID")).Value;
            string HDnIntID = ((HiddenField)gvAll.Rows[index].FindControl("InterestID")).Value;
            Session[RunningCache.InterestID] = HDnIntID;
            Session[RunningCache.SubscriberID] = HDnID;
            if (e.CommandName.Equals("view"))
            {
                try
                {
                    DataTable dtSubscriver = objSubscriber.GetSubscriber(" where InterestID=" + HDnIntID);
                    if (dtSubscriver.Rows.Count > 0)
                    {
                        txtSectorInterestName.Text = dtSubscriver.Rows[0]["SectorInterestName"].ToString();
                        txtAreaInterestName.Text = dtSubscriver.Rows[0]["AreaInterestName"].ToString();
                        txtSubscriberFullName.Text = dtSubscriver.Rows[0]["SubscriberFullName"].ToString();
                        txtSubscriberEmail.Text = dtSubscriver.Rows[0]["SubscriberEmail"].ToString();
                        txtSubscriberTitle.Text = dtSubscriver.Rows[0]["SubscriberTitle"].ToString();
                        txtSubscriberOrganization.Text = dtSubscriver.Rows[0]["SubscriberOrganization"].ToString();
                        txtSubscriberContact.Text = dtSubscriver.Rows[0]["SubscriberContact"].ToString();
                        Session[RunningCache.SubscriberEmail] = dtSubscriver.Rows[0]["SubscriberEmail"].ToString();
                        SubscriberList(dtSubscriver.Rows[0]["SubscriberEmail"].ToString());
                        BtnSave.Text = "Update";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#addEdiModal').modal('show');");
                        sb.Append(@"</script>");
                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addEdiModalScript", sb.ToString(), false);
                        upEdit.Update();
                    }

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

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GetData();
        gvAll.PageIndex = e.NewPageIndex;

        Session[RunningCache.AreaInterestID] = ddlAreaInterest.SelectedValue.ToString();
        Session[RunningCache.SectorInterestID] = ddlSectorInterest.SelectedValue.ToString();

        if (string.IsNullOrEmpty(Session[RunningCache.SectorInterestID].ToString())) Session[RunningCache.SectorInterestID] = "0";
        if (string.IsNullOrEmpty(Session[RunningCache.AreaInterestID].ToString())) Session[RunningCache.AreaInterestID] = "0";

        bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());
        SetData();
        upCrudGrid.Update();
    }

    private void GetData()
    {
        DataTable dt;
        if (ViewState["SelectedRecords"] != null)
            dt = (DataTable)ViewState["SelectedRecords"];
        else
            dt = CreateDataTable();
        for (int i = 0; i < gvAll.Rows.Count; i++)
        {

            CheckBox chk = (CheckBox)gvAll.Rows[i]
                            .Cells[0].FindControl("chk");
            if (chk.Checked)
            {
                dt = AddRow(gvAll.Rows[i], dt);
            }
            else
            {
                dt = RemoveRow(gvAll.Rows[i], dt);
            }

        }
        ViewState["SelectedRecords"] = dt;
    }

    private void SetData()
    {
        if (ViewState["SelectedRecords"] != null)
        {
            DataTable dt = (DataTable)ViewState["SelectedRecords"];
            for (int i = 0; i < gvAll.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvAll.Rows[i].Cells[0].FindControl("chk");
                if (chk != null)
                {
                    DataRow[] dr = dt.Select(" InterestID = '" + gvAll.Rows[i].Cells[1].Text + "'");
                    chk.Checked = dr.Length > 0;

                }
            }
        }
    }

    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("InterestID");
        dt.Columns.Add("SectorInterestName");
        dt.Columns.Add("AreaInterestName");
        dt.AcceptChanges();
        return dt;
    }

    private DataTable AddRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select(" InterestID  = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length <= 0)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["InterestID"] = gvRow.Cells[1].Text;
            dt.Rows[dt.Rows.Count - 1]["SectorInterestName"] = gvRow.Cells[2].Text;
            dt.Rows[dt.Rows.Count - 1]["AreaInterestName"] = gvRow.Cells[3].Text;
            dt.AcceptChanges();
        }
        return dt;
    }

    private DataTable RemoveRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("InterestID = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length > 0)
        {
            dt.Rows.Remove(dr[0]);
            dt.AcceptChanges();
        }
        return dt;
    }

    protected void CheckBox_CheckChanged(object sender, EventArgs e)
    {
        GetData();
        SetData();
        BindSecondaryGrid();
    }

    private void BindSecondaryGrid()
    {
        DataTable dt = (DataTable)ViewState["SelectedRecords"];
        gvSelected.DataSource = dt;
        gvSelected.DataBind();
    }

    #endregion

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            string motif = string.Empty;

            string strmsg = "";
            objSubscriber.SubscriberID = 0;
            objSubscriber.SubscriberFullName = txtSubscriberFullName.Text.Trim();
            objSubscriber.SubscriberEmail = txtSubscriberEmail.Text.Trim();
            objSubscriber.SubscriberTitle = txtSubscriberTitle.Text.Trim();
            objSubscriber.SubscriberOrganization = txtSubscriberOrganization.Text.Trim();
            objSubscriber.SubscriberContact = txtSubscriberContact.Text.Trim();
            objSubscriber.LastModifiedBy = Session[RunningCache.UserID].ToString();
            objSubscriber.LastModifiedDate = DateTime.Now;

            if (BtnSave.Text.ToUpper() == "Save".ToUpper())
            {
                if (IsValid(txtSubscriberEmail.Text.Trim()))
                {
                    DataTable dtSubscriber = objSubscriber.GetSubscriber("  where SubscriberEmail='" + txtSubscriberEmail.Text.Trim() + "'");
                    if (dtSubscriber.Rows.Count == 0)
                    {
                        Session[RunningCache.SubscriberID] = objSubscriber.SaveSubscriber(objSubscriber, out strmsg);
                        objInterest.InterestID = 0;
                        objInterest.InterestSector = int.Parse(ddlSectorInterest.SelectedValue.ToString());
                        objInterest.InterestArea = int.Parse(ddlAreaInterest.SelectedValue.ToString());
                        objInterest.InterestSubscriber = int.Parse(Session[RunningCache.SubscriberID].ToString());
                        objInterest.InterestStatus = true;
                        objInterest.LastModifiedBy = int.Parse(Session[RunningCache.UserID].ToString());
                        objInterest.LastModifiedDate = DateTime.Now;
                        objInterest.SaveInterest(objInterest, out strmsg);

                        if (this.global_error.Visible) this.global_error.Visible = false;
                        this.global_success.Visible = true;
                        this.global_success_msg.Text = txtSubscriberEmail.Text.Trim() + " is successfully saved into the system.";
                        UpdatePanel.Update();
                    }
                    else
                    {
                        if (AlreadyExist(dtSubscriber, ddlSectorInterest.SelectedValue.ToString(), ddlAreaInterest.SelectedValue.ToString()) == 0)
                        {
                            objInterest.InterestID = 0;
                            objInterest.InterestSector = int.Parse(Session[RunningCache.SectorInterestID].ToString());
                            objInterest.InterestArea = int.Parse(Session[RunningCache.AreaInterestID].ToString());
                            objInterest.InterestSubscriber = int.Parse(dtSubscriber.Rows[0]["SubscriberID"].ToString());
                            objInterest.InterestStatus = true;
                            objInterest.LastModifiedBy = int.Parse(Session[RunningCache.UserID].ToString());
                            objInterest.LastModifiedDate = DateTime.Now;
                            objInterest.SaveInterest(objInterest, out strmsg);

                            if (this.global_error.Visible) this.global_error.Visible = false;
                            this.global_success.Visible = true;
                            this.global_success_msg.Text = "The selected Sector and Area have been added to " + txtSubscriberEmail.Text.Trim() + " information in the system.";
                            UpdatePanel.Update();
                        }
                        else
                        {
                            if (this.global_success.Visible) this.global_success.Visible = false;
                            this.global_error.Visible = true;
                            this.global_error_msg.Text = txtSubscriberEmail.Text.Trim() + " email has already subscribed for the selected Sector and Area.";
                            UpdatePanel.Update();

                        }
                    }

                }

                else
                {
                    if (this.global_success.Visible) this.global_success.Visible = false;
                    this.global_error.Visible = true;
                    this.global_error_msg.Text = txtSubscriberEmail.Text.Trim() + " is not a valid email address.";
                    UpdatePanel.Update();
                }


            }

            else if (BtnSave.Text.ToUpper() == "UPDATE".ToUpper())
            {
                objSubscriber.SubscriberID = int.Parse(Session[RunningCache.SubscriberID].ToString());
                objSubscriber.FocalPointUpdateSubscriber(objSubscriber, out strmsg);
                this.global_success.Visible = true;
                this.global_success_msg.Text = txtSubscriberEmail.Text.Trim() + " information is successfully updated into the system.";
                UpdatePanel.Update();
            }

            upCrudGrid.Update();

            GetData();
            Session[RunningCache.SectorInterestID] = ddlSectorInterest.SelectedValue.ToString();
            Session[RunningCache.AreaInterestID] = ddlAreaInterest.SelectedValue.ToString();

            if (string.IsNullOrEmpty(Session[RunningCache.SectorInterestID].ToString())) Session[RunningCache.SectorInterestID] = "0";
            if (string.IsNullOrEmpty(Session[RunningCache.AreaInterestID].ToString())) Session[RunningCache.AreaInterestID] = "0";

            if (string.IsNullOrEmpty(Session[RunningCache.Search].ToString()))
            { Session[RunningCache.Search] = ""; }
            else { Session[RunningCache.Search] = " (SubscriberAddress LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberTitle LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberOrganization LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberFullName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberEmail LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR AreaInterestName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SectorInterestName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' )"; }

            bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());
            SetData();

        }

        catch (Exception ex)
        {
        }


        sb.Append(@"<script type='text/javascript'>");

        sb.Append("$('#addEdiModal').modal('hide');");

        sb.Append(@"</script>");

        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetData();
            Session[RunningCache.Search] = ClsTools.Text_Validator(txtSearch.Text.Trim());

            Session[RunningCache.AreaInterestID] = ddlAreaInterest.SelectedValue.ToString();
            Session[RunningCache.SectorInterestID] = ddlSectorInterest.SelectedValue.ToString();
            if (string.IsNullOrEmpty(Session[RunningCache.SectorInterestID].ToString())) Session[RunningCache.SectorInterestID] = "0";
            if (string.IsNullOrEmpty(Session[RunningCache.AreaInterestID].ToString())) Session[RunningCache.AreaInterestID] = "0";

            Session[RunningCache.Search] = " (SubscriberAddress LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberTitle LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberOrganization LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberFullName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberEmail LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR AreaInterestName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SectorInterestName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' )";
            bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());
            SetData();
        }

        catch (Exception ex)
        {
        }

    }

    protected void bindSectorInterest()
    {
        DataTable dt = objSectorInterest.GetSectorInterest("  where SectorInterestID IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE FocalPointsUser=" + Session[RunningCache.UserID] + ") order by SectorInterestName ");

        DataRow dr = dt.NewRow();
        dr[1] = "All";
        dt.Rows.InsertAt(dr, 0);
        dt.AcceptChanges();

        ddlSectorInterest.DataSource = dt;
        ddlSectorInterest.DataTextField = "SectorInterestName";
        ddlSectorInterest.DataValueField = "SectorInterestID";
        ddlSectorInterest.DataBind();

    }

    protected void bindAreaInterest()
    {
        DataTable dt = objAreaInterest.GetAreaInterest("  where AreaInterestID IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsUser=" + Session[RunningCache.UserID] + ")  order by AreaInterestName ");

        DataRow dr = dt.NewRow();
        dr[1] = "All";
        dt.Rows.InsertAt(dr, 0);
        dt.AcceptChanges();

        ddlAreaInterest.DataSource = dt;
        ddlAreaInterest.DataTextField = "AreaInterestName";
        ddlAreaInterest.DataValueField = "AreaInterestID";
        ddlAreaInterest.DataBind();

    }

    protected void ddlSectorInterest_Changed(object sender, EventArgs e)
    {
        GetData();
        Session[RunningCache.SectorInterestID] = ddlSectorInterest.SelectedValue.ToString();
        Session[RunningCache.AreaInterestID] = ddlAreaInterest.SelectedValue.ToString();

        if (string.IsNullOrEmpty(Session[RunningCache.SectorInterestID].ToString())) Session[RunningCache.SectorInterestID] = "0";
        if (string.IsNullOrEmpty(Session[RunningCache.AreaInterestID].ToString())) Session[RunningCache.AreaInterestID] = "0";

        if (string.IsNullOrEmpty(Session[RunningCache.Search].ToString()))
        { Session[RunningCache.Search] = ""; }
        else { Session[RunningCache.Search] = " (SubscriberAddress LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberTitle LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberOrganization LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberFullName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberEmail LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR AreaInterestName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SectorInterestName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' )"; }

        bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());
        SetData();
    }

    protected void ddlAreaInterest_Changed(object sender, EventArgs e)
    {
        GetData();
        Session[RunningCache.AreaInterestID] = ddlAreaInterest.SelectedValue.ToString();
        Session[RunningCache.SectorInterestID] = ddlSectorInterest.SelectedValue.ToString();

        if (string.IsNullOrEmpty(Session[RunningCache.SectorInterestID].ToString())) Session[RunningCache.SectorInterestID] = "0";
        if (string.IsNullOrEmpty(Session[RunningCache.AreaInterestID].ToString())) Session[RunningCache.AreaInterestID] = "0";

        if (string.IsNullOrEmpty(Session[RunningCache.Search].ToString()))
        { Session[RunningCache.Search] = ""; }
        else { Session[RunningCache.Search] = " (SubscriberAddress LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberTitle LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberOrganization LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberFullName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SubscriberEmail LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR AreaInterestName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' OR SectorInterestName LIKE '%" + Session[RunningCache.Search].ToString().Trim() + "%' )"; }

        bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());
        SetData();
    }

    protected void BtnCopyEmail_Click(object sender, EventArgs e)
    {
        CKEmail.Text = string.Empty;
        string EmailList = string.Empty;
        DataTable dtEmail = new DataTable();
        DataTable Dt = HttpContext.Current.Session[RunningCache.dtEmailList] as DataTable;
        dtEmail = Dt.DefaultView.ToTable(true, "SubscriberEmail");

        if (dtEmail.Rows.Count > 0)
        {
            foreach (DataRow row in dtEmail.Rows)
            {
                EmailList = EmailList + row["SubscriberEmail"].ToString().Trim() + ";";
            }

        }
        CKEmail.Text = EmailList;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#copyEmailModal').modal('show');");
        sb.Append(@"</script>");
        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "copyEmailModalScript", sb.ToString(), false);
        upEmail.Update();
    }

    protected void BtnImportModal_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlSectorInterest.SelectedValue) || string.IsNullOrEmpty(ddlAreaInterest.SelectedValue))
        {
            if (this.global_success.Visible) this.global_success.Visible = false;
            this.global_error.Visible = true;
            this.global_error_msg.Text = "Please specify the sector and the area for the contact to be uploaded.";
            UpdatePanel.Update();
        }
        else
        {
            if (this.global_error.Visible) this.global_error.Visible = false;
            if (this.global_success.Visible) this.global_success.Visible = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#importContactModal').modal('show');");
            sb.Append(@"</script>");
            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "importContactModal", sb.ToString(), false);
        }
    }

    protected void BtnImport_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        try
        {

            GetData();
            if (FileUpload1.HasFile)
            {
                Session[RunningCache.SectorInterestID] = ddlSectorInterest.SelectedValue;
                Session[RunningCache.AreaInterestID] = ddlAreaInterest.SelectedValue;
                DataTable dtSector = objSectorInterest.GetSectorInterest(" where SectorInterestID=" + ddlSectorInterest.SelectedValue);
                DataTable dtArea = objAreaInterest.GetAreaInterest(" where AreaInterestID=" + ddlAreaInterest.SelectedValue);
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);

                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                //Get the Sheets in Excel WorkBook
                conStr = String.Format(conStr, FilePath, "Yes");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmdExcel.Connection = connExcel;
                connExcel.Open();
                System.Data.DataTable Dtschema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                connExcel.Close();

                Query = string.Format("Select [FullName],[Email],[Title],[Organization],[Contact] from [Contacts$]");
                OleDbCommand Ecom = new OleDbCommand(Query, connExcel);
                Ecom.Connection = connExcel;
                connExcel.Open();

                DataSet ds = new DataSet();
                OleDbDataAdapter oda1 = new OleDbDataAdapter(Query, connExcel);
                connExcel.Close();
                oda1.Fill(ds);
                System.Data.DataTable Exceldt = ds.Tables[0];

                string strmsg = "";
                string strSector = dtSector.Rows[0]["SectorInterestName"].ToString();
                string strArea = dtArea.Rows[0]["AreaInterestName"].ToString();
                foreach (DataRow row in Exceldt.Rows)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(row["Email"].ToString().Trim()) && IsValid(row["Email"].ToString().Trim()))
                        {
                            objSubscriber.SubscriberID = 0;
                            objSubscriber.SubscriberFullName = row["FullName"].ToString().Trim();
                            objSubscriber.SubscriberEmail = row["Email"].ToString().Trim();
                            objSubscriber.SubscriberTitle = row["Title"].ToString().Trim();
                            objSubscriber.SubscriberOrganization = row["Organization"].ToString().Trim();
                            objSubscriber.SubscriberContact = row["Contact"].ToString().Trim();
                            objSubscriber.LastModifiedBy = Session[RunningCache.UserID].ToString();
                            objSubscriber.LastModifiedDate = DateTime.Now;
                            objSubscriber.SubscriberCode = GetUniqueKey(6);
                            objSubscriber.SubscriberAddedDate = DateTime.Now;

                            DataTable dtSubscriber = objSubscriber.GetSubscriber("  where SubscriberEmail='" + row["Email"].ToString() + "'");
                            if (dtSubscriber.Rows.Count == 0)
                            {
                                Session[RunningCache.SubscriberID] = objSubscriber.SaveSubscriber(objSubscriber, out strmsg);
                                objInterest.InterestID = 0;
                                objInterest.InterestSector = int.Parse(Session[RunningCache.SectorInterestID].ToString());
                                objInterest.InterestArea = int.Parse(Session[RunningCache.AreaInterestID].ToString());
                                objInterest.InterestSubscriber = int.Parse(Session[RunningCache.SubscriberID].ToString());
                                objInterest.InterestStatus = true;
                                objInterest.LastModifiedBy = int.Parse(Session[RunningCache.UserID].ToString());
                                objInterest.LastModifiedDate = DateTime.Now;
                                objInterest.InterestCode = GetUniqueKey(6);
                                objInterest.SaveInterest(objInterest, out strmsg);

                                this.global_success.Visible = true;
                                this.global_success_msg.Text = "Contacts successfully imported in your list";
                            }
                            else
                            {
                                objSubscriber.SubscriberID = int.Parse(dtSubscriber.Rows[0]["SubscriberID"].ToString());
                                objSubscriber.UpdateSubscriber(objSubscriber, out strmsg);

                                if (AlreadyExist(dtSubscriber, Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString()) == 0)
                                {
                                    objInterest.InterestID = 0;
                                    objInterest.InterestSector = int.Parse(Session[RunningCache.SectorInterestID].ToString());
                                    objInterest.InterestArea = int.Parse(Session[RunningCache.AreaInterestID].ToString());
                                    objInterest.InterestSubscriber = int.Parse(dtSubscriber.Rows[0]["SubscriberID"].ToString());
                                    objInterest.InterestStatus = true;
                                    objInterest.LastModifiedBy = int.Parse(Session[RunningCache.UserID].ToString());
                                    objInterest.LastModifiedDate = DateTime.Now;
                                    objInterest.InterestCode = GetUniqueKey(6);
                                    objInterest.SaveInterest(objInterest, out strmsg);

                                    this.global_success.Visible = true;
                                    this.global_success_msg.Text = "Contacts successfully imported in your list";
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                upCrudGrid.Update();
                bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());
            }
            SetData();
        }
        catch (Exception ex)
        {

        }
        sb.Append(@"<script type='text/javascript'>");

        sb.Append("$('#importContactModal').modal('hide');");

        sb.Append(@"</script>");

        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "importContactModal", sb.ToString(), false);
    }

    private Int32 AlreadyExist(DataTable d, string sector, string area)
    {
        int count = 0;
        foreach (DataRow row in d.Rows)
        {
            if ((row["InterestSector"].ToString().Trim() == sector) && (row["InterestArea"].ToString().Trim() == area))
            {
                count++;
            }
        }
        return count;

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


    protected void BtnDeleteModal_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["SelectedRecords"];
        if (dt != null && dt.Rows.Count > 0)
        {
            if (this.global_error.Visible) this.global_error.Visible = false;
            if (this.global_success.Visible) this.global_success.Visible = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('show');");
            sb.Append(@"</script>");
            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "deleteModal", sb.ToString(), false);
        }
        else
        {
            if (this.global_success.Visible) this.global_success.Visible = false;
            this.global_error.Visible = true;
            this.global_error_msg.Text = "Please select Subcriber(s)";
            UpdatePanel.Update();
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            InterestIDs = "";
            DataTable dt = (DataTable)ViewState["SelectedRecords"];
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                InterestIDs = dt.Rows[i]["InterestID"].ToString() + "," + InterestIDs;
            }
            InterestIDs = InterestIDs + dt.Rows[0]["InterestID"].ToString();



            if (!(objInterest.DeleteMultipleInterest(InterestIDs)))
            {
                if (this.global_success.Visible) this.global_success.Visible = false;
                this.global_error.Visible = true;
                this.global_error_msg.Text = Mains.Constant.FAIL_CRUD;
                UpdatePanel.Update();
                upCrudGrid.Update();
                upDel.Update();
                bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('hide');");
                sb.Append(@"</script>");
                ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
                return;
            }
            else
            {
                //GetData();
                if (this.global_error.Visible) this.global_error.Visible = false;
                this.global_success.Visible = true;
                this.global_success_msg.Text = Mains.Constant.SUCCESS_DELETE;

                Session[RunningCache.SectorInterestID] = ddlSectorInterest.SelectedValue.ToString();
                Session[RunningCache.AreaInterestID] = ddlAreaInterest.SelectedValue.ToString();

                if (string.IsNullOrEmpty(Session[RunningCache.SectorInterestID].ToString())) Session[RunningCache.SectorInterestID] = "0";
                if (string.IsNullOrEmpty(Session[RunningCache.AreaInterestID].ToString())) Session[RunningCache.AreaInterestID] = "0";

                bindgrid(Session[RunningCache.SectorInterestID].ToString(), Session[RunningCache.AreaInterestID].ToString());

                ViewState["SelectedRecords"] = null;
                BindSecondaryGrid();
                UpdatePanel.Update();

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

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(ddlAreaInterest.SelectedValue.ToString()) || string.IsNullOrEmpty(ddlSectorInterest.SelectedValue.ToString()))
        {
            if (this.global_success.Visible) this.global_success.Visible = false;
            this.global_error.Visible = true;
            this.global_error_msg.Text = "Please select a Secor and an Area.";
            UpdatePanel.Update();
        }
        else
        {
            divSubscriptList.Visible = false;
            DataTable dtSector = objSectorInterest.GetSectorInterest(" where SectorInterestID=" + ddlSectorInterest.SelectedValue.ToString());
            DataTable dtArea = objAreaInterest.GetAreaInterest(" where AreaInterestID=" + ddlAreaInterest.SelectedValue.ToString());

            Session[RunningCache.AreaInterestID] = ddlAreaInterest.SelectedValue.ToString();
            Session[RunningCache.SectorInterestID] = ddlSectorInterest.SelectedValue.ToString();

            txtSectorInterestName.Text = dtSector.Rows[0]["SectorInterestName"].ToString();
            txtAreaInterestName.Text = dtArea.Rows[0]["AreaInterestName"].ToString();
            txtSubscriberFullName.Text = string.Empty;
            txtSubscriberEmail.Text = string.Empty;
            txtSubscriberTitle.Text = string.Empty;
            txtSubscriberOrganization.Text = string.Empty;
            txtSubscriberContact.Text = string.Empty;

            BtnSave.Text = "Save";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addEdiModal').modal('show');");
            sb.Append(@"</script>");
            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addEdiModalScript", sb.ToString(), false);
            upEdit.Update();
        }

    }

    //GridView to Excel
    protected void BtnExportToExcel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlSectorInterest.SelectedValue) && string.IsNullOrEmpty(ddlAreaInterest.SelectedValue))
        {
            dtExport = objSubscriber.ExportSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsUser=" + Session[RunningCache.UserID] + ")");

        }
        else if (string.IsNullOrEmpty(ddlSectorInterest.SelectedValue) && !string.IsNullOrEmpty(ddlAreaInterest.SelectedValue))
        {
            dtExport = objSubscriber.ExportSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsArea=" + ddlAreaInterest.SelectedValue.ToString() + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ")");


        }
        else if (!string.IsNullOrEmpty(ddlSectorInterest.SelectedValue) && string.IsNullOrEmpty(ddlAreaInterest.SelectedValue))
        {
            dtExport = objSubscriber.ExportSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsSector= " + ddlSectorInterest.SelectedValue.ToString() + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsUser=" + Session[RunningCache.UserID] + ")");
        }
        else if (!string.IsNullOrEmpty(ddlSectorInterest.SelectedValue) && !string.IsNullOrEmpty(ddlAreaInterest.SelectedValue))
        {
            dtExport = objSubscriber.ExportSubscriber(" WHERE InterestStatus=1 AND InterestSector IN (SELECT FocalPointsSector FROM GetFocalPoints WHERE "
                                     + " FocalPointsSector= " + ddlSectorInterest.SelectedValue.ToString() + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ") AND InterestArea IN (SELECT FocalPointsArea FROM GetFocalPoints WHERE FocalPointsArea=" + ddlAreaInterest.SelectedValue.ToString() + " AND FocalPointsUser=" + Session[RunningCache.UserID] + ")");

        }


        GridView gvExp = new GridView();
        gvExp.AllowPaging = false;
        gvExp.DataSource = dtExport;
        gvExp.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ContactHub.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            gvExp.AllowPaging = false;

            this.bindgrid(ddlSectorInterest.SelectedValue.ToString(), ddlAreaInterest.SelectedValue.ToString());




            gvExp.HeaderRow.BackColor = Color.White;

            foreach (TableCell cell in gvExp.HeaderRow.Cells)
            {
                cell.BackColor = gvExp.HeaderStyle.BackColor;
            }

            foreach (GridViewRow row in gvExp.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvExp.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvExp.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvExp.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
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

        msg.From = new MailAddress("your email address", "your email address title");

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

}