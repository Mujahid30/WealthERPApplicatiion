<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfflineCustomersNCDOrderBook.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.OfflineCustomersNCDOrderBook" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            NCD Offline Order Book
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExport_OnClick"
                                Height="25px" Width="25px" Visible="false"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div id="divConditional" runat="server" style="padding-top: 4px">
    <table class="TableBackground" cellpadding="2">
        <tr>
            <td id="td1" runat="server" align="right">
                <asp:Label runat="server" class="FieldName" Text="Order Status:" ID="Label1"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList CssClass="cmbField" ID="ddlOrderStatus" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblIssueName" runat="server" class="FieldName" Text="Issue Name:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList CssClass="cmbField" ID="ddlIssueName" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label runat="server" class="FieldName" Text="Bid Order Status:" ID="Label3"></asp:Label>
            </td>
            <td>
                <asp:DropDownList CssClass="cmbField" ID="ddlBidType" runat="server" AutoPostBack="false">
                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    <asp:ListItem Text="New" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Modified" Value="M"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server">
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
            <td id="tdlblToDate" runat="server" align="right">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
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
            <td id="tdBtnOrder" runat="server">
                <asp:Button ID="btnViewOrder" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnViewOrder"
                    OnClick="btnViewOrder_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="99%" ScrollBars="Horizontal"
    Visible="false">
    <table id="tblCommissionStructureRule" runat="server">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvNCDOrderBook" runat="server" AllowSorting="True" enableloadondemand="True"
                                PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                OnItemDataBound="gvNCDOrderBook_OnItemDataCommand" Skin="Telerik" AllowFilteringByColumn="false"
                                OnNeedDataSource="gvNCDOrderBook_OnNeedDataSource" OnUpdateCommand="gvNCDOrderBook_UpdateCommand">
                                <MasterTableView DataKeyNames="CO_OrderId,AIM_IssueId,Scrip,WTS_TransactionStatusCode,WOS_OrderStepCode,BBAmounttoinvest,CO_IsAuthenticated,WES_Code,WOS_OrderStep,AIM_IsCancelAllowed,AAC_AgentCode,AgenId,BBEndDate,AR_StaffCode,AR_FirstName"
                                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" AllowFilteringByColumn="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="AllDetailslink" runat="server" CommandName="ExpandAllCollapse"
                                                    Font-Underline="False" Font-Bold="true" UniqueName="AllDetailslink" Font-Size="Medium"
                                                    OnClick="btnExpand_Click">+</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                    Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" Visible="false">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbViewDetails" runat="server" CommandName="ViewDetails" Font-Underline="true"
                                                    Font-Bold="false" UniqueName="ViewDetailslink" CssClass="LinkButtons" Text="View"
                                                    Visible="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                            HeaderStyle-Width="110px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                                    Width="110px">
                                                    <Items>
                                                        <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                                        <asp:ListItem Text="View" Value="View" />
                                                        <asp:ListItem Text="Edit" Value="Edit" />
                                                    </Items>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Customer_Name" SortExpression="Customer_Name"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Customer Name" UniqueName="Customer_Name">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AAC_AgentCode" SortExpression="AAC_AgentCode"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Agent Code" UniqueName="AAC_AgentCode">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C_PANNum" SortExpression="C_PANNum" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="PAN Num" UniqueName="C_PANNum">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                       <%-- <telerik:GridBoundColumn DataField="CustomerAssociate" SortExpression="CustomerAssociate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="User Type" UniqueName="CustomerAssociate">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn DataField="CO_BankName" SortExpression="CO_BankName" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Bank Name" UniqueName="CO_BankName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_BankBranchName" SortExpression="CO_BankBranchName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Bank Branch Name"
                                            UniqueName="CO_BankBranchName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="AR_FirstName" SortExpression="AR_FirstName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="BLP Name" UniqueName="AR_FirstName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_ChequeNumber" SortExpression="CO_ChequeNumber"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Cheque Number" UniqueName="CO_ChequeNumber">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_ASBAAccNo" SortExpression="CO_ASBAAccNo" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="ASBA Bank A/c NO" UniqueName="CO_ASBAAccNo" Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="JointHolder" SortExpression="JointHolder" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="JointHolder" UniqueName="JointHolder" Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Nominee" SortExpression="Nominee" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Nominee" UniqueName="Nominee" Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COA_AssociationType" SortExpression="COA_AssociationType"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Type" UniqueName="COA_AssociationType"
                                            Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_PaymentDate" SortExpression="CO_PaymentDate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Cheque Date" UniqueName="CO_PaymentDate">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C_CustCode" SortExpression="C_CustCode" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Account Id" UniqueName="C_CustCode">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Scrip" SortExpression="Scrip" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Issue Name" UniqueName="Scrip">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="AIM_IssueId" SortExpression="AIM_IssueId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="PI_IssuerId" SortExpression="PI_IssuerId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerCode" HeaderStyle-Width="70px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Issuer" UniqueName="PI_IssuerCode" SortExpression="PI_IssuerCode">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="AID_IssueDetailId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="AID_IssueDetailId" SortExpression="AID_IssueDetailId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            AllowFiltering="true" HeaderText="Transaction Date" UniqueName="CO_OrderDate"
                                            SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="true" HeaderText="Order No."
                                            UniqueName="CO_OrderId" SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                            AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="75px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_MaxApplNo" AllowFiltering="true" HeaderText="Application No."
                                            UniqueName="AIM_MaxApplNo" SortExpression="AIM_MaxApplNo" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                            FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BBStartDate" SortExpression="BBStartDate" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Start Date" UniqueName="BBStartDate"
                                            HeaderStyle-Width="77px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BBEndDate" SortExpression="BBEndDate" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                            HeaderText="End Date" UniqueName="BBEndDate" HeaderStyle-Width="77px" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BBAmounttoinvest" AllowFiltering="true" HeaderText="Amount to invest"
                                            UniqueName="BBAmounttoinvest" SortExpression="BBAmounttoinvest" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                            FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="WOS_OrderStep" AllowFiltering="true" HeaderText="Status"
                                            HeaderStyle-Width="70px" UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WES_Code" AllowFiltering="true" HeaderText="ExtractionStatus"
                                            HeaderStyle-Width="70px" UniqueName="WES_Code" SortExpression="WES_Code" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IsCancelAllowed" AllowFiltering="true" HeaderText="IsCancel Allowed"
                                            HeaderStyle-Width="70px" UniqueName="AIM_IsCancelAllowed" SortExpression="AIM_IsCancelAllowed"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="XB_BrokerShortName" SortExpression="XB_BrokerShortName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Broker Name" UniqueName="XB_BrokerShortName">
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
                                        <%--<telerik:GridBoundColumn DataField="UserType" SortExpression="UserType" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="User Type" UniqueName="UserType">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn DataField="CEDA_DPClientId" SortExpression="CEDA_DPClientId"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Beneficiary Account No"
                                            UniqueName="CEDA_DPClientId">
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
                                        <telerik:GridBoundColumn DataField="CO_Remarks" AllowFiltering="true" HeaderText="Remarks"
                                            UniqueName="CO_Remarks" HeaderStyle-Width="160px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" SortExpression="CO_Remarks">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridEditCommandColumn HeaderStyle-Width="60px" UniqueName="MarkAsReject"
                                                EditText="Cancel" CancelText="Cancel" UpdateText="OK" HeaderText="Cancel" Visible="true">
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false">
                                                <itemtemplate>
                                                <tr>
                                                    <td colspan="100%">
                                                        <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                            Width="100%" ScrollBars="Both" Visible="false">
                                                            <telerik:RadGrid ID="gvChildDetails" runat="server" AutoGenerateColumns="False" enableloadondemand="True"
                                                                PageSize="10" AllowPaging="false" EnableEmbeddedSkins="False" GridLines="None"
                                                                ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                                                                AllowFilteringByColumn="false" OnNeedDataSource="gvChildDetails_OnNeedDataSource">
                                                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId,CO_OrderId,AID_IssueDetailId"
                                                                    AutoGenerateColumns="false" Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="AID_IssueDetailName" HeaderStyle-Width="60px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Series" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBTenure" SortExpression="BBTenure" AutoPostBackOnFilter="true"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                                            HeaderText="Tenure (Months)" UniqueName="BBTenure" HeaderStyle-Width="77px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBCouponrate" SortExpression="BBCouponrate" AutoPostBackOnFilter="true"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                                            HeaderText="Coupon rate(%)" UniqueName="BBCouponrate" HeaderStyle-Width="55px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBInterestPaymentFreq" AllowFiltering="false"
                                                                            SortExpression="BBInterestPaymentFreq" HeaderText="Interest Payment Freq" UniqueName="BBInterestPaymentFreq"
                                                                            HeaderStyle-Width="65px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBRenewedcouponrate" AllowFiltering="true" HeaderText="Renewed coupon rate(%)"
                                                                            UniqueName="BBRenewedcouponrate" HeaderStyle-Width="81px" SortExpression="BBRenewedcouponrate">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBFacevalue" AllowFiltering="true" HeaderText="Face value"
                                                                            UniqueName="BBFacevalue" HeaderStyle-Width="77px" SortExpression="BBFacevalue"
                                                                            DataFormatString="{0:N0}">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBYieldatCall" AllowFiltering="true" HeaderText="Yield at Call(%)"
                                                                            UniqueName="BBYieldatCall" HeaderStyle-Width="77px" SortExpression="BBYieldatCall">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBYieldatMaturity" AllowFiltering="true" HeaderText="Yield at Maturity(%)"
                                                                            UniqueName="BBYieldatMaturity" HeaderStyle-Width="77px" SortExpression="BBYieldatMaturity">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBYieldatbuyback" AllowFiltering="true" HeaderText="Yield at buyback(%)"
                                                                            UniqueName="BBYieldatbuyback" HeaderStyle-Width="77px" SortExpression="BBYieldatbuyback">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBLockinPeriod" AllowFiltering="true" HeaderText="Lockin Period"
                                                                            UniqueName="BBLockinPeriod" HeaderStyle-Width="77px" DataFormatString="{0:N0}"
                                                                            SortExpression="BBLockinPeriod">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBCallOption" AllowFiltering="false" HeaderText="Call Option"
                                                                            UniqueName="BBCallOption" HeaderStyle-Width="77px" SortExpression="BBCallOption">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBBuybackfacility" AllowFiltering="false" HeaderText="Buyback facility"
                                                                            ShowFilterIcon="false" UniqueName="BBBuybackfacility" HeaderStyle-Width="77px"
                                                                            SortExpression="BBBuybackfacility" AutoPostBackOnFilter="true">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBQtytoinvest" AllowFiltering="true" HeaderText="Qty to invest"
                                                                            UniqueName="BBQtytoinvest" HeaderStyle-Width="77px" SortExpression="BBQtytoinvest">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BBAmounttoinvest" AllowFiltering="true" HeaderText="Amount to invest"
                                                                            UniqueName="BBAmounttoinvest" HeaderStyle-Width="77px" SortExpression="BBAmounttoinvest">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Channel" AllowFiltering="true" HeaderText="Channel"
                                                                            UniqueName="Channel" SortExpression="Channel" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                                            AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                         <telerik:GridBoundColumn DataField="COID_ExchangeRefrenceNo" AllowFiltering="false"
                                                                            SortExpression="COID_ExchangeRefrenceNo" HeaderText="ExchangeRefrenceNo." UniqueName="COID_ExchangeRefrenceNo"
                                                                            HeaderStyle-Width="65px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ModificationType" AllowFiltering="false" SortExpression="ModificationType"
                                                                            HeaderText="Modification Type." UniqueName="ModificationType" HeaderStyle-Width="65px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </itemtemplate>
                                            </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings FormTableStyle-Height="40%" EditFormType="Template" FormMainTableStyle-Width="300px">
                                        <FormTemplate>
                                            <table style="background-color: White;" border="0">
                                                <tr>
                                                    <td colspan="4">
                                                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                            Order canceling Request
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftField">
                                                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Request No.:"></asp:Label>
                                                    </td>
                                                    <td class="rightField">
                                                        <asp:TextBox ID="txtRejOrderId" runat="server" CssClass="txtField" Style="width: 250px;"
                                                            Text='<%# Bind("CO_OrderId") %>' ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftField">
                                                        <asp:Label ID="Label20" runat="server" Text="Remark:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightField">
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="txtField" Style="width: 250px;"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="Button1" Text="OK" runat="server" CssClass="PCGButton" CommandName="Update"
                                                            ValidationGroup="btnSubmit">
                                                        </asp:Button>

                                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                            CommandName="Cancel"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnOrderStatus" runat="server" />
