<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Questionnaire.aspx.cs"
    Inherits="FPUtilityTool.Questionnaire" %>

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
        .radio
        {
            padding-left: 20px;
        }
        .radio label
        {
            display: inline-block;
            vertical-align: middle;
            position: relative;
            padding-left: 5px;
        }
        .radio label::before
        {
            content: "";
            display: inline-block;
            position: absolute;
            width: 17px;
            height: 17px;
            left: 0;
            margin-left: -20px;
            border: 1px solid #cccccc;
            border-radius: 50%;
            background-color: #fff;
            -webkit-transition: border 0.15s ease-in-out;
            -o-transition: border 0.15s ease-in-out;
            transition: border 0.15s ease-in-out;
        }
        .radio label::after
        {
            display: inline-block;
            position: absolute;
            content: " ";
            width: 11px;
            height: 11px;
            left: 3px;
            top: 3px;
            margin-left: -20px;
            border-radius: 50%;
            background-color: #555555;
            -webkit-transform: scale(0, 0);
            -ms-transform: scale(0, 0);
            -o-transform: scale(0, 0);
            transform: scale(0, 0);
            -webkit-transition: -webkit-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
            -moz-transition: -moz-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
            -o-transition: -o-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
            transition: transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
        }
        .radio input[type="radio"]
        {
            opacity: 0;
            z-index: 1;
        }
        .radio input[type="radio"]:focus + label::before
        {
            outline: thin dotted;
            outline: 5px auto -webkit-focus-ring-color;
            outline-offset: -2px;
        }
        .radio input[type="radio"]:checked + label::after
        {
            -webkit-transform: scale(1, 1);
            -ms-transform: scale(1, 1);
            -o-transform: scale(1, 1);
            transform: scale(1, 1);
        }
        .radio-info input[type="radio"] + label::after
        {
            background-color: #5bc0de;
        }
        .radio-info input[type="radio"]:checked + label::before
        {
            border-color: #5bc0de;
        }
        .radio-info input[type="radio"]:checked + label::after
        {
            background-color: #5bc0de;
        }
        .bg-grey
        {
            background-color: #f6f6f6;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" role="form">
    <nav class="navbar navbar-default">
  <div class="container-fluid">
      <div class="navbar-header">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <span class="navbar-brand"><img alt="Logo" class="img-responsive" src="Content/Images/1021_1189700.png"  /> </span>
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
                    Risk Profile Questionnaire</h2>
                <asp:MultiView ID="MultiView1" runat="server" OnActiveViewChanged="MultiView1_ActiveViewChanged">
                </asp:MultiView>
            </div>
            <div class="col-sm-1">
            </div>
        </div>
    </div>
    <footer class="container-fluid text-center">
  <p>
© 2016. All rights Reserved. SBICAP Securities Limited </p>  
</footer>
    </form>
</body>
</html>
