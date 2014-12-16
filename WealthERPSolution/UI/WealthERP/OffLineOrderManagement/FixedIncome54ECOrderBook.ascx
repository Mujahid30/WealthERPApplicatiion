<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedIncome54ECOrderBook.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.FixedIncome54ECOrderBook" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            54EC Orde Book
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
        <td align="right">
            <asp:Label ID="lblIssue" runat="server" Text="Issue Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssue" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName" Text="From:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderFrom" CssClass="txtField" runat="server" Culture="English (United States)"
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
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtOrderFrom"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
        </td>
        <td align="right">
            <asp:Label ID="lblTo" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderTo" CssClass="txtField" runat="server" Culture="English (United States)"
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrderTo"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderTo" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtOrderTo"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtOrderFrom" CssClass="cvPCG" ValidationGroup="btnViewOrder"
                Display="Dynamic">
            </asp:CompareValidator>
        </td>
        <td align="right">
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnViewOrder"
                OnClick="btnGo_OnClick" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="99%" ScrollBars="Horizontal"
    Visible="false">
    <table id="tblCommissionStructureRule" runat="server">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gv54FDOrderBook" runat="server" AllowSorting="True" enableloadondemand="True"
                                PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                Skin="Telerik" AllowFilteringByColumn="false" OnNeedDataSource="gv54FDOrderBook_OnNeedDataSource">
                                <MasterTableView DataKeyNames="CO_OrderId,AIM_IssueId,C_CustomerId,AAC_AgentCode,AssociatesName" Width="100%" AllowMultiColumnSorting="True"
                                    AutoGenerateColumns="false" AllowFilteringByColumn="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                            HeaderStyle-Width="110px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                                    Width="110px">
                                                    <Items>
                                                        <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                                        <asp:ListItem Text="View" Value="View" />
                                                        <asp:ListItem Text="Edit" Value="Edit" Enabled="false"/>
                                                    </Items>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="NAME" SortExpression="NAME" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Customer Name" UniqueName="NAME">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AAC_AgentCode" SortExpression="AAC_AgentCode"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="SubBroker Code" UniqueName="AAC_AgentCode">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C_PANNum" SortExpression="C_PANNum" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="PAN Num" UniqueName="C_PANNum">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_CreatedBy" SortExpression="CO_CreatedBy" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Created By" UniqueName="CO_CreatedBy">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_CreatedOn" SortExpression="CO_CreatedOn" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Created On" UniqueName="CO_CreatedOn">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_ModifiedBy" SortExpression="CO_ModifiedBy"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Modified By" UniqueName="CO_ModifiedBy">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_ModifiedOn" SortExpression="CO_ModifiedOn"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Modified On" UniqueName="CO_ModifiedOn">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CustomerAssociate" SortExpression="CustomerAssociate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="User Type" UniqueName="CustomerAssociate">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C_CustCode" SortExpression="C_CustCode" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Account Id" UniqueName="C_CustCode">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IssueName" SortExpression="AIM_IssueName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Scrip Name" UniqueName="AIM_IssueName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="AIM_IssueId" SortExpression="AIM_IssueId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            AllowFiltering="true" HeaderText="Transaction Date" UniqueName="CO_OrderDate"
                                            SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="true" HeaderText="Transaction No."
                                            UniqueName="CO_OrderId" SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="75px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Orderstep" AllowFiltering="true" HeaderText="Status"
                                            HeaderStyle-Width="70px" UniqueName="Orderstep" SortExpression="Orderstep" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CFIOD_TransactionType" AllowFiltering="true"
                                            HeaderText="TransactionType" HeaderStyle-Width="70px" UniqueName="CFIOD_TransactionType"
                                            SortExpression="CFIOD_TransactionType" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DeputyHead" SortExpression="DeputyHead" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Deputy Manager" UniqueName="DeputyHead">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ZonalManagerName" SortExpression="ZonalManagerName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Zonal Manager" UniqueName="ZonalManagerName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AreaManager" SortExpression="AreaManager" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Area Manager" UniqueName="AreaManager">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AssociatesName" SortExpression="AssociatesName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Associates Name"
                                            UniqueName="AssociatesName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ClusterManager" SortExpression="ClusterManager"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Cluster Manager"
                                            UniqueName="ClusterManager">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ChannelName" SortExpression="ChannelName" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Channel Name" UniqueName="ChannelName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Titles" SortExpression="Titles" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Titles" UniqueName="Titles">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReportingManagerName" SortExpression="ReportingManagerName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="ReportingManagerName"
                                            UniqueName="ReportingManagerName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UserType" SortExpression="UserType" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="User Type" UniqueName="UserType">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
