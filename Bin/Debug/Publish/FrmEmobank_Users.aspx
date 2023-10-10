﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master"
    AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="FrmEmobank_Users.aspx.cs" Inherits="LandingPage" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>

    <asp:UpdateProgress ID="upp1" runat="server" ClientIDMode="AutoID" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <div class="loader"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page animsition">
        <div class="page-header">
            <h1 class="page-title">Anagrafica Utente</h1>
        </div>
        <div class="page-content">
            <asp:UpdatePanel ID="up1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnStampa" />
                </Triggers>
                <ContentTemplate>
                    <div class="panel" id="divMainPage" runat="server" visible="false">
                        <div class="panel-body container-fluid">
                            <div class="row row-lg">
                                <div class="col-sm-12">
                                    <!-- Example Basic Form -->
                                    <div class="example-wrap">
                                        <div class="example">

                                            <div class="row">

                                                <div class="form-group col-sm-9 col-xs-12">
                                                </div>
                                                <div class="col-sm-2 form-group" style="float: right">

                                                    <asp:LinkButton ID="btnAnnulla" runat="server" OnClick="btnAnnulla_Click" Text="Annulla"
                                                        CssClass="btn btn-block btn-danger" />

                                                </div>
                                                <div class="col-sm-1 form-group" style="float: right">

                                                    <asp:Button ID="btnRegister" Text="Registra" ValidationGroup="A" CssClass="btn btn-primary floatRight"
                                                        runat="server" OnClick="btnRegister_Click" />
                                                </div>

                                            </div>
                                            <div class="row" runat="server" id="CodicInterno" visible="false">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicFirstName">
                                                        Codice Interno</label>
                                                    <asp:TextBox type="text" runat="server" MaxLength="9" class="form-control" ID="txtCodice"
                                                        autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicFirstName">
                                                        User-ID</label>
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator ID="req1" ValidationGroup="A" runat="server" ErrorMessage="Enter User Id"
                                                        ControlToValidate="txtUserId" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtUserId" placeholder="User Id"
                                                        autocomplete="off" MaxLength="20"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicLastName">
                                                        PSW</label>
                                                    &nbsp;
                                                    <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter PSW" SetFocusOnError="true"  ControlToValidate="txtPSW" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                                    <asp:TextBox type="text" TextMode="Password" MaxLength="20" runat="server"
                                                        class="form-control" ID="txtPSW" Text="12345678" placeholder="12345678" ReadOnly="true" autocomplete="off"></asp:TextBox>
                                             
                                                <div class="form-group col-sm-1" style="float:right; margin-bottom: 0;margin-top: -30px;">

                                                    <asp:ImageButton ID="img1" runat="server" ImageUrl="images/reset.png" OnClick="img1_Click" Style="width: 25px;" ToolTip="Reset Psw" />
                                                </div>
												   </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-12">
                                                    <label class="control-label" for="inputBasicFirstName">
                                                        Denominazione</label>
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server"
                                                        ErrorMessage="Enter Denominazione" SetFocusOnError="true" ControlToValidate="txtDenominazione"
                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtDenominazione"
                                                        placeholder="Denominazione" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicFirstName">
                                                        Tipo Utente</label>
                                                    <asp:DropDownList ID="ddltipo" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltipo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-sm-6" id="div1" runat="server" visible="false">
                                                    <label class="control-label" for="inputBasicFirstName">
                                                        Chiave Collegata</label>
                                                    <asp:DropDownList ID="ddlChiave" class="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicFirstName">
                                                        Data Scadenza</label>
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="A" runat="server"
                                                        ErrorMessage="Enter Data Scadenza" SetFocusOnError="true" ControlToValidate="txtDataScadenza"
                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtDataScadenza"
                                                        autocomplete="off" disabled="disabled"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicFirstName">
                                                        Scadenza Mesi</label>
                                                    <asp:DropDownList ID="ddlScadenza" class="form-control" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlScadenza_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <%-- <div class="form-group">
                                            <label class="control-label">Gender</label>
                                            <div>
                                                <div class="radio-custom radio-default radio-inline">
                                                    <input type="radio" id="inputBasicMale" name="inputGender" />
                                                    <label for="inputBasicMale">Male</label>
                                                </div>
                                                <div class="radio-custom radio-default radio-inline">
                                                    <input type="radio" id="inputBasicFemale" name="inputGender" checked />
                                                    <label for="inputBasicFemale">Female</label>
                                                </div>
                                            </div>
                                        </div>--%>
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicEmail">
                                                        Telefono</label>
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="A" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Enter Telefone" ControlToValidate="txtTelefono"
                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="c1" runat="server" Type="Integer" Operator="DataTypeCheck"
                                                        ControlToValidate="txtTelefono" ErrorMessage="Enter valid Mobile">*</asp:CompareValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtTelefono" name="inputEmail"
                                                        placeholder="Telefono" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicEmail">
                                                        E_mail</label>
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="A" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Enter Email Id" ControlToValidate="txtE_mail"
                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="EmailIDValidation" runat="server" ControlToValidate="txtE_mail"
                                                        ErrorMessage="Invalid Email Address" ValidationGroup="A" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Text="*" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RegularExpressionValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtE_mail" name="inputEmail"
                                                        placeholder="E_mail" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="inputBasicPassword">
                                                    Note</label>
                                                <asp:TextBox type="text" runat="server" class="form-control" ID="txtNote" name="txtNote"
                                                    placeholder="Note" autocomplete="off" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Example Basic Form -->
                                    <asp:Panel ID="pnl" runat="server" Visible="false">
                                        <div class="example-wrap">
                                            <h4 class="example-title" style="background: none repeat scroll 0% 0% rgb(63, 72, 204); color: white; margin: -10px; padding: 10px;">Autorizzazioni Utente</h4>
                                            <div class="example">
                                                <div class="row" style="border-bottom: 2px solid; margin: 0px 0px 20px;">
                                                    <div class="form-group col-sm-6 col-xs-6">
                                                        <label class="control-label" for="inputBasicEmail">
                                                            Gestione Utenti e Accessi</label>
                                                        <asp:CheckBoxList ID="chkGestione" runat="server" class="form-control">
                                                            <asp:ListItem Text="Gestione Utenti e Accessi" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="row" style="border-bottom: 2px solid; margin: 0px 0px 20px;">
                                                    <div class="form-group col-sm-6 col-xs-6">
                                                        <label class="control-label" for="inputBasicEmail">
                                                            Gestione Anagrafiche</label>
                                                        <asp:CheckBoxList ID="chkGestioneAnagrafiche" RepeatDirection="Horizontal" runat="server"
                                                            class="form-control">
                                                            <asp:ListItem Text="Banca Sangue / Emoderivati" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div class="form-group col-sm-6 col-xs-6">
                                                        <label class="control-label" for="inputBasicEmail">
                                                        </label>
                                                        <asp:CheckBoxList ID="ChkBchkAmbulatori" RepeatDirection="Horizontal" runat="server"
                                                            class="form-control">
                                                            <asp:ListItem Text="Ambulatori Punti Prelievo" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                     <div class="form-group col-sm-6 col-xs-6">
                                                       
                                                        <asp:CheckBoxList ID="ChkAnagraficaDonatori" RepeatDirection="Horizontal" runat="server"
                                                            class="form-control">
                                                            <asp:ListItem Text="Anagrafica Donatori" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>

                                                </div>
                                                <div class="row" style="border-bottom: 2px solid; margin: 0px 0px 20px;">
                                                    <label class="control-label" for="inputBasicEmail" style="float: left; width: 100%; padding-left: 17px;">
                                                        Gestione Prelievi</label>
                                                    <div class="form-group col-sm-6 col-xs-6" >

                                                        <asp:CheckBoxList ID="chkMivimenti" RepeatDirection="Horizontal" runat="server" class="form-control">
                                                            <asp:ListItem Text="Movimenti Prelievi" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div class="form-group col-sm-6 col-xs-6">

                                                        <asp:CheckBoxList ID="chkLoadBloodRrefrigerator" RepeatDirection="Horizontal" runat="server" class="form-control">
                                                            <asp:ListItem Text="Carico Frigo Emoteca" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>

                                                    <div class="form-group col-sm-6 col-xs-6">

                                                        <asp:CheckBoxList ID="chkAnalisi" RepeatDirection="Horizontal" runat="server" class="form-control">
                                                            <asp:ListItem Text="Analisi Statistiche Prelievi" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 0px 0px 20px;">
                                                    <label class="control-label" for="inputBasicEmail" style="float: left; width: 100%; padding-left: 17px;">
                                                        Gestione Cliniche Veterinarie</label>
                                                    <div class="form-group col-sm-6  col-xs-6"> 

                                                        <asp:CheckBoxList ID="chkAnagrafica" RepeatDirection="Horizontal" runat="server"
                                                            class="form-control">
                                                            <asp:ListItem Text="Anagrafica Cliniche" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div class="form-group col-sm-6 col-xs-6">

                                                        <asp:CheckBoxList ID="chkMovimentoR" RepeatDirection="Horizontal" runat="server"
                                                            class="form-control">
                                                            <asp:ListItem Text="Movimento Richieste Cliniche" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>

                                                    <div class="form-group col-sm-6 col-xs-6" style="display: none">

                                                        <asp:CheckBoxList ID="chkAnalisiC" RepeatDirection="Horizontal" runat="server" class="form-control">
                                                            <asp:ListItem Text="Analisi Consegne Cliniche" Value="1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div class="form-group col-sm-6 col-xs-6">
                                                    </div>

                                                </div>
                                                <%-- <div class="form-group">
                                            <div class="checkbox-custom checkbox-default">
                                                <input type="checkbox" id="inputBasicRemember" name="inputCheckbox" checked autocomplete="off" />
                                                <label for="inputBasicRemember">Remember Me</label>
                                            </div>
                                        </div>--%>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="A" ForeColor="Red" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="clearfix hidden-xs">
                                </div>
                                <!-- End Example Horizontal Form -->
                            </div>
                        </div>
                    </div>
                    <div class="panel" id="divList" runat="server" visible="true">
                        <div class="panel-body">
                            <div class="row row-lg">
                                <div class="col-sm-12">
                                    <!-- Example Table From Data -->
                                    <div class="example-wrap">
                                        <div class="row">
                                            <div class="form-group col-sm-12">
                                                <div id="lblmsg" runat="server">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-9 col-xs-12 form-group" style="float: right">
                                            <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                                            <p class="col-sm-6 col-xs-12" style="font-size: 16px; padding: 6px;">
                                                <b>Tipologia di utente </b>
                                                <asp:DropDownList runat="server" ID="ddl1" OnSelectedIndexChanged="ddl1_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="Tutti" Value="All"></asp:ListItem>
                                                    <asp:ListItem Text="Attivo" Value="A" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Inattivo" Value="I"></asp:ListItem>
                                                </asp:DropDownList>
                                            </p>
                                            <p class="col-sm-3 col-xs-6">
                                                <asp:LinkButton ID="btnStampa" runat="server" OnClientClick="window.document.forms[0].target='_blank';" OnClick="btnStampa_Click" Text="Stampa"
                                                    CssClass="btn btn-block btn-danger" />
                                            </p>
                                            <p class="col-sm-3 col-xs-6">
                                                <asp:LinkButton ID="btnNuovo" runat="server" OnClick="btnNuovo_Click" Text="Nuovo"
                                                    CssClass="btn btn-primary" />
                                                <%--                                        <a href="FrmEmobank_Users.aspx" class="btn btn-primary">Nuovo</a>--%>
                                            </p>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <%-- <p>Transform table from an existing data.</p>--%>
                                        <div class="example">
                                            <asp:GridView ID="grdData" ShowHeaderWhenEmpty="true" DataKeyNames="CodID" AutoGenerateColumns="false"
                                                runat="server" data-mobile-responsive="true" border="1" Style="width: 100%" OnSorting="grdData_Sorting"
                                                PageSize="50" AllowSorting="true" AllowPaging="true" OnPageIndexChanging="grdData_PageIndexChanging"
                                                OnRowCommand="grdData_RowDataCommand" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged">
                                                <Columns>
                                                    <%-- <asp:TemplateField HeaderText="Sr. No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" ImageUrl="images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');"
                                                                Text="Delete" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/images/edt.jpg" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Edit" name="btnEdit" Text="Edit" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="CodID" HeaderText="Cod. Int" SortExpression="CodID" />
                                                    <asp:BoundField DataField="UserID" HeaderText="User-Id" SortExpression="UserID" />
                                                    <asp:TemplateField HeaderText="Denominazione" SortExpression="NameUser">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("NameUser") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="UserTypeName" HeaderText="Tipo" SortExpression="UserTypeName" />
                                                    <asp:BoundField DataField="ExpiaryType" HeaderText="Tp.Scad" SortExpression="ExpiaryType" />
                                                    <asp:TemplateField HeaderText="Scad.PSW" SortExpression="PSWDeadline">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Convert.ToDateTime(Eval("PSWDeadline")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Phone" HeaderText="Telefono" SortExpression="Phone" />
                                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                    <asp:BoundField DataField="Note" HeaderText="Note" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- Panel Inline Form -->
    </div>
</asp:Content>
