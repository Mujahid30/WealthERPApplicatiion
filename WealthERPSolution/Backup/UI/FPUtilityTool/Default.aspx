﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FPUtilityTool._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FP</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="Content/CSS/bootstrap-3.3.6.min.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>

    <script src="Scripts/bootstrap-3.3.6.min.js" type="text/javascript"></script>

    <link href="Content/CSS/commonStyle.css" rel="stylesheet" type="text/css" />
    <link href="Content/CSS/datepicker.css" rel="stylesheet" type="text/css" />
    <style>
        .invalid
        {
            outline: none;
            border: 0.5px solid red; /* create a BIG glow */
            box-shadow: 0px 0px 6px red;
            -moz-box-shadow: 0px 0px 6px red;
            -webkit-box-shadow: 0px 0px 6px red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" role="form">
    <nav class="navbar  navbar-default">
  <div class="container-fluid">
     <div class="navbar-header">
      <span class="navbar-brand"><img alt="Logo" class="img-responsive" src="Content/Images/1021_1189700.png"  /> </span>
    </div>
  </div>
</nav>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-body">
                <div id="Tabs" role="tabpanel">
                    <ul class="nav nav-tabs nav-justified">
                        <li><a data-toggle="tab" id="tbl" runat="server" aria-controls="signUp" href="#signUp">
                            <p class="lead">
                                New Investor</p>
                        </a></li>
                        <li><a data-toggle="tab" id="tbl1" runat="server" aria-controls="signIn" href="#signIn">
                            <p class="lead">
                                Existing Investor</p>
                        </a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="signUp" runat="server" class="tab-pane fade in">
                            <div class="well">
                                <div class="row">
                                    <div class="form-group col-sm-6">
                                        <label class="control-label col-sm-4" for="txtName">
                                            Name:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control required" placeholder="Enter your Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtName"
                                                ValidationGroup="btnsignUpsubmit" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label class="control-label col-sm-4" for="txtEmail">
                                            Email:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control required" placeholder="Enter Email Id"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtEmail"
                                                ValidationGroup="btnsignUpsubmit" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEmail"
                                                ErrorMessage="Please enter a valid EmailId" Display="Dynamic" runat="server"
                                                ValidationGroup="btnsignUpsubmit" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                CssClass="revPCG"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-6">
                                        <label class="control-label col-sm-4" for="txtMobNo">
                                            Mobile No:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMobNo" runat="server" CssClass="form-control required" MaxLength="10"
                                                placeholder="Enter Mobile No"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMobNo"
                                                ValidationGroup="btnsignUpsubmit" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                                ValidationGroup="btnsignUpsubmit" CssClass="text-danger" ErrorMessage="Please enter a valid Mobile No."
                                                ControlToValidate="txtMobNo" ValidationExpression="\d{10}">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label class="control-label col-sm-4" for="txtPan1">
                                            PAN:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPan1" runat="server" CssClass="form-control required" MaxLength="10"
                                                placeholder="Enter PAN"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPan1"
                                                ValidationGroup="btnsignUpsubmit" runat="server" />
                                            <asp:RegularExpressionValidator ID="revPan" runat="server" Display="Dynamic" ValidationGroup="btnsignUpsubmit"
                                                CssClass="text-danger" ErrorMessage="Please check PAN Format" ControlToValidate="txtPan1"
                                                ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-6">
                                        <label class="control-label col-sm-4" for="txtDob">
                                            Date of Birth:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDob" runat="server" data-date="" data-date-format="dd/mm/yyyy"
                                                CssClass="form-control required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtDob"
                                                ValidationGroup="btnsignUpsubmit" runat="server" />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDob"
                                                ErrorMessage="Enter valid Date" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6">
                                    </div>
                                </div>
                                <div class="row">
                                    <div id="asd" class="form-group col-sm-6">
                                        <asp:Label ID="Label1" runat="server" Visible="false" CssClass="ValidationMsg">Client already exists.<a data-toggle="tab" id="A1" runat="server" aria-controls="signIn" href="#signIn">Signin here</a></asp:Label>
                                        <asp:Label ID="lbllogedIn1" runat="server" Visible="false" Text="User already loged In."
                                            CssClass="ValidationMsg"></asp:Label>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-6 pull-right">
                                                <asp:Button ID="btnSignUpsubmit" runat="server" OnClick="btnsignUpsubmit_Click" Text="Submit"
                                                    ValidationGroup="btnsignUpsubmit" CssClass="btn btn-info" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="signIn" class="tab-pane fade">
                            <div class="well">
                                <div class="row">
                                    <div class="form-group col-sm-6">
                                        <label class="control-label col-sm-4" for="txtpan2">
                                            PAN:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtpan2" runat="server" CssClass="form-control required" MaxLength="10"
                                                placeholder="Enter PAN"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtpan2"
                                                ValidationGroup="btnsignInsubmit" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                ValidationGroup="btnsignInsubmit" CssClass="text-danger" ErrorMessage="Please check PAN Format"
                                                ControlToValidate="txtpan2" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6" style="visibility: hidden">
                                        <label class="control-label col-sm-4" for="txtclientCode">
                                            Client Code:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtclientCode" runat="server" CssClass="form-control required" placeholder="Enter Client Code"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Enabled="false" ControlToValidate="txtclientCode"
                                                ValidationGroup="btnsignInsubmit" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-9">
                                        <asp:Label ID="lblClient" runat="server" Visible="false" 
                                            CssClass="ValidationMsg">Client does not exists.<a data-toggle="tab" onclick="lblClient_Click" runat="server" aria-controls="signIn" href="#signUp">SignIn here.</a></asp:Label>
                                        <asp:Label ID="lbllogedIn2" runat="server" Visible="false" Text="User already loged In."
                                            CssClass="ValidationMsg"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-6 pull-right">
                                                <asp:Button ID="btnsignInsubmit" runat="server" OnClick="btnsignInsubmit_Click" Text="Submit"
                                                    ValidationGroup="btnsignInsubmit" CssClass="btn btn-info" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <footer class="container-fluid text-center">
  <p>© 2016. All rights Reserved. SBICAP Securities Limited </p>
</footer>
    <asp:HiddenField ID="TabName" runat="server" />
    </form>

    <script src="Scripts/bootstrap-datepicker.js" type="text/javascript" charset="UTF-8"></script>

    <script>

        $(function() {

            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "signUp";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function() {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
            $("#asd a").click(function() {
                $('ul.nav li:eq(1)').addClass("active");
                $('ul.nav li:eq(0)').removeClass("active");
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
            $('#txtDob').datepicker({
                autoclose: 1
            });


        });

        $(document).ready(function() {
            var button = $("[id*=submit],[id*=Complete]");

            $(button).click(function(e) {
                var elements = $(".required");

                for (var i = 0; i < elements.length; i++) {
                    var fieldval = $(elements[i]).val();
                    if (fieldval == "" || fieldval == "Select") {
                        $(elements[i]).addClass("invalid");
                    }
                    else if ($(elements[i]).hasClass("invalid")) {
                        $(elements[i]).removeClass("invalid");
                    }
                }





            });

            var textbox = $("[id*=txt]");
            $(textbox).change(function(e) {

                var elements = $(".required");
                for (var i = 0; i < elements.length; i++) {
                    var fieldval = $(elements[i]).val();

                    if (fieldval == "" || fieldval == "Select") {
                        $(elements[i]).addClass("invalid");
                    }
                    else if ($(elements[i]).hasClass("invalid")) {
                        $(elements[i]).removeClass("invalid");
                    }
                }

            });

        });

         
  

    </script>

</body>
</html>
