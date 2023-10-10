﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

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
  <%--  <link rel="shortcut icon" href="assets/images/favicon.ico">--%>

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
    <%--<link rel="stylesheet" href="assets/examples/css/forms/layouts.min.css?v2.2.0">--%>
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
                    <img class="brand-img" src="assets/images/logo.png"+ "?r=" + <%DateTime.Now.Ticks.ToString();%> alt="..." style="width: 339px; background: white none repeat scroll 0% 0%;">
                </div>
                <p>Accedi con il tuo account</p>

                 <div class="form-group">
                   <div id="lblMsg" runat="server"></div>
                   
                </div>

                <div class="form-group">
                    
                    <label style="margin-right: 290px;" for="inputName">Utente</label>
                    <input type="text" class="form-control" id="inputName" name="name" placeholder="ID utente" runat="server">
                </div>
                
                <div class="form-group">
                    <label style="margin-right: 265px;" for="inputPassword">Password</label>
                    <asp:TextBox TextMode="Password" class="form-control" ID="inputPassword" name="password"
                        placeholder="parola d'ordine" runat="server"></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                   <%-- <div class="checkbox-custom checkbox-inline checkbox-primary pull-left">
                        <input type="checkbox" id="inputCheckbox" name="remember">
                        <label for="inputCheckbox">Remember me</label>
                    </div>--%>
                    <%--<a class="pull-right" href="FrmForgetPassword.aspx">Ha dimenticato la password?</a>--%>
                </div>
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-block" Text="LOGIN" OnClick="btnLogin_Click" />

              <%--  <p>Still no account? Please go to <a href="FrmEmobank_Users.aspx">Register</a></p>--%>

                <footer class="page-copyright page-copyright-inverse">
                 <%--   <p>WEBSITE BY Vermadeveloper</p>--%>
                    <p>© 2016 EmoBank. All RIGHT RESERVED.</p>

                </footer>
            </div>
        </div>
        <!-- End Page -->

    </form>



    <script src="global/vendor/jquery/jquery.min.js"></script>
    <script src="global/vendor/bootstrap/bootstrap.min.js"></script>
    <script src="global/vendor/animsition/animsition.min.js"></script>
   <%-- <script src="global/vendor/asscroll/jquery-asScroll.min.js"></script>--%>
    <script src="global/vendor/mousewheel/jquery.mousewheel.min.js"></script>
 <%--   <script src="global/vendor/asscrollable/jquery.asScrollable.all.min.js"></script>--%>
    <script src="global/vendor/asscrollable/http%20_getbootstrapadmin.com_remark_global_vendor_asscrollable_jquery.asScrollable.all.min.js"></script>
    <script src="global/vendor/ashoverscroll/jquery-asHoverScroll.min.js"></script>

    <!-- Plugins -->
    <script src="global/vendor/switchery/switchery.min.js"></script>
    <script src="global/vendor/intro-js/intro.min.js"></script>
    <script src="global/vendor/screenfull/screenfull.min.js"></script>
    <script src="global/vendor/slidepanel/jquery-slidePanel.min.js"></script>

    <!-- Plugins For This Page -->
    <%--<script src="global/vendor/jquery-placeholder/jquery.placeholder.min.js"></script>--%>

    <!-- Scripts -->
    <script src="global/js/core.min.js"></script>
    <script src="assets/js/site.min.js"></script>

    <script src="assets/js/sections/menu.min.js"></script>
    <script src="assets/js/sections/menubar.min.js"></script>
    <script src="assets/js/sections/gridmenu.min.js"></script>
    <script src="assets/js/sections/sidebar.min.js"></script>

    <script src="global/js/configs/config-colors.min.js"></script>
    <script src="assets/js/configs/config-tour.min.js"></script>

    <script src="global/js/components/asscrollable.min.js"></script>
    <script src="global/js/components/animsition.min.js"></script>
    <script src="global/js/components/slidepanel.min.js"></script>
    <script src="global/js/components/switchery.min.js"></script>

    <%--<script src="global/js/components/jquery-placeholder.min.js"></script>--%>
      <script src="global/js/components/jquery.placeholder.min.js"></script>

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
