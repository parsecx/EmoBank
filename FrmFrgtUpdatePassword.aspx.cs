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
public partial class FrmFrgtUpdatePassword : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    clsencrypt.classxx.Class1 c = new clsencrypt.classxx.Class1();
    string encry_Newpassword;
    string encry_Oldpassword;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                string Email = "";
                try
                {
                    Email = Request.QueryString["Id"].ToString();
                    Email = encrydecryp.Decrypt(Email);
                    Email = Email.Substring(0, Email.IndexOf("@#$"));
                    txtEmail.Text = Email;
                    if (CheckEmail().Rows.Count <= 0)
                    {
                        divMain.Visible = false;
                        lblmsg.InnerHtml = Common.ShowMessage("Collegamento Expired.Please Rigenera", 2);
                    }
                    else
                    {
                        divMain.Visible = true ;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect(ResolveUrl("~/Index.aspx"));
                }


            }
        }

    }
    protected void btnForgot_Click(object sender, EventArgs e)
    {

        string newPwd = "";
        string conPwd = "";
        string oldp = "";
        int x = hdnew.Value.Length - 6;
        if (hdnew.Value.Length < 20)
        {
            lblmsg.InnerHtml = Common.ShowMessage("Deve essere la password minimo in 8 caratteri", 2);

        }
        else if (Common.valid_password1(hdnew.Value) == false)
        {
            //lblmsg.InnerHtml = Common.ShowMessage("Deve essere la password in almeno 8 caratteri e combinazione Lettere (dalla A alla Z), (az), e numerici (da 0 a 9) e carattere speciale", 2);
            lblmsg.InnerHtml = Common.ShowMessage("La Password deve essere composta da lettere alfabetiche  Maiuscole e Minuscole, Numeri  da 0 a 9, Almeno un carattere speciale  (,.?= ecc.)", 2);
        }

        else
        {

            newPwd = hdnew.Value.Substring(4, 3) + hdnew.Value.Substring(11, 3) + hdnew.Value.Substring(18);

            int y = hdconfirm.Value.Length - 6;
            conPwd = hdconfirm.Value.Substring(4, 3) + hdconfirm.Value.Substring(11, 3) + hdconfirm.Value.Substring(18);

            int z = hdold.Value.Length - 6;
            oldp = hdold.Value.Substring(4, 3) + hdold.Value.Substring(11, 3) + hdold.Value.Substring(18);
            if (oldp.Trim().Length == 0)
            {
                lblmsg.InnerHtml = Common.ShowMessage("Riempire Vecchia password", 2);
            }
            else if (newPwd.Trim().Length == 0)
            {
                lblmsg.InnerHtml = Common.ShowMessage("Riempire nuova password", 2);
            }

            else if (newPwd.Trim() != conPwd.Trim())
            {
                lblmsg.InnerHtml = Common.ShowMessage("Controllare La Nuova password Password e Conferma", 2);
            }
            else if (newPwd.Length >= 6)
            {
                if (newPwd == conPwd)
                {

                    DataTable tbb=CheckEmail();
                    if (tbb.Rows.Count > 0)
                    {

                        encry_Oldpassword = tbb.Rows[0]["Pswd"].ToString();
                        encry_Newpassword = c.encrypt(oldp);
                        if (encry_Newpassword == encry_Oldpassword)
                        {
                            clsencrypt.classxx.Class1 encry = new clsencrypt.classxx.Class1();
                            string encry_password = encry.encrypt(newPwd);
                            SqlParameter[] opare ={
                                           new SqlParameter("@Email",SqlDbType.VarChar),
                                           new SqlParameter("@NewPswd",SqlDbType.VarChar),
                                            new SqlParameter("@rval",SqlDbType.Int)

                                       };
                            opare[0].Value = txtEmail.Text;
                            opare[1].Value = encry_password;
                            opare[2].Direction = ParameterDirection.ReturnValue;
                            int irval = cls.ExecuteNonQuery("sp_ChangePassword", opare, CommandType.StoredProcedure);
                            if (Convert.ToInt16(opare[2].Value) == 1)
                            {
                                lblmsg.InnerHtml = Common.ShowMessage("Password modificata Succssfully", 1);
                                Response.Redirect(ResolveUrl("LandingPage.aspx"));
                            }
                            else
                            {
                                lblmsg.InnerHtml = Common.ShowMessage("Alcuni errori occure", 2);
                            }
                        }
                        else
                        {
                            lblmsg.InnerHtml = Common.ShowMessage("Password temporanea è corretto prego Rigenera ", 2);
                        }

                    }
                    else
                    {
                        lblmsg.InnerHtml = Common.ShowMessage("Collegamento Expired.Please Rigenera", 2);
                    }


                }

            }




        }
    }
    public DataTable CheckEmail()
    {
        SqlParameter[] oPara ={
                                           new SqlParameter("@Email",SqlDbType.VarChar),
                                           new SqlParameter("@Pswd",SqlDbType.VarChar),
                                           };

        oPara[0].Value = txtEmail.Text;
        oPara[1].Value = "";


        DataTable tbb = cls.ExecuteDataTable("sp_CheckTempPswd", oPara, CommandType.StoredProcedure);
        return tbb;

      
    }
}