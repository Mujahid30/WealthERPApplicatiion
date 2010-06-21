<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserCustomerSMSAlerts.ascx.cs" Inherits="WealthERP.Advisor.AdviserCustomerSMSAlerts" %>
<table width="100%">
<tr><td><asp:Label ID="lblCustomerSMSAlerts" Text="Customer SMS Alerts" CssClass="HeaderTextSmall" runat="server"></asp:Label> </td></tr>
<tr><td>
<table>
<tr>
<td><asp:Label ID="lblLicenceName" Text="SMS Licence Left:" runat="server" CssClass="FieldName"></asp:Label></td>
<td><asp:Label ID="lblLincenceValue" Text="" runat="server" CssClass="Field"></asp:Label></td>
</tr>
</table>
</td></tr>
<tr>

<td>
<asp:Panel ID="pnlCustomerSMSAlerts" runat="server" Height="300px" 
                       Width="100%" ScrollBars="Vertical" Visible="false" HorizontalAlign="Left">
<asp:Label ID="lblNoRecords" Text="No Alert Exists" runat="server" Visible="false" CssClass="FieldName"></asp:Label>
<asp:GridView ID="gvCustomerSMSAlerts" DataKeyNames="CustomerId,AlertId" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Width="624px" Height="78px" 
                        Font-Size="Small" BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg" CssClass="GridViewStyle" 
                        ShowFooter="true" OnRowDataBound="gvCustomerSMSAlerts_RowDataBound">
                        <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                        <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                        <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                        <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <Columns>                      

                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                   <asp:CheckBox ID="chkCustomerSMSAlert" runat="server" Visible="false" CssClass="Field" AutoPostBack="true"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="CustomerName" HeaderText="Customer"  />
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile"  />
                            <asp:BoundField DataField="AlertMessage" HeaderText="Alert Message"  />
                            <asp:BoundField DataField="AlertDate" HeaderText="Alert Date"  />

                        </Columns>
                        
                    </asp:GridView>
                    </asp:Panel>
</td>
</tr>
<tr><td><asp:Button ID="btnSend" Text="Send SMS" runat="server" 
        CssClass="PCGButton" onclick="btnSend_Click" /> </td></tr>
</table>