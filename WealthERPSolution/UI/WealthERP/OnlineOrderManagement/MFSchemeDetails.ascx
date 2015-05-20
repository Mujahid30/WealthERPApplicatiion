<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFSchemeDetails.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFSchemeDetails" %>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">
<!-- Latest compiled and minified JavaScript -->

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<style>
    .header 
    {
        background: #258dc8; /* Old browsers */
        background: -moz-linear-gradient(top,  #258dc8 0%, #2475c7 100%); /* FF3.6+ */
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#258dc8), color-stop(100%,#2475c7)); /* Chrome,Safari4+ */
        background: -webkit-linear-gradient(top,  #258dc8 0%,#2475c7 100%); /* Chrome10+,Safari5.1+ */
        background: -o-linear-gradient(top,  #258dc8 0%,#2475c7 100%); /* Opera 11.10+ */
        background: -ms-linear-gradient(top,  #258dc8 0%,#2475c7 100%); /* IE10+ */
        background: linear-gradient(to bottom,  #258dc8 0%,#2475c7 100%); /* W3C */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#258dc8', endColorstr='#2475c7',GradientType=0 ); /* IE6-9 */
        height: 20px;
        text-align: center;
        font-color: white;
    }
    .tddate
    {
        background: #ffffff; /* Old browsers */
        background: -moz-linear-gradient(top,  #ffffff 0%, #f6f6f6 47%, #ededed 100%); /* FF3.6+ */
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#ffffff), color-stop(47%,#f6f6f6), color-stop(100%,#ededed)); /* Chrome,Safari4+ */
        background: -webkit-linear-gradient(top,  #ffffff 0%,#f6f6f6 47%,#ededed 100%); /* Chrome10+,Safari5.1+ */
        background: -o-linear-gradient(top,  #ffffff 0%,#f6f6f6 47%,#ededed 100%); /* Opera 11.10+ */
        background: -ms-linear-gradient(top,  #ffffff 0%,#f6f6f6 47%,#ededed 100%); /* IE10+ */
        background: linear-gradient(to bottom,  #ffffff 0%,#f6f6f6 47%,#ededed 100%); /* W3C */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#ededed',GradientType=0 ); /* IE6-9 */
    }
</style>
<div id="demo" class="row " style="margin-left: 6.1%; margin-top: 1%; margin-bottom: 2%;
    margin-right: 10.5%; border: 2px solid #a1a1a1; padding-top: 1%; padding-bottom: 1%">
    <div class="col-md-12 col-xs-12 col-sm-12">
        <div class="col-md-7">
            <b>AMC:</b>
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="form-control input-sm" Width="100%"
                OnSelectedIndexChanged="ddlAMC_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-md-5">
            <b>Category: </b>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control input-sm">
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-md-12">
        <div class="col-md-8">
            <b>Scheme:</b>
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control input-sm"
                class="form-control">
            </asp:DropDownList>
        </div>
        <div class="col-md-4" style="margin-top: 2%">
            <asp:Button ID="Button1" runat="server" class="btn btn-sm btn-primary" Text="GO"
                OnClick="Go_OnClick"></asp:Button>
        </div>
    </div>
</div>
<div class="table-responsive" style="margin-left: 5%;">
    <div class="col-md-3" style="width: 70%;">
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th colspan="4" class="header">
                        Scheme Details
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td style="width: auto;">
                        <b>Scheme Rating</b>
                    </td>
                    <td>
                        <asp:Image runat="server" ID="imgSchemeRating" /></a>
                    </td>
                    <td>
                        <b>Scheme Name</b>
                    </td>
                    <td>
                        <asp:Label ID="lblSchemeName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>AMC Name</b>
                    </td>
                    <td>
                        <asp:Label ID="lblAMC" runat="server"></asp:Label>
                    </td>
                    <td>
                        <b>Category</b>
                    </td>
                    <td>
                        <asp:Label ID="lblCategory" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>Fund Manager</b>
                    </td>
                    <td>
                        <asp:Label ID="lblFundManager" runat="server"></asp:Label>
                    </td>
                    <td>
                        <b>Scheme Benchmark</b>
                    </td>
                    <td>
                        <asp:Label ID="lblBanchMark" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>Funds Returns 1st Year</b>
                    </td>
                    <td>
                        <asp:Label ID="lblFundReturn1styear" runat="server"></asp:Label>
                    </td>
                    <td>
                        <b>Funds Returns 3rd Year</b>
                    </td>
                    <td>
                        <asp:Label ID="lblFundReturn3rdyear" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>Fund Return 5th Year</b>
                    </td>
                    <td>
                        <asp:Label ID="lblFundReturn5thyear" runat="server"></asp:Label>
                    </td>
                    <td>
                        <b>Benchmark Return 1st year</b>
                    </td>
                    <td>
                        <asp:Label ID="lblBenchmarkReturn" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>Benchmark Return 3rd year</b>
                    </td>
                    <td>
                        <asp:Label ID="lblBenchMarkReturn3rd" runat="server"></asp:Label>
                    </td>
                    <td>
                        <b>Benchmark Return 5th year</b>
                    </td>
                    <td>
                        <asp:Label ID="lblBenchMarkReturn5th" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>NAV Date</b>
                    </td>
                    <td>
                        <asp:Label ID="lblNAVDate" runat="server"></asp:Label>
                    </td>
                    <td>
                        <b>NAV</b>
                    </td>
                    <td>
                        <asp:Label ID="lblNAV" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>Min Investment Amount</b>
                    </td>
                    <td>
                        <asp:Label ID="lblMinInvestment" runat="server"></asp:Label>
                    </td>
                    <td>
                        <b>Multiple of</b>
                    </td>
                    <td>
                        <asp:Label ID="lblMinMultipleOf" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>Min SIP Amount</b>
                    </td>
                    <td>
                        <asp:Label ID="lblMinSIP" runat="server"></asp:Label>
                    </td>
                    <td>
                        <b>Multiple of</b>
                    </td>
                    <td>
                        <asp:Label ID="lblSIPMultipleOf" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <b>Exit Load</b>
                    </td>
                    <td>
                        <asp:Label ID="lblExitLoad" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="col-md-3" style="width: 20%;">
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th class="header">
                        Morning Star
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <div>
                            <asp:Image ID="imgStyleBox" runat="server" /></div>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th colspan="2" class="header">
                        Additional Information
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <asp:LinkButton ID="lnkSID" runat="server" Text="SID" CssClass="LinkButtons"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        <asp:LinkButton ID="lnkSAI" runat="server" Text="SAI" CssClass="LinkButtons"></asp:LinkButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
<div class="row" style="margin-left: 20%; margin-right: 2%; margin-bottom: 2%; margin-top: 2%">
    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
    <asp:LinkButton ID="lnkAddToCompare" runat="server" CssClass="btn btn-primary btn-primary"
            OnClick="lnkAddToCompare_OnClick"> Add To Compare <span class="glyphicon glyphicon-shopping-list">
                            </span></asp:LinkButton>
        &nbsp &nbsp  <asp:LinkButton ID="lbBuy" runat="server" CssClass="btn btn-primary btn-primary"
            OnClick="lbBuy_OnClick"> Buy <span class="glyphicon glyphicon-shopping-cart">
                            </span></asp:LinkButton>
        &nbsp &nbsp
        <asp:LinkButton ID="lbAddPurchase" runat="server" CssClass="btn btn-primary btn-success"
            OnClick="lbAddPurchase_OnClick" Visible="false"> Additional Purchase <span class="glyphicon glyphicon-plus-sign">
                            </span></asp:LinkButton>
        &nbsp &nbsp
        <asp:LinkButton ID="lbSIP" runat="server" CssClass="btn btn-primary btn-info" OnClick="lbSIP_OnClick" > SIP <span class="glyphicon glyphicon-th-list">
                            </span></asp:LinkButton>
        &nbsp &nbsp
        <asp:LinkButton ID="lbRedem" runat="server" CssClass="btn btn-primary btn-danger"
            OnClick="lbRedem_OnClick" Visible="false"> Redemption <span class="glyphicon glyphicon-minus">
                            </span></asp:LinkButton>
    </div>
</div>
<div class="table-responsive" style="margin-left: 5%;">
    <div class="col-md-3" style="width: 40%;">
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th colspan="2" class="header">
                        Fund Manager Profile
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        FundManager
                    </td>
                    <td>
                        <asp:Label ID="lblFundMAnagername" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        Qualification
                    </td>
                    <td>
                        <asp:Label ID="lblQualification" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        Designation
                    </td>
                    <td>
                        <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="searchable-spec cell top sub-name small bordered">
                    <td>
                        Experience
                    </td>
                    <td>
                        <asp:Label ID="lblExperience" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
