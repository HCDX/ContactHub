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
using System.Text;

/// <summary>
/// Summary description for clsEmail
/// </summary>
public class clsEmail
{
	public clsEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    public Boolean EmailConfig(string template, MailMessage msg)
    {
        msg.IsBodyHtml = true;
        msg.Priority = MailPriority.High;
        msg.Body = template;

        msg.From = new MailAddress("your email address", "Title of the email");//add your email address and Title of the email 
        SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Timeout = 10000;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("Network username", "Network password");//add your network username and password. In this application, I am using SendGrid
        smtpClient.Credentials = credentials;

        smtpClient.Send(msg);
        return true;

    }


}