<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFSchemeDetails.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFSchemeDetails" %>
<asp:ScriptManager ID="scrptMgr" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
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

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>
 <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

<style type="text/css">
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
            .alignment tr th
            {
                text-align: justify;
                width: 40%;
            }
            .alignment tr td
            {
                text-align: justify;
                width: 60%;
            }
</style>
<asp:UpdatePanel ID="updSchemDetails" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div id="demo" class="row " style="margin-top: 1%; margin-bottom: 2%; width: 80%;
            padding-top: 1%; padding-bottom: 1%; background-color: #2480C7; margin-left: auto;
            margin-right: auto;">
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-7">
                    <font color="#fff"><b>AMC:</b></font>
                    <asp:DropDownList ID="ddlAMC" runat="server" CssClass="form-control input-sm" Width="100%"
                        OnSelectedIndexChanged="ddlAMC_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span1">*</span>
                    <asp:RequiredFieldValidator ID="reqAMC" runat="server" Style="color: White;" Display="Dynamic"
                        ValidationGroup="btnGo" ErrorMessage="Select AMC" ControlToValidate="ddlAMC"
                        InitialValue="0"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-5">
                    <font color="#fff"><b>Category: </b></font>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control input-sm"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span2">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCategory"
                        ErrorMessage="<br />Please select category" Style="color: White;" Display="Dynamic"
                        runat="server" InitialValue="0" ValidationGroup="btnViewscheme">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-8">
                    <font color="#fff"><b>Scheme:</b></font>
                    <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control input-sm"
                        class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-4" style="margin-top: 1.7%">
                    <asp:Button ID="Button1" runat="server" class="btn btn-sm btn btn-default" Text="Scheme Details"
                        OnClick="Go_OnClick" ValidationGroup="btnGo"></asp:Button>
                </div>
            </div>
        </div>
        <div  style="margin-top: 1%; margin-bottom: 2%; width: 80%;
            padding-top: 1%; padding-bottom: 1%;  margin-left: auto;
            margin-right: auto;">
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
      
        <div style="margin-top: 1%; margin-bottom: 2%; width: 80%; padding-top: 1%; padding-bottom: 1%;
            margin-left: auto; margin-right: auto;">
            <div style="width: 70%; float: left; min-width: 320px;">
                <div style="width: 100%;">
                    <table class="col-md-12 table-bordered table-striped table-condensed cf" style="margin-bottom: 0px;
                        min-width: 320px;">
                        <thead>
                            <tr>
                                <th colspan="2" class="header">
                                    <font color="#fff">Scheme Details</font>
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div>
                    <div style="float: left; width: 50%; min-width: 320px">
                        <table class="col-md-12 table-bordered table-striped table-condensed cf">
                            <tbody class="alignment">
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Scheme Rating</b>
                                    </th>
                                    <td>
                                        <asp:Image runat="server" ID="imgSchemeRating" />
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Scheme Name</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblSchemeName" runat="server" Style="text-align: justify;"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>AMC Name</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblAMC" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Category</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Fund Manager</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblFundManager" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Scheme Benchmark</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblBanchMark" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Funds Returns 1st Year</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblFundReturn1styear" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Funds Returns 3rd Year</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblFundReturn3rdyear" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Fund Return 5th Year</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblFundReturn5thyear" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Benchmark Return 1st year</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblBenchmarkReturn" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div style="float: left; width: 50%; min-width: 320px">
                        <table class="col-md-12 table-bordered table-striped table-condensed cf">
                            <tbody class="alignment">
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Benchmark Return 3rd year</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblBenchMarkReturn3rd" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Benchmark Return 5th year</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblBenchMarkReturn5th" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>NAV Date</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblNAVDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>NAV</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblNAV" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Min Investment Amount</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblMinInvestment" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Multiple of</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblMinMultipleOf" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Min SIP Amount</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblMinSIP" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Multiple of</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblSIPMultipleOf" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="searchable-spec cell top sub-name small bordered">
                                    <th>
                                        <b>Exit Load</b>
                                    </th>
                                    <td>
                                        <asp:Label ID="lblExitLoad" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div style="width: 30%; float: left; min-width: 320px;">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th class="header">
                                <font color="#fff">Morning Star </font>
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
                                <font color="#fff">Additional Information</font>
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
        <table style="margin-left: 15%;">
            <tr id="trMessage" runat="server" visible="false">
                <td colspan="6">
                    <table class="tblMessage" cellspacing="0">
                        <tr>
                            <td align="center">
                                <div id="divMessage" align="center">
                                </div>
                                <div style="clear: both">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="row" style="margin-left: 20%; margin-right: 2%; margin-bottom: 2%;">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:LinkButton ID="lnkAddToCompare" runat="server" CssClass="btn btn-primary btn-primary"
                    OnClick="lnkAddToCompare_OnClick" ValidationGroup="btnGo"> Add To Compare <span class="glyphicon glyphicon-shopping-list">
                            </span></asp:LinkButton>
                &nbsp &nbsp
                <asp:LinkButton ID="lbBuy" runat="server" CssClass="btn btn-primary btn-primary"
                    OnClick="lbBuy_OnClick" ValidationGroup="btnGo"> Buy <span class="glyphicon glyphicon-shopping-cart">
                            </span></asp:LinkButton>
                &nbsp &nbsp
                <asp:LinkButton ID="lbAddPurchase" runat="server" CssClass="btn btn-primary btn-success"
                    OnClick="lbAddPurchase_OnClick" Visible="false"> Additional Purchase <span class="glyphicon glyphicon-plus-sign">
                            </span></asp:LinkButton>
                &nbsp &nbsp
                <asp:LinkButton ID="lbSIP" runat="server" CssClass="btn btn-primary btn-info" OnClick="lbSIP_OnClick"
                    ValidationGroup="btnGo"> SIP <span class="glyphicon glyphicon-th-list">
                            </span></asp:LinkButton>
                &nbsp &nbsp
                <asp:LinkButton ID="lbRedem" runat="server" CssClass="btn btn-primary btn-danger"
                    OnClick="lbRedem_OnClick" Visible="false" ValidationGroup="btnGo"> Redemption <span class="glyphicon glyphicon-minus">
                            </span></asp:LinkButton>
            </div>
        </div>
        <div style="margin-top: 1%; margin-bottom: 2%; width: 80%; padding-top: 1%; padding-bottom: 1%;
            margin-left: auto; margin-right: auto;">
            <div style="width: 40%;min-width:320px;" id="jay">
                <table class="table table-bordered" style="margin-bottom: 0px">
                    <thead>
                        <tr>
                            <th class="header">
                                <div data-toggle="collapse" data-target="#Divfunddetails" style="color: White;" id="togglefund"
                                    onclick="togglediv('togglefund')">+</div>
                            </th>
                            <th class="header">
                                <font color="#fff">Fund Manager Profile </font>
                            </th>
                        </tr>
                    </thead>
                </table>
                <div id="Divfunddetails" style="margin-top: 0px;" class="collapse">
                    <table class="table table-bordered">
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
        </div>
        <asp:HiddenField ID="hidCurrentScheme" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function togglediv(x) {
        myDivObj = document.getElementById(x);
        if (myDivObj.innerHTML == "+") {
            document.getElementById(x).innerHTML = "-";
        }
        else if (myDivObj.innerHTML == "-") {
            document.getElementById(x).innerHTML = "+";

        }
    }

</script>

