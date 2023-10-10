﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="frmLabelCreate.aspx.cs" Inherits="frmLabelCreate" %>

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
                                            <asp:Repeater ID="rpt" runat="server">
                                                <ItemTemplate>

                                                    <table style="display: block; overflow: hidden; width: 388px; height: 410px; border: solid 1px #000; font-size: small; float: left" border="1">
                                                        <tr>
                                                            <td colspan="3" style="height: 20px">
                                                                <table border="0">
                                                                    <tr>
                                                                        <td style="height: 40px; border-right: none">
                                                                            <img src="images/logo.png" width="160" id="img" runat="server" />
                                                                            <asp:Label ID="hdCodId" Visible="false" runat="server" Text='<%# Eval("CodID") %>' />
                                                                        </td>
                                                                        <td colspan="2" style="border-left: none">
                                                                            <table border="0">
                                                                                <tr>
                                                                                    <td colspan="2" style="line-height: 6px;">
                                                                                        <asp:Label ID="sp1" runat="server" Text='<%# Eval("Description") %>' ></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="line-height: 6px;">
                                                                                        <asp:Label ID="sp2" runat="server" Text='<%# Eval("Address") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="line-height: 6px;">
                                                                                        <asp:Label ID="sp3" runat="server" Text='<%# Eval("resort") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="line-height: 6px;">
                                                                                        <asp:Label ID="sp4" runat="server" Text='<%# Eval("PostalCode") %>'></asp:Label></td>
                                                                                    <td style="line-height: 6px;">
                                                                                        <asp:Label ID="sp5" runat="server" Text='<%# Eval("province") %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 185px; height: 40px; vertical-align: sub;"><b>Protocollo Prelievo</b>
                                                                <br />
                                                                <asp:Label ID="lblProtocolNo" runat="server" Text='<%# Eval("ProtocolNumber") %>'></asp:Label></td>
                                                            <td style="width: 100px"><b>Data Prelievo</b><br />
                                                                <asp:Label ID="lblDataPrelievo" runat="server" Text='<%# Eval("DateTimeDrawing") %>'></asp:Label></td>
                                                            <td style="width: 100px"><b>Data Scadenza</b><br />
                                                                <asp:Label ID="lblDataScadenza" runat="server" Text='<%# Eval("ProductExpirationDate") %>'></asp:Label></td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 185px; height: 40px; vertical-align: sub;"><b>Tipo Preparato</b><br />
                                                                <asp:Label ID="lblTipoPreparato" runat="server" Text='<%# Eval("TypePrepared") %>'> </asp:Label></td>
                                                            <td style="width: 100px"><b>Specie Animale</b><br />
                                                                <asp:Label ID="lblSpecie" runat="server" Text='<%# Eval("SpeciesName") %>'></asp:Label></td>
                                                            <td style="width: 100px"><b>Gruppo Sanguigno</b><br />
                                                                <asp:Label ID="lblGruppo" runat="server" Text='<%# Eval("AnimalGroupBlood") %>'></asp:Label></td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="2" style="height: 40px"><b>Note</b><br />
                                                                <asp:Label ID="lblNote" runat="server" Text='<%# Eval("Note") %>'></asp:Label>
                                                            </td>
                                                            <td><b>Peso Lordo</b><br />
                                                                <asp:Label ID="lblPesoLordo" runat="server" Text='<%# Eval("GrossWeightPrepared") %>'></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3"><b>Composizione</b><br />
                                                                <asp:Label ID="lblComposizione" runat="server" Text='<%# Eval("CompositionVolumeAnticoagulant") %>'></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="3"><b>Conservazione</b><br />
                                                                <asp:Label ID="lblConservazione" runat="server" Text='<%# Eval("ModeStorageTemp") %>'></asp:Label></td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="3" style="font-size: 6px; font-family: tahoma; padding: 4px 2px; font-weight: normal">Esclusivamente per uso Veterinario ( Specie Destinazione
                    <asp:Label ID="lblSpecie1" runat="server" ForeColor="Red"  Text='<%# Eval("SpeciesName") %>'></asp:Label>
                                                                ), non utilizzabile a scopo trasfusionale se presenta emolisi
o altre anomalie evidenti. Per la trasfusione utilizzare un dato dispositivo munito appropriato filtro.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="font-size: 3px; text-align: center">
                                                                <table border="0" style="font-size: 3px; text-align: center">
                                                                    <tr>
                                                                        <td colspan="6"></td>
                                                                        <td colspan="5" style="text-align: right">
                                                                            <img src="images/7_bold.png" id="img7" width="15" runat="server" style="float: right;" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 15px;">
                                                                            <span style="width: 15%; display: block; float: left; font-family: tahoma;">
                                                                                <img src="images/1_bold.png" width="20" height="16" id="img1" runat="server" style="vertical-align: top" />
                                                                                <br />
                                                                                Sterilizzata per Irradiazione
                                                                            </span>
                                                                        </td>
                                                                        <td style="width: 15px">
                                                                            <span style="width: 10%; display: block; float: left; font-family: tahoma;">
                                                                                <img src="images/2_bold.png" width="20" id="img2" runat="server" />
                                                                                <br />
                                                                                Fluido Libero da Batteri

                                                                            </span></td>
                                                                        <td style="width: 15px">
                                                                            <span style="width: 10%; display: block; float: left; font-family: tahoma;">
                                                                                <img src="images/3_bold.png" width="20" id="img3" runat="server" />
                                                                                <br />
                                                                                Monouso   </span></td>
                                                                        <td style="width: 15px">
                                                                            <span style="width: 10%; display: block; float: left; font-family: tahoma;">
                                                                                <img src="images/4_bold.png" width="20" id="img4" runat="server" />
                                                                                <br />
                                                                                Leggere Istruzioni d’uso  </span></td>
                                                                        <td style="width: 15px">
                                                                            <span style="width: 10%; display: block; float: left; font-family: tahoma;">
                                                                                <img src="images/5_bold.png" width="20" id="img5" runat="server" />
                                                                                <br />
                                                                                Non Utilizzare se Danneggiata </span></td>
                                                                        <td style="width: 15px">
                                                                            <span style="width: 10%; display: block; float: left; font-family: tahoma;">
                                                                                <img src="images/6_bold.png" width="20" id="img6" runat="server" />
                                                                                <br />
                                                                                Non Ventilare
                                                                            </span></td>
                                                                        <td colspan="5" style="line-height: 22px; font-size: 28px">
                                                                            <span style="display: block; float: left; text-align: left; font-size: 50px">


                                                                                <img src="logo.png" id="imgBarCode" runat="server" />


                                                                                <%-- <asp:Image ID="imgBarCode" runat="server" Width="100%" Height="37px" />--%>
                                                                            </span>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                   
                                                    ========================================
                                                    <br /><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
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

