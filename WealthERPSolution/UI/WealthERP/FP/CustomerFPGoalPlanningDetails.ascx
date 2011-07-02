<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalPlanningDetails.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalPlanningDetails" %>
<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Goal Planning"></asp:Label>
<hr />

<table class="TableBackground" style="width: 100%">
<tr>
<td>
<asp:Panel ID="tbl" runat="server" class="Landscape" Width="68%" ScrollBars="Horizontal">

<asp:GridView ID="gvrGoalPlanning" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" HorizontalAlign="Center"
                CellPadding="4" EnableViewState="True" AllowPaging="True" ShowFooter="true"
                CssClass="GridViewStyle" DataKeyNames="GoalId,GoalCategory">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" CssClass="cmbField" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange" AutoPostBack="true" runat="server" Width="70px">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>View</asp:ListItem>
                            <asp:ListItem>Edit</asp:ListItem>
                            <asp:ListItem>Fund</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                     </asp:TemplateField>
                    <asp:TemplateField HeaderText="Goal Type">
                        <ItemTemplate>
                         <asp:Label ID="lblGoalType" runat="server" CssClass="cmbField" Text='<%#Eval("GoalType")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Name of Associalte<br/>Customer/Description">
                        <ItemTemplate>
                         <asp:Label ID="lblChildName" runat="server" CssClass="cmbField" Text='<%#Eval("ChildName")%>'>
                         </asp:Label>                        
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Cost Today" >
                        <ItemTemplate>
                         <asp:Label ID="lblCostToday" runat="server" CssClass="cmbField" Text='<%#Eval("CostToday")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Req in Year">
                        <ItemTemplate>
                         <asp:Label ID="lblGoalYear" runat="server" CssClass="cmbField" Text='<%#Eval("GaolYear")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Goal Amount<br/>in Goal Year">
                        <ItemTemplate>
                         <asp:Label ID="lblGoalAmountInGoalYear" runat="server" CssClass="cmbField" Text='<%#Eval("GoalAmountInGoalYear")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Corpus to be<br/>Left Behind">
                        <ItemTemplate>
                         <asp:Label ID="lblCorpusLeftBehind" runat="server" CssClass="cmbField" Text='<%#Eval("CorpusLeftBehind")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Priority">
                        <ItemTemplate>
                         <asp:Label ID="lblGoalPriority" runat="server" CssClass="cmbField" Text='<%#Eval("GoalPriority")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>  
                    
                    <asp:TemplateField HeaderText="Eq Funding(Rs.)">
                        <ItemTemplate>
                         <asp:Label ID="lblEquityFunded" runat="server" CssClass="cmbField" Text='<%#Eval("EquityFundedAmount")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Debt Funding(Rs.)" >
                        <ItemTemplate>
                         <asp:Label ID="lblDebtFunded" runat="server" CssClass="cmbField" Text='<%#Eval("DebtFundedAmount")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Cash Funding(Rs.)" >
                        <ItemTemplate>
                         <asp:Label ID="lblCashFunded" runat="server" CssClass="cmbField" Text='<%#Eval("CashFundedAmount")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Alternate Funding(Rs.)">
                        <ItemTemplate>
                         <asp:Label ID="lblAlternateFunded" runat="server" CssClass="cmbField" Text='<%#Eval("AlternateFundedAmount")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Total Funding(Rs.)">
                        <ItemTemplate>
                         <asp:Label ID="lblTotalFunded" runat="server" CssClass="cmbField" Text='<%#Eval("TotalFundedAmount")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Funded Gap(Rs)">
                        <ItemTemplate>
                         <asp:Label ID="lblGoalFundGap" runat="server" CssClass="cmbField" Text='<%#Eval("GoalFundedGap")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Indicator">
                        <ItemTemplate>                         
                            <asp:Image ID="GoalFundIndicator" ImageAlign="Middle" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                   <asp:TemplateField HeaderText="FundedType">
                        <ItemTemplate>
                         <asp:Label ID="lblGoalFundedType" runat="server" CssClass="cmbField" Text='<%#Eval("GoalFundedType")%>'>
                         </asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>  
                                
                </Columns>
            </asp:GridView>
 
 </asp:Panel>
</td>
</tr>

</table>