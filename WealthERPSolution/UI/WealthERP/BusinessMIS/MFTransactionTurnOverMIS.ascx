<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFTransactionTurnOverMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.MFTransactionTurnOverMIS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            MF Transaction TurnOver MIS
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgProduct" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="imgProduct_Click"></asp:ImageButton>
                            <asp:ImageButton ID="imgOrganization" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="imgOrganization_Click"></asp:ImageButton>
                            <asp:ImageButton ID="imgMember" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="imgMember_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<tr id="trSelectType" runat="server">
        <td class="leftField">
            <asp:Label ID="lblSelectType" runat="server" CssClass="FieldName" Text="Select Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Associates" Value="Associates"></asp:ListItem>
                <asp:ListItem Text="Branch" Value="BM"></asp:ListItem>
                <asp:ListItem Text="Employee" Value="RM"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CVTrxType" runat="server" ControlToValidate="ddlUserType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an user type"
                Operator="NotEqual" ValidationGroup="vgBtnGo" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lblSelectCode" runat="server" CssClass="FieldName" Text="Select SubBrokerCode:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlSelectType" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
        </td>
    </tr>--%>
<table width="60%">
    <tr>
        <td valign="top" colspan="4" align="left">
            <div id="divDateRange" runat="server" visible="false" style="float: left;">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                &nbsp;<telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                    CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="vgBtnGo"> </asp:RequiredFieldValidator>
                <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                <telerik:RadDatePicker ID="txtToDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
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
    <tr>
        <td>
           &nbsp; <asp:Label ID="lblFilter" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
            <asp:DropDownList ID="ddlFilter" runat="server" CssClass="cmbField">
                <asp:ListItem Value="S">Select</asp:ListItem>
                <asp:ListItem Value="1">Online</asp:ListItem>
                <asp:ListItem Value="0">Offline</asp:ListItem>
                <asp:ListItem Value="2">All</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:Label ID="lblErrorFilter" Text="Please Select a Filter" CssClass="rfvPCG" Visible="false"
                runat="server" Style="color: Red"></asp:Label>
        </td>
        <td align="left" class="rightData" colspan="4" visible="false">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date Type:" Visible="false"></asp:Label>
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" Visible="false" />
            <asp:Label ID="lblPickDate" runat="server" Text="Date Range" CssClass="Field" Visible="false"></asp:Label>
            &nbsp;
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" Visible="false" />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Period" CssClass="Field" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
</td> </tr>
<tr>
    <td colspan="4">
       &nbsp;&nbsp;  <asp:Label ID="lblMis" runat="server" CssClass="FieldName" Text="Select MIS:"></asp:Label>
        <asp:LinkButton ID="lnkBtnSubBrokerCustomer" Text="CUSTOMER/FOLIO" CssClass="LinkButtonsWithoutUnderLine"
            runat="server" ValidationGroup="vgBtnGo" OnClick="lnkBtnSubBrokerCustomer_Click"></asp:LinkButton>
        <span>|</span>
        <%--<asp:LinkButton ID="lnkBtnOrganization" Text="ORGANIZATION" CssClass="LinkButtonsWithoutUnderLine"
                runat="server" ValidationGroup="vgBtnGo" OnClick="lnkBtnOrganization_Click"></asp:LinkButton>
            <span>|</span>--%>
        <asp:LinkButton ID="lnkBtnProduct" Text="PRODUCT" CssClass="LinkButtonsWithoutUnderLine"
            runat="server" ValidationGroup="vgBtnGo" OnClick="lnkBtnProduct_Click"></asp:LinkButton>
    </td>
</tr>
<tr>
    <td colspan="4">
    </td>
</tr>
</table>
<div id="dvSectionHeading" runat="server" class="divSectionHeading" style="vertical-align: middle;
    margin: 2px; padding-top: 2px;">
    <table width="100%" cellspacing="0" cellpadding="0" style="padding-top: 0px;">
        <tr>
            <td style="width: 33%" align="left">
                <asp:Label ID="lblMFMISType" runat="server" CssClass="LinkButtons"></asp:Label>
            </td>
            <td style="width: 34%">
            </td>
            <td style="width: 33%">
            </td>
        </tr>
    </table>
</div>
<table width="100%">
    <tr id="trPnlProduct" runat="server">
        <td>
            <asp:Panel ID="pnlProduct" ScrollBars="Horizontal" runat="server">
                <div runat="server" id="dvProduct" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid ID="gvProduct" runat="server" GridLines="None" AutoGenerateColumns="false"
                        PageSize="15" AllowPaging="True" AllowSorting="true" ShowStatusBar="true" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                        EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvProduct_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Product Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="SchemeCode" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="None" GroupsDefaultExpanded="false"
                            ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" ShowGroupFooter="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="100px" AllowFiltering="false" UniqueName="action"
                                    DataField="action" FooterText="Grand Total:" Visible="false">
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
                                <telerik:GridBoundColumn HeaderTooltip="Scheme" HeaderText="Scheme" DataField="Scheme"
                                    HeaderStyle-Width="350px" UniqueName="Scheme" SortExpression="Scheme" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    Aggregate="Count">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
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
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Is Online" HeaderText="Is Online"
                                    DataField="IsOnline" HeaderStyle-HorizontalAlign="Right" UniqueName="IsOnline"
                                    SortExpression="IsOnline" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    FooterStyle-HorizontalAlign="Right">
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
                                    UniqueName="ABYCount" SortExpression="ABYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase Amount"
                                    HeaderText="ABY Amt" DataField="ABYAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="ABYAmount" SortExpression="ABYAmount" AutoPostBackOnFilter="true"
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
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection Amount"
                                    HeaderText="BIR Amt" DataField="BIRAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="BIRAmount" SortExpression="BIRAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units Count"
                                    HeaderText="BNS Cnt" DataField="BNSCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="BNSCount" SortExpression="BNSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                                    UniqueName="DSICount" SortExpression="DSICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In"
                                    HeaderText="DSI Amt" DataField="DSIAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSIAmount" SortExpression="DSIAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings Count"
                                    HeaderText="HLD Cnt" DataField="HLDCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="HLDCount" SortExpression="HLDCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                                    UniqueName="SRJCount" SortExpression="SRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection"
                                    HeaderText="SRJ Amt" DataField="SRJAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="SRJAmount" SortExpression="SRJAmount" AutoPostBackOnFilter="true"
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
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out Count"
                                    HeaderText="CNO Cnt" DataField="CNOCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="CNOCount" SortExpression="CNOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out"
                                    HeaderText="CNO Amt" DataField="CNOAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="CNOAmount" SortExpression="CNOAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out Count"
                                    HeaderText="DSO Cnt" DataField="DSOCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSOCount" SortExpression="DSOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out"
                                    HeaderText="DSO Amt" DataField="DSOAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSOAmount" SortExpression="DSOAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Width="150px" />
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            <Resizing AllowColumnResize="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
        </td>
    </tr>
    <tr id="trPnlOrganization" runat="server">
        <td>
            <asp:Panel ID="pnlOrganization" ScrollBars="Horizontal" runat="server">
                <div runat="server" id="divOrganization" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid ID="gvOrganization" runat="server" GridLines="None" AutoGenerateColumns="false"
                        PageSize="15" AllowPaging="True" AllowSorting="true" ShowStatusBar="true" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                        EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvOrganization_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Organization Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="customerId" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="None" GroupsDefaultExpanded="false"
                            ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" ShowGroupFooter="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="100px" AllowFiltering="false" UniqueName="action"
                                    DataField="action" FooterText="Grand Total:" Visible="false">
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="ZonalManagerName" HeaderTooltip="ZonalManagerName"
                                    DataField="ZonalManagerName" UniqueName="ZonalManagerName" SortExpression="ZonalManagerName"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterText="Grand Total:">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="AreaManager" DataField="AreaManager" UniqueName="AreaManager"
                                    SortExpression="AreaManager" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="CircleManager" HeaderText="CircleManager"
                                    DataField="CircleManager" HeaderStyle-Width="350px" UniqueName="CircleManager"
                                    SortExpression="CircleManager" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" Aggregate="Count">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ChannelMgr" HeaderTooltip="ChannelMgr" DataField="ChannelMgr"
                                    UniqueName="ChannelMgr" SortExpression="ChannelMgr" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="CustomerName" HeaderTooltip="CustomerName" DataField="CustomerName"
                                    UniqueName="CustomerName" SortExpression="CustomerName" AutoPostBackOnFilter="true"
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
                                    UniqueName="ABYCount" SortExpression="ABYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase Amount"
                                    HeaderText="ABY Amt" DataField="ABYAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="ABYAmount" SortExpression="ABYAmount" AutoPostBackOnFilter="true"
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
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection Amount"
                                    HeaderText="BIR Amt" DataField="BIRAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="BIRAmount" SortExpression="BIRAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units Count"
                                    HeaderText="BNS Cnt" DataField="BNSCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="BNSCount" SortExpression="BNSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                                    UniqueName="DSICount" SortExpression="DSICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In"
                                    HeaderText="DSI Amt" DataField="DSIAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSIAmount" SortExpression="DSIAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings Count"
                                    HeaderText="HLD Cnt" DataField="HLDCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="HLDCount" SortExpression="HLDCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                                    UniqueName="SRJCount" SortExpression="SRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection"
                                    HeaderText="SRJ Amt" DataField="SRJAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="SRJAmount" SortExpression="SRJAmount" AutoPostBackOnFilter="true"
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
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out Count"
                                    HeaderText="CNO Cnt" DataField="CNOCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="CNOCount" SortExpression="CNOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out"
                                    HeaderText="CNO Amt" DataField="CNOAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="CNOAmount" SortExpression="CNOAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out Count"
                                    HeaderText="DSO Cnt" DataField="DSOCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSOCount" SortExpression="DSOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out"
                                    HeaderText="DSO Amt" DataField="DSOAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSOAmount" SortExpression="DSOAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Width="150px" />
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            <Resizing AllowColumnResize="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
        </td>
    </tr>
    <tr id="trMember" runat="server">
        <td>
            <asp:Panel ID="pnlMember" ScrollBars="Horizontal" runat="server">
                <div runat="server" id="divMember" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid ID="gvMember" runat="server" GridLines="None" AutoGenerateColumns="false"
                        PageSize="15" AllowPaging="True" AllowSorting="true" ShowStatusBar="true" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                        EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvMember_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Member Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="AccountId" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="None" GroupsDefaultExpanded="false"
                            ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" ShowGroupFooter="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="100px" AllowFiltering="false" UniqueName="action"
                                    DataField="action" FooterText="Grand Total:" Visible="false">
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="CustomerName" HeaderTooltip="CustomerName" DataField="CustomerName"
                                    UniqueName="CustomerName" SortExpression="CustomerName" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterText="Grand Total:">
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
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Is Online" HeaderText="Is Online"
                                    DataField="IsOnline" HeaderStyle-HorizontalAlign="Right" UniqueName="IsOnline"
                                    SortExpression="IsOnline" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    FooterStyle-HorizontalAlign="Right">
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
                                    UniqueName="ABYCount" SortExpression="ABYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Additional Purchase Amount"
                                    HeaderText="ABY Amt" DataField="ABYAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="ABYAmount" SortExpression="ABYAmount" AutoPostBackOnFilter="true"
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
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker In Rejection Amount"
                                    HeaderText="BIR Amt" DataField="BIRAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="BIRAmount" SortExpression="BIRAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Bonus Units Count"
                                    HeaderText="BNS Cnt" DataField="BNSCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="BNSCount" SortExpression="BNSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                                    UniqueName="DSICount" SortExpression="DSICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep In"
                                    HeaderText="DSI Amt" DataField="DSIAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSIAmount" SortExpression="DSIAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Holdings Count"
                                    HeaderText="HLD Cnt" DataField="HLDCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="HLDCount" SortExpression="HLDCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                                    UniqueName="SRJCount" SortExpression="SRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Redemption Rejection"
                                    HeaderText="SRJ Amt" DataField="SRJAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="SRJAmount" SortExpression="SRJAmount" AutoPostBackOnFilter="true"
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
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out Count"
                                    HeaderText="CNO Cnt" DataField="CNOCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="CNOCount" SortExpression="CNOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Consolidation Out"
                                    HeaderText="CNO Amt" DataField="CNOAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="CNOAmount" SortExpression="CNOAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out Count"
                                    HeaderText="DSO Cnt" DataField="DSOCount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSOCount" SortExpression="DSOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Sweep Out"
                                    HeaderText="DSO Amt" DataField="DSOAmount" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="DSOAmount" SortExpression="DSOAmount" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="SubBrokerCode" DataField="SubBrokerCode" UniqueName="SubBrokerCode"
                                    SortExpression="SubBrokerCode" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="SubBroker Name" HeaderText="SubBroker Name"
                                    DataField="AssociatesName" HeaderStyle-Width="80px" UniqueName="AssociatesName"
                                    SortExpression="AssociatesName" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%--  Aggregate="Count"--%>
                                <telerik:GridBoundColumn HeaderText="Folio No." HeaderTooltip="Folio No." DataField="Folio"
                                    HeaderStyle-Width="80px" UniqueName="Folio" SortExpression="Folio" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Channel" HeaderText="Channel" DataField="ChannelName"
                                    HeaderStyle-Width="80px" UniqueName="ChannelName" SortExpression="ChannelName"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Titles" HeaderText="Titles" DataField="Titles"
                                    HeaderStyle-Width="80px" UniqueName="Titles" SortExpression="Titles" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Cluster Manager" HeaderText="Cluster Manager"
                                    DataField="ClusterManager" HeaderStyle-Width="80px" UniqueName="ClusterManager"
                                    SortExpression="ClusterManager" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Area Manager" HeaderText="Area Manager" DataField="AreaManager"
                                    HeaderStyle-Width="80px" UniqueName="AreaManager" SortExpression="AreaManager"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Zonal Manager" HeaderText="Zonal Manager"
                                    DataField="ZonalManagerName" HeaderStyle-Width="80px" UniqueName="ZonalManagerName"
                                    SortExpression="ZonalManagerName" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Deputy Head" HeaderText="Deputy Head" DataField="DeputyHead"
                                    HeaderStyle-Width="80px" UniqueName="DeputyHead" SortExpression="DeputyHead"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Width="150px" />
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            <Resizing AllowColumnResize="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnsubBrokerCode" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAgentId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
