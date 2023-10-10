﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master"
    AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="frmCaricoFrigoEmoteca.aspx.cs" Inherits="frmCaricoFrigoEmoteca" %>

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
            text-align: left;
        }

        .Head-table {
            border-bottom: 0 none !important;
            margin-bottom: -1px !important;
        }

            .Head-table select {
                margin: 10px 0px;
                width: 100% !important;
            }

        .itemstyle {
            padding: 0;
            width: 12%;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page animsition">
        <div class="page-header">
            <h1 class="page-title">Carico Frigo Emoteca</h1>
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
                </Triggers>
                <ContentTemplate>
                    <div class="panel" id="List" runat="server">
                        <div class="panel-body">
                            <div class="row row-lg">
                                <div class="col-sm-12">
                                    <%--<h3>Elenco Prelievo</h3>--%>
                                    <!-- Example Table From Data -->
                                    <div class="example-wrap">
                                        <div class="col-sm-6">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                                            <p style="font-size: 16px; padding: 6px;">
                                                <b>Seleziona tipo </b>
                                                <asp:DropDownList runat="server" ID="ddlSeleziona" OnSelectedIndexChanged="ddlSeleziona_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="NUOVI" Value="NUOVI" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="AGGIORNATI" Value="AGGIORNATI"></asp:ListItem>
                                                    <asp:ListItem Text="TUTTI" Value="TUTTI"></asp:ListItem>
                                                </asp:DropDownList>
                                            </p>
                                        </div>
                                        <div class="col-sm-2">

                                            <asp:LinkButton ID="btnStampa" OnClientClick="window.document.forms[0].target='_blank';" runat="server" OnClick="btnStampa_Click" Text="Stampa" CssClass="btn btn-block btn-danger" />

                                            <p class="col-sm-3">
                                                <asp:Button ID="btnNuovo" runat="server" Text="Nuovo" CssClass="btn btn-primary" OnClick="btnNuovo_Click" Visible="false" />

                                            </p>
                                        </div>

                                        <div class="clearfix"></div>
                                        <%-- <p>Transform table from an existing data.</p>--%>
                                        <div class="example">

                                            <table data-mobile-responsive="true" border="1" style="width: 100%; margin-bottom: 10px" class="Head-table">
                                                <tr>
                                                    <th style="width: 9%; padding: 0px;">
                                                        <asp:LinkButton ID="lnk1" runat="server" Text=" Cod. Int" OnClick="lnk_Click" CommandArgument="CodID"></asp:LinkButton>
                                                    </th>
                                                    <th style="width: 9%; padding: 0px;">
                                                        <asp:LinkButton ID="lnkProtocolNumber" runat="server" Text="Protocollo" OnClick="lnk_Click" CommandArgument="ProtocolNumber"></asp:LinkButton>
                                                        <th style="width: 3%; padding: 0px;">PR</th>

                                                        <th style="width: 10%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkDateTimeDrawing" runat="server" Text="Data Prelievo" OnClick="lnk_Click" CommandArgument="DateTimeDrawing"></asp:LinkButton>
                                                        </th>
                                                        <th style="width: 24%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkDescription" runat="server" Text="Punto Prelievo" OnClick="lnk_Click" CommandArgument="Description"></asp:LinkButton></th>
                                                        <th style="width: 10%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkAninale" runat="server" Text="Aninale" OnClick="lnk_Click" CommandArgument="SpeciesName"></asp:LinkButton></th>
                                                        <th style="width: 10%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkAnimalGroupBlood" runat="server" Text="Gruppo Sanguigno" OnClick="lnk_Click" CommandArgument="AnimalGroupBlood"></asp:LinkButton></th>
                                                        <th style="width: 24%; padding: 0px;">
                                                            <asp:LinkButton ID="lnkName" runat="server" Text="Donatore" OnClick="lnk_Click" CommandArgument="Name"></asp:LinkButton></th>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        <asp:DropDownList ID="ddlCod1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </th>
                                                    <th>
                                                        <asp:DropDownList ID="ddlProtocol1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true">
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
                                                        <asp:DropDownList ID="ddlNamee1" runat="server" class="form-control" OnSelectedIndexChanged="ddlCod_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList></th>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="grdData" ShowHeaderWhenEmpty="false" DataKeyNames="CodID" AutoGenerateColumns="false" runat="server" data-mobile-responsive="true" border="1" Style="width: 100%" OnSorting="grdData_Sorting" PageSize="500" AllowSorting="true" AllowPaging="false" OnPageIndexChanging="grdData_PageIndexChanging" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged" ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" ImageUrl="images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');" Text="Delete" runat="server" />
                                                            <asp:HiddenField ID="hdUserLoadBloodBankRefrigerator" runat="server" Value='<%# Eval("UserLoadBloodBankRefrigerator") %>' />
                                                            <asp:HiddenField ID="hdDateLoadBloodBankRefrigerator" runat="server" Value='<%# Eval("DateLoadBloodBankRefrigerator") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="CodID" HeaderText="Cod. Int" SortExpression="CodID" ItemStyle-Width="9%" ItemStyle-CssClass="textright" />
                                                    <asp:TemplateField HeaderText="Protocollo" SortExpression="ProtocolNumber" ItemStyle-Width="9%" ItemStyle-CssClass="textright">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("ProtocolNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Progressive" HeaderText="PR" SortExpression="Progressive" ItemStyle-Width="3%" ItemStyle-CssClass="textright" />

                                                    <asp:BoundField DataField="DateTimeDrawing" HeaderText="Data Prelievo " SortExpression="DateTimeDrawing" ItemStyle-Width="10%" />
                                                    <asp:BoundField DataField="Description" HeaderText="Punto Prelievo" SortExpression="Description" ItemStyle-Width="24%" />
                                                    <asp:BoundField DataField="SpeciesName" HeaderText="Aninale" SortExpression="SpeciesName" ItemStyle-Width="10%" />
                                                    <asp:BoundField DataField="AnimalGroupBlood" HeaderText="Gruppo Sanguigno" SortExpression="AnimalGroupBlood" ItemStyle-Width="10%" />
                                                    <asp:BoundField DataField="Name" HeaderText="Donatore" SortExpression="Name" ItemStyle-Width="24%" />
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

                                            <div class="row" id="msg" runat="server" visible="false">
                                                <div class="form-group col-sm-12">
                                                    <div id="lblmsg" runat="server">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-8">
                                                </div>
                                                <div class="form-group col-sm-2">
                                                    <asp:Button ID="btnBack" Text="Indietro" CssClass="btn btn-block btn-danger floatRight" runat="server" OnClick="btnBack_Click" />

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
                                                        <asp:TextBox type="text" runat="server" MaxLength="10" class="form-control" ID="txtDatePrelievo"
                                                            autocomplete="off" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-sm-3">
                                                        <label class="control-label" for="inputBasicFirstName">C.Interno</label>
                                                        <asp:TextBox type="text" runat="server" MaxLength="9" class="form-control" ID="txtCInterno"
                                                            autocomplete="off" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-sm-3">
                                                        <label class="control-label" for="inputBasicFirstName">Protocollo</label>
                                                        <asp:TextBox type="text" runat="server" MaxLength="9" class="form-control" ID="txtProtocollo"
                                                            autocomplete="off" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-sm-3">
                                                        <label class="control-label" for="inputBasicFirstName">Progressivo</label>
                                                        <asp:TextBox type="text" runat="server" MaxLength="9" class="form-control" ID="txtProgressivo"
                                                            autocomplete="off" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Punto Prelievo</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Denominazione" SetFocusOnError="true" ControlToValidate="ddlPuntoPrelievo" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlPuntoPrelievo" disabled="disabled" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPuntoPrelievo_SelectedIndexChanged"></asp:DropDownList>
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
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="ACC"
                                                            runat="server" SetFocusOnError="true" ErrorMessage="Enter Referente" ControlToValidate="txtReferente" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                                                        <asp:DropDownList ID="ddlSpecieAnimale" runat="server" class="form-control" disabled="disabled">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-sm-4">
                                                        <label class="control-label" for="inputBasicFirstName">Tipo Preparato</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Tipo Preparato" SetFocusOnError="true" ControlToValidate="ddlTipoPreparato" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlTipoPreparato" runat="server" class="form-control" disabled="disabled"></asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-sm-4">
                                                        <label class="control-label" for="inputBasicFirstName">Peso Lordo</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Peso Lordo" SetFocusOnError="true" ControlToValidate="txtPesoLordo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtPesoLordo" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Composizione</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Composozione" SetFocusOnError="true" ControlToValidate="txtComposizione" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtComposizione" runat="server" class="form-control" ReadOnly="true">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Conservazione</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Conservazione" SetFocusOnError="true" ControlToValidate="txtConservazione" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtConservazione" runat="server" class="form-control" ReadOnly="true">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-7">
                                                        <label class="control-label" for="inputBasicFirstName">Gruppo Sangue</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Gruppo Sangue" SetFocusOnError="true" ControlToValidate="txtGruppoSangue" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtGruppoSangue" runat="server" class="form-control" ReadOnly="true">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-sm-5">
                                                        <label class="control-label" for="inputBasicFirstName">Data Scadenza</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Data Scadenza" SetFocusOnError="true" ControlToValidate="txtDataScadenza" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtDataScadenza" runat="server" class="form-control" disabled="disabled">
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
                                                <div class="row" id="d" runat="server" visible="false">
                                                    <div class="form-group col-sm-9">
                                                        <asp:HiddenField ID="hdNewDonor" runat="server" Value="0" />
                                                    </div>
                                                    <div class="form-group col-sm-3">
                                                        <asp:Button ID="btnDonatorNuovo" runat="server" Text="Nuovo" CssClass="btn btn-primary floatRight" OnClick="btnDonatorNuovo_Click" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Denominazione</label>
                                                        &nbsp;
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Specie Animale" SetFocusOnError="true" ControlToValidate="ddlDenominazione" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                                                        <asp:DropDownList ID="ddlDenominazione" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDenominazione_SelectedIndexChanged" disabled="disabled">
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
                                            </div>


                                            <div id="pnlAnimalList" runat="server">
                                                <div class="row" style="margin-bottom: 30px; padding: 10px; border: 2px solid rgb(249, 104, 104);">
                                                    <div class="row">
                                                        <div class="form-group col-sm-12">
                                                            <h4 style="background: none repeat scroll 0% 0% rgb(249, 104, 104); color: white; margin: -10px; padding: 10px;">Riferimenti Animale</h4>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-sm-12">
                                                            <asp:GridView ID="grdAnimals" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="Progressive" AutoGenerateColumns="false" data-mobile-responsive="true" border="1" Style="width: 100%">
                                                                <Columns>
                                                                    <%--     <asp:TemplateField HeaderText="" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnDelete" ImageUrl="~/images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');" Text="Delete" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
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


                                            <div class="row" style="margin-bottom: 30px; padding: 10px; border: 2px solid #3f48cc;">
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <h4 style="background: none repeat scroll 0% 0% #3f48cc; color: white; margin: -10px; padding: 10px;">Carico Frigo Emoteca</h4>
                                                    </div>


                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Banca</label>
                                                        &nbsp;
                                                       <asp:DropDownList ID="ddlDenominazioneB" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDenominazioneB_SelectedIndexChanged">
                                                       </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Indirzzo</label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="A" runat="server"
                                                            ErrorMessage="Enter Donator Indirzzo" SetFocusOnError="true" ControlToValidate="txtIndirzzoB" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtIndirzzoB"
                                                            placeholder="Indirzzo" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <label class="control-label" for="inputBasicFirstName">Localita</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="A"
                                                runat="server" ErrorMessage="Enter Donator Localita" SetFocusOnError="true" ControlToValidate="txtLocalitaB" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" MaxLength="30" class="form-control" ID="txtLocalitaB" placeholder="Localita"
                                                            autocomplete="off" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Provincia</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Donator Provincia" ControlToValidate="txtProvinciaB" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtProvinciaB" name="inputEmail"
                                                            placeholder="Provincia" autocomplete="off" MaxLength="5" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Cap</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Donator Cap" ControlToValidate="txtCapB" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtCapB" name="inputEmail"
                                                            placeholder="Cap" autocomplete="off" MaxLength="5" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Utente</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Utente" ControlToValidate="txtUtente" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control" ID="txtUtente" name="inputEmail"
                                                            placeholder="Utente" autocomplete="off" MaxLength="5"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicEmail">Data</label>
                                                        &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Data" ControlToValidate="txtData" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox type="text" runat="server" class="form-control myDate" ID="txtData" name="inputEmail"
                                                            placeholder="Data" autocomplete="off" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="B" ForeColor="Red" />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix hidden-xs"></div>


                                <!-- End Example Horizontal Form -->
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>



        </div>

        <!-- Panel Inline Form -->


    </div>
</asp:Content>

