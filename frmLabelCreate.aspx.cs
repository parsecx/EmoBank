using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Data;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class frmLabelCreate : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        string CodId = Convert.ToString(HttpContext.Current.Request.QueryString["CodIds"]);
        if (!string.IsNullOrEmpty(CodId))
        {
            BindDetailForPrintLabel(CodId);
            //PrintWebControl(pnl, "");
        }

        //PrintWebControl(pnl, "");
    }
    private void BindDetailForPrintLabel(string CodId)
    {
        DataTable dtt = new DataTable();
        string[] CodIds = CodId.Split(',');
        for (int i = 0; i < CodIds.Count(); i++)
        {
            SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CodIds[i].ToString())
        };
            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable("GetDataLabelPrinting", oPera, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                if (dtt.Columns.Count == 0)
                {
                    dtt = dt.Clone();
                }
                dtt.ImportRow(dt.Rows[0]);
                //lblComposizione.Text = dt.Rows[0]["CompositionVolumeAnticoagulant"].ToString();
                //lblConservazione.Text = dt.Rows[0]["ModeStorageTemp"].ToString();
                //lblDataPrelievo.Text = dt.Rows[0]["DateTimeDrawing"].ToString();
                //lblDataScadenza.Text = dt.Rows[0]["ProductExpirationDate"].ToString();
                //lblGruppo.Text = dt.Rows[0]["AnimalGroupBlood"].ToString();
                //lblPesoLordo.Text = dt.Rows[0]["GrossWeightPrepared"].ToString();
                //lblProtocolNo.Text = dt.Rows[0]["ProtocolNumber"].ToString();
                //lblSpecie.Text = dt.Rows[0]["SpeciesName"].ToString();
                //lblTipoPreparato.Text = dt.Rows[0]["TypePrepared"].ToString();

                //lblSpecie1.Text = dt.Rows[0]["SpeciesName"].ToString();
                //lblNote.Text = dt.Rows[0]["Note"].ToString();

                //sp1.Text = dt.Rows[0]["Description"].ToString();
                //sp2.Text = dt.Rows[0]["Address"].ToString();
                //sp3.Text = dt.Rows[0]["resort"].ToString();
                //sp4.Text = dt.Rows[0]["PostalCode"].ToString();
                //sp5.Text = dt.Rows[0]["province"].ToString();


                string LabelText = CodId + "." + dt.Rows[0]["ProtocolNumber"].ToString() + "." + Convert.ToDateTime(dt.Rows[0]["DateTimeDrawing"].ToString()).ToString("ddMMyyyy") + "." + Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"].ToString()).ToString("ddMMyyyy");

                GenerateBacode(LabelText, Server.MapPath("/BARQR/BarImg/Bar" + CodIds[i] + ".png"));
                GenerateQrcode(LabelText, Server.MapPath("/BARQR/BarImg/QR" + CodIds[i] + ".png"));
                //imgBarCode.ImageUrl = ("/BARQR/BarImg/Bar" + CodId + ".png");
                //imgBarCode.Attributes.Add("src", HttpContext.Current.Server.MapPath("\\BARQR\\BarImg\\Bar" + CodId + ".png"));
                //imgQrCode.ImageUrl = ("/BARQR/BarImg/Qr" + CodId + ".png");
                //imgQrCode.Height = 500;

            }
        }
        rpt.DataSource = dtt;
        rpt.DataBind();
        PrintPdf();
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


    protected void btnStampa_Click(object sender, EventArgs e)
    {

        PrintPdf();
        //PrintWebControl(pnl, "");
    }
    public void PrintPdf()
    {
        foreach (RepeaterItem item in rpt.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.HtmlControls.HtmlImage img = item.FindControl("img") as System.Web.UI.HtmlControls.HtmlImage;
                System.Web.UI.HtmlControls.HtmlImage img1 = item.FindControl("img1") as System.Web.UI.HtmlControls.HtmlImage;
                System.Web.UI.HtmlControls.HtmlImage img2 = item.FindControl("img2") as System.Web.UI.HtmlControls.HtmlImage;
                System.Web.UI.HtmlControls.HtmlImage img3 = item.FindControl("img3") as System.Web.UI.HtmlControls.HtmlImage;
                System.Web.UI.HtmlControls.HtmlImage img4 = item.FindControl("img4") as System.Web.UI.HtmlControls.HtmlImage;
                System.Web.UI.HtmlControls.HtmlImage img5 = item.FindControl("img5") as System.Web.UI.HtmlControls.HtmlImage;
                System.Web.UI.HtmlControls.HtmlImage img6 = item.FindControl("img6") as System.Web.UI.HtmlControls.HtmlImage;
                System.Web.UI.HtmlControls.HtmlImage img7 = item.FindControl("img7") as System.Web.UI.HtmlControls.HtmlImage;
                System.Web.UI.HtmlControls.HtmlImage imgBarCode = item.FindControl("imgBarCode") as System.Web.UI.HtmlControls.HtmlImage;
                // HiddenField hdCodId = (HiddenField)item.FindControl("hdCodId");
                Label hdCodId = item.FindControl("hdCodId") as Label;
                img.Attributes.Add("src", HttpContext.Current.Server.MapPath("~\\images\\logo.png"));
                img1.Attributes.Add("src", HttpContext.Current.Server.MapPath("~\\images\\1_bold.png"));
                img2.Attributes.Add("src", HttpContext.Current.Server.MapPath("~\\images\\2_bold.png"));
                img3.Attributes.Add("src", HttpContext.Current.Server.MapPath("~\\images\\3_bold.png"));
                img4.Attributes.Add("src", HttpContext.Current.Server.MapPath("~\\images\\4_bold.png"));
                img5.Attributes.Add("src", HttpContext.Current.Server.MapPath("~\\images\\5_bold.png"));
                img6.Attributes.Add("src", HttpContext.Current.Server.MapPath("~\\images\\6_bold.png"));
                img7.Attributes.Add("src", HttpContext.Current.Server.MapPath("~\\images\\7_bold.png"));

                if (!string.IsNullOrEmpty(hdCodId.Text))
                {
                    imgBarCode.Attributes.Add("src", HttpContext.Current.Server.MapPath("\\BARQR\\BarImg\\Bar" + hdCodId.Text + ".png"));
                }
                else
                {
                    imgBarCode.Attributes.Add("src", HttpContext.Current.Server.MapPath("\\BARQR\\BarImg\\Bar" + "1" + ".png"));
                }
                imgBarCode.Height = 500;
            }
        }

        // instantiate the html to pdf converter 
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.Clear();


        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnl.RenderControl(hw);

        StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(new Rectangle(283, 283), 5, 5, 5, 3);

        //Document pdfDoc = new Document();

        Document pdfDoc = new Document(PageSize.A4, 20, 290, 15, 0);


        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        //Response.Write(pdfDoc);


        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "inline;filename=" + DateTime.Now.Ticks.ToString() + ".pdf");
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.End();




    }
    public void PrintPdfWebControl(Control ctrl, string Script)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        StringReader sr = new StringReader(stringWrite.ToString());
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


        Document pdfDoc = new Document(PageSize.A4, 20, 300, 25, 0);

        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        //pdfDoc.Close();
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "inline;filename=" + DateTime.Now.Ticks.ToString() + ".pdf");
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.End();
    }
}