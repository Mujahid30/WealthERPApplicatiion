<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetAllocationMIS.ascx.cs" Inherits="WealthERP.BusinessMIS.AssetAllocationMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">Asset Allocation MIS</td>
        <td  align="right">
                             
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
                CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true"
                Style="vertical-align: middle" >
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
                <asp:ListItem Value="0" Text="Group Head"></asp:ListItem>
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
    <td></td>
    <td>
    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" 
            ValidationGroup="ButtonGo" onclick="btnGo_Click" />
    </td>
    <td colspan="3"></td>
    </tr>
    <tr>
    <td colspan="5">
    <asp:Panel ID="tbl" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="true">
    <table>
     <tr>
       <td>
       <div id="dvHoldings" runat="server" style="width: 640px;">
                <telerik:RadGrid ID="gvAssetAllocationMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                    AllowAutomaticInserts="false" ExportSettings-FileName="AssetAllocation MIS"> 
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" FileName="AssetAllocation MIS" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView 
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                         <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Name"
                                AllowFiltering="false" UniqueName="CustomerName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="EquityCurrentValue" HeaderText="Equity Current Value"
                                AllowFiltering="false" UniqueName="EquityCurrentValue" DataFormatString="{0:C0}" >
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EquityCurrentPer" HeaderText="Equity Current Per"
                                AllowFiltering="false" UniqueName="EquityCurrentPer" DataFormatString="{0:C2}" >
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EquityRecomendedValue" HeaderText="EquityRecomendedValue"
                                AllowFiltering="false" UniqueName="EquityRecomendedValue" DataFormatString="{0:N0}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EquityRecomendedPer" HeaderText="EquityRecomendedPer"
                                AllowFiltering="false" UniqueName="EquityRecomendedPer" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="DebtCurrentValue" HeaderText="DebtCurrentValue"
                                AllowFiltering="false" UniqueName="DebtCurrentValue">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DebtCurrentPer" HeaderText="DebtCurrentPer"
                                AllowFiltering="false" UniqueName="DebtCurrentPer">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DebtRecomendedValue" HeaderText="DebtRecomendedValue"
                                AllowFiltering="false" UniqueName="DebtRecomendedValue">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DebtRecomendedPer" HeaderText="DebtRecomendedPer"
                                AllowFiltering="false" UniqueName="DebtRecomendedPer">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CashCurrentValue" HeaderText="CashCurrentValue"
                                AllowFiltering="false" UniqueName="CashCurrentValue">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CashCurrentPer" HeaderText="CashCurrentPer"
                                AllowFiltering="false" UniqueName="CashCurrentPer">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CashRecomendedValue" HeaderText="CashRecomendedValue"
                                AllowFiltering="false" UniqueName="CashRecomendedValue">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CashRecomendedPer" HeaderText="CashRecomendedPer"
                                AllowFiltering="false" UniqueName="CashRecomendedPer">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="AlternateCurrentValue" HeaderText="AlternateCurrentValue"
                                AllowFiltering="false" UniqueName="AlternateCurrentValue">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AlternateCurrentPer" HeaderText="AlternateCurrentPer"
                                AllowFiltering="false" UniqueName="AlternateCurrentPer">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AlternateRecomendedValue" HeaderText="AlternateRecomendedValue"
                                AllowFiltering="false" UniqueName="AlternateRecomendedValue">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AlternateRecomendedPer" HeaderText="AlternateRecomendedPer"
                                AllowFiltering="false" UniqueName="AlternateRecomendedPer">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
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
    onvaluechanged="hdnCustomerId_ValueChanged"  />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
<asp:HiddenField ID="hdnadviserId" runat="server"/>
<asp:HiddenField ID="hdnAll" runat="server"/>
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<asp:HiddenField ID="hdnschemeCade" runat="server" />
