﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintLabel.aspx.cs" Inherits="PrintLabel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            font-family: Arial;
        }

        td {
            color: black;
            font-weight: bold;
            font-size: 10px;
            vertical-align: sub;
            padding: 4px;
        }

            td span {
                font-weight: normal !important;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnl" runat="server">


            <table style="display: block; overflow: hidden; width: 378px; height: 378px; border: solid 1px #000; font-size: small; float: left" border="1">
                <tr>
                    <td colspan="3" style="height: 40px">
                        <img src="images/logo.png" width="160px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 185px; height: 40px; vertical-align: sub;">Protocollo Prelievo<br />
                        <asp:Label ID="lblProtocolNo" runat="server" Font-Bold="true"></asp:Label></td>
                    <td style="width: 100px">Data Prelievo<br />
                        <asp:Label ID="lblDataPrelievo" runat="server" Font-Bold="true"></asp:Label></td>
                    <td style="width: 100px">Data Scadenza<br />
                        <asp:Label ID="lblDataScadenza" runat="server" Font-Bold="true"></asp:Label></td>
                </tr>

                <tr>
                    <td style="width: 185px; height: 40px; vertical-align: sub;">Tipo Preparato<br />
                        <asp:Label ID="lblTipoPreparato" runat="server" Font-Bold="true"></asp:Label></td>
                    <td style="width: 100px">Specie Animale<br />
                        <asp:Label ID="lblSpecie" runat="server" Font-Bold="true"></asp:Label></td>
                    <td style="width: 100px">Gruppo Sanguigno<br />
                        <asp:Label ID="lblGruppo" runat="server" Font-Bold="true"></asp:Label></td>
                </tr>

                <tr>
                    <td colspan="2" style="height: 40px">Note<br />
                        <asp:Label ID="lblNote" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td>Peso Lordo<br />
                        <asp:Label ID="lblPesoLordo" runat="server" Font-Bold="true"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">Composizione<br />
                        <asp:Label ID="lblComposizione" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="3">Conservazione<br />
                        <asp:Label ID="lblConservazione" runat="server" Font-Bold="true"></asp:Label></td>
                </tr>

                <tr>
                    <td colspan="3" style="font-size: 6px; font-family: tahoma; padding: 4px 2px; font-weight: normal">Esclusivamente per uso Veterinario ( Specie Destinazione
                    <asp:Label ID="lblSpecie1" runat="server" ForeColor="Red"></asp:Label>
                        ), non utilizzabile a scopo trasfusionale se presenta emolisi
o altre anomalie evidenti. Per la trasfusione utilizzare un dato dispositivo munito appropriato filtro.
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-size: 6px; text-align: center; height: 102px">
                        <span style="width: 7%; display: block; float: left; margin-top: 12px; font-family: tahoma;">
                            <img src="images/1_bold.png" width="100%" />
                            <br />
                            Sterilizzata per Irradiazione
                        </span>
                        <span style="width: 10%; display: block; float: left; margin-top: 12px; font-family: tahoma;">
                            <img src="images/2_bold.png" width="80%" />
                            <br />
                            Fluido Libero da Batteri    </span>
                        <span style="width: 10%; display: block; float: left; margin-top: 12px; font-family: tahoma;">
                            <img src="images/3_bold.png" width="80%" />
                            <br />
                            Monouso   </span>
                        <span style="width: 10%; display: block; float: left; margin-top: 12px; font-family: tahoma;">
                            <img src="images/4_bold.png" width="80%" />
                            <br />
                            Leggere Istruzioni d’uso  </span>
                        <span style="width: 10%; display: block; float: left; margin-top: 12px; font-family: tahoma;">
                            <img src="images/5_bold.png" width="80%" />
                            <br />
                            Non Utilizzare se Danneggiata </span>
                        <span style="width: 10%; display: block; float: left; margin-top: 12px; font-family: tahoma;">
                            <img src="images/6_bold.png" width="80%" />
                            <br />
                            Non Ventilar  </span>
                        <span style="width: 40%; display: block; float: left; text-align: left;">
                            <img src="images/7_bold.png" width="25%" style="float: left; margin-bottom: 2px" />
                            <br />
                            <%--<asp:Image ID="imgBarCode" runat="server" Width="100%" Height="37px" Visible="false" />--%>
                              <asp:Label ID="lblBarCode" runat="server" Text="13.201600013.08072016.1108" style="font-family:IDAutomationHC39M !important" ></asp:Label>
                        </span>

                        <asp:Image ID="imgQrCode" runat="server" Visible="false" />
                      
                    </td>
                    
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
