<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalFundingProgress.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalFundingProgress" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<script type="text/javascript">
    function SetPopUpId() {

        document.getElementById("<%= GoalMappingPopUp.ClientID %>").style.visibility = 'hidden';

        var mpu = document.getElementById("<%=mdlPopupSlabCalculate.ClientID %>").value;
        mpu.hide();
//        $find('mdlPopupSlabCalculate_ModalPopupExtender').hide();
        alert('hi');
        document.getElementById("<%= mdlPopupSlabCalculate.ClientID %>").setAttribute("BackgroundCssClass", "hdnModalPopupId"); 
    }

</script>--%>


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
    </script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Goal Funding/Progress"></asp:Label>
<br />
<telerik:RadToolBar ID="aplToolBar" runat="server" Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%">
    <Items>
        <telerik:RadToolBarButton ID="btnEdit" runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">            
        </telerik:RadToolBarButton>
        
    </Items>
</telerik:RadToolBar>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record saved Successfully
            </div>
        </td>
    </tr>
</table>

<telerik:RadTabStrip ID="RadTabStripFPFundingProgress" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="CustomerFPGoalFunding" SelectedIndex="0" EnableViewState="true">
    <Tabs>
        <telerik:RadTab runat="server" Text="Goal Funding" Value="Funding" Selected="true" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Goal Progress" Value="Progress" TabIndex="1">
        </telerik:RadTab>       
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
    EnableEmbeddedSkins="false">
</telerik:RadAjaxLoadingPanel>

<telerik:RadMultiPage ID="CustomerFPGoalFunding" EnableViewState="true" runat="server" SelectedIndex="0">
 <telerik:RadPageView ID="RadPageView2" runat="server">
    <asp:Panel ID="pnlFunding" runat="server">
    <br />
    <table>
    <tr>
    <td runat="server" align="right">
    <asp:Label ID="lblGoal" runat="server" CssClass="FieldName" Text="Goal:"></asp:Label>
    
    </td>
     <td runat="server" align="left">
     <asp:TextBox ID="txtGoalName" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
     </td>
    </tr>
    <tr>
    <td  runat="server" align="right">
     <asp:Label ID="lblGoalYear" runat="server" CssClass="FieldName" Text="Goal Year:"></asp:Label>
    </td>
    <td  runat="server" align="left">
     <asp:TextBox ID="txtGoalYearDetails" runat="server" CssClass="txtField"  ReadOnly="true"></asp:TextBox>
    </td>
    </tr>
    
     <tr>
    <td runat="server" align="right">
     <asp:Label ID="lblGoalTargetAmount" runat="server" CssClass="FieldName" Text="Goal Target Amount:"></asp:Label>
    </td>
    <td  runat="server" align="left">
     <asp:TextBox ID="txtGoalTargetAmountDetails" runat="server" CssClass="txtField"  ReadOnly="true"></asp:TextBox>
    </td>
    </tr>
    
    <tr>
    <td  runat="server" align="right">
     <asp:Label ID="lblGoalBalanceAmount" runat="server" CssClass="FieldName" Text="Goal Balance Amount:"></asp:Label>
    </td>
    <td  runat="server" align="left">
     <asp:TextBox ID="txtGoalBalanceAmountDetails" runat="server" CssClass="txtField"  ReadOnly="true"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td runat="server" align="right">
     <asp:Label ID="lblGoalFundAccumulated" runat="server" CssClass="FieldName" Text="Fund Accumulated:"></asp:Label>
    </td>
    <td  runat="server" align="left">
     <asp:TextBox ID="txtGoalFundAccumulatedDetails" runat="server" CssClass="txtField"  ReadOnly="true"></asp:TextBox>
    </td>
    </tr>
    
    <tr>
    <td id="tdMapping"  runat="server" align="right">
     <asp:Label ID="lblGoalMapping" runat="server" CssClass="FieldName" Text="Map From:"></asp:Label>
    </td>
    <td id="Td2"  runat="server" align="left">
     <asp:DropDownList ID="ddlGoalMapping" onclick="SetPopUpId()" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGoalMapping_OnSelectedIndexChanged" CssClass="cmbField">
      <asp:ListItem Text="Select" Value="Select" />
     <asp:ListItem Text="Existing Investment" Value="Investment" />
     <asp:ListItem Text="Existing MF SIPs" Value="MfSIP" />
     <asp:ListItem Text="Future Savings" Value="FutureSavings" />
     </asp:DropDownList>
    </td>
    </tr>
    <tr>
     <td>
                          <asp:Label ID="lblPickAssetClass" runat="server" Text="Pick a asset class" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left">
                          <asp:TextBox ID="txtPickAssetClass" runat="server" Text="Mutual Fund"  ReadOnly="true"  CssClass="txtField"></asp:TextBox>
                                                
                    </td>
    
    </tr>
    
     <tr><td></td>
                <td>
                 <asp:GridView ID="gvExistInvestMapping" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    ShowFooter="true" CssClass="GridViewStyle" DataKeyNames="GoalId,SchemeCode,OtherGoalAllocation" Visible="true" AllowSorting="true">
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <HeaderTemplate>
                            <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                            <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                             <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("GoalId").ToString()%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField Visible="false">
                     <HeaderTemplate>
                                <asp:Label ID="lbl_SchemeCode" runat="server" Text="SchemeCode" Visible="false"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSchemeCode" runat="server" Visible="false" Text='<%# Bind("SchemeCode") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_Scheme" runat="server" Text="Schemes"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSchemes" runat="server" Text='<%# Bind("Schemes") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_Amount" runat="server" Text="Invested Amount"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblInvested" runat="server" Text='<%# Bind("InvestedAmount") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_units" runat="server" Text="Units"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblUnits" runat="server" Text='<%# Bind("Units") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                       <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_CurrValue" runat="server" Text="Current Value"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCurrValue" runat="server" Text='<%# Bind("CurrentValue") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_Allocation" runat="server" Text="Allocation(%)"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
            <%-- <asp:Label ID="lblAllocationEntry" runat="server" Text='<%# Bind("AllocationEntry") %>'></asp:Label>--%>
                          <asp:TextBox ID="txtAllocationEntry" runat="server" Text='<%# Bind("CurrentGoalAllocation") %>' CssClass="txtField"></asp:TextBox>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                       <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_AllocationEntry" runat="server" Text="Overall Allocation(%)"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
           <asp:Label ID="lblAllocationEntry" runat="server" Text='<%# Bind("AllocationEntry") %>'></asp:Label>
                         <%-- <asp:TextBox ID="txtAllocationEntry" runat="server" Text='<%# Bind("AllocationEntry") %>' OnBlur="return ChkAllocationPercentage()" CssClass="txtField"></asp:TextBox>
                    --%>        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_AvailableAllocationEntry" runat="server" Text="Available Allocation"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                           <%--  <asp:TextBox ID="txtAvailableAllocation" runat="server" Text='<%# Bind("AvailableAllocation") %>' OnBlur="return ChkAllocationPercentage()" CssClass="txtField"></asp:TextBox>--%>
                              <asp:Label ID="lblAvailableAllocation" runat="server" Text='<%# Bind("AvailableAllocation") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    <%--<asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_XIRR" runat="server" Text="Returns(XIRR)"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblXIRR" runat="server" Text='<%# Bind("CMFNP_XIRR") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>--%>
                     </Columns>
                </asp:GridView>
                </td>
                </tr>
                <tr>
                <td>
                
                </td>
                <td>
                <asp:Button ID="btnMapAllocation" runat="server" CssClass="PCGLongButton" Text="Submit"
                OnClientClick="return ShowPopup()" OnClick="btnMapAllocation_OnClick" />
                </td>
                </tr>
                
   <%-- <tr>
     <td align="left">
          <cc1:ModalPopupExtender ID="mdlPopupSlabCalculate" runat="server" PopupControlID="GoalMappingPopUp"
          TargetControlID="hdnModalPopupId" OkControlID="btnCalculationSubmit" CancelControlID="btnCancel" BackgroundCssClass="modalBackground" Enabled="true" Drag="true"></cc1:ModalPopupExtender>
                                    
    </td>
     <td>
       <asp:Panel ID="GoalMappingPopUp" Width="300px" CssClass="ModelPup" runat="server">
             <table>
                 <tr>
                     <td>
                          <asp:Label ID="lblPickAssetClass" runat="server" Text="Pick a asset class" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left">
                          <asp:TextBox ID="txtPickAssetClass" runat="server" Text="Mutual Fund" ></asp:TextBox>
                                                
                    </td>
                </tr>
                <tr>
                <td>
                 <asp:GridView ID="gvExistInvestMapping" runat="server" AutoGenerateColumns="true" CellPadding="4"
                    ShowFooter="true" CssClass="GridViewStyle" Visible="true" AllowSorting="true"
                    >
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <HeaderTemplate>
                            <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                            <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_Scheme" runat="server" Text="Schemes"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSchemes" runat="server" Text='<%# Bind("PASP_SchemePlanName") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                      <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_units" runat="server" Text="Units"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblUnits" runat="server" Text='<%# Bind("CMFNP_NetHoldings") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                       <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_CurrValue" runat="server" Text="Current Value"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCurrValue" runat="server" Text='<%# Bind("CMFNP_CurrentValue") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                       <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_AllocationEntry" runat="server" Text="Allocation Entry(%)"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAllocationEntry" runat="server" Text='<%# Bind("CMFA_AccountId") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                     <HeaderTemplate>
                                <asp:Label ID="lbl_XIRR" runat="server" Text="Returns(XIRR)"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblXIRR" runat="server" Text='<%# Bind("CMFNP_XIRR") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                     </Columns>
                </asp:GridView>
                </td>
                </tr>
                <tr>
                 <td>
                        <asp:Button ID="btnCalculationSubmit" runat="server" Text="Submit" 
                                    CssClass="PCGButton" />
                  </td>
                   <td>
                         <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                  </td>
                </tr>
            </table>
    
           </asp:Panel>
           </td>
           </tr>--%>
     </table>
     
     </asp:Panel>
     
   
 </telerik:RadPageView>
  
 <telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:Panel ID="Panel1" runat="server">
       
       
       
        </asp:Panel>
        </telerik:RadPageView>
        
 </telerik:RadMultiPage>


<asp:HiddenField ID="hdnModalPopupId" runat="server" />