<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderEntry.ascx.cs" Inherits="WealthERP.OPS.OrderEntry" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

    
</script>

 <%--<script type="text/javascript">

     function ShowHideFuture() {


       
         if (document.getElementById("<%= rbtnFuture.ClientID %>").checked == true) {
          

             document.getElementById("<%= trFutureTrigger.ClientID %>").style.display = 'block';
             document.getElementById("<%= trfutureDate.ClientID %>").style.display = 'block';
         }
         else if (document.getElementById("<%= rbtnImmediate.ClientID %>").checked == true) {

         document.getElementById("<%= trFutureTrigger.ClientID %>").style.display = 'none';
         document.getElementById("<%= trfutureDate.ClientID %>").style.display = 'none';
       
         }
          
     }
  

</script>--%>
<asp:Label ID="lblOrderEntry" runat="server" CssClass="HeaderTextBig" Text="Order Entry"></asp:Label>
<br />
<hr />
<telerik:RadToolBar ID="aplToolBar" runat="server" Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%"  OnButtonClick="aplToolBar_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>
        
        <telerik:RadToolBarButton ID="btnPrintTransSlip" runat="server" Text="Print Transaction Slip" Value="PrintTransactionSlip" ToolTip="Print Transaction Slip">            
        </telerik:RadToolBarButton>
        
        <telerik:RadToolBarButton ID="btnSubmitOnline" runat="server" Text="Submit Online" Value="SubmitOnline" ToolTip="Submit Online">            
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<br />
<br />
<table width="85%">
<tr>
  <td align="right">
<asp:Label ID="Label3" runat="server" Text="Asset Type: "  CssClass="FieldName"></asp:Label>
</td>
  <td>
    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="cmbField" AutoPostBack="true"
          onselectedindexchanged="DropDownList2_SelectedIndexChanged">
       <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Mutual Fund" Value="Mutual Fund"></asp:ListItem>
        <asp:ListItem Text="Equity" Value="Equity"></asp:ListItem>
        <asp:ListItem Text="Insurance" Value="Insurance"></asp:ListItem>
        <asp:ListItem Text="Loan Application" Value="Loan Application"></asp:ListItem>
        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
     </asp:DropDownList>
  </td>
  <td align="right">
  <asp:Label ID="lblCustomer" runat="server" Text="Customer: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
    <asp:HiddenField ID="txtCustomerId" runat="server"  Visible="true" 
            onvaluechanged="txtCustomerId_ValueChanged" />
   <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" Text="Gk Jain"></asp:TextBox>
    <%-- <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" TargetControlID="txtCustomerName" WatermarkText="Enter few chars of Customer"
     runat="server" EnableViewState="false">
     </cc1:TextBoxWatermarkExtender>--%>
     <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
     TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
     MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
      CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
     CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
     UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters="" Enabled="True"  />
     
       <asp:Button ID="btnAddCustomer" runat="server" Text="Add a Customer" 
          CssClass="PCGMediumButton" CausesValidation="false" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddCustomer','M');"
                
          onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddCustomer','M');" 
          onclick="btnAddCustomer_Click" />
    </td>
</tr>

<tr>
  <td align="right">
  <asp:Label ID="lblBranch" runat="server" Text="Branch: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
   <asp:Label ID="Label4" runat="server" Text="Kalina"  CssClass="txtField"></asp:Label>
  </td>
  <td align="right">
  <asp:Label ID="lblRM" runat="server" Text="RM: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:Label ID="Label5" runat="server" Text="Walkin"  CssClass="txtField"></asp:Label>
  </td>
</tr>


<tr id="trSectionTwo1" runat="server">
<td colspan="4">
<hr />
</td>
</tr>

<tr id="trSectionTwo2" runat="server">
<td align="right">
  <asp:Label ID="lblReceivedDate" runat="server" Text="Application Received Date: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
  <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceivedDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                TargetControlID="txtReceivedDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
  </td>
   <td align="right">
  <asp:Label ID="lblApplicationNumber" runat="server" Text="Application Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
  
</tr>

<tr id="trSectionTwo3" runat="server">
  <td align="right">
  <asp:Label ID="ddlTransactionType" runat="server" Text="Transaction Type: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField" AutoPostBack="true" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
         <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
        <asp:ListItem Text="New Purchase" Value="New_Purchase"></asp:ListItem>
        <asp:ListItem Text="Additional Purchase" Value="Additional_Purchase"></asp:ListItem>
        <asp:ListItem Text="Sell" Value="Sell"></asp:ListItem>
        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
        <asp:ListItem Text="SWP" Value="SWP"></asp:ListItem>
        <asp:ListItem Text="STP" Value="STP"></asp:ListItem>
        <asp:ListItem Text="Switch" Value="Switch"></asp:ListItem>
        <asp:ListItem Text="Change Of Bank Details" Value="Change_Of_Bank_Details"></asp:ListItem>
        <asp:ListItem Text="Change Of Address Form" Value="Change_Of_Address_Form"></asp:ListItem>
        <asp:ListItem Text="ConsoliDation Of Folio" Value="ConsoliDation_Of_Folio"></asp:ListItem>
        <asp:ListItem Text="Change Of Nominee" Value="Change_Of_Nominee"></asp:ListItem>
    </asp:DropDownList>
  </td>
  <td colspan="2">
   </td>
</tr>

<tr id="trSectionTwo4" runat="server">
<td align="right">
<asp:Label ID="Label6" runat="server" Text="AMC: "  CssClass="FieldName"></asp:Label>
</td>
<td align="left">
  <asp:DropDownList ID="ddlAMCList" runat="server" CssClass="cmbField">
  <asp:ListItem Selected="True" Text="Birla Sun Life Mutual Fund" Value="Birla Sun Life Mutual Fund"></asp:ListItem>
  </asp:DropDownList>
</td>

<td align="right">
<asp:Label ID="Label7" runat="server" Text="Category: "  CssClass="FieldName"></asp:Label>
</td>
<td align="left">
  <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField">  
  <asp:ListItem Text="Equity" Value="Equity"></asp:ListItem>
  <asp:ListItem  Selected="True" Text="Debt" Value="Debt"></asp:ListItem>
  <asp:ListItem Text="Hybrid" Value="Hybrid"></asp:ListItem>
  </asp:DropDownList>
</td>
</tr>

<tr id="trSectionTwo5" runat="server">
  <td align="right">
  <asp:Label ID="lblSearchScheme" runat="server" Text="Scheme: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>  
<asp:DropDownList ID="ddlAmcSchemeList" runat="server" CssClass="cmbField">
<asp:ListItem value="19">Birla Fixed Term Debt Fund - Series 1-36 Months-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Debt Fund - Series 1-36 Months-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Quarterly Series 21 DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Quarterly Series 21 GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Quarterly Series 22 DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Quarterly Series 22 GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Quarterly Series 23 DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Quarterly Series 23 GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series T Retail -GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AW INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AW INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AW RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AW RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AY INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AY RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AY RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series D-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series G-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series O-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series O-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series P-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series P-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series Q - DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series Q - GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series R-Institutional-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series R-Institutional-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series R-Retail-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series R-Retail-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series S-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series S-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series S-Institutional DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series S-Institutional GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series T Retail-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series T-Institutional DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series T-Institutional GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series U Institutional-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series U Institutional-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series U Retail-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series U Retail-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series V - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series V - INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series V - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series V - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series W - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series W - INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series W - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series W - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan &ndash; Half Yearly Series 4 DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan &ndash; Half Yearly Series 4 GROWTH</asp:ListItem>
<asp:ListItem value="19">BIRLA FIXED TERM PLAN QUARTERLY SERIES 5-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA FIXED TERM PLAN QUARTERLY SERIES 5-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla FTP - Quarterly -Series 13 - Dividend</asp:ListItem>
<asp:ListItem value="19">Birla FTP - Quarterly -Series 7 - Dividend</asp:ListItem>
<asp:ListItem value="19">BIRLA FTP-HALF YEARLY SERIES 3-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA FTP-HALF YEARLY SERIES 3-GROWTH</asp:ListItem>
<asp:ListItem value="19">BIRLA FTP-QUARTERLY SERIES 20 - DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA FTP-QUARTERLY SERIES 20 - GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Buy India Fund-Plan A(Divivdend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Buy India Fund-Plan B(Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Half Yearly Series 5 - Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life 95 Fund-Plan A (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life 95 Fund-Plan B(Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Advantage Fund-Plan A (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Advantage Fund-Plan B (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Asset Allocation Fund - Conservative Plan - Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Asset Allocation Fund-Aggressive Plan-Dividend Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Asset Allocation Fund-Aggressive Plan-Growth Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Asset Allocation Fund-Conservative Plan-Growth Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Asset Allocation Fund-Moderate Plan-Dividend Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Asset Allocation Fund-Moderate Plan-Growth Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Basic Industries Fund - Dividend Trigger Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Basic Industries Fund-Plan A(Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Basic Industries Fund-Plan B(Growth)</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE CAPITAL PROTECTION ORIENTED FUND-3 YRS- DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE CAPITAL PROTECTION ORIENTED FUND-3 YRS- GROWTH</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE CAPITAL PROTECTION ORIENTED FUND-5 YRS-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE CAPITAL PROTECTION ORIENTED FUND-5 YRS-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Manager-Plan A(Institutional Daily Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Manager-Plan B(Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Manager-Plan C(Institutional Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Manager-Plan D(Weekly Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Manager-Plan E(Institutional Weekly Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus Sweep Plan-Dividend Option</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus- Discipline Advantage Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional - Weekly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional - Daily Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional - Fortnightly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional Premium - Weekly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional Premium - Fortnightly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional Premium Plan (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional Premium Plan - Daily Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Institutional Premium Plan-Monthly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Retail (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Cash Plus-Retail (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Agri Plan -Institutional Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Agri Plan -Institutional Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Agri Plan -Retail Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Agri Plan -Retail Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Multi Commodity Plan -Institutional Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Multi Commodity Plan -Institutional Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Multi Commodity Plan -Retail Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Multi Commodity Plan -Retail Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Precious Metals Plan -Institutional Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Precious Metals Plan -Institutional Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Precious Metals Plan -Retail Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Commodities Equities Fund Global Precious Metals Plan -Retail Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Dividend Yield Plus-Plan A (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Dividend Yield Plus-Plan B (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Dynamic Bond Fund-Discipline Advantage Plan-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Dynamic Bond Fund-Retail Plan-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Dynamic Bond Fund-Retail Plan-Monthly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Dynamic Bond Fund-Retail Plan-Quarterly Dividend</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE ENHANCED ARBITRAGE FUND - INSTITUTIONAL PLAN - DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE ENHANCED ARBITRAGE FUND - RETAIL PLAN - DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE ENHANCED ARBITRAGE FUND - RETAIL PLAN - GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Equity Fund-Plan A(Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Equity Fund-Plan B(Growth)</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP ? SERIES B INSTITUTIONAL Growth</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP ? SERIES B RETAIL Growth</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP &ndash; SERIES A RETAIL Dividend</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP &ndash; SERIES A RETAIL Growth</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP &ndash; SERIES B INSTITUTIONAL Dividend</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP &ndash; SERIES B RETAIL Dividend</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP &ndash; SERIES C RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP &ndash; SERIES C RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP &ndash; SERIES D RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE EQUITY LINKED FMP &ndash; SERIES D RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Maturity Plan - Annual Series 3-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Maturity Plan - Annual Series 3-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Maturity Plan - Annual Series 1-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Maturity Plan - Annual Series 1-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Maturity Plan - Quarterly Series II-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Maturity Plan - Quarterly Series II-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Debt Fund Series 3-36 Months-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Debt Fund Series 3-36 Months-Growth</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE FIXED TERM PLAN - 24 MONTHS - DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE FIXED TERM PLAN - 24 MONTHS - GROWTH</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE FIXED TERM PLAN - 24 MONTHS - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE FIXED TERM PLAN - 24 MONTHS - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AA - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AA - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AA - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AA- INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AB - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AB - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AB - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AB- INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AC - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AC - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AC - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AC- INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AD - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AD - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AD - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AD- INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AE - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AE - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AE - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AE- INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AF INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AF INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AF RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AF RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AJ - Institutional Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AJ - Institutional Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AJ - Retail Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AJ - Retail Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AK INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AK INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AK RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AK RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AL INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AL INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AL RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AL RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AM INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AM INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AM RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AM RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AN INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AN RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AN RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AO INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AO RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AO RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AP INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AP RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AP RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AQ INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AQ RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AQ RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AR INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AR RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AR RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AS INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AS INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AS RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AS RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AT INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AT INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AT RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AT RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AU INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AU INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AU RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AU RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AV INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AV INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AV RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AV RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AZ INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AZ INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AZ RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series AZ RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BA INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BA INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BA RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BA RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BB - Institutional Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BB - Institutional Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BB - Retail Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BB - Retail Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BC INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BC INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BC RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BC RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BD INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BD RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BD RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BE INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BE INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BE RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BE RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BF INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BF RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BF RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BG INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BG INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BG RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BG RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BH INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BH INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BH RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BH RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BI INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BI INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BI RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BI RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BJ INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BJ INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BJ RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BJ RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BK - Institutional - Growth Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series BK - Retail - Growth Plan</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series N-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series N-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series X - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series X - INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series X - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series X - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series Y - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series Y - INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series Y - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan - Series Y - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Half Yearly Series 5 - Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Series AG - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Series AG - INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Series AG - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Series AG - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Series AH - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Series AH - INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Series AH - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan &ndash; Series AH - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Fixed Term Plan Series BV - Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Long Term - Institutional Plan-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Long Term - Institutional Plan-Weekly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Long Term - Retail Plan-Daily Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Long Term - Retail Plan-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Long Term - Retail Plan-Weekly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Long Term Plan-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Short Term Plan-Daily Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Short Term Plan-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Short Term Plan-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Short Term Plan-Institutional Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Short Term Plan-Institutional Fortnightly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Short Term Plan-Institutional Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Floating Rate Fund-Short Term Plan-Institutional weekly Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Freedom Fund-Plan A (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Freedom Fund-Plan B (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Frontline Equity Fund-Plan A (Divivdend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Frontline Equity Fund-Plan B (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Frontline Equity Fund-Plan B (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Frontline Equity Fund-Plan B(Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-Liquid Plan (Annual Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-Liquid Plan-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-Liquid Plan-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-PF Plan (Annual Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-PF Plan-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-PF Plan-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-Regular Plan (Annual Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-Regular Plan-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Gilt Plus-Regular Plan-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Long Term Fund-Plan A (Divivdend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Long Term Fund-Plan B (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Short Term Fund-Institutional Daily Dividend Option</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Short Term Fund-Institutional Plan A(Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Short Term Fund-Institutional Weekly Dividend Option</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Short Term Fund-Retail Daily Dividend Option</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Short Term Fund-Retail Plan A(Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Short Term Fund-Retail Plan B(Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Govt, Securities Short Term Fund-Retail Weekly Dividend Option</asp:ListItem>
<asp:ListItem value="19">BIRLA SUN LIFE INCOME FUND (Discipline Advantage Plan)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Fund-Plan A(Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Fund-Plan B(Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Fund-Plan C(Quarterly Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Fund-Plan D(54EA Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Fund-Plan E(54EA Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Fund-PLan F(54EB Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Fund-Plan G(54EB Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Plus (Discipline Advantage Plan)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Plus (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Income Plus (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Index Fund-Plan A (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Index Fund-Plan B (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life India Gennext Fund-Dividend Option</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life India Gennext Fund-Growth Option</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life India Opportunities Fund-Plan A (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life India Opportunities Fund-Plan B (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Infrastructure Fund-Plan A (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Infrastructure Fund-Plan B (Dividend)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life Infrastructure Fund-Plan B (Growth)</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life International Equity Fund Plan A- Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life International Equity Fund Plan A- Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life International Equity Fund Plan B - Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Sun Life International Equity Fund Plan B - Growth</asp:ListItem>
<asp:ListItem value="19">Birla Sun life Interval Income Fund - MONTHLY Plan Series I INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun life Interval Income Fund - MONTHLY Plan Series I INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Sun life Interval Income Fund - MONTHLY Plan Series I RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Sun life Interval Income Fund - MONTHLY Plan Series I RETAIL GROWTH</asp:ListItem>

</asp:DropDownList> 
  
   
  </td>
  <td align="right">
  <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
   <asp:DropDownList ID="ddlFolioNumber" runat="server" CssClass="cmbField" >
   <asp:ListItem Text="7034832/85" Value="7034832/85" Selected="True"></asp:ListItem>
   <asp:ListItem Text="5360043/46" Value="5360043/46"></asp:ListItem>
   <asp:ListItem Text="1166727/64" Value="1166727/64"></asp:ListItem>
   <asp:ListItem Text="1831166/97" Value="1831166/97"></asp:ListItem>
   <asp:ListItem Text="2637263/16" Value="2637263/16"></asp:ListItem>
   </asp:DropDownList>
   <asp:Button ID="btnAddFolio" runat="server" Text="Add a Folio" CssClass="PCGMediumButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_OrderEntry_btnAddFolio','M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_OrderEntry_btnAddFolio','M');" Visible="false"/>
  </td>
 
</tr>

<tr id="trSectionTwo6" runat="server">
  <td align="right">
  <asp:Label ID="lblOrderDate" runat="server" Text="Order Date: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
  <asp:Label ID="Label10" runat="server" Text="3/8/2011"  CssClass="cmbField"></asp:Label>   
         
  </td>
  <td align="right">
  <asp:Label ID="Label2" runat="server" Text="Portfolio: "  CssClass="FieldName"></asp:Label>
  </td>
  <td align="left">
   <asp:Label ID="Label8" runat="server" Text="MyPortfolio"  CssClass="txtField"></asp:Label>
  </td>
  
</tr>

<tr id="trSectionTwo7" runat="server">
<td align="right">
  <asp:Label ID="lblOrderNumber" runat="server" Text="Order Number: "  CssClass="FieldName"></asp:Label>
  </td>
   <td align="left">
   <asp:Label ID="Label11" runat="server" Text="10001"  CssClass="txtField"></asp:Label>
  </td>
  <td colspan="2">
  </td>
</tr>

<tr id="trSectionTwo8" runat="server">
  <td align="right">
     <asp:Label ID="lblOrderType" runat="server" Text="Order Type: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
<asp:RadioButton ID="rbtnImmediate" Class="cmbField"  runat="server" AutoPostBack="true" GroupName="OrderDate" Checked="True"  Text="Immediate" OnCheckedChanged="rbtnImmediate_CheckedChanged" />
<asp:RadioButton ID="rbtnFuture" Class="cmbField" runat="server" AutoPostBack="true" GroupName="OrderDate" Text="Future" OnCheckedChanged="rbtnFuture_CheckedChanged" />
</td>
 <td align="right">
 <asp:Label ID="Label9" runat="server" Text="Order Status: "  CssClass="FieldName"></asp:Label>
 </td>
 <td align="left">
  <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField">
  <asp:ListItem Text="In Progress" Value="In Progress" Selected="True">  </asp:ListItem>
  <asp:ListItem Text="Pending" Value="Pending">  </asp:ListItem>
  <asp:ListItem Text="Executed" Value="Executed">  </asp:ListItem>
  <asp:ListItem Text="Cancelled" Value="Cancelled">  </asp:ListItem>
  <asp:ListItem Text="Reject" Value="Reject">  </asp:ListItem>
  </asp:DropDownList>
</td>
</tr>

<tr id="trSectionTwo9" runat="server">
<td colspan="2">
</td>
<td align="right" valign="top">
<asp:Label ID="lblOrderPendingReason" runat="server" Text="Order Pending Reason: "  CssClass="FieldName"></asp:Label>
</td>
<td valign="top">
        <asp:DropDownList ID="ddlOrderPendingReason" runat="server" CssClass="cmbField">
        <asp:ListItem Text="KYC not completed" Value="KYC not completed" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Product information not adequate/client wants more details" Value="Product information not adequate/client wants more details"></asp:ListItem>
        <asp:ListItem Text="Waiting for market correction/ levels" Value="Waiting for market correction/ levels"></asp:ListItem>
        <asp:ListItem Text="Incomplete information" Value="Incomplete information"></asp:ListItem>
        <asp:ListItem Text="Support documentation not provided" Value="Support documentation not provided"></asp:ListItem>
        <asp:ListItem Text="For want of funds,/post Dated cheque" Value="For want of funds,/post Dated cheque"></asp:ListItem>
        <asp:ListItem Text="Attestation pending (signature verfication)" Value="Attestation pending (signature verfication)"></asp:ListItem>
        <asp:ListItem Text="Signature not done/not tallied" Value="Signature not done/not tallied"></asp:ListItem>
    </asp:DropDownList>
</td>
</tr>

<tr id="trSectionTwo10" runat="server">
 
 <td  align="right">
 <asp:Label ID="lblFutureDate" runat="server" Text="Select Future Date: "  CssClass="FieldName"></asp:Label>
 </td>
 <td>
   <asp:TextBox ID="txtFutureDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFutureDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                TargetControlID="txtFutureDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
</td>

<td align="right">
<asp:Label ID="lblFutureTrigger" runat="server" Text="Future Trigger: "  CssClass="FieldName"></asp:Label>
</td>
 <td>
 <asp:TextBox ID="txtFutureTrigger" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
</td>

</tr>







<tr id="trPurchase" runat="server">
<td colspan="4">
<hr />
</td>
</tr>


<tr id="trPurchase1" runat="server">

  <td align="right">
  <asp:Label ID="lblAmount" runat="server" Text="Amount: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
  
  <td align="right" valign="top">
  <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:CheckBox ID="chkCheque" runat="server" CssClass="cmbField" Text="Cheque" Checked="true"  />
<asp:CheckBox ID="chkECS" runat="server" CssClass="cmbField" Text="ECS" Checked="false" />
<asp:CheckBox ID="chkDraft" runat="server" CssClass="cmbField" Text="Draft" Checked="false"/>
</td>
  
</tr>


<tr  id="trPurchase2" runat="server">
  <td align="right" valign="top">
 <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument Number: "  CssClass="FieldName"></asp:Label>
  </td>
  <td valign="top">
  <asp:TextBox ID="txtPaymentNumber" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
   
  <td  align="right">
 <asp:Label ID="Label1" runat="server" Text="Payment Instrument Date: "  CssClass="FieldName"></asp:Label>
 </td>
  <td>
   <asp:TextBox ID="txtPaymentInstDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPaymentInstDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                TargetControlID="txtPaymentInstDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
</td>


</tr>

<tr id="trPurchase3" runat="server">
  <td align="right">
  <asp:Label ID="lblPaymentDetails" runat="server" Text="Bank Name: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtPaymentDetails" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
   <td align="right">
  <asp:Label ID="lblBankDetails" runat="server" Text="Branch Name: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
 <asp:TextBox ID="txtBankDetails" runat="server" CssClass="txtField"></asp:TextBox>
  </td>
</tr>

<tr id="trPurchase4" runat="server">
<td colspan="4">
<hr />
</td>
</tr>

<tr id="trSell1" runat="server">
   <td align="right">
  <asp:Label ID="Label12" runat="server" Text="Available Amount: "  CssClass="FieldName"></asp:Label>
  </td>
   <td align="left">
  <asp:Label ID="Label13" runat="server" Text="5000000"  CssClass="txtField"></asp:Label>
  </td>
   
   <td align="right">
  <asp:Label ID="Label14" runat="server" Text="Available Units: "  CssClass="FieldName"></asp:Label>
  </td>
   <td align="left">
  <asp:Label ID="Label15" runat="server" Text="50000 "  CssClass="txtField"></asp:Label>
  </td>
 
</tr>


<tr id="trSell2" runat="server">
  <td align="right">
  <asp:Label ID="Label16" runat="server" Text="Redeem/Switch: "  CssClass="FieldName"></asp:Label>
  </td>
  <td>
<asp:RadioButton ID="rbtAmount" Class="cmbField"  runat="server" AutoPostBack="true" GroupName="AmountUnit" Checked="True"  Text="Amount"/>
<asp:RadioButton ID="rbtUnit" Class="cmbField" runat="server" AutoPostBack="true" GroupName="AmountUnit" Text="Units"/>
</td>

 <td align="right">
   <asp:Label ID="lblAmountUnits" runat="server" Text="Amount/Units: "  CssClass="FieldName"></asp:Label>
  </td>
 <td>
 <asp:TextBox ID="txtUnits" runat="server" CssClass="txtField"></asp:TextBox>
 </td>

</tr>

<tr id="trSell3" runat="server">
  <td align="right">
  <asp:Label ID="Label17" runat="server" Text="To Scheme: "  CssClass="FieldName"></asp:Label>
  </td>
   <td align="left">
   <asp:DropDownList ID="ddlToScheme" runat="server" CssClass="cmbField" >
   <asp:ListItem value="19" Selected="True">Birla Fixed Term Plan - Series AW RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AW RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AY INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AY RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series AY RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series D-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series G-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series O-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series O-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series P-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series P-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series Q - DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series Q - GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series R-Institutional-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series R-Institutional-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series R-Retail-Dividend</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series R-Retail-Growth</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series S-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series S-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series S-Institutional DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series S-Institutional GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series T Retail-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series T-Institutional DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series T-Institutional GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series U Institutional-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series U Institutional-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series U Retail-DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series U Retail-GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series V - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series V - INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series V - RETAIL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series V - RETAIL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series W - INSTITUTIONAL DIVIDEND</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series W - INSTITUTIONAL GROWTH</asp:ListItem>
<asp:ListItem value="19">Birla Fixed Term Plan - Series W - RETAIL DIVIDEND</asp:ListItem>
   </asp:DropDownList>
   </td>
<td colspan="2">
</td>
</tr>

<tr id="trSell4" runat="server">
<td colspan="4">
<hr />
</td>
</tr>


<%--Current Address--%>
 <tr id="trAddress1" runat="server">
<td colspan="4">
 <asp:Label ID="Label23" CssClass="HeaderTextSmall" runat="server" Text="Current Address"></asp:Label>
</td>
</tr>
   
 <tr id="trAddress2" runat="server">

                            <td class="leftField">
                                <asp:Label ID="Label24" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="Label26" CssClass="txtField" runat="server" Text="E-757"></asp:Label>                          
                            </td>
                       
                            <td class="leftField">
                                <asp:Label ID="Label25" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="Label27" CssClass="txtField" runat="server" Text="K NAGAR"></asp:Label>
                            </td>
                        
</tr>
  
 <tr id="trAddress3" runat="server">
                            <td class="leftField">
                                <asp:Label ID="Label28" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                            </td>
                            <td class="rightField">
                                 <asp:Label ID="Label30" CssClass="txtField" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label29" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="Label31" CssClass="txtField" runat="server" Text=""></asp:Label>
                            </td>
   </tr>
   
 <tr id="trAddress4" runat="server">
                            <td class="leftField">
                                <asp:Label ID="Label32" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td class="rightField">
                             <asp:Label ID="Label36" CssClass="txtField" runat="server" Text="AGRA"></asp:Label>                               
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label33" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                            </td>
                            
                            <td class="rightField">
                             <asp:Label ID="Label37" CssClass="FieldName" runat="server" Text=""></asp:Label>                             
                            </td>
   </tr>
   
 <tr id="trAddress5" runat="server">
                            <td class="leftField">
                                <asp:Label ID="Label34" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                            </td>
                            <td class="rightField">
                               <asp:Label ID="Label38" CssClass="txtField" runat="server" Text="282002"></asp:Label>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label35" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                            </td>
                            <td class="rightField" >
                              <asp:Label ID="Label39" CssClass="txtField" runat="server" Text="India"></asp:Label>
                            </td>
  </tr>

<%--New Address--%>

 <tr id="trAddress6" runat="server">
<td colspan="4">
 <asp:Label ID="Label18" CssClass="HeaderTextSmall" runat="server" Text="New Address"></asp:Label>
</td>
</tr>

 <tr id="trAddress7" runat="server">

                            <td class="leftField">
                                <asp:Label ID="lblAdrLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>                              
                            </td>
                       
                            <td class="leftField">
                                <asp:Label ID="Label19" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        
</tr>

 <tr id="trAddress8" runat="server">
                            <td class="leftField">
                                <asp:Label ID="Label20" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblResidenceLivingDate" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtLivingSince" runat="server" CssClass="txtField"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtLivingSince_CalendarExtender" runat="server" TargetControlID="txtLivingSince"
                                    Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                                </cc1:CalendarExtender>
                                <cc1:TextBoxWatermarkExtender ID="txtLivingSince_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                                    TargetControlID="txtLivingSince" runat="server">
                                </cc1:TextBoxWatermarkExtender>
                                <asp:CompareValidator ID="txtLivingSince_CompareValidator" runat="server" ErrorMessage="<br/>Please enter a valid date."
                                    Type="Date" ControlToValidate="txtLivingSince" CssClass="cvPCG" Operator="DataTypeCheck"
                                    ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                            </td>
   </tr>
   
 <tr id="trAddress9" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblAdrCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label21" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
   </tr>
   
 <tr id="trAddress10" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblAdrPinCode" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                                    runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                                    Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label22" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                            </td>
                            <td class="rightField" >
                                <asp:DropDownList ID="ddlCorrAdrCountry" runat="server" CssClass="cmbField">
                                    <asp:ListItem>India</asp:ListItem>
                                </asp:DropDownList>
                            </td>
  </tr>
   

<tr id="trBtnSubmit" runat="server">
<td colspan="2" align="left">
<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_OnClick" />
</td>

<td colspan="2"></td>
</tr>
</table>
