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

    <script type="text/javascript" src="Scripts/jsapi"></script>

    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'Recomended Asset Allocation',
                is3D: true,
                slices: { 2: { offset: 0.2} }


            };
            $.ajax({
                type: "POST",
                url: "result.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.PieChart($("#chart")[0]);
                    chart.draw(data, options);
                },
                failure: function(r) {
                    alert(r.d);
                },
                error: function(r) {
                    alert(r.d);
                }
            });
        }
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("#chkAgree").is(':checked');
        }
    </script>

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
                    Risk Profile Result</h2>
            </div>
            <div class="col-sm-1">
            </div>
        </div>
        <div class="well">
            <div class="row">
                <div class="col-sm-2">
                </div>
                <div class="col-sm-8">
                    <div id="divTncSuccess" runat="server" class="alert alert-success">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> Thank
                        you for choosing our Financial Planning Services. Our Financial Planner will get
                        back to you shortly.
                    </div>
                </div>
                <div class="col-sm-2">
                </div>
            </div>
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
                <div class="col-sm-2">
                </div>
                <div class="col-sm-8  form-group" style="z-index:1" >
                  <%--  <div id="chart" style="height: 50%;">
                    </div>--%>
                    <asp:Literal ID="ltrAssets" runat="server"></asp:Literal>
                </div>
                <div class="col-sm-2">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2">
                </div>
                <div class="col-sm-8  form-group text-primary lead">
                    <asp:Label ID="lblRiskText" runat="server"></asp:Label>
                </div>
                <div class="col-sm-2">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8 text-center">
                <h2>
                    Know About Your Detailed Personalized Financial plan
                </h2>
            </div>
            <div class="col-sm-2">
            </div>
        </div>
        <div class="row">
            <div class="col-sm-4">
            </div>
            <div class="col-sm-4 text-center">
                <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-keyboard="true"
                    data-target="#myModal">
                    Click Here</button>
            </div>
            <div class="col-sm-4">
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog" >
            <div class="modal-dialog modal-lg" style="z-index:10000">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            Terms & Conditions</h4>
                    </div>
                    <div class="modal-body well">
                        <p>
                            The document will be prepared on the basis of your risk profile and the information
                            provided by you for preparing your financial plan. Kindly note the plan will be
                            prepared by considering certain assumption which may not be appropriate at every
                            given time.
                        </p>
                        <p>
                            By using this Sample financial planning report you agree that you are aware and
                            understand that the materials provided are for your personal information only and
                            cannot be construed to be any investment, legal, accounting or taxation advice to
                            you. Any action taken or not taken by you on the basis of the information contained
                            in this sample financial planning report or by any other means of communication
                            made available to you by SBICAP Securities Ltd; is your responsibility alone and
                            SBICAP Securities Ltd will not be liable in any manner whatsoever for the direct
                            or indirect consequences of such action taken or not taken.
                        </p>
                        <p>
                            The content and the interpretation of data available through us or by any other
                            means of communication provided by SBICAP Securities Ltd are solely the personal
                            views of the contributors. The investments discussed or recommended on this Sample
                            financial plan or by any other means of communication made available to you by SBICAP
                            Securities Ltd may not be suitable for all investors. SBICAP Securities Ltd does
                            not warranty the adequacy, timeliness, accuracy or quality of the content.
                        </p>
                        <p>
                            This service does not constitute an offer to sell or a solicitation of an offer
                            to buy any shares, securities or other instruments to any person in any jurisdiction
                            where it is unlawful to make such an offer or solicitation.
                        </p>
                        <p>
                            Please note that by accepting the above mentioned details and its terms and conditions,
                            you are authorizing SBICAP Securities Ltd to call you even though you may be registered
                            under DNC.
                        </p>
                        <div class="row">
                            <div class="col-sm-5">
                                <div class="checkbox  pull-right">
                                    <label>
                                        <asp:CheckBox ID="chkAgree" runat="server" />I Agree</label>
                                </div>
                                <asp:CustomValidator runat="server" ID="CheckBoxRequired" EnableClientScript="true" ValidationGroup="accept"
                                    ClientValidationFunction="CheckBoxRequired_ClientValidate"></asp:CustomValidator>
                            </div>
                            <div class="col-sm-2 pull-left">
                                <asp:Button ID="btnTnC" runat="server" Text="Submit" ValidationGroup="accept" OnClick="btnTnC_Click" CssClass="btn btn-info " />
                            </div>
                            <div class="col-sm-5">
                            </div>
                        </div>
                    </div>
                </div>
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
