using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for EmailSender
/// </summary>
public static class EmailSender
{
    public static bool sendmail(string AuthenticateID, string Password, string FromEmailid, string ToEmail, string Subject, string Body)
    {
        try
        {

            string Host = System.Configuration.ConfigurationManager.AppSettings["hostname"].ToString();
            int Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());
            using (MailMessage mm = new MailMessage(FromEmailid, ToEmail))
            {
                mm.Subject = Subject;
                mm.Body = Body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new NetworkCredential(FromEmailid, Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = Port;
                smtp.Send(mm);
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}