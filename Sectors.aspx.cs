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

public partial class Sectors : System.Web.UI.Page
{
    private DataTable d = new DataTable();
    SectorInterest objSectorInterest = new SectorInterest();
    Users objUsers = new Users();
    protected static int l;
    private DataTable detail_dt = new DataTable("");
    string Strsearch = "";


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

                    bindgrid();

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
            DataTable Dt = new DataTable();
            if (string.IsNullOrEmpty(Strsearch) == false)
            {
                Dt = objSectorInterest.GetSectorInterest(Strsearch);
                Strsearch = "";
                if (Dt == null || Dt.Rows.Count == 0)
                {
                    no_data.Visible = true;
                    NoDatalbl.Text = "No data found";
                    d.Columns.Clear();
                    d.Rows.Clear();
                    EntGridView.DataSource = d;
                    EntGridView.DataBind();
                    return;
                }

            }
            else Dt = objSectorInterest.GetSectorInterest(" order by  SectorInterestName");

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
            string HDnID = ((HiddenField)EntGridView.Rows[index].FindControl("SectorInterestID")).Value;
            string SectorInterestName = ((Label)EntGridView.Rows[index].FindControl("SectorInterestName")).Text;
            string SectorInterestDescription = ((Label)EntGridView.Rows[index].FindControl("SectorInterestDescription")).Text;

            Session[RunningCache.SectorInterestID] = HDnID;

            if (e.CommandName.Equals("view"))
            {
                try
                {
                    txtSectorInterestName.Text = SectorInterestName;
                    txtSectorInterestDescription.Text = SectorInterestDescription;
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
            string motif = string.Empty;

            string strmsg = "";
            objSectorInterest.SectorInterestID = 0;
            objSectorInterest.SectorInterestName = txtSectorInterestName.Text.Trim();
            objSectorInterest.SectorInterestDescription = txtSectorInterestDescription.Text.Trim();
            objSectorInterest.LastModifiedBy = int.Parse(Session[RunningCache.UserID].ToString());
            objSectorInterest.LastModifiedDate = DateTime.Now;

            if (BtnSave.Text.ToUpper() == "Save".ToUpper())
            {
                DataTable dtSector = objSectorInterest.GetSectorInterest(" where SectorInterestName='" + txtSectorInterestName.Text.Trim() + "'");
                if (dtSector.Rows.Count > 0)
                {
                    if (this.global_success.Visible) this.global_success.Visible = false;
                    this.global_error.Visible = true;
                    this.global_error_msg.Text = txtSectorInterestName.Text.Trim() + " already exist in the database";
                    UpdatePanel.Update();
                }
                else
                {
                    objSectorInterest.SaveSectorInterest(objSectorInterest, out strmsg);
                    if (this.global_error.Visible) this.global_error.Visible = false;
                    this.global_success.Visible = true;
                    this.global_success_msg.Text = "The Sector of Interest is successfully saved into the system.";
                    UpdatePanel.Update();
                }
            }


            else if (BtnSave.Text.ToUpper() == "UPDATE".ToUpper())
            {
                objSectorInterest.SectorInterestID = int.Parse(Session[RunningCache.SectorInterestID].ToString());
                objSectorInterest.UpdateSectorInterest(objSectorInterest, out strmsg);
                this.global_success.Visible = true;
                this.global_success_msg.Text = "The Sector of Interest is successfully updated into the system.";
                UpdatePanel.Update();
            }

            upCrudGrid.Update();
            bindgrid();

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

            if (!(objSectorInterest.DeleteSectorInterest(Session[RunningCache.SectorInterestID].ToString())))
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
            Strsearch = "  WHERE SectorInterestName LIKE  '%" + search_value + "%'    ORDER BY SectorInterestName";
            bindgrid();
        }

        catch (Exception ex)
        {
        }

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        txtSectorInterestName.Text = string.Empty;
        txtSectorInterestDescription.Text = string.Empty;
        BtnSave.Text = "Save";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addEdiModal').modal('show');");
        sb.Append(@"</script>");
        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addEdiModalScript", sb.ToString(), false);
        upEdit.Update();
    }

}