using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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


using System.Reflection;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.ComponentModel;



/// <summary>
/// Summary description for CommonFunctions
/// </summary>
public class CommonFunctions
{
    public CommonFunctions()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    #region  Common function

    public static int getInt(string valueToInt)
    {
        int outValue = 0;
        try
        {
            if (string.IsNullOrEmpty(valueToInt) == false)
            {

                try
                {
                    outValue = int.Parse(valueToInt);

                }
                catch 
                {
                    outValue = 0;
                }
            }
            else
            {
                outValue = 0;
            }
        }
        catch
        {
            outValue = 0;
        }
        return outValue;
    }

    public static Boolean getBoolean(string valueToInt)
    {
        Boolean outValue = false;
        if (string.IsNullOrEmpty(valueToInt) == false)
        {

            if ((valueToInt == "1") || (valueToInt == "true") || ((valueToInt == "True")))
                outValue = true;
            else
                outValue = false;
        }
        else
        {
            outValue = false;
        }
        return outValue;
    }

    public static float getFloat(string valueToFloat)
    {
        float outValue = 0;
        try
        {
            outValue = float.Parse(valueToFloat);

        }
        catch (Exception ex)
        {
            outValue = 0;
        }
        return outValue;
    }

    public static int getIntFromSession(string mySession)
    {
        // Session[RunningCache.Strwhere] = "";
        int outValue = 0;
        try
        {

            //if (int.TryParse(mySession.ToString, outValue) == true)
            //{

            //}
            outValue = int.Parse(mySession.ToString());

        }
        catch (Exception ex)
        {
            outValue = 0;
            System.Web.HttpContext.Current.Response.Redirect("Login.aspx?erID=1");
        }
        return outValue;
    }

    //public static string getStringFromSession(Section mySession)
    //{
    //    // Session[RunningCache.Strwhere] = "";
    //    String outValue = "";
    //    try
    //    {
    //        outValue = mySession.ToString();

    //    }
    //    catch (Exception ex)
    //    {
    //        outValue = "";
    //        System.Web.HttpContext.Current.Response.Redirect("Login.aspx?erID=1");
    //    }
    //    return outValue;
    //}


    public static void bindGv(GridView gv, DataTable Dt)
    {
        //DataTable Dt = objScolarite.getScolarite(" where etudID = " + Session[RunningCache.etudID].ToString() + "  order by  scolariteID ");
        gv.DataSource = Dt;
        gv.DataBind();
    }

    //public static void bindHtmlSelect(HtmlInputControl Select , Text)
    //{
    //    for (int i = 0; i < dtObjSector.Rows.Count; i++)
    //    {
    //        ddlSectors.Items.Add(new ListItem(dtObjSector.Rows[i]["MilSectorName"].ToString(), dtObjSector.Rows[i]["SectorID"].ToString()));
    //    }
    //}

    public static void bindDropDownList(DropDownList ddl, DataTable dataTable, string TextField, string ValueField, string defaultTextField)
    {

        DataRow dr = dataTable.NewRow();

        dr[0] = 0;
        dr[1] = defaultTextField.ToString();
        dataTable.Rows.InsertAt(dr, 0);
        dataTable.AcceptChanges();

        ddl.DataSource = dataTable;
        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();

    }

    public static void bindDropDownListCol(DropDownList ddl, DataTable dataTable, string TextField, string ValueField, string defaultTextField, string defaultValueField)
    {

        
        DataRow dr = dataTable.NewRow();

        if (string.IsNullOrEmpty(defaultValueField.Trim()) == false)
        {
            if (defaultValueField.Trim() == "0")
            {
                dr[ValueField] = 0;
            }
            else
            {
                dr[ValueField] = "";
            }

        }
        else
        {
            dr[ValueField] = "";
        }

        dr[TextField] = defaultTextField.ToString();
        dataTable.Rows.InsertAt(dr, 0);
        dataTable.AcceptChanges();

        ddl.DataSource = dataTable;
        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();

    }

    public static void bindDropDownList(DropDownList ddl, DataTable dataTable, string TextField, string ValueField)
    {

        // DataRow dr = dataTable.NewRow();

        // dr[0] = "";
        // dr[1] = defaultTextField.ToString();
        // dataTable.Rows.InsertAt(dr, 0);
        //  dataTable.AcceptChanges();

        ddl.DataSource = dataTable;
        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();

    }

    public static void bindDropDownList2(DropDownList ddl, DataTable dataTable, string TextField, string ValueField, string defaultTextField)
    {

        DataRow dr = dataTable.NewRow();

        dr[0] = "";
        dr[1] = defaultTextField.ToString();
        dataTable.Rows.InsertAt(dr, 0);
        dataTable.AcceptChanges();

        ddl.DataSource = dataTable;
        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();

    }

    public static void bindDropDownList2(DropDownList ddl, DataTable dataTable, string TextField)
    {

        DataRow dr = dataTable.NewRow();

        dr[0] = "All";
       // dr[1] = defaultTextField.ToString();
        dataTable.Rows.InsertAt(dr, 0);
        dataTable.AcceptChanges();

        ddl.DataSource = dataTable;
        ddl.DataTextField = TextField;
        ddl.DataValueField = TextField;
        ddl.DataBind();

    }

    public static void bindDropDownList2(DropDownList ddl, DataTable dataTable, string TextField, string ValueField)
    {

        //DataRow dr = dataTable.NewRow();

        //dr[0] = "";
        //dr[1] = defaultTextField.ToString();
        //dataTable.Rows.InsertAt(dr, 0);
        //dataTable.AcceptChanges();

        ddl.DataSource = dataTable;
        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();

    }

    public static string getDate(string OldDate)
    {
        String NewDate = "";
        try
        {
            int index = OldDate.IndexOf("/");

            string[] stringSeparators = new string[] { "/" };
            string[] TextDate = OldDate.Split(stringSeparators, StringSplitOptions.None);

            if (TextDate.Length >= 3)
            {
                String DD = TextDate[0];
                String MM = TextDate[1];
                String YY = TextDate[2].Substring(0,4);

                NewDate = YY + "-" + MM + "-" + DD;
                //if (YY.Length != 4)
                 /// NewDate = "1800-01-01";
            }
            else
            {
                NewDate = "1800-01-01";
            }
        }
        catch
        {
            NewDate = "1800-01-01";
        }


        return NewDate;

    }

    public static string setDateString(string OldDate)
    {
        //befor
        //input = input.Substring(input.IndexOf("/"));

        //after
        //input = input.Substring(0, input.IndexOf("/") + 1);
        String NewDate = "";
        try
        {
            int index = OldDate.IndexOf("/");

            string[] stringSeparators = new string[] { "/" };
            string[] TextDate = OldDate.Split(stringSeparators, StringSplitOptions.None);

            if (TextDate.Length >= 3)
            {
                String DD = TextDate[0];
                String MM = TextDate[1];
                int yyuy = TextDate[2].IndexOf(" ");
                String YYPlusTime = TextDate[2].ToString().Trim();
                String YY = YYPlusTime.Substring(0, YYPlusTime.IndexOf(" "));

                NewDate = YY + "-" + MM + "-" + DD;
                DateTime dtt = new DateTime(int.Parse(YY), int.Parse(MM), int.Parse(DD));
            }
            else
            {
                NewDate = "1800-01-01";
            }
        }
        catch
        {
            NewDate = "1800-01-01";
        }


        return NewDate;

    }

    public static string getDateFromDataBase(string OldDate)
    {

        int index = OldDate.IndexOf("/");


        string[] stringSeparators = new string[] { "/" };
        string[] TextDate = OldDate.Split(stringSeparators, StringSplitOptions.None);
        String NewDate = "";
        if (TextDate.Length >= 3)
        {
            String DD = TextDate[0];
            String MM = TextDate[1];
            String YY = TextDate[2];

            NewDate = DD + "/" + MM + "/" + YY;

        }

        return NewDate;

    }

    public static string getDateFromDataBase2(string OldDate)
    {
        string myOldDate = "";
        try
        {
            myOldDate = Convert.ToDateTime(OldDate).ToShortDateString();
        }
        catch { }

        int index = myOldDate.IndexOf("/");

        string myNewDate = "";

        if (index >= 2)
        {
            string[] stringSeparators = new string[] { "/" };
            string[] TextDate = myOldDate.Split(stringSeparators, StringSplitOptions.None);
            String DD = TextDate[0];
            String MM = TextDate[1];
            String YY = TextDate[2];

            myNewDate = DD + "-" + monthName(MM) + "-" + YY;

        }

        return myNewDate;

    }

    public static string monthName(string monthNumber)
    {
        int value = 0;
        try
        {
            value = int.Parse(monthNumber);
        }
        catch
        { }

        string myMonthName = "";

        switch (value)
        {
            case 1:
                myMonthName = "Jan";
                break;
            case 2:
                myMonthName = "Fev";
                break;
            case 3:
                myMonthName = "Mar";
                break;
            case 4:
                myMonthName = "Avr";
                break;
            case 5:
                myMonthName = "Mai";
                break;
            case 6:
                myMonthName = "Jun";
                break;
            case 7:
                myMonthName = "Jul";
                break;
            case 8:
                myMonthName = "Aou";
                break;
            case 9:
                myMonthName = "Sep";
                break;
            case 10:
                myMonthName = "Oct";
                break;
            case 11:
                myMonthName = "Nov";
                break;
            case 12:
                myMonthName = "Dec";
                break;
        }

        return myMonthName;
    }


    public static void SetDdlSelectedValue(DropDownList ddl, string valueToselect)
    {
        try
        {
            if (ddl.Items.Count > 0)
            {
                ddl.ClearSelection();
                if (string.IsNullOrEmpty(valueToselect) == false)
                {
                    ddl.Items.FindByValue(valueToselect).Selected = true;
                }
                else
                {
                    ddl.SelectedIndex = 0;
                }
            }
        }
        catch
        {

        }
    }


    public static DataTable GetData(string strql, SqlConnection conn)
    {
        DataTable dt = new DataTable();
        string cmd = strql;
        SqlDataAdapter adp = new SqlDataAdapter(cmd, conn);
        adp.Fill(dt);
        return dt;
    }



    public static void AddListItemsToSelect( System.Web.UI.HtmlControls.HtmlSelect SelectDDlID, DataTable Dt, string textValue, string IDvalue,string defaultValue)
    {
        if (defaultValue.Trim() != "")
        {
            SelectDDlID.Items.Add(new ListItem("All", ""));
        }
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            SelectDDlID.Items.Add(new ListItem(Dt.Rows[i][textValue].ToString(), Dt.Rows[i][IDvalue].ToString()));
        }

    }

    public static void AddListItemsToSelect(System.Web.UI.HtmlControls.HtmlSelect SelectDDlID, DataTable Dt, string textValue, string IDvalue)
    {
        SelectDDlID.Items.Add(new ListItem("All", "0"));
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            SelectDDlID.Items.Add(new ListItem(Dt.Rows[i][textValue].ToString(), Dt.Rows[i][IDvalue].ToString()));
        }

    }

    public static decimal Division(int dividande, int diviseur)
    {
        decimal result;
        try
        {


            if ((dividande != 0) && (diviseur != 0))
            {
                result = decimal.Parse(dividande.ToString()) / decimal.Parse(diviseur.ToString());
            }
            else
            {
                result = 0;
            }
        }
        catch
        {
            result = 0;
        }


        return result;

    }




    /// <summary>
    /// Global variable storing important stuff.
    /// </summary>
    static string _importantData;

    /// <summary>
    /// Get or set the static important data.
    /// </summary>
    public static string ImportantData
    {
        get
        {
            return _importantData;
        }
        set
        {
            _importantData = value;
        }
    }




    #endregion


    // Supporting function that converts an image to base64.
    public static string PhotoBase64ImgSrc(string fileNameandPath)
    {
        byte[] byteArray = File.ReadAllBytes(fileNameandPath);
        string base64 = Convert.ToBase64String(byteArray);

        return string.Format("data:image/gif;base64,{0}", base64);
    }

   


    #region   GueryString encryption

    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public static string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }


    #endregion



}