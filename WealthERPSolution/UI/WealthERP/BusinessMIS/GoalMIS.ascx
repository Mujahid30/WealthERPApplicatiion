<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoalMIS.ascx.cs" Inherits="WealthERP.BusinessMIS.GoalMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
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
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">Goal MIS</td>
        <td  align="right" id="tdGoalExport" runat="server" style="padding-bottom:2px;">
        <asp:ImageButton ID="btnGoalMIS" ImageUrl="~/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Visible="false"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
            onclick="btnGoalMIS_Click"></asp:ImageButton>
    </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>
<table class="TableBackground" width="100%">
    <tr id="trBranchRM" runat="server">
        <td align="left" style="width: 30%">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
            <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="left" style="width: 30%">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
             <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true"
                Style="vertical-align: middle" >
            </asp:DropDownList>
        </td>
        <td align="left" style="width: 40%">
        <asp:Label ID="lblGoal" runat="server" CssClass="FieldName" Text="Goal Type: "></asp:Label>
            <asp:DropDownList ID="ddlGoal" runat="server" CssClass="cmbField" AutoPostBack="true"
                Style="vertical-align: middle" >
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td align="left" style="width: 30%">
            <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS for :"></asp:Label>
            <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="left" style="width: 30%">
            <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
            <asp:DropDownList ID="ddlCustomerType" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Value="0" Text="Group Head"></asp:ListItem>
                <asp:ListItem Value="1" Text="Individual"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 40%">
        </td>
    </tr>
    <tr id="trCustomerSearch" runat="server">
        <td class="leftField" style="width:30%">
            
        </td>
        <td align="left" style="width: 30%" onkeypress="return keyPress(this, event)">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
            <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
           <%-- <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender"
                runat="server" TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName"
                ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" DelimiterCharacters="" OnClientItemSelected="GetCustomerId"
                Enabled="True" />--%>
                 <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="3" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                runat="server" ValidationGroup="CustomerValidation">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 40%">
        </td>
    </tr>
    <tr>
    <td align="left" style="width: 30%">
    <asp:Button ID="btnGo" runat="Server" Text="Go" CssClass="PCGButton" OnClick="btnGo_Click" />
    </td>
    <td class="leftField" style="width: 30%" colspan="2">&nbsp;</td>
    </tr> 
    <tr>
    
    </tr>
    <tr>
    <td colspan="3">
    <asp:Panel ID="tbl" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="false">
    <table>
     <tr>
       <td>
       <div id="dvHoldings" runat="server" style="width: 640px;">
                <telerik:RadGrid ID="gvGoalMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                    AllowAutomaticInserts="false" ExportSettings-FileName="Goal MIS" OnNeedDataSource="gvGoalMIS_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" FileName="Goal MIS" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CG_GoalId"
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                        <telerik:GridBoundColumn DataField="Customer_Name" AllowFiltering="true" HeaderText="Customer" UniqueName="Customer_Name"
                                SortExpression="Customer_Name" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="XG_GoalName" HeaderText="Goal"
                                AllowFiltering="false" UniqueName="XG_GoalName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CG_GoalYear" HeaderText="Goal Year"
                                AllowFiltering="false" UniqueName="CG_GoalYear">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CG_FVofCostToday" HeaderText="Goal Amount"
                                AllowFiltering="false" UniqueName="CG_FVofCostToday" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CG_CostToday" HeaderText="Cost Today"
                                AllowFiltering="false" UniqueName="CG_CostToday" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CG_GoalProfileDate" HeaderText="Goal setup Date"
                                AllowFiltering="false" UniqueName="CG_GoalProfileDate" DataFormatString="{0:d}" DataType="System.DateTime">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CG_InflationPer" HeaderText="Inflation(%)"
                                AllowFiltering="false" UniqueName="CG_InflationPer" DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CG_MonthlySavingsRequired" HeaderText="Monthly Savings Required"
                                AllowFiltering="false" UniqueName="CG_MonthlySavingsRequired" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" >
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CG_CurrentInvestment" HeaderText="Current Investment"
                                AllowFiltering="false" UniqueName="CG_CurrentInvestment" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CG_ROIEarned" HeaderText="Return of Existing Investment(%)"
                                AllowFiltering="false" UniqueName="CG_ROIEarned" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CG_ExpectedROI" HeaderText="Return Of Future Investment(%)"
                                AllowFiltering="false" UniqueName="CG_ExpectedROI" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CG_GoalDescription" HeaderText="Goal Description"
                                AllowFiltering="false" UniqueName="CG_GoalDescription">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CG_YearlySavingsRequired" HeaderText="Yearly Savings Required" 
                                AllowFiltering="false" UniqueName="CG_YearlySavingsRequired" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid></div>
               </td></tr></table>
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
    <asp:HiddenField ID="hdnCustomerId" runat="server" 
    onvaluechanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
<asp:HiddenField ID="hdnGoalCode" runat="server" />

<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:HiddenField ID="hdnadviserId" runat="server"/>
<asp:HiddenField ID="hdnAll" runat="server"/>
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />

<asp:HiddenField ID="HiddenField2" runat="server" />