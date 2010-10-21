<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdvisorsNote.ascx.cs" Inherits="WealthERP.Customer.CustomerAdvisorsNote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="TableBackground" style="width: 100%">
<tr>
<td class="HeaderCell" colspan="2">
            <asp:Label ID="Label61" runat="server" CssClass="HeaderTextBig" Text="Advisor Note"></asp:Label>
            <hr />
 </td>
</tr>
<tr>
<td colspan="2">

</td>
</tr>
<tr>
<td class="leftField">
<asp:Label ID="lblClassification" Text="Customer Classissification: " CssClass="FieldName" runat="server">
</asp:Label>
</td>
<td class="rightField">
<asp:DropDownList ID="ddlClassification" CssClass="cmbField" runat="server">
</asp:DropDownList>
</td>
</tr>
<tr>
<td class="leftField" style="vertical-align:top;">
<asp:Label ID="lblComments" Text="Enter your comments: " CssClass="FieldName" runat="server">
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
 <asp:Checkbox ID="chkdeactivatecustomer" runat="server" CssClass="txtField" Text="Deactivate Customer"
                AutoPostBack="false"  />
</td>
</tr>
<tr>
<td class="leftField">
</td>
<td class="rightField">
 <asp:Button ID="btnEdit" runat="server" Text="Submit" CssClass="PCGButton" 
                OnClick="btnEdit_Click" />
</td>
</tr>
</table>