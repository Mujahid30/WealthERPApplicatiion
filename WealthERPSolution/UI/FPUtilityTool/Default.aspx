<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FPUtilityTool._Default" %>

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
      <span class="navbar-brand">SBI Capsec</span>
    </div>
  </div>
</nav>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-body">
                <div id="Tabs" role="tabpanel">
                    <ul class="nav nav-tabs nav-justified">
                        <li><a data-toggle="tab" aria-controls="signUp" href="#signUp">
                            <p class="lead">
                                New Investor</p>
                        </a></li>
                        <li><a data-toggle="tab" aria-controls="signIn" href="#signIn">
                            <p class="lead">
                                Existing Investor</p>
                        </a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="signUp" class="tab-pane fade in">
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
                                            <asp:TextBox ID="txtMobNo" runat="server" CssClass="form-control required" placeholder="Enter Mobile No"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMobNo"
                                                ValidationGroup="btnsignUpsubmit" runat="server" />
                                            <asp:CompareValidator ID="cvMobileNo" ControlToValidate="txtMobNo" runat="server"
                                                Display="Dynamic" ValidationGroup="btnsignUpsubmit" ErrorMessage="Please enter a valid Mobile No."
                                                Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label class="control-label col-sm-4" for="txtPan1">
                                            PAN:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPan1" runat="server" CssClass="form-control required" placeholder="Enter PAN"></asp:TextBox>
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
                                    <div class="col-sm-9">
                                    <asp:Label ID="lbllogedIn1" runat="server" Visible="false" Text="User already loged In." CssClass="text-danger"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-6 pull-right">
                                                <asp:Button ID="btnSignUpsubmit" runat="server" OnClick="btnsignUpsubmit_Click" Text="Submit"
                                                    ValidationGroup="btnsignUpsubmit" CssClass="btn btn-default" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
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
                                            <asp:TextBox ID="txtpan2" runat="server" CssClass="form-control required" placeholder="Enter PAN"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtpan2"
                                                ValidationGroup="btnsignInsubmit" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                ValidationGroup="btnsignInsubmit" CssClass="text-danger" ErrorMessage="Please check PAN Format"
                                                ControlToValidate="txtpan2" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label class="control-label col-sm-4" for="txtclientCode">
                                            Client Code:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtclientCode" runat="server" CssClass="form-control required" placeholder="Enter Client Code"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtclientCode"
                                                ValidationGroup="btnsignInsubmit" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-9">
                                        <asp:Label ID="lblClient" runat="server" Visible="false" Text="Client does not exists." CssClass="text-danger"></asp:Label>
                                         <asp:Label ID="lbllogedIn2" runat="server" Visible="false" Text="User already loged In." CssClass="text-danger"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-6 pull-right">
                                                <asp:Button ID="btnsignInsubmit" runat="server" OnClick="btnsignInsubmit_Click" Text="Submit"
                                                    ValidationGroup="btnsignInsubmit" CssClass="btn btn-default" />
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
  <p>Copyright : Ampsys Consulting Pvt. Ltd.</p>  
</footer>
    <asp:HiddenField ID="TabName" runat="server" />
    </form>

    <script>

        $(function() {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "signUp";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function() {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
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
