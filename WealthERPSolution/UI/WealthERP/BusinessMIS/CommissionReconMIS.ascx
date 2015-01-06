<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionReconMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.CommissionReconMIS" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Expected Commission/Payable MIS
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="90%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr>
        <td class="leftField" width="16%">
            <asp:Label ID="lblSearchType" runat="server" Text="Select  Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="rightField">
            <asp:DropDownList ID="ddlSearchType" AutoPostBack="true" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlSearchType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Commission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField" width="20%">
            <asp:Label ID="lblSelectProduct" runat="server" Text="Select product:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Product type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField" width="20%" id="tdCategory" runat="server" visible="false">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right" id="tdDdlCategory" runat="server" visible="false">
            <asp:DropDownList ID="ddlProductCategory" runat="server" AutoPostBack="false" CssClass="cmbField">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddlProductCategory"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Category type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trSelectProduct" runat="server">
        <td align="left" class="leftField" width="20%">
            <asp:Label ID="Label2" runat="server" Text="Order Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="EXECUTED" Value="IP"> </asp:ListItem>
                <asp:ListItem Text="ORDERED" Value="IP"> </asp:ListItem>
                <asp:ListItem Text="ACCEPTED" Value="OR"></asp:ListItem>
                <asp:ListItem Text="ALLOTTED" Value="OR"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddlOrderStatus"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select Order Status"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField" width="16%">
            <asp:Label ID="lblOffline" runat="server" Text="Select Transaction Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSelectMode" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Both" Value="2">
                </asp:ListItem>
                <asp:ListItem Text="Online" Value="1"></asp:ListItem>
                <asp:ListItem Text="Offline" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="left" class="leftField" width="16%">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Select Commission Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCommType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Upfront" Value="UF"></asp:ListItem>
                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
                <asp:ListItem Text="Incentive" Value="IN"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlCommType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Commission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trNCDIPO" runat="server" visible="false">
        <td align="left" class="leftField">
            <asp:Label ID="lblIssueType" runat="server" CssClass="FieldName" Text="Select Issue Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlIssueType_OnSelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Closed Issues" Value="2"></asp:ListItem>
                <asp:ListItem Text="Current Issues" Value="1"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlIssueType" runat="server" ControlToValidate="ddlIssueType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select Issue Type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select" Enabled="false"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblIssueName" runat="server" CssClass="FieldName" Text="Select Issue Name"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trSelectMutualFund" runat="server" visible="false">
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select Product Type"></asp:CompareValidator>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlScheme"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <%--<tr id="trIssue" runat="server"  visible="false">
    <td>
    <asp:Label ID="lblIssue" runat="server"  Text="Select Issue:" CssClass="FieldName"></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="ddlIssueName" runat="server" AutoPostBack="true" CssClass="cmbField"></asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlIssueName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Issue" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
    </td>
    </tr>--%>
    <tr>
        <td id="tdFromDate" class="leftField" runat="server" visible="false">
            <asp:Label ID="lblPeriod" Text="Select Month/Quarter:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" id="tdFrom" runat="server" visible="false">
            <asp:DropDownList ID="ddlMnthQtr" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlMnthQtr"
                CssClass="rfvPCG" ErrorMessage="<br />Please Select Month" Display="Dynamic"
                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" id="tdTolbl" runat="server" visible="false">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Year:"></asp:Label>
        </td>
        <td class="rightField" id="tdToDate" runat="server" visible="false">
            <asp:DropDownList ID="ddlYear" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlYear"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Year" Display="Dynamic"
                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <td class="rightField" style="padding-right: 50px">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="GdBind_Click"
                Text="GO" ValidationGroup="vgbtnSubmit" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblIllegal" runat="server" CssClass="Error" Text="" />
        </td>
    </tr>
</table>
<div style="width: 100%; overflow: scroll;" runat="server" visible="false" id="dvMfMIS">
    <telerik:RadGrid ID="gvCommissionReceiveRecon" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableViewState="true" EnableEmbeddedSkins="false" Width="150%"
        AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
        EnableHeaderContextMenu="true" OnItemDataBound="gvWERPTrans_ItemDataBound" EnableHeaderContextFilterMenu="true"
        OnNeedDataSource="gvCommissionReceiveRecon_OnNeedDataSource">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true" DataKeyNames="CMFT_MFTransId,totalNAV,perDayTrail,PerDayAssets">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Sub Broker Code" DataField="CMFT_SubBrokerCode"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CMFT_SubBrokerCode" SortExpression="CMFT_SubBrokerCode"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Folio No" DataField="CMFA_FolioNum"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="20%" HeaderText="SchemePlan" DataField="schemeplanname"
                    UniqueName="schemeplanname" SortExpression="schemeplanname" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Transaction Date" DataField="transactiondate"
                    UniqueName="transactiondate" SortExpression="transactiondate" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Amount" DataField="amount"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="amount" SortExpression="amount"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Transaction Type" DataField="transactiontype"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="transactiontype" SortExpression="transactiontype"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Age" DataField="Age"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Age" SortExpression="Age" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Current Value" DataField="currentvalue"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="currentvalue" SortExpression="currentvalue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" HeaderText="Commission Amount Received"
                    DataField="receivedamount" HeaderStyle-HorizontalAlign="Right" UniqueName="receivedamount"
                    SortExpression="receivedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                    Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="20px" HeaderText="Cumulative Nav" DataField="totalNAV"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="totalNAV" SortExpression="totalNAV"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="20px" HeaderText="PerDayAssets" DataField="PerDayAssets"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="PerDayAssets" SortExpression="PerDayAssets"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="20px" HeaderText="PerDayTrail" DataField="perDayTrail"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="perDayTrail" SortExpression="perDayTrail"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Commission Amount Expected"
                    DataField="expectedamount" HeaderStyle-HorizontalAlign="Right" UniqueName="expectedamount"
                    SortExpression="expectedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="160px" HeaderText="Commission Adjustment Amount"
                    DataField="CMFT_ReceivedCommissionAdjustment" SortExpression="CMFT_ReceivedCommissionAdjustment"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAdjustAmount" CssClass="txtField" runat="server" Text='<%# Eval("adjust").ToString()%>'></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtAdjustAmountMultiple" CssClass="txtField" runat="server" />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Amount Difference"
                    DataField="diff" HeaderStyle-HorizontalAlign="Right" UniqueName="diff" SortExpression="diff"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Commission Updated Expected Amount"
                    DataField="UpdatedExpectedAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="UpdatedExpectedAmount"
                    SortExpression="UpdatedExpectedAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                    Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Recon Status" DataField="ReconStatus"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="ReconStatus" SortExpression="ReconStatus"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Rate" DataField="ACSR_BrokerageValue"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="ACSR_BrokerageValue" SortExpression="ACSR_BrokerageValue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Units" DataField="units"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="units" SortExpression="units"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div id="dvNCDIPOMIS" style="width: 100%; overflow: scroll;" runat="server" visible="false">
    <telerik:RadGrid ID="rgNCDIPOMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableViewState="true" EnableEmbeddedSkins="false" Width="100%"
        AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
        EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" OnNeedDataSource="rgNCDIPOMIS_OnNeedDataSource">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Customer Name" DataField="CustomerName"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CustomerName" SortExpression="CustomerName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Order No." DataField="CO_OrderId"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CO_OrderId" SortExpression="CO_OrderId"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Transaction Date." DataField="transactionDate"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="transactionDate" SortExpression="transactionDate"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="ApplicationNo." DataField="CO_ApplicationNumber"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Status" DataField="WOS_OrderStep"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Sub Broker Code" DataField="AAC_AgentCode"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Associates Name" DataField="AA_ContactPersonName"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="User Type" DataField="CustomerAssociate"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CustomerAssociate" SortExpression="CustomerAssociate"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="20%" HeaderText="Issue Name" DataField="AIM_IssueName"
                    UniqueName="AIM_IssueName" SortExpression="AIM_IssueName" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Alloted Quantity" DataField="allotedQty"
                    UniqueName="allotedQty" SortExpression="allotedQty" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Holdings Amount" DataField="holdings"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="holdings" SortExpression="holdings"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Brokage Rate" DataField="rate"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="rate" SortExpression="rate" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Expected Commission Value"
                    DataField="borkageExpectedvalue" HeaderStyle-HorizontalAlign="Right" UniqueName="borkageExpectedvalue"
                    SortExpression="borkageExpectedvalue" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<asp:HiddenField ID="hdnschemeId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnSBbrokercode" runat="server" />
<asp:HiddenField ID="hdnIssueId" runat="server" />
<asp:HiddenField ID="hdnProductCategory" runat="server" />
