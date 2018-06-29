using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using System.Web.Mail;
using System.IO;

using System.Net.Mime;

using System.Configuration;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Summary description for clsEmail
/// </summary>
public class PagesAccess
{

    public PagesAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public Boolean CheckAccess(string PageName, string ProfileID)
    {
        DataTable DtModulePageAccess = HttpContext.Current.Session[RunningCache.DtModulePageAccess] as DataTable;
        DataRow[] result = DtModulePageAccess.Select(" Access= 1 and   PageName='" + PageName + "' and   ProfileID=" + ProfileID);
        if (result.Length > 0)

            return true;
        else
            return false;
    }






}