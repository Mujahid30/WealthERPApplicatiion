<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserCustomerManualSMS.ascx.cs" Inherits="WealthERP.Advisor.AdviserCustomerManualSMS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%">
<tr><td><asp:Label ID="lblCustomerSMS" Text="Customer Manual SMS" CssClass="HeaderTextSmall" runat="server"></asp:Label> </td></tr>
<tr><td>
<table>
<tr>
<td><asp:Label ID="lblLicenceName" Text="SMS Licence Left:" runat="server" CssClass="FieldName"></asp:Label></td>
<td><asp:Label ID="lblLincenceValue" Text="" runat="server" CssClass="Field"></asp:Label></td>
</tr>
<tr>
<td><asp:Label ID="lblMessage" Text="Message:" runat="server" CssClass="FieldName"></asp:Label></td>
<td><asp:TextBox ID="txtMessage" TextMode="MultiLine" Text="" runat="server" 
        Height="53px" Width="269px"></asp:TextBox>
<ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
 TargetControlID="txtMessage" WatermarkText="Enter Your Messsage">
</ajaxToolkit:TextBoxWatermarkExtender>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMessage"
 CssClass="rfvPCG" ErrorMessage="<br />Please Enter Your Message" Display="Dynamic"
 runat="server" InitialValue="" ValidationGroup="btnSend"> 
</asp:RequiredFieldValidator>
</td>
</tr>
</table>
</td></tr>

<tr>

<td>
<asp:Panel ID="pnlCustomerSMSAlerts" runat="server" Height="300px" 
                       Width="100%" ScrollBars="Vertical" Visible="false" HorizontalAlign="Left">
<asp:Label ID="lblNoRecords" Text="No Alert Exists" runat="server" Visible="false" CssClass="FieldName"></asp:Label>
<asp:GridView ID="gvCustomerSMSAlerts" DataKeyNames="CustomerId" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Width="624px" Height="78px" 
                        Font-Size="Small" BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg" CssClass="GridViewStyle" 
                        ShowFooter="true" OnRowDataBound="gvCustomerSMSAlerts_RowDataBound" EnableViewState="true">
                        <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                        <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                        <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                        <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <Columns>                      

                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                   <asp:CheckBox ID="chkCustomerSMSAlert" runat="server" Visible="false" CssClass="Field"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                                    <asp:TextBox ID="txtCustomerSearch" runat="server" CssClass="GridViewtxtField" onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomerManualSMS_btnSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerNameHeader" runat="server" Text='<%# Eval("CustomerName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile" />

                        </Columns>
                        
                    </asp:GridView>
                    </asp:Panel>
</td>
</tr>
<tr><td><asp:Button ID="btnSend" Text="Send SMS" runat="server" 
        CssClass="PCGButton" onclick="btnSend_Click"/> </td></tr>
        <tr><td><asp:Button ID="btnSearch" runat="server" Text=""
    BorderStyle="None" BackColor="Transparent" 
    onclick="btnSearch_Click" /></td></tr>
</table>