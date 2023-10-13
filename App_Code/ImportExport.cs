using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI.HtmlControls;
using Image = iTextSharp.text.Image;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;

using System.Collections.Generic;

/// <summary>
/// Summary description for ImportExport
/// </summary>
public class ImportExport : IPdfPageEvent
{
    public static StringBuilder strHeder;
    public static StringBuilder strFilter;
    public static StringBuilder strEndFilter;
    public static StringBuilder strFooter;
    public static string strPaperType;
    public ImportExport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private StyleSheet GenerateStylesForPdfTable()
    {
        StyleSheet styles = new StyleSheet();
        styles.LoadTagStyle("th", "border", "1");
        styles.LoadTagStyle("th", "padding", "8px");
        styles.LoadTagStyle("th", "background-color", "#f2f2f2");
        styles.LoadTagStyle("td", "border", "1");
        styles.LoadTagStyle("td", "padding", "8px");
        styles.LoadTagStyle("td", "width", "50px"); 
        return styles;
    }

    public static void PrepareForExport(Control ctrl)
    {
        for (int i = 0; i < ctrl.Controls.Count; i++)
        {
            Control childControl = ctrl.Controls[i];
            if (childControl.GetType() == typeof(TextBox))
            {
                TextBox txt = (TextBox)childControl;
                string Text = txt.Text;
                ctrl.Controls.Remove(childControl);
                Label lbl = new Label();
                lbl.Text = Text;
                ctrl.Controls.Add(lbl);
            }
            //if the child control is not empty, repeat the process
            // for all its controls
            else if (childControl.HasControls())
            {
                PrepareForExport(childControl);
            }

        }
    }

    public static void ExportToPDF(GridView gv, string Title, string Filter, int[] ColToRmove, string PaperType, bool PrepareExport, bool isCaricoFrigoEmoteca = false)
    {
        int[] columnWidths = { 20, 30, 20, 20, 50, 60, 30, 20, 20 };
        if (PrepareExport == true)
            PrepareForExport(gv);
            //set the cotent type to PDF
            //gv.EnableTheming = true;
            StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        
        if (ColToRmove != null)
        {
            int i = 0;
            foreach (int a in ColToRmove)
            {

                if (i == 0)
                {
                    gv.Columns[a].Visible = false;
                    gv.Columns.RemoveAt(a);
                    gv.HeaderRow.Cells.RemoveAt(a);
                }
                else
                {
                    gv.Columns[a - i].Visible = false;
                    gv.Columns.RemoveAt(a - i);
                    gv.HeaderRow.Cells.RemoveAt(a - i);
                }
                i = i + 1;
            }
        }

        // TemplateField tf = new TemplateField();
        //gv.Columns.Insert(0, tf);

        BoundField field = new BoundField();
        field.HeaderText = "SrNo.";
        gv.Columns.Insert(0, field);
        foreach (GridViewRow row in gv.Rows)
        {
            TableCell cl = new TableCell();
            if (gv.Rows[row.RowIndex].Cells[0].Text != "Total:")
                cl.Text = Convert.ToString(row.RowIndex + 1);
            //cl.Width = 20f;
            cl.Width = Unit.Pixel(20);
            cl.Attributes.Add("style", "color:#000");
            row.Cells.AddAt(0, cl);
        }

        if(isCaricoFrigoEmoteca)
        {
            BoundField fieldDesk = new BoundField();
            fieldDesk.HeaderText = "Frigo Emoteca";
            gv.Columns.Insert(gv.Columns.Count, fieldDesk);
            foreach(GridViewRow row in gv.Rows)
            {
                TableCell cl = new TableCell();
                cl.Text = " ";
                cl.Width = Unit.Pixel(20);
                cl.Attributes.Add("style", "color:#0047298");
                row.Cells.AddAt(row.Cells.Count, cl);               
            }
        }
        gv.HeaderRow.Visible = false;
        gv.RenderControl(hw);

        //PdfPTable table = new PdfPTable();

        //load the html content to the string reader
        StringBuilder strbldr = new StringBuilder();

        strHeder = new StringBuilder();
        strFilter = new StringBuilder();
        // College Name Header
        strHeder.Append("<div><table style='text-align: center; line-height:12px;' cellpadding='0' cellspacing='0'><tr ><td style='vertical-align: top;'><img src='"
            + HttpContext.Current.Server.MapPath("~\\images\\logo.png")
            + "' width='150px'; height ='90px'/></td><td colspan='7' style='font-size: 13px;  font-weight: bold; text-align: center; vertical-align: top;'> <br />"
           + "</td></tr></table>");
        // Filter And Table header
        strFilter.Append("<table style='text-align: center; line-height:12px;' cellpadding='1' cellspacing='0'><tr bgcolor='#eeeeee'><td style='font-size: 10px;  text-align:left; vertical-align: top; font-weight:bold' >" + Title + "</td><td style='font-size: 8px;  text-align:right; vertical-align: top; font-weight:bold' >Print Data: " + System.DateTime.Now.ToString("dd/MM/yyyy") + "</td></tr><tr><td style='font-size: 10px;font-weight: bold;  text-align:left; vertical-align: top;' colspan='2' bgcolor='#eeeeee'>" + Filter + " </td></tr></table>");
        strFilter.Append("<table border='1' style='font-size:9px;'>");
        strFilter.Append("<tr bgcolor='rgb(49,49,49)' color='#fff' style='width:100px !important'>");

        for (int i = 0; i < gv.Columns.Count; i++)
        {
            strFilter.Append("<th style='width:30px !important'>");
            strFilter.Append(gv.Columns[i].ToString());
            strFilter.Append("</th>");
        }

        if (gv.Columns.Count == 1)
        {
            for (int c = 0; c < gv.HeaderRow.Cells.Count; c++)
            {
                strFilter.Append("<th style='width:50px !important'>");
                strFilter.Append(gv.HeaderRow.Cells[c].Text.ToString());
                strFilter.Append("</th>");
            }
        }
        strFilter.Append("</tr>");
        strFilter.Append("</table>");

        strbldr.Append("<div style='font-size:9px;margin-top:10px;'>"); 
        strbldr.Append(sw.ToString());
        strbldr.Append("</div>");
        string val = strbldr.ToString();
        StringReader sr = new StringReader(strbldr.ToString());
        Document document = new Document();

        strPaperType = PaperType.ToUpper();
        Image image = Image.GetInstance(HttpContext.Current.Server.MapPath("~\\images\\logo.png"));
        image.Alignment = Image.UNDERLYING;
        image.ScalePercent(50);
        if (PaperType.ToUpper() == "P")
        {
            document = new Document(PageSize.A4_LANDSCAPE, 10f, 10f, 10f, 0f);
            image.SetAbsolutePosition(120f, 300f);
        }
        else if (PaperType.ToUpper() == "LS")
        {
            document = new Document(PageSize.LEGAL_LANDSCAPE, 10f, 10f, 10f, 0f);
            document.SetPageSize(iTextSharp.text.PageSize.LEGAL_LANDSCAPE.Rotate());
            image.SetAbsolutePosition(275f, 100f);
        }
        else
        {
            document = new Document(new Rectangle(288f, 144f), 10, 10, 10, 10);
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            image.SetAbsolutePosition(275f, 100f);
        }
        HTMLWorker htmlWorker = new HTMLWorker(document);
        //htmlWorker.SetStyleSheet(Generate); 
        PdfWriter pdfDoc = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
        pdfDoc.PageEvent = new ImportExport();
        document.Open();
        //string s = pdfDoc.PageNumber.ToString();
        //document.Add(image);
        htmlWorker.Parse(sr);
        document.Close();

       //   HttpContext.Current.Response.TransmitFile(Server.MapPath("~//Uploads//" + "CHD80000" + Name[0] + Convert.ToInt16(Name[1]).ToString() + Name[2] + "01.txt"));
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "inline;filename=" + DateTime.Now.Ticks.ToString() + ".pdf");
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.End();

        //HttpContext.Current.Response.Write(document);
        //HttpContext.Current.Response.End();


    }


    public static void ExportToExcel(GridView grdControl)
    {

        string Path = HttpContext.Current.Server.MapPath("~/ImportExport/" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".xls");
        FileInfo FI = new FileInfo(Path);
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
        grdControl.RenderControl(htmlWrite);
        string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
        stringWriter.ToString().Normalize();
        vw.Write(stringWriter.ToString());
        vw.Flush();
        vw.Close();
        File.Delete(Path);
        WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
    }
    public static void ExportToWord(GridView grdControl)
    {
        PrepareForExport(grdControl);
        string Path = HttpContext.Current.Server.MapPath("~/ImportExport/" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".doc");
        FileInfo FI = new FileInfo(Path);
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
        grdControl.RenderControl(htmlWrite);
        string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
        stringWriter.ToString().Normalize();
        vw.Write(stringWriter.ToString());
        vw.Flush();
        vw.Close();
        File.Delete(Path);
        WriteAttachment(FI.Name, " application/vnd.ms-word ", stringWriter.ToString());
    }
    public static void WriteAttachment(string FileName, string FileType, string content)
    {
        HttpResponse Response = System.Web.HttpContext.Current.Response;
        Response.ClearHeaders();
        Response.AppendHeader("Content-Disposition", "inline; filename=" + FileName);
        Response.ContentType = FileType;
        Response.Write(content);
        Response.End();
    }
    //Create object of PdfContentByte
    public void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
    {


    }
    public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title) { }
    public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition) { }
    public void OnCloseDocument(PdfWriter writer, Document document) { }
    public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text) { }
    public void OnOpenDocument(PdfWriter writer, Document document)
    {


    }
    public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition) { }
    public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition) { }
    public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title) { }
    public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition) { }
    public void OnStartPage(PdfWriter writer, Document document)
    {
        string ss = writer.PageNumber.ToString();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        StringReader sr;
        if (writer.PageNumber.ToString() == "1")
            sr = new StringReader(strHeder.Append(strFilter.ToString()).ToString());
        else
            sr = new StringReader(strFilter.ToString());

        Image image = Image.GetInstance(HttpContext.Current.Server.MapPath("~\\images\\logo.png"));
        image.Alignment = Image.UNDERLYING;
        image.ScalePercent(80);
        if (strPaperType.ToUpper() == "P")
            image.SetAbsolutePosition(120f, 300f);
        else
            image.SetAbsolutePosition(275f, 100f);

        //document.Add(image);
        HTMLWorker htmlWorker = new HTMLWorker(document);
        htmlWorker.Parse(sr);

    }

}


