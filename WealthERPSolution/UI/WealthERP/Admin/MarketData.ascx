<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarketData.ascx.cs" Inherits="WealthERP.Admin.MarketData" %>
<table width="100%">
<tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr>
<tr align="center">
<td>
<asp:LinkButton  ID="lnkMFData" CssClass="HeaderTextBig" runat="server" 
        Text="Query MF Data" onclick="lnkMFData_Click" ></asp:LinkButton>
        
</td>
</tr>
<tr><td> </td></tr><tr><td> </td></tr><tr><td> </td></tr><tr><td> </td></tr>
<tr align="center">
<td>
<asp:LinkButton ID="lnkEquityData" CssClass="HeaderTextBig" runat="server" 
        Text="Query Equity Data" onclick="lnkEquityData_Click"></asp:LinkButton>
</td>
</tr>
</table>