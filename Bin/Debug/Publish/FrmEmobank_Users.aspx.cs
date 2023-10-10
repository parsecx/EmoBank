﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Text;
using System.Security.Policy;

public partial class LandingPage : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    string CodInt;
    string Type;
    clsencrypt.classxx.Class1 c = new clsencrypt.classxx.Class1();
    protected void Page_Load(object sender, EventArgs e)
    {

        lblmsg.InnerText = "";
        if (Session["UserId"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {

                Response.Redirect(ResolveUrl("Login.aspx"));
            }
            else
            {

                if (!IsPostBack)
                {

                    BindDropdowns();
                    BindUsers();


                }
            }
        }

        else
        {
            Response.Redirect(ResolveUrl("Login.aspx"));
        }

        if (Convert.ToString(Session["UserType"]) == "A")
        {
            pnl.Visible = true;
        }
        else
        {
            pnl.Visible = false;
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

        DataTable dt1 = cls.ExecuteDataTable("GetUserTypes", null, CommandType.Text);
        ddltipo.DataSource = dt1;
        ddltipo.DataTextField = "UserTypeName";
        ddltipo.DataValueField = "UserType";
        ddltipo.DataBind();
        ddltipo.SelectedValue = "U";
    }
    private void BindUserDetailById(string CodID)
    {
        img1.Visible = true;
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",CodID),
        };
        DataTable dt = cls.ExecuteDataTable("BindUserDetailByCodID", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            txtPSW.Text = dt.Rows[0]["Psw"].ToString();
            txtCodice.Text = dt.Rows[0]["CodID"].ToString();
            txtDataScadenza.Text = Convert.ToDateTime(dt.Rows[0]["PSWDeadline"]).ToString("dd/MM/yyyy");
            txtDenominazione.Text = dt.Rows[0]["NameUser"].ToString();
            txtE_mail.Text = dt.Rows[0]["Email"].ToString();
            txtNote.Text = dt.Rows[0]["Note"].ToString();
            txtTelefono.Text = dt.Rows[0]["Phone"].ToString();
            txtUserId.Text = dt.Rows[0]["UserID"].ToString();
            ddlScadenza.SelectedValue = dt.Rows[0]["TypeExpiry"].ToString();
            ddltipo.SelectedValue = dt.Rows[0]["TypeUser"].ToString();
            chkGestione.Items[0].Selected = dt.Rows[0]["GestioneUtentieAccessi"].ToString() == "Y" ? true : false;
            chkGestioneAnagrafiche.Items[0].Selected = dt.Rows[0]["BancaSangue/Emoderivati"].ToString() == "Y" ? true : false;
            chkMovimentoR.Items[0].Selected = dt.Rows[0]["MovimentoRichiesteCliniche"].ToString() == "Y" ? true : false;
            ChkBchkAmbulatori.Items[0].Selected = dt.Rows[0]["AmbulatoriPuntiPrelievo"].ToString() == "Y" ? true : false;
            ChkAnagraficaDonatori.Items[0].Selected = dt.Rows[0]["AnagraficaDonatori"].ToString() == "Y" ? true : false;
            chkMivimenti.Items[0].Selected = dt.Rows[0]["MovimentiPrelievi"].ToString() == "Y" ? true : false;
            chkAnalisi.Items[0].Selected = dt.Rows[0]["AnalisiStatistichePrelievi"].ToString() == "Y" ? true : false;
            chkAnagrafica.Items[0].Selected = dt.Rows[0]["AnagraficaCliniche"].ToString() == "Y" ? true : false;
            chkAnalisiC.Items[0].Selected = dt.Rows[0]["AnalisiConsegneCliniche"].ToString() == "Y" ? true : false;
            chkLoadBloodRrefrigerator.Items[0].Selected = dt.Rows[0]["LoadBloodRrefrigerator"].ToString().Trim() == "Y" ? true : false;
            btnRegister.Text = "Aggiornamento Registra";
            lblmsg.InnerHtml = "";
            txtUserId.ReadOnly = true;
            txtE_mail.ReadOnly = true;
            BindChivo(ddltipo.SelectedValue);
            if (ddlChiave.Items.FindByValue(dt.Rows[0]["CodeID_Point_Clinic"].ToString()) != null)
            {
                ddlChiave.SelectedValue = dt.Rows[0]["CodeID_Point_Clinic"].ToString();
            }
        }
        else
        {
            ClearInput();
            lblmsg.InnerHtml = Common.ShowMessage("No Record Found", 2);
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
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
            txtDataScadenza.Text = DateTime.Now.AddMonths(Convert.ToInt32(1)).ToShortDateString();
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
            if (btnRegister.Text == "Aggiornamento Registra")
            {
                Type = "U";
                encry_password = "NA";
            }
            else
            {
                Type = "S";
            }

            SqlParameter[] oPera =
           {
            new SqlParameter("@UserID",txtUserId.Text),
            new SqlParameter("@NameUser",txtDenominazione.Text),
            new SqlParameter("@Psw",encry_password),
            new SqlParameter("@TypeExpiry",ddlScadenza.SelectedValue),
            new SqlParameter("@PSWDeadline",PSWDeadline),
            new SqlParameter("@Phone",txtTelefono.Text),
            new SqlParameter("@Email",txtE_mail.Text),
            new SqlParameter("@TypeUser",ddltipo.SelectedValue),
            new SqlParameter("@Note",txtNote.Text),
            new SqlParameter("@GestioneUtentieAccessi",chkGestione.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@BancaSangue",chkGestioneAnagrafiche.SelectedValue=="1"?"Y":"N"  ),
            new SqlParameter("@AmbulatoriPuntiPrelievo",ChkBchkAmbulatori.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@AnagraficaDonatori",ChkAnagraficaDonatori.SelectedValue=="1"?"Y":"N" ),
            
            new SqlParameter("@MovimentiPrelievi",chkMivimenti.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@AnalisiStatistichePrelievi",chkAnalisi.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@AnagraficaCliniche",chkAnagrafica.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@MovimentoRichiesteCliniche",chkMovimentoR.SelectedValue=="1"?"Y":"N" ),
            new SqlParameter("@AnalisiConsegneCliniche",chkAnalisiC.SelectedValue=="1"?"N":"N" ),
            new SqlParameter("@LoadBloodRrefrigerator",chkLoadBloodRrefrigerator.SelectedValue=="1"?"Y":"N" ),
             new SqlParameter("@Type",Type ),
              new SqlParameter("@Chiave",ddlChiave.SelectedValue )
           
        };
            int result = cls.ExecuteNonQuery("RegisterEmoUser", oPera, CommandType.StoredProcedure);
            if (result > 0)
            {
                string SenderEmailId = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string SenderEmailIdPwd = System.Configuration.ConfigurationManager.AppSettings["SenderEmailPwd"].ToString();
                string strUrl = Common.GetProjectLocation(false) + "Login.aspx";
                string linkText = "Click here";
                var strBldr = new StringBuilder();

                strBldr.Append("UserName:" + " " + txtUserId.Text);
                strBldr.Append("<br>");
                strBldr.Append("UserPassword:" + " " + txtPSW.Text);
                strBldr.Append("<br>");
                strBldr.AppendFormat("<a href='{0}'>{1}</a>", strUrl, linkText);

                bool email = EmailSender.sendmail(SenderEmailId, SenderEmailIdPwd, SenderEmailId, txtE_mail.Text, "Emobank Login Credentials", strBldr.ToString());

                if (Convert.ToString(Session["UserId"]) == txtCodice.Text)
                {
                    Session["UserType"] = ddltipo.SelectedValue;
                    Session["Sample_Clinic_Code"] = ddlChiave.SelectedValue;
                }
                ClearInput();
                lblmsg.InnerHtml = Common.ShowMessage("Record salvato con successo.", 1);
                divList.Visible = true;
                divMainPage.Visible = false;
                BindUsers();
            }
            else if (result == -1)
            {
                lblmsg.InnerHtml = Common.ShowMessage("User ID e -mail già exists.Use un altro.", 2);
            }
            else if (result == 0)
            {
                lblmsg.InnerHtml = Common.ShowMessage("Errore nei dati . Contatti con Amministratr", 3);
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
    }

    protected void ddlScadenza_SelectedIndexChanged(object sender, EventArgs e)
    {
        Expirydate();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    protected void btnStampa_Click(object sender, EventArgs e)
    {
        int[] a = new int[1];
        a[0] = 0;

        ImportExport.ExportToPDF(grdData, "Utenti", "", a, "A4", true);
    }
    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        lblmsg.InnerHtml = "";
        txtE_mail.ReadOnly = false;
        txtUserId.ReadOnly = false;
        divList.Visible = false;
        divMainPage.Visible = true;
        btnRegister.Text = "Registra";
        img1.Visible = false;
        ClearInput();
        Expirydate();
    }
    protected void btnAnnulla_Click(object sender, EventArgs e)
    {
        divList.Visible = true;
        divMainPage.Visible = false;
    }
    private DataTable BindUsers()
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("Type",ddl1.SelectedValue)
        };
        DataTable dt = cls.ExecuteDataTable("GetUsers", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            grdData.DataSource = dt;
            grdData.DataBind();
            CheckGridVisible();
        }
        else
        {
            grdData.DataSource = null;
            grdData.DataBind();
        }


        return dt;
    }
    protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUsers();
    }
    protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        DataView sortedView = new DataView(BindUsers());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdData.DataSource = sortedView;
        grdData.DataBind();
    }
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }

    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        BindUsers();
    }
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gv = grdData.SelectedRow;
        string CodInt = gv.Cells[1].Text;
        //     Session["CodInt"] = CodInt;
        divList.Visible = false;
        divMainPage.Visible = true;

        BindUserDetailById(CodInt);

    }
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 != 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            }
            else
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            ImageButton img = (ImageButton)e.Row.Cells[0].FindControl("btnDelete");
           
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {

                e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdData, "Select$" + e.Row.RowIndex);
                e.Row.Cells[i].ToolTip = "Click to view detail.";
                e.Row.Cells[i].Attributes.Add("style", "cursor:pointer");
            }
        }
    }
    #region VisibleDeleteButton
    public void CheckGridVisible()
    {
        DataTable dt = cls.ExecuteDataTable("Get_UserForMovements", null, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int j = 0; j < grdData.Rows.Count; j++)
                {

                    ImageButton img = (ImageButton)grdData.Rows[j].Cells[0].FindControl("btnDelete");
                    if (grdData.Rows[j].Cells[1].Text.ToString().Trim() == dt.Rows[i]["UserInsert"].ToString().Trim())
                    {
                        img.Visible = false;
                    }
                    else if (grdData.Rows[j].Cells[1].Text == Session["UserId"].ToString())
                    {
                        img.Visible = false;
                    }
           
                   
                }


            }

        }
    }
#endregion
    protected void grdData_RowDataCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "DEL")
        {
            ltrID.Text = grdData.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",ltrID.Text)
        };
            int a = cls.ExecuteNonQuery("[sp_DeleteRegisterEmoUser]", oPera, CommandType.StoredProcedure);
            BindUsers();
        }
        if (e.CommandName == "Edit")
        {
            ltrID.Text = grdData.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",ltrID.Text)
        };

        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divList.Visible = true;
        divMainPage.Visible = false;
    }
    protected void img1_Click(object sender, ImageClickEventArgs e)
    {
        clsencrypt.classxx.Class1 encry = new clsencrypt.classxx.Class1();
        string encry_password = encry.encrypt(ConfigurationManager.AppSettings["defaultPsw"].ToString());
        SqlParameter[] Opara =
            {
              new SqlParameter("@Email", txtE_mail.Text),
              new SqlParameter("@Pswd", encry_password)
            };
        int a = cls.ExecuteNonQuery("ResetPsw", Opara, CommandType.StoredProcedure);
        if (a > 0)
        {
            ClearInput();
            divList.Visible = true;
            divMainPage.Visible = false;
            lblmsg.InnerHtml = Common.ShowMessage("password riposo", 1);
        }
    }
    protected void ddltipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindChivo(ddltipo.SelectedValue);
    }
    private void BindChivo(string UserType)
    {
        string ProcedureName = "";
        if (UserType == "P")
        {
            ProcedureName = "GetSamplingPoints";
        }
        else if (UserType == "C")
        {
            ProcedureName = "GetClinics";
        }
        if (!string.IsNullOrEmpty(ProcedureName))
        {
            DataTable dt = cls.ExecuteDataTable(ProcedureName, null, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ddlChiave.DataSource = dt;
                ddlChiave.DataTextField = "Description";
                ddlChiave.DataValueField = "CodID";
                ddlChiave.DataBind();
                div1.Visible = true;
            }
            else
            {
                ddlChiave.Items.Insert(0, new ListItem("--Nessuna registrazione--", "0"));
                ddlChiave.SelectedIndex = 0;
            }
        }
        else
        {
            DataTable dt = new DataTable();
            dt = null;
            div1.Visible = false;
            ddlChiave.DataSource = dt;
            ddlChiave.DataBind();
            ddlChiave.Items.Insert(0, new ListItem("--Nessuna registrazione--", "0"));
            ddlChiave.SelectedIndex = 0;
        }
    }
}