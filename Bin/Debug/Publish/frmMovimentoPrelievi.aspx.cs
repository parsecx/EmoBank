using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class frmMovimentoPrelievi : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
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
                    BindMovimentoPrelievi();
                    txtDataInserimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDatePrelievo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        else
        {
            Response.Redirect(ResolveUrl("Login.aspx"));
        }
    }
    private void HideShowDiv(int i)
    {

        if (i == 1)
        {

            List.Visible = false;
            AddNew.Visible = true;
            lblmsg.InnerText = "";

            //Update.Visible = false;
        }
        else if (i == 0)
        {
            btnStampaEti.Visible = true;
            List.Visible = true;
            AddNew.Visible = false;
            //Update.Visible = false;
            ClearInput();
        }
        else if (i == 2)
        {
            List.Visible = false;
            AddNew.Visible = false;
            //Update.Visible = true;
        }

    }
    #region MovimentoPrelievi List
    private DataTable BindMovimentoPrelievi()
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@TipoUtente",Session["UserType"].ToString()),
              new SqlParameter("@CodeID_Point_Clinic",Session["Sample_Clinic_Code"] .ToString())
        };
        DataTable dt = cls.ExecuteDataTable("GetMovimentoPrelievi", oPera, CommandType.StoredProcedure);
        grdData.DataSource = dt;
        grdData.DataBind();
        BindGridDropdowns();
        return dt;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }

    protected void btnStampa_Click(object sender, EventArgs e)
    {
        int[] a = new int[1];
        a[0] = 0;
        //a[1] = 1;
        ImportExport.ExportToPDF(grdData, "Movimento Prelievi", "", a, "A4", true);
    }
    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        hdMode.Value = "S";
        btnRegister.Text = "Registra";
        btnStampaEti.Visible = false;
        txtProgressivo.Text = "01";
        HideShowDiv(1);
        BindDropDowns();
        divProtocollo.Visible = false;
        divCInterno.Visible = false;
        txtDatePrelievo.Text = DateTime.Now.ToString("dd/MM/yyyy");
        grdAnimals.DataSource = null;
        grdAnimals.DataBind();
        btnStampaEti.HRef = "#";
        pnlAddAnimal.Visible = false;

    }
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gv = grdData.SelectedRow;
        string CodInt = gv.Cells[1].Text;
        BindMovimentoPrelieviByCodId(CodInt);
        btnStampaEti.HRef = "GenrateLabel.aspx?CodId=" + CodInt;
        HideShowDiv(1);
    }
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEL")
        {
            string CodId = grdData.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",CodId)
        };
            int a = cls.ExecuteNonQuery("DeleteDrawing_movements", oPera, CommandType.StoredProcedure);
            BindMovimentoPrelievi();
        }
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

        DataView sortedView = new DataView(BindMovimentoPrelievi());
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

    }

    private void BindMovimentoPrelieviByCodId(string CodId)
    {
        BindDropDowns();
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CodId)
        };
        DataTable dt = new DataTable();
        dt = cls.ExecuteDataTable("GetMovimentoPrelieviByCodId", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            ltrID.Text = CodId;
            btnRegister.Text = "Update Registra";

            txtCodice.Text = dt.Rows[0]["CodID"].ToString();
            txtCInterno.Text = dt.Rows[0]["CodID"].ToString();

            txtProtocollo.Text = dt.Rows[0]["ProtocolNumber"].ToString();
            txtProgressivo.Text = dt.Rows[0]["Progressive"].ToString();
            txtDatePrelievo.Text = Convert.ToDateTime(dt.Rows[0]["DateTimeDrawing"]).ToString("dd/MM/yyyy");
            ddlPuntoPrelievo.SelectedValue = dt.Rows[0]["CodeIDSamplingPoint"].ToString();
            string OperatorWithdrawal = dt.Rows[0]["OperatorWithdrawal"].ToString();

            ddlTipoPreparato.ClearSelection(); //making sure the previous selection has been cleared
            try
            {
                ddlTipoPreparato.Items.FindByText(dt.Rows[0]["TypePrepared"].ToString()).Selected = true;
            }
            catch (Exception)
            {
                //ddlTipoPreparato.SelectedIndex = 0;
            }

            //ddlTipoPreparato.SelectedValue = dt.Rows[0]["TypePrepared"].ToString();
            //txtTipoPreparato.Text = dt.Rows[0]["TypePrepared"].ToString();

            txtComposizione.Text = dt.Rows[0]["CompositionVolumeAnticoagulant"].ToString();
            ddlComposizione.SelectedValue = dt.Rows[0]["CompositionVolumeAnticoagulant"].ToString();

            txtConservazione.Text = dt.Rows[0]["ModeStorageTemp"].ToString();
            ddlConservazione.SelectedValue = dt.Rows[0]["ModeStorageTemp"].ToString();

            txtDataScadenza.Text = Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"]).ToString("dd/MM/yyyy");

            //txtGruppoSangue.Text = dt.Rows[0]["AnimalGroupBlood"].ToString();


            txtPesoLordo.Text = dt.Rows[0]["GrossWeightPrepared"].ToString();
            txtNote.Text = dt.Rows[0]["Note"].ToString();
            decimal value = decimal.Parse(txtPesoLordo.Text, CultureInfo.GetCultureInfo("en-US"));

            //Convert to string using DE culture (or other culture using , as decimal separator)
            string output = value.ToString(CultureInfo.GetCultureInfo("de-DE"));

            output = output.Remove(output.Length - 1, 1);
            txtPesoLordo.Text = output;

            try
            {
                ddlDenominazione.SelectedValue = dt.Rows[0]["CodIDDonors"].ToString();
                ddlSpecieAnimale.SelectedValue = dt.Rows[0]["IndicationAnimalSpecies"].ToString();
            }
            catch (Exception)
            {
                //ddlDenominazione.SelectedIndex = 0;
                //ddlSpecieAnimale.SelectedIndex = 0;
            }

            GetBloodGroup(ddlSpecieAnimale.SelectedValue);
            GetSamplingPointById(ddlPuntoPrelievo.SelectedValue);
            GetDonatorsById(ddlDenominazione.SelectedValue);
            BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue);

            try
            {
                ddlGruppoSangue.SelectedValue = dt.Rows[0]["AnimalGroupBlood"].ToString();
            }
            catch (Exception)
            {

            }

            divProtocollo.Visible = true;
            divCInterno.Visible = true;
        }
        else
        {
            btnRegister.Text = "Registra";
        }
    }
    #endregion

    #region Add New Record
    private void BindDropDowns()
    {
        GetTipoPreparato();
        GetSamplingPoints();
        GetAnimalSpecies();
        GetDonators();
        BindComposizione();
        BindConservazione();
        HideShowCompo();
        HideShowConservazione();
        BloodGroupHideShow();
        GetBloodGroup(ddlSpecieAnimale.SelectedValue);
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        int CodIdDonor = 0;
        string Type;
        if (pnlAddAnimal.Visible == false)
        {
            txtPeso.Text = "0";
            hdProgressive.Value = "0";
            txtAge.Text = "0";
        }
        if (btnRegister.Text.Trim() == "Registra")
        {
            Type = "S";
            ltrID.Text = "0";
           
        }
        else
        {
            Type = "U";
        }

        if (Validation())
        {
            //if (hdNewDonor.Value == "1")
            //{
            //    SqlParameter[] Opare =
            //{
            // //new SqlParameter("@ProtocolNumber",txtProtocollo.Text),
            // //new SqlParameter("@Progressive",txtProgressivo.Text),

            // new SqlParameter("@Rval",SqlDbType.Int)
            //  };
            //    Opare[8].Direction = ParameterDirection.ReturnValue;
            //    int rval = cls.ExecuteNonQuery("Save_Emobank_Donors", Opare, CommandType.StoredProcedure);
            //    if (rval > 0)
            //    {
            //        rval = Convert.ToInt32(Opare[8].Value);
            //        if (rval > 0)
            //        {
            //            CodIdDonor = rval;
            //        }
            //    }
            //}
            if (hdNewDonor.Value == "1")
            {

            }
            else
            {
                CodIdDonor = Convert.ToInt32(ddlDenominazione.SelectedValue);
            }
            try
            {
                //txtDataScadenza.Text = Convert.ToDateTime(txtDatePrelievo.Text).AddDays(34).ToString("MM/dd/yyyy");
            }
            catch (Exception)
            {
                txtDatePrelievo.Focus();
                msg.Visible = true;
                lblmsg.InnerHtml = Common.ShowMessage("Inserisci valida Data per dati Prelievo.", 2);
                return;

            }

            string value;
            try
            {
                value = Convert.ToDecimal(txtPesoLordo.Text.Replace(",", ".")).ToString();
            }
            catch (Exception)
            {
                txtPesoLordo.Focus();
                msg.Visible = true;
                lblmsg.InnerHtml = Common.ShowMessage("Inserisci un valore valido per il Peso Lordo.", 2);
                return;
            }

            SqlParameter[] Opara =
            {
             new SqlParameter("@Rval",ParameterDirection.ReturnValue),
             new SqlParameter("@Name",""),
             new SqlParameter("@Address",txtDIndirzzo.Text),
             new SqlParameter("@Resort",txtDLocalita.Text),
             new SqlParameter("@Province",txtDProvincia.Text),
             new SqlParameter("@PostalCode",txtDCap.Text),
             new SqlParameter("@Phone",txtDTelefono.Text),
             new SqlParameter("@email",txtDE_mail.Text),
             new SqlParameter("@Note",txtNoteU.Text),
              new SqlParameter("@IsNewDonor",hdNewDonor.Value),

                new SqlParameter("@CodID",ltrID.Text),
            // new SqlParameter("@ProtocolNumber",txtProtocollo.Text),
             new SqlParameter("@Progressive",txtProgressivo.Text),
             new SqlParameter("@DateTimeDrawing", Convert.ToDateTime(txtDatePrelievo.Text).ToString("MM/dd/yyyy")),
             new SqlParameter("@CodeIDSamplingPoint",ddlPuntoPrelievo.SelectedValue),
             new SqlParameter("@OperatorWithdrawal",""),
            new SqlParameter("@TypePrepared", ddlTipoPreparato.SelectedItem.Text.Trim()),
            // new SqlParameter("@TypePrepared",txtTipoPreparato.Text),
             new SqlParameter("@GrossWeightPrepared",value),
             new SqlParameter("@Preparation","1"),
             new SqlParameter("@ProductExpirationDate",Convert.ToDateTime(txtDataScadenza.Text).ToString("MM/dd/yyyy")),
             new SqlParameter("@CompositionVolumeAnticoagulant",txtComposizione.Visible==false?ddlComposizione.SelectedValue: txtComposizione.Text),
             //new SqlParameter("@AnimalGroupBlood",ddlGruppoSangue.SelectedValue),
             new SqlParameter("@AnimalGroupBlood",txtGruppoSangue.Visible==false?ddlGruppoSangue.SelectedValue: txtGruppoSangue.Text.Trim()),
             new SqlParameter("@ModeStorageTemp",txtConservazione.Visible==false?ddlConservazione.SelectedValue: txtConservazione.Text),
             new SqlParameter("@IndicationAnimalSpecies",ddlSpecieAnimale.SelectedValue),
             new SqlParameter("@CodIDDonors",ddlDenominazione.SelectedValue),
             new SqlParameter("@UserInsert",Session["UserId"]),
             new SqlParameter("@DateTimeUserInsert",DateTime.Now),
             //new SqlParameter("@Barcode","122222"),
             new SqlParameter("@SpecieAnimale",ddlAnimaleSpecie.SelectedValue),
             new SqlParameter("@Nome",txtAnimalName.Text),
             new SqlParameter("@Peso",txtPeso.Text.Trim().Replace(',','.')),
             new SqlParameter("@Eta",txtAge.Text.Trim().Replace(',','.')),
             new SqlParameter("@hdProgressive",hdProgressive.Value),
             //new SqlParameter("@ProgressiveAnimalDonator",ProgressiveValue),
             //new SqlParameter("@DateRequestClinic",DateTime.Now.ToString("MM/dd/yyyy")),
             //new SqlParameter("@UserAcquisitionRequest","1"),
             //new SqlParameter("@DateAcquisitionRequest",DateTime.Now.ToString("MM/dd/yyyy")),
             //new SqlParameter("@UserDrainBloodBankRefrigerator","1"),
             //new SqlParameter("@DatedrainBloodBankRefrigerator",DateTime.Now.ToString("MM/dd/yyyy")),
             //new SqlParameter("@DateTrasmissionClinicaL",DateTime.Now.ToString("MM/dd/yyyy")),
             //new SqlParameter("@NumberProtocolTrasmissionClinical","1"),
             //new SqlParameter("@DateReceiptClinic",DateTime.Now.ToString("MM/dd/yyyy")),
             
              
             new SqlParameter("@Type",Type),
              
             new SqlParameter("@Note1",txtNote.Text)

            };
            Opara[0].Direction = ParameterDirection.ReturnValue;
            int Rval = cls.ExecuteNonQuery("MovimentoPrelieviSave", Opara, CommandType.StoredProcedure);
           
            if (Rval > 0)
            {
                Rval = Convert.ToInt32(Opara[0].Value);
                if (Rval > 0)
                {
                    msg.Visible = true;
                    BindMovimentoPrelievi();
                    HideShowDiv(0);
                    lblmsg.InnerHtml = Common.ShowMessage("Record aggiornato con successo.", 1);
                }
            }
            else if (Rval == -12)
            {
                msg.Visible = true;
                lblmsg.InnerHtml = Common.ShowMessage("Animale non è per ritirare.", 2);
            }
            else
            {
                msg.Visible = true;
                lblmsg.InnerHtml = Common.ShowMessage("Errore nei dati. Il contatto con l'amministratore", 2);
            }



        }



    }
    public bool Validation()
    {

        return true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        HideShowDiv(0);
    }
    #endregion
    #region Update Record


    protected void btnBackUp_Click(object sender, EventArgs e)
    {
        HideShowDiv(0);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
    #endregion


    #region Bind Dropdowns
    private DataTable GetTipoPreparato()
    {
        DataTable dt = cls.ExecuteDataTable("Select * from Emobank_TipoPreparato", null, CommandType.Text);
        ddlTipoPreparato.DataSource = dt;
        ddlTipoPreparato.DataTextField = "TipoPreparato";
        ddlTipoPreparato.DataValueField = "NoOfDays";
        ddlTipoPreparato.DataBind();
        ddlTipoPreparato.Items.Insert(0, new ListItem("--Select Tipo Preparato--", "0"));
        return dt;
    }

    private DataTable GetSamplingPoints()
    {
        DataTable dt = cls.ExecuteDataTable("GetSamplingPoints", null, CommandType.StoredProcedure);
        ddlPuntoPrelievo.DataSource = dt;
        ddlPuntoPrelievo.DataTextField = "Description";
        ddlPuntoPrelievo.DataValueField = "CodID";
        ddlPuntoPrelievo.DataBind();
        ddlPuntoPrelievo.Items.Insert(0, new ListItem("--Select Punto Prelievo--", "0"));

        string CodeID_Point_Clinic = Convert.ToString(Session["Sample_Clinic_Code"]);
        string UserType = Convert.ToString(Session["UserType"]);
        if (!string.IsNullOrEmpty(CodeID_Point_Clinic) && !string.IsNullOrEmpty(UserType))
        {
            if (UserType == "P")
            {
                if (ddlPuntoPrelievo.Items.FindByValue(CodeID_Point_Clinic) != null)
                {
                    ddlPuntoPrelievo.SelectedValue = CodeID_Point_Clinic;
                    ddlPuntoPrelievo.Attributes.Add("disabled", "disabled");
                    GetSamplingPointById(ddlPuntoPrelievo.SelectedValue);
                }
                else
                {

                    ddlPuntoPrelievo.Attributes.Remove("disabled");
                }
            }
        }
        return dt;
    }
    private DataTable GetAnimalSpecies()
    {

        // DataTable dt = cls.ExecuteDataTable("Select * from Emobank_AnimalSpecies EA join Donor_Registry_Detail DR on DR.Animalspecies=EA.SpeciesCode where DCodID=" + ddlDenominazione.SelectedValue + "  ", null, CommandType.Text);

        DataTable dt = cls.ExecuteDataTable("Select * from Emobank_AnimalSpecies", null, CommandType.Text);
        ddlSpecieAnimale.DataSource = dt;
        ddlSpecieAnimale.DataTextField = "SpeciesName";
        ddlSpecieAnimale.DataValueField = "SpeciesCode";
        ddlSpecieAnimale.DataBind();
        ddlSpecieAnimale.Items.Insert(0, new ListItem("--Select Specie Animale--", "0"));
        return dt;
    }
    private DataTable GetDonators()
    {
        DataTable dt = cls.ExecuteDataTable("GetDonators", null, CommandType.StoredProcedure);
        ddlDenominazione.DataSource = dt;
        ddlDenominazione.DataTextField = "Name";
        ddlDenominazione.DataValueField = "DCodID";
        ddlDenominazione.DataBind();
        if (dt.Rows.Count > 0)
        {
            ddlDenominazione.Items.Insert(0, new ListItem("--Select Denominazione--", "0"));
        }
        else
        {
            ddlDenominazione.Items.Insert(0, new ListItem("--Dati non disponibili--", "0"));
        }
        return dt;
    }
    private DataTable GetBloodGroup(string AnimalCode)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@AnimalCode",AnimalCode)
        };
        DataTable dt = cls.ExecuteDataTable("GetBloodGroupByAnimal", oPera, CommandType.StoredProcedure);
        ddlGruppoSangue.DataSource = dt;
        ddlGruppoSangue.DataTextField = "BloodGroup";
        ddlGruppoSangue.DataValueField = "BloodGroup";
        ddlGruppoSangue.DataBind();
        ddlGruppoSangue.Items.Insert(0, new ListItem("", "0"));
        return dt;
    }
    #endregion
    protected void ddlPuntoPrelievo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSamplingPointById(ddlPuntoPrelievo.SelectedValue);
    }
    private void GetSamplingPointById(string CodId)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CodId)
        };
        DataTable dt = cls.ExecuteDataTable("GetSamplingPointById", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            txtIndirzzo.Text = dt.Rows[0]["Address"].ToString();
            txtLocalita.Text = dt.Rows[0]["resort"].ToString();
            txtCap.Text = dt.Rows[0]["PostalCode"].ToString();
            txtE_mail.Text = dt.Rows[0]["Email"].ToString();
            txtTelefono.Text = dt.Rows[0]["Phone"].ToString();
            txtProvincia.Text = dt.Rows[0]["province"].ToString();
            txtReferente.Text = dt.Rows[0]["Contactperson"].ToString();

        }
    }
    protected void ddlDenominazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDenominazione.SelectedIndex == 0)
        {
            ClearNuovo();
            grdAnimals.DataSource = null;
            grdAnimals.DataBind();
        }
        else
        {
            GetDonatorsById(ddlDenominazione.SelectedValue);
            BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue);
        }
    }
    private void GetDonatorsById(string CodId)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CodId)
        };
        DataTable dt = cls.ExecuteDataTable("GetDonatorsById", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
           // txtDenominazione.ReadOnly = false;
            txtDIndirzzo.ReadOnly = false;
            txtDLocalita.ReadOnly = false;
            txtDCap.ReadOnly = false;
            txtDE_mail.ReadOnly = false;
            txtDTelefono.ReadOnly = false;
            txtDProvincia.ReadOnly = false;
            //txtDenominazione.ReadOnly = false;
            txtNoteU.ReadOnly = false;

           // txtDenominazione.Text = dt.Rows[0]["Name"].ToString();
            txtDIndirzzo.Text = dt.Rows[0]["Address"].ToString();
            txtDLocalita.Text = dt.Rows[0]["resort"].ToString();
            txtDCap.Text = dt.Rows[0]["PostalCode"].ToString();
            txtDE_mail.Text = dt.Rows[0]["Email"].ToString();
            txtDTelefono.Text = dt.Rows[0]["Phone"].ToString();
            txtDProvincia.Text = dt.Rows[0]["Provincie"].ToString();
            txtNoteU.Text = dt.Rows[0]["Note"].ToString();
        }
        hdNewDonor.Value = "0";
       // txtDenominazione.ReadOnly = true;
        txtDIndirzzo.ReadOnly = true;
        txtDLocalita.ReadOnly = true;
        txtDCap.ReadOnly = true;
        txtDE_mail.ReadOnly = true;
        txtDTelefono.ReadOnly = true;
        txtDProvincia.ReadOnly = true;
       // txtDenominazione.ReadOnly = true;
        txtNoteU.ReadOnly = true;
    }
    protected void btnDonatorNuovo_Click(object sender, EventArgs e)
    {
        ClearNuovo();
    }
    public void ClearNuovo()
    {


        ddlDenominazione.SelectedIndex = 0;
        hdNewDonor.Value = "1";
        txtDIndirzzo.Text = "";
        txtDLocalita.Text = "";
        txtDCap.Text = "";
        txtDE_mail.Text = "";
        txtDTelefono.Text = "";
        txtDProvincia.Text = "";
       // txtDenominazione.Text = "";
        txtNoteU.Text = "";
        txtNote.Text = "";
        txtDIndirzzo.ReadOnly = false;
        txtDLocalita.ReadOnly = false;
        txtDCap.ReadOnly = false;
        txtDE_mail.ReadOnly = false;
        txtDTelefono.ReadOnly = false;
        txtDProvincia.ReadOnly = false;
       // txtDenominazione.ReadOnly = false;
        txtNoteU.ReadOnly = false;

    }
    public void ClearInput()
    {
        txtCap.Text = string.Empty;
        txtCInterno.Text = string.Empty;
        txtCodice.Text = string.Empty;
        txtComposizione.Text = string.Empty;
        txtConservazione.Text = string.Empty;
        txtDataScadenza.Text = string.Empty;
        txtDatePrelievo.Text = string.Empty;
        txtE_mail.Text = string.Empty;
        //txtGruppoSangue.Text = string.Empty;
        txtIndirzzo.Text = string.Empty;
        txtLocalita.Text = string.Empty;
        txtPesoLordo.Text = string.Empty;
        txtProgressivo.Text = string.Empty;
        txtProtocollo.Text = string.Empty;
        txtProvincia.Text = string.Empty;
        txtReferente.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        //ddlGruppoSangue.SelectedIndex = 0;
        ddlPuntoPrelievo.SelectedIndex = 0;
        ddlSpecieAnimale.SelectedIndex = 0;
        ddlTipoPreparato.SelectedIndex = 0;
        //txtTipoPreparato.Text = string.Empty;
        ddlDenominazione.SelectedIndex = 0;
        hdNewDonor.Value = "0";
        txtDIndirzzo.Text = "";
        txtDLocalita.Text = "";
        txtDCap.Text = "";
        txtDE_mail.Text = "";
        txtDTelefono.Text = "";
        txtDProvincia.Text = "";
        //txtDenominazione.Text = "";
        txtNoteU.Text = "";
        txtNote.Text = "";
        txtDIndirzzo.ReadOnly = false;
        txtDLocalita.ReadOnly = false;
        txtDCap.ReadOnly = false;
        txtDE_mail.ReadOnly = false;
        txtDTelefono.ReadOnly = false;
        txtDProvincia.ReadOnly = false;
       // txtDenominazione.ReadOnly = false;
        txtNoteU.ReadOnly = false;



    }
    protected void txtDatePrelievo_TextChanged(object sender, EventArgs e)
    {
        string date = Convert.ToDateTime(txtDatePrelievo.Text).AddDays(34).ToString("yyyy/MM/dd");
        //txtDataScadenza.Text = date;
    }


    private DataTable BindGridDropdowns()
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@TipoUtente",Session["UserType"].ToString()),
              new SqlParameter("@CodeID_Point_Clinic",Session["Sample_Clinic_Code"] .ToString())
        };
        DataTable dt = cls.ExecuteDataTable("GetMovimentoPrelievi", oPera, CommandType.StoredProcedure);
        //DataTable dt = cls.ExecuteDataTable("GetMovimentoPrelievi", CommandType.StoredProcedure);

        BindDropdowns(ddlCod1, dt, "CodID", "CodID");


        BindDropdowns(ddlProtocol1, dt, "ProtocolNumber", "ProtocolNumber");


        BindDropdowns(ddlDateTimeDrawing1, dt, "DateTimeDrawing", "DateTimeDrawing");


        BindDropdowns(ddlDescription1, dt, "Description", "Description");



        BindDropdowns(ddlSpeciesName1, dt, "SpeciesName", "SpeciesName");


        BindDropdowns(ddlAnimalGroupBlood1, dt, "AnimalGroupBlood", "AnimalGroupBlood");


        BindDropdowns(ddlNamee1, dt, "Name", "Name");
        return dt;
    }
    private void BindDropdowns(DropDownList ddl, DataTable dt, string DataTextField, string DataValueField)
    {
        DataTable uniqueCols = dt.DefaultView.ToTable(true, DataTextField);
        ddl.DataSource = uniqueCols;
        ddl.DataTextField = DataTextField;
        ddl.DataValueField = DataValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("", "All"));
    }
    protected void ddlCod_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        string value = ddl.SelectedValue;
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",ddlCod1.SelectedValue),
            new SqlParameter("@ProtocolNumber",ddlProtocol1.SelectedValue),
            new SqlParameter("@Description",ddlDescription1.SelectedValue),
            new SqlParameter("@Name",ddlNamee1.SelectedValue),
            new SqlParameter("@SpeciesName",ddlSpeciesName1.SelectedValue),
            new SqlParameter("@DateTimeDrawing",ddlDateTimeDrawing1.SelectedValue),
             new SqlParameter("@AnimalGroupBlood",ddlAnimalGroupBlood1.SelectedValue),
            new SqlParameter("@TipoUtente",Session["UserType"].ToString()),
             new SqlParameter("@CodeID_Point_Clinic",Session["Sample_Clinic_Code"] .ToString())
            
        };
        DataTable dt = cls.ExecuteDataTable("GetMovimentoPrelieviFilter", oPera, CommandType.StoredProcedure);
        grdData.DataSource = dt;
        grdData.DataBind();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "myfunction", "$(document).ready(function(){ $('.chzn-select').chosen();});", true);

        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "myfunction", "<script type='text/javascript'>myFunction();</script>", true);
    }

    private void SortingFunction(string ColumnName)
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

        DataView sortedView = new DataView(BindMovimentoPrelievi());
        //sortedView.Sort = e.SortExpression + " " + sortingDirection;
        sortedView.Sort = ColumnName + " " + sortingDirection;
        grdData.DataSource = sortedView;
        grdData.DataBind();
    }
    protected void lnk1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        SortingFunction(lnk.CommandArgument.ToString());
    }
    protected void ddlSpecieAnimale_SelectedIndexChanged(object sender, EventArgs e)
    {
        BloodGroupHideShow();
        GetBloodGroup(ddlSpecieAnimale.SelectedValue);
        txtGruppoSangue.Text = ddlGruppoSangue.SelectedValue;

        ClearNuovo();
        //GetDonatorsById(ddlDenominazione.SelectedValue);
        grdAnimals.DataSource = null;
        grdAnimals.DataBind();
        //BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue);
    }


    [WebMethod]
    public static List<string> GetComposizione(string Composizione)
    {
        SqlHelper clss = new SqlHelper();
        List<string> empResult = new List<string>();
        SqlParameter[] oPera =
        {
            new SqlParameter("@Composizione",Composizione)
        };
        SqlDataReader dr = clss.ExecuteReader("GetComposizione", oPera, CommandType.StoredProcedure);

        while (dr.Read())
        {
            empResult.Add(dr["CompositionVolumeAnticoagulant"].ToString());
        }

        return empResult;

    }
    [WebMethod]
    public static List<string> GetConservazione(string Composizione)
    {
        SqlHelper clss = new SqlHelper();
        List<string> empResult = new List<string>();
        SqlParameter[] oPera =
        {
            new SqlParameter("@Conservazione",Composizione)
        };
        SqlDataReader dr = clss.ExecuteReader("GetConservazione", oPera, CommandType.StoredProcedure);

        while (dr.Read())
        {
            empResult.Add(dr["ModeStorageTemp"].ToString());
        }

        return empResult;

    }

    [WebMethod]
    public static List<string> GetTipo(string Tipo)
    {
        SqlHelper clss = new SqlHelper();
        List<string> empResult = new List<string>();
        SqlParameter[] oPera =
        {
            new SqlParameter("@Tipo",Tipo)
        };
        SqlDataReader dr = clss.ExecuteReader("GetTipo", oPera, CommandType.StoredProcedure);

        while (dr.Read())
        {
            empResult.Add(dr["TypePrepared"].ToString());
        }

        return empResult;

    }

    protected void txtTipoPreparato_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlTipoPreparato.SelectedValue) > 0)
        {
            string date = Convert.ToDateTime(txtDatePrelievo.Text).AddDays(Convert.ToInt32(ddlTipoPreparato.SelectedValue)).ToString("dd/MM/yyyy");
            txtDataScadenza.Text = date;
            txtDataScadenza.Attributes.Add("disabled", "disabled");
        }
        else
        {
            txtDataScadenza.Attributes.Remove("disabled");
        }

    }
    public void OnddlTipoPreparatoChange()
    {
        int Index = ddlTipoPreparato.SelectedIndex;
        if (Convert.ToInt32(ddlTipoPreparato.SelectedValue) > 0)
        {
            if (string.IsNullOrEmpty(txtDatePrelievo.Text))
            {
                msg.Visible = true;
                ddlTipoPreparato.SelectedIndex = 0;
                lblmsg.InnerHtml = Common.ShowMessage("Enter valid Data Prelievo", 2);
            }
            else
            {
                string date = Convert.ToDateTime(txtDatePrelievo.Text).AddDays(Convert.ToInt32(ddlTipoPreparato.SelectedValue)).ToString("dd/MM/yyyy");
                txtDataScadenza.Text = date;
                txtDataScadenza.Attributes.Add("disabled", "disabled");
            }
        }
        else
        {
            txtDataScadenza.Attributes.Remove("disabled");
            ddlTipoPreparato.SelectedIndex = Index;
        }
    }
    protected void ddlTipoPreparato_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnddlTipoPreparatoChange();
    }
    protected void imgComposozione_Click(object sender, ImageClickEventArgs e)
    {
        ddlComposizione.Visible = false;
        txtComposizione.Visible = true;
        imgCompoClose.Visible = true;
        imgComposozioneNew.Visible = false;

    }
    protected void imgCompoClose_Click(object sender, ImageClickEventArgs e)
    {
        HideShowCompo();
    }
    private void HideShowCompo()
    {
        ddlComposizione.SelectedIndex = 0;
        ddlComposizione.Visible = true;
        txtComposizione.Visible = false;
        imgComposozioneNew.Visible = true;
        imgCompoClose.Visible = false;
    }
    public void BindComposizione()
    {
        SqlHelper clss = new SqlHelper();
        List<string> empResult = new List<string>();
        SqlParameter[] oPera =
        {
            new SqlParameter("@Composizione","")
        };
        SqlDataReader dr = clss.ExecuteReader("GetComposizione", oPera, CommandType.StoredProcedure);

        while (dr.Read())
        {
            empResult.Add(dr["CompositionVolumeAnticoagulant"].ToString());
        }
        ddlComposizione.DataSource = empResult;
        //ddlComposizione.DataValueField = "CompositionVolumeAnticoagulant";
        //ddlComposizione.DataTextField = "CompositionVolumeAnticoagulant";
        ddlComposizione.DataBind();
        ddlComposizione.Items.Insert(0, "");
    }

    protected void ddlComposizione_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtComposizione.Text = ddlComposizione.SelectedValue;
    }


    private void HideShowConservazione()
    {
        ddlConservazione.SelectedIndex = 0;
        ddlConservazione.Visible = true;
        txtConservazione.Visible = false;
        imgConserNew.Visible = true;
        imgConserClose.Visible = false;
    }
    public void BindConservazione()
    {
        SqlHelper clss = new SqlHelper();
        List<string> empResult = new List<string>();
        SqlParameter[] oPera =
        {
            new SqlParameter("@Conservazione","")
        };
        SqlDataReader dr = clss.ExecuteReader("GetConservazione", oPera, CommandType.StoredProcedure);

        while (dr.Read())
        {
            empResult.Add(dr["ModeStorageTemp"].ToString());
        }
        ddlConservazione.DataSource = empResult;
        //ddlComposizione.DataValueField = "CompositionVolumeAnticoagulant";
        //ddlComposizione.DataTextField = "CompositionVolumeAnticoagulant";
        ddlConservazione.DataBind();
        ddlConservazione.Items.Insert(0, "");
    }
    protected void imgConserNew_Click(object sender, ImageClickEventArgs e)
    {
        ddlConservazione.Visible = false;
        txtConservazione.Visible = true;
        imgConserClose.Visible = true;
        imgConserNew.Visible = false;
    }
    protected void imgConserClose_Click(object sender, ImageClickEventArgs e)
    {
        HideShowConservazione();
    }
    protected void ddlConservazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtConservazione.Text = ddlConservazione.SelectedValue;
    }

    private void BloodGroupHideShow()
    {
        //ddlGruppoSangue.SelectedIndex = 0;
        ddlGruppoSangue.Visible = true;
        txtGruppoSangue.Visible = false;
        imgBloodNew.Visible = true;
        imgBloodClose.Visible = false;
    }
    protected void imgBloodNew_Click(object sender, ImageClickEventArgs e)
    {
        ddlGruppoSangue.Visible = false;
        txtGruppoSangue.Visible = true;
        imgBloodClose.Visible = true;
        imgBloodNew.Visible = false;
        txtGruppoSangue.Text = ddlGruppoSangue.SelectedValue;
    }
    protected void imgBloodClose_Click(object sender, ImageClickEventArgs e)
    {
        BloodGroupHideShow();
    }
    protected void ddlGruppoSangue_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGruppoSangue.Text = ddlGruppoSangue.SelectedValue;
    }



    #region
    public void BindAnimals(string DCodID, string AnimalSpecies)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@DCodId",DCodID),
            new SqlParameter("@AnimalSpecies",AnimalSpecies)
        };
        DataTable dt = cls.ExecuteDataTable("GetAnimalsForMovimento", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            grdAnimals.DataSource = dt;
            grdAnimals.DataBind();
        }
        else
        {
            ClearNuovo();
            pnlAttchmnt.Visible = false;

            //GetDonatorsById(ddlDenominazione.SelectedValue);
            grdAnimals.DataSource = null;
            grdAnimals.DataBind();

            gvAttachments.DataSource = null;
            gvAttachments.DataBind();
            //string message = "alert('Hello! Mudassar.')";
            //ScriptManager.RegisterClientScriptBlock((grdAnimals as Control), this.GetType(), "alert", message, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    private void BindSpeciesOfAnimals()
    {
        DataTable dt = cls.ExecuteDataTable("Select * from Emobank_AnimalSpecies", null, CommandType.Text);
        ddlAnimaleSpecie.DataSource = dt;
        ddlAnimaleSpecie.DataTextField = "SpeciesName";
        ddlAnimaleSpecie.DataValueField = "SpeciesCode";
        ddlAnimaleSpecie.DataBind();
        ddlAnimaleSpecie.Items.Insert(0, new ListItem("--Selezionare Specie--", "0"));

    }

    protected void btnAnimalNuovo_Click(object sender, EventArgs e)
    {
        hdMode.Value = "S";
        BindSpeciesOfAnimals();
        pnlAddAnimal.Visible = true;
        txtAnimalName.Text = "";
        txtPeso.Text = "";
        txtAge.Text = "";
        pnlAttchmnt.Visible = false;
        popup.Visible = false;
        btnAddNewAnimal.Text = "Registra";
    }
    protected void btnAddNewAnimal_Click(object sender, EventArgs e)
    {
        string[] Age = txtAge.Text.Split(',');
        if (Age.Count() == 2)
        {
            if (Convert.ToInt32(Age[1]) > 11)
            {
                msg.Visible = true;
                txtAge.Focus();
                lblmsg.InnerHtml = Common.ShowMessage("Enter valid Età (Anni,Mesi)", 2);
                return;
            }
        }
        int result = 0;
        if (hdMode.Value == "S")
        {
            SqlParameter[] oPera =
                {
                     new SqlParameter("@rVal",SqlDbType.VarChar),
                    new SqlParameter("@DCodID",ddlDenominazione.SelectedValue),
                    new SqlParameter("@AnimalName",txtAnimalName.Text.Trim()),
                    new SqlParameter("@Animalspecies",ddlAnimaleSpecie.SelectedValue.Trim()),
                    new SqlParameter("@AnimalWeight",txtPeso.Text.Trim().Replace(',','.')),
                    new SqlParameter("@AgeAnimal",txtAge.Text.Trim().Replace(',','.')),
                };
            oPera[0].Direction = ParameterDirection.ReturnValue;
            oPera[0].Size = 255;
            result = cls.ExecuteNonQuery("SaveDonorAnimals", oPera, CommandType.StoredProcedure);
            if (result > 0)
            {
                string Progressive = oPera[0].Value.ToString();
                ShowAttachments(Progressive);
            }
            else
            {
                msg.Visible = true;
                lblmsg.InnerHtml = Common.ShowMessage("Errore nei dati.", 3);

            }
        }
        else if (hdMode.Value == "U")
        {
            SqlParameter[] oPera =
                {
                    new SqlParameter("@DCodID",ddlDenominazione.SelectedValue),
                    new SqlParameter("@AnimalName",txtAnimalName.Text.Trim()),
                    new SqlParameter("@Animalspecies",ddlAnimaleSpecie.SelectedValue.Trim()),
                    new SqlParameter("@AnimalWeight",txtPeso.Text.Trim().Replace(',','.')),
                    new SqlParameter("@AgeAnimal",txtAge.Text.Trim().Replace(',','.')),
                    new SqlParameter("@Progressive", hdProgressive.Value)
                };


            result = cls.ExecuteNonQuery("UpdateDonorAnimals", oPera, CommandType.StoredProcedure);
        }
        msg.Visible = true;
        if (result > 0)
        {
            BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue);
            lblmsg.InnerHtml = Common.ShowMessage("Record salvato con successo", 1);
        }
        else if (result == -1)
        {
            lblmsg.InnerHtml = Common.ShowMessage("Duplica il record", 2);
        }
        else if (result == 0)
        {
            lblmsg.InnerHtml = Common.ShowMessage("Errore nei dati.", 3);
        }
    }
    protected void btnBackAnimalList_Click(object sender, EventArgs e)
    {
        pnlAddAnimal.Visible = false;
        pnlAnimalList.Visible = true;
    }
    public void ClearInputAnimal()
    {
        BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue);
        ddlAnimaleSpecie.ClearSelection();
        txtAnimalName.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtPeso.Text = string.Empty;
        //pnlAddAnimal.Visible = false;
        //pnlAnimalList.Visible = true;
        txtDataInserimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtDescrizioneAllegato.Text = string.Empty;
        txtDescrizioneAllegato.Text = "";
        ddlDenominazione.ClearSelection();

    }

    protected void grdAnimals_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ltrID = "0";
        if (e.CommandName == "DEL")
        {
            ltrID = grdAnimals.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@DCodID",ddlDenominazione.SelectedValue),
             new SqlParameter("@Progressive",ltrID)
        };
            int a = cls.ExecuteNonQuery("DeleteAnimalsDonor", oPera, CommandType.StoredProcedure);
            if (a > 0)
            {

            }
            else
            {
                msg.Visible = true;
                lblmsg.InnerHtml = Common.ShowMessage("Impossibile eliminare perché gli allegati del Registro di sistema sono collegate con questo animale .", 2);
            }
            BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue);
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
            for (int i = 1; i < e.Row.Cells.Count; i++)
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
            //GridView grd = (GridView)e.Row.FindControl("gvOrders");

            //int id = Convert.ToInt32(grdAnimals.DataKeys[e.Row.RowIndex].Values[0]);
            //string group = grdAnimals.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //BindAnimalAttachment(ddlDenominazione.SelectedValue, group, grd);


        }
    }
    protected void lnkFilter_Click(object sender, EventArgs e)
    {
        string DCodId = ddlDenominazione.SelectedValue;

    }
    private void BindAnimalAttachment(string DCodId, string Progressive, GridView grd)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@DCodId",DCodId),
            new SqlParameter("@Progressive",Progressive)
        };
        DataTable dt = cls.ExecuteDataTable("BindAttchments", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            grd.DataSource = dt;
            grd.DataBind();
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
        }
    }
    protected void btnAttachmentNuovo_Click(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string PhotoExt = "";
        if (upFile.HasFile == false)
        {
            // BindMovimentoPrelieviByCodId(txtCodice.Text);
            GetSamplingPointById(ddlPuntoPrelievo.SelectedValue);
            msg.Visible = true;
            lblmsg.InnerHtml = Common.ShowMessage("Selezionare Fissaggio", 2);
        }
        else if (upFile.HasFile)
        {

            string path = Server.MapPath("Uploads/Donor_Registry_Attachments/");
            PhotoExt = Path.GetExtension(upFile.PostedFile.FileName.ToString());
            string filename = upFile.PostedFile.FileName;

            SqlParameter[] oPera =

        {
            new SqlParameter("@FileName",SqlDbType.VarChar),
            new SqlParameter("@DCodID",ddlDenominazione.SelectedValue),
            new SqlParameter("@Progressive",hdProgressive.Value),
            new SqlParameter("@LinkAttachment",filename),
            new SqlParameter("@DataInserimento",DateTime.Now.ToString("MM/dd/yyyy")),
            new SqlParameter("@DescrizioneAllegato",txtDescrizioneAllegato.Text),
        };
            oPera[0].Direction = ParameterDirection.Output;
            oPera[0].Size = 255;

            int result = cls.ExecuteNonQuery("Save_Donor_Registry_Attachments", oPera, CommandType.StoredProcedure);

            msg.Visible = true;

            if (result > 0)
            {
                //ClearInputAnimal();

                string FileName = oPera[0].Value.ToString();
                upFile.PostedFile.SaveAs(path + FileName);
                ShowAttachments(hdProgressive.Value);
                if (btnRegister.Text != "Registra")
                {
                    BindMovimentoPrelieviByCodId(txtCodice.Text);
                }
                else
                {
                    OnddlTipoPreparatoChange();
                }
                GetSamplingPointById(ddlPuntoPrelievo.SelectedValue);
                BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue);
                //txtDescrizioneAllegato.Text = "";
                lblmsg.InnerHtml = Common.ShowMessage("Allegato salvato con successo", 1);

            }
            else if (result == -1)
            {
                lblmsg.InnerHtml = Common.ShowMessage("Duplica il record", 2);
            }
            else if (result == 0)
            {
                lblmsg.InnerHtml = Common.ShowMessage("Errore nei dati.", 3);
            }
        }
    }



    protected void btnAllegati_Click(object sender, EventArgs e)
    {

    }
    protected void grdAnimals_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow row = grdAnimals.SelectedRow;
        string Progressive = grdAnimals.DataKeys[row.RowIndex].Values[0].ToString();
        //hdProgressive.Value = Progressive;
        hdMode.Value = "U";
        ShowAttachments(Progressive);
        btnAddNewAnimal.Text = "Aggiornare";
        BindSpeciesOfAnimals();

    }
    private void BindAnimalsById(string Codice, string Progressive)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@DCodId",Codice),
            new SqlParameter("@Progressive",Progressive),
        };
        DataTable dt = cls.ExecuteDataTable("BindAnimalById", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            ddlAnimaleSpecie.SelectedValue = dt.Rows[0]["Animalspecies"].ToString();

            // ddlAnimaleSpecie.Attributes.Add("style", "disabled");
            txtAnimalName.Text = dt.Rows[0]["AnimalName"].ToString();
            txtAge.Text = dt.Rows[0]["AgeAnimal"].ToString().Replace('.', ',');
            txtPeso.Text = dt.Rows[0]["AnimalWeight"].ToString().Replace('.', ',');
            popup.Visible = true;
        }
    }
    private void ShowAttachments(string Progressive)
    {
        //hdMode.Value = "U";
        hdProgressive.Value = Progressive;
        pnlAddAnimal.Visible = true;
        pnlAttchmnt.Visible = true;
        BindAnimalsById(ddlDenominazione.SelectedValue, Progressive);
        BindAnimalAttachment(ddlDenominazione.SelectedValue, Progressive, gvAttachments);
    }
    protected void gvAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ltrID = "0";
        if (e.CommandName == "DEL")
        {
            ltrID = gvAttachments.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@DCodID",ddlDenominazione.SelectedValue),
             new SqlParameter("@Progressive",hdProgressive.Value),
             new SqlParameter("@ProgressiveAttachment",ltrID)
        };
            int a = cls.ExecuteNonQuery("Delete_Donor_Registry_Attachments", oPera, CommandType.StoredProcedure);
            if (a > 0)
            {
                msg.Visible = true;
                //lblmsg.InnerHtml = Common.ShowMessage("Record eliminato Succussfully", 2);
                lblmsg.InnerHtml = "";
            }
            else
            {
                msg.Visible = true;
                lblmsg.InnerHtml = Common.ShowMessage("Impossibile eliminare perché gli allegati del Registro di sistema sono collegate con questo animale.", 2);
            }
            ShowAttachments(hdProgressive.Value);
        }
    }


    #endregion






    protected void btnStampaEti_Click(object sender, EventArgs e)
    {
        Response.Redirect("PrintLabel.aspx?CodId=" + txtCodice.Text);
    }
}