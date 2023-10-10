<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMasterPage.master" AutoEventWireup="true" CodeFile="UpdatePassword.aspx.cs" Inherits="UpdatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/Login.js"></script>
<script  type="text/javascript">
    function checkit() {
       
        var FirstName = document.getElementById('<%=txtOldPassword.ClientID %>').value;
        var LastName = document.getElementById('<%=txtNewPasswrod.ClientID %>').value;
        var MailId = document.getElementById('<%=txtConfirmPassword.ClientID %>').value;
        var old = document.getElementById('<%=hdold.ClientID %>').value;
        var newpass = document.getElementById('<%=hdnew.ClientID %>').value;
        var conpass = document.getElementById('<%=hdconfirm.ClientID %>').value;
        var hdpass = document.getElementById('<%=hdpass.ClientID %>').value;
        //			alert(FirstName);


        if (FirstName == "") {
            alert("Enter Old Password");
            return false;
        }
        else if (LastName == "") {
            alert("Enter New Password");
            return false;
        }
        else if (MailId == "") {
            alert("Enter Confirm Password");
            return false;
        }
        else {
            //      alert("demo");


            document.getElementById('<%=txtOldPassword.ClientID %>').value = "**********************";
            document.getElementById('<%=txtConfirmPassword.ClientID %>').value = "**********************";
            document.getElementById('<%=txtNewPasswrod.ClientID %>').value = "**********************";


            document.getElementById('<%=hdold.ClientID %>').value = randomString() + FirstName.substring(0, 3) + randomString() + FirstName.substring(3, 6) + randomString() + FirstName.substring(6);

            
            document.getElementById('<%=hdnew.ClientID %>').value = randomString() + LastName.substring(0, 3) + randomString() + LastName.substring(3, 6) + randomString() + LastName.substring(6);


            document.getElementById('<%=hdconfirm.ClientID %>').value = randomString() + MailId.substring(0, 3) + randomString() + MailId.substring(3, 6) + randomString() + MailId.substring(6);

        }
    }
</script> 
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
                                               <asp:HiddenField ID="hdold" runat="server" />
             <asp:HiddenField ID="hdnew" runat="server" />
              <asp:HiddenField ID="hdconfirm" runat="server" />
               <asp:HiddenField ID="hdpass" runat="server" />
                                            <div id="lblmsg" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                    
                             

                                    <div class="row">
                                        <div class="form-group col-sm-12">
                                            <label class="control-label" for="inputBasicFirstName">Old Password</label>
                                             &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Temporary Password" SetFocusOnError="true" ControlToValidate="txtOldPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        
                                            <asp:TextBox type="text" runat="server" TextMode="Password" class="form-control" ID="txtOldPassword"
                                                autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-12">
                                            <label class="control-label" for="inputBasicFirstName">New Password</label>
                                             &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter New Password" SetFocusOnError="true" ControlToValidate="txtNewPasswrod" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        
                                            <asp:TextBox type="text" runat="server" TextMode="Password" class="form-control" ID="txtNewPasswrod"
                                                autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-12">
                                            <label class="control-label" for="inputBasicFirstName">Confirm Password</label>
                                             &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter ConfirmPassword" SetFocusOnError="true" ControlToValidate="txtConfirmPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        
                                            <asp:TextBox type="text" runat="server" TextMode="Password" class="form-control" ID="txtConfirmPassword"
                                                autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6">
                                            <asp:Button ID="btnForgot" Text="Update Password"  OnClientClick="checkit() " ValidationGroup="A" CssClass="btn btn-primary floatRight" runat="server" OnClick="btnForgot_Click" />

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

