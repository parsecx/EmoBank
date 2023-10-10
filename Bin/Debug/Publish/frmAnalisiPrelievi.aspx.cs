using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmAnalisiPrelievi : System.Web.UI.Page
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
                    BindMovimentoPrelievi(ddlSeleziona.SelectedValue);
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
    private DataTable BindMovimentoPrelievi(string Type)
    {
        SqlParameter[] para =
        {
           new SqlParameter("@Type",Type)
       };
        DataTable dt = cls.ExecuteDataTable("GetAnalisiPrelievi", para, CommandType.StoredProcedure);
        //BindGridDropdowns(dt);
        if (dt.Rows.Count > 0)
        {
            BindDropDowns();
            grdData.DataSource = dt;
            grdData.DataBind();

        }
        else
        {
            grdData.DataSource = null;
            grdData.DataBind();
        }
        //grdData.DataSource = dt;
        //grdData.DataBind();
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
        ImportExport.ExportToPDF(grdData, "Analisi Prelievi", "", a, "A4", true);
    }
    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        HideShowDiv(1); BindDropDowns();

    }
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gv = grdData.SelectedRow;
        string CodInt = grdData.SelectedDataKey.Value.ToString();
        BindMovimentoPrelieviByCodId(CodInt);

        BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue);
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
            BindMovimentoPrelievi(ddlSeleziona.SelectedValue);
        }
    }
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdDateLoadBloodBankRefrigerator = (HiddenField)e.Row.FindControl("hdDateLoadBloodBankRefrigerator");
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdData, "Select$" + e.Row.RowIndex);
                e.Row.Cells[i].ToolTip = "Click to Enter detail.";
                if (string.IsNullOrEmpty(hdDateLoadBloodBankRefrigerator.Value))
                {
                    e.Row.Cells[i].Attributes.Add("style", "cursor:pointer;color:blue");
                }
                else
                {
                    e.Row.Cells[i].Attributes.Add("style", "cursor:pointer");
                }
            }
            if (Session["Ricerca"] != null)
            {
                BindGridDropdowns("AdvanceSearch");
            }
            else
            {
                BindGridDropdowns(ddlSeleziona.SelectedValue);
            }

            //if (string.IsNullOrEmpty(hdDateLoadBloodBankRefrigerator.Value))
            //{
            //    e.Row.BackColor = ColorTranslator.FromHtml("#f96868");

            //}
        }
        //else if(e.Row.RowType == DataControlRowType.Header)
        //{
        //    BindGridDropdowns("AdvanceSearch");
        //}
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

        DataView sortedView = new DataView(BindMovimentoPrelievi(ddlSeleziona.SelectedValue));
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

    private void BindMovimentoPrelieviByCodId(string CodId)
    {
        try
        {


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
                BindDropDowns();

                txtUtente.Text = Convert.ToString(Session["NameUser"]);
                txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtCodice.Text = dt.Rows[0]["CodID"].ToString();
                txtCInterno.Text = dt.Rows[0]["CodID"].ToString();
                txtProtocollo.Text = dt.Rows[0]["ProtocolNumber"].ToString();
                txtProgressivo.Text = dt.Rows[0]["Progressive"].ToString();
                txtDatePrelievo.Text = Convert.ToDateTime(dt.Rows[0]["DateTimeDrawing"]).ToString("dd/MM/yyyy");
                ddlPuntoPrelievo.SelectedValue = dt.Rows[0]["CodeIDSamplingPoint"].ToString();
                string OperatorWithdrawal = dt.Rows[0]["OperatorWithdrawal"].ToString();
                //ddlTipoPreparato.SelectedValue = dt.Rows[0]["TypePrepared"].ToString();
                ddlTipoPreparato.ClearSelection();
                ddlTipoPreparato.Items.FindByText(dt.Rows[0]["TypePrepared"].ToString()).Selected = true;

                txtComposizione.Text = dt.Rows[0]["CompositionVolumeAnticoagulant"].ToString();
                txtConservazione.Text = dt.Rows[0]["ModeStorageTemp"].ToString();
                txtDataScadenza.Text = Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"]).ToString("dd/MM/yyyy");

                txtGruppoSangue.Text = dt.Rows[0]["AnimalGroupBlood"].ToString();
                txtPesoLordo.Text = dt.Rows[0]["GrossWeightPrepared"].ToString();


                ddlDenominazione.SelectedValue = dt.Rows[0]["CodIDDonors"].ToString();

                ddlSpecieAnimale.SelectedValue = dt.Rows[0]["IndicationAnimalSpecies"].ToString();
                try
                {
                    ddlDenominazioneB.SelectedValue = dt.Rows[0]["CodIDBankRefrigerator"].ToString();
                }
                catch (Exception ex)
                {

                }
                txtclincaDataRichiesta.Text = dt.Rows[0]["DateAcquisitionRequest"].ToString();
                txtclincaUtente.Text = dt.Rows[0]["Userdrainbloodbankrefrigerator"].ToString();
                txtclincaDataScaricoFrigo.Text = dt.Rows[0]["Datedrainbloodbankrefrigerator"].ToString();
                txtclincaProtocolllo.Text = dt.Rows[0]["NumberProtocolTrasmissionClinical"].ToString();
                txtclincaDataConsegna.Text = dt.Rows[0]["DatereceiptClinic"].ToString();
                string ddlclincaden = "0";
                //DateLoadBloodBankRefrigerator
                //var value = dt.Rows[0]["DateLoadBloodBankRefrigerator"].ToString();

                if (dt.Rows[0]["CodeIDReqVeterinaryClinic"].ToString() != "")
                {
                    ddlclincadeno.SelectedValue = dt.Rows[0]["CodeIDReqVeterinaryClinic"].ToString();
                }
                else
                {
                    ddlclincadeno.SelectedValue = ddlclincaden;
                    string CodeID_Point_Clinic = Convert.ToString(Session["Sample_Clinic_Code"]);
                    string UserType = Convert.ToString(Session["UserType"]);
                    if (!string.IsNullOrEmpty(CodeID_Point_Clinic) && !string.IsNullOrEmpty(UserType))
                    {
                        if (UserType == "C")
                        {
                            if (ddlclincadeno.Items.FindByValue(CodeID_Point_Clinic) != null)
                            {
                                ddlclincadeno.SelectedValue = CodeID_Point_Clinic;
                            }
                        }
                    }
                }

                if (dt.Rows[0]["CodeIDReqVeterinaryClinic"].ToString() != "" || dt.Rows[0]["DatereceiptClinic"].ToString() != "")
                {
                    pnlClinicalVeterinaria.Visible = true;
                }
                else
                {
                    pnlClinicalVeterinaria.Visible = false;
                }
                if (dt.Rows[0]["DateLoadBloodBankRefrigerator"].ToString() != "")
                {
                    pnlCaricoFrigo.Visible = true;
                }
                else
                {
                    pnlCaricoFrigo.Visible = false;
                }


                GetSamplingPointById(ddlPuntoPrelievo.SelectedValue);
                GetDonatorsById(ddlDenominazione.SelectedValue);
                BindCentralBankById(ddlDenominazioneB.SelectedValue);
                GetEmobank_ClinicsById(ddlclincadeno.SelectedValue);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlTipoPreparato_SelectedIndexChanged(object sender, EventArgs e)
    {
        OnddlTipoPreparatoChange();
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
    #endregion

    #region Add New Record
    private void BindDropDowns()
    {
        GetSamplingPoints(); GetAnimalSpecies(); GetDonators(); GetCentralBanks(); GetEmobank_Clinics(); BindBloodGroup(); GetTipoPreparato(); GetFrigoEmoteca();
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        int CodIdDonor = 0;
        string Type;
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

            SqlParameter[] Opare =
            {
             //new SqlParameter("@ProtocolNumber",txtProtocollo.Text),
             //new SqlParameter("@Progressive",txtProgressivo.Text),
             new SqlParameter("@CodId",ltrID.Text),
             new SqlParameter("@DateAcquisitionRequest",Convert.ToDateTime(txtclincaDataRichiesta.Text).ToString("MM/dd/yyyy")),
             new SqlParameter("@Userdrainbloodbankrefrigerator",txtclincaUtente.Text),
             new SqlParameter("@Datedrainbloodbankrefrigerator",Convert.ToDateTime(txtclincaDataScaricoFrigo.Text).ToString("MM/dd/yyyy")),
             new SqlParameter("@NumberProtocolTrasmissionClinical",txtclincaProtocolllo.Text),
             new SqlParameter("@DatereceiptClinic",Convert.ToDateTime(txtclincaDataConsegna.Text).ToString("MM/dd/yyyy")),
              new SqlParameter("@CodeIDReqVeterinaryClinic",ddlclincadeno.SelectedValue),
             new SqlParameter("@Rval",SqlDbType.Int)
              };
            Opare[7].Direction = ParameterDirection.ReturnValue;
            int rval = cls.ExecuteNonQuery("Update_IIDrawing_movements", Opare, CommandType.StoredProcedure);
            if (rval > 0)
            {
                rval = Convert.ToInt32(Opare[7].Value);
                if (rval > 0)
                {
                    CodIdDonor = rval;
                    BindMovimentoPrelievi(ddlSeleziona.SelectedValue);
                    HideShowDiv(0);
                    lblmsg.InnerHtml = Common.ShowMessage("Record aggiornato con successo.", 1);
                }
                else
                {
                    lblmsg.InnerHtml = Common.ShowMessage("Errore nei dati. Il contatto con l'amministratore", 2);
                }
            }
            else
            {
                lblmsg.InnerHtml = Common.ShowMessage("Errore nei dati. Il contatto con l'amministratore", 2);
            }

        }
    }
    public bool Validation()
    {

        return true;
    }
    protected void ddlSeleziona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSeleziona.SelectedValue == "Nuovi Per FrigoEmoteca" || ddlSeleziona.SelectedValue == "Presenti In FrigoEmoteca" || ddlSeleziona.SelectedValue == "Consegnati Cliniche")
        {
            Session["Ricerca"] = null;
        }
        BindMovimentoPrelievi(ddlSeleziona.SelectedValue);
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
    private DataTable GetCentralBanks()
    {
        DataTable dt = cls.ExecuteDataTable("GetCentralBanks", null, CommandType.StoredProcedure);
        ddlDenominazioneB.DataSource = dt;
        ddlDenominazioneB.DataTextField = "Description";
        ddlDenominazioneB.DataValueField = "CodID";
        ddlDenominazioneB.DataBind();
        ddlDenominazioneB.Items.Insert(0, new ListItem("--Select Banca--", "0"));
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

        ddlAmbuPrelievo.DataSource = dt;
        ddlAmbuPrelievo.DataTextField = "Description";
        ddlAmbuPrelievo.DataValueField = "CodID";
        ddlAmbuPrelievo.DataBind();
        ddlAmbuPrelievo.Items.Insert(0, new ListItem("--Select Punto Prelievo--", "0"));

        return dt;
    }
    private DataTable GetAnimalSpecies()
    {
        DataTable dt = cls.ExecuteDataTable("Select * from Emobank_AnimalSpecies", null, CommandType.Text);
        ddlSpecieAnimale.DataSource = dt;
        ddlSpecieAnimale.DataTextField = "SpeciesName";
        ddlSpecieAnimale.DataValueField = "SpeciesCode";
        ddlSpecieAnimale.DataBind();
        ddlSpecieAnimale.Items.Insert(0, new ListItem("--Select Specie Animale--", "0"));

        ddlAnimale.DataSource = dt;
        ddlAnimale.DataTextField = "SpeciesName";
        ddlAnimale.DataValueField = "SpeciesCode";
        ddlAnimale.DataBind();
        ddlAnimale.Items.Insert(0, new ListItem("--Select Specie Animale--", "0"));

        return dt;
    }
    private DataTable GetDonators()
    {
        DataTable dt = cls.ExecuteDataTable("GetDonators", null, CommandType.Text);
        ddlDenominazione.DataSource = dt;
        ddlDenominazione.DataTextField = "Name";
        ddlDenominazione.DataValueField = "DCodID";
        ddlDenominazione.DataBind();

        ddlDonator.DataSource = dt;
        ddlDonator.DataTextField = "Name";
        ddlDonator.DataValueField = "DCodID";
        ddlDonator.DataBind();

        if (dt.Rows.Count > 0)
        {
            ddlDenominazione.Items.Insert(0, new ListItem("--Select Denominazione--", "0"));
            ddlDonator.Items.Insert(0, new ListItem("--Select Denominazione--", "0"));
        }
        else
        {
            ddlDenominazione.Items.Insert(0, new ListItem("--Dati non disponibili--", "0"));
            ddlDonator.Items.Insert(0, new ListItem("--Dati non disponibili--", "0"));
        }


        return dt;
    }
    private DataTable GetEmobank_Clinics()
    {
        DataTable dt = cls.ExecuteDataTable("GetEmobank_Clinics", null, CommandType.Text);
        ddlclincadeno.DataSource = dt;
        ddlclincadeno.DataTextField = "Description";
        ddlclincadeno.DataValueField = "CodID";
        ddlclincadeno.DataBind();
        ddlclincadeno.Items.Insert(0, new ListItem("--Select Denominazione--", "0"));

        ddlClinicVeterina.DataSource = dt;
        ddlClinicVeterina.DataTextField = "Description";
        ddlClinicVeterina.DataValueField = "CodID";
        ddlClinicVeterina.DataBind();
        ddlClinicVeterina.Items.Insert(0, new ListItem("--Select Denominazione--", "0"));

        return dt;
    }

    private DataTable GetFrigoEmoteca()
    {
        DataTable dt = cls.ExecuteDataTable("GetCentralBanks", null, CommandType.StoredProcedure);
        ddlFrigoEmoteca.DataSource = dt;
        ddlFrigoEmoteca.DataTextField = "Description";
        ddlFrigoEmoteca.DataValueField = "CodID";
        ddlFrigoEmoteca.DataBind();
        ddlFrigoEmoteca.Items.Insert(0, new ListItem("--Select FrigoEmoteca--", "0"));
        return dt;
    }

    private void BindBloodGroup()
    {
        DataTable dt = cls.ExecuteDataTable("Select * from Emobank_BloodGroup", CommandType.Text);
        ddlGruppoSangue.DataSource = dt;
        ddlGruppoSangue.DataTextField = "BloodGroup";
        ddlGruppoSangue.DataValueField = "BloodGroup";
        ddlGruppoSangue.DataBind();
        ddlGruppoSangue.Items.Insert(0, new ListItem("--Gruppo Sangue--", "0"));
    }
    #endregion
    protected void ddlPuntoPrelievo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSamplingPointById(ddlPuntoPrelievo.SelectedValue);
    }
    private void BindCentralBankById(string CntrBnkId)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CntrBnkId)
        };
        DataTable dt = new DataTable();
        dt = cls.ExecuteDataTable("GetCentralBankById", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {

            txtCapB.Text = dt.Rows[0]["PostalCode"].ToString();
            txtIndirzzoB.Text = dt.Rows[0]["Address"].ToString();
            txtLocalitaB.Text = dt.Rows[0]["resort"].ToString();
            txtProvinciaB.Text = dt.Rows[0]["province"].ToString();
            //txtReferenteU.Text = dt.Rows[0]["Contactperson"].ToString();
        }
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
    private void GetEmobank_ClinicsById(string CodId)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CodId)
        };
        DataTable dt = cls.ExecuteDataTable("GetEmobank_ClinicsById", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            txtclincaIndirrzo.Text = dt.Rows[0]["Address"].ToString();
            txtclincaLocalita.Text = dt.Rows[0]["resort"].ToString();
            txtclincacap.Text = dt.Rows[0]["PostalCode"].ToString();
            txtclincaProvincia.Text = dt.Rows[0]["province"].ToString();
        }
    }
    protected void ddlDenominazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDonatorsById(ddlDenominazione.SelectedValue);
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
            //txtDenominazione.ReadOnly = false;
            txtDIndirzzo.ReadOnly = false;
            txtDLocalita.ReadOnly = false;
            txtDCap.ReadOnly = false;
            txtDE_mail.ReadOnly = false;
            txtDTelefono.ReadOnly = false;
            txtDProvincia.ReadOnly = false;
            //txtDenominazione.ReadOnly = false;

            // txtDenominazione.Text = dt.Rows[0]["Name"].ToString();
            txtDIndirzzo.Text = dt.Rows[0]["Address"].ToString();
            txtDLocalita.Text = dt.Rows[0]["resort"].ToString();
            txtDCap.Text = dt.Rows[0]["PostalCode"].ToString();
            txtDE_mail.Text = dt.Rows[0]["Email"].ToString();
            txtDTelefono.Text = dt.Rows[0]["Phone"].ToString();
            txtDProvincia.Text = dt.Rows[0]["Provincie"].ToString();
        }
        hdNewDonor.Value = "0";
        //txtDenominazione.ReadOnly = true;
        txtDIndirzzo.ReadOnly = true;
        txtDLocalita.ReadOnly = true;
        txtDCap.ReadOnly = true;
        txtDE_mail.ReadOnly = true;
        txtDTelefono.ReadOnly = true;
        txtDProvincia.ReadOnly = true;
        // txtDenominazione.ReadOnly = true;
    }
    protected void btnDonatorNuovo_Click(object sender, EventArgs e)
    {
        ddlDenominazione.SelectedIndex = 0;
        hdNewDonor.Value = "1";
        txtDIndirzzo.Text = "";
        txtDLocalita.Text = "";
        txtDCap.Text = "";
        txtDE_mail.Text = "";
        txtDTelefono.Text = "";
        txtDProvincia.Text = "";
        //txtDenominazione.Text = "";

        txtDIndirzzo.ReadOnly = false;
        txtDLocalita.ReadOnly = false;
        txtDCap.ReadOnly = false;
        txtDE_mail.ReadOnly = false;
        txtDTelefono.ReadOnly = false;
        txtDProvincia.ReadOnly = false;
        // txtDenominazione.ReadOnly = false;
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
        txtGruppoSangue.Text = string.Empty;
        txtIndirzzo.Text = string.Empty;
        txtLocalita.Text = string.Empty;
        txtPesoLordo.Text = string.Empty;
        txtProgressivo.Text = string.Empty;
        txtProtocollo.Text = string.Empty;
        txtProvincia.Text = string.Empty;
        txtReferente.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        ddlPuntoPrelievo.SelectedIndex = 0;
        ddlSpecieAnimale.SelectedIndex = 0;
        ddlTipoPreparato.SelectedIndex = 0;
        ddlDenominazione.SelectedIndex = 0;
        hdNewDonor.Value = "0";
        txtDIndirzzo.Text = "";
        txtDLocalita.Text = "";
        txtDCap.Text = "";
        txtDE_mail.Text = "";
        txtDTelefono.Text = "";
        txtDProvincia.Text = "";
        //txtDenominazione.Text = "";

        txtDIndirzzo.ReadOnly = false;
        txtDLocalita.ReadOnly = false;
        txtDCap.ReadOnly = false;
        txtDE_mail.ReadOnly = false;
        txtDTelefono.ReadOnly = false;
        txtDProvincia.ReadOnly = false;
        // txtDenominazione.ReadOnly = false;
    }
    protected void ddlDenominazioneB_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCentralBankById(ddlDenominazioneB.SelectedValue);
    }
    private DataTable BindGridDropdowns(string Type)
    {

        SqlParameter[] para =
      {
             new SqlParameter("@Type", Type)
        };

        DataTable dt = cls.ExecuteDataTable("GetAnalisiPrelievi", para, CommandType.StoredProcedure);

        //BindDropdowns(ddlCod1, dt, "CodID", "CodID");

        //BindDropdowns(ddlProtocol1, dt, "ProtocolNumber", "ProtocolNumber");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlProtocol1"), dt, "ProtocolNumber", "ProtocolNumber");
        if (Session["ddlprotocol1"] != null && !string.IsNullOrEmpty(Session["ddlprotocol1"].ToString()) && !Session["ddlprotocol1"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlProtocol1")).SelectedValue = Session["ddlprotocol1"].ToString();
        }

        //BindDropdowns(ddlDateTimeDrawing1, dt, "DateTimeDrawing", "DateTimeDrawing");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlDateTimeDrawing1"), dt, "DateTimeDrawing", "DateTimeDrawing");
        if (Session["ddlDateTimeDrawing1"] != null && !string.IsNullOrEmpty(Session["ddlDateTimeDrawing1"].ToString()) && !Session["ddlDateTimeDrawing1"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlDateTimeDrawing1")).SelectedValue = Session["ddlDateTimeDrawing1"].ToString();
        }

        //BindDropdowns(ddlDescription1, dt, "Description", "Description");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlDescription1"), dt, "Description", "Description");
        if (Session["ddlDescription1"] != null && !string.IsNullOrEmpty(Session["ddlDescription1"].ToString()) && !Session["ddlDescription1"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlDescription1")).SelectedValue = Session["ddlDescription1"].ToString();
        }

        //BindDropdowns(ddlSpeciesName1, dt, "SpeciesName", "SpeciesName");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlSpeciesName1"), dt, "SpeciesName", "SpeciesName");
        if (Session["ddlSpeciesName1"] != null && !string.IsNullOrEmpty(Session["ddlSpeciesName1"].ToString()) && !Session["ddlSpeciesName1"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlSpeciesName1")).SelectedValue = Session["ddlSpeciesName1"].ToString();
        }

        //BindDropdowns(ddlAnimalGroupBlood1, dt, "AnimalGroupBlood", "AnimalGroupBlood");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlAnimalGroupBlood1"), dt, "AnimalGroupBlood", "AnimalGroupBlood");
        if (Session["ddlAnimalGroupBlood1"] != null && !string.IsNullOrEmpty(Session["ddlAnimalGroupBlood1"].ToString()) && !Session["ddlAnimalGroupBlood1"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlAnimalGroupBlood1")).SelectedValue = Session["ddlAnimalGroupBlood1"].ToString();
        }

        //BindDropdowns(ddlNamee1, dt, "Name", "Name");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlNamee1"), dt, "Name", "Name");

        if (Session["ddlNamee1"] != null && !string.IsNullOrEmpty(Session["ddlNamee1"].ToString()) && !Session["ddlNamee1"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlNamee1")).SelectedValue = Session["ddlNamee1"].ToString();
        }

        //BindDropdowns(ddldatacarFrigo, dt, "DateLoadBloodBankRefrigerator", "DateLoadBloodBankRefrigerator");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddldatacarFrigo"), dt, "DateLoadBloodBankRefrigerator", "DateLoadBloodBankRefrigerator");

        if (Session["ddldatacarFrigo"] != null && !string.IsNullOrEmpty(Session["ddldatacarFrigo"].ToString()) && !Session["ddldatacarFrigo"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddldatacarFrigo")).SelectedValue = Session["ddldatacarFrigo"].ToString();
        }

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlFrigoEmo"), dt, "FrigoEmoteca", "FrigoEmoteca");

        if (Session["ddlFrigoEmo"] != null && !string.IsNullOrEmpty(Session["ddlFrigoEmo"].ToString()) && !Session["ddlFrigoEmo"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlFrigoEmo")).SelectedValue = Session["ddlFrigoEmo"].ToString();
        }

        //BindDropdowns(ddlDataScaFrigo, dt, "DatedrainBloodBankRefrigerator", "DatedrainBloodBankRefrigerator");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlDataScaFrigo"), dt, "DatedrainBloodBankRefrigerator", "DatedrainBloodBankRefrigerator");

        if (Session["ddlDataScaFrigo"] != null && !string.IsNullOrEmpty(Session["ddlDataScaFrigo"].ToString()) && !Session["ddlDataScaFrigo"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlDataScaFrigo")).SelectedValue = Session["ddlDataScaFrigo"].ToString();
        }

        //BindDropdowns(ddlClinicaVetern, dt, "ClinicName", "ClinicName");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlClinicaVetern"), dt, "ClinicName", "ClinicName");
        if (Session["ddlClinicaVetern"] != null && !string.IsNullOrEmpty(Session["ddlClinicaVetern"].ToString()) && !Session["ddlClinicaVetern"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlClinicaVetern")).SelectedValue = Session["ddlClinicaVetern"].ToString();
        }

        //BindDropdowns(ddlDataConsegna, dt, "DateReceiptClinic", "DateReceiptClinic");

        BindDropdowns((DropDownList)grdData.HeaderRow.FindControl("ddlDataConsegna"), dt, "DateReceiptClinic", "DateReceiptClinic");

        if (Session["ddlDataConsegna"] != null && !string.IsNullOrEmpty(Session["ddlDataConsegna"].ToString()) && !Session["ddlDataConsegna"].ToString().Equals("all", StringComparison.CurrentCultureIgnoreCase))
        {
            ((DropDownList)grdData.HeaderRow.FindControl("ddlDataConsegna")).SelectedValue = Session["ddlDataConsegna"].ToString();
        }

        return dt;
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

        DataView sortedView = new DataView(BindMovimentoPrelievi(ddlSeleziona.SelectedValue));
        //sortedView.Sort = e.SortExpression + " " + sortingDirection;
        sortedView.Sort = ColumnName + " " + sortingDirection;
        grdData.DataSource = sortedView;
        grdData.DataBind();
    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        SortingFunction(lnk.CommandArgument.ToString());
    }

    private void BindDropdowns(DropDownList ddl, DataTable dt, string DataTextField, string DataValueField)
    {
        DataTable uniqueCols1 = dt.DefaultView.ToTable(true, DataTextField);

        DataTable uniqueCols = new DataTable();
        if (uniqueCols1.Select().Where(z => !z.IsNull(0)).Count() > 0)
        {
            uniqueCols = uniqueCols1.Select().Where(z => !z.IsNull(0)).CopyToDataTable();
            ddl.DataSource = uniqueCols;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("", "All"));
        }
    }
    protected void ddlCod_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        string value = ddl.SelectedValue;
        DropDownList ddlProtocol1 = (DropDownList)grdData.HeaderRow.FindControl("ddlProtocol1");
        DropDownList ddlDescription1 = (DropDownList)grdData.HeaderRow.FindControl("ddlDescription1");
        DropDownList ddlNamee1 = (DropDownList)grdData.HeaderRow.FindControl("ddlNamee1");
        DropDownList ddlSpeciesName1 = (DropDownList)grdData.HeaderRow.FindControl("ddlSpeciesName1");
        DropDownList ddlDateTimeDrawing1 = (DropDownList)grdData.HeaderRow.FindControl("ddlDateTimeDrawing1");
        DropDownList ddlAnimalGroupBlood1 = (DropDownList)grdData.HeaderRow.FindControl("ddlAnimalGroupBlood1");
        DropDownList ddlClinicaVetern = (DropDownList)grdData.HeaderRow.FindControl("ddlClinicaVetern");
        DropDownList ddlFrigoEmo = (DropDownList)grdData.HeaderRow.FindControl("ddlFrigoEmo");
        DropDownList ddldatacarFrigo = (DropDownList)grdData.HeaderRow.FindControl("ddldatacarFrigo");
        DropDownList ddlDataScaFrigo = (DropDownList)grdData.HeaderRow.FindControl("ddlDataScaFrigo");
        DropDownList ddlDataConsegna = (DropDownList)grdData.HeaderRow.FindControl("ddlDataConsegna");

        string Type = null;

        if (Session["Ricerca"] != null)
        {
            Type = "AdvanceSearch";
        }
        else
        {
            Type = ddlSeleziona.SelectedValue;
        }

        SqlParameter[] oPera =
        {
            new SqlParameter("@CodID","All"),
            new SqlParameter("@ProtocolNumber",ddlProtocol1.SelectedValue),
            new SqlParameter("@Description",ddlDescription1.SelectedValue),
            new SqlParameter("@Name",ddlNamee1.SelectedValue),
            new SqlParameter("@SpeciesName",ddlSpeciesName1.SelectedValue),
            new SqlParameter("@DateTimeDrawing",ddlDateTimeDrawing1.SelectedValue),
            new SqlParameter("@AnimalGroupBlood",ddlAnimalGroupBlood1.SelectedValue),
            new SqlParameter("@ClinicDescription", ddlClinicaVetern.SelectedValue),
            new SqlParameter("@FrigoEmoteca", ddlFrigoEmo.SelectedValue),
            new SqlParameter("@DataCarFrigo", ddldatacarFrigo.SelectedValue),
            new SqlParameter("@DataScaFridge", ddlDataScaFrigo.SelectedValue),
            new SqlParameter("@DataConsegna", ddlDataConsegna.SelectedValue),
            new SqlParameter("@Type", Type)

        };

        Session["ddlProtocol1"] = ddlProtocol1.SelectedValue;
        Session["ddlDescription1"] = ddlDescription1.SelectedValue;
        Session["ddlNamee1"] = ddlNamee1.SelectedValue;
        Session["ddlSpeciesName1"] = ddlSpeciesName1.SelectedValue;
        Session["ddlDateTimeDrawing1"] = ddlDateTimeDrawing1.SelectedValue;
        Session["ddlAnimalGroupBlood1"] = ddlAnimalGroupBlood1.SelectedValue;
        Session["ddlClinicaVetern"] = ddlClinicaVetern.SelectedValue;
        Session["ddldatacarFrigo"] = ddldatacarFrigo.SelectedValue;
        Session["ddlFrigoEmo"] = ddlFrigoEmo.SelectedValue;
        Session["ddlDataScaFrigo"] = ddlDataScaFrigo.SelectedValue;
        Session["ddlDataConsegna"] = ddlDataConsegna.SelectedValue;

        DataTable dt = cls.ExecuteDataTable("GetAnalisiPreleviFilter", oPera, CommandType.StoredProcedure);
        grdData.DataSource = dt;
        grdData.DataBind();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "myfunction", "$(document).ready(function(){ $('.chzn-select').chosen();});", true);
    }
    protected void ddlclincadeno_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmobank_ClinicsById(ddlclincadeno.SelectedValue);
    }


    #region Popup Search

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@DateTimeDrawingFrom",txtFromPrelievoDate.Text),
            new SqlParameter("@DateTimeDrawingTo",txtToPrelievoDate.Text),
            new SqlParameter("@ProtocolNumberFrom",txtProtocolFrom.Text),
            new SqlParameter("@ProtocolNumberTo",txtProtocolTo.Text),
            new SqlParameter("@CodeIDSamplingPoint",ddlAmbuPrelievo.SelectedValue),
            new SqlParameter("@IndicationAnimalSpecies",ddlAnimale.SelectedValue),
            new SqlParameter("@AnimalGroupBlood",ddlGruppoSangue.SelectedValue),
            new SqlParameter("@CodIDDonors",ddlDonator.SelectedValue),
            new SqlParameter("@DateLoadBloodBankRefrigeratorFrom",txtDataCaricoFromDate.Text),
            new SqlParameter("@FrigoEmoteca",ddlFrigoEmoteca.SelectedValue),
            new SqlParameter("@DateLoadBloodBankRefrigeratorTo",txtDataCaricoToDate.Text),
            new SqlParameter("@CodeIDReqVeterinaryClinic",ddlClinicVeterina.SelectedValue),
            new SqlParameter("@DateReceiptClinicFrom",txtDataConsegnaFromDate.Text),
            new SqlParameter("@DateReceiptClinicTo",txtDataConsegnaToDate.Text),

        };
        DataTable dt = cls.ExecuteDataTable("GetAdvanceSearchResult", oPera, CommandType.StoredProcedure);

        if (txtFromPrelievoDate.Text == "" && txtToPrelievoDate.Text == "" && txtProtocolFrom.Text == "" && txtProtocolTo.Text == "" &&
            ddlAmbuPrelievo.SelectedValue == "0" && ddlAnimale.SelectedValue == "0" && ddlGruppoSangue.SelectedValue == "0" &&
            ddlDonator.SelectedValue == "0" && txtDataCaricoFromDate.Text == "" && ddlFrigoEmoteca.SelectedValue == "0" && txtDataCaricoToDate.Text == ""
            && ddlClinicVeterina.SelectedValue == "0" && txtDataConsegnaFromDate.Text == "" && txtDataConsegnaToDate.Text == "")
        {
            BindGridDropdowns("AdvanceSearch");
            Session["Ricerca"] = "AdvanceSearch";
        }
        BindGridDropdowns("AdvanceSearch");
        grdData.DataSource = dt;
        grdData.DataBind();

    }

    #endregion

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
            grdAnimals.DataSource = null;
            grdAnimals.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}