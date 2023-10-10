using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using KeepAutomation.Barcode.Bean;
using OnBarcode.Barcode.ASPNET;
using OnBarcode.Barcode;
using System.Drawing.Imaging;

public partial class barcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BarCode code128 = new BarCode();
        code128.Symbology = KeepAutomation.Barcode.Symbology.Code128A;
        
        code128.CodeToEncode = "AB1234567890";
        code128.generateBarcodeToImageFile(Server.MapPath("/BARQR/BarImg/code1283.png"));

        GenerateBacode("AB1234567890", Server.MapPath("/BARQR/BarImg/code1283888BarCode.png"));
        GenerateQrcode("AB1234567890", Server.MapPath("/BARQR/BarImg/code1283888.png"));
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
}
