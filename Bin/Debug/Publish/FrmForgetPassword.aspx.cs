﻿using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Net;
public partial class FrmForgetPassword : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    clsencrypt.classxx.Class1 c = new clsencrypt.classxx.Class1();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnForgot_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
        {
            lblmsg.InnerHtml = Common.ShowMessage("Enter E-mail", 2);
        }
        else
        {


            SendEmail();
           
            
        }
    }
    private void SendEmail()
    {
        //FormDesign/Handler.ashx
        //FormDesign/Handler.ashx?StudentColID=" + Request.QueryString["StudentColID"]
        //string strUrl = Common.GetProjectLocation(false) + "Admission/FormDesign/Handler.ashx?StudentColID=" + ViewState["studentcolid"];

        string SenderEmailId = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
        string SenderEmailIdPwd = System.Configuration.ConfigurationManager.AppSettings["SenderEmailPwd"].ToString();

        string strUrl = Common.GetProjectLocation(false) + "FrmFrgtUpdatePassword.aspx?";

        var strBldr = new StringBuilder();
        System.Random rdmPswd = new Random();
        int pswd = rdmPswd.Next(10000000, 99999999);
        clsencrypt.classxx.Class1 encry = new clsencrypt.classxx.Class1();
        string encry_password = encry.encrypt(pswd.ToString());
        SqlParameter[] Opara =
            {
              new SqlParameter("@Email", txtEmail.Text),
              new SqlParameter("@Pswd", encry_password)
            };
        DataTable tbb = cls.ExecuteDataTable("sp_SaveEmobank_UsersID", Opara, CommandType.StoredProcedure);
        if (tbb.Rows.Count > 0)
        {
            string Path = ResolveUrl("" + strUrl + "Id=" + encrydecryp.Encrypt(tbb.Rows[0]["Email"].ToString() + "@#$"));

            //Click here to print your application Form <br/>
            //OR<br />Copy and paste link below to Edit your Application Form<br/><br /><a>" + strUrl + "</a>
            strBldr.Append("<table style='width: 230px'><tr><td colspan='7'><div style='background-color: CornflowerBlue; '><h2 style='color: White;  text-align:center;'>Dear " + tbb.Rows[0]["UserID"] + "</h2></div></td></tr><tr><td colspan='7' style='font-size:16px !important'>Your Temporary Password is:-  <b>" + pswd + "</b> </td><td></tr><tr><td colspan='7'>For Login please open link " + Path + "</td></tr></table> ");


            bool check = EmailSender.sendmail(SenderEmailId, SenderEmailIdPwd, SenderEmailId, txtEmail.Text, "Emobank Password Reset posta ", strBldr.ToString());

    
            if (check)
            {
                lblmsg.InnerHtml = Common.ShowMessage("Password sent to your e-mail", 1);

            }
            else
            {
                lblmsg.InnerHtml = Common.ShowMessage("Check Your Internet Connection", 2);

            }
        }
        else
        {
            lblmsg.InnerHtml = Common.ShowMessage("Inserito -mail non è registrato con noi.", 2);
        }
        // return strBldr.ToString();
    }
   
}