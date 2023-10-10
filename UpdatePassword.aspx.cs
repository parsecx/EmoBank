using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Data.SqlClient;

public partial class UpdatePassword : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    clsencrypt.classxx.Class1 c = new clsencrypt.classxx.Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["UserId"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {

                Response.Redirect(ResolveUrl("~/index.aspx"));
            }
           else
            {
               if (!IsPostBack)
                {
                }
            }
        }

         else
         {
             Response.Redirect(ResolveUrl("~/index.aspx"));
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
            lblmsg.InnerHtml = Common.ShowMessage("Password must be minimum 8 character", 2);
            
        }
        else if (Common.valid_password1(hdnew.Value) == false)
        {
            lblmsg.InnerHtml = Common.ShowMessage("Password must be minimum 8 character and combination of alphabets (A-Z ),(a-z)and numerics(0 to 9) and special character", 2);
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
                lblmsg.InnerHtml = Common.ShowMessage("Fill Old Password", 2);
            }
            else if (newPwd.Trim().Length == 0)
            {
                lblmsg.InnerHtml = Common.ShowMessage("Fill New Password", 2);
            }

            else if (newPwd.Trim() != conPwd.Trim())
            {
                lblmsg.InnerHtml = Common.ShowMessage("Check New Passowrd and Confirm Password", 2);
            }
            else if (newPwd.Length >= 6)
            {
                if (newPwd == conPwd)
                {

                    SqlParameter[] oPara ={
                                           new SqlParameter("@Email",SqlDbType.VarChar),
                                           new SqlParameter("@Pswd",SqlDbType.VarChar),
                                            new SqlParameter("@rval",SqlDbType.Int)

                                       };

                    oPara[0].Value = Session["Email"].ToString();
                    oPara[1].Value = oldp;
                    oPara[2].Direction = ParameterDirection.ReturnValue;


                    int a = cls.ExecuteNonQuery("[sp_CheckPswd]", oPara, CommandType.StoredProcedure);
                    if (Convert.ToInt16(oPara[2].Value) == 1)
                    {
                        clsencrypt.classxx.Class1 encry = new clsencrypt.classxx.Class1();
                        string encry_password = encry.encrypt(newPwd);
                        SqlParameter[] opare ={
                                           new SqlParameter("@Email",SqlDbType.VarChar),
                                           new SqlParameter("@NewPswd",SqlDbType.VarChar),
                                            new SqlParameter("@rval",SqlDbType.Int)

                                       };
                        opare[0].Value = Session["Email"].ToString(); 
                        opare[1].Value = encry_password;
                        opare[2].Direction = ParameterDirection.ReturnValue;
                        int irval = cls.ExecuteNonQuery("sp_ChangePassword", opare, CommandType.StoredProcedure);
                        if (Convert.ToInt16(opare[2].Value) == 1)
                        {
                            lblmsg.InnerHtml = Common.ShowMessage("Password Changed Succssfully", 1);
                        }
                        else
                        {
                            lblmsg.InnerHtml = Common.ShowMessage("Some Error Occure", 2);
                        }
                    }
                    else
                    {
                        lblmsg.InnerHtml = Common.ShowMessage("Old Password is InCorrect", 2);
                    }

                }

            }

        }

    }
}