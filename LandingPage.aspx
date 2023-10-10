<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="LandingPage.aspx.cs" Inherits="LandingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>

    <div class="page animsition">
        <div class="page-header">
            <h1 class="page-title"></h1>

        </div>
        <div class="page-content">
      <div class="panel margin-top-20" >
        <div class="panel-body container-fluid">
          <div class="row row-lg">
            <div class="col-sm-12">
              <!-- Example Basic Form -->
              <div class="example-wrap" style="text-align:center;margin-top:10px">
               
              
                  <asp:Image ID="imgLogo" Height="50%" Width="50%" ImageUrl="assets/images/logo.png" runat="server" />
                  

              </div>
              <!-- End Example Basic Form -->
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

