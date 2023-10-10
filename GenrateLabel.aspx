﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="GenrateLabel.aspx.cs" Inherits="GenrateLabel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page animsition">
        <div class="page-header">

            <h1 class="page-title">Anagrafica Donatore</h1>
        </div>
        <div class="page-content">
            <div class="panel" id="List" runat="server">
                <div class="panel-body">
                    <div class="row row-lg">
                        <div class="col-sm-12">

                            <!-- Example Table From Data -->
                            <div class="example-wrap">
                                <div class="example">
                                    <div class="row">
                                        <div class="form-group col-sm-2">
                                            <asp:LinkButton ID="btnStampa" OnClientClick="window.document.forms[0].target='_blank';" runat="server" OnClick="btnStampa_Click" Text="Stampa" CssClass="btn btn-block btn-danger" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Panel ID="pnl" runat="server">
                                            <table style="display: block; width: 388px; height: 410px; border: solid 1px #000; font-size: small; float: left" border="1">
                                                <tr>
                                                    <td colspan="3" style="height: 20px">
                                                        <table border="0">
                                                            <tr>
                                                                <td style="height: 40px; border-right: none">

                                                                    <img src="images/logo.png" width="90" id="img" runat="server" />
                                                                </td>
                                                                <td colspan="2" style="border-left: none">
                                                                    <table border="0">
                                                                        <tr>
                                                                            <td colspan="2" style="line-height: 6px;">
                                                                                <asp:Label ID="sp1" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="line-height: 6px;">
                                                                                <asp:Label ID="sp2" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="line-height: 6px;">
                                                                                <asp:Label ID="sp3" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="line-height: 6px;">
                                                                                <asp:Label ID="sp4" runat="server"></asp:Label></td>
                                                                            <td style="line-height: 6px;">
                                                                                <asp:Label ID="sp5" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 185px; height: 40px; vertical-align: sub;">
                                                        <span style="text-align: left"><b>Protocollo Prelievo</b></span>
                                                        <br />

                                                        <table style="text-align: right; float: right" border="0">
                                                            <tr>
                                                                <td style="text-align: center; float: right">
                                                                    <asp:Label ID="lblProtocolNo" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                    <td style="width: 100px"><span><b>Data Prelievo</b></span>
                                                        <br />
                                                        <table style="text-align: right; float: right" border="0">
                                                            <tr>
                                                                <td style="text-align: center; float: right">
                                                                    <asp:Label ID="lblDataPrelievo" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                    <td style="width: 100px"><span><b>Data Scadenza</b></span>
                                                        <br />
                                                        <table style="text-align: center; float: right" border="0">
                                                            <tr>
                                                                <td style="text-align: center; float: right">
                                                                    <asp:Label ID="lblDataScadenza" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 185px; height: 40px; vertical-align: sub;"><b>Tipo Preparato</b><br />
                                                        <asp:Label ID="lblTipoPreparato" runat="server"></asp:Label></td>
                                                    <td style="width: 100px"><b>Specie Animale</b><br />

                                                        <table style="text-align: right; float: right" border="0">
                                                            <tr>
                                                                <td style="text-align:center; float: right">
                                                                    <asp:Label ID="lblSpecie" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                    <td style="width: 100px"><b>Gruppo Sanguigno</b><br />
                                                        <table style="text-align: center; float: right" border="0">
                                                            <tr>
                                                                <td style="text-align: center; float: right">
                                                                    <asp:Label ID="lblGruppo" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="2" style="height: 40px"><b>Note</b><br />
                                                        <asp:Label ID="lblNote" runat="server"></asp:Label>

                                                    </td>
                                                    <td><b>Peso Lordo</b><br />
                                                        <table style="text-align: center; float: right" border="0">
                                                            <tr>
                                                                <td style="text-align: center; float: right">
                                                                    <asp:Label ID="lblPesoLordo" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3"><b>Composizione</b><br />
                                                        <asp:Label ID="lblComposizione" runat="server"></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="3"><b>Conservazione</b><br />
                                                        <asp:Label ID="lblConservazione" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td colspan="3" style="font-size: 6px; font-family: tahoma; padding: 4px 2px; font-weight: normal">Esclusivamente per uso Veterinario ( Specie Destinazione
                    <asp:Label ID="lblSpecie1" runat="server" ForeColor="Red"></asp:Label>
                                                        ), non utilizzabile a scopo trasfusionale se presenta emolisi
o altre anomalie evidenti. Per la trasfusione utilizzare un dato dispositivo munito appropriato filtro.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="font-size: 3px; text-align: center">
                                                        <table border="0" style="font-size: 3px; text-align: center">
                                                            <tr>
                                                                <td colspan="6"></td>
                                                                <td colspan="6">
                                                                    <img src="images/7_bold.png" id="img7" width="15" runat="server" style="float: left;" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">

                                                                    <%--  <span style="width: 15%; display: block; float: left; font-family: tahoma;">--%>
                                                                    <img src="images/Icon_Blood.png" id="img1" runat="server" style="vertical-align: top" width="130" />

                                                                    <%-- </span>--%>
                                                                </td>



                                                                <td colspan="6">
                                                                    <%--  <span style="width: 10%; display: block; float: left; font-family: tahoma;">--%>


                                                                    <img src="logo.png" id="imgBarCode" runat="server" width="140" />
                                                                    <%--<asp:PlaceHolder ID="plBarCode" runat="server" />--%>


                                                                    <%--  <asp:Image ID="imgBarCode" runat="server" Width="100%" Height="37px" />--%>
                                                                   

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

