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

public partial class Default : System.Web.UI.Page
{

    public string _userFullName = string.Empty,
                 _userProfile = string.Empty,
                 _userLastLogin = string.Empty,
                StrToday = string.Empty,
                StrMonth = string.Empty;

    private static string usr;

    FocalPoints objFocalPoints = new FocalPoints();

    protected void Page_Load(object sender, EventArgs e)
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
            var ss = Session[RunningCache.UserLogin];
            if (ss != null)
            {
                if (!string.IsNullOrEmpty(Session[RunningCache.UserLogin].ToString()))
                {
                    usr = Session[RunningCache.UserLogin].ToString();
                    _userFullName = Session[RunningCache.UserFullName].ToString();
                    _userProfile = Session[RunningCache.ProfileName].ToString();
                    StrMonth = DateTime.Now.ToString("MMMM yyyy", ci).ToUpper();
                    StrToday = DateTime.Now.ToString(format, ci).ToUpper();
                    Session[RunningCache.SectorInterestID] = "0";
                    Session[RunningCache.AreaInterestID] = "0";

                    bindgrid();
                }
            }
            else
            {
                Disconnect();
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

    protected void EntGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            int index = Convert.ToInt32(e.CommandArgument) % EntGridView.PageSize;
            string HDnFocalPointID = ((HiddenField)EntGridView.Rows[index].FindControl("FocalPointsID")).Value;
            string HDnFocalPointsUser = ((HiddenField)EntGridView.Rows[index].FindControl("FocalPointsUser")).Value;
            string HDnFocalPointsSector = ((HiddenField)EntGridView.Rows[index].FindControl("FocalPointsSector")).Value;
            string HDnFocalPointsArea = ((HiddenField)EntGridView.Rows[index].FindControl("FocalPointsArea")).Value;

            string SectorInterestName = ((Label)EntGridView.Rows[index].FindControl("SectorInterestName")).Text;
            string AreaInterestName = ((Label)EntGridView.Rows[index].FindControl("AreaInterestName")).Text;



            Session[RunningCache.FocalPointsID] = HDnFocalPointID;
            Session[RunningCache.SectorInterestID] = HDnFocalPointsSector;
            Session[RunningCache.AreaInterestID] = HDnFocalPointsArea;

            if (e.CommandName.Equals("view"))
            {
                try
                {
                    Response.Redirect("ContactList.aspx", false);
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

    protected void EntGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            bindgrid();
            EntGridView.PageIndex = e.NewPageIndex;
            EntGridView.DataBind();

        }
        catch (Exception ex)
        {
            EntGridView.Controls.Add(new LiteralControl("An error occured; please try again, " + ex));
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


    protected void bindgrid()
    {
        try
        {
            DataTable Dt = new DataTable();
            Dt = objFocalPoints.FocalPointsData(Session[RunningCache.UserID].ToString());
            EntGridView.DataSource = Dt;
            EntGridView.DataBind();
            EntGridView.Visible = true;
            no_data.Visible = false;

        }
        catch (Exception ex)
        {

        }


    }

    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        int index = row.RowIndex;
        CheckBox cb = (CheckBox)EntGridView.Rows[index].FindControl("Chk");
        string SectorID = ((HiddenField)EntGridView.Rows[index].FindControl("FocalPointsSector")).Value;
        string AreaID = ((HiddenField)EntGridView.Rows[index].FindControl("FocalPointsArea")).Value;
        string FocalPointID = ((HiddenField)EntGridView.Rows[index].FindControl("FocalPointsID")).Value;
        if (cb.Checked)

            objFocalPoints.CustomUpdateFocalPoints(" FocalPointsNotification=1 where FocalPointsID=" + FocalPointID
                + " and FocalPointsSector=" + SectorID + " and FocalPointsArea=" + AreaID);
        else
            objFocalPoints.CustomUpdateFocalPoints(" FocalPointsNotification=0 where FocalPointsID=" + FocalPointID
                + " and FocalPointsSector=" + SectorID + " and FocalPointsArea=" + AreaID);
    }

}