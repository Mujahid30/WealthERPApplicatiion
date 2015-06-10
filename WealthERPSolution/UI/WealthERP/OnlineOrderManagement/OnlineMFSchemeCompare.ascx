﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineMFSchemeCompare.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.OnlineMFSchemeCompare" %>
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
</style>
<table class="tblMessage" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div class="divOnlinePageHeading" style="height: 25px;">
                <div class="divClientAccountBalance" id="divClientAccountBalance" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="Available Limits:" CssClass="BalanceLabel"> </asp:Label>
                    <asp:Label ID="lblAvailableLimits" runat="server" Text="" CssClass="BalanceAmount"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="updSchemDetails" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div class="table-responsive" style="margin-left: 5%; margin-top: 2%;">
            <div class="col-md-3" style="width: 70%;">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th colspan="5" class="header">
                                <font color="#fff">Scheme Compare</font>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td style="width: auto;">
                            </td>
                            <td id="tdddlAMC1" runat="server">
                                <asp:DropDownList ID="ddlAMC1" runat="server" OnSelectedIndexChanged="ddlAMC1_SelectedIndexChanged"
                                    CssClass="form-control input-sm" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkDelete1" runat="server" OnClick="lnkDelete1_OnClick" Visible="false">  <span class="glyphicon glyphicon-remove">
                            </span></asp:LinkButton>
                            </td>
                            <td id="tdddlAMC2" runat="server">
                                <asp:DropDownList ID="ddlAMC2" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlAMC2_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkDelete2" runat="server" OnClick="lnkDelete2_OnClick" Visible="false">  <span class="glyphicon glyphicon-remove">
                            </span></asp:LinkButton>
                            </td>
                            <td id="tdddlAMC3" runat="server">
                                <asp:DropDownList ID="ddlAMC3" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlAMC3_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkDelete3" runat="server" OnClick="lnkDelete31_OnClick" Visible="false">  <span class="glyphicon glyphicon-remove">
                            </span></asp:LinkButton>
                            </td>
                            <td id="tdddlAMC4" runat="server">
                                <asp:DropDownList ID="ddlAMC4" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlAMC4_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkDelete4" runat="server" OnClick="lnkDelete4_OnClick" Visible="false">  <span class="glyphicon glyphicon-remove">
                            </span></asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td style="width: auto;">
                            </td>
                            <td id="tdddlCategory1" runat="server">
                                <asp:DropDownList ID="ddlCategory1" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </td>
                            <td id="tdddlCategory2" runat="server">
                                <asp:DropDownList ID="ddlCategory2" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </td>
                            <td id="tdddlCategory3" runat="server">
                                <asp:DropDownList ID="ddlCategory3" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </td>
                            <td id="tdddlCategory4" runat="server">
                                <asp:DropDownList ID="ddlCategory4" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td style="width: auto;">
                            </td>
                            <td id="tdddlSchemeList1" runat="server">
                                <asp:DropDownList ID="ddlSchemeList1" runat="server" OnSelectedIndexChanged="ddlSchemeList1_OnSelectedIndexChanged"
                                    AutoPostBack="true" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </td>
                            <td id="tdddlSchemeList2" runat="server">
                                <asp:DropDownList ID="ddlSchemeList2" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="ddlSchemeList2_OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td id="tdddlSchemeList3" runat="server">
                                <asp:DropDownList ID="ddlSchemeList3" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="ddlSchemeList3_OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td id="tdddlSchemeList4" runat="server">
                                <asp:DropDownList ID="ddlSchemeList4" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="ddlSchemeList4_OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td style="width: auto;">
                                <b>Scheme Rating</b>
                            </td>
                            <td>
                                <asp:Image runat="server" ID="imgSchemeRating" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="imgSchemeRating1" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="imgSchemeRating2" />
                            </td>
                            <td>
                                <asp:Image runat="server" ID="imgSchemeRating3" />
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Scheme Name</b>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeName1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeName2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeName3" runat="server"></asp:Label>
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
                                <asp:Label ID="lblAMC1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAMC2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAMC3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Category</b>
                            </td>
                            <td>
                                <asp:Label ID="lblCategory" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCategory1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCategory2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCategory3" runat="server"></asp:Label>
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
                                <asp:Label ID="lblFundManager1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundManager2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundManager3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Scheme Benchmark</b>
                            </td>
                            <td>
                                <asp:Label ID="lblBanchMark" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBanchMark1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBanchMark2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBanchMark3" runat="server"></asp:Label>
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
                                <asp:Label ID="lblFundReturn1styear1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundReturn1styear2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundReturn1styear3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Funds Returns 3rd Year</b>
                            </td>
                            <td>
                                <asp:Label ID="lblFundReturn3rdyear" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundReturn3rdyear1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundReturn3rdyear2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundReturn3rdyear3" runat="server"></asp:Label>
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
                                <asp:Label ID="lblFundReturn5thyear1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundReturn5thyear2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFundReturn5thyear3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Benchmark Return 1st year</b>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchmarkReturn" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchmarkReturn1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchmarkReturn2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchmarkReturn3" runat="server"></asp:Label>
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
                                <asp:Label ID="lblBenchMarkReturn3rd1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchMarkReturn3rd2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchMarkReturn3rd3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Benchmark Return 5th year</b>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchMarkReturn5th" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchMarkReturn5th1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchMarkReturn5th2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBenchMarkReturn5th3" runat="server"></asp:Label>
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
                                <asp:Label ID="lblNAVDate1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNAVDate2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNAVDate3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>NAV</b>
                            </td>
                            <td>
                                <asp:Label ID="lblNAV" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNAV1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNAV2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNAV3" runat="server"></asp:Label>
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
                                <asp:Label ID="lblMinInvestment1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMinInvestment2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMinInvestment3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Multiple of</b>
                            </td>
                            <td>
                                <asp:Label ID="lblMinMultipleOf" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMinMultipleOf1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMinMultipleOf2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMinMultipleOf3" runat="server"></asp:Label>
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
                                <asp:Label ID="lblMinSIP1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMinSIP2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMinSIP3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Multiple of</b>
                            </td>
                            <td>
                                <asp:Label ID="lblSIPMultipleOf" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSIPMultipleOf1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSIPMultipleOf2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSIPMultipleOf3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td>
                                <b>Exit Load</b>
                            </td>
                            <td>
                                <asp:Label ID="lblExitLoad" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblExitLoad1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblExitLoad2" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblExitLoad3" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>