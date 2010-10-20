<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdvisorsNote.ascx.cs" Inherits="WealthERP.Customer.CustomerAdvisorsNote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table width="100%">
<tr>
<td class="leftField">
<asp:Label ID="lblClassification" Text="Customer Classissification" CssClass="FieldName" runat="server">
</asp:Label>
</td>
<td class="rightField">
<asp:DropDownList ID="ddlClassification" CssClass="cmbField" runat="server">
</asp:DropDownList>
</td>
</tr>
<tr>
<td class="leftField">
<asp:Label ID="lblComments" Text="Enter your comments" CssClass="FieldName" runat="server">
</asp:Label>
</td>

<td class="rightField">
<asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="174px" 
        Width="441px"></asp:TextBox>
</td>
</tr>
<tr>
<td class="leftField">
</td>
<td class="rightField">
<asp:CheckBox ID="chkDeactiveCustomer" CssClass="FieldName" runat="server" />
<asp:Label ID="lblDeactiveCustomer" CssClass="FieldName" runat="server" Text="Deactivate Customer"></asp:Label>
</td>
</tr>
<tr>
<td class="leftField">
</td>
<td class="rightField">
<asp:Button ID="btnSubmit" Text="Submit" CssClass="PCGButton" runat="server" />
</td>
</tr>
</table>