<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMEditProfile.ascx.cs"
    Inherits="WealthERP.Advisor.RMEditProfile" %>
<div style="height: 479px; width: 627px;">
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Edit Profile"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

    <table style="width: 100%; height: 476px;">
        <tr>
            <td  colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="Name"></asp:Label>
                <asp:Label ID="lblRMName" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="(First/Middle/Last)"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" Width="90px"></asp:TextBox>
                <asp:TextBox ID="txtMiddleName" runat="server" Width="90px"></asp:TextBox>
                <asp:TextBox ID="txtLastName" runat="server" Width="90px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="Contact Details"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblISD" runat="server" Text="ISD" Font-Bold="True" Font-Names="Arial"
                    Font-Size="X-Small"></asp:Label>
                <asp:Label ID="lblSTD" runat="server" Text="STD" Font-Bold="True" Font-Names="Arial"
                    Font-Size="X-Small"></asp:Label>
                <asp:Label ID="lblPhone" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="PhoneNumber"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPhoneDirect" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="X-Small" Text="Telephone Number Direct"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhDirectISD" runat="server" Width="55px" MaxLength="3"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectSTD" runat="server" Width="55px" MaxLength="3"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectPhoneNumber" runat="server" Width="160px" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="Telephone Number Extention"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhDirectISD0" runat="server" Width="55px" MaxLength="3"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectSTD0" runat="server" Width="55px" MaxLength="3"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectPhoneNumber0" runat="server" Width="160px" MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="Telephone Number Residence"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhDirectISD1" runat="server" MaxLength="3" Width="55px"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectSTD1" runat="server" MaxLength="3" Width="55px"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectPhoneNumber1" runat="server" MaxLength="8" Width="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="Fax"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhDirectISD2" runat="server" MaxLength="3" Width="55px"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectSTD2" runat="server" MaxLength="3" Width="55px"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectPhoneNumber2" runat="server" MaxLength="8" Width="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="Mobile Number"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMobileNumber" runat="server" Width="274px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                    Text="Email Id"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="274px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="2">
                <asp:Button ID="btnSaveChanges" runat="server" Font-Bold="True" OnClick="btnSaveChanges_Click"
                    Text="Save Changes" Width="117px" />
            </td>
        </tr>
    </table>
</div>
