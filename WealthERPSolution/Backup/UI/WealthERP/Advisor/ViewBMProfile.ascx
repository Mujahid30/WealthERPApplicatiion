<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBMProfile.ascx.cs"
    Inherits="WealthERP.Advisor.ViewBMProfile" %>

<table class="TableBackground">
    <tr>
        <td  colspan="2" >
            &nbsp;
        </td>
    </tr>
    <tr>
        <td  colspan="2" >
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label9" runat="server" CssClass="HeaderTextBig" 
                Text="View BM Profile"></asp:Label>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td  colspan="2" >
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name "></asp:Label>
            &nbsp;
        </td>
        <td class="rightField" >
            <asp:Label ID="lblRMName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" 
                Text="Contact Details"></asp:Label>
                <hr />
        </td>
        
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="lblPhoneDirectNumber" runat="server" CssClass="FieldName" Text="Telephone Number Direct"></asp:Label>
        </td>
        <td class="rightField" >
            <asp:Label ID="lblPhDirect" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Telephone Number Extention"></asp:Label>
        </td>
        <td class="rightField" >
            <asp:Label ID="lblPhExt" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Telephone Number Residence"></asp:Label>
        </td>
        <td class="rightField" >
            <asp:Label ID="lblPhResi" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Fax"></asp:Label>
        </td>
        <td class="rightField" >
            <asp:Label ID="lblFax" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Mobile Number"></asp:Label>
        </td>
        <td class="rightField" >
            <asp:Label ID="lblMobile" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id"></asp:Label>
        </td>
        <td class="rightField" >
            <asp:Label ID="lblMail" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style3" >
            &nbsp;
        </td>
        <td class="style5" >
            &nbsp;
        </td>
    </tr>
</table>
