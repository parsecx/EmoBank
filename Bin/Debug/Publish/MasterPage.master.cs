﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections;
using DAL;
using System.Data;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlHelper cls = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

       
        if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            SqlParameter[] opara ={
                                      new SqlParameter("@CodID",SqlDbType.VarChar)
                                   
                                 };
            opara[0].Value = Session["UserId"].ToString();
            DataTable dtEmpDetails = cls.ExecuteDataTable("Sp_GetEmobank_UsersMenu", opara, CommandType.StoredProcedure);
            if (dtEmpDetails.Rows.Count > 0)
            {
                Session["GestioneUtentieAccessi"] = dtEmpDetails.Rows[0]["GestioneUtentieAccessi"].ToString();
                Session["BancaSangue/Emoderivati"] = dtEmpDetails.Rows[0]["BancaSangue/Emoderivati"].ToString();
                Session["AmbulatoriPuntiPrelievo"] = dtEmpDetails.Rows[0]["AmbulatoriPuntiPrelievo"].ToString();
                Session["MovimentiPrelievi"] = dtEmpDetails.Rows[0]["MovimentiPrelievi"].ToString();
                Session["AnalisiStatistichePrelievi"] = dtEmpDetails.Rows[0]["AnalisiStatistichePrelievi"].ToString();
                Session["AnagraficaCliniche"] = dtEmpDetails.Rows[0]["AnagraficaCliniche"].ToString();
                Session["MovimentoRichiesteCliniche"] = dtEmpDetails.Rows[0]["MovimentoRichiesteCliniche"].ToString();
                Session["AnalisiConsegneCliniche"] = dtEmpDetails.Rows[0]["AnalisiConsegneCliniche"].ToString();
                Session["LoadBloodRrefrigerator"] = dtEmpDetails.Rows[0]["LoadBloodRrefrigerator"].ToString();
                Session["AnagraficaDonatori"] = dtEmpDetails.Rows[0]["AnagraficaDonatori"].ToString();
                
                if (Convert.ToString(dtEmpDetails.Rows[0]["GestioneUtentieAccessi"]) == "Y")
                {
                    //id21.Visible = true;
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["BancaSangue/Emoderivati"]) == "Y")
                {
                    id21.Visible = true;
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["AmbulatoriPuntiPrelievo"]) == "Y")
                {
                    id22.Visible = true;
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["MovimentiPrelievi"]) == "Y")
                {
                    id31.Visible = true;
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["AnalisiStatistichePrelievi"]) == "Y")
                {
                    id32.Visible = true;
                }
                if (Convert.ToString(dtEmpDetails.Rows[0]["LoadBloodRrefrigerator"]) == "Y")
                {
                    id33.Visible = true;
                }
                
                if (Convert.ToString(dtEmpDetails.Rows[0]["AnagraficaCliniche"]) == "Y")
                {
                    id41.Visible = true;
                }
                if (Convert.ToString(dtEmpDetails.Rows[0]["AnagraficaDonatori"]) == "Y")
                {
                    id23.Visible = true;
                }
                
                if (Convert.ToString(dtEmpDetails.Rows[0]["MovimentoRichiesteCliniche"]) == "Y")
                {
                    id42.Visible = true;
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["AnalisiConsegneCliniche"]) == "Y")
                {
                    id43.Visible = false;
                }

                string pageName = Path.GetFileName(Request.Path);
                if (Convert.ToString(dtEmpDetails.Rows[0]["GestioneUtentieAccessi"]) != "Y" && pageName == "FrmEmobank_Users.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["BancaSangue/Emoderivati"]) != "Y" && pageName == "frmCentralBank.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["AmbulatoriPuntiPrelievo"]) != "Y" && pageName == "frmSamplingPoint.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["MovimentiPrelievi"]) != "Y" && pageName == "frmMovimentoPrelievi.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }
                if (Convert.ToString(dtEmpDetails.Rows[0]["LoadBloodRrefrigerator"]) != "Y" && pageName == "frmCaricoFrigoEmoteca.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }
                if (Convert.ToString(dtEmpDetails.Rows[0]["AnalisiStatistichePrelievi"]) != "Y" && pageName == "frmAnalisiPrelievi.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["AnagraficaCliniche"]) != "Y" && pageName == "frmAnagraficaClinicheVeterinarie.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }
                if (Convert.ToString(dtEmpDetails.Rows[0]["AnagraficaDonatori"]) != "Y" && pageName == "frmAnagraficaDonatori.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }
                if (Convert.ToString(dtEmpDetails.Rows[0]["MovimentoRichiesteCliniche"]) != "Y" && pageName == "frmMovimentoRichiesteCliniche.aspx")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }

                if (Convert.ToString(dtEmpDetails.Rows[0]["AnalisiConsegneCliniche"]) != "Y" && pageName == "")
                {
                    Response.Redirect("UnauthorizedPage.aspx");
                }
                
            }

        }
    }




    public void ClearApplicationCache()
    {
        List<string> keys = new List<string>();

        // retrieve application Cache enumerator
        IDictionaryEnumerator enumerator = Cache.GetEnumerator();

        // copy all keys that currently exist in Cache
        while (enumerator.MoveNext())
        {
            keys.Add(enumerator.Key.ToString());
        }

        // delete every key from cache
        for (int i = 0; i < keys.Count; i++)
        {
            Cache.Remove(keys[i]);
        }
    }


    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");

        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();

        if (Request.Cookies["ASP.NET_SessionId"] != null)
        {
            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
        }

        if (Request.Cookies["AuthToken"] != null)
        {
            Response.Cookies["AuthToken"].Value = string.Empty;
            Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
        }
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        ClearApplicationCache();
        Context.RewritePath("Login.aspx");
        Response.Redirect("Login.aspx");

    }

}
