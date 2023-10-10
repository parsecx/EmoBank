﻿using DAL;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class PrintLabel : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        string CodId = Convert.ToString(HttpContext.Current.Request.QueryString["CodId"]);
        if (!string.IsNullOrEmpty(CodId))
        {
            BindDetailForPrintLabel(CodId);
            PrintWebControl(pnl, "");
        }
        //BindDetailForPrintLabel("1");
    }
    private void BindDetailForPrintLabel(string CodId)
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CodId)
        };
        DataTable dt = new DataTable();
        dt = cls.ExecuteDataTable("GetDataLabelPrinting", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            lblComposizione.Text = dt.Rows[0]["CompositionVolumeAnticoagulant"].ToString();
            lblConservazione.Text = dt.Rows[0]["ModeStorageTemp"].ToString();
            lblDataPrelievo.Text = dt.Rows[0]["DateTimeDrawing"].ToString();
            lblDataScadenza.Text = dt.Rows[0]["ProductExpirationDate"].ToString();
            lblGruppo.Text = dt.Rows[0]["AnimalGroupBlood"].ToString();
            lblPesoLordo.Text = dt.Rows[0]["GrossWeightPrepared"].ToString();
            lblProtocolNo.Text = dt.Rows[0]["ProtocolNumber"].ToString();
            lblSpecie.Text = dt.Rows[0]["SpeciesName"].ToString();
            lblTipoPreparato.Text = dt.Rows[0]["TypePrepared"].ToString();
            
            lblSpecie1.Text = dt.Rows[0]["SpeciesName"].ToString();
            lblNote.Text = dt.Rows[0]["Note"].ToString();

            string LabelText = CodId + "." + dt.Rows[0]["ProtocolNumber"].ToString() + "." + Convert.ToDateTime(dt.Rows[0]["DateTimeDrawing"].ToString()).ToString("ddMMyyyy") + "." + Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"].ToString()).ToString("ddMMyyyy");

            GenerateBacode(LabelText, Server.MapPath("/BARQR/BarImg/Bar" + CodId + ".png"));
            GenerateQrcode(LabelText, Server.MapPath("/BARQR/BarImg/QR" + CodId + ".png"));
            //imgBarCode.ImageUrl = ("/BARQR/BarImg/Bar" + CodId + ".png");
            //imgQrCode.ImageUrl = ("/BARQR/BarImg/Qr" + CodId + ".png");
        }
    }
    private void GenerateBacode(string _data, string _filename)
    {
        Linear barcode = new Linear();
        barcode.Type = BarcodeType.CODE128A;
        barcode.Data = _data;
        barcode.LeftMargin = 0;
        barcode.drawBarcode(_filename);
    }
    private void GenerateQrcode(string _data, string _filename)
    {
        QRCode qrcode = new QRCode();
        qrcode.Data = _data;
        qrcode.DataMode = QRCodeDataMode.Byte;
        qrcode.UOM = UnitOfMeasure.PIXEL;
        qrcode.X = 6;

        qrcode.LeftMargin = 0;
        qrcode.RightMargin = 0;
        qrcode.TopMargin = 0;
        qrcode.BottomMargin = 0;
        qrcode.Resolution = 230;
        qrcode.Rotate = Rotate.Rotate0;
        qrcode.ImageFormat = ImageFormat.Gif;
        qrcode.drawBarcode(_filename);
    }

    public void PrintWebControl(Control ctrl, string Script)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ctrl is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        if (Script != string.Empty)
        {
            pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
        }
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ctrl);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }
}