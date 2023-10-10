﻿using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class frmSamplingPoint : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    BindUsers();
                }
            }
        }

        else
        {
            Response.Redirect(ResolveUrl("Login.aspx"));
        }
    }
    #region List Sampling Points
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    protected void btnStampa_Click(object sender, EventArgs e)
    {
        int[] a = new int[1];
        a[0] = 0;
        ImportExport.ExportToPDF(grdData, "Ambulatori Punti Prelievo", "", a, "A4", true);
    }
    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        //Response.Redirect("frmSamplingPoint.aspx");
        HideShowDiv(1);
    }
    private DataTable BindUsers()
    {
        DataTable dt = cls.ExecuteDataTable("GetSamplingPoints", null, CommandType.StoredProcedure);
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

    #region VisibleDeleteButton
    public void CheckGridVisible()
    {
        DataTable dt = cls.ExecuteDataTable("GetPuntoPerviloFor", null, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int j = 0; j < grdData.Rows.Count; j++)
                {

                    ImageButton img = (ImageButton)grdData.Rows[j].Cells[0].FindControl("btnDelete");
                    if (grdData.Rows[j].Cells[1].Text.ToString().Trim() == dt.Rows[i]["CodeIDSamplingPoint"].ToString().Trim())
                    {
                        img.Visible = false;
                    }
                    //else
                    //{
                    //    img.Visible = true;
                    //}
                }


            }

        }
    }
    #endregion
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
        if(sortedView.Count>0)
        CheckGridVisible();
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
        //Session["SampleCodInt"] = CodInt;
        //Response.Redirect("frmUpdateSamplingPoint.aspx");
        BindCentralBankById(CodInt);
        HideShowDiv(2);
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
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                //e.Row.Cells[i].Attributes.Add("onclick", "Chk('" + e.Row.Cells[i].Text + "');");
                e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdData, "Select$" + e.Row.RowIndex);
                e.Row.Cells[i].ToolTip = "Click to view detail.";
                e.Row.Cells[i].Attributes.Add("style", "cursor:pointer");
            }
            //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdData, "Select$" + e.Row.RowIndex);

            //e.Row.Cells[0].Attributes["onclick"] = "";
        }
    }
    protected void grdData_RowDataCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "DEL")
        {
            ltrID.Text = grdData.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",ltrID.Text)
        };
            int a = cls.ExecuteNonQuery("DeleteSamplingPoint", oPera, CommandType.StoredProcedure);
            BindUsers();
        }
    }
    #endregion


    #region Add New Sampling Point


    protected void btnRegister_Click(object sender, EventArgs e)
    {
        SaveCentralBank();
    }
    private void SaveCentralBank()
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@Description",txtDenominazione.Text.Trim()),
            new SqlParameter("@Contactperson",txtReferente.Text.Trim()),
            new SqlParameter("@Address",txtIndirizzo.Text.Trim()),
            new SqlParameter("@resort",txtLocalita.Text.Trim()),
            new SqlParameter("@province",txtProvincia.Text.Trim()),
            new SqlParameter("@PostalCode",txtCap.Text.Trim()),
            new SqlParameter("@Phone",txtTelefono.Text.Trim()),
            new SqlParameter("@Email",txtE_mail.Text.Trim()),
            new SqlParameter("@Note",txtNote.Text.Trim())
            
        };
        int result = cls.ExecuteNonQuery("SamplingPointSave", oPera, CommandType.StoredProcedure);
        msg.Visible = true;
        if (result > 0)
        {
            ClearInput();
            lblmsg.InnerHtml = Common.ShowMessage("Record salvato con successo", 1);
            HideShowDiv(0);
            BindUsers();
        }
        else if (result == -1)
        {
            lblmsg.InnerHtml = Common.ShowMessage("Email già esistente. Prova con un altro e-mail", 3);
        }
        else if (result == 0)
        {
            lblmsg.InnerHtml = Common.ShowMessage("Errore nei dati. Il contatto con l'amministratore", 3);
        }

    }
    private void ClearInput()
    {
        txtCodice.Text = string.Empty;
        txtCap.Text = string.Empty;
        txtDenominazione.Text = string.Empty;
        txtE_mail.Text = string.Empty;
        txtNote.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        lblmsg.InnerHtml = "";
        txtIndirizzo.Text = string.Empty;
        txtProvincia.Text = string.Empty;
        txtReferente.Text = string.Empty;
        txtLocalita.Text = string.Empty;
        BindUsers();
        //up1.Update();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearInput();
        HideShowDiv(0);
    }
    #endregion
    private void HideShowDiv(int i)
    {
        if (i == 1)
        {
            List.Visible = false;
            AddNew.Visible = true;
            lblmsg.InnerText = "";
            Update.Visible = false;
        }
        else if (i == 0)
        {
            List.Visible = true;
            AddNew.Visible = false;
            Update.Visible = false;
        }
        else if (i == 2)
        {
            List.Visible = false;
            AddNew.Visible = false;
            Update.Visible = true;
        }

    }





    #region Update Record
    private void BindCentralBankById(string CntrBnkId)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CntrBnkId)
        };
        DataTable dt = new DataTable();
        dt = cls.ExecuteDataTable("GetSamplingPointById", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            txtCodiceU.Text = dt.Rows[0]["CodID"].ToString();
            txtCapU.Text = dt.Rows[0]["PostalCode"].ToString();
            txtDenominazioneU.Text = dt.Rows[0]["Description"].ToString();
            txtE_mailU.Text = dt.Rows[0]["Email"].ToString();
            txtNoteU.Text = dt.Rows[0]["Note"].ToString();
            txtIndirizzoU.Text = dt.Rows[0]["Address"].ToString();
            txtTelefonoU.Text = dt.Rows[0]["Phone"].ToString();
            txtLocalitaU.Text = dt.Rows[0]["resort"].ToString();
            txtProvinciaU.Text = dt.Rows[0]["province"].ToString();
            txtReferenteU.Text = dt.Rows[0]["Contactperson"].ToString();
        }
    }

    private void UpdateCentralBank()
    {
        msg.Visible = true;
        SqlParameter[] oPera =
        {
            new SqlParameter("@Description",txtDenominazioneU.Text.Trim()),
            new SqlParameter("@Contactperson",txtReferenteU.Text.Trim()),
            new SqlParameter("@Address",txtIndirizzoU.Text.Trim()),
            new SqlParameter("@resort",txtLocalitaU.Text.Trim()),
            new SqlParameter("@province",txtProvinciaU.Text.Trim()),
            new SqlParameter("@PostalCode",txtCapU.Text.Trim()),
            new SqlParameter("@Phone",txtTelefonoU.Text.Trim()),
            new SqlParameter("@Email",txtE_mailU.Text.Trim()),
            new SqlParameter("@Note",txtNoteU.Text.Trim()),
            new SqlParameter("@CodId",txtCodiceU.Text.Trim())
            
        };
        int result = cls.ExecuteNonQuery("SamplingPointUpdate", oPera, CommandType.StoredProcedure);
        msgU.Visible = true;
        if (result > 0)
        {
            ClearInputU();
            lblmsgU.InnerHtml = Common.ShowMessage("Record aggiornato con successo.", 1);
            Session.Remove("SampleCodInt");
            BindUsers();
            HideShowDiv(0);
        }
        else if (result == -1)
        {
            lblmsgU.InnerHtml = Common.ShowMessage("Email già esistente. Prova con un altro e-mail", 3);
        }
        else if (result == 0)
        {
            lblmsgU.InnerHtml = Common.ShowMessage("Errore nei dati. Il contatto con l'amministratore", 3);
        }

    }
    private void ClearInputU()
    {
        txtCodiceU.Text = string.Empty;
        txtCapU.Text = string.Empty;
        txtDenominazioneU.Text = string.Empty;
        txtE_mailU.Text = string.Empty;
        txtNoteU.Text = string.Empty;
        txtTelefonoU.Text = string.Empty;
        lblmsgU.InnerHtml = "";
        txtIndirizzoU.Text = string.Empty;
        txtProvinciaU.Text = string.Empty;
        txtReferenteU.Text = string.Empty;
        txtLocalitaU.Text = string.Empty;
        msgU.Visible = false;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateCentralBank();

    }
    protected void btnBackUp_Click(object sender, EventArgs e)
    {
        ClearInputU();
        Session.Remove("SampleCodInt");
        //Response.Redirect("frmCentralBank.aspx");
        HideShowDiv(0);
    }
    #endregion
}