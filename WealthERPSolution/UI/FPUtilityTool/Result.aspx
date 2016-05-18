<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="FPUtilityTool.Result" %>

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
        .bg-grey
        {
            background-color: #f6f6f6;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <nav class="navbar navbar-default">
  <div class="container-fluid">
      <div class="navbar-header">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <span class="navbar-brand">SBI Capsec</span>
    </div>
   <div class="collapse navbar-collapse" id="myNavbar">
    <ul class="nav navbar-nav navbar-right">
      <li><a><span aria-hidden="true" class="glyphicon glyphicon-user"></span><asp:Label ID="lblUserName" runat="server"></asp:Label></a></li>
      <li><asp:LinkButton ID="btnLogOut" runat="server" OnClick="btnLogOut_OnClick" > <span aria-hidden="true" class="glyphicon glyphicon-log-out"></span> LogOut
</asp:LinkButton></li>
    </ul>
    </div>
  </div>
</nav>
    <div class="container-fluid bg-grey">
        <div class="row">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-10">
                <h2 class="text-left">
                    Risk Profile Result</h2>
            </div>
            <div class="col-sm-1">
            </div>
        </div>
        <div class="well">
            <div class="row ">
                <div class="col-sm-5  form-group">
                </div>
                <div class="col-sm-3 text-justify form-group bg-primary">
                    Your Risk Class:
                    <asp:Label ID="lblRiskClass" runat="server" CssClass="text-uppercase"></asp:Label>
                </div>
                <div class="col-sm-4">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2  form-group">
                </div>
                <div class="col-sm-8  form-group text-primary lead">
                    <asp:Label ID="lblRiskText" runat="server"></asp:Label>
                </div>
                <div class="col-sm-2">
                </div>
            </div>
        </div>
    </div>
    <footer class="container-fluid text-center">
  <p>Copyright : Ampsys Consulting Pvt. Ltd.</p>  
</footer>
    </form>
</body>
</html>
