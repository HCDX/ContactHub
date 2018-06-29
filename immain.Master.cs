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
using System.Web.Security;

public partial class immain : System.Web.UI.MasterPage
{
    PagesAccess objPagesAccess = new PagesAccess();
    public string _userFullName = string.Empty,
                  _userProfile = string.Empty;

    private static string usr;

    protected void Page_Load(object sender, EventArgs e)
    {
        var ss = Session[RunningCache.UserLogin];
        if (ss != null)
        {
            if (!string.IsNullOrEmpty(Session[RunningCache.UserLogin].ToString()))
            {
                _userFullName = Session[RunningCache.UserFullName].ToString();
                _userProfile = Session[RunningCache.ProfileName].ToString();
            }
        }
        else
        {
            Disconnect();
        }

        if (Request.Form["__EVENTTARGET"] == "Disconnect")
        {
            Disconnect();
        }

        if (!IsPostBack)
        {

            try
            {
                string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
                string strtype = Request.QueryString["type"];


                #region Visibility of the Pages
                #region Contact List
                string a = Session[RunningCache.UserLogin].ToString();
                if (objPagesAccess.CheckAccess("ContactList", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liContacts.Visible = true;
                    ulContacts.Visible = true;
                    liContactList.Visible = true;
                }

                #endregion

                #region Sector
                if (objPagesAccess.CheckAccess("Sectors", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liContacts.Visible = true;
                    ulContacts.Visible = true;
                    liConfiguration.Visible = true;
                    ulConfiguration.Visible = true;
                    liSector.Visible = true;
                }
                #endregion

                #region Areas
                if (objPagesAccess.CheckAccess("Areas", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liContacts.Visible = true;
                    ulContacts.Visible = true;
                    liConfiguration.Visible = true;
                    ulConfiguration.Visible = true;
                    liArea.Visible = true;
                }
                #endregion
                #region Focal Point
                if (objPagesAccess.CheckAccess("FocalPoint", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liContacts.Visible = true;
                    ulContacts.Visible = true;
                    liConfiguration.Visible = true;
                    ulConfiguration.Visible = true;
                    liSector.Visible = true;
                    liFocalPoint.Visible = true;
                }
                #endregion

                #region Administration

                #region Profiles
                if (objPagesAccess.CheckAccess("Profiles", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liAdmin.Visible = true;
                    ulAdmin.Visible = true;
                    LiUser.Visible = true;
                    LiProfiles.Visible = true;
                }
                #endregion
                #region System page Access
                if (objPagesAccess.CheckAccess("SystemPageAccess", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liAdmin.Visible = true;
                    ulAdmin.Visible = true;
                    LiUser.Visible = true;
                    LiSystemPageAccess.Visible = true;
                }
                #endregion
                #region Add User
                if (objPagesAccess.CheckAccess("AddUsers", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liAdmin.Visible = true;
                    ulAdmin.Visible = true;
                    LiUser.Visible = true;
                    LiAddUsers.Visible = true;
                }
                #endregion

                #region System Module
                if (objPagesAccess.CheckAccess("SystemMod", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liAdmin.Visible = true;
                    ulAdmin.Visible = true;
                    liSystemMod.Visible = true;
                }


                if (objPagesAccess.CheckAccess("SystemPage", Session[RunningCache.UserProfile].ToString()) == true)
                {
                    liAdmin.Visible = true;
                    ulAdmin.Visible = true;
                    liSystemPage.Visible = true;
                }

                #endregion

                #endregion
                #endregion



                #region Active Page

                #region Contact List
                if (pageName == "ContactList")
                {
                    liContacts.Attributes.Add("class", "open");
                    liContactList.Attributes.Add("class", "active");
                }


                if (pageName == "Sectors")
                {
                    liContacts.Attributes.Add("class", "open");
                    liConfiguration.Attributes.Add("class", "open");
                    liSector.Attributes.Add("class", "active");
                }

                if (pageName == "Areas")
                {
                    liContacts.Attributes.Add("class", "open");
                    liConfiguration.Attributes.Add("class", "open");
                    liArea.Attributes.Add("class", "active");
                }

                if (pageName == "FocalPoint")
                {
                    liContacts.Attributes.Add("class", "open");
                    liConfiguration.Attributes.Add("class", "open");
                    liFocalPoint.Attributes.Add("class", "active");
                }
                #endregion

                #region Administration

                if (pageName == "Profiles")
                {
                    liAdmin.Attributes.Add("class", "open");
                    LiUser.Attributes.Add("class", "open");
                    LiProfiles.Attributes.Add("class", "active");
                }

                if (pageName == "AddUsers")
                {
                    liAdmin.Attributes.Add("class", "open");
                    LiUser.Attributes.Add("class", "open");
                    LiAddUsers.Attributes.Add("class", "active");
                }

                if (pageName == "SystemPageAccess")
                {
                    liAdmin.Attributes.Add("class", "open");
                    LiUser.Attributes.Add("class", "open");
                    LiSystemPageAccess.Attributes.Add("class", "active");
                }



                if (pageName == "SystemMod")
                {
                    liAdmin.Attributes.Add("class", "open");
                    liSystemMod.Attributes.Add("class", "active");
                }

                if (pageName == "SystemPage")
                {
                    liAdmin.Attributes.Add("class", "open");

                    liSystemPage.Attributes.Add("class", "active");
                }




                #endregion

                #endregion

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


}
