<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BulkRequestManagement.ascx.cs"
    Inherits="WealthERP.UploadBackOffice.BulkRequestManagement" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 18%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
</style>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Bulk Request Management
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<telerik:RadTabStrip ID="RadTabStripBulkOrderRequest" runat="server" EnableTheming="True"
    Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="multipageBulkOrderRequest"
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Request Bulk Order" Value="RequestBulkOrder"
            TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Bulk Order Status" Value="BulkOrderStatus">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="multipageBulkOrderRequest" EnableViewState="true" runat="server">
    <telerik:RadPageView ID="rpvRequestBulkOrder" runat="server">
        <asp:Panel ID="panelRequestBulkOrder" runat="server">
            <table width="100%">
                <tr>
                    <td width="100%" align="center">
                        <div id="msgUploadComplete" runat="server" class="success-msg" align="center" visible="false">
                            Uploading successfully Completed
                        </div>
                    </td>
                </tr>
            </table>
            <table width="60%" runat="server" id="Table1">
                <tr>
                    <td class="leftLabel">
                        <asp:Label ID="Label1" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlSelectType" runat="server" CssClass="cmbField" AutoPostBack="true"
                            Width="205px" OnSelectedIndexChanged="Product_SelectedIndexChanged" InitialValue="Select"
                            TabIndex="1">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem Value="FI">Order Book NCD</asp:ListItem>
                            <asp:ListItem Value="IP">Order Book IPO</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span1" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvType1" runat="server" ErrorMessage="Please Select Order Type"
                            CssClass="rfvPCG" ControlToValidate="ddlSelectType" ValidationGroup="btnGo1"
                            Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trSelectIssueRow" visible="false">
                    <td class="leftLabel">
                        <asp:Label ID="Label3" runat="server" Text="Select Issue:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlSelectIssue" runat="server" CssClass="cmbField" AutoPostBack="true"
                            Width="205px" Visible="false" TabIndex="2">
                        </asp:DropDownList>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvIssue" runat="server" ErrorMessage="Please Select Issue"
                            CssClass="rfvPCG" ControlToValidate="ddlSelectIssue" ValidationGroup="btnGo1"
                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="tr2" runat="server">
                    <td class="leftLabel">
                        <asp:Button ID="btnGo1" runat="server" Text="Go" TabIndex="3" CssClass="PCGButton"
                            ValidationGroup="btnGo1" OnClick="btnGo1_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="rightData">
                        &nbsp;
                    </td>
                    <td class="leftLabel">
                        &nbsp;
                    </td>
                    <td class="rightData">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvBulkOrderStatus" runat="server">
        <asp:Panel ID="panelBulkOrderStatus" runat="server">
            <table width="100%">
                <tr>
                    <td width="100%" align="center">
                        <div id="msgNoRecords" runat="server" class="success-msg" align="center" visible="false">
                        </div>
                    </td>
                </tr>
            </table>
            <table width="90%" runat="server" id="tbIssueType">
                <tr>
                    <td class="leftLabel">
                        <asp:Label ID="lbtype" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" AutoPostBack="true"
                            Width="205px" InitialValue="Select">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem Value="FI">Order Book NCD</asp:ListItem>
                            <asp:ListItem Value="IP">Order Book IPO</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span2" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvType2" runat="server" ErrorMessage="Please Select Order Type"
                            CssClass="rfvPCG" ControlToValidate="ddlIssueType" ValidationGroup="btnGo2" Display="Dynamic"
                            InitialValue="Select"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftLabel">
                        <asp:Label ID="lbFromdate" runat="server" Text="From Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <telerik:RadDatePicker ID="txtReqFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            TabIndex="3" Width="150px" AutoPostBack="true">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span18" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Requested Date"
                            Display="Dynamic" ControlToValidate="txtReqFromDate" InitialValue="" ValidationGroup="btnGo2">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="leftLabel">
                        <asp:Label ID="lblToDate" runat="server" Text="To Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <telerik:RadDatePicker ID="txtReqToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            TabIndex="3" Width="150px" AutoPostBack="true">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span4" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Requested Date"
                            Display="Dynamic" ControlToValidate="txtReqToDate" InitialValue="" ValidationGroup="btnGo2">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvToDate" runat="server" CssClass="rfvPCG" ErrorMessage="To-date should be after From-date"
                            Display="Dynamic" ControlToValidate="txtReqToDate" ControlToCompare="txtReqFromDate"
                            Operator="GreaterThan" ValidationGroup="btnGo2"></asp:CompareValidator>
                    </td>
                    <td class="leftLabel">
                        <asp:Button ID="btnGo2" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo2"
                            OnClick="btnGo2_Click" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellspacing="0" cellpadding="1">
                <tr>
                    <td id="tdBulkOrderStatusList" runat="server">
                        <div id="DivBulkOrderStatusList" runat="server" style="width: 100%; padding-left: 5px;">
                            <telerik:RadGrid ID="gvBulkOrderStatusList" runat="server" AllowAutomaticDeletes="false"
                                EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                                ShowStatusBar="false" ShowFooter="true" AllowPaging="true" AllowSorting="true"
                                GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                                OnNeedDataSource="gvBulkOrderStatusList_OnNeedDataSource" OnItemDataBound="gvBulkOrderStatusList_OnDataBound"
                                OnItemCommand="gvBulkOrderStatusList_OnItemCommand" Visible="true">
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <MasterTableView DataKeyNames="RequestId,FileNamePath" Width="99%" AllowMultiColumnSorting="True"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <telerik:GridButtonColumn CommandName="Download" Text="Download" HeaderText="File download"
                                            HeaderStyle-Width="47px" UniqueName="Download">
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="true" VerticalAlign="top" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="RequestId" UniqueName="RequestId" HeaderText="Request Id"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                            SortExpression="RequestId" FilterControlWidth="50px">
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="true" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RequestDateTime" UniqueName="RequestDateTime"
                                            HeaderText="Request DateTime" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="RequestDateTime"
                                            FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IssueType" UniqueName="IssueType" HeaderText="Issue Type"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                            SortExpression="IssueType" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IssueName" UniqueName="IssueName" HeaderText="Issue Name"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                            SortExpression="IssueName" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StatusMsg" UniqueName="StatusMsg" HeaderText="Status"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                            SortExpression="StatusMsg" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FileNamePath" UniqueName="FileNamePath" HeaderText="FileName & Path"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                            SortExpression="FileNamePath" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="false" ScrollHeight="380px" />
                                    <Resizing AllowColumnResize="true" />
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
