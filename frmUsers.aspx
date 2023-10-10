﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmUsers.aspx.cs" Inherits="frmUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Chk(val) {
            location.href = "frmUpdateUser.aspx";

        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page animsition">

        <div class="page-header">
            <h1 class="page-title">Aggiornamento Utenti</h1>
        </div>
        <div class="page-content">
            <div class="panel">

                <div class="panel-body">
                    <div class="row row-lg">
                        <div class="col-sm-12">

                            <!-- Example Table From Data -->
                            <div class="example-wrap">
                                <div class="col-sm-6 form-group" style="float: right">
                                    <asp:Literal ID="ltrID" Visible="false" runat="server"></asp:Literal>
                                    <p class="col-sm-4" style="font-size: 16px; padding: 6px;">
                                        <b>Utenti </b>Attivi
                                            <asp:DropDownList runat="server" ID="ddl1" OnSelectedIndexChanged="ddl1_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Tutti" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Attivo" Value="A" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Inattivo" Value="I"></asp:ListItem>
                                            </asp:DropDownList>
                                    </p>
                                    <p class="col-sm-4">
                                        <asp:LinkButton ID="btnStampa" runat="server" OnClick="btnStampa_Click" Text="Stampa" CssClass="btn btn-block btn-danger" />
                                    </p>
                                    <p class="col-sm-4">
                                        <a href="FrmEmobank_Users.aspx" class="btn btn-primary">Nuovo</a>
                                    </p>

                                </div>
                                <div class="clearfix"></div>
                                <%-- <p>Transform table from an existing data.</p>--%>
                                <div class="example">
                                    <asp:GridView ID="grdData" ShowHeaderWhenEmpty="true" DataKeyNames="CodID" AutoGenerateColumns="false" runat="server" data-mobile-responsive="true" border="1" Style="width: 100%" OnSorting="grdData_Sorting" PageSize="50" AllowSorting="true" AllowPaging="true" OnPageIndexChanging="grdData_PageIndexChanging" OnRowCommand="grdData_RowDataCommand" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged">
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
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Convert.ToDateTime(Eval("PSWDeadline")).ToString("dd-MM-yyyy") %>'></asp:Label>
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
        </div>
    </div>




</asp:Content>

