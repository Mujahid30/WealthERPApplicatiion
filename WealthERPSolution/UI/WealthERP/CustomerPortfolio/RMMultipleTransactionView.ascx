<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMMultipleTransactionView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.RMMultipleTransactionView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scptMgr" runat="server" AsyncPostBackTimeout="600">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtcustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }

    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
    }
</script>

<script type="text/javascript" language="javascript">
    function CheckOne(obj) {
        var grid = obj.parentNode.parentNode.parentNode;
        var inputs = grid.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type == "checkbox") {
                if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                    inputs[i].checked = false;
                }
            }
        }
    }
</script>

<script type="text/javascript" language="javascript">
    function CheckOneForRadWindow(obj) {
        var grid = obj.parentNode.parentNode.parentNode;
        var inputs = grid.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type == "checkbox") {
                if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                    inputs[i].checked = false;
                }
            }
        }
    }
</script>

<script type="text/javascript">
    function RowCount() {
        var grid = $find("<%=gvMFTransactions.ClientID %>");
        var MasterTable = grid.get_masterTableView();
        var Rows = MasterTable.get_dataItems();
        if (Rows.length > 7000) {
            alert('One time select Only 7000');
        }

    } 
</script>

<script type="text/javascript" language="javascript">
    function GetParentCustomerId(source, eventArgs) {
        document.getElementById("<%= txtParentCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };


    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvTrail.Items.Count %>');
        var gvControl = document.getElementById('<%= gvTrail.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "cbRecons";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxWerpAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<script language="javascript" type="text/javascript">
    function goBack() {
        window.history.back()
    }
</script>

<style type="text/css" media="print">
    ..noDisplay
    {
    }
    .noPrint
    {
        display: none;
    }
    .landScape
    {
        width: 100%;
        height: 100%;
        margin: 0% 0% 0% 0%;
        filter: progid:DXImageTransform.Microsoft.BasicImage(Rotation=3);
    }
    .pageBreak
    {
        page-break-before: always;
    }
</style>

<script type="text/javascript">
    function ShowPopup() {
        var i = 0;
        var form = document.forms[0];
        var folioId = "";
        var count = 0;
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                }
            }
        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        </td> </tr>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <telerik:RadWindow ID="radwindowForManualMerge" EnableShadow="false" Modal="false"
                        runat="server" InitialBehaviors="None" VisibleStatusbar="false" Title="Link transaction to Trail"
                        Behaviors="Move,close" Width="570px" Height="420px">
                        <ContentTemplate>
                            <div style="padding: 20px">
                                <asp:ImageButton ID="btnExportTrnxToBeMerged" ImageUrl="~/Images/Export_Excel.png"
                                    Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                    OnClientClick="setFormat('excel')" Height="23px" Width="25px" OnClick="btnExportTrnxToBeMerged_Click">
                                </asp:ImageButton>
                            </div>
                            <div style="padding: 20px">
                                <telerik:RadGrid ID="gvManualMerge" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="false"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="500px" AllowFilteringByColumn="false"
                                    OnNeedDataSource="gvManualMerge_NeedDataSource" AllowAutomaticInserts="false"
                                    ExportSettings-FileName="Nominee Details">
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="CMFA_AccountId,CMFT_MFTransId" NoDetailRecordsText="Records not found"
                                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                    <input style="display: none" id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox"
                                                        onclick="checkAllBoxes()" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbRecons" runat="server" onclick="CheckOneForRadWindow(this)" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_MFTransId" HeaderText="MFTransId" AllowFiltering="true"
                                                HeaderStyle-Wrap="false" SortExpression="CMFT_MFTransId" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_MFTransId"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFA_AccountId" HeaderText="AccountId" AllowFiltering="true"
                                                SortExpression="CMFA_AccountId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                AutoPostBackOnFilter="true" UniqueName="CMFA_AccountId" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ADUL_ProcessId" HeaderText="ProcessId" AllowFiltering="true"
                                                SortExpression="ADUL_ProcessId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                AutoPostBackOnFilter="true" UniqueName="ADUL_ProcessId" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PASP_SchemePlanCode" HeaderText="SchemePlanCode"
                                                AllowFiltering="true" SortExpression="PASP_SchemePlanCode" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PASP_SchemePlanCode"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_TransactionDate" HeaderText="TransactionDate"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_TransactionDate"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="CMFT_TransactionDate" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_BuySell" HeaderText="BuySell" AllowFiltering="true"
                                                HeaderStyle-Wrap="false" SortExpression="CMFT_BuySell" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_BuySell"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_DividendRate" HeaderText="DividendRate"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_DividendRate"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="CMFT_DividendRate" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_TransactionNumber" HeaderText="TransactionNumber"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="Transaction Type"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="CMFT_TransactionNumber" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_NAV" HeaderText="NAV" AllowFiltering="true"
                                                AllowSorting="true" HeaderStyle-Wrap="false" SortExpression="CMFT_NAV" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_NAV"
                                                FooterStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_Price" HeaderText="Price (Rs)" AllowFiltering="true"
                                                SortExpression="CMFT_Price" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                HeaderStyle-Wrap="false" AutoPostBackOnFilter="true" UniqueName="CMFT_Price"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_Amount" HeaderText="Amount" AllowFiltering="true"
                                                HeaderStyle-Wrap="false" SortExpression="CMFT_Amount" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_Amount"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_Units" HeaderText="Units" AllowFiltering="true"
                                                HeaderStyle-Wrap="false" SortExpression="CMFT_Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                AutoPostBackOnFilter="true" UniqueName="CMFT_Units" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_STT" HeaderText="STT(Rs)" AllowFiltering="true"
                                                HeaderStyle-Wrap="false" SortExpression="CMFT_STT" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                AutoPostBackOnFilter="true" UniqueName="CMFT_STT" FooterStyle-HorizontalAlign="Right"
                                                DataFormatString="{0:n2}" Aggregate="Sum">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="XES_SourceCode" HeaderText="SourceCode" AllowFiltering="true"
                                                HeaderStyle-Wrap="false" SortExpression="XES_SourceCode" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="XES_SourceCode"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WMTT_TransactionClassificationCode" HeaderText="TransactionClassificationCode"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="WMTT_TransactionClassificationCode"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="WMTT_TransactionClassificationCode" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_ARNNumber" HeaderText="ARNNumber" AllowFiltering="true"
                                                HeaderStyle-Wrap="false" SortExpression="CMFT_ARNNumber" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_ARNNumber"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_ExternalBrokeragePer" HeaderText="ExternalBrokeragePer"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_ExternalBrokeragePer"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="CMFT_ExternalBrokeragePer"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_InternalBrokeragePer" HeaderText="InternalBrokeragePer"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_InternalBrokeragePer"
                                                ShowFilterIcon="false" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="CMFT_InternalBrokeragePer" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_InternalBrokerageAmount" HeaderText="InternalBrokerageAmount"
                                                AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CMFT_InternalBrokerageAmount"
                                                ShowFilterIcon="false" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="CMFT_InternalBrokerageAmount" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_ExternalBrokerageAmount" HeaderText="ExternalBrokerageAmount"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_ExternalBrokerageAmount"
                                                ShowFilterIcon="false" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="CMFT_ExternalBrokerageAmount" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WTS_TransactionStatusCode" HeaderText="TransactionStatusCode"
                                                AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="WTS_TransactionStatusCode"
                                                ShowFilterIcon="false" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="WTS_TransactionStatusCode" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_TransactionStatusChangeDate" HeaderText="TransactionStatusChangeDate"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_TransactionStatusChangeDate"
                                                ShowFilterIcon="false" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="CMFT_TransactionStatusChangeDate" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CMFT_OriginalTransactionNumber" HeaderText="OriginalTransactionNumber"
                                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_OriginalTransactionNumber"
                                                ShowFilterIcon="false" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                UniqueName="CMFT_OriginalTransactionNumber" FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                            <div style="padding: 20px">
                                <asp:Button ID="Button1" runat="server" CssClass="PCGButton" Text="Merge" OnClick="btnManualMerge_Click" />
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 100%;">
                                <div class="divPageHeading">
                                    <table cellspacing="0" cellpadding="3" width="100%">
                                        <tr>
                                            <td align="left">
                                                View MF Transaction
                                            </td>
                                            <td align="right">
                                                <asp:LinkButton runat="server" ID="lbBack" CssClass="LinkButtons" Text="Back" Visible="false"></asp:LinkButton>
                                                <%--<asp:LinkButton ID="lbBack" runat="server" Text="Back" OnClick="lbBack_Click" Visible="false"
                                                    Height="23px" Width="25px" CssClass="LinkButtons"></asp:LinkButton>--%>
                                            </td>
                                            <td align="right">
                                                <asp:LinkButton runat="server" ID="lnkBackHolding" CssClass="LinkButtons" Text="Back"
                                                    OnClientClick="goBack()" Visible="false"></asp:LinkButton>
                                                <%--<asp:LinkButton ID="lbBack" runat="server" Text="Back" OnClick="lbBack_Click" Visible="false"
                                                    Height="23px" Width="25px" CssClass="LinkButtons"></asp:LinkButton>--%>
                                            </td>
                                            <td align="right" style="width: 10px">
                                                <asp:ImageButton ID="btnTrnxExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                                    Height="23px" Width="25px" OnClick="btnTrnxExport_Click"></asp:ImageButton>
                                            </td>
                                            <%-- <asp:ImageButton ID="imgBtnTrail" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="23px"
                                                    Width="25px" OnClick="btnTrailExport_Click"></asp:ImageButton>--%>
                                            <%-- <asp:ImageButton ID="btnbalncExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                                    Height="23px" Width="25px" OnClick="btnbalncExport_Click"></asp:ImageButton>--%>
                            </td>
                        </tr>
                    </table>
                    </div>
                </td>
            </tr>
        </table>
        <div>
            <tr>
                <td>
                    <table>
                        <tr id="trRangeNcustomer" runat="server">
                            <td align="right">
                                <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Display Type:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDisplayType" runat="server" CssClass="cmbField">
                                    <asp:ListItem Text="TransactionView " Value="TV">Transaction View</asp:ListItem>
                                    <asp:ListItem Text="BalanceView" Value="RHV">Return Holding View</asp:ListItem>
                                    <asp:ListItem Text="TrailCommissionView" Value="TCV">Trail Commission View</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
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
                                    runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="right">
                                            &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                                        </td>
                                        <td colspan="2">
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
                                                runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                                                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                                                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <%--  <td align="right">
                                <asp:Label ID="lblType" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
                                    <asp:ListItem Text="Online " Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Offline" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>--%>
                            <%--</td>
                            <td align="right">
                            
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;--%>
                            <td align="right">
                                <asp:Label ID="lblAgentCode" runat="server" Text="SubBroker Code:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAgentCode" runat="server" CssClass="cmbField">
                                    <asp:ListItem Text="Avaliable" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Avaliable" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="All" Value="0" Enabled="false"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                                &nbsp; &nbsp;
                                <td ID="tdCustomerGroup" runat="server" colspan="2">
                                    <asp:Label ID="lblCustomerGroup" runat="server" CssClass="FieldName" 
                                        Text="Search Customer:"></asp:Label>
                                    <%-- </td>
                            <td align="left" colspan="3">--%>
                                    <asp:DropDownList ID="ddlsearchcustomertype" runat="server" AutoPostBack="true" 
                                        CssClass="cmbField" 
                                        OnSelectedIndexChanged="ddlsearchcustomertype_OnSelectedIndexChanged">
                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <table>
                                        <tr ID="trGroupHead" runat="server">
                                            <td align="right">
                                                <asp:Label ID="lblGroupHead" runat="server" CssClass="FieldName" 
                                                    Text="Group Head :"></asp:Label>
                                                <asp:Label ID="lblCustomerSearch" runat="server" CssClass="FieldName" 
                                                    Text="Customer:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOptionSearch" runat="server" AutoPostBack="true" 
                                                    CssClass="cmbField" 
                                                    OnSelectedIndexChanged="ddlOptionSearch_OnSelectedIndexChanged" Visible="false">
                                                    <asp:ListItem Selected="true" Text="Select" Value="Select" />
                                                    <asp:ListItem Text="Name" Value="Name" />
                                                    <%-- <asp:ListItem Text="PAN" Value="Panno" />--%>
                                                    <asp:ListItem Text="Client Code" Value="Clientcode" />
                                                </asp:DropDownList>
                                                <%-- </td> <td align="left">--%>
                                                <asp:TextBox ID="txtcustomerName" runat="server" AutoPostBack="true" 
                                                    CssClass="txtField" Visible="false"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" runat="server" 
                                                    EnableViewState="false" TargetControlID="txtcustomerName" 
                                                    WatermarkText="Enter Three Characters of Customer">
                                                </cc1:TextBoxWatermarkExtender>
                                                <cc1:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" 
                                                    runat="server" CompletionInterval="100" 
                                                    CompletionListCssClass="AutoCompleteExtender_CompletionList" 
                                                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem" 
                                                    CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" 
                                                    CompletionSetCount="5" DelimiterCharacters="" EnableCaching="False" 
                                                    Enabled="True" MinimumPrefixLength="1" OnClientItemSelected="GetCustomerId" 
                                                    ServiceMethod="GetCustomerName" 
                                                    ServicePath="~/CustomerPortfolio/AutoComplete.asmx" 
                                                    TargetControlID="txtcustomerName" UseContextKey="True" />
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                ControlToValidate="txtcustomerName" ErrorMessage="<br />Please Enter Customer Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtCustCode" runat="server" AutoPostBack="true" 
                                                    CssClass="txtField" Visible="false"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" 
                                                    EnableViewState="false" TargetControlID="txtCustCode" 
                                                    WatermarkText="Enter few characters of Client Code">
                                                </cc1:TextBoxWatermarkExtender>
                                                <cc1:AutoCompleteExtender ID="txtClientCode_autoCompleteExtender" 
                                                    runat="server" CompletionInterval="100" 
                                                    CompletionListCssClass="AutoCompleteExtender_CompletionList" 
                                                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem" 
                                                    CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" 
                                                    CompletionSetCount="5" DelimiterCharacters="" EnableCaching="False" 
                                                    Enabled="True" MinimumPrefixLength="1" OnClientItemSelected="GetCustomerId" 
                                                    ServiceMethod="GetCustCode" ServicePath="~/CustomerPortfolio/AutoComplete.asmx" 
                                                    TargetControlID="txtCustCode" UseContextKey="True" />
                                                <%--
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtCustCode"
                ErrorMessage="<br />Please Enter Client Code" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="btnGo"></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rbtnAll" runat="server" AutoPostBack="true" Checked="true" 
                                                    CssClass="cmbFielde" GroupName="GroupAll" 
                                                    OnCheckedChanged="rbtnAll_CheckedChanged" Text="All" Visible="false" />
                                                &nbsp;
                                                <asp:RadioButton ID="rbtnGroup" runat="server" AutoPostBack="true" 
                                                    CssClass="cmbFielde" GroupName="GroupAll" 
                                                    OnCheckedChanged="rbtnAll_CheckedChanged" Text="Group" Visible="false" />
                                                &nbsp;
                                                <asp:RadioButton ID="rbtnIndividual" runat="server" AutoPostBack="true" 
                                                    CssClass="cmbFielde" GroupName="GroupAll" 
                                                    OnCheckedChanged="rbtnIndividual_OnCheckedChanged" Text="Individual" 
                                                    Visible="false" />
                                                <asp:TextBox ID="txtParentCustomer" runat="server" AutoComplete="Off" 
                                                    AutoPostBack="true" CssClass="txtField"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="txtParentCustomer_TextBoxWatermarkExtender" 
                                                    runat="server" TargetControlID="txtParentCustomer" 
                                                    WatermarkText="Type the Customer Name">
                                                </cc1:TextBoxWatermarkExtender>
                                                <cc1:AutoCompleteExtender ID="txtParentCustomer_autoCompleteExtender" 
                                                    runat="server" CompletionInterval="100" 
                                                    CompletionListCssClass="AutoCompleteExtender_CompletionList" 
                                                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem" 
                                                    CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" 
                                                    CompletionSetCount="5" EnableCaching="false" MinimumPrefixLength="1" 
                                                    OnClientItemSelected="GetParentCustomerId" 
                                                    ServiceMethod="GetParentCustomerName" 
                                                    ServicePath="~/CustomerPortfolio/AutoComplete.asmx" 
                                                    TargetControlID="txtParentCustomer" UseContextKey="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                           
                        </tr>
                        <tr id="trPeriod" visible="false" runat="server">
                            <td align="right">
                                <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                                <span id="Span4" class="spnRequiredField"></span>
                                <br />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                                    CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select
                a Period" ValidationGroup="btnGo"> </asp:CompareValidator>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="10%">
                                <asp:Label ID="lblAMC" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlAMC_OnSelectedIndexChanged" Width="300px">
                                    <%--<asp:ListItem
                Text="All" Value="0">All</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="10%">
                                <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblSchemeList" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlSchemeList" runat="server" CssClass="cmbField" AutoPostBack="true"
                                    Width="500px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="false"
                                    Visible="false">
                                    <%--<asp:ListItem Text="All" Value="0">All</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <tr>
                                <td>
                                    <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" 
                                        OnClick="btnGo_Click" onmouseout="javascript:ChangeButtonCss('out',
                'ctrl_RMMultipleTransactionView_btnGo', 'S');" onmouseover="javascript:ChangeButtonCss('hover',
                'ctrl_RMMultipleTransactionView_btnGo', 'S');" Text="Go" ValidationGroup="btnGo" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:RadioButton ID="rdtnOnline" runat="server" Checked="true" CssClass="Field" 
                                        GroupName="status" Text="Online" Visible="false" />
                                    <asp:RadioButton ID="rbtnOffline" runat="server" CssClass="Field" 
                                        GroupName="status" Text="Offline" Visible="false" />
                                </td>
                                <td align="right" visible="false">
                                    <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date Type:" 
                                        Visible="false"></asp:Label>
                                </td>
                                <td align="left" visible="false">
                                    <asp:RadioButton ID="rbtnPickDate" runat="server" AutoPostBack="true" 
                                        Checked="true" GroupName="Date" OnCheckedChanged="rbtnDate_CheckedChanged" 
                                        Visible="false" />
                                    <asp:Label ID="lblPickDate" runat="server" CssClass="Field" Text="Date Range" 
                                        Visible="false"></asp:Label>
                                    &nbsp;
                                    <asp:RadioButton ID="rbtnPickPeriod" runat="server" AutoPostBack="true" 
                                        GroupName="Date" OnCheckedChanged="rbtnDate_CheckedChanged" Visible="false" />
                                    <asp:Label ID="lblPickPeriod" runat="server" CssClass="Field" Text="Period" 
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr visible="false">
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio:" Visible="false"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField" Visible="false">
                                    <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
                                    <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
                                </asp:DropDownList>
                            </td>--%>
                            <tr ID="trZCCS" runat="server" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblBrokerCode" runat="server" CssClass="FieldName" 
                                        Text="Sub Broker Code:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBrokerCode" runat="server" CssClass="cmbField">
                                        <asp:ListItem Selected="true" Text="SubBroker Code" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tr>
                    </table>
                </td>
            </tr>
        </div>
        <tr>
            <td>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <table width="100%">
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
        </tr>
        <%-- <div style="width: 100%">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik" OnTabClick="TabClick"
                    EnableEmbeddedSkins="False" MultiPageID="TransactionMIS" SelectedIndex="1">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Transaction View" Value="Transaction View" TabIndex="0"
                            Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Balance View" Value="Balance View" TabIndex="1">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="TransactionMIS" EnableViewState="true" runat="server" SelectedIndex="0"
                    Width="100%">
                    <telerik:RadPageView ID="RadPageView1" runat="server">--%>
        <tr>
            <td style="padding-top: 20px">
                <%--<div id="tbl" runat="server">--%>
                <asp:Panel ID="Panel2" runat="server" class="Landscape" Height="100%" Width="100%"
                    ScrollBars="Both" Visible="false">
                    <table width="100%">
                        <tr>
                            <td>
                                <div id="dvTransactionsView" runat="server" style="width: 640px; height: 300px">
                                    <telerik:RadGrid ID="gvMFTransactions" runat="server" GridLines="None" AutoGenerateColumns="False"
                                        allowfiltering="true" AllowFilteringByColumn="true" PageSize="10" AllowSorting="true"
                                        fAllowAutomaticDeletes="false" OnPreRender="gvMFTransactions_PreRender" AllowPaging="True"
                                        ShowStatusBar="True" OnExcelMLExportStylesCreated="gvMFTransactions_OnExcelMLExportStylesCreated"
                                        OnExcelMLExportRowCreated="gvMFTransactions_OnExcelMLExportRowCreated" OnItemCommand="gvMFTransactions_OnItemCommand"
                                        OnItemDataBound="gvMFTransactions_ItemDataBound" OnNeedDataSource="gvMFTransactions_OnNeedDataSource"
                                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" Height="400px"
                                        AllowAutomaticInserts="false">
                                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                            FileName="View Transactions Details">
                                        </ExportSettings>
                                        <MasterTableView DataKeyNames="TransactionId" Width="100%" AllowMultiColumnSorting="True"
                                            AllowFilteringByColumn="true" AutoGenerateColumns="false" CommandItemDisplay="None">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false" FooterText="Grand Total:" HeaderStyle-Wrap="false">
                                                    <ItemStyle Wrap="false" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View Details" OnClick="lnkView_Click">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Customer Name" HeaderText="Customer" AllowFiltering="true"
                                                    HeaderStyle-Wrap="false" SortExpression="Customer Name" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Customer Name"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TransactionId" HeaderText="TransactionId" AllowFiltering="false"
                                                    Visible="false" SortExpression="TransactionId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="TransactionId" FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Folio Number" HeaderText="Folio No." AllowFiltering="true"
                                                    SortExpression="Folio Number" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="Folio Number" FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Category" HeaderText="Category" AllowFiltering="true"
                                                    HeaderStyle-Wrap="false" SortExpression="Category" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="Category" FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                    UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AMC" HeaderText="AMC" AllowFiltering="true" HeaderStyle-Wrap="false"
                                                    SortExpression="AMC" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="AMC" FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Scheme Name" HeaderText="Scheme Name" AllowFiltering="true"
                                                    HeaderStyle-Wrap="false" SortExpression="Scheme Name" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Scheme Name"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridTemplateColumn AllowFiltering="true" DataField="Scheme Name" AutoPostBackOnFilter="true"
                                                        HeaderText="Scheme" ShowFilterIcon="false" FilterControlWidth="280px">
                                                        <ItemStyle Wrap="false" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Scheme" Text='<%# Eval("Scheme Name").ToString() %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                                <telerik:GridBoundColumn DataField="Transaction Type" HeaderText="Type" AllowFiltering="true"
                                                    HeaderStyle-Wrap="false" SortExpression="Transaction Type" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Transaction Type"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Transaction Date" HeaderText="Date" AllowFiltering="false"
                                                    AllowSorting="true" HeaderStyle-Wrap="false" SortExpression="Transaction Date"
                                                    ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="Transaction Date" FooterStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Price" HeaderText="Price (Rs)" AllowFiltering="false"
                                                    SortExpression="Price" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    HeaderStyle-Wrap="false" AutoPostBackOnFilter="true" UniqueName="Price" FooterStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n4}" Aggregate="Sum">
                                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Units" HeaderText="Units" AllowFiltering="false"
                                                    HeaderStyle-Wrap="false" SortExpression="Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="Units" FooterStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n3}" Aggregate="Sum">
                                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Amount" HeaderText="Amount (Rs)" AllowFiltering="false"
                                                    HeaderStyle-Wrap="false" SortExpression="Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="Amount" FooterStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n0}" Aggregate="Sum">
                                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CMFT_ExternalBrokerageAmount" HeaderText="Brokerage(Rs)"
                                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CMFT_ExternalBrokerageAmount"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                    UniqueName="CMFT_ExternalBrokerageAmount" FooterStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n2}" Aggregate="Sum">
                                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="STT" HeaderText="STT (Rs)" AllowFiltering="false"
                                                    HeaderStyle-Wrap="false" SortExpression="STT" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="STT" FooterStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n}" Aggregate="Sum">
                                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Portfolio Name" HeaderText="Portfolio Name" AllowFiltering="false"
                                                    HeaderStyle-Wrap="false" SortExpression="Portfolio Name" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Portfolio Name"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ADUL_ProcessId" HeaderText="Process ID" AllowFiltering="true"
                                                    HeaderStyle-Wrap="false" SortExpression="ADUL_ProcessId" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="ADUL_ProcessId"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CMFT_Area" HeaderText="Area" AllowFiltering="true"
                                                    HeaderStyle-Wrap="false" SortExpression="CMFT_Area" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="CMFT_Area" FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CMFT_EUIN" HeaderText="EUIN" AllowFiltering="true"
                                                    HeaderStyle-Wrap="false" SortExpression="CMFT_EUIN" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" UniqueName="CMFT_EUIN" FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TransactionStatus" HeaderText="Transaction Status"
                                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="TransactionStatus"
                                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="TransactionStatus"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                    <FilterTemplate>
                                                        <telerik:RadComboBox ID="RadComboBoxTS" AutoPostBack="true" AllowFiltering="true"
                                                            CssClass="cmbField" Width="100px" IsFilteringEnabled="true" AppendDataBoundItems="true"
                                                            OnPreRender="Transaction_PreRender" EnableViewState="true" OnSelectedIndexChanged="RadComboBoxTS_SelectedIndexChanged"
                                                            SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("TransactionStatus").CurrentFilterValue %>'
                                                            runat="server">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="ALL" Value="" Selected="false"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem Text="OK" Value="OK" Selected="false"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem Text="Cancel" Value="Cancel" Selected="false"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem Text="Original" Value="Original" Selected="false"></telerik:RadComboBoxItem>
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

                                                            <script type="text/javascript">
                                                                function TransactionIndexChanged(sender, args) {
                                                                    var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                                    //////                                                    sender.value = args.get_item().get_value();
                                                                    tableView.filter("RadComboBoxTS", args.get_item().get_value(), "EqualTo");
                                                                }
                                                            </script>

                                                        </telerik:RadScriptBlock>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CreatedOn" HeaderText="Add Date (System)" AllowFiltering="false"
                                                    HeaderStyle-Wrap="false" SortExpression="CreatedOn" ShowFilterIcon="false" AllowSorting="true"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CreatedOn"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CMFT_SubBrokerCode" HeaderText="Sub-Broker Code"
                                                    AllowFiltering="true" SortExpression="CMFT_SubBrokerCode" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_SubBrokerCode"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AssociatesName" AllowFiltering="true" HeaderText="SubBroker Name"
                                                    Visible="true" UniqueName="AssociatesName" SortExpression="AssociatesName" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="UserType" AllowFiltering="true" HeaderText="Type"
                                                    Visible="true" UniqueName="UserType" SortExpression="UserType" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ChannelName" AllowFiltering="true" HeaderText="Channel"
                                                    UniqueName="ChannelName" SortExpression="ChannelName" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Titles" AllowFiltering="true" HeaderText="Title"
                                                    Visible="true" UniqueName="Titles" SortExpression="Titles" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ReportingManagerName" AllowFiltering="true" HeaderText="Reporting Manager"
                                                    Visible="true" UniqueName="ReportingManagerName" SortExpression="ReportingManagerName"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                    HeaderStyle-Width="120px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Visible="true" DataField="ClusterManager" AllowFiltering="true"
                                                    HeaderText="Cluster Manager" UniqueName="ClusterManager" SortExpression="ClusterManager"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                    HeaderStyle-Width="130px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AreaManager" AllowFiltering="true" HeaderText="Area Manager"
                                                    UniqueName="AreaManager" SortExpression="AreaManager" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ZonalManagerName" AllowFiltering="true" HeaderText="Zonal Manager"
                                                    UniqueName="ZonalManagerName" SortExpression="ZonalManagerName" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DeuptyHead" AllowFiltering="true" HeaderText="Deupty Head"
                                                    UniqueName="DeuptyHead" SortExpression="DeuptyHead" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CMFT_UserTransactionNo" AllowFiltering="true"
                                                    HeaderText="User Transaction No" UniqueName="UserTransactionNo" SortExpression="CMFT_UserTransactionNo"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                    HeaderStyle-Width="330px">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%-- </div>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlMFTransactionWithoutAgentCode" runat="server" 
                    class="Landscape" Height="100%" ScrollBars="Both" Visible="false" Width="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <div ID="Div1" runat="server" style="width: 640px; height: 300px">
                                    <telerik:RadGrid ID="gvMFTransactionWithoutAgentCode" runat="server" 
                                        AllowAutomaticInserts="false" allowfiltering="true" 
                                        AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="true" 
                                        AutoGenerateColumns="False" EnableEmbeddedSkins="false" 
                                        fAllowAutomaticDeletes="false" GridLines="None" Height="400px" 
                                        OnNeedDataSource="gvMFTransactionWithoutAgentCode_OnNeedDataSource" 
                                        OnPreRender="gvMFTransactions_PreRender" PageSize="10" ShowFooter="true" 
                                        ShowStatusBar="True" Skin="Telerik" Width="120%">
                                        <ExportSettings ExportOnlyData="true" FileName="View Transactions Details" 
                                            HideStructureColumns="true" IgnorePaging="true">
                                        </ExportSettings>
                                        <MasterTableView AllowFilteringByColumn="true" AllowMultiColumnSorting="True" 
                                            AutoGenerateColumns="false" CommandItemDisplay="None" 
                                            DataKeyNames="TransactionId" Width="100%">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false" FooterText="Grand Total:" 
                                                    HeaderStyle-Wrap="false">
                                                    <ItemStyle Wrap="false" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewWithoutAgentCode" runat="server" 
                                                            OnClick="lnkViewWithoutAgentCode_Click" Text="View Details">
                                                    </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Customer Name" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Customer" ShowFilterIcon="false" SortExpression="Customer Name" 
                                                    UniqueName="Customer Name">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="TransactionId" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderText="TransactionId" 
                                                    ShowFilterIcon="false" SortExpression="TransactionId" 
                                                    UniqueName="TransactionId" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Folio Number" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderText="Folio No." 
                                                    ShowFilterIcon="false" SortExpression="Folio Number" UniqueName="Folio Number">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Category" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Category" ShowFilterIcon="false" SortExpression="Category" 
                                                    UniqueName="Category">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" 
                                                    DataField="PAISC_AssetInstrumentSubCategoryName" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Sub Category" ShowFilterIcon="false" 
                                                    SortExpression="PAISC_AssetInstrumentSubCategoryName" 
                                                    UniqueName="PAISC_AssetInstrumentSubCategoryName">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="AMC" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" HeaderText="AMC" 
                                                    ShowFilterIcon="false" SortExpression="AMC" UniqueName="AMC">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Scheme Name" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Scheme Name" ShowFilterIcon="false" SortExpression="Scheme Name" 
                                                    UniqueName="Scheme Name">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridTemplateColumn AllowFiltering="true" DataField="Scheme Name" AutoPostBackOnFilter="true"
                                                        HeaderText="Scheme" ShowFilterIcon="false" FilterControlWidth="280px">
                                                        <ItemStyle Wrap="false" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Scheme" Text='<%# Eval("Scheme Name").ToString() %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Transaction Type" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" HeaderText="Type" 
                                                    ShowFilterIcon="false" SortExpression="Transaction Type" 
                                                    UniqueName="Transaction Type">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AllowSorting="true" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                                    DataField="Transaction Date" DataFormatString="{0:d}" 
                                                    DataType="System.DateTime" FooterStyle-HorizontalAlign="Center" 
                                                    HeaderStyle-Wrap="false" HeaderText="Date" ShowFilterIcon="false" 
                                                    SortExpression="Transaction Date" UniqueName="Transaction Date">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Price" 
                                                    DataFormatString="{0:n4}" FooterStyle-HorizontalAlign="Right" 
                                                    HeaderStyle-Wrap="false" HeaderText="Price (Rs)" ShowFilterIcon="false" 
                                                    SortExpression="Price" UniqueName="Price">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Units" 
                                                    DataFormatString="{0:n3}" FooterStyle-HorizontalAlign="Right" 
                                                    HeaderStyle-Wrap="false" HeaderText="Units" ShowFilterIcon="false" 
                                                    SortExpression="Units" UniqueName="Units">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Amount" 
                                                    DataFormatString="{0:n0}" FooterStyle-HorizontalAlign="Right" 
                                                    HeaderStyle-Wrap="false" HeaderText="Amount (Rs)" ShowFilterIcon="false" 
                                                    SortExpression="Amount" UniqueName="Amount">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                                    DataField="CMFT_ExternalBrokerageAmount" DataFormatString="{0:n2}" 
                                                    FooterStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" 
                                                    HeaderText="Brokerage(Rs)" ShowFilterIcon="false" 
                                                    SortExpression="CMFT_ExternalBrokerageAmount" 
                                                    UniqueName="CMFT_ExternalBrokerageAmount">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="STT" 
                                                    DataFormatString="{0:n}" FooterStyle-HorizontalAlign="Right" 
                                                    HeaderStyle-Wrap="false" HeaderText="STT (Rs)" ShowFilterIcon="false" 
                                                    SortExpression="STT" UniqueName="STT">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Portfolio Name" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Portfolio Name" ShowFilterIcon="false" 
                                                    SortExpression="Portfolio Name" UniqueName="Portfolio Name">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="ADUL_ProcessId" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Process ID" ShowFilterIcon="false" SortExpression="ADUL_ProcessId" 
                                                    UniqueName="ADUL_ProcessId">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="CMFT_Area" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" HeaderText="Area" 
                                                    ShowFilterIcon="false" SortExpression="CMFT_Area" UniqueName="CMFT_Area">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="CMFT_EUIN" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" HeaderText="EUIN" 
                                                    ShowFilterIcon="false" SortExpression="CMFT_EUIN" UniqueName="CMFT_EUIN">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    DataField="TransactionStatus" FooterStyle-HorizontalAlign="Left" 
                                                    HeaderStyle-Wrap="false" HeaderText="Transaction Status" ShowFilterIcon="false" 
                                                    SortExpression="TransactionStatus" UniqueName="TransactionStatus">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    <FilterTemplate>
                                                        <telerik:RadComboBox ID="RadComboBoxTS" runat="server" AllowFiltering="true" 
                                                            AppendDataBoundItems="true" AutoPostBack="true" CssClass="cmbField" 
                                                            EnableViewState="true" IsFilteringEnabled="true" 
                                                            OnPreRender="Transaction_PreRender" 
                                                            OnSelectedIndexChanged="RadComboBoxTS_SelectedIndexChanged" 
                                                            SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("TransactionStatus").CurrentFilterValue %>' 
                                                            Width="100px">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Selected="false" Text="ALL" Value="" />
                                                                <telerik:RadComboBoxItem Selected="false" Text="OK" Value="OK" />
                                                                <telerik:RadComboBoxItem Selected="false" Text="Cancel" Value="Cancel" />
                                                                <telerik:RadComboBoxItem Selected="false" Text="Original" Value="Original" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

                                                            <script type="text/javascript">

                                                            function TransactionIndexChanged(sender, args) {
                                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                                //////                                                    sender.value = args.get_item().get_value();
                                                                tableView.filter("RadComboBoxTS", args.get_item().get_value(), "EqualTo");
                                                            }
                                                        </script>

                                                        </telerik:RadScriptBlock>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AllowSorting="true" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                                    DataField="CreatedOn" FooterStyle-HorizontalAlign="Left" 
                                                    HeaderStyle-Wrap="false" HeaderText="Add Date (System)" ShowFilterIcon="false" 
                                                    SortExpression="CreatedOn" UniqueName="CreatedOn">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="CMFT_UserTransactionNo" 
                                                    HeaderStyle-Width="330px" HeaderText="User Transaction No" 
                                                    ShowFilterIcon="false" SortExpression="CMFT_UserTransactionNo" 
                                                    UniqueName="UserTransactionNo">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <table ID="tbspace" runat="server" cellpadding="5">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <tr>
                <td style="padding-top: 20px">
                    <%--  <div id="Div1" runat="server">--%>
                    <asp:Panel ID="Panel1" runat="server" class="Landscape" ScrollBars="Horizontal" 
                        Visible="false" Width="100%">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <%-- <div id="dvBalanceView" runat="server" style="margin: 2px; width: 640px;">--%>
                                    <telerik:RadGrid ID="gvBalanceView" runat="server" 
                                        AllowAutomaticInserts="false" allowfiltering="true" 
                                        AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="true" 
                                        AutoGenerateColumns="False" EnableEmbeddedSkins="false" 
                                        ExportSettings-ExportOnlyData="true" GridLines="None" 
                                        OnItemCommand="gvBalanceView_ItemCommand" 
                                        OnItemDataBound="gvBalanceView_ItemDataBound" 
                                        OnNeedDataSource="gvBalanceView_OnNeedDataSource" PageSize="10" 
                                        ShowFooter="true" ShowStatusBar="True" Skin="Telerik" Width="120%">
                                        <ExportSettings Excel-Format="ExcelML" ExportOnlyData="true" 
                                            FileName="View ReturnHolding Details" HideStructureColumns="true" 
                                            IgnorePaging="true">
                                        </ExportSettings>
                                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" 
                                            CommandItemDisplay="None" 
                                            DataKeyNames="TransactionId,AccountId,SchemePlanCode,AMCCode,CMFT_SubBrokerCode" 
                                            Width="100%">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false" FooterText="Grand Total:" 
                                                    HeaderStyle-Wrap="false" Visible="false">
                                                    <ItemStyle Wrap="false" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="cmbField" 
                                                            OnClick="lnkView_Click" Text="View Details" Visible="false">
                                                    </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Customer Name" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Customer" ShowFilterIcon="false" SortExpression="Customer Name" 
                                                    UniqueName="Customer Name">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Folio Number" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderText="Folio No." 
                                                    ShowFilterIcon="false" SortExpression="Folio Number" UniqueName="Folio Number">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="TransactionId" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderText="TransactionId" 
                                                    ShowFilterIcon="false" SortExpression="TransactionId" 
                                                    UniqueName="TransactionId" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="true" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                                    DataField="Transaction Date" DataFormatString="{0:d}" 
                                                    DataType="System.DateTime" FooterStyle-HorizontalAlign="Left" 
                                                    HeaderText="Transaction Date" ShowFilterIcon="false" 
                                                    SortExpression="Transaction Date" UniqueName="Transaction Date" Visible="true">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Scheme Name" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Scheme Name" ShowFilterIcon="false" SortExpression="Scheme Name" 
                                                    UniqueName="Scheme Name">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridTemplateColumn AllowFiltering="true" DataField="Scheme Name" AutoPostBackOnFilter="true"
                                                    HeaderText="Scheme" ShowFilterIcon="false" FilterControlWidth="280px">
                                                    <ItemStyle Wrap="false" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Scheme" Text='<%# Eval("Scheme Name").ToString() %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Category" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Category" ShowFilterIcon="false" SortExpression="Category" 
                                                    UniqueName="Category">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" 
                                                    DataField="PAISC_AssetInstrumentSubCategoryName" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Sub Category" ShowFilterIcon="false" 
                                                    SortExpression="PAISC_AssetInstrumentSubCategoryName" 
                                                    UniqueName="PAISC_AssetInstrumentSubCategoryName">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="AMC" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" HeaderText="AMC" 
                                                    ShowFilterIcon="false" SortExpression="AMC" UniqueName="AMC">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Transaction Type" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" HeaderText="Type" 
                                                    ShowFilterIcon="false" SortExpression="Transaction Type" 
                                                    UniqueName="Transaction Type">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Amount" 
                                                    DataFormatString="{0:n0}" FooterStyle-HorizontalAlign="Right" 
                                                    HeaderStyle-Wrap="false" HeaderText="Amount (Rs)" ShowFilterIcon="false" 
                                                    SortExpression="Amount" UniqueName="Amount">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Price" 
                                                    DataFormatString="{0:n4}" FooterStyle-HorizontalAlign="Right" 
                                                    HeaderStyle-Wrap="false" HeaderText="Price (Rs)" ShowFilterIcon="false" 
                                                    SortExpression="Price" UniqueName="Price">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="NAV" 
                                                    DataFormatString="{0:n4}" FooterStyle-HorizontalAlign="Right" 
                                                    HeaderStyle-Wrap="false" HeaderText="NAV (Rs)" ShowFilterIcon="false" 
                                                    SortExpression="NAV" UniqueName="NAV">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Units" 
                                                    DataFormatString="{0:n3}" FooterStyle-HorizontalAlign="Right" 
                                                    HeaderStyle-Wrap="false" HeaderText="Units" ShowFilterIcon="false" 
                                                    SortExpression="Units" UniqueName="Units">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn Aggregate="Sum" AllowFiltering="false" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                                    DataField="CurrentValue" FooterAggregateFormatString="{0:n3}" 
                                                    FooterStyle-HorizontalAlign="Right" FooterText=" " HeaderText="Current Value" 
                                                    ShowFilterIcon="false" SortExpression="CurrentValue">
                                                    <ItemStyle HorizontalAlign="Right" Width="" Wrap="false" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprAmcB" runat="server" CommandName="SelectTransaction" 
                                                            Text='<%# String.Format("{0:N3}", DataBinder.Eval(Container.DataItem, "CurrentValue")) %>'>
                                                    </asp:LinkButton>
                                                        <%-- Text='<%#(Eval("CurrentValue","{0:n3}").ToString()) %>' />--%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%-- <telerik:GridTemplateColumn AllowFiltering="false" DataField="CurrentValue" AutoPostBackOnFilter="true"
                                                    HeaderText="Current Value" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                                                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprAmcB" runat="server" CommandName="SelectAmt" Text='<%# Eval("CurrentValue").ToString()%>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
                                                <%--<telerik:GridBoundColumn DataField="CurrentValue" HeaderText="Current Value" AllowFiltering="false"
                                                        SortExpression="CurrentValue" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                                        HeaderStyle-Wrap="false" AutoPostBackOnFilter="true" UniqueName="CurrentValue" Aggregate="Sum"
                                                        FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                                                        <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Age" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Age (Days)" ShowFilterIcon="false" SortExpression="CMFTB_Age" 
                                                    UniqueName="CMFTB_Age">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Balance" DataFormatString="{0:n2}" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                    HeaderText="Abs Rtn (%)" ShowFilterIcon="false" SortExpression="Return" 
                                                    UniqueName="Return">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="CMFT_SubBrokerCode" 
                                                    FooterStyle-HorizontalAlign="Left" HeaderText="Sub-Broker Code" 
                                                    ShowFilterIcon="false" SortExpression="CMFT_SubBrokerCode" 
                                                    UniqueName="CMFT_SubBrokerCode">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="AssociatesName" 
                                                    HeaderStyle-Width="120px" HeaderText="SubBroker Name" ShowFilterIcon="false" 
                                                    SortExpression="AssociatesName" UniqueName="AssociatesName" Visible="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="UserType" HeaderStyle-Width="120px" 
                                                    HeaderText="Type" ShowFilterIcon="false" SortExpression="UserType" 
                                                    UniqueName="UserType" Visible="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="ChannelName" 
                                                    HeaderStyle-Width="130px" HeaderText="Channel" ShowFilterIcon="false" 
                                                    SortExpression="ChannelName" UniqueName="ChannelName">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="Titles" HeaderStyle-Width="120px" 
                                                    HeaderText="Title" ShowFilterIcon="false" SortExpression="Titles" 
                                                    UniqueName="Titles" Visible="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="ReportingManagerName" 
                                                    HeaderStyle-Width="120px" HeaderText="Reporting Manager" ShowFilterIcon="false" 
                                                    SortExpression="ReportingManagerName" UniqueName="ReportingManagerName" 
                                                    Visible="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="ClusterManager" 
                                                    HeaderStyle-Width="130px" HeaderText="Cluster Manager" ShowFilterIcon="false" 
                                                    SortExpression="ClusterManager" UniqueName="ClusterManager" Visible="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="AreaManager" 
                                                    HeaderStyle-Width="130px" HeaderText="Area Manager" ShowFilterIcon="false" 
                                                    SortExpression="AreaManager" UniqueName="AreaManager">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="ZonalManagerName" 
                                                    HeaderStyle-Width="130px" HeaderText="Zonal Manager" ShowFilterIcon="false" 
                                                    SortExpression="ZonalManagerName" UniqueName="ZonalManagerName">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                    CurrentFilterFunction="Contains" DataField="DeuptyHead" 
                                                    HeaderStyle-Width="130px" HeaderText="Deupty Head" ShowFilterIcon="false" 
                                                    SortExpression="DeuptyHead" UniqueName="DeuptyHead">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <%-- <Resizing AllowColumnResize="true" />--%>
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    <%-- </div>--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%-- </div>--%>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px">
                    <div ID="divTrail" runat="server" 
                        style="margin: 2px; width: 100%; overflow: auto" visible="false">
                        <%--    <asp:Panel ID="pnlTrail" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">--%>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div ID="Div3" runat="server" style="margin: 2px; width: 100%;" visible="false">
                                        <telerik:RadGrid ID="gvTrail" runat="server" AllowAutomaticInserts="false" 
                                            AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="true" 
                                            AutoGenerateColumns="False" EnableEmbeddedSkins="false" 
                                            ExportSettings-FileName="Trail DETAILS" GridLines="None" 
                                            OnItemDataBound="gvTrail_ItemDataBound" 
                                            OnNeedDataSource="gvTrail_OnNeedDataSource" OnPreRender="gvTrail_PreRender" 
                                            PageSize="10" ShowFooter="true" ShowStatusBar="True" Skin="Telerik" 
                                            Width="100%">
                                            <ExportSettings HideStructureColumns="true">
                                            </ExportSettings>
                                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" 
                                                CommandItemDisplay="None" 
                                                DataKeyNames="CMFA_FolioNum,CMFA_AccountId,CMFTCSU_TrailComissionSetUpId,PASP_SchemePlanCode,CMFT_TransactionNumber,CMFTCSU_Units,CMFTCSU_Amount,CMFTCSU_TransactionDate" 
                                                Width="100%">
                                                <Columns>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        DataField="Conditioning" HeaderStyle-Width="117px" UniqueName="Conditioning">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                        <FilterTemplate>
                                                            <telerik:RadComboBox ID="RadComboBoxRR" runat="server" AllowFiltering="true" 
                                                                AppendDataBoundItems="true" AutoPostBack="true" AutoPostBackOnFilter="false" 
                                                                CssClass="cmbField" EnableViewState="true" IsFilteringEnabled="true" 
                                                                OnPreRender="RCBForTrailCondiotining_PreRender" 
                                                                OnSelectedIndexChanged="RCBForTrailCondiotining_SelectedIndexChanged" 
                                                                SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Conditioning").CurrentFilterValue %>' 
                                                                Width="100px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Selected="false" Text="All" Value="" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                                                <script type="text/javascript">

                                                                function RejectReasonIndexChanged(sender, args) {
                                                                    alert(tableView);
                                                                    var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                                    tableView.filter("RejectReasonCode", args.get_item().get_value(), "EqualTo");
                                                                } 
                                                            </script>

                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="50px">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                            <input ID="chkBxWerpAll" name="chkBxWerpAll" onclick="checkAllBoxes()" 
                                                                style="display: none" type="checkbox" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbOne" runat="server" onclick="CheckOne(this)" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CMFA_FolioNum" 
                                                        FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" 
                                                        HeaderText="FolioNum" ShowFilterIcon="false" UniqueName="CMFA_FolioNum">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="customerName" 
                                                        HeaderStyle-Width="150px" HeaderText="Customer Name" ShowFilterIcon="false" 
                                                        SortExpression="customerName" UniqueName="customerName">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" 
                                                        DataField="PASP_SchemePlanName" FilterControlWidth="280px" 
                                                        HeaderStyle-Width="450px" HeaderText="Scheme" ItemStyle-Wrap="false" 
                                                        ShowFilterIcon="false" SortExpression="PASP_SchemePlanName" 
                                                        UniqueName="PASP_SchemePlanName">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" 
                                                        AutoPostBackOnFilter="true" DataField="CMFTCSU_Units" DataFormatString="{0:N0}" 
                                                        FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" 
                                                        HeaderText="Units" ItemStyle-Wrap="false" ShowFilterIcon="false" 
                                                        SortExpression="CMFTCSU_Units" UniqueName="CMFTCSU_Units">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn Aggregate="Sum" AutoPostBackOnFilter="true" 
                                                        DataField="CMFTCSU_Amount" DataFormatString="{0:N0}" 
                                                        FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" 
                                                        HeaderText="Amount" ShowFilterIcon="false" UniqueName="CMFTCSU_Amount">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="ADUL_ProcessId" 
                                                        FooterStyle-HorizontalAlign="Right" FooterText="Grand Total :" 
                                                        HeaderStyle-Width="100px" HeaderText="ProcessId" ShowFilterIcon="false" 
                                                        SortExpression="ADUL_ProcessId" UniqueName="ADUL_ProcessId">
                                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="PA_AMCName" 
                                                        FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" HeaderText="AMC" 
                                                        ShowFilterIcon="false" SortExpression="PA_AMCName" UniqueName="PA_AMCName">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="CMFTCSU_SubBroker" 
                                                        FooterStyle-HorizontalAlign="Left" HeaderText="Sub-Broker Code" 
                                                        ShowFilterIcon="false" SortExpression="CMFTCSU_SubBroker" 
                                                        UniqueName="CMFTCSU_SubBroker">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" 
                                                        DataField="CMFTCSU_TransactionDate" DataFormatString="{0:d}" 
                                                        HeaderText="TransactionDate" ItemStyle-HorizontalAlign="Center" 
                                                        ShowFilterIcon="false" UniqueName="CMFTCSU_TransactionDate">
                                                        <FilterTemplate>
                                                            <telerik:RadDatePicker ID="CMFTCSU_TransactionDate" runat="server" 
                                                                AutoPostBack="true">
                                                            </telerik:RadDatePicker>
                                                        </FilterTemplate>
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" 
                                                        DataField="CMFTCSU_PRDate" DataFormatString="{0:d}" HeaderText="PR Date" 
                                                        ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" 
                                                        UniqueName="CMFTCSU_PRDate">
                                                        <FilterTemplate>
                                                            <telerik:RadDatePicker ID="CMFTCSU_PRDate" runat="server" AutoPostBack="true">
                                                            </telerik:RadDatePicker>
                                                        </FilterTemplate>
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" 
                                                        DataField="CMFTCSU_FROMDate" DataFormatString="{0:d}" HeaderText="FROM Date" 
                                                        ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" 
                                                        UniqueName="CMFTCSU_FROMDate">
                                                        <FilterTemplate>
                                                            <telerik:RadDatePicker ID="CMFTCSU_FROMDate" runat="server" AutoPostBack="true">
                                                            </telerik:RadDatePicker>
                                                        </FilterTemplate>
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" 
                                                        DataField="CMFTCSU_TODate" DataFormatString="{0:d}" HeaderText="TO Date" 
                                                        ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" 
                                                        UniqueName="CMFTCSU_TODate">
                                                        <FilterTemplate>
                                                            <telerik:RadDatePicker ID="CMFTCSU_TODate" runat="server" AutoPostBack="true">
                                                            </telerik:RadDatePicker>
                                                        </FilterTemplate>
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridBoundColumn Aggregate="Sum" AutoPostBackOnFilter="true" 
                                                        DataField="CMFTCSU_TrailFee" DataFormatString="{0:N2}" 
                                                        FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" 
                                                        HeaderText="Trail Fee" ShowFilterIcon="false" UniqueName="CMFTCSU_TrailFee">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CMFT_MFTransId" 
                                                        FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" 
                                                        HeaderText="MFTransId" ShowFilterIcon="false" UniqueName="CMFT_MFTransId">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CMFA_AccountId" 
                                                        FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" 
                                                        HeaderText="AccountId" ShowFilterIcon="false" UniqueName="CMFA_AccountId">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="CMFSS_StartDate" 
                                                        DataFormatString="{0:d0}" HeaderText="StartDate" ShowFilterIcon="false" 
                                                        SortExpression="CMFSS_StartDate" UniqueName="CMFSS_StartDate" Visible="false">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="EndDate" 
                                                        HeaderText="EndDate" UniqueName="EndDate" Visible="false">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" 
                                                        DataField="CMFTCSU_TrailComissionSetUpId" HeaderStyle-Width="100px" 
                                                        HeaderText="TrailId" ShowFilterIcon="false" 
                                                        SortExpression="CMFTCSU_TrailComissionSetUpId" 
                                                        UniqueName="CMFTCSU_TrailComissionSetUpId">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" 
                                                        DataField="PASP_SchemePlanCode" HeaderText="SchemePlanCode" 
                                                        ShowFilterIcon="false" SortExpression="PASP_SchemePlanCode" 
                                                        UniqueName="PASP_SchemePlanCode" Visible="false">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" 
                                                        DataField="CMFTCSU_TransactionType" HeaderText="TransactionType" 
                                                        ShowFilterIcon="false" SortExpression="CMFTCSU_TransactionType" 
                                                        UniqueName="CMFTCSU_TransactionType" Visible="false">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="PAIC_AssetInstrumentCategoryName" 
                                                        FooterStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                                        HeaderText="Category" ShowFilterIcon="false" 
                                                        SortExpression="PAIC_AssetInstrumentCategoryName" 
                                                        UniqueName="PAIC_AssetInstrumentCategoryName">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" 
                                                        DataField="CMFT_TransactionNumber" FooterStyle-HorizontalAlign="Right" 
                                                        HeaderStyle-Width="100px" HeaderText="TransNo." ShowFilterIcon="false" 
                                                        UniqueName="CMFT_TransactionNumber">
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="CMFT_SubBrokerCode" 
                                                        FooterStyle-HorizontalAlign="Left" HeaderText="Sub-Broker Code" 
                                                        ShowFilterIcon="false" SortExpression="CMFT_SubBrokerCode" 
                                                        UniqueName="CMFT_SubBrokerCode">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="AssociatesName" 
                                                        HeaderStyle-Width="120px" HeaderText="SubBroker Name" ShowFilterIcon="false" 
                                                        SortExpression="AssociatesName" UniqueName="AssociatesName" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="UserType" HeaderStyle-Width="120px" 
                                                        HeaderText="Type" ShowFilterIcon="false" SortExpression="UserType" 
                                                        UniqueName="UserType" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="ChannelName" 
                                                        HeaderStyle-Width="130px" HeaderText="Channel" ShowFilterIcon="false" 
                                                        SortExpression="ChannelName" UniqueName="ChannelName">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="Titles" HeaderStyle-Width="120px" 
                                                        HeaderText="Title" ShowFilterIcon="false" SortExpression="Titles" 
                                                        UniqueName="Titles" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="ReportingManagerName" 
                                                        HeaderStyle-Width="120px" HeaderText="Reporting Manager" ShowFilterIcon="false" 
                                                        SortExpression="ReportingManagerName" UniqueName="ReportingManagerName" 
                                                        Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="ClusterManager" 
                                                        HeaderStyle-Width="130px" HeaderText="Cluster Manager" ShowFilterIcon="false" 
                                                        SortExpression="ClusterManager" UniqueName="ClusterManager" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="AreaManager" 
                                                        HeaderStyle-Width="130px" HeaderText="Area Manager" ShowFilterIcon="false" 
                                                        SortExpression="AreaManager" UniqueName="AreaManager">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="ZonalManagerName" 
                                                        HeaderStyle-Width="130px" HeaderText="Zonal Manager" ShowFilterIcon="false" 
                                                        SortExpression="ZonalManagerName" UniqueName="ZonalManagerName">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" 
                                                        CurrentFilterFunction="Contains" DataField="DeuptyHead" 
                                                        HeaderStyle-Width="130px" HeaderText="Deupty Head" ShowFilterIcon="false" 
                                                        SortExpression="DeuptyHead" UniqueName="DeuptyHead">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <HeaderStyle Width="180px" />
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" ScrollHeight="380px" UseStaticHeaders="true" />
                                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                                <Resizing AllowColumnResize="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAutoMatch" runat="server" CssClass="PCGMediumButton" 
                                        OnClick="btnAutoMatch_Click" Text="Auto Match" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnMannualMatch" runat="server" CssClass="PCGMediumButton" 
                                        OnClick="btnShowTransactionForManualMerge_Click" Text="Manual Match" />
                                </td>
                            </tr>
                        </table>
                        <%-- </asp:Panel>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table ID="ErrorMessage" runat="server" align="center" visible="false">
                        <tr>
                            <td>
                                <div align="center" class="failure-msg">
                                    No Records found.....
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div ID="divNote" runat="server" class="Note" style="margin: 6px">
                        <label ID="lbl">
                        Note:<br />
                        1:To export excel the count should not exceed 8000 records.
                        </label>
                    </div>
                    <asp:HiddenField ID="hdnRecordCount" runat="server" Visible="false" />
                    <asp:HiddenField ID="hdnCurrentPage" runat="server" />
                    <asp:HiddenField ID="hdnCustomerNameSearch" runat="server" Visible="false" />
                    <asp:HiddenField ID="hdnSchemeSearch" runat="server" Visible="false" />
                    <asp:HiddenField ID="hdnTranType" runat="server" Visible="false" />
                    <asp:HiddenField ID="hdnCategory" runat="server" />
                    <asp:HiddenField ID="hdnAMC" runat="server" />
                    <asp:HiddenField ID="hdnAgentCode" runat="server" />
                    <asp:HiddenField ID="hdnSubBrokerCode" runat="server" />
                    <asp:HiddenField ID="hdnFolioNumber" runat="server" Visible="false" />
                    <asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
                    <asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
                    <asp:HiddenField ID="txtParentCustomerId" runat="server" />
                    <asp:HiddenField ID="txtParentCustomerType" runat="server" />
                    <asp:HiddenField ID="hdnStatus" runat="server" />
                    <asp:HiddenField ID="hdnProcessIdSearch" runat="server" />
                    <asp:HiddenField ID="hdnExportType" runat="server" />
                </td>
            </tr>
        </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnTrnxExport" />
        <%-- <asp:PostBackTrigger ControlID="btnbalncExport" />--%>
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
<asp:HiddenField ID="txtcustomerId" runat="server" />
<asp:HiddenField ID="hdnMFTransaction" runat="server" />
