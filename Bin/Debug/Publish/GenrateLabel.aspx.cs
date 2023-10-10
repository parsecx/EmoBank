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
using System.Drawing;

public partial class GenrateLabel : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string CodId = Convert.ToString(HttpContext.Current.Request.QueryString["CodId"]);
            if (!string.IsNullOrEmpty(CodId))
            {
                BindDetailForPrintLabel(CodId);
                //PrintWebControl(pnl, "");
            }
        }
        //BindDetailForPrintLabel("1");
        //PrintWebControl(pnl, "");
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
            var value = dt.Rows[0]["GrossWeightPrepared"].ToString();
            lblPesoLordo.Text = value.Remove(value.Length - 1, 1);
            lblProtocolNo.Text = dt.Rows[0]["ProtocolNumber"].ToString();
            lblSpecie.Text = dt.Rows[0]["SpeciesName"].ToString();
            lblTipoPreparato.Text = dt.Rows[0]["TypePrepared"].ToString();

            lblSpecie1.Text = dt.Rows[0]["SpeciesName"].ToString();
            lblNote.Text = dt.Rows[0]["Note"].ToString();

            sp1.Text = dt.Rows[0]["Description"].ToString();
            sp2.Text = dt.Rows[0]["Address"].ToString();
            sp3.Text = dt.Rows[0]["resort"].ToString();
            sp4.Text = dt.Rows[0]["PostalCode"].ToString();
            sp5.Text = dt.Rows[0]["province"].ToString();


            //string LabelText = CodId + "." + dt.Rows[0]["ProtocolNumber"].ToString() + "." + Convert.ToDateTime(dt.Rows[0]["DateTimeDrawing"].ToString()).ToString("ddMMyyyy") + "." + Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"].ToString()).ToString("ddMMyyyy");
            string LabelText = dt.Rows[0]["ProtocolNumber"].ToString() + "" + Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"].ToString()).ToString("ddMMyy");
            //lblBarCode.Text = LabelText;
            CreateBarCode(LabelText, Server.MapPath("BARQR/BarImg/Bar" + CodId + ".png"));


            //imgBarCode.ImageUrl = ("/BARQR/BarImg/Bar" + CodId + ".png");
            imgBarCode.Attributes.Add("src", HttpContext.Current.Server.MapPath("\\BARQR\\BarImg\\Bar" + CodId + ".png"));
            //imgQrCode.ImageUrl = ("/BARQR/BarImg/Qr" + CodId + ".png");
            //imgQrCode.Height = 500;
            PrintPdf();
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
    protected void btnStampa_Click(object sender, EventArgs e)
    {

        PrintPdf();
        //PrintWebControl(pnl, "");
    }
    public string CreateBarCode(string Text, string Path)
    {
        System.Drawing.Text.PrivateFontCollection privateFonts = new System.Drawing.Text.PrivateFontCollection();
        privateFonts.AddFontFile(HttpContext.Current.Server.MapPath("font-awesome/fonts/IDAutomationHC39M.ttf"));
        System.Drawing.Font oFont = new System.Drawing.Font(privateFonts.Families[0], 16);

        string barCode = Text;
        //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        using (Bitmap bitMap = new Bitmap(barCode.Length * 25, 80))
        {
            using (Graphics graphics = Graphics.FromImage(bitMap))
            {
                //System.Drawing.Font oFont = new System.Drawing.Font("IDAutomationHC39M", 16);
                PointF point = new PointF(1f, 1f);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                Convert.ToBase64String(byteImage);
                System.IO.File.WriteAllBytes(Path, byteImage);
                return "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            // plBarCode.Controls.Add(imgBarCode);
        }
    }
    public void PrintPdf()
    {
        img.Attributes.Add("src", HttpContext.Current.Server.MapPath("images\\logo.png"));
        img1.Attributes.Add("src", HttpContext.Current.Server.MapPath("images\\Icon_Blood.png"));
        img7.Attributes.Add("src", HttpContext.Current.Server.MapPath("images\\7_bold.png"));
        string CodId = Convert.ToString(HttpContext.Current.Request.QueryString["CodId"]);

       

        if (!string.IsNullOrEmpty(CodId))
        {
            // CreateBarCode(lblBarCode.Text);
            imgBarCode.Attributes.Add("src", HttpContext.Current.Server.MapPath("BARQR\\BarImg\\Bar" + CodId + ".png"));

        }
        //imgBarCode.Height = 500;
        //lblBarCode.Attributes.Add("style", "font-family:IDAutomationHC39M");
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