<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineAdviserCustomerRMSLog.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineAdviserCustomerRMSLog" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                           Online RMS Log
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="lblProductType" runat="server" CssClass="FieldName" Text="Product:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlProduct_Selectedindexchanged">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Mutual Fund" Value="MF"></asp:ListItem>
                <asp:ListItem Text="IPO" Value="IP"></asp:ListItem>
                <asp:ListItem Text="NCD" Value="FI"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlProduct"
                ErrorMessage="<br />Please select product" CssClass="cvPCG" Display="Dynamic"
                runat="server" InitialValue="Select" ValidationGroup="ViewRMSLog">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="lblOrderType" runat="server" CssClass="FieldName" Text="Order Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList runat="server" CssClass="cmbField" ID="ddlOrderType">
                <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                <asp:ListItem Text="Normal" Value="NML"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td id="tdlblFromDate" runat="server" align="right">
            <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
        </td>
        <td id="tdTxtFromDate" runat="server">
            <telerik:RadDatePicker ID="txtRMSLogFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="dvTransactionDate" runat="server" class="dvInLine">
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtRMSLogFrom"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="ViewRMSLog">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtRMSLogFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
        </td>
        <td id="tdlblToDate" runat="server">
            <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
        </td>
        <td id="tdTxtToDate" runat="server">
            <telerik:RadDatePicker ID="txtRMSLogTo" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="Div1" runat="server" class="dvInLine">
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRMSLogTo"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="ViewRMSLog">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtRMSLogTo" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtRMSLogTo"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtRMSLogFrom" CssClass="cvPCG" ValidationGroup="ViewRMSLog"
                Display="Dynamic">
            </asp:CompareValidator>
        </td>
        <td>
            <asp:Button ID="btnViewRMSLog" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="ViewRMSLog"
                OnClick="btnViewRMSLog_click" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlRMSLog" runat="server" class="Landscape" Width="80%" ScrollBars="Horizontal"
    Visible="false">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvRMSLog" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" ClientSettings-AllowColumnsReorder="true"
                    AllowAutomaticInserts="false" OnNeedDataSource="gvCommMgmt_OnNeedDataSource"
                    AllowFilteringByColumn="true">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None" AllowFilteringByColumn="true">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="Product" AllowFiltering="false" HeaderText="Product"
                                UniqueName="Product" SortExpression="Product" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="40px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_RMSDebitRequestTime" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                AllowFiltering="false" HeaderText="Request Date/Time" UniqueName="CO_RMSDebitRequestTime"
                                SortExpression="CO_RMSDebitRequestTime" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_RMSDebitResponseTime" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                AllowFiltering="false" HeaderText="Response Date/Time" UniqueName="CO_RMSDebitResponseTime"
                                SortExpression="CO_RMSDebitResponseTime" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_RMSResponseMessage" AllowFiltering="false"
                                HeaderText="Message" HeaderStyle-Width="120px" UniqueName="CO_RMSResponseMessage"
                                SortExpression="CO_RMSResponseMessage" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_RMSReferenceNumber" AllowFiltering="false"
                                HeaderText="Reference Number" HeaderStyle-Width="65px" UniqueName="CO_RMSReferenceNumber"
                                SortExpression="CO_RMSReferenceNumber" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
