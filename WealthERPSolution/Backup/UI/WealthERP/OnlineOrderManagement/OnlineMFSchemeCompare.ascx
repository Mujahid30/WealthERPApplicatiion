<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineMFSchemeCompare.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.OnlineMFSchemeCompare" %>
<asp:ScriptManager ID="scrptMgr" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
<script src="../Scripts/bootstrap.js" type="text/javascript"></script>
<link href="../Base/CSS/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />

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
    .readOnlyFieldsss
    {
        font-family: Verdana,Tahoma;
        font-weight: normal;
        font-size: small;
        color: Black;
    }
    .DetailfieldFontSize
    {
        font-family: Times New Roman;
        font-weight: bold;
        font-size: small;
        color: #000;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                           Scheme Compare
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="updSchemDetails" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div class="table-responsive" style="margin-left: 5%; margin-bottom: 2%;">
            <div class="col-md-3" style="width: 70%;">
                <asp:Panel ID="schemeinformation" runat="server" Height="310Px" ScrollBars="Vertical">
                    <table class="col-md-12 table-bordered table-striped table-condensed cf">
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
                                    <b>
                                        <asp:Label ID="lblRating1" runat="server" Text="Rating" CssClass="DetailfieldFontSize"
                                            Visible="false"></asp:Label></b>
                                </td>
                                <td id="tdddlAMC1" runat="server">
                                    <asp:LinkButton ID="lnkDelete1" runat="server" OnClick="lnkDelete1_OnClick" Visible="false"
                                        ToolTip="Remove" Style="float: right">  <span class="glyphicon glyphicon-remove">
                            </span></asp:LinkButton><br />
                                    <asp:Image runat="server" ID="ImgStyle1" Visible="false" />
                                    <asp:DropDownList ID="ddlAMC1" runat="server" OnSelectedIndexChanged="ddlAMC1_SelectedIndexChanged"
                                        CssClass="form-control input-sm" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlCategory1" runat="server" CssClass="form-control input-sm"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategory1_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlSchemeList1" runat="server" OnSelectedIndexChanged="ddlSchemeList1_OnSelectedIndexChanged"
                                        AutoPostBack="true" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>
                                <td id="tdddlAMC2" runat="server">
                                    <asp:LinkButton ID="lnkDelete2" runat="server" OnClick="lnkDelete2_OnClick" Visible="false"
                                        ToolTip="Remove" Style="float: right">  <span class="glyphicon glyphicon-remove">
                            </span></asp:LinkButton><br />
                                    <asp:Image runat="server" ID="ImgStyle2" Visible="false" />
                                    <asp:DropDownList ID="ddlAMC2" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlAMC2_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlCategory2" runat="server" CssClass="form-control input-sm"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategory2_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlSchemeList2" runat="server" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlSchemeList2_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td id="tdddlAMC3" runat="server">
                                    <asp:LinkButton ID="lnkDelete3" runat="server" OnClick="lnkDelete31_OnClick" Visible="false"
                                        ToolTip="Remove" Style="float: right">  <span class="glyphicon glyphicon-remove">
                            </span></asp:LinkButton><br />
                                    <asp:Image runat="server" ID="ImgStyle3" Visible="false" />
                                    <asp:DropDownList ID="ddlAMC3" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlAMC3_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlCategory3" runat="server" CssClass="form-control input-sm"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategory3_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlSchemeList3" runat="server" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlSchemeList3_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td id="tdddlAMC4" runat="server">
                                    <asp:LinkButton ID="lnkDelete4" runat="server" OnClick="lnkDelete4_OnClick" Visible="false"
                                        ToolTip="Remove" Style="float: right">  <span class="glyphicon glyphicon-remove">
                            </span></asp:LinkButton><br />
                                    <asp:Image runat="server" ID="ImgStyle4" Visible="false" />
                                    <asp:DropDownList ID="ddlAMC4" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlAMC4_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlCategory4" runat="server" CssClass="form-control input-sm"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategory4_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlSchemeList4" runat="server" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlSchemeList4_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr class="searchable-spec cell top sub-name small bordered">
                            <td style="width: auto;">
                            </td>--%>
                            <%-- <td id="tdddlCategory1" runat="server">
                               
                            </td>
                            <td id="tdddlCategory2" runat="server">
                               
                            </td>
                            <td id="tdddlCategory3" runat="server">
                              
                            </td>
                            <td id="tdddlCategory4" runat="server">
                              
                            </td>--%>
                            <%-- </tr>
                        <tr class="searchable-spec cell top sub-name small bordered">
                            <td style="width: auto;">
                            </td>--%>
                            <%-- <td id="tdddlSchemeList1" runat="server">
                              
                            </td>
                            <td id="tdddlSchemeList2" runat="server">
                              
                            </td>
                            <td id="tdddlSchemeList3" runat="server">
                               
                            </td>
                            <td id="tdddlSchemeList4" runat="server">
                               
                            </td>--%>
                            </tr>
                            <tr class="searchable-spec cell top sub-name small bordered">
                                <td>
                                    <b class="DetailfieldFontSize">Action</b>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged" Width="100px">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="Purchase" Value="Buy"></asp:ListItem>
                                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddlAction1" runat="server" AutoPostBack="true" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlAction1_OnSelectedIndexChanged" Width="100px">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="Purchase" Value="Buy"></asp:ListItem>
                                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddlAction2" runat="server" AutoPostBack="true" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlAction2_OnSelectedIndexChanged" Width="100px">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="Purchase" Value="Buy"></asp:ListItem>
                                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddlAction3" runat="server" AutoPostBack="true" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlAction3_OnSelectedIndexChanged" Width="100px">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="Purchase" Value="Buy"></asp:ListItem>
                                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="searchable-spec cell top sub-name small bordered" visible="false" id="trschemerating"
                                runat="server">
                                <td style="width: auto;">
                                    <b class="DetailfieldFontSize">Scheme Rating</b>
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
                                    <b class="DetailfieldFontSize">Scheme</b>
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
                                    <b class="DetailfieldFontSize">Category</b>
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
                                    <b class="DetailfieldFontSize">NAV</b>
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
                                    <b class="DetailfieldFontSize">NAV Date</b>
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
                                    <b class="DetailfieldFontSize">Fund Manager</b>
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
                                    <b class="DetailfieldFontSize">Return 1st yr (%)</b>
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
                                    <b class="DetailfieldFontSize">Return 3rd yr (%)</b>
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
                                    <b class="DetailfieldFontSize">Return 5th yr (%)</b>
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
                                    <b class="DetailfieldFontSize">Benchmark</b>
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
                                    <b class="DetailfieldFontSize">Benchmark Return 1st yr (%)</b>
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
                                    <b class="DetailfieldFontSize">Benchmark Return 3rd yr (%)</b>
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
                                    <b class="DetailfieldFontSize">Benchmark Return 5th yr (%)</b>
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
                                    <b class="DetailfieldFontSize">Min Investment Amt.</b>
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
                                    <b class="DetailfieldFontSize">Multiple of</b>
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
                                    <b class="DetailfieldFontSize">Min SIP Amt.</b>
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
                                    <b class="DetailfieldFontSize">Multiple of</b>
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
                                    <b class="DetailfieldFontSize">Exit Load</b>
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
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
