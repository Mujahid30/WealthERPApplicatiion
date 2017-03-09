<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlertServicesExecution.ascx.cs" Inherits="WealthERP.Alerts.AlertServicesExecution" %>

<Table ID="Table1" runat="server" width="100%" align="center" 
    style="height: 268px">
<tr>

<td class="rightField">
    <asp:Label ID="lblAlertServices" runat="server" CssClass="HeaderTextBig" 
        Text="Alert Services"></asp:Label>
        <hr />
</td>

</tr>
<tr>
<td align="center">
    <asp:Button ID="btnDateService" runat="server" Text="Execute Reminder Service" 
        CssClass="ButtonField" onclick="btnDateService_Click" 
        Width="250px" />
</td>
</tr>
<tr>
<td align="center">
    <asp:Button ID="btnDataConditionService" runat="server" 
        Text="Execute Occurrence Service" CssClass="ButtonField"
        Width="250px" onclick="btnDataConditionService_Click" />
    </td>

</tr>
<tr>
<td align="center">
    <asp:Button ID="btnTransactionService" runat="server" 
        Text="Execute Confirmation Service" CssClass="ButtonField"
        Width="250px" onclick="btnTransactionService_Click" />
    </td>
</tr>
</Table>
