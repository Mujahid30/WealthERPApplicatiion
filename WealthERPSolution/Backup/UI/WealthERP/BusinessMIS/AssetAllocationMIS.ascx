<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetAllocationMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.AssetAllocationMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

</script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function ResetDDL() {
        var installtype;
        var tenureYr;
        var tenureMn;
        var strtdate = new Date();

        document.getElementById("<%=ddlSelectCustomer.ClientID %>").value = "All Customer";

        document.getElementById("<%= trCustomerSearch.ClientID %>").visible = false;
        document.getElementById("<%= lblSelectTypeOfCustomer.ClientID %>").visible = false;
        document.getElementById("<%= ddlCustomerType.ClientID %>").visible = false;
    }
</script>

<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Asset Allocation MIS
                        </td>
                        <td align="right" id="tdExport" runat="server" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="btnImagExport" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnImagExport_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr id="trBranchRM" runat="server">
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                CssClass="cmbField" onchange="ResetDDL()" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" onchange="ResetDDL()"
                AutoPostBack="true" Style="vertical-align: middle">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 35%">
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS for :"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlCustomerType" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
                <%--<asp:ListItem Value="0" Text="Group Head"  ></asp:ListItem>--%>
                <asp:ListItem Value="1" Text="Individual"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 35%">
        </td>
    </tr>
    <tr id="trCustomerSearch" runat="server">
        <td class="leftField" style="width: 15%">
            &nbsp;
        </td>
        <td class="rightField" style="width: 15%">
            &nbsp;
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
        </td>
        <td align="left" width="10%" onkeypress="return keyPress(this, event)">
            <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">  </asp:TextBox>
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
                runat="server" ValidationGroup="ButtonGo">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 35%">
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="ButtonGo"
                OnClick="btnGo_Click" />
        </td>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:Panel ID="tbl" runat="server" ScrollBars="Horizontal" Width="98%" Visible="true">
                <table>
                    <tr>
                        <td>
                            <div id="dvHoldings" runat="server" style="width: 640px;">
                                <telerik:RadGrid ID="gvAssetAllocationMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-FileName="AssetAllocation MIS" OnNeedDataSource="gvAssetAllocationMIS_OnNeedDataSource"
                                    OnItemDataBound="gvAssetAllocationMIS_ItemDataBound">
                                    <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                        filename="AssetAllocation MIS" excel-format="ExcelML">
                    </exportsettings>
                                    <mastertableview width="100%" allowmulticolumnsorting="True" autogeneratecolumns="false"
                                        commanditemdisplay="None">
                        <Columns>
                         <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Name"
                                SortExpression="CustomerName" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CustomerName" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="EquityCurrentValue" HeaderText="Eq Current Value"
                                AllowFiltering="false" UniqueName="EquityCurrentValue" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" >
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EquityCurrentPer" HeaderText="Eq Current (%)"
                                AllowFiltering="false" UniqueName="EquityCurrentPer" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EquityRecomendedValue" HeaderText="Eq Rec Value"
                                AllowFiltering="false" UniqueName="EquityRecomendedValue" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EquityRecomendedPer" HeaderText="Eq Rec (%)" SortExpression="EquityRecomendedPer"
                                AllowFiltering="false" UniqueName="EquityRecomendedPer" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EqValueAction" HeaderText="Eq Action Needed" SortExpression="EqValueAction"
                                AllowFiltering="false" UniqueName="EqValueAction" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EqPerAction" HeaderText="Eq Action Needed (%)" SortExpression="EqPerAction"
                                AllowFiltering="false" UniqueName="EqPerAction" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Eq Action Indicator" ShowFilterIcon="false" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Image ID="imgEqIndicator" ImageAlign="Middle" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        
                        
                        <telerik:GridBoundColumn DataField="DebtCurrentValue" HeaderText="Debt Current Value"
                                AllowFiltering="false" UniqueName="DebtCurrentValue" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DebtCurrentPer" HeaderText="Debt Current (%)"
                                AllowFiltering="false" UniqueName="DebtCurrentPer" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DebtRecomendedValue" HeaderText="Debt Rec Value"
                                AllowFiltering="false" UniqueName="DebtRecomendedValue" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DebtRecomendedPer" HeaderText="Debt Rec (%)"
                                AllowFiltering="false" UniqueName="DebtRecomendedPer" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DtValueAction" HeaderText="Debt Action Needed" SortExpression="DtValueAction"
                                AllowFiltering="false" UniqueName="DtValueAction" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DtPerAction" HeaderText="Debt Action Needed (%)" SortExpression="DtPerAction"
                                AllowFiltering="false" UniqueName="DtPerAction" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Debt Action Indicator" ShowFilterIcon="false" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Image ID="imgDtIndicator" ImageAlign="Middle" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        
                        
                        <telerik:GridBoundColumn DataField="CashCurrentValue" HeaderText="Cash Current Value"
                                AllowFiltering="false" UniqueName="CashCurrentValue" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CashCurrentPer" HeaderText="Cash Current (%)"
                                AllowFiltering="false" UniqueName="CashCurrentPer" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CashRecomendedValue" HeaderText="Cash Rec Value"
                                AllowFiltering="false" UniqueName="CashRecomendedValue" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CashRecomendedPer" HeaderText="Cash Rec (%)"
                                AllowFiltering="false" UniqueName="CashRecomendedPer" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CaValueAction" HeaderText="Cash Action Needed" SortExpression="CaValueAction"
                                AllowFiltering="false" UniqueName="CaValueAction" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CaPerAction" HeaderText="Cash Action Needed (%)" SortExpression="CaPerAction"
                                AllowFiltering="false" UniqueName="CaPerAction" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Cash Action Indicator" ShowFilterIcon="false" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Image ID="imgCaIndicator" ImageAlign="Middle" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridBoundColumn DataField="AlternateCurrentValue" HeaderText="Alt Current Value"
                                AllowFiltering="false" UniqueName="AlternateCurrentValue" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AlternateCurrentPer" HeaderText="Alt Current (%)"
                                AllowFiltering="false" UniqueName="AlternateCurrentPer" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="AlternateRecomendedValue" HeaderText="Alt Rec Value"
                                AllowFiltering="false" UniqueName="AlternateRecomendedValue" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AlternateRecomendedPer" HeaderText="Alt Rec (%)"
                                AllowFiltering="false" UniqueName="AlternateRecomendedPer" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AltValueAction" HeaderText="Alt Action Needed" SortExpression="AltValueAction"
                                AllowFiltering="false" UniqueName="AltValueAction" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AltPerAction" HeaderText="Alt Action Needed (%)" SortExpression="AltPerAction"
                                AllowFiltering="false" UniqueName="AltPerAction" DataFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Alt Action Indicator" ShowFilterIcon="false" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Image ID="imgAltIndicator" ImageAlign="Middle" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        
                        </Columns>
                    </mastertableview>
                                    <clientsettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </clientsettings>
                                </telerik:RadGrid></div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="ErrorMessage" align="center" runat="server">
    <tr>
        <td>
            <div class="failure-msg" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnAll" runat="server" />
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<asp:HiddenField ID="hdnschemeCade" runat="server" />
