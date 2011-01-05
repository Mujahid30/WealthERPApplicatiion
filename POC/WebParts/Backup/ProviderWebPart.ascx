<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProviderWebPart.ascx.cs" Inherits="ProviderWebPart" %>
<table width="360px" cellpadding="4" cellspacing="0" bgcolor="#ececec">
  <tr>
    <td align="left" valign="top">
      This Web Part is a connection point provider.
      It provides the text typed into the box below to the
      Consumer Web Part.
    </td>
  </tr>
  <tr>
    <td>
      <asp:TextBox ID="TextBox1" MaxLength="16" Runat="server" />
      <asp:Button ID="Button1" Text="Transmit" Runat="server" OnClick="Button1_Click" />
    </td>
  </tr>
</table>
