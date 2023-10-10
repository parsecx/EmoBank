using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Security.Cryptography;
/// <summary>
/// Summary description for Class1
/// </summary>
public static class Common
{
    public static string ShowMessage(string Message, int msgType)
    {
        string Msg = string.Empty;
        if (msgType == 1)
            Msg = "<Div class='alert alert-success fade in'>" + Message + " </Div>";
        else if (msgType == 2)
            Msg = "<Div class='alert alert-error fade in'>" + Message + " </Div>";
        else if (msgType == 3)
            Msg = "<Div class='alert alert-info fade in'>" + Message + " </Div>";
        return Msg;
    }
    public static bool valid_address(string address)
    {
        //Contact Address can have alphabets numbers . , ( ) space only.

        string strRegex = @"^[a-zA-Z0-9\.\,\(\)\s]+$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(address))
            return (true);
        else
            return (false);
    }


    // Valid alphabets including numeric
    public static bool valid_alphabet(string alphabet)
    {
        string strRegex = @"^[a-zA-Z0-9\.\,\s]+$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(alphabet))
            return (true);
        else
            return (false);
    }


    //City Name can have alphabets . space only.
    public static bool valid_city(string city)
    {

        string strRegex = @"^[a-zA-Z\.\s]+$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(city))
            return (true);
        else
            return (false);
    }




    //Password must be minimum 8 character and combination of alphabets (A-Z )or(a-z)and numerics(0 to 9)


    public static bool valid_password1(string pwd)
    {

        string strRegex = "^.*(?=.{8,20})(?=.*)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).*$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(pwd))
            return (true);
        else
            return (false);
    }




    //Password must be minimum 6 character and combination of alphabets (A-Z )or(a-z)and numerics(0 to 9)


    public static bool valid_password(string pwd)
    {

        string strRegex = "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,10})$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(pwd))
            return (true);
        else
            return (false);
    }

    // Email Validation

    public static bool Valid_Email(string Email)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(Email))
            return (true);
        else
            return (false);
    }

    // Numeric value Validation

    public static bool Valid_Numeric(string value)
    {
        //string strRegex = @"^[0-9]+$";
        //Regex re = new Regex(strRegex);
        //if (re.IsMatch(value))
        //    return (true);
        //else
        //    return (false);
        try
        {
            Convert.ToInt64(value);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    // Date validation


    public static bool Valid_Date(string date)
    {
        string strRegex = @"^([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]([1-9]|0[1-9]|1[012])[- /.][0-9]{4}$";

        Regex re = new Regex(strRegex);
        if (re.IsMatch(date))
            return (true);
        else
            return (false);
    }


    //Only Character validation

    public static bool Valid_Character(string text)
    {
        string strRegex = @"^([a-zA-Z\s])";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(text))
            return (true);
        else
            return (false);
    }




    public static string filterBadchars2(string str)
    {
        string newchars;
        int i;
        //string[] badchars = new string[15] { "[script]"," select ", ":" ,";", "--", " insert ", " delete ", "sp_", "xp_", "(", ")", " and ", " or ", "union", " drop ", ";" };
        string[] badchars = new string[41] 
        { 
            "[/script]", "[script]", " select ", ":" , "*" , ";", "--", " insert ", " delete ", "sp_", "xp_", " and ", " or ", " union ", "drop","=","<",">","<>","< >","?","~","!","^","<html>", "'", "''" , "|", "&" , "$" , "%" , "@" , "\'" , "\''" , "()" , "+" , " CR " , " LF " , "," , "(" , ")" 
        };
        int n = badchars.Length;
        //newchars = str.ToLower().ToString();
        newchars = str.ToString();
        for (i = 0; i < n; i++)
        {
            newchars = newchars.Replace(badchars[i], "");
            // Response.Write(badchars[i]);
        }
        return newchars;
    }




    public static string filterBadchars1(string str)
    {
        string newchars;
        int i;
        //string[] badchars = new string[15] { "[script]"," select ", ";", "--", " insert ", " delete ", "sp_", "xp_", "(", ")", " and ", " or ", "union", " drop ", ";" };
        string[] badchars = new string[39] 
        { 
            "[/script]", "[script]", "select", ";", "--", "insert", "delete", "sp_", "xp_", " and ", "or", "union", "drop","=","<",">","<>","< >","?","~","!","^","<html>", "'", "''" , "|", "&" , "$" , "%" , "@" , "\'" , "\''" , "()" , "+" , "CR" , "LF" , "," , "(" , ")" 
        };
        int n = badchars.Length;
        //newchars = str.ToLower().ToString();
        newchars = str.ToString();
        for (i = 0; i < n; i++)
        {
            newchars = newchars.Replace(badchars[i], "");
            // Response.Write(badchars[i]);
        }
        return newchars;
    }

    public static string filterBadchars_ProperCase1(string str)
    {
        string newchars;
        int i;
        //string[] badchars = new string[15] { "[script]"," select ", ";", "--", " insert ", " delete ", "sp_", "xp_", "(", ")", " and ", " or ", "union", " drop ", ";" };
        string[] badchars = new string[24] 
{ 
    "[/script]", "[script]", "select", ";", "--", "insert", "delete", "sp_", "xp_", " and ", "or", "union", "drop", ";","<",">","<>","< >","?","~","!","^","--","<html>"
};
        int n = badchars.Length;
        newchars = str.ToString();
        for (i = 0; i < n; i++)
        {
            newchars = newchars.Replace(badchars[i], "");
            // Response.Write(badchars[i]);
        }
        return newchars;
    }



    public static string filterBadchars(string str)
    {
        string newchars;
        int i;
        //string[] badchars = new string[15] { "[script]"," select ", ";", "--", " insert ", " delete ", "sp_", "xp_", "(", ")", " and ", " or ", "union", " drop ", ";" };
        string[] badchars = new string[22] 
{ 
    "[/script]", "[script]", "select", ";", "--", "insert", "delete", "sp_", "xp_", " and ", "or", "union", "drop", ";","<",">","<>","< >","'","&","?","<html>"
};
        int n = badchars.Length;
        newchars = str;
        for (i = 0; i < n; i++)
        {
            newchars = newchars.Replace(badchars[i], "");
            // Response.Write(badchars[i]);
        }
        return newchars;
    }

    public static bool Valid_Year(string value)
    {
        string strRegex = @"^[0-9\/\-]+$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(value))
            return (true);
        else
            return (false);
    }

    public static bool valid_name(string city)
    {

        string strRegex = @"^[a-zA-Z\.\s]+$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(city))
            return (true);
        else
            return (false);
    }
    private static string ServerVariables(string name)
    {
        string tmpS = string.Empty;
        try
        {
            if (HttpContext.Current.Request.ServerVariables[name] != null)
            {
                tmpS = HttpContext.Current.Request.ServerVariables[name].ToString();
            }
        }
        catch
        {
            tmpS = string.Empty;
        }
        return tmpS;
    }
    private static string GetProejctHost(bool useSsl)
    {
        string result = "http://" + ServerVariables("HTTP_HOST");
        if (!result.EndsWith("/"))
            result += "/";
        if (useSsl)
        {
            //shared SSL certificate URL
            string sharedSslUrl = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SharedSSLUrl"]))
                sharedSslUrl = ConfigurationManager.AppSettings["SharedSSLUrl"].Trim();

            if (!String.IsNullOrEmpty(sharedSslUrl))
            {
                //shared SSL
                result = sharedSslUrl;
            }
            else
            {
                //standard SSL
                result = result.Replace("http:/", "https:/");
            }
        }
        else
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseSSL"])
                && Convert.ToBoolean(ConfigurationManager.AppSettings["UseSSL"]))
            {
                //SSL is enabled

                //get shared SSL certificate URL
                string sharedSslUrl = string.Empty;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SharedSSLUrl"]))
                    sharedSslUrl = ConfigurationManager.AppSettings["SharedSSLUrl"].Trim();
                if (!String.IsNullOrEmpty(sharedSslUrl))
                {
                    //shared SSL

                    /* we need to set a store URL here (IoC.Resolve<ISettingManager>().StoreUrl property)
                     * but we cannot reference Nop.BusinessLogic.dll assembly.
                     * So we are using one more app config settings - <add key="NonSharedSSLUrl" value="http://www.yourStore.com" />
                     */
                    string nonSharedSslUrl = string.Empty;
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["NonSharedSSLUrl"]))
                        nonSharedSslUrl = ConfigurationManager.AppSettings["NonSharedSSLUrl"].Trim();
                    if (string.IsNullOrEmpty(nonSharedSslUrl))
                        throw new Exception("NonSharedSSLUrl app config setting is not empty");
                    result = nonSharedSslUrl;
                }
            }
        }

        if (!result.EndsWith("/"))
            result += "/";

        return result.ToLowerInvariant();
    }
    public static string GetProjectLocation(bool useSsl)
    {
        string result = GetProejctHost(useSsl);
        if (result.EndsWith("/"))
            result = result.Substring(0, result.Length - 1);
        result = result + HttpContext.Current.Request.ApplicationPath;
        if (!result.EndsWith("/"))
            result += "/";
        return result.ToLowerInvariant();
    }

    public static void SendSMS(string mobilenumber, string msg)
    {
        String smsservicetype = "singlemsg";
        HttpWebRequest request;
        request = (HttpWebRequest)WebRequest.Create("http://msdgweb.mgov.gov.in/esms/sendsmsrequest");
        request.ProtocolVersion = HttpVersion.Version10;
        ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98;DigExt)";
        request.Method = "POST";
        //For single message.
        String query = "username=" + HttpUtility.UrlEncode("CHDIT-SPIC") +
        "&password=" + HttpUtility.UrlEncode("Spic123-") + "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) + "&content=" + HttpUtility.UrlEncode(msg) + "&mobileno=" + HttpUtility.UrlEncode(mobilenumber) + "&senderid=" + HttpUtility.UrlEncode("ESMPRK");
        byte[] byteArray = Encoding.ASCII.GetBytes(query);

        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;

        Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        String Status = ((HttpWebResponse)response).StatusDescription;
        dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();
        reader.Close();
        dataStream.Close();
        response.Close();



    }
    public static string GetTxnStatus(string TransactionId)
    {

        string Url = ConfigurationManager.AppSettings["Verficationlink"];

        string method = ConfigurationManager.AppSettings["method"];
        string salt = ConfigurationManager.AppSettings["SALT"];
        string key = ConfigurationManager.AppSettings["MERCHANT_KEY"];
        string var1 = TransactionId;//Transaction ID of the merchant
        string var2 = " ";//TokenID of the merchant
        string var3 = " ";//Amount to be use in refund

        string toHash = key + "|" + method + "|" + var1 + "|" + salt;

        string Hashed = Generatehash512(toHash);

        string postString = "key=" + key +
            "&command=" + method +
            "&hash=" + Hashed +
            "&var1=" + var1 +
            "&var2=" + var2 +
            "&var3=" + var3;

        WebRequest myWebRequest = WebRequest.Create(Url);
        myWebRequest.Method = "POST";
        myWebRequest.ContentType = "application/x-www-form-urlencoded";
        myWebRequest.Timeout = 180000;
        StreamWriter requestWriter = new StreamWriter(myWebRequest.GetRequestStream());
        requestWriter.Write(postString);
        requestWriter.Close();

        StreamReader responseReader = new StreamReader(myWebRequest.GetResponse().GetResponseStream());
        WebResponse myWebResponse = myWebRequest.GetResponse();
        Stream ReceiveStream = myWebResponse.GetResponseStream();
        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        StreamReader readStream = new StreamReader(ReceiveStream, encode);

        string response = readStream.ReadToEnd();
        //JObject account = JObject.Parse(response);
        //String status = (string)account.SelectToken("transaction_details." + var1 + ".status");
        return response;
    }
    public static string Generatehash512(string text)
    {

        byte[] message = Encoding.UTF8.GetBytes(text);
        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
    public static void ExportToExcel(GridView grdControl)
    {

        string Path = HttpContext.Current.Server.MapPath("~/Files/" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".xls");
        FileInfo FI = new FileInfo(Path);
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
        grdControl.RenderControl(htmlWrite);
        string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
        stringWriter.ToString().Normalize();
        vw.Write(stringWriter.ToString());
        vw.Flush();
        vw.Close();
        WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
    }
    public static void WriteAttachment(string FileName, string FileType, string content)
    {
        HttpResponse Response = System.Web.HttpContext.Current.Response;
        Response.ClearHeaders();
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
        Response.ContentType = FileType;
        Response.Write(content);
        Response.End();
    }
}
