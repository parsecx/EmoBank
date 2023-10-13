using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

/// <summary>
/// Summary description for NewPdfGenerator
/// </summary>
public static class NewPdfGenerator
{
    public static void ExportGridToPdf(GridView gv, string Title, string Filter, int[] ColToRmove, string PaperType, bool PrepareExport, bool isCaricoFrigoEmoteca = false)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv.RenderControl(hw);
       
    }
}
