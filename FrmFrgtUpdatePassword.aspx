﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmFrgtUpdatePassword.aspx.cs"
    Inherits="FrmFrgtUpdatePassword" %>

<!DOCTYPE html>
<html class="no-js css-menubar" lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta name="description" content="bootstrap admin template">
    <meta name="author" content="">

    <title>EMobank</title>

    <link rel="apple-touch-icon" href="assets/images/apple-touch-icon.png">
    <link rel="shortcut icon" href="assets/images/favicon.ico">

    <!-- Stylesheets -->
    <link rel="stylesheet" href="global/css/bootstrap.min.css?v2.2.0">
    <link rel="stylesheet" href="global/css/bootstrap-extend.min.css?v2.2.0">
    <link rel="stylesheet" href="assets/css/site.min.css?v2.2.0">

    <!-- Skin tools (demo site only) -->

    <!-- Plugins -->
    <link rel="stylesheet" href="global/vendor/animsition/animsition.min.css?v2.2.0">
    <link rel="stylesheet" href="global/vendor/asscrollable/asScrollable.min.css?v2.2.0">
    <link rel="stylesheet" href="global/vendor/switchery/switchery.min.css?v2.2.0">
    <link rel="stylesheet" href="global/vendor/intro-js/introjs.min.css?v2.2.0">
    <link rel="stylesheet" href="global/vendor/slidepanel/slidePanel.min.css?v2.2.0">
    <link rel="stylesheet" href="global/vendor/flag-icon-css/flag-icon.min.css?v2.2.0">

    <!-- Page -->
    <link rel="stylesheet" href="assets/examples/css/forms/layouts.min.css?v2.2.0">
    <link rel="stylesheet" href="assets/examples/css/pages/login.min.css?v2.2.0">
    <!-- Fonts -->
    <link rel="stylesheet" href="global/fonts/web-icons/web-icons.min.css?v2.2.0">
    <link rel="stylesheet" href="global/fonts/brand-icons/brand-icons.min.css?v2.2.0">
    <link rel='stylesheet' href='http://fonts.googleapis.com/css?family=Roboto:300,400,500,300italic'>

    <link rel="stylesheet" href="css/message.css">
    <!--[if lt IE 9]>
    <script src="global/vendor/html5shiv/html5shiv.min.js"></script>
    <![endif]-->

    <!--[if lt IE 10]>
    <script src="global/vendor/media-match/media.match.min.js"></script>
    <script src="global/vendor/respond/respond.min.js"></script>
    <![endif]-->

    <!-- Scripts -->
    <script src="global/vendor/modernizr/modernizr.min.js"></script>
    <script src="global/vendor/breakpoints/breakpoints.min.js"></script>
    <script>
        Breakpoints();
    </script>
    <script src="js/Login.js"></script>
    <script type="text/javascript">
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
            

            document.getElementById('<%=hdold.ClientID %>').value = randomString() + FirstName.substring(0, 3) + randomString() + FirstName.substring(3, 6) + randomString() + FirstName.substring(6);


            document.getElementById('<%=hdnew.ClientID %>').value = randomString() + LastName.substring(0, 3) + randomString() + LastName.substring(3, 6) + randomString() + LastName.substring(6);


            document.getElementById('<%=hdconfirm.ClientID %>').value = randomString() + MailId.substring(0, 3) + randomString() + MailId.substring(3, 6) + randomString() + MailId.substring(6);



           <%-- document.getElementById('<%=txtOldPassword.ClientID %>').value = "**********************";--%>
            document.getElementById('<%=txtOldPassword.ClientID %>').value = "12345678";
            document.getElementById('<%=txtConfirmPassword.ClientID %>').value = "**********************";
            document.getElementById('<%=txtNewPasswrod.ClientID %>').value = "**********************";


        }
}
    </script>
    <style>
        .page-login form {
            width: auto;
        }
    </style>
</head>
<body class="page-login layout-full page-dark">
    <!--[if lt IE 8]>
        <p class="browserupgrade">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> to improve your experience.</p>
    <![endif]-->


    <!-- Page -->
    <form id="form2" runat="server">
        <div class="page animsition vertical-align text-center" data-animsition-in="fade-in"
            data-animsition-out="fade-out">
            <div class="page-content vertical-align-middle">
                <div class="brand">
                    <img class="brand-img" src="assets/images/logo.png" alt="..." style="width: 339px; background: white none repeat scroll 0% 0%; border: 2px solid gray;">
                </div>
                <h4 style="color: #fff">Cambia la password</h4>

                <div class="row" style="width:365px">
                    <asp:HiddenField ID="hdold" runat="server" />
                    <asp:HiddenField ID="hdnew" runat="server" />
                    <asp:HiddenField ID="hdconfirm" runat="server" />
                    <asp:HiddenField ID="hdpass" runat="server" />
                    <asp:HiddenField ID="txtOldPassword" runat="server"  Value="12345678" />
                    <div id="lblmsg" runat="server" style="width:100%"></div>

                </div>

                <div id="divMain" runat="server" visible="false">
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <label class="control-label" for="inputBasicFirstName">E-mail</label>
                            <asp:TextBox type="text" ReadOnly="true" runat="server" class="form-control" ID="txtEmail"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="display:none">
                        <div class="form-group col-sm-12">
                            <label class="control-label" for="inputBasicFirstName">password temporanea</label>
                            &nbsp;
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter Temporary Password" SetFocusOnError="true" ControlToValidate="txtOldPassword" ForeColor="Red">*</asp:RequiredFieldValidator>--%>

                            <asp:TextBox type="text" runat="server" TextMode="Password" Text="12345678" class="form-control" ID="txtOldPassword1" placeHolder='password temporanea'
                                autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <label class="control-label" for="inputBasicFirstName">nuova password</label>
                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter New Password" SetFocusOnError="true" ControlToValidate="txtNewPasswrod" ForeColor="Red">*</asp:RequiredFieldValidator>

                            <asp:TextBox type="text" runat="server" TextMode="Password" class="form-control" ID="txtNewPasswrod" placeHolder='nuova password'
                                autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <label class="control-label" for="inputBasicFirstName">conferma password</label>
                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="A" runat="server"
                                                ErrorMessage="Enter ConfirmPassword" SetFocusOnError="true" ControlToValidate="txtConfirmPassword" ForeColor="Red">*</asp:RequiredFieldValidator>

                            <asp:TextBox type="text" runat="server" TextMode="Password" class="form-control" ID="txtConfirmPassword" placeHolder='conferma password'
                                autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <asp:Button ID="btnForgot" Text="Aggiornamento password" OnClientClick="checkit() " ValidationGroup="A" CssClass="btn btn-primary btn-block" runat="server" OnClick="btnForgot_Click" />

                        </div>
                    </div>
                </div>
                <p><a href="Login.aspx">Vai alla pagina di login</a></p>
              <%--  <footer class="page-copyright page-copyright-inverse">

                    <p>© 2016 Emobank. All RIGHT RESERVED.</p>

                </footer>--%>
            </div>
        </div>
        <!-- End Page -->

    </form>



    <script src="global/vendor/jquery/jquery.min.js"></script>
    <script src="global/vendor/bootstrap/bootstrap.min.js"></script>
    <script src="global/vendor/animsition/animsition.min.js"></script>
    <script src="global/vendor/asscroll/jquery-asScroll.min.js"></script>
    <script src="global/vendor/mousewheel/jquery.mousewheel.min.js"></script>
    <script src="global/vendor/asscrollable/jquery.asScrollable.all.min.js"></script>
    <script src="global/vendor/ashoverscroll/jquery-asHoverScroll.min.js"></script>

    <!-- Plugins -->
    <script src="global/vendor/switchery/switchery.min.js"></script>
    <script src="global/vendor/intro-js/intro.min.js"></script>
    <script src="global/vendor/screenfull/screenfull.min.js"></script>
    <script src="global/vendor/slidepanel/jquery-slidePanel.min.js"></script>

    <!-- Plugins For This Page -->
    <script src="global/vendor/jquery-placeholder/jquery.placeholder.min.js"></script>

    <!-- Scripts -->
    <script src="global/js/core.min.js"></script>
    <script src="assets/js/site.min.js"></script>

    <script src="assets/js/sections/menu.min.js"></script>
    <script src="assets/js/sections/menubar.min.js"></script>
    <script src="assets/js/sections/gridmenu.min.js"></script>
   <%-- asscrollable.min.js--%>

    <script src="global/js/configs/config-colors.min.js"></script>
    <script src="assets/js/configs/config-tour.min.js"></script>

    <script src="global/js/components/asscrollable.min.js"></script>
    <script src="global/js/components/animsition.min.js"></script>
    <script src="global/js/components/slidepanel.min.js"></script>
    <script src="global/js/components/switchery.min.js"></script>

    <script src="global/js/components/jquery-placeholder.min.js"></script>


    <script>
        (function (document, window, $) {
            'use strict';

            var Site = window.Site;
            $(document).ready(function () {
                Site.run();
            });
        })(document, window, jQuery);
    </script>


    <!-- Google Analytics -->
</body>

</html>
