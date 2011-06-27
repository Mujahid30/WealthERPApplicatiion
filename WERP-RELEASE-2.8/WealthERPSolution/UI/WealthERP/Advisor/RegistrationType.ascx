<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationType.ascx.cs" Inherits="WealthERP.Advisor.RegistrationType" %>

<table style="width:100%;">
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="lblTitle" runat="server" Text="Registration Type" CssClass="HeaderTextBig" ></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:RadioButton ID="rbtnAdviser" Text="Register as an Adviser" GroupName="grpType" CssClass="cmbField" runat="server" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
             <asp:RadioButton ID="rbtnDirectInvestor" Text="Register as a Direct Investor" GroupName="grpType" CssClass="cmbField" runat="server" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" 
                onclick="btnSubmit_Click" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>

