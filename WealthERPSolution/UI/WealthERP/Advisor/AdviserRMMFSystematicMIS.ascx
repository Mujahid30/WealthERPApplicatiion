<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserRMMFSystematicMIS.ascx.cs" Inherits="WealthERP.Advisor.AdviserRMMFSystematicMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="demo" Namespace="DanLudwig.Controls.Web" Assembly="DanLudwig.Controls.AspAjax.ListBox" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true">
 <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
</script>

<table style="width:100%;">
    <tr>
        <td class="HeaderTextBig" colspan="6">
            <asp:Label ID="lblMFSystematicMIS" runat="server" CssClass="HeaderTextBig" Text="MF Systematic MIS"></asp:Label>
            <hr />
        </td>
    </tr>
    </table>
    <table width="50%">
        <tr id="trBranchRM" runat="server" >
    <td align="left" style="width: 25%">
    <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
  
    <asp:DropDownList ID="ddlBranch" runat="server" style="vertical-align: middle" AutoPostBack="true"
            CssClass="cmbField" onselectedindexchanged="ddlBranch_SelectedIndexChanged" >
    </asp:DropDownList>                  
    </td>
    <td align="left" style="width: 25%">
    <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
    
    <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" style="vertical-align: middle" >
     </asp:DropDownList>           
     </td>
    </tr>
    </table>
    
    <table width="100%">
     <tr>
      <td id="tdSelectCusto" runat="server"  style="width: 40%" >
       <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS for :"></asp:Label>
       <asp:RadioButton runat="server" ID="rdoAllCustomer" Text="All Customers" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" Checked="True" 
              oncheckedchanged="rdoAllCustomer_CheckedChanged" />
         <asp:RadioButton runat="server" ID="rdoPickCustomer" Text="Pick a Customer" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" 
              oncheckedchanged="rdoPickCustomer_CheckedChanged" />
       </td>
       </tr>
       <tr>
       <td>
       <table>
       <tr>
      <td align="right">
      <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Select customer type: "></asp:Label>
      </td>
      <td>
      <asp:DropDownList ID="ddlSelectCutomer" style="vertical-align: middle" 
              runat="server" CssClass="cmbField" AutoPostBack="true" 
              onselectedindexchanged="ddlSelectCutomer_SelectedIndexChanged">
      <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
      <asp:ListItem Value="Group Head" Text="Group Head"></asp:ListItem>
      <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
      </asp:DropDownList>
      </td>
      </tr>
      <tr>
      <td align="right">
      <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Select Customer: "></asp:Label>
      </td>
      <td>
     <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="True">  </asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer" WatermarkText="Enter few chars of Customer"
     runat="server" EnableViewState="false"></cc1:TextBoxWatermarkExtender>
             <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender" runat="server"
              TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
              MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
              CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
              CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
              UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters="" Enabled="True"  />
              
     <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic" ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer" runat="server" ValidationGroup="btnGo">
     </asp:RequiredFieldValidator>
              
              
                                                                
       </td>
       </tr>
     </table> 
     </td>
     </tr>
     
<%--     <tr id="trGroupCustomer" runat="server">
     <td></td>
     <td></td>
     <td></td>
     </tr>--%>
     <tr>
      <td align="right">
     <asp:Label ID="lblSystematicType" runat="server" Text="Systematic Type:" CssClass="FieldName"></asp:Label>
     </td>
     <td>
     <asp:DropDownList ID="ddlSystematicType" runat="server" CssClass="cmbField"  AutoPostBack="false"> 
      </asp:DropDownList>
     </td>
     <td align="right">
     <asp:Label ID="lblAMC" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
     </td>
     <td>
     <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField"  
             AutoPostBack="true" onselectedindexchanged="ddlAMC_SelectedIndexChanged"> 
      </asp:DropDownList>
     </td>
     <td></td>
    </tr>
    
     <tr>
     <td align="right">
     <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
     </td>
     <td>
     <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField"  AutoPostBack="false"> 
      </asp:DropDownList>
     </td>
     <td align="right">
      <asp:Label ID="lblScheme" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
      </td>
      <td>
     <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField"  AutoPostBack="false"> 
      </asp:DropDownList>
     </td>
     <td></td>
      </tr>
      
      <tr>
      <td valign="middle" align="Right">
         <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName">
         </asp:Label>
        
         </td>
         <td align="left">
         <asp:TextBox ID="txtFrom" runat="server" CssClass="txtField"></asp:TextBox> <span id="SpanFromDate" class="spnRequiredField">*</span>
         <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
         </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:TextBoxWatermarkExtender ID="txtFrom_TextBoxWatermarkExtender" runat="server" TargetControlID="txtFrom" WatermarkText="dd/mm/yyyy" Enabled="True">
         </ajaxToolkit:TextBoxWatermarkExtender>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtFrom" CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" runat="server" ValidationGroup="btnGo">
          </asp:RequiredFieldValidator>
          </td>          
         <td valign="middle" align="Right">
             <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
             
             </td>
             <td align="left">
              <asp:TextBox ID="txtTo" runat="server" CssClass="txtField"></asp:TextBox> <span id="SpanToDate" class="spnRequiredField">*</span>
              <ajaxToolkit:CalendarExtender ID="txtTo_CalendarExtender" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
               </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:TextBoxWatermarkExtender ID="txtTo_TextBoxWatermarkExtender" runat="server" TargetControlID="txtTo" WatermarkText="dd/mm/yyyy" Enabled="True">
                </ajaxToolkit:TextBoxWatermarkExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTo" CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" ValidationGroup="btnGo">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date" Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo">
               </asp:CompareValidator>
         </td>
         <td></td>
      </tr>
    </table>
    <table width="100%">
    <tr>
     <td align="left" style="width:35%;">
     <asp:Label ID="lblDate" runat="server" Text="Date filter on: " CssClass="FieldName"></asp:Label>
     <asp:DropDownList ID="ddlDateFilter" style="vertical-align: middle" runat="server" CssClass="cmbField"> 
     <asp:ListItem Text="Start Date" Value="StartDate" Selected="True"></asp:ListItem>
     <asp:ListItem Text="End Date" Value="EndDate"></asp:ListItem>
      </asp:DropDownList>
     </td>
    <%--<td style="width:66%">
     <asp:Label ID="lblFrom" runat="server" Text="From:" CssClass="FieldName"></asp:Label>
     <asp:TextBox ID="txtFrom" runat="server" style="vertical-align: middle" CssClass="txtField">
      </asp:TextBox>
      <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" TargetControlID="txtFrom"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtStartDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFrom" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
      <asp:Label ID="Label1" runat="server" Text="To:" CssClass="FieldName" ></asp:Label>
     <asp:TextBox ID="txtTo" runat="server" style="vertical-align: middle" CssClass="txtField">
      </asp:TextBox>
     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                TargetControlID="txtTo" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
     </td>--%>
                
     </tr>
     <tr>
      
     </tr>
    </table>
    <table>
    <tr>
    <td>
    <asp:Button ID="btnGo" runat="server" Text="Go" ValidationGroup="btnGo"  
            CssClass="PCGButton" onclick="btnGo_Click" />
    </td>
    </tr>
    </table>
<div>
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="SystameticMISMultiPage" 
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server"  Text="Systematic Setup View" 
            Value="Systematic Setup View" TabIndex="0">
        </telerik:RadTab>
        <%--<telerik:RadTab runat="server" Text="Calendar Detail View" 
            Value="Calendar Detail View" TabIndex="1" Selected="True">
        </telerik:RadTab>--%>
  <telerik:RadTab runat="server" Text="Calender Summary View" Value="Calender Summary View" TabIndex="1">
        </telerik:RadTab>
       </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="SystameticMISMultiPage" EnableViewState="true" 
    runat="server" SelectedIndex="0">
<telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:Panel ID="pnlSystameticSetupView" runat="server">        
                   <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="90%" EnableHistory="True"
                        HorizontalAlign="NotSet" LoadingPanelID="PorspectListLoading">
                       <telerik:RadGrid ID="gvSystematicMIS" runat="server" 
                           AllowAutomaticInserts="false" AllowFilteringByColumn="false" AllowPaging="True" 
                           AllowSorting="false" AutoGenerateColumns="False" EnableEmbeddedSkins="false" 
                           GridLines="None" PageSize="15" ShowFooter="true" ShowStatusBar="True" 
                           Skin="Telerik" Width="100%">
                           <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" 
                               Width="100%">
                               <Columns>
                                   <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer Name" 
                                       UniqueName="CustomerName">
                                       <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="SystematicTransactionType" 
                                       HeaderText="Type" UniqueName="SystematicTransactionType">
                                       <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="AMCname" HeaderText="AMC" 
                                       UniqueName="AMCname">
                                       <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="SchemePlaneName" 
                                       HeaderText="Scheme" UniqueName="SchemePlaneName">
                                       <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="FolioNumber" HeaderText="Folio Number" 
                                       UniqueName="FolioNumber">
                                       <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="StartDate" 
                                       DataFormatString="{0:dd-MM-yyyy}" HeaderText="Start Date" 
                                       UniqueName="StartDate">
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="EndDate" DataFormatString="{0:dd-MM-yyyy}" 
                                       HeaderText="End Date" UniqueName="EndDate">
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataField="Frequency" HeaderText="Frequency" 
                                       UniqueName="Frequency">
                                       <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                                   
                                   <telerik:GridBoundColumn  DataField="NextSystematicDate" HeaderText="Next Systematic Date" 
                    UniqueName="NextSystematicDate" SortExpression="NextSystematicDate">
                     <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                                   
                                   <telerik:GridBoundColumn Aggregate="Sum" DataField="Amount" 
                                       DataType="System.Decimal" FooterText="Total:" HeaderText="Amount" 
                                       UniqueName="Amount">
                                       <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>
                               </Columns>
                           </MasterTableView>
                           <HeaderStyle Width="200px" />
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1">
                                </Scrolling>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                           </ClientSettings>
                            </telerik:RadGrid>
                    </telerik:RadAjaxPanel>                    
        </asp:Panel>
    </telerik:RadPageView>
    <%--<telerik:RadPageView ID="RadPageView2" runat="server">
        <asp:Panel ID="pnlCalenderDetailView" runat="server">
            <table width="100%"> 
            <tr>
            <td>
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" Width="90%" EnableHistory="True"
                        HorizontalAlign="NotSet" LoadingPanelID="PorspectListLoading">               
        <telerik:RadGrid ID="gvCalenderDetailView" runat="server" GridLines="None" AutoGenerateColumns="False"
         AllowSorting="false" AllowPaging="True" PageSize="15"  ShowStatusBar="True"
         ShowFooter="true" OnItemBound="gvCalenderDetailView_ItemDataBound"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" 
        AllowFilteringByColumn="false" AllowAutomaticInserts="false" >
        <MasterTableView AllowMultiColumnSorting="True" Width="100%" AutoGenerateColumns="false">
           
            <Columns>
                   
                 <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer Name"  
                    UniqueName="CustomerName">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn DataField="Type" HeaderText="Type"  
                    UniqueName="Type">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="Scheme" HeaderText="Scheme"  
                    UniqueName="Scheme">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Frequency" HeaderText="Frequency"  
                    UniqueName="Frequency">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                            
              <%--   <telerik:GridTemplateColumn AllowFiltering="false"  HeaderText="Next Systematic Date" >
                <ItemStyle HorizontalAlign="center" Wrap="false" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblNextSystematicDate" runat="server" CssClass="CmbField" Text=''></asp:Label>
                    </ItemTemplate>
                  </telerik:GridTemplateColumn>
                  
                  <telerik:GridBoundColumn  DataField="NextSystematicDate" HeaderText="Next Systematic Date" 
                    UniqueName="NextSystematicDate" SortExpression="NextSystematicDate">
                     <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                  
                <telerik:GridBoundColumn  Aggregate="Sum" DataField="Amount" DataType="System.Decimal" HeaderText="Amount" 
                    UniqueName="Amount" FooterText="Total:"  >
                    <ItemStyle Width="" HorizontalAlign="Right"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
             
            </Columns>
                
        </MasterTableView>
        
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" >
            </Scrolling>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
               </telerik:RadAjaxPanel>
            </td>
            </tr>             
        </table>
        </asp:Panel>
    </telerik:RadPageView>--%>
   <telerik:RadPageView ID="RadPageView2" runat="server">
        <asp:Panel ID="pnlCalenderSummaryView" runat="server">
            <table width="100%" > 
           <tr><td>
         <telerik:RadGrid  ID="reptCalenderSummaryView" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="false" AllowPaging="True" 
        ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" 
        AllowFilteringByColumn="false" AllowAutomaticInserts="false" 
                   onitemdatabound="reptCalenderSummaryView_ItemDataBound">
            <PagerStyle Mode="NumericPages"></PagerStyle>
            <MasterTableView Width="100%">
            <Columns>
            <telerik:GridBoundColumn  DataField="Year"  HeaderText="Year" 
                    UniqueName="Year" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn  DataField="FinalMonth"  HeaderText="Month" 
                    UniqueName="FinalMonth" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridTemplateColumn HeaderText="SIP Amount" UniqueName="SIPAmount">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSIPAmount" Text='<%# Eval("SIPAmount")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblSIPAmountFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn HeaderText="No. of SIPs" UniqueName="NoOfSIP">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblNoOfSIP" Text='<%# Eval("NoOfSIP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfSIPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn HeaderText="No. of Fresh SIPs" UniqueName="NoOfFreshSIP">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSIP" Text='<%# Eval("NoOfFreshSIP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSIPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn HeaderText="SWP Amount" UniqueName="NoOfFreshSIP">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSWPAmount" Text='<%# Eval("SWPAmount")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblSWPAmountFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                                
                <telerik:GridTemplateColumn HeaderText="No. of SWPs" UniqueName="NoOfSWP">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblNoOfSWP" Text='<%# Eval("NoOfSWP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfSWPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                               
                 <telerik:GridTemplateColumn HeaderText="No. of fresh SWPs" UniqueName="NoOfFreshSWP">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSWP" Text='<%# Eval("NoOfFreshSWP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSWPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                 
                 
            </Columns>
            </MasterTableView>
            <ClientSettings>
                <Resizing AllowColumnResize="True"></Resizing>
            </ClientSettings>
        </telerik:RadGrid>
             </td></tr>           
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
</div>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
     <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    </div>
    </td>
    </tr>
 </table>
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnadviserId" runat="server"/>
<asp:HiddenField ID="hdnAll" runat="server"/>
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<asp:HiddenField ID="hdnstartdate" runat="server" />
<asp:HiddenField ID="hdnendDate" runat="server" />
<asp:HiddenField ID="hdnamcCode" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
<asp:HiddenField ID="hdnschemeCade" runat="server" />
<asp:HiddenField ID="hdnSystematicType" runat="server" />
<asp:HiddenField ID="hdnTodate" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />

