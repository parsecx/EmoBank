﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="frmMovimentoPrelievi.aspx.cs" Inherits="frmMovimentoPrelievi" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .floatRight {
            float: right;
        }

        .cc td:first-child {
            display: none;
        }

        .cc th:first-child {
            display: none;
        }

        .Head-table th {
            font-weight: bold;
            text-align: center;
        }

        .Head-table {
            border-bottom: 0 none !important;
            margin-bottom: -1px !important;
        }

            .Head-table select {
                margin: 10px 0%;
                width: 100% !important;
            }

        .itemstyle {
            padding: 0;
            width: 12%;
            text-align: center;
        }


        .chzn-container {
            float: left;
            margin: 0 !important;
            padding: 0 !important;
            width: 100% !important;
        }

        th > .chzn-container {
            margin: 11px 0px !important;
            width: 100% !important;
        }

        .chzn-container-single .chzn-single span {
            font-size: 14px;
            padding: 5px 0 0 15px;
        }

        select {
            background: yellow none repeat scroll 0 0 !important;
        }

        .yell input {
            background: yellow none repeat scroll 0 0 !important;
        }
    </style>
    <style>
        a img {
            border: none;
        }

        ol li {
            list-style: decimal outside;
        }

        div#container {
            width: 780px;
            margin: 0 auto;
            padding: 1em 0;
        }

        div.side-by-side {
            width: 100%;
            margin-bottom: 1em;
        }

            div.side-by-side > div {
                float: left;
                width: 50%;
            }

                div.side-by-side > div > em {
                    margin-bottom: 10px;
                    display: block;
                }

        .clearfix:after {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }

        select {
        }
    </style>
    <%--<link rel="stylesheet" href="Style/chosen.css" />--%>
    <link href="Style/chosen.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page animsition">
        <div class="page-header">
            <h1 class="page-title">Movimento Prelievi</h1>
        </div>
        <div class="page-content">
            <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
            <asp:UpdateProgress ID="upp1" runat="server" ClientIDMode="AutoID" AssociatedUpdatePanelID="up1">
                <ProgressTemplate>
                    <div class="loader"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="up1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnStampa" />
                    <asp:AsyncPostBackTrigger ControlID="grdData" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTipoPreparato" EventName="SelectedIndexChanged" />
                    <%-- <asp:PostBackTrigger ControlID="btnSubmit" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="panel" id="List" runat="server">
                        <div class="panel-body">
                            <div class="row row-lg">
                                <div class="col-sm-12">
                                    <h3>Elenco Prelievi</h3>
                                    <!-- Example Table From Data -->
                                    <div class="example-wrap">
                                        <div class="row">
                                            <div class="form-group col-sm-8">
                                                <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                                            </div>
                                            <div class="form-group col-sm-2" style="margin-left: 64px;">
                                                <asp:LinkButton ID="btnStampa" OnClientClick="window.document.forms[0].target='_blank';" runat="server" OnClick="btnStampa_Click" Text="Stampa" CssClass="btn btn-block btn-danger" />
                                            </div>
                                            <div class="form-group col-sm-1">
                                                <asp:Button ID="btnNuovo" runat="server" Text="Nuovo" CssClass="btn btn-primary" OnClick="btnNuovo_Click" />
                                            </div>
                                        </div>

                                        <div class="clearfix"></div>
                                        <%-- <p>Transform table from an existing data.</p>--%>
                                        <div class="example">
                                            <table data-mobile-responsive="true" border="1" style="width: 100%; margin-bottom: 10px" class="Head-table">
                                                <tr>
                                                    <th style="width: 2%; padding: 0px;">
                                                        <img src="images/del.gif" style="visibility: hidden;" /></th>
                                                    <th style="width: 5%; padding: 0px;">
                                                        <asp:LinkButton ID="lnk1" runat="server" Text=" Cod. Int" OnClick="lnk1_Click" CommandArgument="CodID"></asp:LinkButton>
                                                    </th>
                                                    <th style="width: 10%; padding: 0px;">
                                                        <asp:LinkButton ID="lnkProtocolNumber" runat="server" Text="Protocollo" OnClick="lnk1_Click" CommandArgument="ProtocolNumber"></asp:LinkButton>
                                                        <th style="width: 3%; padding: 0px;">PR</th>

                                                        <th style="width: 10%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkDateTimeDrawing" runat="server" Text="Data Prelievo" OnClick="lnk1_Click" CommandArgument="DateTimeDrawing"></asp:LinkButton>
                                                        </th>
                                                        <th style="width: 25%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkDescription" runat="server" Text="Punto Prelievo" OnClick="lnk1_Click" CommandArgument="Description"></asp:LinkButton></th>
                                                        <th style="width: 10%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkAninale" runat="server" Text="Aninale" OnClick="lnk1_Click" CommandArgument="SpeciesName"></asp:LinkButton></th>
                                                        <th style="width: 10%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkAnimalGroupBlood" runat="server" Text="Gruppo Sanguigno" OnClick="lnk1_Click" CommandArgument="AnimalGroupBlood"></asp:LinkButton></th>
                                                        <th style="width: 25%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkName" runat="server" Text="Donatore" OnClick="lnk1_Click" CommandArgument="Name"></asp:LinkButton></th>
                                                </tr>
                                                <tr>
                                                    <th></th>
                                                    <th>
                                                        <asp:DropDownList ID="ddlCod1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true" Style="direction: rtl">
                                                        </asp:DropDownList>
                                                    </th>
                                                    <th>
                                                        <asp:DropDownList ID="ddlProtocol1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true" Style="direction: rtl">
                                                        </asp:DropDownList></th>
                                                    <th></th>

                                                    <th>
                                                        <asp:DropDownList ID="ddlDateTimeDrawing1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="ddlDescription1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="ddlSpeciesName1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="ddlAnimalGroupBlood1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList></th>
                                                    <th>
                                                        <asp:DropDownList ID="ddlNamee1" runat="server" class="form-control  chzn-select" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList></th>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="grdData" ShowHeaderWhenEmpty="false" DataKeyNames="CodID" AutoGenerateColumns="false" runat="server" data-mobile-responsive="true" border="1" Style="width: 100%" OnSorting="grdData_Sorting" PageSize="500" AllowSorting="true" AllowPaging="false" OnPageIndexChanging="grdData_PageIndexChanging" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged" ShowHeader="false" CssClass="Head-table1">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" ImageUrl="images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');" Text="Delete" runat="server" Style="padding-left: -2px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CodID" HeaderText="Cod. Int" SortExpression="CodID" ItemStyle-Width="5%" ItemStyle-CssClass="textright" />
                                                    <asp:TemplateField HeaderText="Protocollo" SortExpression="ProtocolNumber" ItemStyle-CssClass="textright width10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("ProtocolNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Progressive" HeaderText="PR" SortExpression="Progressive" ItemStyle-Width="3%" ItemStyle-CssClass="textright" />

                                                    <asp:BoundField DataField="DateTimeDrawing" HeaderText="Data Prelievo " SortExpression="DateTimeDrawing" ItemStyle-Width="10%" />
                                                    <asp:BoundField DataField="Description" HeaderText="Punto Prelievo" SortExpression="Description" ItemStyle-Width="25%" />
                                                    <asp:BoundField DataField="SpeciesName" HeaderText="Aninale" SortExpression="SpeciesName" ItemStyle-Width="10%" />
                                                    <asp:BoundField DataField="AnimalGroupBlood" HeaderText="Gruppo Sanguigno" SortExpression="AnimalGroupBlood" ItemStyle-Width="10%" />
                                                    <asp:BoundField DataField="Name" HeaderText="Donatore" SortExpression="Name" ItemStyle-Width="25%" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Record Found
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="panel" id="AddNew" runat="server" visible="false">
                        <div class="panel-body container-fluid">
                            <div class="row row-lg">
                                <div class="col-sm-12">
                                    <!-- Example Basic Form -->
                                    <div class="example-wrap">
                                        <h3>Scheda Prelievo</h3>
                                        <div class="example">
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="A" ForeColor="Red" />

                                                </div>
                                            </div>
                                            <div class="row" id="msg" runat="server" visible="false">
                                                <div class="form-group col-sm-12">
                                                    <div id="lblmsg" runat="server">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-5">
                                                </div>
                                                <div class="form-group col-sm-2">
                                                    <asp:Button ID="btnBack" Text="Indietro" CssClass="btn btn-primary floatRight" runat="server" OnClick="btnBack_Click" />
                                                </div>
                                                <div class="form-group col-sm-3" style="width: 21%">
                                                    <a id="btnStampaEti" runat="server" href="PrintLabel.aspx" target="_blank" class="btn btn-block btn-danger floatRight">Stampa Etichetta</a>
                                                    <%--<asp:Button ID="btnStampaEti" Text="Stampa Etichetta" CssClass="btn btn-block btn-danger floatRight" runat="server" OnClick="btnStampaEti_Click" OnClientClick="window.document.forms[0].target='_blank';" />--%>
                                                </div>

                                                <div class="form-group col-sm-2">
                                                    <asp:Button ID="btnRegister" Text="Registra" ValidationGroup="A" CssClass="btn btn-primary floatRight" runat="server" OnClick="btnRegister_Click" />

                                                </div>
                                            </div>

                                            <div class="row" style="margin-bottom: 30px; padding: 10px; border: 2px solid #3f48cc;">
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <h4 style="background: none repeat scroll 0% 0% rgb(63, 72, 204); color: white; margin: -10px; padding: 10px;">Riferimenti di Base </h4>
                                                    </div>
                                                </div>
                                                <div class="row" runat="server" id="CodicInterno" visible="false">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicFirstName">Codice Interno</label>
                                                        <asp:TextBox type="text" runat="server" MaxLength="9" class="form-control" ID="txtCodice"
                                                            autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-3">
                                                        <label class="control-label" for="inputBasicFirstName">Data Prelievo</label>
                                                        &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="A" runat="server" Display="Dynamic"
                                                            ErrorMessage="Enter Data Prelievo" SetFocusOnError="true" ControlToValidate="txtDatePrelievo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="com" runat="server" ControlToValidate="txtDatePrelievo" Type="Date" Operator="DataTypeCheck" ValidationGroup="A" SetFocusOnError="true" ErrorMessage="Enter valid Data Prelievo" Display="Dynamic" ForeColor="Red">*</asp:CompareValidator>
                                                        <asp:TextBox type="text" runat="server" MaxLength="10" class="form-control myDate" ID="txtDatePrelievo"
                                                            autocomplete="off"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-sm-3" runat="server" id="divCInterno" visible="false">
                                                        <label class="control-label" for="inputBasicFirstName">C.Interno</label>
                                                        <asp:TextBox type="text" ReadOnly="true" runat="server" MaxLength="9" class="form-control" ID="txtCInterno"
                                                            autocomplete="off"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-sm-3" runat="server" id="divProtocollo" visible="false">
                                                        <label class="control-label" for="inputBasicFirstName">Protocollo</label>
                                                        <asp:TextBox type="text" ReadOnly="true" runat="server" MaxLength="9" class="form-control" ID="txtProtocollo"
                                                            autocomplete="off"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-sm-3">
                                                        <label class="control-label" for="inputBasicFirstName">Progressivo</label>
                                                        <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtProgressivo" SetFocusOnError="true" ForeColor="Red" Type="Integer" Display="Dynamic" Operator="DataTypeCheck" ValidationGroup="A" ErrorMessage="Enter  Progressivo numeric Value" runat="server">*</asp:CompareValidator>
                                                        &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Enter Progressivo" SetFocusOnError="true" ControlToValidate="txtProgressivo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" Text="01" MaxLength="2" class="form-control" ID="txtProgressivo"
                                                            autocomplete="off"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Punto Prelievo</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Denominazione" SetFocusOnError="true" ControlToValidate="ddlPuntoPrelievo" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlPuntoPrelievo" runat="server" class="form-control  chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPuntoPrelievo_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Indirzzo</label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Enter Indirzzo" SetFocusOnError="true" ControlToValidate="txtIndirzzo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtIndirzzo"
                                                            placeholder="Indirzzo" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Localita</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="A"
                                                runat="server" ErrorMessage="Enter Localita" SetFocusOnError="true" ControlToValidate="txtLocalita" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" MaxLength="30" class="form-control" ID="txtLocalita" placeholder="Localita"
                                                            autocomplete="off" disabled="disabled"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Provincia</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Provincia" ControlToValidate="txtProvincia" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtProvincia" name="inputEmail"
                                                            placeholder="Provincia" autocomplete="off" MaxLength="5" disabled="disabled"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Cap</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Cap" ControlToValidate="txtCap" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtCap" name="inputEmail"
                                                            placeholder="Cap" autocomplete="off" MaxLength="5" ReadOnly="true" disabled="disabled"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Telefono</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Telefone" ControlToValidate="txtTelefono" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="c1" runat="server" Type="Integer" Operator="DataTypeCheck"
                                                            ControlToValidate="txtTelefono" ErrorMessage="Enter valid Mobile">*</asp:CompareValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtTelefono" name="inputEmail"
                                                            placeholder="Telefono" autocomplete="off" MaxLength="30" disabled="disabled"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">E_mail</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Email Id" ControlToValidate="txtE_mail" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="EmailIDValidation" runat="server" ControlToValidate="txtE_mail"
                                                            ErrorMessage="Invalid Email Address" ValidationGroup="A" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            Text="*" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtE_mail" name="inputEmail"
                                                            placeholder="E_mail" autocomplete="off" MaxLength="30" disabled="disabled"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group  col-sm-12">
                                                        <label class="control-label" for="inputBasicPassword">Referente</label>
                                                        <asp:TextBox type="text" runat="server" class="form-control"
                                                            ID="txtReferente" name="txtReferente"
                                                            placeholder="Referente" autocomplete="off" MaxLength="30" disabled="disabled"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-bottom: 30px; padding: 10px; border: 2px solid rgb(249, 104, 104);">
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <h4 style="background: none repeat scroll 0% 0% rgb(249, 104, 104); color: white; margin: -10px; padding: 10px;">Riferimenti Tecnici Prelievo</h4>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-4">
                                                        <label class="control-label" for="inputBasicFirstName">Specie Animale</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Specie Animale" SetFocusOnError="true" ControlToValidate="ddlSpecieAnimale" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlSpecieAnimale" runat="server" class="form-control" OnSelectedIndexChanged="ddlSpecieAnimale_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-sm-4">
                                                        <label class="control-label" for="inputBasicFirstName">Tipo Preparato</label>
                                                        &nbsp;
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Tipo Preparato" SetFocusOnError="true" ControlToValidate="ddlTipoPreparato" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                                                        <asp:DropDownList ID="ddlTipoPreparato" runat="server" class="form-control" OnSelectedIndexChanged="ddlTipoPreparato_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Enter Tipo Preparato" SetFocusOnError="true" ControlToValidate="txtTipoPreparato" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtTipoPreparato" class="form-control txtTipoPreparato" runat="server" Text="Sangue Intero Conservato" OnTextChanged="txtTipoPreparato_TextChanged" AutoPostBack="true" ></asp:TextBox>--%>
                                                    </div>

                                                    <div class="form-group col-sm-4">
                                                        <%-- <asp:CompareValidator ID="cmprValidatorDoubleType" ControlToValidate="txtPesoLordo" SetFocusOnError="true" ForeColor="Red" Type="Currency" Display="Dynamic" Operator="DataTypeCheck" EnableClientScript="true" ValidationGroup="A" ErrorMessage="Enter Peso Lordo Decimal Value" runat="server">*</asp:CompareValidator>--%>

                                                        <label class="control-label" for="inputBasicFirstName">Peso Lordo</label>
                                                        &nbsp;
                                                        <%--<asp:RegularExpressionValidator ID="reg1" runat="server" ValidationExpression="^(([5-9][0-9])|([0-9][0-9][0-9][0-9]))(\,\d{1,2})?$" ControlToValidate="txtPesoLordo" ValidationGroup="A" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Enter Peso Lordo Decimal Value">*</asp:RegularExpressionValidator>--%>
                                                        <asp:RegularExpressionValidator ID="reg1" runat="server" ValidationExpression="^(0|[1-9]\d*)(\,\d{1,2})?$" ControlToValidate="txtPesoLordo" ValidationGroup="A" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Enter Peso Lordo Decimal Value">*</asp:RegularExpressionValidator>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Peso Lordo" SetFocusOnError="true" ControlToValidate="txtPesoLordo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtPesoLordo" MaxLength="7" runat="server" class="form-control"></asp:TextBox>

                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-1">
                                                        <label class="control-label" for="inputBasicFirstName"></label>
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgComposozioneNew" runat="server" ImageUrl="images/edt.jpg" OnClick="imgComposozione_Click" Style="margin-bottom: -36px;" />
                                                        <asp:ImageButton ID="imgCompoClose" runat="server" ImageUrl="images/del.gif" OnClick="imgCompoClose_Click" Visible="false" Style="margin-bottom: -36px;" />
                                                    </div>
                                                    <div class="form-group col-sm-11">
                                                        <label class="control-label" for="inputBasicFirstName">Composizione</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Composozione" SetFocusOnError="true" ControlToValidate="txtComposizione" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtComposizione" runat="server" class="form-control txtComposizione" Visible="false">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Select Composozione" SetFocusOnError="true" ControlToValidate="ddlComposizione" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlComposizione" runat="server" class="form-control" OnSelectedIndexChanged="ddlComposizione_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-1">
                                                        <label class="control-label" for="inputBasicFirstName"></label>
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgConserNew" runat="server" ImageUrl="images/edt.jpg" OnClick="imgConserNew_Click" Style="margin-bottom: -36px;" />
                                                        <asp:ImageButton ID="imgConserClose" runat="server" ImageUrl="images/del.gif" OnClick="imgConserClose_Click" Visible="false" Style="margin-bottom: -36px;" />
                                                    </div>
                                                    <div class="form-group col-sm-11">
                                                        <label class="control-label" for="inputBasicFirstName">Conservazione</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Conservazione" SetFocusOnError="true" ControlToValidate="txtConservazione" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtConservazione" runat="server" class="form-control txtConservazione" Visible="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Select Conservazione" SetFocusOnError="true" ControlToValidate="ddlConservazione" ForeColor="Red" InitialValue="">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlConservazione" runat="server" OnSelectedIndexChanged="ddlConservazione_SelectedIndexChanged" AutoPostBack="true" class="form-control"></asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-1">
                                                        <label class="control-label" for="inputBasicFirstName"></label>
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgBloodNew" runat="server" ImageUrl="images/edt.jpg" OnClick="imgBloodNew_Click" Style="margin-bottom: -36px;" />
                                                        <asp:ImageButton ID="imgBloodClose" runat="server" ImageUrl="images/del.gif" OnClick="imgBloodClose_Click" Visible="false" Style="margin-bottom: -36px;" />
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicFirstName">Gruppo Sanguigno</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Gruppo Sangue" SetFocusOnError="true" ControlToValidate="ddlGruppoSangue" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Select Conservazione" SetFocusOnError="true" ControlToValidate="ddlGruppoSangue" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlGruppoSangue" runat="server" class="form-control" OnSelectedIndexChanged="ddlGruppoSangue_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtGruppoSangue" runat="server" class="form-control" Visible="false">
                                                        </asp:TextBox>
                                                    </div>

                                                    <div class="form-group col-sm-5">
                                                        <label class="control-label" for="inputBasicFirstName">Data Scadenza</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Data Scadenza" SetFocusOnError="true" ControlToValidate="txtDataScadenza" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtDataScadenza" Type="Date" Operator="DataTypeCheck" ValidationGroup="A" SetFocusOnError="true" ErrorMessage="Enter valid Data Scadenza" Display="Dynamic" ForeColor="Red">*</asp:CompareValidator>
                                                        <asp:TextBox ID="txtDataScadenza" runat="server" class="form-control myDate">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Note su Etichetta</label>
                                                        &nbsp;
                                           
                                                         <asp:TextBox ID="txtNote" runat="server" class="form-control" MaxLength="50">
                                                         </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-bottom: 30px; padding: 10px; border: 2px solid #22b14c;">
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <h4 style="background: none repeat scroll 0% 0% #22b14c; color: white; margin: -10px; padding: 10px;">Riferimenti del Donatore</h4>
                                                    </div>


                                                </div>
                                                <%-- <div class="row">
                                                    <div class="form-group col-sm-9">
                                                        
                                                    </div>
                                                   <div class="form-group col-sm-3">
                                                        <asp:Button ID="btnDonatorNuovo" runat="server" Text="Nuovo" CssClass="btn btn-primary floatRight" OnClick="btnDonatorNuovo_Click" />
                                                    </div>

                                                </div>--%>
                                                <div class="row">
                                                    <asp:HiddenField ID="hdNewDonor" runat="server" Value="0" />
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Denominazione</label>
                                                        &nbsp;
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Specie Animale" SetFocusOnError="true" ControlToValidate="ddlDenominazione" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                                                        <asp:DropDownList ID="ddlDenominazione" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDenominazione_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%--<div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Denominazione</label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Enter Denominazione" SetFocusOnError="true" ControlToValidate="txtDenominazione" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                        <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtDenominazione"
                                                            placeholder="Denominazione" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>--%>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Indirzzo</label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Enter Donator Indirzzo" SetFocusOnError="true" ControlToValidate="txtDIndirzzo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtDIndirzzo"
                                                            placeholder="Indirzzo" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Localita</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="A"
                                                runat="server" ErrorMessage="Enter Donator Localita" SetFocusOnError="true" ControlToValidate="txtDLocalita" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" MaxLength="30" class="form-control" ID="txtDLocalita" placeholder="Localita"
                                                            autocomplete="off" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Provincia</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Donator Provincia" ControlToValidate="txtDProvincia" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtDProvincia" name="inputEmail"
                                                            placeholder="Provincia" autocomplete="off" MaxLength="5" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Cap</label>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Integer" Operator="DataTypeCheck"
                                                            ControlToValidate="txtDCap" ErrorMessage="Enter valid Mobile">*</asp:CompareValidator>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Donator Cap" ControlToValidate="txtDCap" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtDCap" name="inputEmail"
                                                            placeholder="Cap" autocomplete="off" MaxLength="5" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Telefono</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Telefone" ControlToValidate="txtDTelefono" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator3" runat="server" Type="Integer" Operator="DataTypeCheck"
                                                            ControlToValidate="txtDTelefono" ErrorMessage="Enter valid Mobile">*</asp:CompareValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtDTelefono" name="inputEmail"
                                                            placeholder="Telefono" autocomplete="off" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">E_mail</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Email Id" ControlToValidate="txtDE_mail" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDE_mail"
                                                            ErrorMessage="Invalid Email Address" ValidationGroup="A" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            Text="*" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtDE_mail" name="inputEmail"
                                                            placeholder="E_mail" autocomplete="off" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label" for="inputBasicPassword">Note</label>
                                                    <asp:TextBox type="text" runat="server" class="form-control"
                                                        ID="txtNoteU" name="txtNote"
                                                        placeholder="Note" autocomplete="off" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="pnlAnimalList" runat="server">
                                                <div class="row" style="margin-bottom: 30px; padding: 10px; border: 2px solid rgb(249, 104, 104);">
                                                    <div class="row">
                                                        <div class="form-group col-sm-12">
                                                            <h4 style="background: none repeat scroll 0% 0% rgb(249, 104, 104); color: white; margin: -10px; padding: 10px;">Riferimenti Animale</h4>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-sm-12" style="text-align: right">
                                                            <asp:Button ID="btnAnimalNuovo" runat="server" Text="Nuovo" Visible="false" CssClass="btn btn-primary" OnClick="btnAnimalNuovo_Click" />
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="form-group col-sm-12">
                                                            <asp:GridView ID="grdAnimals" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="Progressive" AutoGenerateColumns="false" data-mobile-responsive="true" border="1" Style="width: 100%" OnRowCommand="grdAnimals_RowCommand" OnRowDataBound="grdAnimals_RowDataBound" OnSelectedIndexChanged="grdAnimals_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnDelete" ImageUrl="images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');" Text="Delete" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="SpeciesName" HeaderText="Specie Animale" ItemStyle-CssClass="width20" />
                                                                    <asp:BoundField DataField="AnimalName" HeaderText="Nome" ItemStyle-CssClass="width20" />
                                                                    <asp:BoundField DataField="AnimalWeight" HeaderText="Peso" ItemStyle-CssClass="textright width10" />
                                                                    <asp:BoundField DataField="AgeAnimal" HeaderText="Età" ItemStyle-CssClass="textright width10" />
                                                                    <asp:BoundField DataField="DescrizioneAllegato" HeaderText="Allegati" ItemStyle-CssClass="width30" />
                                                                    <asp:BoundField DataField="DataInserimento" HeaderText="Data Ult. Agg." ItemStyle-CssClass="width10" />
                                                                    <asp:BoundField DataField="AttachmentCount" HeaderText="Numero Allegati" ItemStyle-CssClass="textright width10" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    Dati non disponibili
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlAddAnimal" runat="server" Visible="false">

                                                <div class="row" style="margin-bottom: 30px; padding: 10px; border: 2px solid rgb(249, 104, 104);">
                                                    <div class="row">
                                                        <div class="form-group col-sm-12">
                                                            <h4 style="background: none repeat scroll 0% 0% rgb(249, 104, 104); color: white; margin: -10px; padding: 10px;">Riferimenti Animale</h4>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-sm-6">
                                                        </div>
                                                        <div class="form-group col-sm-2">
                                                            <asp:Button ID="btnBackAnimalList" Text="Indietro" CausesValidation="false" CssClass="btn btn-danger floatRight" runat="server" OnClick="btnBackAnimalList_Click" />
                                                        </div>
                                                        <div class="form-group col-sm-2">

                                                            <a id="popup" runat="server" class="btn btn-primary floatRight" data-toggle="modal" data-target="#myModal">Allegati</a>
                                                        </div>
                                                        <div class="form-group col-sm-2" runat="server" visible="false">
                                                            <asp:Button ID="btnAddNewAnimal" Text="Registra" ValidationGroup="D" CssClass="btn btn-primary floatRight" runat="server" OnClick="btnAddNewAnimal_Click" />
                                                        </div>

                                                    </div>
                                                    <div class="row yell">
                                                        <div class="form-group col-sm-3">
                                                            <label class="control-label" for="inputBasicFirstName">Specie Animale</label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="D" runat="server"
                                                                ErrorMessage="Select Animale Specie" SetFocusOnError="true" ControlToValidate="ddlAnimaleSpecie" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                            <asp:DropDownList ID="ddlAnimaleSpecie" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-sm-3">
                                                            <label class="control-label" for="inputBasicFirstName">Nome</label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="D" runat="server"
                                                                ErrorMessage="Enter Anumale Name" SetFocusOnError="true" ControlToValidate="txtAnimalName" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txtAnimalName" MaxLength="30" runat="server" class="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-sm-3">
                                                            <label class="control-label" for="inputBasicFirstName">Peso(Kg)</label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" ValidationGroup="D" runat="server"
                                                                ErrorMessage="Enter Peso" SetFocusOnError="true" ControlToValidate="txtPeso" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[0-9]+([,][0-9]{1,2})?$" ControlToValidate="txtPeso" ValidationGroup="D" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Enter Peso Lordo Decimal Value">*</asp:RegularExpressionValidator>


                                                            <asp:TextBox ID="txtPeso" runat="server" MaxLength="5" class="form-control" placeholder="PESO(Kg)"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-sm-3">
                                                            <label class="control-label" for="inputBasicFirstName">Età (Anni,Mesi)</label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" ValidationGroup="D" runat="server"
                                                                ErrorMessage="Enter Age" SetFocusOnError="true" ControlToValidate="txtAge" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[0-9]+([,][0-9]{1,2})?$" ControlToValidate="txtAge" ValidationGroup="D" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Enter valid Età (Anni,Mesi)">*</asp:RegularExpressionValidator>--%>

                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="^[0-9]?[0-9]?(\,[0-9][0-9]?)?" ControlToValidate="txtAge" ValidationGroup="D" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Enter valid Età (Anni,Mesi)">*</asp:RegularExpressionValidator>

                                                            <asp:TextBox ID="txtAge" runat="server" MaxLength="5" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="pnlAttchmnt" runat="server" Visible="false">
                                                        <div class="form-group col-sm-12">
                                                            <asp:GridView ID="gvAttachments" ShowHeaderWhenEmpty="true" runat="server" Width="100%" DataKeyNames="ProgressiveAttachment" AutoGenerateColumns="false" OnRowCommand="gvAttachments_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdAttchProgressive" runat="server" Value='<%# Eval("ProgressiveAttachment") %>' />
                                                                            <asp:ImageButton ID="btnDelete" ImageUrl="images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');" Text="Delete" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="DescrizioneAllegato" HeaderText="Descrizione Allegato" />
                                                                    <asp:BoundField DataField="DataInserimentoa" HeaderText="Data Inserimento" />
                                                                    <asp:TemplateField HeaderText="Nome e formato">
                                                                        <ItemTemplate>
                                                                            <a style="text-decoration: underline" onclick="window.open('<%# ResolveUrl("~/Uploads/Donor_Registry_Attachments/"+DataBinder.Eval(Container.DataItem, "LinkAttachment"))%>')" href="#"><%# DataBinder.Eval(Container.DataItem, "LinkAttachment")%> </a>


                                                                        </ItemTemplate>


                                                                    </asp:TemplateField>
                                                                    <%--                                                                    <asp:BoundField DataField="LinkAttachment" HeaderText="Nome e formato" />--%>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    Nessun record trovato
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="row">
                                                        <div class="form-group col-sm-6">
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="D" ForeColor="Red" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <%--<div class="row">
                                                <div class="form-group col-sm-6">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="A" ForeColor="Red" />

                                                </div>
                                            </div>--%>


                                        </div>
                                    </div>
                                    <div class="clearfix hidden-xs"></div>


                                    <!-- End Example Horizontal Form -->
                                </div>
                            </div>
                        </div>





                        <asp:HiddenField ID="hdProgressive" runat="server" />
                        <asp:HiddenField ID="hdMode" runat="server" Value="S" />



                    </div>

                    <!-- Panel Inline Form -->

                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modal" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 40%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h2 class="modal-title" id="myModalLabel" align="center">Allegati</h2>

                        </div>
                        <div class="modal-body">
                            <div class="page-content">
                                <div class="panel">
                                    <div class="panel-body container-fluid" style="padding-top: 0">
                                        <div class="row row-lg">
                                            <div class="col-sm-12">
                                                <!-- Example Basic Form -->
                                                <div class="example-wrap">

                                                    <div class="example">
                                                        <div class="row">
                                                            <div class="form-group col-sm-12">
                                                                <label class="control-label" for="inputBasicFirstName">Descrizione Allegato </label>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" ValidationGroup="F" runat="server"
                                                                    ErrorMessage="Enter Descrizione Allegato" SetFocusOnError="true" ControlToValidate="txtDescrizioneAllegato" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                <asp:TextBox type="text" runat="server" class="form-control" MaxLength="50" ID="txtDescrizioneAllegato"
                                                                    placeholder="Descrizione Allegato" autocomplete="off"></asp:TextBox>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="form-group col-sm-12">
                                                                <label class="control-label" for="inputBasicFirstName">Data Inserimento</label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" ValidationGroup="F" runat="server"
                                                                    ErrorMessage="Enter Data Inserimento" SetFocusOnError="true" ControlToValidate="txtDataInserimento" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                <asp:TextBox type="text" runat="server" class="form-control myDate" MaxLength="50" ID="txtDataInserimento"
                                                                    placeholder="Data Inserimento" autocomplete="off"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-sm-12">


                                                                <label class="control-label" for="inputBasicFirstName">Allega file</label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" ValidationGroup="F" runat="server"
                                                                    ErrorMessage="Allega file" SetFocusOnError="true" ControlToValidate="upFile" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                <asp:FileUpload ID="upFile" runat="server" />

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-sm-12">
                                                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="F" Text="Aggiorna" CssClass="btn btn-primary floatRight" OnClick="btnSubmit_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>




            <div class="modal" id="modalAlert" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 40%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h2 class="modal-title" id="H1" align="center">Attenzione !!!!</h2>

                        </div>
                        <div class="modal-body">
                            <div class="page-content">
                                <div class="panel1">
                                    <div class="panel-body container-fluid" style="padding-top: 0">
                                        <div class="row row-lg">
                                            <div class="col-sm-12">
                                                <!-- Example Basic Form -->
                                                <div class="example-wrap">

                                                    <div class="example1">
                                                        <div class="row">
                                                            <div class="form-group col-sm-12">
                                                                <label class="control-label" for="inputBasicFirstName" style="color: red !important">il donatore non ha animali con la stessa specie animale per il prelievo</label>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            $("[src*=plus]").live("click", function () {
                $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
                $(this).attr("src", "images/minus.png");
            });
            $("[src*=minus]").live("click", function () {
                $(this).attr("src", "images/plus.png");
                $(this).closest("tr").next().remove();
            });
        </script>








        <script type="text/javascript">
            //On UpdatePanel Refresh
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        SetAutoComplete();
                    }
                });
            };
            $(function () {
                SetAutoComplete();
            });

            function SetAutoComplete() {

                $(".txtTipoPreparato").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "frmMovimentoPrelievi.aspx/GetTipo",
                            data: "{'Tipo':'" + request.term + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                //alert("No Match");
                            }
                        });
                    }
                });
                $(".txtComposizione").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "frmMovimentoPrelievi.aspx/GetComposizione",
                            data: "{'Composizione':'" + request.term + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                //alert("No Match");
                            }
                        });
                    }
                });
                $(".txtConservazione").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "frmMovimentoPrelievi.aspx/GetConservazione",
                            data: "{'Composizione':'" + request.term + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                //alert("No Match");
                            }
                        });
                    }
                });
            }
        </script>

        <script type="text/javascript">
            function openModal() {
                $('#modalAlert').modal('show');
            }
        </script>
    </div>
</asp:Content>

