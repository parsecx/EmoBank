﻿using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    SqlHelper SqlHelp = new SqlHelper();
    string UserId = "";
    string UserType = "";
    string Email = "";
    bool IsChanged = false;
    string Name = "";
    string Sample_Clinic_Code = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Login1())
        {
            string guid = Guid.NewGuid().ToString();

            Session["AuthToken"] = guid;
            // now create a new cookie with this guid value  
            Response.Cookies.Add(new HttpCookie("AuthToken", guid));
            if (IsChanged == true)
            {

                Response.Redirect(ResolveUrl("LandingPage.aspx"));
            }
            else
            {
                Response.Redirect(ResolveUrl("FrmFrgtUpdatePassword.aspx?Id=" + encrydecryp.Encrypt(Session["Email"].ToString() + "@#$")));
            }

        }
        else
        {
            lblMsg.InnerHtml = Common.ShowMessage("Credenziali utente sono sbagliate.", 2);
        }
    }
    private bool Login1()
    {
        string userName = inputName.Value.Trim();
        string encry_password = inputPassword.Text.Trim();

        string hPassword, sPassword;
        int result = IsValidUser(userName, out hPassword, out sPassword, out UserId, out UserType);
        if (result > 0)
        {
            clsencrypt.classxx.Class1 encry = new clsencrypt.classxx.Class1();
            encry_password = encry.encrypt(inputPassword.Text);
            if (encry_password == hPassword)
            {
                Session["UserId"] = UserId;
               
                Session["userName"] = userName;
                Session["Email"] = Email;
                Session["NameUser"] = Name;
                Session["UserType"] = UserType;
                Session["Sample_Clinic_Code"] = Sample_Clinic_Code;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private int IsValidUser(string userName, out string hPassword, out string sPassword, out string UserId, out string UserType)
    {
        hPassword = string.Empty;
        sPassword = string.Empty;
        UserId = string.Empty;
        UserType = string.Empty;
        Email = string.Empty;
        IsChanged = false;
        SqlParameter[] oPera =
        {
            new SqlParameter("@UserName",inputName.Value),
        };

        DataTable tb = SqlHelp.ExecuteDataTable("Select * from Emobank_Users where UserID=@UserName", oPera, CommandType.Text);
        if (tb.Rows.Count > 0)
        {

            hPassword = tb.Rows[0]["PSW"].ToString();
            sPassword = tb.Rows[0]["PSW"].ToString();
            UserId = tb.Rows[0]["CodID"].ToString();
            UserType = tb.Rows[0]["TypeUser"].ToString();
            Email = tb.Rows[0]["Email"].ToString();
            IsChanged = tb.Rows[0]["IsChanged"].ToString() == "True" ? true : false;
            Name = tb.Rows[0]["NameUser"].ToString();
            Sample_Clinic_Code = tb.Rows[0]["CodeID_Point_Clinic"].ToString();
            return 1;
        }
        return 0;


    }
}