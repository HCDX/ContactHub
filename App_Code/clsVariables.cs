using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
 
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Mail;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mime;
using System.Configuration;
using System.Net.Mail;
using System.Net;
 



/// <summary>
/// Summary description for clsEmail
/// </summary>
public class clsVariables
{
    clsEmail obj_Email = new clsEmail();
    public clsVariables()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetDateFromDataBase(string OldDate)
    {
        int index = OldDate.IndexOf("/");

        string[] stringSeparators = new string[] { "/" };
        string[] TextDate = OldDate.Split(stringSeparators, StringSplitOptions.None);

        String MM = TextDate[0];
        String DD = TextDate[1];
        String YY = TextDate[2];

        String NewDate = DD + "/" + MM + "/" + YY;
        return NewDate;
    }


    public DateTime GetDate(string strDate)
    {
        int index3 = strDate.IndexOf("/");
        string [] stringSeparators = new string[] {"/" };
        string[] TextDate = strDate.Split(stringSeparators, StringSplitOptions.None);
        string DD = TextDate[0];
        string MM = TextDate[1];
        string YY = TextDate[2];
        String NewDate = YY + "." + MM + "." + DD;
        return DateTime.Parse(NewDate);
    }

    public int TotalUserCroping;

   
}