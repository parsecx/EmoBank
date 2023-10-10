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
using System.IO;

public partial class frmAnagraficaDonatori : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
     public static string PhysicalPath = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {

                Response.Redirect(ResolveUrl("~/Login.aspx"));
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
            Response.Redirect(ResolveUrl("~/Login.aspx"));
        }
    }

    #region List Records
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    protected void btnStampa_Click(object sender, EventArgs e)
    {
        int[] a = new int[1];
        a[0] = 0;
        //a[1] = 1;
        ImportExport.ExportToPDF(grdData, "Donor Registry", "", a, "A4", true);
    }
    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        HideShowDiv(1);
    }
    private DataTable BindUsers()
    {
        DataTable dt = cls.ExecuteDataTable("GetDonor_Registry", null, CommandType.StoredProcedure);
        grdData.DataSource = dt;
        grdData.DataBind();
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
        //Session["BankCodInt"] = CodInt;
        //Response.Redirect("frmUpdateCentralBank.aspx");
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
            int a = cls.ExecuteNonQuery("DeleteDonor_Registry", oPera, CommandType.StoredProcedure);
            BindUsers();
        }

    }

    #endregion

    #region Add New Record


    protected void btnRegister_Click(object sender, EventArgs e)
    {
        SaveCentralBank();
    }
    private void SaveCentralBank()
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@Description",txtDenominazione.Text.Trim()),
            new SqlParameter("@Address",txtIndirizzo.Text.Trim()),
            new SqlParameter("@resort",txtLocalita.Text.Trim()),
            new SqlParameter("@province",txtProvincia.Text.Trim()),
            new SqlParameter("@PostalCode",txtCap.Text.Trim()),
            new SqlParameter("@Phone",txtTelefono.Text.Trim()),
            new SqlParameter("@Email",txtE_mail.Text.Trim()),
            new SqlParameter("@Note",txtNote.Text.Trim())
            
        };
        int result = cls.ExecuteNonQuery("Donor_RegistrySave", oPera, CommandType.StoredProcedure);
        msg.Visible = true;
        if (result > 0)
        {
            ClearInput();
            lblmsg.InnerHtml = Common.ShowMessage("Record salvato con successo", 1);
            HideShowDiv(0);
        }
        else if (result == -1)
        {
            lblmsg.InnerHtml = Common.ShowMessage("Email già esistente. Prova con un altro e-mail", 2);
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
        //txtReferente.Text = string.Empty;
        txtLocalita.Text = string.Empty;
        BindUsers();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
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
            ClearInput();
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
            ClearInputAnimal();
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
        dt = cls.ExecuteDataTable("GetDonor_RegistryById", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            txtCodiceU.Text = dt.Rows[0]["DCodID"].ToString();
            txtCapU.Text = dt.Rows[0]["PostalCode"].ToString();
            txtDenominazioneU.Text = dt.Rows[0]["Name"].ToString();
            txtE_mailU.Text = dt.Rows[0]["Email"].ToString();
            txtNoteU.Text = dt.Rows[0]["Note"].ToString();
            txtIndirizzoU.Text = dt.Rows[0]["Address"].ToString();
            txtTelefonoU.Text = dt.Rows[0]["Phone"].ToString();
            txtLocalitaU.Text = dt.Rows[0]["resort"].ToString();
            txtProvinciaU.Text = dt.Rows[0]["Provincie"].ToString();
            //txtReferenteU.Text = dt.Rows[0]["Contactperson"].ToString();
        }
    }

    private void UpdateCentralBank()
    {
        msg.Visible = true;
        SqlParameter[] oPera =
        {
            new SqlParameter("@Description",txtDenominazioneU.Text.Trim()),
            //new SqlParameter("@Contactperson",txtReferenteU.Text.Trim()),
            new SqlParameter("@Address",txtIndirizzoU.Text.Trim()),
            new SqlParameter("@resort",txtLocalitaU.Text.Trim()),
            new SqlParameter("@province",txtProvinciaU.Text.Trim()),
            new SqlParameter("@PostalCode",txtCapU.Text.Trim()),
            new SqlParameter("@Phone",txtTelefonoU.Text.Trim()),
            new SqlParameter("@Email",txtE_mailU.Text.Trim()),
            new SqlParameter("@Note",txtNoteU.Text.Trim()),
            new SqlParameter("@CodId",txtCodiceU.Text.Trim())
            
        };
        int result = cls.ExecuteNonQuery("Donor_RegistryUpdate", oPera, CommandType.StoredProcedure);
        msgU.Visible = true;
        if (result > 0)
        {
            ClearInputU();
            lblmsgU.InnerHtml = Common.ShowMessage("Record aggiornato con successo.", 1);

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
        //txtReferenteU.Text = string.Empty;
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

        //Response.Redirect("frmCentralBank.aspx");
        HideShowDiv(0);
    }
    #endregion


    #region Animals
    public void BindAnimals(string DCodID)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@DCodId",DCodID)
        };
        DataTable dt = cls.ExecuteDataTable("GetAnimals", oPera, CommandType.StoredProcedure);
        grdAnimals.DataSource = dt;
        grdAnimals.DataBind();
    }
    private void BindSpeciesOfAnimals()
    {
        DataTable dt = cls.ExecuteDataTable("Select * from Emobank_AnimalSpecies", null, CommandType.Text);
        ddlAnimaleSpecie.DataSource = dt;
        ddlAnimaleSpecie.DataTextField = "SpeciesName";
        ddlAnimaleSpecie.DataValueField = "SpeciesCode";
        ddlAnimaleSpecie.DataBind();
        ddlAnimaleSpecie.Items.Insert(0, new ListItem("--Select Specie Animale--", "0"));
    }



    protected void btnAnimalNuovo_Click(object sender, EventArgs e)
    {
        BindSpeciesOfAnimals();
        pnlAddAnimal.Visible = true;
        pnlAnimalList.Visible = false;
    }
    protected void btnAddNewAnimal_Click(object sender, EventArgs e)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@DCodID",txtCodiceU.Text),
            new SqlParameter("@AnimalName",txtAnimalName.Text.Trim()),
            new SqlParameter("@Animalspecies",ddlAnimaleSpecie.SelectedValue.Trim()),
            new SqlParameter("@AnimalWeight",txtPeso.Text.Trim()),
            new SqlParameter("@AgeAnimal",txtAge.Text.Trim()),
        };
        int result = cls.ExecuteNonQuery("SaveDonorAnimals", oPera, CommandType.StoredProcedure);
        msgU.Visible = true;
        if (result > 0)
        {
            ClearInputAnimal();
            lblmsgU.InnerHtml = Common.ShowMessage("Record salvato con successo", 1);
        }
        else if (result == -1)
        {
            lblmsgU.InnerHtml = Common.ShowMessage("Email già esistente. Prova con un altro e-mail", 2);
        }
        else if (result == 0)
        {
            lblmsgU.InnerHtml = Common.ShowMessage("Errore nei dati. Il contatto con l'amministratore", 3);
        }
    }
    protected void btnBackAnimalList_Click(object sender, EventArgs e)
    {
        pnlAddAnimal.Visible = false;
        pnlAnimalList.Visible = true;
    }
    public void ClearInputAnimal()
    {
        BindAnimals(txtCodiceU.Text);
        ddlAnimaleSpecie.ClearSelection();
        txtAnimalName.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtPeso.Text = string.Empty;
        pnlAddAnimal.Visible = false;
        pnlAnimalList.Visible = true;

    }
    #endregion
    protected void grdAnimals_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ltrID = "0";
        if (e.CommandName == "DEL")
        {
            ltrID = grdAnimals.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@DCodID",txtCodiceU.Text),
             new SqlParameter("@Progressive",ltrID)
        };
            int a = cls.ExecuteNonQuery("DeleteAnimalsDonor", oPera, CommandType.StoredProcedure);
            if (a > 0)
            {

            }
            else
            {
                msgU.Visible = true;
                lblmsgU.InnerHtml = Common.ShowMessage("Can not delete because Registry Attachments are linked with this animal.", 2);
            }
            BindAnimals(txtCodiceU.Text);
        }
    }

    protected void grdAnimals_RowDataBound(object sender, GridViewRowEventArgs e)
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
            for (int i = 2; i < e.Row.Cells.Count; i++)
            {
                //e.Row.Cells[i].Attributes.Add("onclick", "Chk('" + e.Row.Cells[i].Text + "');");
                e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdAnimals, "Select$" + e.Row.RowIndex);
                e.Row.Cells[i].ToolTip = "Click to view detail.";
                e.Row.Cells[i].Attributes.Add("style", "cursor:pointer");
                //e.Row.Cells[i].Attributes.Add("data-toggle", "modal");
                //e.Row.Cells[i].Attributes.Add("data-target", "#myModal");
            }
            //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdData, "Select$" + e.Row.RowIndex);

            //e.Row.Cells[0].Attributes["onclick"] = "";
            GridView grd = (GridView)e.Row.FindControl("gvOrders");

            int id = Convert.ToInt32(grdAnimals.DataKeys[e.Row.RowIndex].Values[0]);
            string group = grdAnimals.DataKeys[e.Row.RowIndex].Values[0].ToString();
            BindAnimalAttachment(txtCodiceU.Text.Trim(), group, grd);


        }
    }
    protected void lnkFilter_Click(object sender, EventArgs e)
    {
        string DCodId = txtCodiceU.Text;

    }
    private void BindAnimalAttachment(string DCodId, string Progressive, GridView grd)
    {
        DataTable dt = cls.ExecuteDataTable("select * from Donor_Registry_Attachments where DCodId=" + DCodId + "and Progressive=" + Progressive, CommandType.Text);
        grd.DataSource = dt;
        grd.DataBind();
    }
    protected void btnAttachmentNuovo_Click(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string PhotoExt = "";
        if (upFile.HasFile == false)
        {
            lblmsgU.InnerHtml = Common.ShowMessage("Select Attachment", 3);
        }
        else if (upFile.HasFile)
        {

            string path = Server.MapPath("~/Upload/Donor_Registry_Attachments/");
            PhotoExt = Path.GetExtension(upFile.PostedFile.FileName.ToString());
            string filename = System.DateTime.Now.AddTicks(2).ToString() + PhotoExt;
            upFile.PostedFile.SaveAs(path + filename);
            SqlParameter[] oPera =
        {
            new SqlParameter("@DCodID",txtCodiceU.Text),
            new SqlParameter("@Progressive","01"),
            new SqlParameter("@LinkAttachment",filename),
            new SqlParameter("@DataInserimento",DateTime.Now.ToString("MM/dd/yyyy")),
            new SqlParameter("@DescrizioneAllegato",txtDescrizioneAllegato.Text),
        };
            int result = cls.ExecuteNonQuery("Save_Donor_Registry_Attachments", oPera, CommandType.StoredProcedure);
            msgU.Visible = true;
            if (result > 0)
            {
                ClearInputAnimal();
                lblmsgU.InnerHtml = Common.ShowMessage("Record salvato con successo", 1);
            }
            else if (result == -1)
            {
                lblmsgU.InnerHtml = Common.ShowMessage("Email già esistente. Prova con un altro e-mail", 2);
            }
            else if (result == 0)
            {
                lblmsgU.InnerHtml = Common.ShowMessage("Errore nei dati. Il contatto con l'amministratore", 3);
            }
        }
    }

    
}