<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMProfile.ascx.cs" Inherits="WealthERP.RMProfile" %>

<div style="height: 479px; width: 605px;">
    <table style="width: 605px; height: 476px;">
        <tr>
            <td colspan="2" class="HeaderCell">
                <asp:Label ID="lblTitle" runat="server" Text="RM Profile" CssClass="HeaderTextBig"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Name"></asp:Label>
                <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="(First/Middle/Last)"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Contact Details"></asp:Label>
                <hr />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblISD" runat="server" Text="ISD" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblSTD" runat="server" Text="STD" CssClass="FieldName"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblPhone" runat="server" CssClass="FieldName" Text="PhoneNumber"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblPhoneDirect" runat="server" CssClass="FieldName" Text="Telephone Number Direct"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtPhDirectISD" runat="server" CssClass="txtField" Width="55px"
                    MaxLength="3"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectSTD" runat="server" CssClass="txtField" Width="55px"
                    MaxLength="3"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectPhoneNumber" runat="server" CssClass="txtField" Width="160px"
                    MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Telephone Number Extention"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtPhDirectISD0" runat="server" CssClass="txtField" Width="55px"
                    MaxLength="3"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectSTD0" runat="server" CssClass="txtField" Width="55px"
                    MaxLength="3"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectPhoneNumber0" runat="server" CssClass="txtField" Width="160px"
                    MaxLength="8"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Telephone Number Residence"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtPhDirectISD1" runat="server" MaxLength="3" CssClass="txtField"
                    Width="55px"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectSTD1" runat="server" MaxLength="3" CssClass="txtField"
                    Width="55px"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectPhoneNumber1" runat="server" MaxLength="8" CssClass="txtField"
                    Width="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Fax"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtPhDirectISD2" runat="server" MaxLength="3" CssClass="txtField"
                    Width="55px"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectSTD2" runat="server" MaxLength="3" CssClass="txtField"
                    Width="55px"></asp:TextBox>
                <asp:TextBox ID="txtPhDirectPhoneNumber2" runat="server" MaxLength="8" CssClass="txtField"
                    Width="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Mobile Number"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtMobileNumber" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
    </table>
</div>
