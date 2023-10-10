﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmAnagraficaDonatori1.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="frmAnagraficaDonatori" %>

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

        .modal-backdrop {
            position: relative;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page animsition">
        <div class="page-header">
            <h1 class="page-title">Anagrafica Donatore</h1>
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
                     <asp:PostBackTrigger ControlID = "btnSubmit"   />
                    <%--<asp:AsyncPostBackTrigger ControlID="grdAnimals" EventName="RowDataBound" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="panel" id="List" runat="server">
                        <div class="panel-body">
                            <div class="row row-lg">
                                <div class="col-sm-12">

                                    <!-- Example Table From Data -->
                                    <div class="example-wrap">
                                        <div class="example">
                                            <div class="row">
                                                <div class="form-group col-sm-8" style="margin-right: 63px">
                                                    <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                                                </div>
                                                <div class="form-group col-sm-2">
                                                    <asp:LinkButton ID="btnStampa" OnClientClick="window.document.forms[0].target='_blank';" runat="server" OnClick="btnStampa_Click" Text="Stampa" CssClass="btn btn-block btn-danger" />
                                                </div>
                                                <div class="form-group col-sm-1">
                                                    <asp:Button ID="btnNuovo" runat="server" Text="Nuovo" CssClass="btn btn-primary" OnClick="btnNuovo_Click" />
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                            <%-- <p>Transform table from an existing data.</p>--%>


                                            <asp:GridView ID="grdData" ShowHeaderWhenEmpty="true" DataKeyNames="DCodID" AutoGenerateColumns="false" runat="server" data-mobile-responsive="true" border="1" Style="width: 100%" OnSorting="grdData_Sorting" PageSize="50" AllowSorting="true" AllowPaging="true" OnPageIndexChanging="grdData_PageIndexChanging" OnRowCommand="grdData_RowDataCommand" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged">
                                                <Columns>
                                                    <%-- <asp:TemplateField HeaderText="Sr. No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" ImageUrl="~/images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');" Text="Delete" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/images/edt.jpg" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Edit" name="btnEdit" Text="Edit" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="DCodID" HeaderText="Cod. Int" SortExpression="DCodID" />
                                                    <asp:TemplateField HeaderText="Denominazione" SortExpression="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Address" HeaderText="Indirizzo" SortExpression="Address" />
                                                    <asp:BoundField DataField="resort" HeaderText="Localita" SortExpression="resort" />
                                                    <asp:BoundField DataField="Provincie" HeaderText="PV" SortExpression="Provincie" />
                                                    <asp:BoundField DataField="PostalCode" HeaderText="Cap" SortExpression="PostalCode" />
                                                    <asp:BoundField DataField="Phone" HeaderText="Telefono" SortExpression="Phone" />
                                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                    <%--  <asp:BoundField DataField="Note" HeaderText="Note" />--%>
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
                                        <h4>Anagrafica Banca</h4>
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
                                                    <asp:Button ID="btnBack" Text="Indietro" CssClass="btn btn-block btn-danger" runat="server" OnClick="btnBack_Click" />

                                                </div>
                                                <div class="form-group col-sm-2">
                                                    <asp:Button ID="btnRegister" Text="Registra" ValidationGroup="A" CssClass="btn btn-primary" runat="server" OnClick="btnRegister_Click" />

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
                                                <div class="form-group col-sm-12">
                                                    <label class="control-label" for="inputBasicFirstName">Nominativo</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Denominazione" SetFocusOnError="true" ControlToValidate="txtDenominazione" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" MaxLength="50" ID="txtDenominazione"
                                                        placeholder="Denominazione" autocomplete="off"></asp:TextBox>

                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-12">
                                                    <label class="control-label" for="inputBasicFirstName">Indirzzo</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server"
                                                        ErrorMessage="Enter Indirzzo" SetFocusOnError="true" ControlToValidate="txtIndirzzo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtIndirzzo"
                                                        placeholder="Indirzzo" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-12">
                                                    <label class="control-label" for="inputBasicFirstName">Localita</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="A"
                                                runat="server" ErrorMessage="Enter Localita" SetFocusOnError="true" ControlToValidate="txtLocalita" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" MaxLength="30" class="form-control" ID="txtLocalita" placeholder="Localita"
                                                        autocomplete="off"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicEmail">Provincia</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Provincia" ControlToValidate="txtProvincia" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtProvincia" name="inputEmail"
                                                        placeholder="Provincia" autocomplete="off" MaxLength="5"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicEmail">Cap</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="A"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Cap" ControlToValidate="txtCap" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" Type="Integer" Operator="DataTypeCheck"
                                                        ControlToValidate="txtCap" ErrorMessage="Enter valid Cap">*</asp:CompareValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtCap" name="inputEmail"
                                                        placeholder="Cap" autocomplete="off" MaxLength="5"></asp:TextBox>
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
                                            <%--  <div class="form-group">
                                                <label class="control-label" for="inputBasicPassword">Referente</label>
                                                <asp:TextBox type="text" runat="server" class="form-control"
                                                    ID="txtReferente" name="txtReferente"
                                                    placeholder="Referente" autocomplete="off" MaxLength="30"></asp:TextBox>
                                            </div>--%>
                                            <div class="form-group">
                                                <label class="control-label" for="inputBasicPassword">Note</label>
                                                <asp:TextBox type="text" runat="server" class="form-control"
                                                    ID="txtNote" name="txtNote"
                                                    placeholder="Note" autocomplete="off" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                            </div>

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


                    <div class="panel" id="Update" runat="server" visible="false">
                        <div class="panel-body container-fluid">
                            <div class="row row-lg">
                                <div class="col-sm-12">
                                    <!-- Example Basic Form -->
                                    <div class="example-wrap">
                                        <h4>Anagrafica Banca</h4>
                                        <div class="example">
                                            <div class="row" id="msgU" runat="server" visible="false">
                                                <div class="form-group col-sm-12">
                                                    <div id="lblmsgU" runat="server">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-8">
                                                </div>
                                                <div class="form-group col-sm-1">
                                                    <asp:Button ID="btnBackUp" Text="Indietro" CssClass="btn btn-danger" runat="server" OnClick="btnBackUp_Click" />
                                                </div>
                                                <div class="form-group col-sm-3">
                                                    <asp:Button ID="btnUpdate" Text="Update Registra" ValidationGroup="A" CssClass="btn btn-primary floatRight" runat="server" OnClick="btnUpdate_Click" />
                                                </div>

                                            </div>

                                            <div class="row" runat="server" id="Div3">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicFirstName">Codice Interno</label>
                                                    <asp:TextBox type="text" runat="server" MaxLength="9" class="form-control" ID="txtCodiceU"
                                                        autocomplete="off" ReadOnly="true"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-12">
                                                    <label class="control-label" for="inputBasicFirstName">Denominazione</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="B" runat="server"
                                                ErrorMessage="Enter Denominazione" SetFocusOnError="true" ControlToValidate="txtDenominazioneU" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtDenominazioneU"
                                                        placeholder="Denominazione" autocomplete="off"></asp:TextBox>

                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-12">
                                                    <label class="control-label" for="inputBasicFirstName">Indirzzo</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="B" runat="server"
                                                        ErrorMessage="Enter Indirzzo" SetFocusOnError="true" ControlToValidate="txtIndirzzoU" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtIndirzzoU"
                                                        placeholder="Indirzzo" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-12">
                                                    <label class="control-label" for="inputBasicFirstName">Localita</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="B"
                                                runat="server" ErrorMessage="Enter Localita" SetFocusOnError="true" ControlToValidate="txtLocalitaU" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" MaxLength="30" class="form-control" ID="txtLocalitaU" placeholder="Localita"
                                                        autocomplete="off"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicEmail">Provincia</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="B"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Provincia" ControlToValidate="txtProvinciaU" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtProvinciaU" name="inputEmail"
                                                        placeholder="Provincia" autocomplete="off" MaxLength="5"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicEmail">Cap</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="B"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Cap" ControlToValidate="txtCapU" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Integer" Operator="DataTypeCheck"
                                                        ControlToValidate="txtCap" ErrorMessage="Enter valid Postal Code">*</asp:CompareValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtCapU" name="inputEmail"
                                                        placeholder="Cap" autocomplete="off" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicEmail">Telefono</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="B"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Telefone" ControlToValidate="txtTelefonoU" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" Type="Integer" Operator="DataTypeCheck"
                                                        ControlToValidate="txtTelefono" ErrorMessage="Enter valid Mobile">*</asp:CompareValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtTelefonoU" name="inputEmail"
                                                        placeholder="Telefono" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-sm-6">
                                                    <label class="control-label" for="inputBasicEmail">E_mail</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="B"
                                                runat="server" SetFocusOnError="true" ErrorMessage="Enter Email Id" ControlToValidate="txtE_mailU" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtE_mail"
                                                        ErrorMessage="Invalid Email Address" ValidationGroup="B" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Text="*" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RegularExpressionValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" ID="txtE_mailU" name="inputEmail"
                                                        placeholder="E_mail" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                </div>
                                            </div>
                                            <%-- <div class="form-group">
                                                <label class="control-label" for="inputBasicPassword">Referente</label>
                                                <asp:TextBox type="text" runat="server" class="form-control"
                                                    ID="txtReferenteU" name="txtReferente"
                                                    placeholder="Referente" autocomplete="off" MaxLength="30"></asp:TextBox>
                                            </div>--%>
                                            <div class="form-group">
                                                <label class="control-label" for="inputBasicPassword">Note</label>
                                                <asp:TextBox type="text" runat="server" class="form-control"
                                                    ID="txtNoteU" name="txtNote"
                                                    placeholder="Note" autocomplete="off" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                            </div>

                                            <div id="pnlAnimalList" runat="server">
                                                <div class="row">
                                                    <div class="form-group col-sm-11">
                                                        Riferimenti Animale
                                                    </div>
                                                    <div class="form-group col-sm-1">
                                                        <asp:Button ID="btnAnimalNuovo" runat="server" Text="Nuovo" CssClass="btn btn-primary" OnClick="btnAnimalNuovo_Click" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-12">
                                                        <asp:GridView ID="grdAnimals" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="Progressive" AutoGenerateColumns="false" data-mobile-responsive="true" border="1" Style="width: 100%" OnRowCommand="grdAnimals_RowCommand" OnRowDataBound="grdAnimals_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDelete" ImageUrl="~/images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');" Text="Delete" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img alt="" style="cursor: pointer" src="images/plus.png" width="20px" />

                                                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                            <div class="row" style="margin-top: 10px">
                                                                                <div class="form-group col-sm-10">
                                                                                    List Of Attachments
                                                                                </div>
                                                                                <div class="form-group col-sm-1">
                                                                                    <a class="btn btn-primary" data-toggle="modal" data-target="#myModal">Nuovo</a>     <%--<asp:Button ID="btnAttachmentNuovo" runat="server" Text="Nuovo" CssClass="btn btn-primary"   data-toggle="modal" data-target="#myModal" />--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="form-group col-sm-12">
                                                                                    <asp:GridView ID="gvOrders" runat="server" Width="99%">
                                                                                        <EmptyDataTemplate>
                                                                                            No Attachments
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="SpeciesName" HeaderText="Species Animale" />
                                                                <asp:BoundField DataField="AnimalName" HeaderText="Name" />
                                                                <asp:BoundField DataField="AnimalWeight" HeaderText="Peso" />
                                                                <asp:BoundField DataField="AgeAnimal" HeaderText="Age" />
                                                                <asp:BoundField DataField="DescrizioneAllegato" HeaderText="Allegati" />
                                                                <asp:BoundField DataField="DataInserimento" HeaderText="Data Ultimo Agg." />
                                                                <asp:BoundField DataField="AttachmentCount" HeaderText="Numero Allegati" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No Record Found
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>

                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlAddAnimal" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="form-group col-sm-8">
                                                    </div>
                                                    <div class="form-group col-sm-1">
                                                        <asp:Button ID="btnBackAnimalList" Text="Indietro" CssClass="btn btn-danger" runat="server" OnClick="btnBackAnimalList_Click" />
                                                    </div>
                                                    <div class="form-group col-sm-3">
                                                        <asp:Button ID="btnAddNewAnimal" Text="Add Animal" ValidationGroup="D" CssClass="btn btn-primary floatRight" runat="server" OnClick="btnAddNewAnimal_Click" />
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicFirstName">Specie Animale</label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="D" runat="server"
                                                            ErrorMessage="Select Animale Specie" SetFocusOnError="true" ControlToValidate="ddlAnimaleSpecie" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlAnimaleSpecie" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicFirstName">Nme Animale</label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="D" runat="server"
                                                            ErrorMessage="Enter Anumale Name" SetFocusOnError="true" ControlToValidate="txtAnimalName" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtAnimalName" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicFirstName">Peso</label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="D" runat="server"
                                                            ErrorMessage="Enter Peso" SetFocusOnError="true" ControlToValidate="txtPeso" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtPeso" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-sm-6">
                                                        <label class="control-label" for="inputBasicFirstName">Age</label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ValidationGroup="D" runat="server"
                                                            ErrorMessage="Enter Age" SetFocusOnError="true" ControlToValidate="txtAge" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtAge" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-sm-6">
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="D" ForeColor="Red" />

                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="row">
                                                <div class="form-group col-sm-6">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="B" ForeColor="Red" />

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


                    <div class="modal" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog" style="width: 40%">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h2 class="modal-title" id="myModalLabel" align="center">Ricerca Avanzata</h2>

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

                                                                        <asp:TextBox type="text" runat="server" class="form-control" MaxLength="50" ID="txtDescrizioneAllegato"
                                                                            placeholder="Descrizione Allegato" autocomplete="off"></asp:TextBox>

                                                                    </div>

                                                                </div>

                                                                <div class="row">
                                                                    <div class="form-group col-sm-12">
                                                                        <label class="control-label" for="inputBasicFirstName">Data Inserimento</label>

                                                                        <asp:TextBox type="text" runat="server" class="form-control myDate" MaxLength="50" ID="txtDataInserimento"
                                                                            placeholder="Data Inserimento" autocomplete="off"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-sm-12">
                                                                        <label class="control-label" for="inputBasicFirstName">Data Inserimento</label>

                                                                        <asp:FileUpload ID="upFile" runat="server" />

                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-sm-12">
                                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary floatRight" OnClick="btnSubmit_Click" />
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
                </ContentTemplate>
            </asp:UpdatePanel>



        </div>

        <!-- Panel Inline Form -->


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
</asp:Content>

