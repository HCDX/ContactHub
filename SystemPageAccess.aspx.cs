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


public partial class SystemPageAccess : System.Web.UI.Page
{
    private DataTable d = new DataTable();
    Modules objModules = new Modules();
    Users objUsers = new Users();
    ModulesPage objModulesPage = new ModulesPage();
    ModulePageAccess objModulePageAccess = new ModulePageAccess();
    Profile objProfile = new Profile();
    protected static int l;
    private DataTable detail_dt = new DataTable("");
    string Strsearch = "";
    private const string CHECKED_ITEMS = "CheckedItems";

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

                    bindProfile();
                    bindModule();
                    bindPage(ddlProfile.SelectedValue, ddlModule.SelectedValue);
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

    protected void EntGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            EntGridView.PageIndex = e.NewPageIndex;
            bindPage(ddlProfile.SelectedValue, ddlModule.SelectedValue);
            EntGridView.DataBind();

        }
        catch (Exception ex)
        {
            EntGridView.Controls.Add(new LiteralControl("An error occured; please try again, " + ex));
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            string motif = string.Empty;

            foreach (GridViewRow rowType in EntGridView.Rows)
            {
                try
                {

                    string strmsg = "";
                    HiddenField ModulePageAccessID = (HiddenField)rowType.FindControl("ModulePageAccessID");
                    HiddenField ModulesPageID = (HiddenField)rowType.FindControl("ModulesPageID");
                    CheckBox Chk = (CheckBox)rowType.FindControl("Chk");

                    objModulePageAccess.ModulePageAccessID = int.Parse(ModulePageAccessID.Value);
                    objModulePageAccess.PageID = int.Parse(ModulesPageID.Value);
                    objModulePageAccess.ProfileID = int.Parse(ddlProfile.SelectedValue);
                    if (Chk.Checked == true)
                        objModulePageAccess.Access = true;
                    else objModulePageAccess.Access = false;
                    objModulePageAccess.Doneby = int.Parse(Session[RunningCache.UserID].ToString());
                    objModulePageAccess.DoneDate = DateTime.Now;

                    if (ModulePageAccessID.Value == "0")
                    {

                        objModulePageAccess.SaveModulePageAccess(objModulePageAccess, out strmsg);
                    }
                    else
                    {
                        objModulePageAccess.UpdateModulePageAccess(objModulePageAccess, out strmsg);
                    }

                }
                catch (Exception ex)
                {


                }

            }

            bindPage(ddlProfile.SelectedValue, ddlModule.SelectedValue);

        }

        catch (Exception ex)
        {
        }

    }

    protected void bindModule()
    {
        DataTable Dt = objModules.GetModules("  where ModuleName is not null  order by  ModuleName");
        DataRow dr = Dt.NewRow();
        dr[0] = 0;
        dr[1] = "Choose a Module";
        Dt.Rows.InsertAt(dr, 0);
        Dt.AcceptChanges();
        ddlModule.DataSource = Dt;

        ddlModule.DataTextField = "ModuleName";
        ddlModule.DataValueField = "ModuleID";
        ddlModule.DataBind();

    }

    protected void bindProfile()
    {

        DataTable Dt = objProfile.GetProfile("  where ProfileName is not null  order by  Profilename");
        DataRow dr = Dt.NewRow();
        dr[0] = 0;
        dr[1] = "Choose a Profile";
        Dt.Rows.InsertAt(dr, 0);
        Dt.AcceptChanges();
        ddlProfile.DataSource = Dt;
        ddlProfile.DataTextField = "Profilename";
        ddlProfile.DataValueField = "ProfileID";
        ddlProfile.DataBind();

    }

    protected void bindPage(string ProfileID, string ModuleID)
    {
        try
        {

            DataTable Dt = new DataTable();

            if (ModuleID == "0")
            {
                Dt = objModulePageAccess.GetModulePageAccessProc(ProfileID);
            }

            else
            {
                Dt = objModulePageAccess.GetModulePageAccessProcModule(ProfileID, ModuleID);
            }
            EntGridView.DataSource = Dt;
            EntGridView.DataBind();
            l = Dt.Rows.Count;
        }
        catch (Exception ex)
        {
            if (this.global_success.Visible) this.global_success.Visible = false;
            this.global_error.Visible = true;
            this.global_error_msg.Text = Mains.Constant.GENERAL_ERR;
        }
    }

    protected void ddlProfile_Click(object sender, EventArgs e)
    {

        bindPage(ddlProfile.SelectedValue, ddlModule.SelectedValue);


    }

}