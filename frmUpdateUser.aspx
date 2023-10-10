﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="frmUpdateUser.aspx.cs" Inherits="frmUpdateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .floatRight {
            float:right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="page animsition">
        <div class="page-header">
            <h1 class="page-title">Anagrafica Utente</h1>
        </div>
        <div class="page-content">
            <div class="panel">
                <div class="panel-body container-fluid">
                    <div class="row row-lg">
                        <div class="col-sm-12">
                            <!-- Example Basic Form -->
                            <div class="example-wrap">

                                <div class="example">
                                   
                                    <div class="row">
                                        <div class="form-group col-sm-9">
                                           <div id="lblmsg" runat="server">
    </div>
                                        </div>
                                         <div class="form-group col-sm-1">
                                              <asp:Button ID="btnBack" Text="Back" CssClass="btn btn-danger" runat="server" OnClick="btnBack_Click" />
                                        </div>
                                        <div class="form-group col-sm-2">
                                            <asp:Button ID="btnUpdate" Text="Update Registra" ValidationGroup="A" CssClass="btn btn-primary floatRight" runat="server" OnClick="btnUpdate_Click" />
                                        </div>
                                       
                                    </div>

                                    <div class="row" runat="server" id="CodicInterno">
                                       
                                    </div>
                                    <div class="row">
                                         <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicFirstName">Codice Interno</label>
                                            <asp:TextBox type="text" runat="server" MaxLength="9" class="form-control" ID="txtCodice"
                                                autocomplete="off"  ReadOnly="true" > </asp:TextBox>

                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicFirstName">User-ID</label>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="req1" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter User Id" ControlToValidate="txtUserId" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:TextBox type="text" runat="server" class="form-control" ID="txtUserId"
                                                placeholder="User Id"  ReadOnly="true" autocomplete="off" MaxLength="20"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-sm-6" id="t" runat="server" visible="false">
                                            <label class="control-label" for="inputBasicLastName">PSW</label>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter PSW" SetFocusOnError="true" ControlToValidate="txtPSW" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:TextBox type="text" TextMode="Password" MaxLength="20" runat="server" class="form-control" ID="txtPSW"
                                                placeholder="PSW" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-12">
                                            <label class="control-label" for="inputBasicFirstName">Denominazione</label>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Denominazione" SetFocusOnError="true" ControlToValidate="txtDenominazione" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtDenominazione"
                                                placeholder="Denominazione" autocomplete="off"></asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicFirstName">Tipo Utente</label>
                                            <asp:DropDownList ID="ddltipo" class="form-control" runat="server">
                                                <%-- <asp:ListItem Text="User" Value="U"></asp:ListItem>
                                                <asp:ListItem Text="External User" Value="E"></asp:ListItem>
                                                <asp:ListItem Text="Administrator" Value="A"></asp:ListItem>--%>
                                            </asp:DropDownList>

                                        </div>

                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicFirstName">Scadenza Mesi</label>
                                            <asp:DropDownList ID="ddlScadenza" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlScadenza_SelectedIndexChanged">
                                                <%--<asp:ListItem Text="One Month" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="12 Months" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="No Expiary" Value="99"></asp:ListItem>--%>
                                            </asp:DropDownList>

                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicFirstName">Data Scadenza</label>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="A"
                                                runat="server" ErrorMessage="Enter Data Scadenza" SetFocusOnError="true" ControlToValidate="txtDataScadenza" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:TextBox type="text" runat="server" class="form-control" ID="txtDataScadenza"
                                                autocomplete="off"  ReadOnly="true"></asp:TextBox>

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
                                            <label class="control-label" for="inputBasicEmail">Telefono</label>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Telefone" ControlToValidate="txtTelefono" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="c1" runat="server" Type="Integer" Operator="DataTypeCheck"
                                                ControlToValidate="txtTelefono" ErrorMessage="Enter valid Mobile">*</asp:CompareValidator>
                                            <asp:TextBox type="text" runat="server" class="form-control" ID="txtTelefono" name="inputEmail"
                                                placeholder="Telefono" autocomplete="off" MaxLength="30"></asp:TextBox>
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
                                                placeholder="E_mail" autocomplete="off" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label" for="inputBasicPassword">Note</label>
                                        <asp:TextBox type="text" runat="server" class="form-control"
                                            ID="txtNote" name="txtNote"
                                            placeholder="Note" autocomplete="off" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                    </div>



                                </div>
                            </div>
                            <!-- End Example Basic Form -->
                            <div class="example-wrap">
                                <h4 class="example-title">Autorizzazioni Utente</h4>
                                <div class="example">

                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicEmail">Gestione Utenti e Accessi</label>
                                            <asp:CheckBoxList ID="chkGestione" runat="server" class="form-control">
                                                <asp:ListItem Text="Gestione Utenti e Accessi" Value="1"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicEmail">Gestione Anagrafiche</label>
                                            <asp:CheckBoxList ID="chkGestioneAnagrafiche" RepeatDirection="Horizontal" runat="server" class="form-control">
                                                <asp:ListItem Text="Banca Sangue / Emoderivati" Value="1"></asp:ListItem>

                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicEmail"></label>
                                            <asp:CheckBoxList ID="ChkBchkAmbulatori" RepeatDirection="Horizontal" runat="server" class="form-control">

                                                <asp:ListItem Text="Ambulatori Punti Prelievo" Value="2"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicEmail">Gestione Prelievi</label>
                                            <asp:CheckBoxList ID="chkMivimenti" RepeatDirection="Horizontal" runat="server" class="form-control">
                                                <asp:ListItem Text="Movimenti Prelievi" Value="1"></asp:ListItem>

                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicEmail"></label>
                                            <asp:CheckBoxList ID="chkAnalisi" RepeatDirection="Horizontal" runat="server" class="form-control">

                                                <asp:ListItem Text="Analisi Statistiche Prelievi" Value="2"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicEmail">Gestione Cliniche Veterinarie</label>
                                            <asp:CheckBoxList ID="chkAnagrafica" RepeatDirection="Horizontal" runat="server" class="form-control">
                                                <asp:ListItem Text="Anagrafica Cliniche" Value="1"></asp:ListItem>

                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicEmail"></label>
                                            <asp:CheckBoxList ID="chkMovimentoR" RepeatDirection="Horizontal" runat="server" class="form-control">

                                                <asp:ListItem Text="Movimento Richieste Cliniche" Value="2"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicEmail"></label>
                                            <asp:CheckBoxList ID="chkAnalisiC" RepeatDirection="Horizontal" runat="server" class="form-control">
                                                <asp:ListItem Text="Analisi Consegne Cliniche" Value="1"></asp:ListItem>

                                            </asp:CheckBoxList>
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
                        </div>


                        <div class="clearfix hidden-xs"></div>


                        <!-- End Example Horizontal Form -->
                    </div>
                </div>
            </div>
        </div>

        <!-- Panel Inline Form -->


    </div>
</asp:Content>

