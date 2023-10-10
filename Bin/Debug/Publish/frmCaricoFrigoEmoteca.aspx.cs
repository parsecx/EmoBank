using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmCaricoFrigoEmoteca : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {if (Session["UserId"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
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
        DataTable dt = cls.ExecuteDataTable("GetCaricoFrigoEmoteca", para, CommandType.StoredProcedure);
        BindGridDropdowns(dt);
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
        ImportExport.ExportToPDF(grdData, "Carico Frigo Emoteca", "", a, "A4", true);
    }
    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        HideShowDiv(1); BindDropDowns();

    }
    //row binding 
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gv = grdData.SelectedRow;
        Label lbl = (Label)gv.FindControl("lblCodID");
        string CodInt = gv.Cells[1].Text;
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
        if (e.Row.RowType == DataControlRowType.Header)
        {

            //DataTable dt = BindGridDropdowns();

            //DropDownList ddlCod = (DropDownList)e.Row.FindControl("ddlCod");
            //BindDropdowns(ddlCod, dt, "CodID", "CodID");

            //DropDownList ddlProtocol = (DropDownList)e.Row.FindControl("ddlProtocol");
            //BindDropdowns(ddlProtocol, dt, "ProtocolNumber", "ProtocolNumber");

            //DropDownList ddlDateTimeDrawing = (DropDownList)e.Row.FindControl("ddlDateTimeDrawing");
            //BindDropdowns(ddlDateTimeDrawing, dt, "DateTimeDrawing", "DateTimeDrawing");

            //DropDownList ddlDescription = (DropDownList)e.Row.FindControl("ddlDescription");
            //BindDropdowns(ddlDescription, dt, "Description", "Description");


            //DropDownList ddlSpeciesName = (DropDownList)e.Row.FindControl("ddlSpeciesName");
            //BindDropdowns(ddlSpeciesName, dt, "SpeciesName", "SpeciesName");

            //DropDownList ddlAnimalGroupBlood = (DropDownList)e.Row.FindControl("ddlAnimalGroupBlood");
            //BindDropdowns(ddlAnimalGroupBlood, dt, "AnimalGroupBlood", "AnimalGroupBlood");

            //DropDownList ddlNamee = (DropDownList)e.Row.FindControl("ddlNamee");
            //BindDropdowns(ddlNamee, dt, "Name", "Name");



        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdDateLoadBloodBankRefrigerator = (HiddenField)e.Row.FindControl("hdDateLoadBloodBankRefrigerator");
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdData, "Select$" + e.Row.RowIndex);
                e.Row.Cells[i].ToolTip = "Clicca per entrare dettaglio.";
                if (string.IsNullOrEmpty(hdDateLoadBloodBankRefrigerator.Value))
                {
                    e.Row.Cells[i].Attributes.Add("style", "cursor:pointer;color:blue");
                }
                else
                {
                    e.Row.Cells[i].Attributes.Add("style", "cursor:pointer");
                }
            }
            if (string.IsNullOrEmpty(hdDateLoadBloodBankRefrigerator.Value))
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#f96868");

            }
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

    private void BindMovimentoPrelieviByCodId(string CodId)
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
            try
            {
                ddlTipoPreparato.Items.FindByText(dt.Rows[0]["TypePrepared"].ToString()).Selected = true;
            }
            catch (Exception)
            {
                //ddlTipoPreparato.SelectedIndex = 0; Concentrato eritrocitario

            }

            txtComposizione.Text = dt.Rows[0]["CompositionVolumeAnticoagulant"].ToString();
            txtConservazione.Text = dt.Rows[0]["ModeStorageTemp"].ToString();
            txtDataScadenza.Text = Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"]).ToString("dd/MM/yyyy");

            txtGruppoSangue.Text = dt.Rows[0]["AnimalGroupBlood"].ToString();
            txtPesoLordo.Text = dt.Rows[0]["GrossWeightPrepared"].ToString();


            ddlDenominazione.SelectedValue = dt.Rows[0]["CodIDDonors"].ToString();

            ddlSpecieAnimale.SelectedValue = dt.Rows[0]["IndicationAnimalSpecies"].ToString();

            string ddlclincaden = "0";
            if (dt.Rows[0]["CodIDBankRefrigerator"].ToString() != "")
            {
                ddlDenominazioneB.SelectedValue = dt.Rows[0]["CodIDBankRefrigerator"].ToString();
            }
            else
            {
                ddlDenominazioneB.SelectedValue = ddlclincaden;
            }


            GetSamplingPointById(ddlPuntoPrelievo.SelectedValue);
            GetDonatorsById(ddlDenominazione.SelectedValue);
            BindCentralBankById(ddlDenominazioneB.SelectedValue);
        }
    }
    #endregion

    #region Add New Record
    private void BindDropDowns()
    {
        GetSamplingPoints(); GetAnimalSpecies(); GetDonators(); GetCentralBanks(); GetTipoPreparato();
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
             new SqlParameter("@UserLoadName",txtUtente.Text),
             new SqlParameter("@DateLoad",Convert.ToDateTime(txtData.Text).ToString("MM/dd/yyyy")),
             new SqlParameter("@BankId",ddlDenominazioneB.SelectedValue),
             new SqlParameter("@Rval",SqlDbType.Int)
              };
            Opare[4].Direction = ParameterDirection.ReturnValue;
            int rval = cls.ExecuteNonQuery("Update_Drawing_movements", Opare, CommandType.StoredProcedure);
            if (rval > 0)
            {
                rval = Convert.ToInt32(Opare[4].Value);
                if (rval > 0)
                {
                    CodIdDonor = rval;
                  
                    BindMovimentoPrelievi(ddlSeleziona.SelectedValue);
                    HideShowDiv(0);
                    lblmsg.InnerHtml = Common.ShowMessage("Record aggiornato con successo.", 1);
                    ClearInput();
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
        ClearInput();
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
        else
        {
            txtCapB.Text = "";
            txtIndirzzoB.Text = "";
            txtLocalitaB.Text = "";
            txtProvinciaB.Text = "";
            
           // txtData.Text = "";
          
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
           // txtDenominazione.ReadOnly = false;

           // txtDenominazione.Text = dt.Rows[0]["Name"].ToString();
            txtDIndirzzo.Text = dt.Rows[0]["Address"].ToString();
            txtDLocalita.Text = dt.Rows[0]["resort"].ToString();
            txtDCap.Text = dt.Rows[0]["PostalCode"].ToString();
            txtDE_mail.Text = dt.Rows[0]["Email"].ToString();
            txtDTelefono.Text = dt.Rows[0]["Phone"].ToString();
            txtDProvincia.Text = dt.Rows[0]["Provincie"].ToString();
        }
        hdNewDonor.Value = "0";
       // txtDenominazione.ReadOnly = true;
        txtDIndirzzo.ReadOnly = true;
        txtDLocalita.ReadOnly = true;
        txtDCap.ReadOnly = true;
        txtDE_mail.ReadOnly = true;
        txtDTelefono.ReadOnly = true;
        txtDProvincia.ReadOnly = true;
        //txtDenominazione.ReadOnly = true;
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
    private DataTable BindGridDropdowns(DataTable dt)
    {
        //SqlParameter[] para =
        //{
        //   new SqlParameter("@Type",ddlSeleziona.SelectedValue)

        //};
        //DataTable dt = cls.ExecuteDataTable("GetCaricoFrigoEmoteca", para, CommandType.StoredProcedure);
       
        BindDropdowns(ddlCod1, dt, "CodID", "CodID");

        
        BindDropdowns(ddlProtocol1, dt, "ProtocolNumber", "ProtocolNumber");

        
        BindDropdowns(ddlDateTimeDrawing1, dt, "DateTimeDrawing", "DateTimeDrawing");

        
        BindDropdowns(ddlDescription1, dt, "Description", "Description");


       
        BindDropdowns(ddlSpeciesName1, dt, "SpeciesName", "SpeciesName");

       
        BindDropdowns(ddlAnimalGroupBlood1, dt, "AnimalGroupBlood", "AnimalGroupBlood");

       
        BindDropdowns(ddlNamee1, dt, "Name", "Name");
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
            new SqlParameter("@Type",ddlSeleziona.SelectedValue)
        };
        DataTable dt = cls.ExecuteDataTable("GetCaricoFrigoEmotecaFilter", oPera, CommandType.StoredProcedure);
        grdData.DataSource = dt;
        grdData.DataBind();
    }
    protected void ddlSeleziona_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMovimentoPrelievi(ddlSeleziona.SelectedValue);
    }




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
}