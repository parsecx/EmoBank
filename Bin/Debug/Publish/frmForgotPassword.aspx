﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMasterPage.master" AutoEventWireup="true" CodeFile="frmForgotPassword.aspx.cs" Inherits="User_frmForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                        <div class="form-group col-sm-12">
                                            <div id="lblmsg" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                   

                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <label class="control-label" for="inputBasicFirstName">Email</label>
                                            <asp:TextBox type="text" runat="server" class="form-control" ID="txtEmail"
                                                autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="form-group col-sm-6">
                                            <asp:Button ID="btnForgot" Text="Forgot Password" ValidationGroup="A" CssClass="btn btn-primary floatRight" runat="server" OnClick="btnForgot_Click" />

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


</asp:Content>

