<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalDashBoard.ascx.cs" Inherits="WealthERP.FP.CustomerFPGoalDashBoard" %>
<asp:Label ID="lblOrderMIS" runat="server" CssClass="HeaderTextBig" Text="Customer Dashboard"></asp:Label>
<br />
<hr />
<br />

<table width="100%" class="TableBackground">
<tr>
<td></td>
<td>
    <asp:GridView ID="gVCustomerDashboard" runat="server" CssClass="GridViewStyle" 
        ShowFooter="True" onrowdatabound="gVCustomerDashboard_RowDataBound1"  AutoGenerateColumns="False" Width="60%">
    
    <RowStyle CssClass="RowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />  
    
    <Columns>
                               
                                 <asp:BoundField DataField="Goal" HeaderText="Goal">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Font-Underline="true"></ItemStyle>
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="Goal Amount" HeaderText="Goal Amount">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="Target year" HeaderText="Target year">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            
                                <asp:TemplateField HeaderText="Performance">
                                    <ItemTemplate>                         
                                        <asp:Image ID="imgActionIndicator" ImageAlign="Middle" runat="server" />
                                    </ItemTemplate>                           
                                     <ItemStyle HorizontalAlign="Center" />
                                 </asp:TemplateField>  
                                 
                                 <asp:TemplateField HeaderText="Performance">
                                    <ItemTemplate>                         
                                        <asp:label ID="lblImage"  Text='<%#Eval("GoalFlag") %>' runat="server" />
                                    </ItemTemplate>                           
                                     <ItemStyle HorizontalAlign="Center" />
                                 </asp:TemplateField>  
                                 
                                  <asp:BoundField DataField="Fund Gap" HeaderText="Fund Gap">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                 
                                
   </Columns>
    </asp:GridView>
    </td></tr>
</table>

<table>
<tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr>
<tr>
<td>
<asp:linkbutton CssClass="FieldName" ID="lblGoalNotification" runat="server" text="Goal Notifications"/>
</td>
</tr>
<tr></tr><tr></tr><tr></tr><tr></tr>
<tr>
<td>
<asp:label CssClass="FieldName" ID="lblSip" runat="server" text="SIP of Rs 100 is due on 4th Oct for goal 1"/>
</tr>
<tr>
<td>
<asp:label CssClass="FieldName" ID="lblSipUnits" runat="server" text="SIP units received for goal 2 for scheme 2"/>
</tr>
</table>