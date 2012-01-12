<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalFundingProgress.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalFundingProgress" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

<script type="text/javascript">
    function ShowPopup() {
        var form = document.forms[0];
        var transactionId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("chkId", "hdnchkBx");
                    hiddenFieldValues = document.getElementById(hiddenField).value;
                    var splittedValues = hiddenFieldValues.split("-");

                }
            }
        }
        //        if (count > 1) {
        //            alert("You can select only one record at a time.")
        //            return false;
        //        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
    }
</script>

<%--
<script type="text/javascript">

    function ChkAllocationPercentage() {
        var gvControl = document.getElementById('<%= gvExistInvestMapping.ClientID %>');
        var inputTypes = gvControl.getElementsByTagName("TextBox");
        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'TextBox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
               alert('2')
        }
        var u = document.getElementById("ctrl_CustomerFPGoalFundingProgress_gvExistInvestMapping_ctl03_lblAvailableAllocation").innerHTML;
        alert(u);
    }
     </script>
<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvExistInvestMapping.Rows.Count %>');
        var gvControl = document.getElementById('<%= gvExistInvestMapping.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
    </script>--%>



      


<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Goal Funding/Progress"></asp:Label>
<br />
<%--<telerik:RadToolBar ID="aplToolBar" runat="server" Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>
        
    </Items>
</telerik:RadToolBar>--%>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
</table>

<telerik:RadTabStrip ID="RadTabStripFPGoalDetails" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerFPGoalDetail" SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Goal Funding/Progress" Value="Funding" Selected="true" TabIndex="0">
        </telerik:RadTab>  
         <telerik:RadTab runat="server" Text="Model Portfolio" Value="Model" TabIndex="1">
        </telerik:RadTab>  
    </Tabs>
</telerik:RadTabStrip>



<telerik:RadMultiPage ID="CustomerFPGoalDetail" EnableViewState="true" runat="server" SelectedIndex="0">
 <telerik:RadPageView ID="RadPageView2" runat="server">
    <asp:Panel ID="pnlFundingProgress" runat="server">
    <br />
    <table>
    <%--****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblGoalName" Text="Goal:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtGoalName" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
    <td colspan="4">
     <asp:Label id="lblGoalStatus" Text="" CssClass="FieldName" runat="server"></asp:Label>
    </td>
    </tr>
   <%-- ****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblStartDate" Text="Start Date:"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtStartDate" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblTargetDate" Text="Target Date:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtTargetDate" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
    
    <td class="leftField">
     <asp:Label id="lblGoalAmount" Text="Goal Amount(Rs):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtGoalAmount" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>     
    </tr>
     <%-- ****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblTenureCompleted" Text="Tenure Completed (Years):"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtTenureCompleted" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblBalanceTenor" Text="Balance Tenure(Years):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtBalanceTenor" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
    
    <td class="leftField">
     <asp:Label id="lblMonthlyContribution" Text="Monthly Contribution(Rs):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtMonthlyContribution" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>    
    </tr>
     <%-- ****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblAmountInvestedTillDate" Text="Amount Invested Till Date(Rs):"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtAmountInvested" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblValueOfCurrentGoal" Text="Value of Current Goal(Rs):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtValueOfCurrentGoal" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
    
    <td class="leftField">
     <asp:Label id="lblReturnsXIRR" Text="Returns (XIRR)(%):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtReturnsXIRR" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>    
    </tr>
    <%-- ****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblCostAtBeginning" Text="Cost At Beginning(Rs):"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtCostAtBeginning" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField" colspan="4">
     <asp:Label id="Label2" Text=""  CssClass="FieldName" runat="server"></asp:Label>
    </td>
     
    </tr>
    <%-- ****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblEstmdTimeToReachGoal" Text="Estmd Time To Reach Goal:"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtEstmdTimeToReachGoal" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblProjectedValueOnGoalDate" Text="Projected Value On Goal Date:"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtProjectedValueOnGoalDate" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
    
    <td class="leftField">
     <asp:Label id="lblProjectedGap" Text="Projected Gap(Rs):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtProjectedGap" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>    
    </tr>
    <%-- ****************************************************************************--%>
    <tr>
    <td class="leftField">
     <asp:Label id="lblAdditionalInvestmentsRequired" Text="Additional Investments Required(Rs)(Per Month):"  CssClass="FieldName" runat="server"></asp:Label>
     </td>
    <td class="rightField">
     <asp:TextBox ID="txtAdditionalInvestmentsRequired" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
     
    <td class="leftField">
     <asp:Label id="lblAdditionalInvestments" Text="Additional Investments Required(Rs)(Per Year):"  CssClass="FieldName" runat="server"></asp:Label>
    </td>
    <td class="rightField">
     <asp:TextBox ID="txtAdditionalInvestments" runat="server" Text="" CssClass="txtField" ReadOnly="true"></asp:TextBox>
    </td>
    
    <td class="leftField" colspan="2">
     <asp:Label id="Label4" Text=""  CssClass="FieldName" runat="server"></asp:Label>
    </td>
        
    </tr>
    <tr>
    <td colspan="6">
    <hr />
    </td>
    </tr>
   </table>
   </asp:Panel>
   <br />
  
   
    <asp:Panel runat="server" ID="pnlDocuments">
   <table ID="tblDocuments" runat="server" Style="border: solid 1px #CCC" Width="100%" rules="rows">
     <tr class="EditFormHeader">
                            <td colspan="2" style="font-size: small">
                                <b>MF Investment Funding</b>
                            </td>
                        </tr>
                        </table>
      <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid" GridLines="None" Width="100%"
        AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="RadGrid1_ItemDataBound" OnDeleteCommand="RadGrid1_DeleteCommand"  OnInsertCommand="RadGrid1_ItemInserted"
        OnItemUpdated="RadGrid1_ItemUpdated" OnItemCommand="RadGrid1_ItemCommand"
        OnPreRender="RadGrid1_PreRender">
        <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false" DataKeyNames="SchemeCode,OtherGoalAllocation">
            <Columns>
                <telerik:GridEditCommandColumn>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Scheme" DataField="SchemeName">
                    <%--<HeaderStyle ForeColor="Silver"></HeaderStyle>--%>
                   <%-- <ItemStyle ForeColor="Gray" />--%>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="InvestedAmount" HeaderText="Invested Amount(Rs)" DataField="InvestedAmount" DataFormatString="{0:C2}">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Units" HeaderText="Units" DataField="Units" DataFormatString="{0:C2}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CurrentValue" HeaderText="Current Value(Rs)" DataField="CurrentValue" DataFormatString="{0:C2}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="ReturnsXIRR" HeaderText="Returns (XIRR)(%)" DataField="ReturnsXIRR" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="ProjectedAmount" HeaderText="Projected amount in goal year(Rs)" DataField="ProjectedAmount" DataFormatString="{0:n2}">
                </telerik:GridBoundColumn>
                 <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Are you sure you want to Remove this Record?"  UniqueName="column">
                </telerik:GridButtonColumn>
                <%--<telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>--%>
            </Columns>
          <EditFormSettings EditFormType="Template">
                <FormTemplate>
                    <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
                        style="border-collapse: collapse; background: white;">
                        <tr class="EditFormHeader">
                            <td colspan="2" style="font-size: small">
                                <b>MF Investment Funding</b>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                <table id="Table3" cellspacing="1" cellpadding="1" border="0" class="module">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trSchemeDDL">
                                     <td align="right">
                                            <asp:Label id="Label13" Text="Scheme:" CssClass="FieldName" runat="server" >
                                        </asp:Label> 
                                        </td>
                                    <td>
                               <asp:DropDownList ID="ddlPickScheme" runat="server" CssClass="cmbField">                                    
                                </asp:DropDownList>
                            </td>
                            </tr>
                                    <tr runat="server" id="trSchemeTextBox" >
                                        <td align="right">
                                            <asp:Label id="Label3" Text="Scheme:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                        <asp:Label id="lblGoalName" Text='<%# Bind("SchemeName") %>'  CssClass="FieldName" runat="server">
                                        </asp:Label>
                                           
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="right">
                                        <asp:Label id="Label5" Text="Units:" CssClass="FieldName" runat="server">
                                        </asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUnits" runat="server" CssClass="txtField" Text='<%# Bind("Units") %>'  Enabled="false" TabIndex="2" >
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                          <asp:Label id="Label6" Text="Current Value(Rs):" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrentValue" CssClass="txtField" runat="server" Text='<%# Bind("CurrentValue") %>'  Enabled="false" TabIndex="3">
                                            </asp:TextBox>
                                        </td>
                                    </tr>      
                                     <tr>
                                        <td align="right">
                                           <asp:Label id="Label7" Text="Total Goal Allocation(%):" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="txtField" Enabled="false" Text='<%# Bind("AllocationEntry") %>' TabIndex="3">
                                            </asp:TextBox>
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td align="right">
                                        <asp:Label id="Label8" Text="Current Goal Allocation(%):" CssClass="FieldName" runat="server" Enabled="false">
                                        </asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="txtField" Text='<%# Bind("CurrentGoalAllocation") %>' TabIndex="3" > 
                                            </asp:TextBox>
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td align="right">
                                         <asp:Label id="Label9" Text="Other Goal Allocation(%):" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSchemeAllocationPer" CssClass="txtField" runat="server" Text='<%# Bind("OtherGoalAllocation") %>'  Enabled="false" TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                         <asp:Label id="Label10" Text="Available Allocation(%):" CssClass="FieldName" runat="server">
                                        </asp:Label>  
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="txtField"  Enabled="false" Text='<%# Bind("AvailableAllocation") %>' TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                    </tr>                                
                                </table>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
        
        </ClientSettings>
    </telerik:RadGrid>

    </asp:Panel>
    
    <hr />
  
   <asp:Panel runat="server" ID="Panel2">
   <table ID="Table1" runat="server" Style="border: solid 1px #CCC" Width="100%" rules="rows">
        <tr class="EditFormHeader">
                            <td colspan="2" style="font-size: small">
                                <b>Monthly SIP MF Funding</b>
                            </td>
                             <td id="Td1" runat="server" align="right">
     <asp:Button ID="btnSIPAdd" runat="server" CssClass="PCGButton" Text="Add SIP" OnClick="btnSIPAdd_OnClick" />
     </td>
                        </tr>
                        </table>
      <telerik:RadGrid ID="RadGrid2" runat="server" CssClass="RadGrid" GridLines="None"
        AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="RadGrid2_ItemDataBound" OnInsertCommand="RadGrid2_ItemInserted" OnDeleteCommand="RadGrid2_DeleteCommand"
        OnItemCommand="RadGrid2_ItemCommand" >
        <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false" DataKeyNames="SIPId,TotalSIPamount">
            <Columns>
                <telerik:GridEditCommandColumn>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Scheme" DataField="SchemeName">
                    <%--<HeaderStyle ForeColor="Silver"></HeaderStyle>--%>
                   <%-- <ItemStyle ForeColor="Gray" />--%>
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn UniqueName="AvailableAllocation" HeaderText="SIP Amount Available" DataField="AvailableAllocation">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="SIPInvestedAmount" HeaderText="SIP Amount Invested" DataField="SIPInvestedAmount">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="SIPProjectedAmount" HeaderText="SIP Projected Amount" DataField="SIPProjectedAmount">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn CommandName="Delete" Text="Delete" ConfirmText="Are you sure you want to Remove this Record?"  UniqueName="column">
                </telerik:GridButtonColumn>
               
                <%--<telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="column">
                </telerik:GridButtonColumn>--%>
            </Columns>
            <EditFormSettings EditFormType="Template">
                <FormTemplate>
                    <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
                        style="border-collapse: collapse; background: white;">
                        <tr class="EditFormHeader">
                            <td colspan="2" style="font-size: small">
                                <b>Monthly SIP MF Funding</b>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                <table id="Table3" cellspacing="1" cellpadding="1" border="0" class="module">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trSchemeNameDDL">
                                     <td align="right">
                                            <asp:Label id="Label13" Text="Scheme-Amount-SIP Date:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                    <td>
                               <asp:DropDownList ID="ddlPickSIPScheme" runat="server" CssClass="cmbField">                                    
                                </asp:DropDownList>
                            </td>
                            </tr>
                                    <tr runat="server" id="trSchemeNameText">
                                        <td align="right">
                                            <asp:Label id="Label3" Text="Scheme:" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                        <asp:Label id="lblSchemeName" Text='<%# Bind("SchemeName") %>'  CssClass="FieldName" runat="server">
                                        </asp:Label>
                                           
                                        </td>
                                    </tr>                                  
                                      
                                    <tr>
                                        <td align="right">
                                        <asp:Label id="Label8" Text="Current Goal Allocation(Rs):" CssClass="FieldName" runat="server" Enabled="false">
                                        </asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="txtField" Text='<%# Bind("SIPInvestedAmount") %>' TabIndex="3">
                                            </asp:TextBox>
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td align="right">
                                         <asp:Label id="Label9" Text="Other Goal Allocation(Rs):" CssClass="FieldName" runat="server">
                                        </asp:Label> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOtherSchemeAllocationPer" CssClass="txtField" runat="server" Text='<%# Bind("OtherGoalAllocation") %>'  Enabled="false" TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                         <asp:Label id="Label10" Text="Available Amount(Rs):" CssClass="FieldName" runat="server">
                                        </asp:Label>  
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="txtField"  Enabled="false" Text='<%# Bind("AvailableAllocation") %>' TabIndex="1">
                                            </asp:TextBox>
                                        </td>
                                    </tr>                                
                                </table>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
        </ClientSettings>
    </telerik:RadGrid>
  
    </asp:Panel>
     
   
 </telerik:RadPageView>
 <telerik:RadPageView ID="RadPageView4" runat="server">
  <asp:Panel runat="server" ID="Panel4">
        <table width="100%">
                            <tr style="float: left">
                            <td>
                            <asp:Label ID="lblModelPortfolio" runat="server" CssClass="FieldName" Text="Select Model Portfolio :"></asp:Label>
                            </td>
                                <td>
                                <asp:DropDownList ID="ddlModelPortFolio" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlModelPortFolio_OnSelectedIndexChanged"></asp:DropDownList>
                                </td>
                                </tr>
                                </table>
                             
                                <br />
                                
                                <table id="tableGrid" runat="server" class="TableBackground" width="100%">
                                

    <tr>
        <td>
    <telerik:RadGrid ID="RadGrid3" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" 
    PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" 
    AllowAutomaticInserts="false" 
    AllowAutomaticUpdates="false" HorizontalAlign="NotSet">
        <MasterTableView>
            <Columns>
                <%--<telerik:GridClientSelectColumn UniqueName="SelectColumn"/>--%>               
                <telerik:GridBoundColumn  DataField="PASP_SchemePlanName"  HeaderText="Scheme Name" UniqueName="PASP_SchemePlanName" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_AllocationPercentage"  HeaderText="Weightage" UniqueName="AMFMPD_AllocationPercentage">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                      
                <telerik:GridBoundColumn  DataField="AMFMPD_AddedOn"  HeaderText="Started Date" UniqueName="AMFMPD_AddedOn">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="AMFMPD_SchemeDescription"  HeaderText="Description" UniqueName="AMFMPD_SchemeDescription">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
            </Columns>
           <%-- <table id="tblArchive" runat="server">
            <tr>
            <td class="leftField">
                        <asp:Label ID="lblArchive" runat="server" CssClass="FieldName" Text="Reason for Archiving:"></asp:Label>
                    </td>
                    <td class="rightField">                         
                        <asp:DropDownList ID="ddlArchive" runat="server" CssClass="cmbField">               
                        </asp:DropDownList>
                    </td>
            </tr>
            </table>--%>                    
        </MasterTableView>
        <ClientSettings>
            <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
        </ClientSettings>
    </telerik:RadGrid>   
    </td>
    </tr>
    </table>
    </asp:Panel>
 </telerik:RadPageView>
       
 </telerik:RadMultiPage>
