using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmUpdateUser : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    clsencrypt.classxx.Class1 c = new clsencrypt.classxx.Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string CodInt = Convert.ToString(Session["CodInt"]);
            if (!string.IsNullOrEmpty(CodInt))
            {
                BindDropdowns();
                BindUserDetailById(CodInt);
            }
            else
            {
                Session.Remove("CodInt");
                Response.Redirect("frmUsers.aspx");
            }
        }
    }
    private void BindDropdowns()
    {
        DataTable dt = cls.ExecuteDataTable("GetExpiaryDates", null, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            Expirydate();
        }
        ddlScadenza.DataSource = dt;
        ddlScadenza.DataTextField = "ExpiaryType";
        ddlScadenza.DataValueField = "ExpiaryMonths";
        ddlScadenza.DataBind();

        DataTable dt1 = cls.ExecuteDataTable("Select * from tbUserType", null, CommandType.Text);
        ddltipo.DataSource = dt1;
        ddltipo.DataTextField = "UserTypeName";
        ddltipo.DataValueField = "UserType";
        ddltipo.DataBind();
        ddltipo.SelectedValue = "U";
    }
    private void BindUserDetailById(string CodID)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",CodID),
        };
        DataTable dt = cls.ExecuteDataTable("BindUserDetailByCodID", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            txtCodice.Text = dt.Rows[0]["CodID"].ToString();
            txtDataScadenza.Text = Convert.ToDateTime(dt.Rows[0]["PSWDeadline"]).ToString("dd-MM-yyyy");
            txtDenominazione.Text = dt.Rows[0]["NameUser"].ToString();
            txtE_mail.Text = dt.Rows[0]["Email"].ToString();
            txtNote.Text = dt.Rows[0]["Note"].ToString();
            txtPSW.Text = dt.Rows[0]["Psw"].ToString();
            txtTelefono.Text = dt.Rows[0]["Phone"].ToString();
            txtUserId.Text = dt.Rows[0]["UserID"].ToString();
            ddlScadenza.SelectedValue = dt.Rows[0]["TypeExpiry"].ToString();
            ddltipo.SelectedValue = dt.Rows[0]["TypeUser"].ToString();
            chkGestione.Items[0].Selected = dt.Rows[0]["GestioneUtentieAccessi"].ToString() == "N" ? false : true;
            chkGestioneAnagrafiche.Items[0].Selected = dt.Rows[0]["BancaSangue/Emoderivati"].ToString() == "N" ? false : true;
            chkMovimentoR.Items[0].Selected = dt.Rows[0]["MovimentoRichiesteCliniche"].ToString() == "N" ? false : true;
            ChkBchkAmbulatori.Items[0].Selected = dt.Rows[0]["AmbulatoriPuntiPrelievo"].ToString() == "N" ? false : true;
            chkMivimenti.Items[0].Selected = dt.Rows[0]["MovimentiPrelievi"].ToString() == "N" ? false : true;
            chkAnalisi.Items[0].Selected = dt.Rows[0]["AnalisiStatistichePrelievi"].ToString() == "N" ? false : true;
            chkAnagrafica.Items[0].Selected = dt.Rows[0]["AnagraficaCliniche"].ToString() == "N" ? false : true;
            chkAnalisiC.Items[0].Selected = dt.Rows[0]["AnalisiConsegneCliniche"].ToString() == "N" ? false : true;
        }
        else
        {
            ClearInput();
            lblmsg.InnerHtml = Common.ShowMessage("No Record Found", 2);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Savedata();
    }
    private void Expirydate()
    {
        if (ddlScadenza.SelectedValue == "99")
        {
            txtDataScadenza.Text = "31/12/2050";
        }
        else if (string.IsNullOrEmpty(ddlScadenza.SelectedValue))
        {
            txtDataScadenza.Text = "31/12/2050";
        }
        else
        {
            txtDataScadenza.Text = DateTime.Now.AddMonths(Convert.ToInt32(ddlScadenza.SelectedValue)).ToShortDateString();
        }
        txtDataScadenza.Attributes.Remove("class");
        txtDataScadenza.Attributes.Add("class", "form-control");
    }
    private void Savedata()
    {
        if (Validate())
        {
            lblmsg.InnerHtml = "";
            string PSWDeadline = "12/31/2050";
            clsencrypt.classxx.Class1 encry = new clsencrypt.classxx.Class1();
            string encry_password = encry.encrypt(txtPSW.Text);

            if (ddlScadenza.SelectedValue == "99")
            {
                PSWDeadline = "12/31/2050";
            }
            else
            {
                PSWDeadline = DateTime.Now.AddMonths(Convert.ToInt32(ddlScadenza.SelectedValue)).ToString("MM-dd-yyyy");
            }
            SqlParameter[] oPera =
        {
            new SqlParameter("@UserID",txtUserId.Text),
             new SqlParameter("@NameUser",txtDenominazione.Text),
            new SqlParameter("@Psw",""),
            new SqlParameter("@TypeExpiry",ddlScadenza.SelectedValue),
            new SqlParameter("@PSWDeadline",PSWDeadline),
            new SqlParameter("@Phone",txtTelefono.Text),
            new SqlParameter("@Email",txtE_mail.Text),
            new SqlParameter("@TypeUser",ddltipo.SelectedValue),
            new SqlParameter("@Note",txtNote.Text),
            new SqlParameter("@GestioneUtentieAccessi",chkGestione.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@BancaSangue",chkGestioneAnagrafiche.SelectedValue=="1"?"Y":"N"  ),
            new SqlParameter("@AmbulatoriPuntiPrelievo",ChkBchkAmbulatori.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@MovimentiPrelievi",chkMivimenti.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@AnalisiStatistichePrelievi",chkAnalisi.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@AnagraficaCliniche",chkAnagrafica.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@MovimentoRichiesteCliniche",chkMovimentoR.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@AnalisiConsegneCliniche",chkAnalisiC.SelectedValue=="1"?"Y":"N" ),
              new SqlParameter("@Type","U" ),
               new SqlParameter("@CodeId", txtCodice.Text ),  
           
           
        };
            int result = cls.ExecuteNonQuery("UpdateEmoUser", oPera, CommandType.StoredProcedure);
            if (result > 0)
            {
                ClearInput();
                lblmsg.InnerHtml = Common.ShowMessage("Record Updated Successfully.", 1);
                Response.Redirect("frmUsers.aspx");
            }
            else if (result == -1)
            {
                lblmsg.InnerHtml = Common.ShowMessage("User Id already exists. Use another one.", 2);
            }
            else if (result == 0)
            {
                lblmsg.InnerHtml = Common.ShowMessage("Error in data. Contact with administrator", 3);
            }
        }
    }
    public bool Validate()
    {
        //if (txtCodice.Text == "")
        //{
        //    lblmsg.InnerHtml = Common.ShowMessage("Enter Codic", 3);
        //    return false;
        //}

        return true;
    }
    private void ClearInput()
    {
        txtCodice.Text = string.Empty;
        txtDataScadenza.Text = string.Empty;
        txtDenominazione.Text = string.Empty;
        txtE_mail.Text = string.Empty;
        txtNote.Text = string.Empty;
        txtPSW.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        txtUserId.Text = string.Empty;
        lblmsg.InnerHtml = "";
        Session.Remove("CodInt");
    }

    protected void ddlScadenza_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydate();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session.Remove("CodInt");
        Response.Redirect("frmUsers.aspx");
    }
}