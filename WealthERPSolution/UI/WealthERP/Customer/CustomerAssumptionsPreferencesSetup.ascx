<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssumptionsPreferencesSetup.ascx.cs" Inherits="WealthERP.Customer.CustomerAssumptionsPreferencesSetup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>


<script type="text/javascript">
    function tSpeedValue() 
    {

        var currentTime = new Date();

        var age = document.getElementById('<%=hfAge.ClientID%>').value;
        var year = currentTime.getFullYear();

        var remaining = year - age;
        var assumptionValue = document.getElementById('<%=txtAssumptionValue.ClientID%>').value;
        var lifeExpectancy = document.getElementById('<%=txtLifeExpectancy.ClientID%>').value;
        var RetirementAge = document.getElementById('<%=txtRetirementAge.ClientID%>').value;

        var assumptionType = document.getElementById('<%=ddlPickAssumtion.ClientID%>').value;
       
       if (assumptionType == 'LE') {
           if (assumptionValue < RetirementAge) {
               alert("Life Expectancy Should Be More Than Retirement Age");
            
           }

           if (assumptionValue < remaining) {

               alert("Life Expectancy Should Be More Than Your Age");
          
           }

       }
       if (assumptionType == 'RA') {
           if (assumptionValue > lifeExpectancy) {
               alert(" Retirement Age Should Be Less Than Life Expectancy ");
              
           }
           if (assumptionValue < remaining) {
               alert("Retirement Age Should Be More Than Your Age");
              
           }

       }
   }
  </script>
 
 <script type="text/javascript">

     function ShowHideGaolType() {

         
         document.getElementById("<%= trRangeYear.ClientID %>").style.display = 'none';
         if (document.getElementById("<%= rdbYearWise.ClientID %>").checked == true) {

             document.getElementById("<%= trPickYear.ClientID %>").style.display = 'block';

             document.getElementById("<%= trRangeYear.ClientID %>").visible = false;
         }
         else if (document.getElementById("<%= rdbYearRangeWise.ClientID %>").checked == true) {
             document.getElementById("<%= trPickYear.ClientID %>").style.display = 'none';
             document.getElementById("<%= trRangeYear.ClientID %>").style.display = 'block';
         }
         var assumptiontype = document.getElementById("<%=ddlPickAssumtion.ClientID %>").value;
         if (assumptiontype == 'LE' || assumptiontype == 'RA') {

             document.getElementById("<%= trRbtnYear.ClientID %>").style.display = 'none';
         }

     }
  

</script>
  

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Preferences"></asp:Label>
<hr />
<table width="100%" cellspacing="0" cellpadding="0">
<tr>
<td>
<telerik:RadToolBar ID="aplToolBar" runat="server" Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%"  OnButtonClick="aplToolBar_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>
        
    </Items>
</telerik:RadToolBar>
</td>
</tr>
</table>
<hr />

<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
</table>

<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerAssumptionsPreferencesSetupId" 
    SelectedIndex="0">
    <Tabs>
        
        <telerik:RadTab runat="server" Text="Preferences"
            Value="Preferences" TabIndex="0"  Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Assumption"
            Value="Assumtion" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Calculation Basis"
            Value="Calculation Basis" TabIndex="2">
        </telerik:RadTab>
        
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="CustomerAssumptionsPreferencesSetupId" EnableViewState="true" runat="server" SelectedIndex="0">
 <telerik:RadPageView ID="RadPageView3" runat="server">
        <asp:Panel ID="pnlPreferences" runat="server">
            <table width="50%">
            <tr>
            <td><br /></td>
            </tr>
                <tr>
                <td></td>
   <td>
<asp:RadioButton ID="rbtnSelfOnly" runat="server" CssClass="cmbField" Text="Plan for self only" GroupName="gpPlan" Checked="true"/>
        
 </td>

                    </tr>
                    <tr>
                    <td></td><td>
                        <asp:RadioButton ID="rbtnSpouse" runat="server" CssClass="cmbField" 
                            GroupName="gpPlan" Text="Plan with Spouse" />
                        </td>
                    </tr>
                    
                    <tr>
                    <td style="width:95px"></td>
                    <td>
                        <asp:Button ID="btnPlanPreference" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnPlanPreference_OnClick" />
                    </td>
                    
                    </tr>
                    </table>
                    </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:Panel ID="pnlAssumption" runat="server">
            <table>
                <tr>
            <td></td>
            <td></td>
            </tr>
                <tr>
            
                <td>
                    <asp:Label ID="lblPickAssumtion" runat="server" CssClass="FieldName" 
                        Text="Pick a assumption"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPickAssumtion" runat="server" AutoPostBack="true" 
                        CssClass="cmbField"
                        OnSelectedIndexChanged="ddlPickAssumtion_OnSelectedIndexChanged">
                    </asp:DropDownList><br />
<%--                    <asp:RequiredFieldValidator ID="rfvPickAssumption" runat="server" ValidationGroup="vgroup1" CssClass="rfvPCG" Text="Pick Assumption" ControlToValidate="ddlPickAssumtion"></asp:RequiredFieldValidator>
--%>                <asp:CompareValidator ID="cvPickAssumption" runat="server" ControlToValidate="ddlPickAssumtion"
                ErrorMessage="Please Select Assumption" Operator="NotEqual" ValueToCompare="-Select-"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                </td>
                
                </tr>
             </table>
        <table>
            <tr id="trRbtnYear" runat="server">
        
        <td >
        <asp:RadioButton ID="rdbYearWise" runat="server" Checked="true" GroupName="year" Text="Edit value for a year" Class="FieldName" onClick="return ShowHideGaolType()"/>
        </td><td><asp:RadioButton ID="rdbYearRangeWise" runat="server" GroupName="year" Text="Edit value for a range of years" Class="FieldName" onClick="return ShowHideGaolType()"/>
        </td>        
        </tr>
        </table>
        <table>
        
        <tr id="trPickYear" runat="server">
        
        <td align="right">
        <asp:Label ID="lblTerm" runat="server" Text="Pick a year : " CssClass="FieldName"></asp:Label>
        </td>
        <td>
        <asp:DropDownList ID="ddlPickYear" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td>
        </tr>  
        
        <tr id="trRangeYear" runat="server">
        
        <td align="right">
        <asp:Label ID="Label1" runat="server" Text="From year : " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
        <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td>
        <td></td>
        <td align="right">
        <asp:Label ID="Label2" runat="server" Text="To year : " CssClass="FieldName"></asp:Label>
        </td><td></td>
        <td align="left" >
        <asp:DropDownList ID="ddlToYear" runat="server" CssClass="cmbField">
        </asp:DropDownList>
        </td><td><asp:CompareValidator ID="cvlblRangeTo" runat="server" ValidationGroup="vgbtnSubmit" ControlToCompare="ddlFromYear" ControlToValidate="ddlToYear" Operator="GreaterThanEqual" ErrorMessage="To Year can not be leas then From Year" CssClass="cvPCG"></asp:CompareValidator></td>
        </tr>
        </table>
        <table>
        <tr>        
              <td align="right" valign="middle">
                    <asp:Label ID="lblAssumptionValue" runat="server" CssClass="FieldName" 
                        Text="Enter Assumption value:"></asp:Label>
                </td>
              
                <td align="left">
                    <asp:TextBox ID="txtAssumptionValue" runat="server" CssClass="cmbField" onchange="tSpeedValue()"></asp:TextBox>
                    <br />
 <asp:RequiredFieldValidator ID="rfvAssumptionValue" runat="server" Display="Dynamic" CssClass="cvPCG" ControlToValidate="txtAssumptionValue" ErrorMessage="Please Enter Assumption Value" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>                   
                </td>  <td> <%--<asp:CompareValidator ID="cvAssumptionValue" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Integer" ControlToValidate="txtAssumptionValue" Operator="DataTypeCheck"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>--%>
         <asp:RangeValidator ID="rvAssumptionValue" runat="server" ControlToValidate="txtAssumptionValue"
                         Display="Dynamic" ErrorMessage="Please enter value less than 100" MaximumValue="99.9" MinimumValue="0.0" CssClass="cvPCG"
                                          Type="Double" ValidationGroup="vgbtnSubmit"></asp:RangeValidator>
                                          
                </td></tr>
              </table>
        <table>
<tr>
    
    <td>
        <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" ValidationGroup="vgbtnSubmit"  
            OnClick="btnSubmit_OnClick" Text="Submit" />
    </td>
</tr>
<tr>

<td></td>
</tr>

<tr>

<td>
 <asp:Label ID="lblStaticAssumption" runat="server" Text="Static Assumptions:" 
        CssClass="FieldName"></asp:Label>

</td>
</tr>
<tr ID="trLifeExpectancy" runat="server">

<td align="right">
    <asp:Label ID="lblLifeExpectancy" runat="server" CssClass="FieldName" 
        Text="Life Expectancy:"></asp:Label>
</td>
<td align="left">
    <asp:TextBox ID="txtLifeExpectancy" runat="server" Enabled="false" CssClass="cmbField"></asp:TextBox>
</td>

</tr>
<tr ID="trRetirementAge" runat="server">

                    <td align="right">
                        <asp:Label ID="lblRetirementAge" runat="server" CssClass="FieldName" 
                            Text="Retirement Age:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRetirementAge" runat="server" Enabled="false" CssClass="cmbField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                
                <td></td>
                </tr>
<tr>

<td>
    <asp:Label ID="lblProjectedAssumptions" runat="server" CssClass="FieldName" Text="Projected Assumptions:" ></asp:Label>
</td></tr></table>
 
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="98%" EnableHistory="True"
    HorizontalAlign="NotSet" LoadingPanelID="PorspectListLoading">
<telerik:RadGrid ID="gvProjectedAssumption" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="12" AllowSorting="True" AllowPaging="True" HeaderStyle-Wrap="true" HeaderStyle-VerticalAlign="Top" 
        ShowStatusBar="True" ShowFooter="true" Width="100%"
        Skin="Telerik" EnableEmbeddedSkins="false"
        
        AllowAutomaticInserts="false">
        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
           
            <Columns>
             <telerik:GridBoundColumn DataField="Year" HeaderText="Year" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" SortExpression="Year"
                    UniqueName="Year">
                     <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
               
                  <telerik:GridTemplateColumn UniqueName="Inflation" AllowFiltering="true" HeaderStyle-Width="80px" DataField="Inflation" HeaderText="Inflation(%)" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblInflation" runat="server" CssClass="CmbField" Text='<%# Eval("Inflation").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn UniqueName="Equity" AllowFiltering="true" HeaderStyle-Width="110px" DataField="Equity" HeaderText="Return On Equity" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblEquity" runat="server" CssClass="CmbField" Text='<%# Eval("Equity").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                
                <telerik:GridTemplateColumn UniqueName="Debt" AllowFiltering="true" HeaderStyle-Width="100px" DataField="Debt" HeaderText="Return On Debt" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblDebt" runat="server" CssClass="CmbField" Text='<%# Eval("Debt").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                
                <telerik:GridTemplateColumn UniqueName="Cash" AllowFiltering="true" HeaderStyle-Width="100px" DataField="Cash" HeaderText="Return On Cash" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCash" runat="server" CssClass="CmbField" Text='<%# Eval("Cash").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                
                <telerik:GridTemplateColumn UniqueName="Alternate" AllowFiltering="true" HeaderStyle-Width="125px" DataField="Alternate" HeaderText="Return On Alternate" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblAlternate" runat="server" CssClass="CmbField" Text='<%# Eval("Alternate").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                  <telerik:GridTemplateColumn UniqueName="IncomeGrowth" AllowFiltering="true" HeaderStyle-Width="125px" DataField="IncomeGrowth" HeaderText="Income Growth Rate" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblIncomeGrowth" runat="server" CssClass="CmbField" Text='<%# Eval("IncomeGrowth").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                  <telerik:GridTemplateColumn UniqueName="ExpenseGrowth" AllowFiltering="true" DataField="ExpenseGrowth" HeaderText="Expense Growth Rate" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblExpenseGrowth" runat="server" CssClass="CmbField" Text='<%# Eval("ExpenseGrowth").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                             <telerik:GridTemplateColumn UniqueName="DiscountRate" AllowFiltering="true" DataField="DiscountRate" HeaderStyle-Width="90px" HeaderText="Discount Rate" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountRate" runat="server" CssClass="CmbField" Text='<%# Eval("DiscountRate").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                        <telerik:GridTemplateColumn UniqueName="PostRetirement" AllowFiltering="true" DataField="PostRetirement" HeaderText="Post Retirement Returns" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblPostRetirement" runat="server" CssClass="CmbField" Text='<%# Eval("PostRetirement").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                             <telerik:GridTemplateColumn UniqueName="ReturnNewInvestments" AllowFiltering="true" DataField="ReturnNewInvestments" HeaderText="Return On New Investments" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblReturnNewInvestments" runat="server" CssClass="CmbField" Text='<%# Eval("ReturnNewInvestments").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
            </Columns>
                
        </MasterTableView>
        <HeaderStyle Width="140px" VerticalAlign="Top" Wrap="false" />
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" EnableVirtualScrollPaging="false" SaveScrollPosition="true" FrozenColumnsCount="1">
            </Scrolling>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
    </telerik:RadAjaxPanel>
            <asp:HiddenField ID="hfAge" runat="server" />
       </asp:Panel>
        </telerik:RadPageView>
                    
   <telerik:RadPageView ID="RadPageView2" runat="server">
   <asp:Panel ID="pnlCalculation" runat="server">
   <table width="50%">
   <tr>
       <td style="width:35px">
       </td>
       <td>
       <br />
           <asp:Label ID="lblRetirementGoalAnalysis0" runat="server" CssClass="FieldName" 
               Text="Retirement Goal Analysis:"></asp:Label>
       </td>
   </tr>
       </table>
       <table width="50%">
   <tr>
       <td style="width:95px"></td>
       
       <td align="left">
           <asp:RadioButton ID="rbtnNoCorpus" runat="server" Checked="true" 
               CssClass="cmbField" GroupName="gpRetirement" 
               Text="No Corpus to be left behind" />
       </td><td></td>
       </tr>
   <tr>
   <td style="width:95px"></td>
   <td align="left">
       <asp:RadioButton ID="rbtnCorpus" runat="server" CssClass="cmbField" 
           GroupName="gpRetirement" Text="Corpus to be left behind" />
       </td><td>
           &nbsp;</td>
   </tr>
   <tr>
   <td style="width:35px"></td>
   <td></td><td></td>
   </tr>
   <tr>
   <td style="width:35px"></td>
   <td>
       <asp:Button ID="btnCalculationBasis" runat="server" CssClass="PCGButton" 
           OnClick="btnCalculationBasis_OnClick " Text="Submit" />
   </td>
   <td></td>
   </tr>
           </table>
               </asp:Panel>
                    </telerik:RadPageView>
                    
                    </telerik:RadMultiPage>  
                    <script language="javascript" type="text/javascript">
                        document.getElementById('<%=trRangeYear.ClientID %>').style.display = 'none';
                        
 </script>                 