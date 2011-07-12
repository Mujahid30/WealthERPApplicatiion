<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssumptionsPreferencesSetup.ascx.cs" Inherits="WealthERP.Customer.CustomerAssumptionsPreferencesSetup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>


<style type="text/css">

p { padding: 5px 0; }


</style>


<script type="text/javascript">

    function HideStatusMsg() {
        document.getElementById("<%=msgRecordStatus.ClientID%>").style.display = 'none';
    }

</script>

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
         var pickperiod = document.getElementById("<%=ddlPickPeriod.ClientID%>").value;
         if (pickperiod == "SY") {
             document.getElementById("<%= trPickYear.ClientID %>").style.display = 'block';

             document.getElementById("<%= trRangeYear.ClientID %>").visible = false; 
         }
         else if (pickperiod == "RY") {
             document.getElementById("<%= trPickYear.ClientID %>").style.display = 'none';
             document.getElementById("<%= trRangeYear.ClientID %>").style.display = 'block';
         }
         var assumptiontype = document.getElementById("<%=ddlPickAssumtion.ClientID %>").value;
         if (assumptiontype == 'LE' || assumptiontype == 'RA') {

             document.getElementById("<%= trRbtnYear.ClientID %>").style.display = 'none';
         }

     }
  

</script>

<script type="text/javascript">
    $(document).ready(function() {
    //hide the all of the element with class Collapse_body
    $(".Collapse_body").hide();
    //toggle the componenet with class Collapse_body
        $(".Collapse_header").click(function() {
        $(this).next(".Collapse_body").slideToggle(600);
        });
    });
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

<table width="100%" cellpadding="0" style="margin-top:0px">
    <tr valign="top">
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
</table>

<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerAssumptionsPreferencesSetupId" 
    SelectedIndex="1">
    <Tabs>
        
        <telerik:RadTab runat="server" Text="Preferences" onclick="HideStatusMsg()"
            Value="Preferences" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Assumptions" onclick="HideStatusMsg()"
            Value="Assumtion" TabIndex="1" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Calculation Basis" onclick="HideStatusMsg()"
            Value="Calculation Basis" TabIndex="2">
        </telerik:RadTab>
        
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="CustomerAssumptionsPreferencesSetupId" 
    EnableViewState="true" runat="server" SelectedIndex="1">
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
                    

                    
<table width="100%">
<tr>
<td>
    <telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:Panel ID="pnlAssumption" runat="server">
        
        <table width="768px">
            <tr>
                <td style="width:10%" align="center">
                    <asp:LinkButton ID="lnkBtnYearly" runat="server" CssClass="FieldName" onclick="lnkBtnYearly_Click" Text="Yearly"></asp:LinkButton>                             
                </td> 
                <td style="width:10%" align="center">
                    <asp:LinkButton ID="lnkBtnStatic" runat="server" CssClass="FieldName" onclick="lnkBtnStatic_Click" Text="Static"></asp:LinkButton>
                    
                </td>
                <td style="width:80%"></td>
            </tr>
        </table>
        
        <div class="Collapse_list" style="width:100%">
        <p class="Collapse_header">
        <asp:Label ID="lblHeader1" runat="server" CssClass="Collapse_header" Text="Edit" ></asp:Label>
        </p>
        <div class="Collapse_body" style="width:100%">
        
        <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                <table width="100%" cellpadding="0">           
                    <tr>            
                        <td align="right" style="width:20%">
                            <asp:Label ID="lblPickAssumtion" runat="server" CssClass="FieldName" 
                            Text="Pick a assumption:"></asp:Label>
                        </td>
                        <td style="width:15%">
                            <asp:DropDownList ID="ddlPickAssumtion" runat="server" AutoPostBack="true"
                            CssClass="cmbField" OnSelectedIndexChanged="ddlPickAssumtion_OnSelectedIndexChanged">
                            </asp:DropDownList><br />
                            <%--<asp:RequiredFieldValidator ID="rfvPickAssumption" runat="server" ValidationGroup="vgroup1" CssClass="rfvPCG" Text="Pick Assumption" ControlToValidate="ddlPickAssumtion"></asp:RequiredFieldValidator>--%>
                            <asp:CompareValidator ID="cvPickAssumption" runat="server" ControlToValidate="ddlPickAssumtion"
                            ErrorMessage="Please Select Assumption" Operator="NotEqual" ValueToCompare="-Select-"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                        </td> 
                        <td style="width:10%"></td>
                        <td style="width:55%"></td>
                           
                    </tr>
                    <tr id="trRbtnYear" runat="server">
                        <td align="right" style="width:20%">
                            <asp:Label ID="lblPickPeriod" runat="server" CssClass="FieldName" 
                            Text="Pick a Period:"></asp:Label>
                        </td>
                        <td style="width:15%">
                            <asp:DropDownList ID="ddlPickPeriod" runat="server" AutoPostBack="true"
                            CssClass="cmbField" 
                                onselectedindexchanged="ddlPickPeriod_SelectedIndexChanged" >
                            <asp:ListItem Value="0">Select period</asp:ListItem>
                            <asp:ListItem Value="SY">Single year</asp:ListItem>
                            <asp:ListItem Value="RY">Range of years</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:10%"></td>
                        <td style="width:55%"></td>
                                        
                    </tr>  
                    <tr id="trPickYear" runat="server">        
                        <td align="right" style="width:20%">
                            <asp:Label ID="lblTerm" runat="server" Text="Pick a year : " CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:15%">
                            <asp:DropDownList ID="ddlPickYear" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                        </td>
                        <td style="width:10%"></td>
                        <td style="width:55%"></td>
                        
                    </tr>
                    <tr id="trRangeYear" runat="server">        
                        <td align="right" style="width:20%">
                            <asp:Label ID="Label1" runat="server" Text="From year : " CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left" style="width:15%">
                            <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                        </td>        
                        <td align="right" style="width:10%">
                            <asp:Label ID="Label2" runat="server" Text="To year : " CssClass="FieldName"></asp:Label>
                        </td>        
                        <td align="left" style="width:55%">
                            <asp:DropDownList ID="ddlToYear" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="cvlblRangeTo" runat="server" 
                                ControlToCompare="ddlFromYear" ControlToValidate="ddlToYear" CssClass="cvPCG" 
                                ErrorMessage="To Year can not be leas then From Year" 
                                Operator="GreaterThanEqual" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>                           
                      <td align="right" valign="middle" style="width:20%">
                        <asp:Label ID="lblAssumptionValue" runat="server" CssClass="FieldName" 
                        Text="Enter Assumption value:"></asp:Label>
                      </td>
                      <td align="left" style="width:15%">
                        <asp:TextBox ID="txtAssumptionValue" runat="server" CssClass="txtField" onchange="tSpeedValue()"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvAssumptionValue" runat="server" 
                              ControlToValidate="txtAssumptionValue" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please Enter Assumption Value" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>
                        
                      </td>
                      <td style="width:10%"> <%--<asp:CompareValidator ID="cvAssumptionValue" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Integer" ControlToValidate="txtAssumptionValue" Operator="DataTypeCheck"
                        CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>--%>
                        
                          <asp:RangeValidator ID="rvAssumptionValue" runat="server" 
                              ControlToValidate="txtAssumptionValue" CssClass="cvPCG" Display="Dynamic" 
                              ErrorMessage="Please enter value less than 100" MaximumValue="99.9" 
                              MinimumValue="0.0" Type="Double" ValidationGroup="vgbtnSubmit"></asp:RangeValidator>
                        
                          </td>
                      <td style="width:55%">
                          &nbsp;</td>
                      </tr>
                     
                    <tr>
                    <td align="right" style="width:20%">
                        </td>
                        <td align="left" style="width:15%">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" 
                            OnClick="btnSubmit_OnClick" Text="Submit" ValidationGroup="vgbtnSubmit" />
                        </td>
                        <td style="width:10%"></td>
                        <td style="width:55%"></td>
                    </tr>



                                     
                </table>
           
                
                 </ContentTemplate>
         </asp:UpdatePanel>        
<%--        <table  width="768px">
            <tr id="trRbtnYear" runat="server">
        
        <td >
        <asp:RadioButton ID="rdbYearWise" runat="server" Checked="true" GroupName="year" Text="Edit value for a year" Class="FieldName" onClick="return ShowHideGaolType()"/>
        </td><td><asp:RadioButton ID="rdbYearRangeWise" runat="server" GroupName="year" Text="Edit value for a range of years" Class="FieldName" onClick="return ShowHideGaolType()"/>
        </td>        
        </tr>
        </table>--%>


    </div>

        </div>
        

 
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="98%" EnableHistory="True"
    HorizontalAlign="NotSet" LoadingPanelID="PorspectListLoading">
    
            <p class="Collapse_header" style="width:100%">
        <asp:Label ID="lblProjectedAssumptions" runat="server" CssClass="Collapse_header" Text="View" ></asp:Label>
       </p>
            <div class="Collapse_body" style="width:100%">
            
            <table width="100%">
            <tr id="trStaticGrid" runat="server">
            <td>
                <table  width="100%">     
                <%--<tr>
                    <td>
                        <asp:Label ID="lblStaticAssumption" runat="server" Text="Static Assumptions:" 
                        CssClass="FieldName"></asp:Label>
                    </td>
                </tr>--%>
                <tr ID="trLifeExpectancy" runat="server">
                    <td align="right" style="width:20%">
                        <asp:Label ID="lblLifeExpectancy" runat="server" CssClass="FieldName" 
                            Text="Life Expectancy:"></asp:Label>
                    </td>
                    <td align="left" style="width:15%">
                        <asp:TextBox ID="txtLifeExpectancy" runat="server" Enabled="false" CssClass="cmbField"></asp:TextBox>
                    </td>
                    <td style="width:10%"></td>
                    <td style="width:55%"></td>
                </tr>
                <tr ID="trRetirementAge" runat="server">
                    <td align="right">
                        <asp:Label ID="lblRetirementAge" runat="server" CssClass="FieldName" 
                            Text="Retirement Age:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRetirementAge" runat="server" Enabled="false" CssClass="cmbField"></asp:TextBox>
                    </td>
                    <td style="width:10%"></td>
                    <td style="width:15%"></td>
                    <td style="width:15%"></td>
                </tr>
            </table>
            </td>
            </tr>
            <tr id="trGridAssumption" runat="server">
            <td>
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
               
                  <telerik:GridTemplateColumn UniqueName="Inflation" HeaderStyle-HorizontalAlign="Center" AllowFiltering="true" HeaderStyle-Width="50px" DataField="Inflation" HeaderText="Inflation(%)" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblInflation" runat="server" CssClass="CmbField" Text='<%# Eval("Inflation").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn UniqueName="Equity" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" DataField="Equity" HeaderText="Return<br/>On Equity" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblEquity" runat="server" CssClass="CmbField" Text='<%# Eval("Equity").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                
                <telerik:GridTemplateColumn UniqueName="Debt" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" DataField="Debt" HeaderText="Return<br/>On Debt" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblDebt" runat="server" CssClass="CmbField" Text='<%# Eval("Debt").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                
                <telerik:GridTemplateColumn UniqueName="Cash" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" DataField="Cash" HeaderText="Return<br/>On Cash" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCash" runat="server" CssClass="CmbField" Text='<%# Eval("Cash").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                
                <telerik:GridTemplateColumn UniqueName="Alternate" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" DataField="Alternate" HeaderText="Return<br/>On Alternate" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblAlternate" runat="server" CssClass="CmbField" Text='<%# Eval("Alternate").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                  <telerik:GridTemplateColumn UniqueName="IncomeGrowth" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" DataField="IncomeGrowth" HeaderText="Income<br/>Growth Rate" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblIncomeGrowth" runat="server" CssClass="CmbField" Text='<%# Eval("IncomeGrowth").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                  <telerik:GridTemplateColumn UniqueName="ExpenseGrowth" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" DataField="ExpenseGrowth" HeaderText="Expense<br/>Growth Rate" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblExpenseGrowth" runat="server" CssClass="CmbField" Text='<%# Eval("ExpenseGrowth").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                             <telerik:GridTemplateColumn UniqueName="DiscountRate" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" DataField="DiscountRate" HeaderStyle-Width="50px" HeaderText="Discount<br/>Rate" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountRate" runat="server" CssClass="CmbField" Text='<%# Eval("DiscountRate").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                        <telerik:GridTemplateColumn UniqueName="PostRetirement" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="70px" DataField="PostRetirement" HeaderText="Post Retirement<br/>Returns" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblPostRetirement" runat="server" CssClass="CmbField" Text='<%# Eval("PostRetirement").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                             <telerik:GridTemplateColumn UniqueName="ReturnNewInvestments" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" DataField="ReturnNewInvestments" HeaderText="Return On<br/>New Investments" >
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
            
            </td>
            </tr>
            </table>
            
            
            

    </telerik:RadAjaxPanel>
            <asp:HiddenField ID="hfAge" runat="server" />
            
            </div>
            
            
       </asp:Panel>
        </telerik:RadPageView>
        </div>
        
        </td>
</tr>
</table>
                    
   <telerik:RadPageView ID="RadPageView2" runat="server">
   <asp:Panel ID="pnlCalculation" runat="server">
   <table width="50%">
   <tr>
       <td style="width:35px">
       </td>
       <td>
       <br />
           <asp:Label ID="lblRetirementGoalAnalysis0" runat="server" CssClass="FieldName" 
               Text="Retirement Goal Settings:"></asp:Label>
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