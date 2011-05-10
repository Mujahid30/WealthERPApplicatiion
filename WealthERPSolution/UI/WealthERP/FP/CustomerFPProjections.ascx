<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPProjections.ascx.cs" Inherits="WealthERP.FP.CustomerFPProjections" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

   <script src="Scripts/jquery-1.2.6.js" type="text/javascript"></script>
    <script src="Scripts/webtoolkit.jscrollable.js" type="text/javascript"></script>
    <script src="Scripts/webtoolkit.scrollabletable.js" type="text/javascript"></script> 


  <script type="text/javascript">
        $(document).ready(function() {
            jQuery('table').Scrollable(400, 800);
        });
   </script>

<script language="javascript" type="text/javascript">

    function JSValidateToAssetClass(source, args) {
        alert("hi");

        var equity = document.getElementById('<%=txtEquity.ClientID %>');
        var debt = document.getElementById('<%=txtDebt.ClientID %>');
        var cash = document.getElementById('<%=txtCash.ClientID %>');
        var alternate = document.getElementById('<%=txtDebt.ClientID %>');
        var sum = equity.value + debt.value + cash.value+ alternate.value;

        if (sum > 100) // you can also write args.Value
        {
            args.IsValid = false;
            document.getElementById('<%=trValidation.ClientID %>').style.display = 'block';
        }

        else 
        {
            args.IsValid = true;
            document.getElementById('<%=trValidation.ClientID %>').style.display = 'none';

        }

    }

</script>



<style type="text/css">
                  table {                      
                      font: normal 11px "Trebuchet MS", Verdana, Arial;                                              
                      background:#fff;                                 
                      border:solid 1px #C2EAD6;
                  }                
                 
                  td{                  
                  padding: 3px 3px 3px 6px;
                  color: #5D829B;
                  }
                  th {
                        font-weight:bold;
                        font-size:smaller;
                  color: #5D728A;                                             
                  padding: 0px 3px 3px 6px;
                  background: #CAE8EA                      
                  }
            </style>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="FP Projection"></asp:Label>

<telerik:RadTabStrip ID="RadTabStripFPProjection" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerFPProjection" SelectedIndex="0" EnableViewState="true">
    <Tabs>
        <telerik:RadTab runat="server" Text="AssetAllocation" Value="AssetAllocation" Selected="true" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="FutureSavings" Value="FutureSavings" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Rebalancing" Value="Rebalancing" TabIndex="2">
        </telerik:RadTab>        
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
    EnableEmbeddedSkins="false">
</telerik:RadAjaxLoadingPanel>

<telerik:RadMultiPage ID="CustomerFPProjection" EnableViewState="true" runat="server" SelectedIndex="0">
 <telerik:RadPageView ID="RadPageView2" runat="server">
        <asp:Panel ID="pnlInvestment" runat="server">
        <table width="50%">
        <tr>
        <td colspan="4">
        <asp:RadioButton ID="rdbYearWise" runat="server" GroupName="year" Text="Edit value for a year" Class="FieldName" />
        <asp:RadioButton ID="rdbYearRangeWise" runat="server" GroupName="year" Text="Edit value for a range of years" Class="FieldName"/>
        </td>        
        </tr>
        
        <tr>
        <td align="right">
        <asp:Label ID="lblTerm" runat="server" Text="Pick a year : " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" colspan="3">
        <asp:DropDownList ID="ddlPickYear" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td>
        </tr>  
        
        <tr>
        <td align="right">
        <asp:Label ID="Label1" runat="server" Text="From year : " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
        <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td>
        
        <td align="right">
        <asp:Label ID="Label2" runat="server" Text="To year : " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" >
        <asp:DropDownList ID="ddlToYear" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td>
        </tr>
        
        <tr>
        <td colspan="4">
        <asp:Label ID="Label3" runat="server" Text="Agreed allocation" CssClass="FieldName"  Font-Bold="true"></asp:Label>
        </td>
        </tr> 
        
        <tr>
          <td align="right">
           <asp:Label ID="lblEquity" runat="server" Text="Equity : " CssClass="FieldName"></asp:Label>
          </td>
          <td align="left">
           <asp:TextBox ID="txtEquity" runat="server" Style="direction: rtl" CssClass="txtField"></asp:TextBox>
           <asp:RangeValidator ID="txtEquityRV" Type="Integer" ControlToValidate="txtEquity" MinimumValue="0" MaximumValue="100" runat="server" ErrorMessage="Should not be greater than 100" Display="Dynamic">
           </asp:RangeValidator>
           
           <asp:CustomValidator ID="txtEquityCV" runat="Server" ClientValidationFunction="JSValidateToAssetClass" ValidationGroup="btnSubmit"
            ControlToValidate="txtEquity" ValidateEmptyText="True" ErrorMessage="" Display="Dynamic">
            </asp:CustomValidator>
          </td>
          <td align="right">
          <asp:Label ID="lblDebt" runat="server" Text="Debt : " CssClass="FieldName"></asp:Label>
          </td>
           <td align="left">
           <asp:TextBox ID="txtDebt" runat="server" Style="direction: rtl" CssClass="txtField"></asp:TextBox>
           <asp:RangeValidator ID="txtDebtRV" Type="Integer" ControlToValidate="txtDebt" MinimumValue="0" MaximumValue="100" runat="server" ErrorMessage="Should not be greater than 100" Display="Dynamic">
           </asp:RangeValidator>
           
            <asp:CustomValidator ID="txtDebtCV" runat="Server" ClientValidationFunction="JSValidateToAssetClass" ValidationGroup="btnSubmit"
            ControlToValidate="txtDebt" ValidateEmptyText="True" ErrorMessage="" Display="Dynamic">
            </asp:CustomValidator>
           </td>
        </tr>
        
        <tr>
          <td align="right">
           <asp:Label ID="lblCash" runat="server" Text="Cash : " CssClass="FieldName"></asp:Label>
          </td>
          <td align="left">
           <asp:TextBox ID="txtCash" runat="server" Style="direction: rtl" CssClass="txtField"></asp:TextBox>
           <asp:RangeValidator ID="txtCashRV" Type="Integer" ControlToValidate="txtCash" MinimumValue="0" MaximumValue="100" runat="server" ErrorMessage="Should not be greater than 100" Display="Dynamic">
           </asp:RangeValidator>
           <asp:CustomValidator ID="txtCashCV" runat="Server" ClientValidationFunction="JSValidateToAssetClass" ValidationGroup="btnSubmit"
            ControlToValidate="txtCash" ValidateEmptyText="True" ErrorMessage="" Display="Dynamic">
            </asp:CustomValidator>
          </td>
          <td align="right">
          <asp:Label ID="lblAlternate" runat="server" Text="Alternate : " CssClass="FieldName"></asp:Label>
          </td>
           <td align="left">
           <asp:TextBox ID="txtAlternate" runat="server" Style="direction: rtl" CssClass="txtField"></asp:TextBox>
           <asp:RangeValidator ID="txtAlternateRV" Type="Integer" ControlToValidate="txtAlternate" MinimumValue="0" MaximumValue="100" runat="server" ErrorMessage="Should not be greater than 100" Display="Dynamic">
           </asp:RangeValidator>           
           <asp:CustomValidator ID="txtAlternateCV" runat="Server" ClientValidationFunction="JSValidateToAssetClass" ValidationGroup="btnSubmit"
            ControlToValidate="txtAlternate" ValidateEmptyText="True" ErrorMessage="" Display="Dynamic">
            </asp:CustomValidator>
           </td>
        </tr>
         <tr id="trValidation" runat="server">
         <td align="right">
           
         </td>
        <td colspan="3">
         Sum of all asset class should not be greater than 100
        </td>
        </tr>
         <tr>
          <td align="right">
           
          </td>
          <td align="left" colspan="3">
           <asp:Button ID="btnSubmitAggredAllocation" runat="server" Text="Submit" CssClass="PCGButton" ValidationGroup="btnSubmit"/>
          </td>          
        </tr>
               
        
        </table>
        <table width="100%">
        <tr>
        <td>
        <asp:Panel ID="Panel3" runat="server" Height="200px" Width="800px" ScrollBars="Vertical">

        <asp:GridView ID="gvAssetAllocation" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                            HorizontalAlign="Center" ShowFooter="True" 
                            EnableViewState="true" OnPreRender="gvAssetAllocation_PreRender">
                            <FooterStyle CssClass="FooterStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Year") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Equity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRec_Equity" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Rec_Equity") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Debt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRec_Debt" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Rec_Debt") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRec_Cash" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Rec_Cash") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Alternate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRec_Alternate" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Rec_Alternate") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Equity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgr_Equity" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Agr_Equity") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Debt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgr_Debt" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Agr_Debt") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgr_Cash" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Agr_Cash") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Alternate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgr_Alternate" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("Agr_Alternate") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
         </asp:GridView>
         
         </asp:Panel>
        </td>
        </tr>
        
        </table>
        </asp:Panel>
 </telerik:RadPageView>
 
 <telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:Panel ID="Panel1" runat="server">
         <table width="50%">
        <tr>
        <td colspan="4">
        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="year" Text="Edit value for a year" Class="FieldName" />
        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="year" Text="Edit value for a range of years" Class="FieldName"/>
        </td>        
        </tr>
        
        <tr>
        <td align="right">
        <asp:Label ID="Label4" runat="server" Text="Pick a year : " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" colspan="3">
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td>
        </tr>  
        
        <tr>
        <td align="right">
        <asp:Label ID="Label5" runat="server" Text="From year : " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td>
        
        <td align="right">
        <asp:Label ID="Label6" runat="server" Text="To year : " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" >
        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td>
        </tr>
        
        <tr>
        <td colspan="4">
        <asp:Label ID="Label7" runat="server" Text="Agreed allocation" CssClass="FieldName"  Font-Bold="true"></asp:Label>
        </td>
        </tr> 
        
        <tr>
          <td align="right">
           <asp:Label ID="Label8" runat="server" Text="Equity : " CssClass="FieldName"></asp:Label>
          </td>
          <td align="left">
           <asp:TextBox ID="TextBox1" runat="server" Style="direction: rtl" CssClass="txtField"></asp:TextBox>
          </td>
          <td align="right">
          <asp:Label ID="Label9" runat="server" Text="Debt : " CssClass="FieldName"></asp:Label>
          </td>
           <td align="left">
           <asp:TextBox ID="TextBox2" runat="server" Style="direction: rtl" CssClass="txtField"></asp:TextBox>
           </td>
        </tr>
        
        <tr>
          <td align="right">
           <asp:Label ID="Label10" runat="server" Text="Cash : " CssClass="FieldName"></asp:Label>
          </td>
          <td align="left">
           <asp:TextBox ID="TextBox3" runat="server" Style="direction: rtl" CssClass="txtField"></asp:TextBox>
          </td>
          <td align="right">
          <asp:Label ID="Label11" runat="server" Text="Alternate : " CssClass="FieldName"></asp:Label>
          </td>
           <td align="left">
           <asp:TextBox ID="TextBox4" runat="server" Style="direction: rtl" CssClass="txtField"></asp:TextBox>
           </td>
        </tr>
        
         <tr>
          <td align="right">
           
          </td>
          <td align="left" colspan="3">
           <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="PCGButton" />
          </td>          
        </tr>        
        
        </table>
        </asp:Panel>
 </telerik:RadPageView>
 
 <telerik:RadPageView ID="RadPageView3" runat="server">
        <asp:Panel ID="Panel2" runat="server">
        <table width="100%">
        
        </table>
        </asp:Panel>
 </telerik:RadPageView>
</telerik:RadMultiPage>

<script language="javascript" type="text/javascript">
    document.getElementById('<%=trValidation.ClientID %>').style.display = 'none';
 </script>