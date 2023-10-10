﻿using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class User_frmForgotPassword : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnForgot_Click(object sender, EventArgs e)
    {

       if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
        {
            lblmsg.InnerHtml = Common.ShowMessage("Enter Email", 2);
        }
        else
        {
//            SqlParameter[] Opare=
//            {
//new SqlParameter("EmailId", txtEmail.Text)
//            };


//            DataTable tb = cls.ExecuteDataTable("sp_GetEmobank_UsersID", Opare, CommandType.StoredProcedure);
//            if (tb.Rows.Count>0)
//            {
                System.Random rdmPswd = new Random();
                int pswd = rdmPswd.Next(10000000, 99999999);
                SqlParameter[] Opara =
            {
              new SqlParameter("@Email", txtEmail.Text),
              new SqlParameter("@Pswd", pswd)
            };

                DataTable tbb = cls.ExecuteDataTable("sp_SaveEmobank_UsersID", Opara, CommandType.StoredProcedure);
                if (tbb.Rows.Count>0)
                {
                    var strBldr = new StringBuilder();
                    string Path = ResolveUrl("UpdatePassword.aspx?Id=" + encrydecryp.Encrypt(tbb.Rows[0]["Email"].ToString() + "@#$"));

                    strBldr.Append("<body><table><tr><td><div style='background-color: #CEE3F6;' ><table style='width: 820px'><tr><td colspan='3'><div style='background-color: CornflowerBlue; width: 820px;'><h2 style='color: White;  text-align:center;'>Dear " + tbb.Rows[0]["UserID"] + "</h2></div></td></tr><tr><td style='font-size:16px !important'>Your Temporary Password is:-  <b>" + pswd + "</b> ,</td><td>For Login please open link " + Path + "</td></tr><tr></table></body></html> ");


                    lblmsg.InnerHtml = Common.ShowMessage(strBldr.ToString(),1);
                   // lblmsg.InnerHtml = Common.ShowMessage("Instructions sent on your Email Id.", 1);
                    txtEmail.Text = string.Empty;
                }
       //}
            else
            {
                lblmsg.InnerHtml = Common.ShowMessage("Entered Email is not registered with us.", 2);
            }
        }
    }
}