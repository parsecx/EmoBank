﻿using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmMovimentoRichiesteCliniche : System.Web.UI.Page
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
                    BindClinicaVetern();
                    //BindMovimentoPrelievi(); 
                    BindDropDowns();
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
            BindRichiesteCliniche();
        }
        else if (i == 2)
        {
            List.Visible = false;
            AddNew.Visible = false;
            //Update.Visible = true;
        }

    }
    #region MovimentoPrelievi List
    //private DataTable BindMovimentoPrelievi()
    //{
    //    DataTable dt = null; 
    //    //DataTable dt = cls.ExecuteDataTable("GetMovimentoRichiesteCliniche", null, CommandType.StoredProcedure);
    //    grdData.DataSource = dt;
    //    grdData.DataBind();
    //    return dt;
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }

    protected void btnStampa_Click(object sender, EventArgs e)
    {
        string Codids = "";
        foreach (GridViewRow row in grdData.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk.Checked)
            {
                string CodId = grdData.DataKeys[row.RowIndex].Value.ToString(); ;
                Codids = Codids + CodId + ",";
            }
        }

        Codids = Codids.Remove(Codids.Length - 1, 1);
        Response.Redirect("frmLabelCreate.aspx?CodIds=" + Codids);

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
        BindAnimals(ddlDenominazione.SelectedValue, ddlSpecieAnimale.SelectedValue, txtProgressivo.Text, txtProtocollo.Text);
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
            //BindMovimentoPrelievi();
        }
    }
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //HiddenField hdDateLoadBloodBankRefrigerator = (HiddenField)e.Row.FindControl("hdDateLoadBloodBankRefrigerator");
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdData, "Select$" + e.Row.RowIndex);
                e.Row.Cells[i].ToolTip = "Click to View Detail.";
                //if (string.IsNullOrEmpty(hdDateLoadBloodBankRefrigerator.Value))
                //{
                //    e.Row.Cells[i].Attributes.Add("style", "cursor:pointer;color:blue");
                //}
                //else
                //{
                   e.Row.Cells[i].Attributes.Add("style", "cursor:pointer");
                //}
            }
            //if (string.IsNullOrEmpty(hdDateLoadBloodBankRefrigerator.Value))
            //{
            //    e.Row.BackColor = ColorTranslator.FromHtml("#f96868");

            //}
        }
    }
    protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
    {
        //string sortingDirection = string.Empty;
        //if (dir == SortDirection.Ascending)
        //{
        //    dir = SortDirection.Descending;
        //    sortingDirection = "Desc";
        //}
        //else
        //{
        //    dir = SortDirection.Ascending;
        //    sortingDirection = "Asc";
        //}

        //DataView sortedView = new DataView(BindMovimentoPrelievi());
        //sortedView.Sort = e.SortExpression + " " + sortingDirection;
        //grdData.DataSource = sortedView;
        //grdData.DataBind();
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
            SqlParameter[] oPera = { new SqlParameter("@CodId",CodId) };
            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable("GetMovimentoRichiesteClinicheByCodId", oPera, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ltrID.Text = CodId;
                //btnRegister.Text = "Update Registra";
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
                string testText = dt.Rows[0]["TypePrepared"].ToString();
                try
                {
                    ddlTipoPreparato.Items.FindByText(dt.Rows[0]["TypePrepared"].ToString()).Selected = true;
                }
                catch(NullReferenceException ex)
                {
                    string message = "Tipo sbagliato!";
                    ddlTipoPreparato.Items.Add(message);
                    ddlTipoPreparato.Items.FindByText(message).Selected = true;
                }
                txtComposizione.Text = dt.Rows[0]["CompositionVolumeAnticoagulant"].ToString();
                txtConservazione.Text = dt.Rows[0]["ModeStorageTemp"].ToString();
                txtDataScadenza.Text = Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"]).ToString("dd/MM/yyyy");
                txtGruppoSangue.Text = dt.Rows[0]["AnimalGroupBlood"].ToString();
                txtPesoLordo.Text = dt.Rows[0]["GrossWeightPrepared"].ToString();
                ddlDenominazione.SelectedValue = dt.Rows[0]["CodIDDonors"].ToString();
                ddlSpecieAnimale.SelectedValue = dt.Rows[0]["IndicationAnimalSpecies"].ToString();
                try
                {
                    string ddlDenominazion = "0";
                    if (dt.Rows[0]["CodIDBankRefrigerator"].ToString() != "")
                    {
                        ddlDenominazioneB.SelectedValue = dt.Rows[0]["CodIDBankRefrigerator"].ToString();
                    }
                    else 
                    {
                        ddlDenominazioneB.SelectedValue = ddlDenominazion;
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
        GetSamplingPoints(); GetAnimalSpecies(); GetDonators(); GetCentralBanks(); GetEmobank_Clinics(); GetTipoPreparato();
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        int CodIdDonor = 0;
        string Type;
        //if (btnRegister.Text.Trim() == "Registra")
        //{
        //    Type = "S";
        //    ltrID.Text = "0";
        //}
        //else
        //{
        //    Type = "U";
        //}

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
                    // BindMovimentoPrelievi();
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



        return dt;
    }
    private DataTable GetDonators()
    {
        DataTable dt = cls.ExecuteDataTable("GetDonators", null, CommandType.Text);
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
    private DataTable GetEmobank_Clinics()
    {
        DataTable dt = cls.ExecuteDataTable("GetEmobank_Clinics", null, CommandType.Text);
        ddlclincadeno.DataSource = dt;
        ddlclincadeno.DataTextField = "Description";
        ddlclincadeno.DataValueField = "CodID";
        ddlclincadeno.DataBind();
        ddlclincadeno.Items.Insert(0, new ListItem("--Select Denominazione--", "0"));


        return dt;
    }

    private DataTable BindBloodGroup()
    {
        DataTable dt = cls.ExecuteDataTable("Select * from Emobank_BloodGroup", CommandType.Text);
        return dt;
        //ddlGruppoSangue.DataSource = dt;
        //ddlGruppoSangue.DataTextField = "BloodGroup";
        //ddlGruppoSangue.DataValueField = "BloodGroup";
        //ddlGruppoSangue.DataBind();
        //ddlGruppoSangue.Items.Insert(0, new ListItem("--Gruppo Sangue--", "0"));
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
            txtIndirizzoB.Text = dt.Rows[0]["Address"].ToString();
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
            txtIndirizzo.Text = dt.Rows[0]["Address"].ToString();
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
            txtclincaIndirizzo.Text = dt.Rows[0]["Address"].ToString();
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
            txtDIndirizzo.ReadOnly = false;
            txtDLocalita.ReadOnly = false;
            txtDCap.ReadOnly = false;
            txtDE_mail.ReadOnly = false;
            txtDTelefono.ReadOnly = false;
            txtDProvincia.ReadOnly = false;
            // txtDenominazione.ReadOnly = false;

            // txtDenominazione.Text = dt.Rows[0]["Name"].ToString();
            txtDIndirizzo.Text = dt.Rows[0]["Address"].ToString();
            txtDLocalita.Text = dt.Rows[0]["resort"].ToString();
            txtDCap.Text = dt.Rows[0]["PostalCode"].ToString();
            txtDE_mail.Text = dt.Rows[0]["Email"].ToString();
            txtDTelefono.Text = dt.Rows[0]["Phone"].ToString();
            txtDProvincia.Text = dt.Rows[0]["Provincie"].ToString();
        }
        hdNewDonor.Value = "0";
        // txtDenominazione.ReadOnly = true;
        txtDIndirizzo.ReadOnly = true;
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
        txtDIndirizzo.Text = "";
        txtDLocalita.Text = "";
        txtDCap.Text = "";
        txtDE_mail.Text = "";
        txtDTelefono.Text = "";
        txtDProvincia.Text = "";
        // txtDenominazione.Text = "";

        txtDIndirizzo.ReadOnly = false;
        txtDLocalita.ReadOnly = false;
        txtDCap.ReadOnly = false;
        txtDE_mail.ReadOnly = false;
        txtDTelefono.ReadOnly = false;
        txtDProvincia.ReadOnly = false;
      //  txtDenominazione.ReadOnly = false;
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
        txtIndirizzo.Text = string.Empty;
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
        txtDIndirizzo.Text = "";
        txtDLocalita.Text = "";
        txtDCap.Text = "";
        txtDE_mail.Text = "";
        txtDTelefono.Text = "";
        txtDProvincia.Text = "";
        //txtDenominazione.Text = "";

        txtDIndirizzo.ReadOnly = false;
        txtDLocalita.ReadOnly = false;
        txtDCap.ReadOnly = false;
        txtDE_mail.ReadOnly = false;
        txtDTelefono.ReadOnly = false;
        txtDProvincia.ReadOnly = false;
       // txtDenominazione.ReadOnly = false;
        grdData.DataSource = null;
        grdData.DataBind();
        //ddlSpeciesName1.ClearSelection();
        //ddlAnimalGroupBlood1.ClearSelection();
        //ddlSpeciesName1.DataSource = null;
        //ddlSpeciesName1.DataBind();
        //ddlAnimalGroupBlood1.DataSource = null;
        //ddlAnimalGroupBlood1.DataBind();
    }
    protected void ddlDenominazioneB_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCentralBankById(ddlDenominazioneB.SelectedValue);
    }
    private DataTable BindGridDropdowns()
    {
        DataTable dt = cls.ExecuteDataTable("GetMovimentoRichiesteCliniche", CommandType.StoredProcedure);

        //BindDropdowns(ddlCod1, dt, "CodID", "CodID");


        //BindDropdowns(ddlProtocol1, dt, "ProtocolNumber", "ProtocolNumber");


        //BindDropdowns(ddlDateTimeDrawing1, dt, "DateTimeDrawing", "DateTimeDrawing");


        //BindDropdowns(ddlDescription1, dt, "Name", "Name");



        BindDropdowns(ddlSpeciesName1, dt, "SpeciesName", "IndicationAnimalSpecies");
        //ddlSpeciesName1.DataSource = GetAnimalSpecies();
        //ddlSpeciesName1.DataTextField = "SpeciesName";
        //ddlSpeciesName1.DataValueField = "SpeciesCode";
        //ddlSpeciesName1.DataBind();
        //ddlSpeciesName1.Items.Insert(0, "");
        GetBloodGroup(ddlSpeciesName1.SelectedValue);
        //BindDropdowns(ddlAnimalGroupBlood1, dt, "AnimalGroupBlood", "AnimalGroupBlood");
        //ddlAnimalGroupBlood1.DataSource = BindBloodGroup();
        //ddlAnimalGroupBlood1.DataValueField = "BloodGroup";
        //ddlAnimalGroupBlood1.DataValueField = "BloodGroup";
        //ddlAnimalGroupBlood1.DataBind();
        //ddlAnimalGroupBlood1.Items.Insert(0, "");
        //BindDropdowns(ddlNamee1, dt, "Name", "Name");

        //BindDropdowns(ddldatacarFrigo, dt, "DateLoadBloodBankRefrigerator", "DateLoadBloodBankRefrigerator");
        //BindDropdowns(ddlDataScaFrigo, dt, "ProductExpirationDate", "ProductExpirationDate");
        // BindDropdowns(ddlClinicaVetern, dt, "ClinicName", "ClinicName");
        //BindDropdowns(ddlDataConsegna, dt, "DateReceiptClinic", "DateReceiptClinic");

        return dt;
    }
    private void SortingFunction(string ColumnName)
    {
        //string sortingDirection = string.Empty;
        //if (dir == SortDirection.Ascending)
        //{
        //    dir = SortDirection.Descending;
        //    sortingDirection = "Desc";
        //}
        //else
        //{
        //    dir = SortDirection.Ascending;
        //    sortingDirection = "Asc";
        //}

        //DataView sortedView = new DataView(BindMovimentoPrelievi());
        ////sortedView.Sort = e.SortExpression + " " + sortingDirection;
        //sortedView.Sort = ColumnName + " " + sortingDirection;
        //grdData.DataSource = sortedView;
        //grdData.DataBind();
    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        SortingFunction(lnk.CommandArgument.ToString());
    }

    private void BindDropdowns(DropDownList ddl, DataTable dt, string DataTextField, string DataValueField)
    {
        DataTable uniqueCols1 = dt.DefaultView.ToTable(true, DataTextField, DataValueField);

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
    private DataTable GetBloodGroup(string AnimalCode)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@AnimalCode",AnimalCode)
        };
        DataTable dt = cls.ExecuteDataTable("GetMovimentoRichiesteClinicheBloodGroup",oPera, CommandType.StoredProcedure);


        ddlAnimalGroupBlood1.DataSource = dt;
        ddlAnimalGroupBlood1.DataTextField = "AnimalgroupBlood";
        ddlAnimalGroupBlood1.DataValueField = "AnimalgroupBlood";
        ddlAnimalGroupBlood1.DataBind();
        ddlAnimalGroupBlood1.Items.Insert(0, new ListItem("", "0"));


        return dt;
    }
    public void BindRichiesteCliniche()
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodID","All"),
            new SqlParameter("@ProtocolNumber","All"),
            new SqlParameter("@Description","All"),
            new SqlParameter("@Name","All"),
            new SqlParameter("@SpeciesName",ddlSpeciesName1.SelectedValue),
            new SqlParameter("@DateTimeDrawing","All"),
            new SqlParameter("@AnimalGroupBlood",ddlAnimalGroupBlood1.SelectedValue),
            new SqlParameter("@ClinicId", "0"),
        };
        DataTable dt = cls.ExecuteDataTable("GetRichiesteClinicheFilter", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            grdData.DataSource = dt;
            grdData.DataBind();
        }
        else
        {
            grdData.DataSource = null;
            grdData.DataBind();
        }
    }
    protected void ddlCod_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetBloodGroup(ddlSpeciesName1.SelectedValue);
        BindRichiesteCliniche();
    }
    protected void ddlCod1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRichiesteCliniche();
    }
    protected void ddlClinicaVetern_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClinicaVetern.SelectedIndex <= 0)
        {
            Div.Visible = false;
            ddlSpeciesName1.ClearSelection();
            ddlAnimalGroupBlood1.ClearSelection();
            ddlSpeciesName1.DataSource = null;
            ddlSpeciesName1.DataBind();
            ddlAnimalGroupBlood1.DataSource = null;
            ddlAnimalGroupBlood1.DataBind();
            grdData.DataSource = null;
            grdData.DataBind();
        }
        else
        {
            Div.Visible = true;
            grdData.DataSource = null;
            grdData.DataBind();
            BindGridDropdowns();
        }

    }
    protected void ddlclincadeno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetEmobank_ClinicsById(ddlclincadeno.SelectedValue);
    }


    private void BindClinicaVetern()
    {
        DataTable dt = cls.ExecuteDataTable("GetClinics", CommandType.StoredProcedure);
        ddlClinicaVetern.DataSource = dt;
        ddlClinicaVetern.DataTextField = "Description";
        ddlClinicaVetern.DataValueField = "CodId";
        ddlClinicaVetern.DataBind();
        ddlClinicaVetern.Items.Insert(0, new ListItem("", "0"));
    }

    public void BindAnimals(string DCodID, string AnimalSpecies, string Progressive, string ProtocolNumber)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@DCodId",DCodID),
            new SqlParameter("@AnimalSpecies",AnimalSpecies),
            new SqlParameter("@Progressive", Progressive),
            new SqlParameter("@ProtocolNumber", ProtocolNumber)
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
    protected void lnkYes_Click(object sender, EventArgs e)
    {
        Button lnkButton = sender as Button;
        string ltrCodId = lnkButton.CommandArgument;
        SqlParameter[] Opare =
            {
             //new SqlParameter("@ProtocolNumber",txtProtocollo.Text),
             //new SqlParameter("@Progressive",txtProgressivo.Text),
             new SqlParameter("@CodId",ltrCodId),
             new SqlParameter("@DateAcquisitionRequest",Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy")),
             new SqlParameter("@Userdrainbloodbankrefrigerator",Convert.ToString(Session["NameUser"])),
             new SqlParameter("@Datedrainbloodbankrefrigerator",Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy")),
             new SqlParameter("@DatereceiptClinic",Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy")),
              new SqlParameter("@CodeIDReqVeterinaryClinic",ddlClinicaVetern.SelectedValue),
             new SqlParameter("@Rval",SqlDbType.Int)
              };
        Opare[6].Direction = ParameterDirection.ReturnValue;
        int rval = cls.ExecuteNonQuery("UpdateMovementsRichiesteCliniche", Opare, CommandType.StoredProcedure);
        //int rval = 1;
        if (rval > 0)
        {
            BindRichiesteCliniche();
            string url = "GenrateLabel.aspx?CodId=" + ltrCodId;
            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                     "click", @"<script>window.open('" + url + "','_newtab');</script>", false);
        }

    }

}