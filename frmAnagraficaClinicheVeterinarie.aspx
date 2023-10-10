<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmAnagraficaClinicheVeterinarie.aspx.cs" Inherits="frmAnagraficaClinicheVeterinarie" %>

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
    <div class="page animsition">
        <div class="page-header">
            <h1 class="page-title">Cliniche Veterinarie</h1>
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
                                    <h3>Elenco Cliniche Veterinarie</h3>
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


                                            <asp:GridView ID="grdData" ShowHeaderWhenEmpty="true" DataKeyNames="CodID" AutoGenerateColumns="false" runat="server" data-mobile-responsive="true" border="1" Style="width: 100%" OnSorting="grdData_Sorting" PageSize="50" AllowSorting="true" AllowPaging="true" OnPageIndexChanging="grdData_PageIndexChanging" OnRowCommand="grdData_RowDataCommand" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged">
                                                <Columns>
                                                    <%-- <asp:TemplateField HeaderText="Sr. No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" ImageUrl="images/del.gif" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DEL" name="btnDelete" OnClientClick="JavaScript:return confirm('Do you want to delete this record');" Text="Delete" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/images/edt.jpg" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Edit" name="btnEdit" Text="Edit" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="CodID" HeaderText="Cod. Int" SortExpression="CodID" />
                                                    <asp:TemplateField HeaderText="Denominazione" SortExpression="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Address" HeaderText="Indirizzo" SortExpression="Address" />
                                                    <asp:BoundField DataField="resort" HeaderText="Localita" SortExpression="resort" />
                                                    <asp:BoundField DataField="province" HeaderText="PV" SortExpression="province" />
                                                    <asp:BoundField DataField="PostalCode" HeaderText="Cap" SortExpression="PostalCode" />
                                                    <asp:BoundField DataField="Phone" HeaderText="Telefono" SortExpression="Phone" />
                                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                    <%-- <asp:BoundField DataField="Note" HeaderText="Note" />--%>
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
                                        <h4>Anagrafica Clinica Veterinaria</h4>
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
                                                    <label class="control-label" for="inputBasicFirstName">Denominazione</label>
                                                    &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Denominazione" SetFocusOnError="true" ControlToValidate="txtDenominazione" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" MaxLength="50" ID="txtDenominazione"
                                                        placeholder="Denominazione" autocomplete="off"></asp:TextBox>

                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-12">
                                                    <label class="control-label" for="inputBasicFirstName">Indirizzo</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server"
                                                        ErrorMessage="Enter Indirizzo" SetFocusOnError="true" ControlToValidate="txtIndirizzo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtIndirizzo"
                                                        placeholder="Indirizzo" autocomplete="off"></asp:TextBox>
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
                                            <div class="form-group">
                                                <label class="control-label" for="inputBasicPassword">Referente</label>
                                                <asp:TextBox type="text" runat="server" class="form-control"
                                                    ID="txtReferente" name="txtReferente"
                                                    placeholder="Referente" autocomplete="off" MaxLength="30"></asp:TextBox>
                                            </div>
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
                                        <h4>Anagrafica Clinica Veterinaria</h4>
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
                                                    <label class="control-label" for="inputBasicFirstName">Indirizzo</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="B" runat="server"
                                                        ErrorMessage="Enter Indirizzo" SetFocusOnError="true" ControlToValidate="txtIndirizzoU" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:TextBox type="text" runat="server" class="form-control" MaxLength="40" ID="txtIndirizzoU"
                                                        placeholder="Indirizzo" autocomplete="off"></asp:TextBox>
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
                                            <div class="form-group">
                                                <label class="control-label" for="inputBasicPassword">Referente</label>
                                                <asp:TextBox type="text" runat="server" class="form-control"
                                                    ID="txtReferenteU" name="txtReferente"
                                                    placeholder="Referente" autocomplete="off" MaxLength="30"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="inputBasicPassword">Note</label>
                                                <asp:TextBox type="text" runat="server" class="form-control"
                                                    ID="txtNoteU" name="txtNote"
                                                    placeholder="Note" autocomplete="off" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                            </div>

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
                </ContentTemplate>
            </asp:UpdatePanel>



        </div>

        <!-- Panel Inline Form -->


    </div>
</asp:Content>

