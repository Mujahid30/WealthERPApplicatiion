<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFTurnOverMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.MFTurnOverMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<style type="text/css">
    .leftLabel
    {
        width: 10%;
        text-align: right;
    }
    .rightData
    {
        width: 15%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 40%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
</style>
<asp:UpdatePanel ID="updnTurnOverMIS" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td style="width: 100%;">
                    <div class="divPageHeading">
                        <table cellspacing="0" width="100%">
                            <tr>
                                <td align="left" style="width: 33%; text-align: left">
                                    MF Turnover MIS
                                </td>
                                <td style="width: 34%;" align="center">
                                </td>
                                <td align="right" style="width: 33%; padding-bottom: 2px;">
                                    <asp:ImageButton ID="btnAMCExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                        Height="25px" Width="25px" OnClick="btnAMCExport_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnSchemeExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                        Height="25px" Width="25px" OnClick="btnSchemeExport_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnFolioExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                        Height="25px" Width="25px" OnClick="btnFolioExport_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnBranchExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                        Height="25px" Width="25px" OnClick="btnBranchExport_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnCategoryExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                        Height="25px" Width="25px" OnClick="btnCategoryExport_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnRMExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                        Height="25px" Width="25px" OnClick="btnRMExport_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnClusterZoneExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                        Height="25px" Width="25px" OnClick="btnClusterZoneExport_Click"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table class="TableBackground" width="100%">
            <tr id="trBranchRM" runat="server">
                <td align="right" valign="top" class="leftLabel">
                    <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
                </td>
                <td valign="top" class="rightData">
                    <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                        CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 1%">
                </td>
                <td align="right" valign="top" class="leftLabel">
                    <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
                </td>
                <td valign="top" class="rightData">
                    <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" Style="vertical-align: middle">
                    </asp:DropDownList>
                </td>
                <td style="width: 1%">
                </td>
                </tr>
                <tr>
                <td align="left" class="rightData" style="width: 40%;" colspan="2">
                    <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date Type:"></asp:Label>
                    <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                        runat="server" GroupName="Date" />
                    <asp:Label ID="lblPickDate" runat="server" Text="Date Range" CssClass="Field"></asp:Label>
                    &nbsp;
                    <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                        runat="server" GroupName="Date" />
                    <asp:Label ID="lblPickPeriod" runat="server" Text="Period" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr id="trCategoryAction" runat="server">
                <td align="right" valign="top" class="leftLabel">
                    <asp:Label ID="Action" runat="server" CssClass="FieldName" Text="Select MIS:"></asp:Label>
                </td>
                <td valign="top" class="rightData">
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Style="vertical-align: middle" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                        <asp:ListItem Text="Organization Level" Value="Organization" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Staff/Customer Level" Value="Staff"></asp:ListItem>
                        <asp:ListItem Text="Product Level" Value="Product"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 1%">
                    &nbsp;
                </td>
                <td runat="server" align="right" valign="top" class="leftLabel" id="tdlblCategory">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                </td>
                <td valign="top" class="rightData" id="tdddlCategory" runat="server">
                    <asp:DropDownList ID="ddlCategory" runat="server" Style="vertical-align: middle"
                        AutoPostBack="true" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td style="width: 1%">
                    &nbsp;
                </td>
                <td valign="top" style="width: 40%" colspan="2" align="left">
                    <div id="divDateRange" runat="server" visible="false" style="float: left;">
                        <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                        <telerik:RadDatePicker id="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <calendar id="Calendar1" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                                viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                            </calendar>
                            <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                            <dateinput id="DateInput1" runat="server" displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                            </dateinput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="vgBtnGo"> </asp:RequiredFieldValidator>
                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                        <telerik:RadDatePicker ID="txtToDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <calendar id="Calendar2" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                                viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                            </calendar>
                            <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                            <dateinput id="DateInput2" runat="server" displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                            </dateinput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="vgBtnGo"> </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                            Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgBtnGo"></asp:CompareValidator>
                    </div>
                    <div id="divDatePeriod" visible="false" runat="server" style="float: left;">
                        <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
                        &nbsp; &nbsp;
                        <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span4" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                            CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                            ValidationGroup="vgBtnGo"> </asp:CompareValidator>
                    </div>
                </td>
            </tr>
         <tr id="trGoButton" runat="server"> 
         <td id="tdGoBtn" runat="server" colspan="7">
          <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnGo_Click"
                ValidationGroup="vgBtnGo"/>
         </td>
         </tr>
        </table>
        <div id="dvSectionHeading" runat="server" class="divSectionHeading" style="vertical-align: middle; margin: 2px; padding-top: 2px;">
            <table width="100%" cellspacing="0" cellpadding="0" style="padding-top: 0px;">
                <tr>
                    <td style="width: 33%" align="left">
                        <asp:Label ID="lblMFMISType" runat="server" CssClass="LinkButtons"></asp:Label>
                    </td>
                    <td style="width: 34%" align="center">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updnTurnOverMIS">
                            <ProgressTemplate>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="center">
                                            <asp:Image ID="imgProgress" ImageUrl="~/Images/ajax-loader.gif" AlternateText="Processing"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <%--<img alt="Processing" src="~/Images/ajax_loader.gif" style="width: 200px; height: 100px" />--%>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    <td style="width: 33%">
                    </td>
                </tr>
            </table>
        </div>
        <table width="99%">
            <tr runat="server" id="trPnlAMC">
                <td>
                    <asp:Panel ID="pnlAMC" ScrollBars="Horizontal" Width="100%" runat="server">
                        <div runat="server" id="divGvAmcWise" visible="false" style="margin: 2px; width: 640px;">
                            <telerik:RadGrid ID="gvAmcWise" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvAmcWise_OnNeedDataSource"
                                OnItemCommand="gvAmcWise_OnItemCommand" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                    filename="AmcWise Details" excel-format="ExcelML">
                                </exportsettings>
                                <mastertableview datakeynames="AMCCode" width="100%" allowmulticolumnsorting="True"
                                    autogeneratecolumns="false" commanditemdisplay="None">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" AllowFiltering="false" UniqueName="action"
                                            DataField="action" FooterText="Grand Total:">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details"
                                                    ItemStyle-Width="12px" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="AMC" DataField="AMC" UniqueName="AMC" SortExpression="AMC"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <%--    <telerik:GridBoundColumn HeaderText="Category" HeaderTooltip="Category"  DataField="Category" 
                    UniqueName="Category" SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="SubCategory" HeaderTooltip="SubCategory" DataField="SubCategory"
                    UniqueName="SubCategory" SortExpression="SubCategory" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Purchase Cnt" HeaderTooltip="Purchase Transaction"
                                            DataField="BUYCount" HeaderStyle-HorizontalAlign="Right" UniqueName="BUYCount"
                                            SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Amount"
                                            HeaderText="Purchase Amt" DataField="BUYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYAmount" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                            SortExpression="BUYAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Sell Cnt" HeaderTooltip="Sell Transaction"
                                            DataField="SELCount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELCount"
                                            SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Sell Amt" HeaderTooltip="Sell  Amount"
                                            DataField="SELAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELAmount"
                                            SortExpression="SELAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="DVR Cnt" HeaderTooltip="Dividend Reinvested Count"
                                            DataField="DVRCount" HeaderStyle-HorizontalAlign="Right" UniqueName="DVRCount"
                                            SortExpression="DVRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="DVR Amt" HeaderTooltip="Dividend Reinvested Amount"
                                            DataField="DVRAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="DVRAmount"
                                            SortExpression="DVRAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="DVP Cnt" HeaderTooltip="Dividend Payout Count"
                                            DataField="DVPCount" HeaderStyle-HorizontalAlign="Right" UniqueName="DVPCount"
                                            SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Amount"
                                            HeaderText="DVP Amt" DataField="DVPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPAmount" SortExpression="DVPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Count"
                                            HeaderText="SIP Cnt" DataField="SIPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Amount"
                                            HeaderText="SIP Amt" DataField="SIPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Count"
                                            HeaderText="BCI Cnt" DataField="BCICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Amount"
                                            HeaderText="BCI Amt" DataField="BCIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="BCO Cnt" HeaderTooltip="Broker Change Out Count"
                                            DataField="BCOCount" HeaderStyle-HorizontalAlign="Right" UniqueName="BCOCount"
                                            SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Amount"
                                            HeaderText="BCO Amt" DataField="BCOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Count" HeaderText="STB Cnt"
                                            DataField="STBCount" HeaderStyle-HorizontalAlign="Right" UniqueName="STBCount"
                                            SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Amount"
                                            HeaderText="STB Amt" DataField="STBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Count"
                                            HeaderText="STS Cnt" DataField="STSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Amount"
                                            HeaderText="STS Amt" DataField="STSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Count"
                                            HeaderText="SWB Cnt" DataField="SWBCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Amount"
                                            HeaderText="SWB Amt" DataField="SWBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Count"
                                            HeaderText="SWP Cnt" DataField="SWPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Amount"
                                            HeaderText="SWP Amt" DataField="SWPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Count"
                                            HeaderText="SWS Cnt" DataField="SWSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Amount"
                                            HeaderText="SWS Amt" DataField="SWSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Count"
                                            HeaderText="PRJ Cnt" DataField="PRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Amount"
                                            HeaderText="PRJ Amt" DataField="PRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <headerstyle width="150px" />
                                <clientsettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </clientsettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr id="trPnlScheme" runat="server">
                <td>
                    <asp:Panel ID="pnlScheme" ScrollBars="Horizontal" Height="400px" runat="server">
                        <div runat="server" id="dvScheme" style="margin: 2px; width: 640px;">
                            <telerik:RadGrid id="gvSchemeWise" runat="server" GridLines="None" AutoGenerateColumns="false"
                                AllowSorting="true" ShowStatusBar="true" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvSchemeWise_OnNeedDataSource"
                                OnItemCommand="gvSchemeWise_OnItemCommand" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                <exportsettings hidestructurecolumns="true" exportonlydata="true" IgnorePaging="true"
                                    filename="Scheme Details" excel-format="ExcelML">
                                </exportsettings>
                                <mastertableview datakeynames="SchemeCode" width="100%" allowmulticolumnsorting="True"
                                    autogeneratecolumns="false" commanditemdisplay="None" groupsdefaultexpanded="false"
                                    expandcollapsecolumn-groupable="true" grouploadmode="Client" showgroupfooter="true">
                                     <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="Category"   />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="Category" FieldAlias="Category" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    
                                  <%--    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="AMC"   />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="AMC" FieldAlias="AMC" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                     <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="Scheme"   />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="Scheme" FieldAlias="Scheme" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>--%>
                                    
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" AllowFiltering="false" UniqueName="action"
                                            DataField="action" FooterText="Grand Total:" Visible="false">
                                           <%-- <ItemTemplate>
                                                <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details"
                                                    ItemStyle-Width="12px" />
                                            </ItemTemplate>--%>
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridBoundColumn HeaderText="Category" HeaderTooltip="Category" DataField="Category"
                                            UniqueName="Category" SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true"
                                          
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="AMC" DataField="AMC" UniqueName="AMC" SortExpression="AMC"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderTooltip="Scheme" HeaderText="Scheme" DataField="Scheme" HeaderStyle-Width="350px"
                                            UniqueName="Scheme" SortExpression="Scheme" AutoPostBackOnFilter="true" AllowFiltering="true"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                               Aggregate="Count" FooterText="Row Count : ">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                     <%--   <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="External Code" HeaderText="ExternalCode"
                                            AllowFiltering="false" DataField="ExternalCode" UniqueName="ExternalCode" SortExpression="ExternalCode"
                                            CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>--%>
                                       
                                        <telerik:GridBoundColumn HeaderText="SubCategory" HeaderTooltip="SubCategory" DataField="SubCategory"
                                            UniqueName="SubCategory" SortExpression="SubCategory" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Net Investment"
                                            HeaderText="Net Invest" DataField="Net" HeaderStyle-HorizontalAlign="Right" UniqueName="Net"
                                            SortExpression="Net" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Gross Investment"
                                            HeaderText="Gross Invest" DataField="GrossInvestment" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="GrossInvestment" SortExpression="GrossInvestment" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Gross Redemption"
                                            HeaderText="Gross Redemp" DataField="GrossRedemption" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="GrossRedemption" SortExpression="GrossRedemption" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Transaction"
                                            HeaderText="Purchase Cnt" DataField="BUYCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Amount"
                                            HeaderText="Purchase Amt" DataField="BUYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Count"
                                            HeaderText="DVR Cnt" DataField="DVRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRCount" SortExpression="DVRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Amount"
                                            HeaderText="DVR Amt" DataField="DVRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Count"
                                            HeaderText="SIP Cnt" DataField="SIPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Amount"
                                            HeaderText="SIP Amt" DataField="SIPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Count"
                                            HeaderText="SWB Cnt" DataField="SWBCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Amount"
                                            HeaderText="SWB Amt" DataField="SWBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase Count"
                                            HeaderText="ABY Cnt" DataField="ABYCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="ABYCount" SortExpression="ABYCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase Amount"
                                            HeaderText="ABY Amt" DataField="ABYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="ABYAmount" SortExpression="ABYAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                       
                                       
                                       
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Transaction"
                                            HeaderText="Sell Cnt" DataField="SELCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Amount" HeaderText="Sell Amt"
                                            DataField="SELAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELAmount"
                                            SortExpression="SELAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Count"
                                            HeaderText="STS Cnt" DataField="STSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Amount"
                                            HeaderText="STS Amt" DataField="STSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Count"
                                            HeaderText="SWP Cnt" DataField="SWPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Amount"
                                            HeaderText="SWP Amt" DataField="SWPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Count"
                                            HeaderText="SWS Cnt" DataField="SWSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Amount"
                                            HeaderText="SWS Amt" DataField="SWSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Count"
                                            HeaderText="DVP Cnt" DataField="DVPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Amount"
                                            HeaderText="DVP Amt" DataField="DVPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPAmount" SortExpression="DVPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Count"
                                            HeaderText="PRJ Cnt" DataField="PRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Amount"
                                            HeaderText="PRJ Amt" DataField="PRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Count" HeaderText="STB Cnt"
                                            DataField="STBCount" HeaderStyle-HorizontalAlign="Right" UniqueName="STBCount"
                                            SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Amount"
                                            HeaderText="STB Amt" DataField="STBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Count"
                                            HeaderText="BCI Cnt" DataField="BCICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Amount"
                                            HeaderText="BCI Amt" DataField="BCIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection Count"
                                            HeaderText="BIR Cnt" DataField="BIRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BIRCount" SortExpression="BIRCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection Amount"
                                            HeaderText="BIR Amt" DataField="BIRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BIRAmount" SortExpression="BIRAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units Count"
                                            HeaderText="BNS Cnt" DataField="BNSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BNSCount" SortExpression="BNSCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units Amount"
                                            HeaderText="BNS Amt" DataField="BNSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BNSAmount" SortExpression="BNSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation In Count"
                                            HeaderText="CNI Cnt" DataField="CNICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNICount" SortExpression="CNICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation In Amount"
                                            HeaderText="CNI Amt" DataField="CNIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNIAmount" SortExpression="CNIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In Count"
                                            HeaderText="DSI Cnt" DataField="DSICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSICount" SortExpression="DSICount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In"
                                            HeaderText="DSI Amt" DataField="DSIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSIAmount" SortExpression="DSIAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings Count"
                                            HeaderText="HLD Cnt" DataField="HLDCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="HLDCount" SortExpression="HLDCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings Amount"
                                            HeaderText="HLD Amount" DataField="HLDAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="HLDAmount" SortExpression="HLDAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Initial Allotment Count"
                                            HeaderText="NFO Cnt" DataField="NFOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="NFOCount" SortExpression="NFOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Initial Allotment"
                                            HeaderText="NFO Amt" DataField="NFOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="NFOAmount" SortExpression="NFOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Rejection Count"
                                            HeaderText="RRJ Cnt" DataField="RRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="RRJCount" SortExpression="RRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Rejection"
                                            HeaderText="RRJ Amt" DataField="RRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="RRJAmount" SortExpression="RRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection Count"
                                            HeaderText="SRJ Cnt" DataField="SRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SRJCount" SortExpression="SRJCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection"
                                            HeaderText="SRJ Amt" DataField="SRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SRJAmount" SortExpression="SRJAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Count"
                                            HeaderText="BCO Cnt" DataField="BCOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Amount"
                                            HeaderText="BCO Amt" DataField="BCOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out Count"
                                            HeaderText="CNO Cnt" DataField="CNOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNOCount" SortExpression="CNOCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out"
                                            HeaderText="CNO Amt" DataField="CNOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNOAmount" SortExpression="CNOAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                     
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out Count"
                                            HeaderText="DSO Cnt" DataField="DSOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSOCount" SortExpression="DSOCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out"
                                            HeaderText="DSO Amt" DataField="DSOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSOAmount" SortExpression="DSOAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn> 
                                        
                                       
                                        
                                    </Columns>
                                </mastertableview>
                                <headerstyle width="150px" />
                                <clientsettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </clientsettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr id="trPnlBranch" runat="server">
                <td>
                    <asp:Panel ID="pnlBranch" ScrollBars="Horizontal" Width="100%" runat="server">
                        <div runat="server" id="divBranch" style="margin: 2px; width: 640px;">
                            <telerik:RadGrid id="gvBranchWise" runat="server" GridLines="None" AutoGenerateColumns="false"
                                PageSize="10" AllowSorting="true" AllowPaging="true" ShowStatusBar="true" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvBranchWise_OnNeedDataSource"
                                EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                    filename="Branch Details" excel-format="ExcelML">
                                </exportsettings>
                                <mastertableview width="100%" allowmulticolumnsorting="True" autogeneratecolumns="false"
                                    commanditemdisplay="None">
                                    <Columns>
                                        <%-- <telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                                        <telerik:GridBoundColumn HeaderText="Branch" DataField="Branch" FooterText="Grand Total:"
                                            UniqueName="Branch" SortExpression="Branch" AutoPostBackOnFilter="true" AllowFiltering="true"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Transaction"
                                            HeaderText="Purchase Cnt" DataField="BUYCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Amount"
                                            HeaderText="Purchase Amt" DataField="BUYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Transaction"
                                            HeaderText="Sell Cnt" DataField="SELCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Amount" HeaderText="Sell Amt"
                                            DataField="SELAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELAmount"
                                            SortExpression="SELAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Count"
                                            HeaderText="DVR Cnt" DataField="DVRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRCount" SortExpression="DVRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Amount"
                                            HeaderText="DVR Amt" DataField="DVRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Count"
                                            HeaderText="DVP Cnt" DataField="DVPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Amount"
                                            HeaderText="DVP Amt" DataField="DVPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPAmount" SortExpression="DVPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Transaction Plan Count"
                                            HeaderText="SIP Cnt" DataField="SIPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Transaction Plan Amount"
                                            HeaderText="SIP Amt" DataField="SIPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Count"
                                            HeaderText="BCI Cnt" DataField="BCICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Amount"
                                            HeaderText="BCI Amt" DataField="BCIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Count"
                                            HeaderText="BCO Cnt" DataField="BCOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Amount"
                                            HeaderText="BCO Amt" DataField="BCOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Count" HeaderText="STB Cnt"
                                            DataField="STBCount" HeaderStyle-HorizontalAlign="Right" UniqueName="STBCount"
                                            SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Amount"
                                            HeaderText="STB Amt" DataField="STBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Count"
                                            HeaderText="STS Cnt" DataField="STSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Amount"
                                            HeaderText="STS Amt" DataField="STSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Count"
                                            HeaderText="SWB Cnt" DataField="SWBCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Amount"
                                            HeaderText="SWB Amt" DataField="SWBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic WithDrawal Plan Count"
                                            HeaderText="SWP Cnt" DataField="SWPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic WithDrawal Plan Amount"
                                            HeaderText="SWP Amt" DataField="SWPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Count"
                                            HeaderText="SWS Cnt" DataField="SWSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Amount"
                                            HeaderText="SWS Amt" DataField="SWSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Count"
                                            HeaderText="PRJ Cnt" DataField="PRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Amount"
                                            HeaderText="PRJ Amt" DataField="PRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <headerstyle width="150px" />
                                <clientsettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </clientsettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr id="trPnlFolio" runat="server">
                <td>
                    <asp:Panel ID="pnlFolio" ScrollBars="Horizontal" Width="100%" runat="server">
                        <div runat="server" id="divFolioWise" style="margin: 2px; width: 640px;">
                            <telerik:RadGrid ID="gvFolioWise" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvFolioWise_OnNeedDataSource"
                                EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                    filename="Folio Details" excel-format="ExcelML">
                                </exportsettings>
                                <mastertableview groupsdefaultexpanded="false" expandcollapsecolumn-groupable="true"
                                    grouploadmode="Client" editmode="EditForms" showgroupfooter="true" width="100%"
                                    allowmulticolumnsorting="True" autogeneratecolumns="false" commanditemdisplay="None">
                                    
                                   <%--     <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="RMName" />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="RMName" FieldAlias="RM" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    
                                      <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="Customer"   />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="Customer" FieldAlias="Customer" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="Folio" />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="Folio" FieldAlias="Folio" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>--%>
                                    
                                    <Columns>
                                        <%--<telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                                       
                                         <telerik:GridBoundColumn HeaderText="RM" HeaderTooltip="RM" DataField="RMName" UniqueName="RMName"
                                            SortExpression="RMName" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn HeaderText="Customer" HeaderTooltip="Customer" DataField="Customer"
                                            FooterText="Grand Total:" UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                      <%--  <telerik:GridBoundColumn HeaderText="Branch" HeaderTooltip="Branch" DataField="BranchName"
                                            UniqueName="BranchName" SortExpression="BranchName" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>--%>
                                      
                                        <telerik:GridBoundColumn HeaderText="Folio" HeaderTooltip="Folio" DataField="Folio"
                                            UniqueName="Folio" SortExpression="Folio" AutoPostBackOnFilter="true" AllowFiltering="true"
                                              Aggregate="Count" FooterText="Row Count : "
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderText="Group" HeaderTooltip="Group" DataField="Parent"
                                            UniqueName="Parent" SortExpression="Parent" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Net Investment"
                                            HeaderText="Net Invest" DataField="Net" HeaderStyle-HorizontalAlign="Right" UniqueName="Net"
                                            SortExpression="Net" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Gross Investment"
                                            HeaderText="Gross Invest" DataField="GrossInvestment" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="GrossInvestment" SortExpression="GrossInvestment" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Gross Redemption"
                                            HeaderText="Gross Redemp" DataField="GrossRedemption" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="GrossRedemption" SortExpression="GrossRedemption" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Transaction"
                                            HeaderText="Purchase Cnt" DataField="BUYCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Amount"
                                            HeaderText="Purchase Amt" DataField="BUYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Count"
                                            HeaderText="DVR Cnt" DataField="DVRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRCount" SortExpression="DVRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Amount"
                                            HeaderText="DVR Amt" DataField="DVRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Count"
                                            HeaderText="SIP Cnt" DataField="SIPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Amount"
                                            HeaderText="SIP Amt" DataField="SIPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Count"
                                            HeaderText="SWB Cnt" DataField="SWBCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Amount"
                                            HeaderText="SWB Amt" DataField="SWBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase Count"
                                            HeaderText="ABY Cnt" DataField="ABYCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="ABYCount" SortExpression="ABYCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase Amount"
                                            HeaderText="ABY Amt" DataField="ABYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="ABYAmount" SortExpression="ABYAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                       
                                       <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Transaction"
                                            HeaderText="Sell Cnt" DataField="SELCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Amount" HeaderText="Sell Amt"
                                            DataField="SELAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELAmount"
                                            SortExpression="SELAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Count"
                                            HeaderText="STS Cnt" DataField="STSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Amount"
                                            HeaderText="STS Amt" DataField="STSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Count"
                                            HeaderText="SWP Cnt" DataField="SWPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Amount"
                                            HeaderText="SWP Amt" DataField="SWPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Count"
                                            HeaderText="SWS Cnt" DataField="SWSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Amount"
                                            HeaderText="SWS Amt" DataField="SWSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Count"
                                            HeaderText="DVP Cnt" DataField="DVPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Amount"
                                            HeaderText="DVP Amt" DataField="DVPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPAmount" SortExpression="DVPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Count"
                                            HeaderText="PRJ Cnt" DataField="PRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Amount"
                                            HeaderText="PRJ Amt" DataField="PRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Count" HeaderText="STB Cnt"
                                            DataField="STBCount" HeaderStyle-HorizontalAlign="Right" UniqueName="STBCount"
                                            SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Amount"
                                            HeaderText="STB Amt" DataField="STBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Count"
                                            HeaderText="BCI Cnt" DataField="BCICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Amount"
                                            HeaderText="BCI Amt" DataField="BCIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection Count"
                                            HeaderText="BIR Cnt" DataField="BIRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BIRCount" SortExpression="BIRCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection Amount"
                                            HeaderText="BIR Amt" DataField="BIRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BIRAmount" SortExpression="BIRAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units Count"
                                            HeaderText="BNS Cnt" DataField="BNSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BNSCount" SortExpression="BNSCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units Amount"
                                            HeaderText="BNS Amt" DataField="BNSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BNSAmount" SortExpression="BNSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation In Count"
                                            HeaderText="CNI Cnt" DataField="CNICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNICount" SortExpression="CNICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation In Amount"
                                            HeaderText="CNI Amt" DataField="CNIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNIAmount" SortExpression="CNIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                     
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In Count"
                                            HeaderText="DSI Cnt" DataField="DSICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSICount" SortExpression="DSICount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In"
                                            HeaderText="DSI Amt" DataField="DSIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSIAmount" SortExpression="DSIAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings Count"
                                            HeaderText="HLD Cnt" DataField="HLDCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="HLDCount" SortExpression="HLDCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings Amount"
                                            HeaderText="HLD Amt" DataField="HLDAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="HLDAmount" SortExpression="HLDAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Initial Allotment Count"
                                            HeaderText="NFO Cnt" DataField="NFOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="NFOCount" SortExpression="NFOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Initial Allotment"
                                            HeaderText="NFO Amt" DataField="NFOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="NFOAmount" SortExpression="NFOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Rejection Count"
                                            HeaderText="RRJ Cnt" DataField="RRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="RRJCount" SortExpression="RRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Rejection"
                                            HeaderText="RRJ Amt" DataField="RRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="RRJAmount" SortExpression="RRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection Count"
                                            HeaderText="SRJ Cnt" DataField="SRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SRJCount" SortExpression="SRJCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection"
                                            HeaderText="SRJ Amt" DataField="SRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SRJAmount" SortExpression="SRJAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Count"
                                            HeaderText="BCO Cnt" DataField="BCOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Amount"
                                            HeaderText="BCO Amt" DataField="BCOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out Count"
                                            HeaderText="DSO Cnt" DataField="DSOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSOCount" SortExpression="DSOCount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out"
                                            HeaderText="DSO Amt" DataField="DSOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSOAmount" SortExpression="DSOAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <headerstyle width="150px" />
                                <clientsettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </clientsettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr id="trPnlRM" runat="server">
                <td>
                    <asp:Panel ID="pnlRM" ScrollBars="Horizontal" Width="100%" runat="server">
                        <div runat="server" id="divRM" style="margin: 2px; width: 640px;">
                            <telerik:RadGrid ID="gvRM" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvRM_OnNeedDataSource"
                                EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                    filename="RM Details" excel-format="ExcelML">
                                </exportsettings>
                                <mastertableview width="100%" allowmulticolumnsorting="True" autogeneratecolumns="false"
                                    commanditemdisplay="None">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="RM" DataField="RM" FooterText="Grand Total:"
                                            UniqueName="RM" SortExpression="RM" AutoPostBackOnFilter="true" AllowFiltering="true"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Transaction"
                                            HeaderText="Purchase Cnt" DataField="BUYCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Amount"
                                            HeaderText="Purchase Amt" DataField="BUYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Transaction"
                                            HeaderText="Sell Cnt" DataField="SELCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Amount" HeaderText="Sell Amt"
                                            DataField="SELAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELAmount"
                                            SortExpression="SELAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Count"
                                            HeaderText="DVR Cnt" DataField="DVRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRCount" SortExpression="DVRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Amount"
                                            HeaderText="DVR Amt" DataField="DVRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Count"
                                            HeaderText="DVP Cnt" DataField="DVPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Amount"
                                            HeaderText="DVP Amt" DataField="DVPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPAmount" SortExpression="DVPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Transaction Plan Count"
                                            HeaderText="SIP Cnt" DataField="SIPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Transaction Plan Amount"
                                            HeaderText="SIP Amt" DataField="SIPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Count"
                                            HeaderText="BCI Cnt" DataField="BCICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Amount"
                                            HeaderText="BCI Amt" DataField="BCIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Count"
                                            HeaderText="BCO Cnt" DataField="BCOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Amount"
                                            HeaderText="BCO Amt" DataField="BCOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Count" HeaderText="STB Cnt"
                                            DataField="STBCount" HeaderStyle-HorizontalAlign="Right" UniqueName="STBCount"
                                            SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Amount"
                                            HeaderText="STB Amt" DataField="STBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Count"
                                            HeaderText="STS Cnt" DataField="STSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Amount"
                                            HeaderText="STS Amt" DataField="STSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Count"
                                            HeaderText="SWB Cnt" DataField="SWBCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Amount"
                                            HeaderText="SWB Amt" DataField="SWBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic WithDrawal Plan Count"
                                            HeaderText="SWP Cnt" DataField="SWPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic WithDrawal Plan Amount"
                                            HeaderText="SWP Amt" DataField="SWPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Count"
                                            HeaderText="SWS Cnt" DataField="SWSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Amount"
                                            HeaderText="SWS Amt" DataField="SWSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Count"
                                            HeaderText="PRJ Cnt" DataField="PRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Amount"
                                            HeaderText="PRJ Amt" DataField="PRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <headerstyle width="150px" />
                                <clientsettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </clientsettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr id="trPnlCategory" runat="server">
                <td>
                    <asp:Panel ID="pnlCategory" ScrollBars="Horizontal" Width="100%" runat="server">
                        <div runat="server" id="divCategory" style="margin: 2px; width: 640px;">
                            <telerik:RadGrid ID="gvCategoryWise" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvCategoryWise_OnNeedDataSource"
                                EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                    filename="Category Details" excel-format="ExcelML">
                                </exportsettings>
                                <mastertableview width="100%" allowmulticolumnsorting="True" autogeneratecolumns="false"
                                    commanditemdisplay="None">
                                    <Columns>
                                        <%--<telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                                        <telerik:GridBoundColumn HeaderText="Category" HeaderTooltip="Category" DataField="Category"
                                            FooterText="Grand Total:" UniqueName="Category" SortExpression="Category" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="SubCategory" HeaderTooltip="SubCategory" DataField="SubCategory"
                                            UniqueName="SubCategory" SortExpression="SubCategory" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Transaction Count"
                                            HeaderText="Purchase Cnt" DataField="BUYCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Amount"
                                            HeaderText="Purchase Amt" DataField="BUYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Transaction"
                                            HeaderText="Sell Cnt" DataField="SELCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Amount" HeaderText="Sell Amt"
                                            DataField="SELAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELAmount"
                                            SortExpression="SELAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Count"
                                            HeaderText="DVR Cnt" DataField="DVRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRCount" SortExpression="DVRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Amount"
                                            HeaderText="DVR Amt" DataField="DVRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Count"
                                            HeaderText="DVP Cnt" DataField="DVPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Amount"
                                            HeaderText="DVP Amt" DataField="DVPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPAmount" SortExpression="DVPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Count"
                                            HeaderText="SIP Cnt" DataField="SIPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Amount"
                                            HeaderText="SIP Amt" DataField="SIPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Count"
                                            HeaderText="BCI Cnt" DataField="BCICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Amount"
                                            HeaderText="BCI Amt" DataField="BCIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Count"
                                            HeaderText="BCO Cnt" DataField="BCOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Amount"
                                            HeaderText="BCO Amt" DataField="BCOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Count" HeaderText="STB Cnt"
                                            DataField="STBCount" HeaderStyle-HorizontalAlign="Right" UniqueName="STBCount"
                                            SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Amount"
                                            HeaderText="STB Amt" DataField="STBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Count"
                                            HeaderText="STS Cnt" DataField="STSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Amount"
                                            HeaderText="STS Amt" DataField="STSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Count"
                                            HeaderText="SWB Cnt" DataField="SWBCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Amount"
                                            HeaderText="SWB Amt" DataField="SWBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Count"
                                            HeaderText="SWP Cnt" DataField="SWPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Amount"
                                            HeaderText="SWP Amt" DataField="SWPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Count"
                                            HeaderText="SWS Cnt" DataField="SWSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Amount"
                                            HeaderText="SWS Amt" DataField="SWSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Count"
                                            HeaderText="PRJ Cnt" DataField="PRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Amount"
                                            HeaderText="PRJ Amt" DataField="PRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <headerstyle width="150px" />
                                <clientsettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </clientsettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr id="trPnlZoneCluster" runat="server">
                <td>
                    <asp:Panel ID="pnlZoneCluster" ScrollBars="Horizontal" Height="400px" runat="server">
                        <div runat="server" id="divZoneWise" style="margin: 2px; width: 640px;">
                            <telerik:RadGrid ID="gvZoneClusterWise" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="20" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowAutomaticInserts="false"
                                ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvZoneClusterWise_OnNeedDataSource">
                                <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                    filename="Zone-Cluster Details" excel-format="ExcelML">
                                </exportsettings>
                                <mastertableview groupsdefaultexpanded="false" expandcollapsecolumn-groupable="true"
                                    grouploadmode="Client" editmode="EditForms" showgroupfooter="true" datakeynames="ZoneClusterId,CategoryCode"
                                    width="100%" allowmulticolumnsorting="True" autogeneratecolumns="false" commanditemdisplay="None">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ZoneName"   />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="ZoneName" FieldAlias="Zone" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ClusterName" />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="ClusterName" FieldAlias="Cluster" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="BranchName" />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="BranchName" FieldAlias="Branch" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <%--<telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                                        <telerik:GridBoundColumn HeaderText="Zone" HeaderTooltip="Zone Name" DataField="ZoneName"
                                            UniqueName="ZoneName" SortExpression="ZoneName" FooterText="Grand Total:" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Cluster" HeaderTooltip="Cluster Name" DataField="ClusterName"
                                            UniqueName="ClusterName" SortExpression="ClusterName" AutoPostBackOnFilter="true"
                                            
                                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Branch" HeaderTooltip="Branch Name" DataField="BranchName"
                                            UniqueName="BranchName" SortExpression="BranchName" AutoPostBackOnFilter="true"
                                             Aggregate="Count" FooterText="Row Count : "
                                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Category" HeaderTooltip="Category Name" DataField="CategoryName"
                                             UniqueName="CategoryName" SortExpression="CategoryName"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <%--     <telerik:GridBoundColumn HeaderText="CategoryCode" Visible="false" HeaderTooltip="CategoryCode"  DataField="CategoryCode"
                    UniqueName="CategoryCode" SortExpression="CategoryCode" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Net Investment"
                                            HeaderText="Net Invest" DataField="Net" HeaderStyle-HorizontalAlign="Right" UniqueName="Net"
                                            SortExpression="Net" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Gross Investment"
                                            HeaderText="Gross Invest" DataField="GrossInvestment" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="GrossInvestment" SortExpression="GrossInvestment" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Gross Redemption"
                                            HeaderText="Gross Redemp" DataField="GrossRedemption" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="GrossRedemption" SortExpression="GrossRedemption" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Transaction"
                                            HeaderText="Purchase Cnt" DataField="BUYCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Amount"
                                            HeaderText="Purchase Amt" DataField="BUYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Count"
                                            HeaderText="DVR Cnt" DataField="DVRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRCount" SortExpression="DVRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Amount"
                                            HeaderText="DVR Amt" DataField="DVRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Syatematic Investment Plan Count"
                                            HeaderText="SIP Cnt" DataField="SIPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Syatematic Investment Plan Amount"
                                            HeaderText="SIP Amt" DataField="SIPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Count"
                                            HeaderText="SWB Cnt" DataField="SWBCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Amount"
                                            HeaderText="SWB Amt" DataField="SWBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase Count"
                                            HeaderText="ABY Cnt" DataField="AddCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="AddCount" SortExpression="AddCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase"
                                            HeaderText="ABY Amt" DataField="AddAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="AddAmount" SortExpression="AddAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        
                                        
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Transaction"
                                            HeaderText="Sell Cnt" DataField="SELCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Sell Amount" HeaderText="Sell Amt"
                                            DataField="SELAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELAmount"
                                            SortExpression="SELAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Count"
                                            HeaderText="STS Cnt" DataField="STSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Amount"
                                            HeaderText="STS Amt" DataField="STSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Count"
                                            HeaderText="SWP Cnt" DataField="SWPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Amount"
                                            HeaderText="SWP Amt" DataField="SWPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Count"
                                            HeaderText="SWS Cnt" DataField="SWSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Amount"
                                            HeaderText="SWS Amt" DataField="SWSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Count"
                                            HeaderText="DVP Cnt" DataField="DVPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Amount"
                                            HeaderText="DVP Amt" DataField="DVPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPAmount" SortExpression="DVPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Count"
                                            HeaderText="PRJ Cnt" DataField="PRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Amount"
                                            HeaderText="PRJ Amt" DataField="PRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Count" HeaderText="STB Cnt"
                                            DataField="STBCount" HeaderStyle-HorizontalAlign="Right" UniqueName="STBCount"
                                            SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Amount"
                                            HeaderText="STB Amt" DataField="STBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                             </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Count"
                                            HeaderText="BCI Cnt" DataField="BCICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Amount"
                                            HeaderText="BCI Amt" DataField="BCIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection Count"
                                            HeaderText="BIR Cnt" DataField="BIRCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BIRCount" SortExpression="BIRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection"
                                            HeaderText="BIR Amt" DataField="BIRAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BIRAmount" SortExpression="BIRAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units Count"
                                            HeaderText="BNS Cnt" DataField="BNSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BNSCount" SortExpression="BNSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units" HeaderText="BNS Amt"
                                            DataField="BNSAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="BNSAmount"
                                            SortExpression="BNSAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation In Count"
                                            HeaderText="CNI Cnt" DataField="CNICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNICount" SortExpression="CNICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation In"
                                            HeaderText="CNI Amt" DataField="CNIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNIAmount" SortExpression="CNIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In Count"
                                            HeaderText="DSI Cnt" DataField="DSICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSICount" SortExpression="DSICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In"
                                            HeaderText="DSI Amt" DataField="DSIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSIAmount" SortExpression="DSIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings Count"
                                            HeaderText="HLD Cnt" DataField="HLDCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="HLDCount" SortExpression="HLDCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings" HeaderText="HLD Amt"
                                            DataField="HLDAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="HLDAmount"
                                            SortExpression="HLDAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Initial Allotment Count"
                                            HeaderText="NFO Cnt" DataField="NFOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="NFOCount" SortExpression="NFOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Initial Allotment"
                                            HeaderText="NFO Amt" DataField="NFOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="NFOAmount" SortExpression="NFOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Rejection Count"
                                            HeaderText="RRJ Cnt" DataField="RRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="RRJCount" SortExpression="RRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Reinvested Rejection"
                                            HeaderText="RRJ Amt" DataField="RRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="RRJAmount" SortExpression="RRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection Count"
                                            HeaderText="SRJ Cnt" DataField="SRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SRJCount" SortExpression="SRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection"
                                            HeaderText="SRJ Amt" DataField="SRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SRJAmount" SortExpression="SRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Count"
                                            HeaderText="BCO Cnt" DataField="BCOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Amount"
                                            HeaderText="BCO Amt" DataField="BCOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out Count"
                                            HeaderText="CNO Cnt" DataField="CNOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNOCount" SortExpression="CNOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out"
                                            HeaderText="CNO Amt" DataField="CNOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="CNOAmount" SortExpression="CNOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out Count"
                                            HeaderText="DSO Cnt" DataField="DSOCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSOCount" SortExpression="DSOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out"
                                            HeaderText="DSO Amt" DataField="DSOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DSOAmount" SortExpression="DSOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <%-- dd--%>
                                        
                                    </Columns>
                                </mastertableview>
                                <headerstyle width="150px" />
                                <clientsettings reordercolumnsonclient="True" allowcolumnsreorder="True" enablerowhoverstyle="true">
                                    <Resizing AllowColumnResize="true" />
                                    <Selecting AllowRowSelect="true" />
                                </clientsettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="LabelMainNote" runat="server" Font-Size="Small" CssClass="cmbField" 
                    Text="Note:1.To sort on a field click on its label. <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.To know field details browse over the label.<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.You can group/ungroup or hide/unhide fields by a right click on the grid label and then making the selection.<br />
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 4.Gross Investment : BUY,DVR,SIP,SWB,STB,BCI,BIR,BNS,CNI,DSI,HLD,NFO,RRJ. <br />
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.Gross Redemption : SELL,STS,SWP,SWS,SRJ,BCO,CNO,DSO. <br />
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  6.DVP & PRJ : Excluded from both categories."
            
            
                        ></asp:Label>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
        <asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
        <asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
        <asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
         <asp:HiddenField ID="hdnAgentId" runat="server" Visible="false" />
        <asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
        <asp:HiddenField ID="hdnadviserId" runat="server" />
        <asp:HiddenField ID="hdnType" runat="server" />
        <asp:HiddenField ID="hdnFromDate" runat="server" />
        <asp:HiddenField ID="hdnToDate" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAMCExport" />
        <asp:PostBackTrigger ControlID="btnSchemeExport" />
        <asp:PostBackTrigger ControlID="btnFolioExport" />
        <asp:PostBackTrigger ControlID="btnBranchExport" />
        <asp:PostBackTrigger ControlID="btnCategoryExport" />
        <asp:PostBackTrigger ControlID="btnRMExport" />
        <asp:PostBackTrigger ControlID="btnClusterZoneExport" />
    </Triggers>
</asp:UpdatePanel>
