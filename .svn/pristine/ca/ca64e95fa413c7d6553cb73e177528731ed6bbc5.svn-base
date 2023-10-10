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

public partial class frmUsers : System.Web.UI.Page
{
    SqlHelper cls = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
        {
            if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
            {

                Response.Redirect(ResolveUrl("~/index.aspx"));
            }
            else
            {
                if (!IsPostBack)
                {

                    BindUsers();


                }
            }
        }

        else
        {
            Response.Redirect(ResolveUrl("~/index.aspx"));
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    protected void btnStampa_Click(object sender, EventArgs e)
    {
        int[] a = new int[1];
        a[0] = 0;
        ImportExport.ExportToPDF(grdData, "Users", "", a, "A4", true);
    }
    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmEmobank_Users.aspx");
    }
    private DataTable BindUsers()
    {
        SqlParameter[] oPera =
        {
            new SqlParameter("Type",ddl1.SelectedValue)
        };
        DataTable dt = cls.ExecuteDataTable("GetUsers", oPera, CommandType.StoredProcedure);
        grdData.DataSource = dt;
        grdData.DataBind();
        return dt;
    }
    protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUsers();
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

        DataView sortedView = new DataView(BindUsers());
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
        BindUsers();
    }
    protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gv = grdData.SelectedRow;
        string CodInt = gv.Cells[1].Text;
        Session["CodInt"] = CodInt;
        Response.Redirect("frmUpdateUser.aspx");

    }
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 != 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            }
            else
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            ImageButton img = (ImageButton)e.Row.Cells[0].FindControl("btnDelete");
            if (e.Row.Cells[1].Text != Session["UserId"].ToString())
            {
                img.Visible = true;
            }
            else
            {
                img.Visible = false;
            }
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {

                e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdData, "Select$" + e.Row.RowIndex);
                e.Row.Cells[i].ToolTip = "Click to view detail.";
                e.Row.Cells[i].Attributes.Add("style", "cursor:pointer");
            }
        }
    }
    protected void grdData_RowDataCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "DEL")
        {
            ltrID.Text = grdData.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",ltrID.Text)
        };
            int a = cls.ExecuteNonQuery("[sp_DeleteRegisterEmoUser]", oPera, CommandType.StoredProcedure);
            BindUsers();
        }
        if (e.CommandName == "Edit")
        {
            ltrID.Text = grdData.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();

            SqlParameter[] oPera =
        {
            new SqlParameter("@CodID",ltrID.Text)
        };
            string CodInt = ltrID.Text;
            Session["CodInt"] = CodInt;
            Response.Redirect("frmUpdateUser.aspx");
        }

    }


}