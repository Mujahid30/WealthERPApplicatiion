<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderMIS.ascx.cs" Inherits="WealthERP.OPS.OrderMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        //alert(document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value());
        return false;
    };

</script>

<script type="text/javascript">
    $(document).ready(function() {
        $(".flip").click(function() { $(".panel").slideToggle(); });
    });

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
</script>

<style type="text/css">
    .ajax__calendar_container
    {
        z-index: 1000;
    }
</style>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {


        var totalChkBoxes = parseInt('<%= gvCustomerOrderMIS.Items.Count %>');
        var gvControl = document.getElementById('<%= gvCustomerOrderMIS.ClientID %>');


        var gvChkBoxControl = "cbRecons";


        var mainChkBox = document.getElementById("chkBxWerpAll");


        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {

            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<table style="width: 100%;">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            MF Order Recon
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                            <img src="../Images/helpImage.png" height="20px" width="25px" style="float: right;"
                                class="flip" />
                            <asp:ImageButton ID="btnMForderRecon" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnMForderRecon_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    1.View various orders.<br />
                    2.Match orders to the receive transactions.
                </p>
            </div>
        </td>
    </tr>
</table>
<table width="80%" onkeypress="return keyPress(this, event)">
    <tr runat="server" visible="false">
        <td align="right">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Order Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProductType" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                <asp:ListItem Value="MF" Text="Mutual Fund" Selected="True"></asp:ListItem>
                <asp:ListItem Value="IN" Text="Life Insurance" Enabled="false"></asp:ListItem>
                <asp:ListItem Value="FI" Text="FixedIncome" Enabled="false"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
        </td>
        <td>
        </td>
    </tr>
    <tr id="trOrderDates" visible="false" runat="server">
        <td align="right" valign="top">
            <asp:Label ID="lblFrom" runat="server" Text=" Order From Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFrom" runat="server" CssClass="txtField">
            </asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFrom"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvFromdate" ControlToValidate="txtFrom" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a  Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG" ControlToValidate="txtFrom"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblTo" runat="server" Text="Order To Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:TextBox ID="txtTo" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtTo"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvtoDate" ControlToValidate="txtTo" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="rfvPCG" ControlToValidate="txtTo"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
            <asp:CompareValidator ID="cvtodate" runat="server" ErrorMessage="<br/>To Date should not less than From Date"
                Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trFITransType" runat="server" visible="false">
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label2" runat="server" Text="Transaction type: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:DropDownList ID="ddlFITrxType" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                Width="150px">
                <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                <asp:ListItem Text="New" Value="New"></asp:ListItem>
                <asp:ListItem Text="Renewal" Value="Renewal"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trMFTransType" runat="server" visible="false">
        <td align="right" runat="server" visible="false">
            <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td runat="server" visible="false">
            <asp:DropDownList ID="ddlTrxType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="All" Value="All" Selected="true"></asp:ListItem>
                <asp:ListItem Text="New Purchase" Value="BUY"></asp:ListItem>
                <asp:ListItem Text="Additional Purchase" Value="ABY"></asp:ListItem>
                <asp:ListItem Text="Redemption" Value="Sel"></asp:ListItem>
                <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                <asp:ListItem Text="SWP" Value="SWP"></asp:ListItem>
                <asp:ListItem Text="STP" Value="STP"></asp:ListItem>
                <asp:ListItem Text="Switch" Value="SWB"></asp:ListItem>
                <asp:ListItem Text="Change Of Address Form" Value="CAF" Enabled="false"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblAMC" runat="server" Text="AMC: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" Width="300px">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Online/Offline:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOnlineOffline" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlOnlineOffline_SelectedIndexChanged">
                <asp:ListItem Value="Online" Text="Online" Enabled="false"></asp:ListItem>
                <asp:ListItem Value="Offline" Text="Offline"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rvOnlineOffline" ControlToValidate="ddlOnlineOffline"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Online/Offline Orders" Display="Dynamic"
                runat="server" InitialValue="Select" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblBranch" runat="server" Text="Select The Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trOrderStatus" visible="false" runat="server">
        <td align="right" runat="server" visible="false">
            <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: " CssClass="FieldName"></asp:Label>
        </td>
        <td runat="server" visible="false">
            <asp:DropDownList ID="ddlMISOrderStatus" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMISOrderStatus_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right" runat="server" visible="false">
            <asp:Label ID="lblOrderType" runat="server" Text="Order Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Immediate" Value="1" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Future" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" runat="server" visible="false">
            <asp:Label ID="lblRM" runat="server" Text="Select the RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td runat="server" visible="false">
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trCustomerType" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlCustomerType" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                <asp:ListItem Value="All" Text="All" Selected="True"></asp:ListItem>
                <asp:ListItem Value="0" Text="Group Head"></asp:ListItem>
                <asp:ListItem Value="1" Text="Individual"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
        </td>
        <td align="left" onkeypress="return keyPress(this, event)">
            <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                Enabled="false" AutoPostBack="True">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender"
                runat="server" TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName"
                ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" DelimiterCharacters="" OnClientItemSelected="GetCustomerId"
                Enabled="True" />
            <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                runat="server" ValidationGroup="CustomerValidation">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trSatus" runat="server">
        <td align="right">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Type:"></asp:Label>
        </td>
        <td>
         <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField"   Width="320px" >
            <asp:ListItem Text="All" Value="1" Enabled="false">
            </asp:ListItem>
             <asp:ListItem Text="Orders Exist & Transaction Exist(Accepted)" Value="2">
                </asp:ListItem>              
            
            <asp:ListItem Text="Orders Exist & Transaction Not Exist" Value="3">
            </asp:ListItem>
            <asp:ListItem Text="Orders Not Exist & Transaction Exist" Value="4">
            </asp:ListItem>
              <asp:ListItem Text="Orders Exist & Transaction Exist(Partial Match)" Value="5">
                </asp:ListItem>
             </asp:DropDownList>
             
        </td>
          
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="MFSubmit"
                Visible="false" OnClick="btnGo_Click" />
        </td>
        <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td colspan="2" align="left">
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="tbgvMIS" runat="server" class="Landscape" Height="100%" Width="100%"
    Visible="false" ScrollBars="Horizontal">
    <table width="100%">
        <tr>
            <td>
                <div id="dvOrderMIS" runat="server" style="width: 640px; height: 400px">
                    <telerik:RadGrid ID="gvCustomerOrderMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnItemDataBound="gvCustomerOrderMIS_ItemDataBound"
                        OnNeedDataSource="gvCustomerOrderMIS_OnNeedDataSource" ShowFooter="true" Skin="Telerik"
                        EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                        ExportSettings-FileName="MF Order Recon" OnUpdateCommand="gvCustomerOrderMIS_UpdateCommand"
                        Width="120%" Height="400px">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="MF Order Recon" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="CO_OrderId,CMFOD_OrderDetailsId,C_CustomerId,CP_portfolioId,PASP_SchemePlanCode,CMFA_AccountId,WMTT_TransactionClassificationCode,CMFOD_Amount,CO_OrderDate,PASP_SchemePlanSwitch,CO_IsOnline,CMFT_UserTransactionNo,Tra_TransactionType,Tra_AMC,Tra_SchemplanCode,TransactionPAN,TransactionSubbrokerCode"
                            Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None"
                            EditMode="PopUp">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Select" ShowFilterIcon="false" AllowFiltering="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                        <input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxes()" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="CO_OrderId" HeaderText="Order/Transaction No."
                                    SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                    AutoPostBackOnFilter="true" UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="OrderDetID" DataField="CMFOD_OrderNumber"
                                    Visible="false">
                                    <ItemStyle />
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn DataField="CO_OrderDate" HeaderText="Order Date" SortExpression="CO_OrderDate"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CO_OrderDate" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:d}"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridDateTimeColumn>
                                
                                <telerik:GridBoundColumn DataField="PAN" HeaderText="Order PAN" SortExpression="PAN"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PAN" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                 <telerik:GridBoundColumn DataField="SubbrokerCode" HeaderText="Order SubbrokerCode" SortExpression="SubbrokerCode"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="SubbrokerCode" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer" SortExpression="Customer_Name"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="WMTT_TransactionClassificationName" HeaderText="Trans Type"
                                    SortExpression="WMTT_TransactionClassificationName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="WMTT_TransactionClassificationName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="WOS_OrderStep" HeaderText="Status" SortExpression="WOS_OrderStep"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="WOS_OrderStep" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderStep" runat="server" Text='<%#Eval("WOS_OrderStep").ToString() %>'> </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="WOS_OrderStepCode" HeaderText="Status" SortExpression="WOS_OrderStepCode"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="WOS_OrderStepCode" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderStepCode" runat="server" Text='<%#Eval("WOS_OrderStepCode").ToString() %>'> </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="CMFOD_IsImmediate" HeaderText="Order Type"
                                    ShowFilterIcon="false" Visible="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblOrderTypeHeader" runat="server" Text="Order Type"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderType" runat="server" Text='<%#Eval("CMFOD_IsImmediate").ToString() %>'> </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderText="Folio No" SortExpression="CMFA_FolioNum"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFA_FolioNum" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="Scheme Name"
                                    SortExpression="PASP_SchemePlanName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="PASP_SchemePlanName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CO_ApplicationReceivedDate" HeaderText="App rcv Date"
                                    SortExpression="CO_ApplicationReceivedDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CO_ApplicationReceivedDate" DataFormatString="{0:d}"
                                    FooterStyle-HorizontalAlign="Left" AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CO_ApplicationNumber" HeaderText="App No" SortExpression="CO_ApplicationNumber"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CO_ApplicationNumber" FooterStyle-HorizontalAlign="Left" AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFOD_Amount" HeaderText="Amount" SortExpression="CMFOD_Amount"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFOD_Amount" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFOD_Units" HeaderText="Units" SortExpression="CMFOD_Units"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFOD_Units" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AB_BranchName" HeaderText="Branch" SortExpression="AB_BranchName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AB_BranchName" FooterStyle-HorizontalAlign="Left" AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RM_Name" HeaderText="RM" SortExpression="RM_Name"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="RM_Name" FooterStyle-HorizontalAlign="Left" AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_TransactionNumber" HeaderText="Trans No"
                                    SortExpression="CMFT_TransactionNumber" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CMFT_TransactionNumber" FooterStyle-HorizontalAlign="Left"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="TransactionPAN" HeaderText="Transaction PAN" SortExpression="TransactionPAN"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="TransactionPAN" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                 <telerik:GridBoundColumn DataField="TransactionSubbrokerCode" HeaderText="Transaction SubbrokerCode" SortExpression="TransactionSubbrokerCode"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="TransactionSubbrokerCode" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="ADUL_ProcessId" HeaderText="Upload ProcessID"
                                    SortExpression="ADUL_ProcessId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="ADUL_ProcessId" FooterStyle-HorizontalAlign="Left"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_TransactionDate" HeaderText="Trans Date"
                                    SortExpression="CMFT_TransactionDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CMFT_TransactionDate" FooterStyle-HorizontalAlign="Left"
                                    DataFormatString="{0:d}" AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_Price" HeaderText="Price (Rs)" SortExpression="CMFT_Price"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFT_Price" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_Amount" HeaderText="Trans Amount" SortExpression="CMFT_Amount"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFT_Amount" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_Units" HeaderText="Trans Units" SortExpression="CMFT_Units"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFT_Units" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COS_Reason" AllowFiltering="false" HeaderText="Remark"
                                    HeaderStyle-Width="80px" UniqueName="COS_Reason" SortExpression="COS_Reason"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="100px" UniqueName="MarkAsReject"
                                    EditText="Mark As Reject" CancelText="Cancel" UpdateText="OK">
                                    <ItemStyle Width="150px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="Order1" Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMatch" runat="server" Text="Add Order" OnClick="lnkMatch_SelectedIndexChanged"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings FormTableStyle-Height="40%" EditFormType="Template" FormMainTableStyle-Width="300px">
                                <FormTemplate>
                                    <table style="background-color: White;" border="0">
                                        <tr>
                                            <td colspan="4">
                                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                    Order Rejection Request
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Request No.:"></asp:Label>
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
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="Button1" Text="OK" runat="server" CssClass="PCGButton" CommandName="Update"
                                                    ValidationGroup="btnSubmit"></asp:Button>
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                    CommandName="Cancel"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </FormTemplate>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvFIOrderMIS" runat="server" style="width: 640px;" visible="false">
                    <telerik:RadGrid ID="gvCustomerFIOrderMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="false"
                        AllowAutomaticInserts="false" ExportSettings-FileName="MF Order Recon">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="MF Order Recon" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="CO_OrderId,CFIOD_DetailsId,C_CustomerId,CO_OrderDate,PAIC_AssetInstrumentCategoryCode"
                            Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Select">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                        <input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxes()" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Order No" AllowFiltering="false" DataField="CMFOD_OrderNumber">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFIOrderNo" runat="server" CssClass="cmbField" Text='<%# Eval("CFIOD_OrderNO") %>'
                                            OnClick="lnkFIOrderNo_Click">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn DataField="CO_OrderDate" HeaderText="Order Date" SortExpression="CO_OrderDate"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CO_OrderDate" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:d}"
                                    AllowFiltering="false">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer" SortExpression="Customer_Name"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="XS_Status" HeaderText="Status" SortExpression="XS_Status"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="XS_Status" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CO_ApplicationNumber" HeaderText="App No" SortExpression="CO_ApplicationNumber"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CO_ApplicationNumber" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CFIOD_AmountPayable" HeaderText="Amount" SortExpression="CFIOD_AmountPayable"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CFIOD_AmountPayable" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AB_BranchName" HeaderText="Branch" SortExpression="AB_BranchName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AB_BranchName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RM_Name" HeaderText="RM" SortExpression="RM_Name"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="RM_Name" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid></div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSync" runat="server" Text="Auto Match" CssClass="PCGMediumButton"
                    OnClick="btnSync_Click" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnMannualMatch" runat="server" Text="Manual Match" CssClass="PCGMediumButton"
                    OnClick="btnMannualMatch_Click" Visible="false" />
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="50%">
    <tr>
    </tr>
</table>
<asp:HiddenField ID="hdnBranchId" runat="server" />
<asp:HiddenField ID="hdnRMId" runat="server" />
<asp:HiddenField ID="hdnTransactionType" runat="server" />
<asp:HiddenField ID="hdnOrdStatus" runat="server" />
<asp:HiddenField ID="hdnOrderType" runat="server" />
<asp:HiddenField ID="hdnamcCode" runat="server" />
<asp:HiddenField ID="hdnFromdate" runat="server" />
<asp:HiddenField ID="hdnTodate" runat="server" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
<asp:HiddenField ID="hdnCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnProductType" runat="server" />
