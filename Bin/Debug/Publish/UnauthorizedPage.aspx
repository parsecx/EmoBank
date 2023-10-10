﻿<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="UnauthorizedPage.aspx.cs" Inherits="UnauthorizedPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page animsition">
        <div class="page-header">
            <h1 class="page-title">Non sei autorizzato ad accedere a questa pagina</h1>
             <a class="pull-right" href="Login.aspx">Vai Torna alla Login</a>
        </div>
        <%--<div class="page-content">
      <div class="panel">
        <div class="panel-body container-fluid">
          <div class="row row-lg">
            <div class="col-sm-6">
              <!-- Example Basic Form -->
              <div class="example-wrap">
                <h4 class="example-title">Basic Form</h4>
                <div class="example">
                  <form autocomplete="off">
                    <div class="row">
                      <div class="form-group col-sm-6">
                        <label class="control-label" for="inputBasicFirstName">First Name</label>
                        <input type="text" class="form-control" id="inputBasicFirstName" name="inputFirstName"
                        placeholder="First Name" autocomplete="off" />
                      </div>
                      <div class="form-group col-sm-6">
                        <label class="control-label" for="inputBasicLastName">Last Name</label>
                        <input type="text" class="form-control" id="inputBasicLastName" name="inputLastName"
                        placeholder="Last Name" autocomplete="off" />
                      </div>
                    </div>
                    <div class="form-group">
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
                    </div>
                    <div class="form-group">
                      <label class="control-label" for="inputBasicEmail">Email Address</label>
                      <input type="email" class="form-control" id="inputBasicEmail" name="inputEmail"
                      placeholder="Email Address" autocomplete="off" />
                    </div>
                    <div class="form-group">
                      <label class="control-label" for="inputBasicPassword">Password</label>
                      <input type="password" class="form-control" id="inputBasicPassword" name="inputPassword"
                      placeholder="Password" autocomplete="off" />
                    </div>
                    <div class="form-group">
                      <div class="checkbox-custom checkbox-default">
                        <input type="checkbox" id="inputBasicRemember" name="inputCheckbox" checked autocomplete="off"
                        />
                        <label for="inputBasicRemember">Remember Me</label>
                      </div>
                    </div>
                    <div class="form-group">
                      <button type="button" class="btn btn-primary">Sign Up</button>
                    </div>
                  </form>
                </div>
              </div>
              <!-- End Example Basic Form -->
            </div>

            <div class="col-sm-6" style="display:none">
              <!-- Example Basic Form Without Label -->
              <div class="example-wrap">
                <h4 class="example-title">Basic Form Without Label</h4>
                <div class="example">
                  <form class="">
                    <div class="row">
                      <div class="form-group col-sm-6">
                        <input type="text" class="form-control" name="firstname" placeholder="First Name"
                        autocomplete="off" />
                      </div>
                      <div class="form-group col-sm-6">
                        <input type="text" class="form-control" name="lastname" placeholder="Last Name"
                        autocomplete="off" />
                      </div>
                    </div>
                    <div class="form-group">
                      <div class="radio-custom radio-default radio-inline">
                        <input type="radio" id="inputLabelMale" name="inputRadioGender" />
                        <label for="inputLabelMale">Male</label>
                      </div>
                      <div class="radio-custom radio-default radio-inline">
                        <input type="radio" id="inputLabelFemale" name="inputRadioGender" checked />
                        <label for="inputLabelFemale">Female</label>
                      </div>
                    </div>
                    <div class="form-group">
                      <input type="email" class="form-control" name="email" placeholder="Email Address"
                      autocomplete="off" />
                    </div>
                    <div class="form-group">
                      <input type="password" class="form-control" name="password" placeholder="Password"
                      autocomplete="off" />
                    </div>
                    <div class="form-group">
                      <textarea class="form-control" placeholder="Briefly Describe Yourself"></textarea>
                    </div>
                    <div class="form-group">
                      <div class="checkbox-custom checkbox-default">
                        <input type="checkbox" id="inputCheckboxAgree" name="inputCheckboxesAgree" checked
                        autocomplete="off" />
                        <label for="inputCheckboxAgree">Agree Policy</label>
                      </div>
                    </div>
                    <div class="form-group">
                      <button type="button" class="btn btn-primary">Submit</button>
                    </div>
                  </form>
                </div>
              </div>
              <!-- End Example Basic Form Without Label -->
            </div>

            <div class="clearfix hidden-xs"></div>

  
              <!-- End Example Horizontal Form -->
            </div>
          </div>
        </div>
      </div>--%>

        <!-- Panel Inline Form -->

       
    </div>
</asp:Content>

