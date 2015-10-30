<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SIPBookSummmaryList.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.SIPBookSummmaryList" %>
<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">

<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<style>
    .ft_sort
    {
        background: #999999 none repeat scroll 0 0;
        border: 0 none;
        border-radius: 12px;
        box-shadow: 0 1.5px 2px #bfbfbf inset;
        color: #ffffff;
        cursor: pointer;
        display: block;
        font-size: 11.5px;
        font-style: normal;
        padding: 3px 12px;
        width: 60px;
    }
    .ft_sort:hover
    {
        background: #565656;
        color: #ffffff;
    }
    .divs
    {
        background-color: #EEEEEE;
        margin-bottom: .5%;
        margin-top: .5%;
        margin-left: .2%;
        margin-right: .1%;
        border: solid 1.5px #EEEEEE;
    }
    .divs:hover
    {
        border: solid 1.5px #00CEFF;
        cursor: auto;
    }
</style>
<style type="text/css">
    .style1
    {
        width: 37%;
    }
</style>

<script type="text/jscript">
    //    jQuery(document).ready(function($) {
    //        $('.bxslider').bxSlider(
    //    {
    //        auto: true,
    //        autoControls: true
    //    }
    //    );
    //    });

        
</script>

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

<%--<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            SIP Book
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>--%>
<table width="100%">
    <table class="tblMessage" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div class="divOnlinePageHeading">
                    <div class="divClientAccountBalance" id="divClientAccountBalance" runat="server">
                        <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                            runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                            OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                SIP Book
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <%--<tr>
        <td>
            <div class="divOnlinePageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <%-- <td align="left">
                            Systematic Book
                        </td>--%>
    <%--<td align="right">
                    <asp:LinkButton runat="server" ID="lbBack" CssClass="LinkButtons" Text="Back" Visible="false"
                        OnClick="lbBack_Click"></asp:LinkButton>
                </td
    <td>
    </td>
    </tr>
</table>
</div> </td> </tr>--%>
</table>
<div id="divConditional" runat="server" class="row " style="margin-left: 5%; margin-bottom: 0.5%;
    margin-right: 5%; padding-top: 2%; padding-bottom: 0.5%;">
    <div class="col-md-12 col-xs-12 col-sm-12" style="margin-bottom: 1%">
        <div class="col-md-4">
            AMC:
            <asp:DropDownList CssClass="form-control input-sm" ID="ddlAMCCode" runat="server"
                AutoPostBack="false">
                <%--<asp:ListItem Text="All" Value="0"></asp:ListItem>--%>
            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            <br />
            <asp:Button ID="btnViewSIP" runat="server" CssClass="btn btn-primary btn-primary"
                Text="Go" ValidationGroup="btnViewSIP" OnClick="btnViewOrder_Click" />
        </div>
        <div style="float: right">
            <asp:ImageButton ID="imgInformation" runat="server" ImageUrl="../Images/help.png"
                OnClick="imgInformation_OnClick" ToolTip="Help" Style="cursor: hand;" />
        </div>
    </div>
</div>
<table style="width: 100%" class="TableBackground">
    <tr id="trNoRecords" runat="server" visible="false">
        <td align="center">
            <div id="divNoRecords" runat="server" class="failure-msg" visible="true">
                No Record Found
            </div>
        </td>
    </tr>
</table>
<%--<asp:Panel ID="pnlSIPSumBook" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
    Visible="false">
    <table width="100%">
        <tr id="trExportFilteredDupData" runat="server">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvSIPSummaryBookMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" Width="102%"
                    AllowAutomaticInserts="false" OnNeedDataSource="gvSIPSummaryBookMIS_OnNeedDataSource"
                    OnItemDataBound="gvSIPSummaryBookMIS_OnItemDataBound" OnItemCommand="gvSIPSummaryBookMIS_OnItemCommand"
                    OnUpdateCommand="gvSIPSummaryBookMIS_UpdateCommand">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CMFSS_SystematicSetupId,CMFSS_IsCanceled,AcceptCount,InProcessCount,RejectedCount,ExecutedCount,CMFA_AccountId,PASP_SchemePlanCode,CMFSS_IsSourceAA,CMFSS_TotalInstallment,CMFSS_CurrentInstallmentNumber,CMFSS_EndDate,CMFSS_Amount,CMFSS_StartDate"
                        Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None"
                        EditMode="PopUp">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="CMFSS_CreatedOn" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                AllowFiltering="true" HeaderText="Request Date/Time" UniqueName="CMFSS_CreatedOn"
                                SortExpression="CMFSS_CreatedOn" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="CMFSS_SystematicSetupId"
                                AutoPostBackOnFilter="true" HeaderText="Request No." ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="CMFSS_SystematicSetupId" FooterStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="80px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkprAmcB" runat="server" OnClick="lnkprAmcB_Click" CommandName="Select"
                                        Text='<%# Eval("CMFSS_SystematicSetupId").ToString() %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridBoundColumn DataField="CMFSS_SystematicSetupId" AllowFiltering="true"
                                HeaderText="Transaction No." UniqueName="CMFSS_SystematicSetupId" SortExpression="CMFSS_SystematicSetupId"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="75px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PA_AMCName" HeaderText="Fund Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="PA_AMCName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" UniqueName="PA_AMCName"
                                FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFA_FolioNum" AllowFiltering="true" HeaderText="Folio"
                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="SchemePlanName"
                                AllowFiltering="true" SortExpression="PASP_SchemePlanName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PASP_SchemePlanName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="250px">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="250px" AllowFiltering="false" HeaderText="Scheme Rating"
                                HeaderStyle-Width="125px" ItemStyle-Wrap="false">
                                <ItemTemplate>

                                    <script type="text/jscript">

                                        jQuery(document).ready(function($) {
                                            var moveLeft = 0;
                                            var moveDown = 0;
                                            $('a.popper').hover(function(e) {

                                                //var target = '#' + ($(this).attr('data-popbox'));
                                                var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');

                                                $(target).show();
                                                moveLeft = $(this).outerWidth();
                                                moveDown = ($(target).outerHeight() / 2);
                                            }, function() {
                                                //var target = '#' + ($(this).attr('data-popbox'));
                                                var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');
                                                $(target).hide();
                                            });

                                            $('a.popper').mousemove(function(e) {
                                                //var target = '#' + ($(this).attr('data-popbox'));
                                                var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');

                                                leftD = e.pageX + parseInt(moveLeft);
                                                maxRight = leftD + $(target).outerWidth();
                                                windowLeft = $(window).width() - 40;
                                                windowRight = 0;
                                                maxLeft = e.pageX - (parseInt(moveLeft) + $(target).outerWidth() + 20);

                                                if (maxRight > windowLeft && maxLeft > windowRight) {
                                                    leftD = maxLeft;
                                                }

                                                topD = e.pageY - parseInt(moveDown);
                                                maxBottom = parseInt(e.pageY + parseInt(moveDown) + 20);
                                                windowBottom = parseInt(parseInt($(document).scrollTop()) + parseInt($(window).height()));
                                                maxTop = topD;
                                                windowTop = parseInt($(document).scrollTop());
                                                if (maxBottom > windowBottom) {
                                                    topD = windowBottom - $(target).outerHeight() - 20;
                                                } else if (maxTop < windowTop) {
                                                    topD = windowTop + 20;
                                                }

                                                $(target).css('top', topD).css('left', leftD);


                                            });

                                        });
    
                                    </script>

                                    <a href="#" class="popper" data-popbox="divSchemeRatingDetails"><span class="FieldName">
                                    </span>
                                        <asp:Image runat="server" ID="imgSchemeRating" />
                                    </a>
                                    <asp:Label ID="lblSchemeRating" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRatingOverall") %>'
                                        Visible="false">
                                    </asp:Label>
                                    <asp:Label ID="lblRating3Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating3Year") %>'
                                        Visible="false">
                                    </asp:Label>
                                    <asp:Label ID="lblRating5Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating5Year") %>'
                                        Visible="false">
                                    </asp:Label>
                                    <asp:Label ID="lblRating10Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating10Year") %>'
                                        Visible="false">
                                    </asp:Label>
                                    <div id="divSchemeRatingDetails" class="popbox" runat="server" style="float: left;">
                                        <h2 class="popup-title">
                                            SCHEME RATING DETAILS
                                        </h2>
                                        <table border="1" cellpadding="1" cellspacing="2" style="border-collapse: collapse;"
                                            width="10% !important;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRatingAsOnPopUp" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRatingDate") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <span class="readOnlyField">RATING</span>
                                                </td>
                                                <td>
                                                    <span class="readOnlyField">RETURN</span>
                                                </td>
                                                <td>
                                                    <span class="readOnlyField">RISK</span>
                                                </td>
                                                <td>
                                                    <span class="readOnlyField">RATING OVERALL</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="readOnlyField">3 YEAR</span>
                                                </td>
                                                <td>
                                                    <asp:Image runat="server" ID="imgRating3yr" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRetrun3yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn3Year") %>'> </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRisk3yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk3Year")%>'> </asp:Label>
                                                </td>
                                                <td rowspan="3">
                                                    <asp:Image runat="server" ID="imgRatingOvelAll" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="readOnlyField">5 YEAR</span>
                                                </td>
                                                <td>
                                                    <asp:Image runat="server" ID="imgRating5yr" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRetrun5yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn5Year") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRisk5yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk5Year")%>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="readOnlyField">10 YEAR</span>
                                                </td>
                                                <td>
                                                    <asp:Image runat="server" ID="imgRating10yr" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRetrun10yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn10Year") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSchemeRisk10yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk10Year")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridBoundColumn DataField="XF_Frequency" AllowFiltering="true"
                                HeaderText="Order Type" UniqueName="XF_Frequency" SortExpression="XF_Frequency"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>22
                            <telerik:GridBoundColumn Visible="false" DataField="CMFT_Price" AllowFiltering="false"
                                HeaderText="Actioned NAV" DataFormatString="{0:N0}" UniqueName="CMFT_Price" SortExpression="CMFT_Price"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CMFT_TransactionDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Date" UniqueName="CMFT_TransactionDate" SortExpression="CMFT_TransactionDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_DividendOption" HeaderText="Dividend Type"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFSS_DividendOption"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSS_DividendOption" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="DivedendFrequency" HeaderText="Div-Reinvestment Freq."
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="DivedendFrequency"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="DivedendFrequency" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CMFT_TransactionNumber" HeaderText="Transaction No"
                                AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CMFT_TransactionNumber"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CMFT_TransactionNumber" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_Amount" AllowFiltering="false" HeaderText="Amount"
                                DataFormatString="{0:N2}" UniqueName="CMFSS_Amount" SortExpression="CMFSS_Amount"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Unit" AllowFiltering="false" HeaderText="Units"
                                DataFormatString="{0:N0}" UniqueName="Unit" SortExpression="Unit" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CMFOD_Units" AllowFiltering="false"
                                HeaderText="Order Units" DataFormatString="{0:N0}" UniqueName="CMFOD_Units" SortExpression="CMFOD_Units"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XF_Frequency" HeaderText="SIP Frequency" AllowFiltering="false"
                                HeaderStyle-Wrap="false" SortExpression="XF_Frequency" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="XF_Frequency" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_StartDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Start Date" UniqueName="CMFSS_StartDate" SortExpression="CMFSS_StartDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_EndDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="End Date" UniqueName="CMFSS_EndDate" SortExpression="CMFSS_EndDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_NextSIPDueDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Next SIP Date" UniqueName="CMFSS_NextSIPDueDate"
                                SortExpression="CMFSS_NextSIPDueDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_TotalInstallment" AllowFiltering="false"
                                HeaderText="Total Installment" UniqueName="CMFSS_TotalInstallment" SortExpression="CMFSS_TotalInstallment"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_CurrentInstallmentNumber" AllowFiltering="false"
                                HeaderText="Current Installment" UniqueName="CMFSS_CurrentInstallmentNumber"
                                SortExpression="CMFSS_TotalInstallment" ShowFilterIcon="false" HeaderStyle-Width="80px"
                                Visible="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridBoundColumn DataField="AcceptCount" AllowFiltering="false" HeaderText="Accepted"
                                UniqueName="AcceptCount" SortExpression="AcceptCount" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>44
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="AcceptCount" AutoPostBackOnFilter="true"
                                HeaderText="Accepted" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="AcceptCount" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkpraccpt" runat="server" CommandName="Accepted" Text='<%# Eval("AcceptCount").ToString() %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="SIPDueCount" AllowFiltering="false" HeaderText="Pending"
                                UniqueName="SIPDueCount" SortExpression="SIPDueCount" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridBoundColumn DataField="InProcessCount" AllowFiltering="false" HeaderText="In Process"
                                UniqueName="InProcessCount" SortExpression="InProcessCount" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>33
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="InProcessCount" AutoPostBackOnFilter="true"
                                HeaderText="In Process" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="InProcessCount" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkprprcess" runat="server" CommandName="InProcess" Text='<%# Eval("InProcessCount").ToString() %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridBoundColumn DataField="RejectedCount" AllowFiltering="false" HeaderText="Rejected"
                                UniqueName="RejectedCount" SortExpression="RejectedCount" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>44
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="RejectedCount" AutoPostBackOnFilter="true"
                                HeaderText="Rejected" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="RejectedCount" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkprreject" runat="server" CommandName="Rejected" Text='<%# Eval("RejectedCount").ToString() %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="ExecutedCount" AutoPostBackOnFilter="true"
                                HeaderText="Executed" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="ExecutedCount" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkprexecuted" runat="server" CommandName="Executed" Text='<%# Eval("ExecutedCount").ToString() %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="SystemRejectCount" AllowFiltering="false" HeaderText="System Rejected"
                                UniqueName="SystemRejectCount" SortExpression="SystemRejectCount" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_InstallmentOther" AllowFiltering="false"
                                HeaderText="Other" UniqueName="CMFSS_InstallmentOther" SortExpression="CMFSS_InstallmentOther"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="XS_Status" AllowFiltering="false"
                                HeaderText="Order Status" HeaderStyle-Width="80px" UniqueName="XS_Status" SortExpression="XS_Status"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Channel" AllowFiltering="false" HeaderText="Channel"
                                HeaderStyle-Width="80px" UniqueName="Channel" SortExpression="Channel" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_IsCanceled" AllowFiltering="false" HeaderText="Status"
                                HeaderStyle-Width="80px" UniqueName="CMFSS_IsCanceled" SortExpression="CMFSS_IsCanceled"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSS_Remark" AllowFiltering="false" HeaderText="Remark"
                                HeaderStyle-Width="80px" UniqueName="CMFSS_Remark" SortExpression="CMFSS_Remark"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridButtonColumn HeaderText="Action" CommandName="Update" Text="Cancel"
                                ConfirmText="Do you want to Cancel this SIP? Click OK to proceed" UniqueName="column">
                            </telerik:GridButtonColumn>444
                            <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="editColumn"
                                EditText="Cancel" CancelText="Cancel" UpdateText="OK">
                            </telerik:GridEditCommandColumn>
                            <%--  <telerik:GridTemplateColumn UniqueName="Action" ItemStyle-Width="60px" AllowFiltering="false" HeaderText="Action"
                                Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAction" runat="server" CommandName="Update" Text="Cancel" ConfirmText="Do you want to Cancel this SIP? Click OK to proceed"
                                        UniqueName="Action" >
                                        </asp:LinkButton>
                                  <%--  </telerik:GridButtonColumn>444
                            <%-- <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/Images/Buy-Button.png" />444
                        </Columns>
                    
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>--%>
<div id="Div1" class="row" style="margin-left: 5%; margin-right: 3%; background-color: #2480C7;"
    visible="false" runat="server">
    <telerik:RadGrid ID="gvSIPSummaryBookMIS" runat="server" GridLines="None" AllowPaging="True"
        PageSize="5" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
        AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
        HorizontalAlign="NotSet" CellPadding="15" OnNeedDataSource="gvSIPSummaryBookMIS_OnNeedDataSource"
        OnItemDataBound="gvSIPSummaryBookMIS_OnItemDataBound" OnItemCommand="gvSIPSummaryBookMIS_OnItemCommand"
        OnUpdateCommand="gvSIPSummaryBookMIS_UpdateCommand">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="CMFSS_SystematicSetupId,CMFSS_IsCanceled,AcceptCount,InProcessCount,RejectedCount,ExecutedCount,CMFA_AccountId,PASP_SchemePlanCode,CMFSS_IsSourceAA,CMFSS_TotalInstallment,CMFSS_CurrentInstallmentNumber,CMFSS_EndDate,CMFSS_Amount,CMFSS_StartDate"
            AllowCustomSorting="true" EditMode="PopUp">
            <Columns>
                <telerik:GridTemplateColumn>
                    <HeaderTemplate>
                        <%--  <div class="col-md-2">
                            <asp:Button ID="btnAMC" runat="server" CssClass="ft_sort" Text="AMC" /></div>
                        <div class="col-md-2">
                            <asp:Button ID="Button1" runat="server" CssClass="ft_sort" Text="Scheme" /></div>--%>
                        <%--<div class="col-md-4">
                            <asp:Button ID="Button2" runat="server" CssClass="ft_sort" Text="Order Status" Width="100px" /></div>--%>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12 divs">
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Fund Name:</b></font>
                                <%# Eval("PA_AMCName")%>
                            </div>
                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 fk-font-6" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Scheme Name:</b> </font>
                                <%# Eval("PASP_SchemePlanName")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Sub Category:</b></font>
                                <%# Eval("PAISC_AssetInstrumentSubCategoryName")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Scheme Rating:</b></font>

                                <script type="text/jscript">

                                    jQuery(document).ready(function($) {
                                        var moveLeft = 0;
                                        var moveDown = 0;
                                        $('a.popper').hover(function(e) {

                                            //var target = '#' + ($(this).attr('data-popbox'));
                                            var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');

                                            $(target).show();
                                            moveLeft = $(this).outerWidth();
                                            moveDown = ($(target).outerHeight() / 0);
                                        }, function() {
                                            //var target = '#' + ($(this).attr('data-popbox'));
                                            var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');
                                            $(target).hide();
                                        });

                                        $('a.popper').mousemove(function(e) {
                                            //var target = '#' + ($(this).attr('data-popbox'));
                                            var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');

                                            leftD = e.pageX + parseInt(moveLeft);
                                            maxRight = leftD + $(target).outerWidth();
                                            windowLeft = $(window).width();
                                            windowRight = 0;
                                            maxLeft = e.pageX - (parseInt(moveLeft) + $(target).outerWidth());

                                            if (maxRight > windowLeft && maxLeft > windowRight) {
                                                leftD = maxLeft;
                                            }

                                            topD = e.pageY - parseInt(moveDown);
                                            maxBottom = parseInt(e.pageY + parseInt(moveDown));
                                            windowBottom = parseInt(parseInt($(document).scrollTop()) + parseInt($(window).height()));
                                            maxTop = topD;
                                            windowTop = parseInt($(document).scrollTop());
                                            //                                            if (maxBottom > windowBottom) {
                                            //                                                topD = windowBottom - $(target).outerHeight() - 20;
                                            //                                            } else if (maxTop < windowTop) {
                                            //                                                topD = windowTop ;
                                            //                                            }

                                            $(target).css('top', topD).css('centre', leftD);
                                        });
                                    });
    
                                </script>

                                <a href="#" class="popper" data-popbox="divSchemeRatingDetails"><span class="FieldName">
                                </span>
                                    <asp:Image runat="server" ID="imgSchemeRating" />
                                </a>
                                <asp:Label ID="lblSchemeRating" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRatingOverall") %>'
                                    Visible="false">
                                </asp:Label>
                                <asp:Label ID="lblRating3Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating3Year") %>'
                                    Visible="false">
                                </asp:Label>
                                <asp:Label ID="lblRating5Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating5Year") %>'
                                    Visible="false">
                                </asp:Label>
                                <asp:Label ID="lblRating10Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating10Year") %>'
                                    Visible="false">
                                </asp:Label>
                                <div id="divSchemeRatingDetails" class="popbox" runat="server">
                                    <h2 class="popup-title">
                                        SCHEME RATING DETAILS
                                    </h2>
                                    <table border="1" cellpadding="1" cellspacing="2" style="border-collapse: collapse;"
                                        width="10% !important;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRatingAsOnPopUp" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRatingDate") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span class="readOnlyField">RATING</span>
                                            </td>
                                            <td>
                                                <span class="readOnlyField">RETURN</span>
                                            </td>
                                            <td>
                                                <span class="readOnlyField">RISK</span>
                                            </td>
                                            <td>
                                                <span class="readOnlyField">RATING OVERALL</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="readOnlyField">3 YEAR</span>
                                            </td>
                                            <td>
                                                <asp:Image runat="server" ID="imgRating3yr" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSchemeRetrun3yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn3Year") %>'> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSchemeRisk3yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk3Year")%>'> </asp:Label>
                                            </td>
                                            <td rowspan="3">
                                                <asp:Image runat="server" ID="imgRatingOvelAll" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="readOnlyField">5 YEAR</span>
                                            </td>
                                            <td>
                                                <asp:Image runat="server" ID="imgRating5yr" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSchemeRetrun5yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn5Year") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSchemeRisk5yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk5Year")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="readOnlyField">10 YEAR</span>
                                            </td>
                                            <td>
                                                <asp:Image runat="server" ID="imgRating10yr" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSchemeRetrun10yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn10Year") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSchemeRisk10yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk10Year")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Request Date/Time:</b></font>
                                <%# Eval("CMFSS_CreatedOn")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Request No.:</b></font>
                                <asp:Button ID="btnDetails" runat="server" Text='<%# Eval("CMFSS_SystematicSetupId").ToString() %>'
                                    OnClick="btnDetails_OnClick" Style="border-style: hidden; text-decoration: underline;
                                    color: #0396CC;"></asp:Button>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Folio No.:</b></font>
                                <%# Eval("CMFA_FolioNum")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Dividend Type:</b></font>
                                <%# Eval("CMFSS_DividendOption")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Amount:</b></font>
                                <%# Eval("CMFSS_Amount")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Units:</b></font>
                                <%# Eval("Unit")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>SIP Frequency:</b></font>
                                <%# Eval("XF_Frequency")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Start date:</b></font>
                                <%# Eval("CMFSS_StartDate")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>End date:</b></font>
                                <%# Eval("CMFSS_EndDate")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Next SIP Date:</b></font>
                                <%# Eval("CMFSS_NextSIPDueDate")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Total Installment:</b></font>
                                <%# Eval("CMFSS_TotalInstallment")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Accepted:</b></font>
                                <asp:LinkButton ID="lnkpraccpt" runat="server" CssClass="LinkButtons" Style="text-decoration: underline;
                                    color: #0396CC;" CommandName="Accepted" Text='<%# Eval("AcceptCount").ToString() %>'>
                                </asp:LinkButton>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Pending:</b></font>
                                <%# Eval("SIPDueCount")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>In Process:</b></font>
                                <asp:LinkButton ID="lnkprprcess" runat="server" CssClass="LinkButtons" Style="text-decoration: underline;
                                    color: #0396CC;" CommandName="InProcess" Text='<%# Eval("InProcessCount").ToString() %>'>
                                </asp:LinkButton>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Rajected:</b></font>
                                <asp:LinkButton ID="lnkprreject" runat="server" CssClass="LinkButtons" Style="text-decoration: underline;
                                    color: #0396CC;" CommandName="Rejected" Text='<%# Eval("RejectedCount").ToString() %>'>
                                </asp:LinkButton>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Executed:</b></font>
                                <asp:LinkButton ID="lnkprexecuted" runat="server" CssClass="LinkButtons" Style="text-decoration: underline;
                                    color: #0396CC;" CommandName="Executed" Text='<%# Eval("ExecutedCount").ToString() %>'>
                                </asp:LinkButton>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>System Rejected:</b></font>
                                <%# Eval("SystemRejectCount")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Other:</b></font>
                                <%# Eval("CMFSS_InstallmentOther")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Channel:</b></font>
                                <%# Eval("Channel")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>Status:</b></font>
                                <%# Eval("CMFSS_IsCanceled")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <font color="#565656"><b>InProcessCount:</b></font>
                                <%# Eval("InProcessCount")%>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                <asp:LinkButton ID="lbtnMarkAsReject" UniqueName="editColumn" runat="server" CommandName="Edit"
                                    class="ft_sort btn-sm btn-info" Text="Cancel"></asp:LinkButton>
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                visibility: hidden;">
                                <font color="#565656"><b>InProcessCount:</b></font>
                                <%# Eval("InProcessCount")%>
                            </div>
                        </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%">
                                <telerik:RadGrid ID="gvChildOrderBookDetails" runat="server" AllowPaging="false"
                                    AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false"
                                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                                    CellPadding="15" Visible="false">
                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                        Width="100%" ShowHeader="false">
                                        <Columns>
                                            <telerik:GridTemplateColumn>
                                                <ItemTemplate>
                                                    <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12">
                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Order Date/Time:</b></font>
                                                            <%# Eval("CO_OrderDate")%>
                                                        </div>
                                                        <div class="col-xs-3 col-sm-3 col-md-3col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Request No.:</b> </font>
                                                            <%# Eval("CMFSS_SystematicSetupId")%>
                                                        </div>
                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Transaction No./Order No.:</b></font>
                                                            <%# Eval("CO_OrderId")%>
                                                        </div>
                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Order Type:</b></font>
                                                            <%# Eval("XSTT_SystematicTypeCode")%>
                                                        </div>
                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Actioned NAV:</b></font>
                                                            <%# Eval("CMFT_Price")%>
                                                        </div>
                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Transaction Date:</b></font>
                                                            <%# Eval("CMFT_TransactionDate")%>
                                                        </div>
                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Intallment No:</b></font>
                                                            <%# Eval("IntallmentNo")%>
                                                        </div>
                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Order Units:</b></font>
                                                            <%# Eval("CMFOD_Units")%>
                                                        </div>
                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                            <font color="#565656"><b>Reject Remark:</b></font>
                                                            <%# Eval("COS_Reason")%>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings FormTableStyle-Height="60%" EditFormType="Template" FormMainTableStyle-Width="300px"
                CaptionFormatString=" SIP Cancel Request">
                <FormTemplate>
                    <table style="background-color: White;" border="0">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Request No.:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSystematicSetupId" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CMFSS_SystematicSetupId") %>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td c>
                                <asp:Label ID="Label20" runat="server" Text="Remark:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="txtField" Style="width: 250px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" Text="OK" runat="server" class="ft_sort btn-sm btn-info" CommandName="Update"
                                    ValidationGroup="btnSubmit"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" class="ft_sort btn-sm btn-info"
                                    CommandName="Cancel"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableAlternatingItems="false">
        </ClientSettings>
        <PagerStyle Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
</div>
<asp:HiddenField ID="hdnAmc" runat="server" />
<asp:HiddenField ID="hdnOrderStatus" runat="server" />
<asp:HiddenField ID="hdnsystamaticType" runat="server" Value="" />
<asp:Panel ID="pnlHelp" runat="server">
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
        <Windows>
            <telerik:RadWindow ID="RadInformation1" Modal="true" Behaviors="Close, Move" VisibleOnPageLoad="false"
                Width="760px" Height="580px" runat="server" Left="300px" Top="50px" OnClientShow="setCustomPosition">
                <ContentTemplate>
                    <div style="padding: 0px; width: 100%; height: 100%;">
                        <iframe src="../ReferenceFiles/MFOrderbook.htm" name="iframeTermsCondition" style="width: 100%;
                            height: 100%"></iframe>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Panel>

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

