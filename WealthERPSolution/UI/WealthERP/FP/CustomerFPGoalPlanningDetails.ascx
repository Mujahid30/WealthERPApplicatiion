<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalPlanningDetails.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalPlanningDetails" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<script language="javascript" type="text/javascript">
    function showmessage() {
      
            var bool = window.confirm('Are you sure you want to delete this profile?');

            if (bool) {
                document.getElementById("ctrl_CustomerFPGoalPlanningDetails_hdnMsgValue").value = 1;
                document.getElementById("ctrl_CustomerFPGoalPlanningDetails_hiddenassociation").click();
                return false;
            }
            else {
                document.getElementById("ctrl_CustomerFPGoalPlanningDetails_hdnMsgValue").value = 0;
                document.getElementById("ctrl_CustomerFPGoalPlanningDetails_hiddenassociation").click();
                return true;
       
        } 
    }
</script>

    
 <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
 
<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Goal Planning"></asp:Label>
<hr />
<table width="100%">
    <tr id="trDeleteSuccess" runat="server" visible="false">
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center">
                Goal has been deleted Successfully.
            </div>
        </td>
    </tr>
</table>

<table width="100%">
 <tr id="trNoRecordFound" runat="server" visible="false">
        <td align="center">
         <div id="Div1" runat="server" class="failure-msg" align="center">
                No Records Found...
         </div>
           
        </td>
  </tr>
<tr>
<td>
<asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Vertical">

<asp:GridView ID="gvGoalList" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" HorizontalAlign="Center"
                CellPadding="4" EnableViewState="True" AllowPaging="True" ShowFooter="true"
                CssClass="GridViewStyle" DataKeyNames="GoalId,GoalCode" OnRowDataBound="gvGoalList_RowDataBound">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                   <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <%-- <asp:DropDownList ID="ddlAction" CssClass="cmbField" AutoPostBack="true" runat="server">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>View</asp:ListItem>
                            <asp:ListItem>Edit</asp:ListItem>
                            <asp:ListItem>Fund</asp:ListItem>
                            <asp:ListItem>Delete</asp:ListItem>
                            </asp:DropDownList>--%>
                           
                         <telerik:radcombobox id="ddlAction" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange" CssClass="cmbField" runat="server" EnableEmbeddedSkins=false skin="Telerik" allowcustomtext="true" Width="130px" AutoPostBack="true">    
                                <Items>   
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">        
                                    </telerik:RadComboBoxItem>       
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/DetailedView.png" Text="View" Value="View" runat="server">        
                                    </telerik:RadComboBoxItem>        
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit" runat="server">        
                                    </telerik:RadComboBoxItem>        
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/GoalFund.png" Text="Fund" Value="Fund" runat="server">        
                                    </telerik:RadComboBoxItem>        
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete" runat="server">        
                                    </telerik:RadComboBoxItem>        
                                       
                                </Items>
                        </telerik:radcombobox>
                        
                        </ItemTemplate>
                     </asp:TemplateField>
                    
                   <asp:TemplateField HeaderText="Goal" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lnkGoalType" runat="server" CssClass="cmbField" 
                                            OnClick="lnkGoalType_Click" Text='<%# Eval("GoalName") %>'>
                                        </asp:LinkButton>--%>
                                        <asp:Label ID="lblGoalName" runat="server" CssClass="cmbField" Text='<%# Eval("GoalCode") %>'>
                                        </asp:Label>
                                        <asp:Image ID="imgGoalImage" ImageAlign="Middle" runat="server" />
                                        
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text=" Total  =  Rs.">
                                        </asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>   
                                
                   <asp:TemplateField HeaderText="Year" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoalYear" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("GoalYear") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                        
                   <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGaolAmount" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("GoalAmount")%>'>
                                        </asp:Label>
                                    </ItemTemplate>  
                                     <FooterTemplate>
                                        <asp:Label ID="lblGoalAmountTotal" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                                    </FooterTemplate>                                 
                                </asp:TemplateField>                   
                                
                   <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoalDate" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("GoalPrifileDate", "{0:M-dd-yyyy}")  %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                      
                   <asp:TemplateField HeaderText="Cost At Start" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCostToday" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CostToday")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblCostTodayTotal" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                                    </FooterTemplate>
                     </asp:TemplateField>
                                
                   <asp:TemplateField HeaderText="Value Of Investment" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentGoalValue" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("CurrentGoalValue")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                
                   <asp:TemplateField HeaderText="Monthly SIP Contribution" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValueOfInvestment" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("SIPAmount")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                
                   <asp:TemplateField HeaderText="Projected Value On Goal Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectedValue" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("ProjectedValue")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                
                   <asp:TemplateField HeaderText="Additional Saving Req." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdditionalSavingReq" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("AdditionalSavingReq")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>   
                       
                   <asp:TemplateField HeaderText="IsActive" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsActive" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("IsActive") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:DropDownList ID="ddlActiveFilter" runat="server" AutoPostBack="true" 
                                            CssClass="cmbField" 
                                             Visible="false">
                                            <asp:ListItem Text="Active" Value="1">
                                            </asp:ListItem>
                                            <asp:ListItem Text="InActive" Value="0">
                                            </asp:ListItem>
                                            <asp:ListItem Text="All" Value="2">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                        <br></br>
                                        <asp:Label ID="ActiveMessage" runat="server" BackColor="Transparent" 
                                            CssClass="cmbField" Font-Bold="true" ForeColor="White" Text="No Active Goals">
                                         </asp:Label>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                    
                                               
                   <asp:TemplateField HeaderText="Projected Gap Value" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                      <ItemTemplate>
                            <asp:Label ID="lblProjectedGapValue" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("ProjectedGapValue")%>'>
                            </asp:Label>
                       </ItemTemplate>
                                    
                   </asp:TemplateField>   
                       
                   <asp:TemplateField HeaderText="Progress Indicator" ItemStyle-HorizontalAlign="Center">
                          <ItemTemplate>  
                             <asp:Label ID="lblIsGoalGap" runat="server" CssClass="cmbField" 
                                            Text='<%#Eval("IsGoalBehind") %>'>
                             </asp:Label>                       
                                <asp:Image ID="imgGoalFundGap" ImageAlign="Middle" runat="server" />
                          </ItemTemplate>
                   </asp:TemplateField>
                             
                             
                </Columns>
                
                 <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                 <HeaderStyle CssClass="HeaderStyle" Wrap="true"/>
                 <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                 <RowStyle CssClass="RowStyle" />
                 <SelectedRowStyle CssClass="SelectedRowStyle" />
            </asp:GridView>
 
 </asp:Panel>
</td>
</tr>

</table>
<asp:HiddenField ID="hidRTSaveReq" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" BorderStyle="None" BackColor="Transparent" />