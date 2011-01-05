<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProgrammableWebPart.ascx.cs" Inherits="ProgrammableWebPart" %>

<table width="360px" cellpadding="4" cellspacing="0" bgcolor="#ececec">
  <tr>
    <td align="left" valign="top">
      This Web Part implements a public property named Text.
      The value of that property is shown below.
    </td>
  </tr>
  <tr>
    <td>
      <span style="font-size: 14pt; font-weight: bold">
        <asp:Label ID="Label1" Runat="server" />
      </span>
    </td>
  </tr>
</table>
