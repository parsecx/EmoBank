<%@ WebHandler Language="C#" Class="PrintLabel" %>

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;
using System;
using System.Data.SqlClient;
using DAL;

public class PrintLabel : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string CodId = Convert.ToString(HttpContext.Current.Request.QueryString["CodId"]);
        if (!string.IsNullOrEmpty(CodId))
        {
            LabelPrint(CodId);
        }
        LabelPrint("2");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    private void GenerateBacode(string _data, string _filename)
    {
        OnBarcode.Barcode.Linear barcode = new OnBarcode.Barcode.Linear();
        barcode.Type = OnBarcode.Barcode.BarcodeType.CODE128A;
        barcode.Data = _data;
        barcode.LeftMargin = 0;
        barcode.drawBarcode(_filename);
    }
    public void LabelPrint(string CodId)
    {
        SqlHelper cls = new SqlHelper();
        var strBldr = new System.Text.StringBuilder();

        strBldr.Append("<html><body>");

        SqlParameter[] oPera =
        {
            new SqlParameter("@CodId",CodId)
        };
        DataTable dt = new DataTable();
        dt = cls.ExecuteDataTable("GetDataLabelPrinting", oPera, CommandType.StoredProcedure);
        if (dt.Rows.Count > 0)
        {
            string LabelText = CodId + "." + dt.Rows[0]["ProtocolNumber"].ToString() + "." + Convert.ToDateTime(dt.Rows[0]["DateTimeDrawing"].ToString()).ToString("ddMMyyyy") + "." + Convert.ToDateTime(dt.Rows[0]["ProductExpirationDate"].ToString()).ToString("ddMMyyyy");
            GenerateBacode(LabelText, HttpContext.Current.Server.MapPath("/BARQR/BarImg/Bar" + CodId + ".png"));

            string BarImage = "~\\BARQR\\BarImg\\Bar" + CodId + ".png";
            
            
            
            strBldr.Append("<table style='display:block;overflow:hidden;width:388px; height:388px; border: solid 1px #000; font-size:small;float:left' border='1'> ");
            strBldr.Append("<tr>");
            strBldr.Append("<td colspan='3'  style='height:40px'>");
            strBldr.Append("<img src='" + HttpContext.Current.Server.MapPath("~\\images\\logo.png") + "' width='160px' />");
            strBldr.Append("</td>                                                 ");
            strBldr.Append(" </tr>                                                           ");
            strBldr.Append("<tr>                                                     ");
            strBldr.Append("<td style='width: 185px; height: 40px; vertical-align: sub;'>Protocollo Prelievo<br />                  ");
            strBldr.Append("" + dt.Rows[0]["ProtocolNumber"].ToString() + "</td>       ");
            strBldr.Append(" <td style='width:100px'>Data Prelievo<br />                                                                   ");
            strBldr.Append("" + dt.Rows[0]["DateTimeDrawing"].ToString() + "</td>     ");
            strBldr.Append("<td style='width:100px'>Data Scadenza<br />                                                                   ");
            strBldr.Append("" + dt.Rows[0]["ProductExpirationDate"].ToString() + "</td>     ");
            strBldr.Append(" </tr>");
            strBldr.Append(" <tr>");
            strBldr.Append("<td style='width: 185px; height: 40px; vertical-align: sub;'>Tipo Preparato<br /> ");
            strBldr.Append("" + dt.Rows[0]["TypePrepared"].ToString() + "</td>    ");
            strBldr.Append("<td style='width:100px'>Specie Animale<br /> ");
            strBldr.Append("" + dt.Rows[0]["SpeciesName"].ToString() + "</td>           ");
            strBldr.Append("<td style='width:100px'>Gruppo Sanguigno<br />");
            strBldr.Append("" + dt.Rows[0]["AnimalGroupBlood"].ToString() + "</td>           ");
            strBldr.Append("</tr>");
            strBldr.Append("<tr>");
            strBldr.Append(" <td colspan='2' style='height:40px'>Note<br /> " + dt.Rows[0]["Note"].ToString() + "");
            strBldr.Append("</td>");
            strBldr.Append("<td>Peso Lordo<br />");
            strBldr.Append(" " + dt.Rows[0]["GrossWeightPrepared"].ToString() + "</td>        ");
            strBldr.Append(" </tr>");
            strBldr.Append("<tr>");
            strBldr.Append("<td colspan='3'>Composizione<br />");
            strBldr.Append("" + dt.Rows[0]["CompositionVolumeAnticoagulant"].ToString() + "           ");
            strBldr.Append("</td>");
            strBldr.Append("</tr>");

            strBldr.Append("<tr>");
            strBldr.Append(" <td colspan='3'>Conservazione<br />");
            strBldr.Append(" " + dt.Rows[0]["ModeStorageTemp"].ToString() + "</td>     ");
            strBldr.Append("</tr>");

            strBldr.Append("  <tr> ");
            strBldr.Append("  <td colspan='3' style='font-size: 6px; font-family: tahoma; padding: 4px 2px;font-weight:normal'>Esclusivamente per uso Veterinario ( Specie Destinazione ");
            strBldr.Append("<span style='Color=Red'>" + dt.Rows[0]["SpeciesName"].ToString() + "</span>");
            strBldr.Append("                    ), non utilizzabile a scopo trasfusionale se presenta emolisi                          ");
            strBldr.Append("o altre anomalie evidenti. Per la trasfusione utilizzare un dato dispositivo munito appropriato filtro.    ");
            strBldr.Append("                </td>                                                                                      ");
            strBldr.Append("            </tr> ");
            
            
             strBldr.Append(" <tr>                                                                                  ");
             strBldr.Append("     <td colspan='3' style='font-size:6px;text-align:center;height:72px'><table border='0'><tr><td  style='width:43px'>");
             strBldr.Append("         <span style='width: 43px; display:block; float:left;margin-top: 15px; font-family: tahoma;'>        ");
             strBldr.Append("             <img src='" + HttpContext.Current.Server.MapPath("~\\images\\1_bold.png") + "' width='43px' />                              ");
             strBldr.Append("             <br />                                                                    ");
             strBldr.Append("             Sterilizzata per Irradiazione                                             ");
             strBldr.Append("         </span>   </td><td>                                                                    ");
             strBldr.Append("         <span style='float:left;margin-top: 15px; font-family: tahoma;'>        ");
             strBldr.Append("             <img src='" + HttpContext.Current.Server.MapPath("~\\images\\2_bold.png") + "' width='15px' />                               ");
             strBldr.Append("             <br />                                                                    ");
             strBldr.Append("             Fluido Libero da Batteri  </span>  </td><td style='width:43px'>                                  ");
             strBldr.Append("         <span style='float:left;margin-top: 15px; font-family: tahoma;'>        ");
             strBldr.Append("             <img src='" + HttpContext.Current.Server.MapPath("~\\images\\3_bold.png") + "' width='17px' />                               ");
             strBldr.Append("             <br />                                                                    ");
             strBldr.Append("             Monouso  </span>  </td><td style='width:43px'>                                  ");
             strBldr.Append("         <span style='width: 12%; display:block; float:left;margin-top: 15px; font-family: tahoma;'>        ");
             strBldr.Append("             <img src='" + HttpContext.Current.Server.MapPath("~\\images\\4_bold.png") + "' width='15px' />                               ");
             strBldr.Append("             <br />                                                                    ");
             strBldr.Append("             Leggere Istruzioni d’uso  </span>  </td><td style='width:43px'>                                  ");
             strBldr.Append("         <span style='width: 12%; display:block; float:left;margin-top: 15px; font-family: tahoma;'>        ");
             strBldr.Append("             <img src='" + HttpContext.Current.Server.MapPath("~\\images\\5_bold.png") + "' width='15px' />                               ");
             strBldr.Append("             <br />                                                                    ");
             strBldr.Append("             Non Utilizzare se Danneggiata  </span>  </td><td style='width:43px'>    ");
             strBldr.Append("         <span style='width: 12%; display:block; float:left;margin-top: 15px; font-family: tahoma;'>        ");
             strBldr.Append("             <img src='" + HttpContext.Current.Server.MapPath("~\\images\\6_bold.png") + "' width='15px' />                               ");
             strBldr.Append("             <br />                                                                    ");
             strBldr.Append("             Non Ventilare  </span>  </td><td>                                  ");
             strBldr.Append("         <span style='width: 50px; display:block; float:left;'>                         ");
             strBldr.Append("             <img src='" + HttpContext.Current.Server.MapPath("~\\images\\7_bold.png") + "' width='15px' />                               ");
             strBldr.Append("             <br />    <br />                                                                    ");
            
             strBldr.Append("             <img src='" + HttpContext.Current.Server.MapPath(BarImage) + "' height='60px'  />  </span>");
             strBldr.Append("</td></tr></table>     </td>                                                                             ");
             strBldr.Append("                                                                                       ");
             strBldr.Append(" </tr>                                                                                 ");
                                                                                                                  
            
            strBldr.Append("        </table>                                                                                           ");
           
        }



        strBldr.Append("</body></html>");
        string htmlText = strBldr.ToString();

        htmlText = htmlText.Replace("ï»¿", "");
        var msOutput = new MemoryStream();
        TextReader reader = new StringReader(htmlText);

        // step 1: creation of a document-object
        var document = new Document(PageSize.A4, 10, 320, 5, 5);
        // step 2:
        // we create a writer that listens to the document

        // and directs a XML-stream to a file
        PdfWriter.GetInstance(document, msOutput);

        // step 3: we create a worker parse the document

        // PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath("FormDesign/formdesign.pdf"), FileMode.Create));

        // step 4: we open document and start the worker on the document

        document.Open();

        var worker = new HTMLWorker(document);
        //Image image = Image.GetInstance(HttpContext.Current.Server.MapPath("~\\images\\logo.png"));

        //image.Alignment = Image.UNDERLYING;
        //image.SetAbsolutePosition(60f, 270f);
        //document.Add(image);
        var code128 = new Barcode128
        {
            CodeType = Barcode.CODE128,
            ChecksumText = true,
            GenerateChecksum = true,
            Code = "A100"
        };
        var bm = new Bitmap(code128.CreateDrawingImage(Color.Black, Color.White));
        Image barImage = Image.GetInstance(bm, ImageFormat.Jpeg);
        barImage.Alignment = Element.ALIGN_TOP | Element.ALIGN_RIGHT;
        barImage.SetAbsolutePosition(500, 590);
        //  document.Add(barImage);
        worker.StartDocument();
        worker.Parse(reader);
        worker.EndDocument();
        worker.Close();

        document.Close();


        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + "a" + ".pdf");
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.OutputStream.Write(msOutput.GetBuffer(), 0, msOutput.GetBuffer().Length);
        HttpContext.Current.Response.OutputStream.Flush();
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.End();

    }



}