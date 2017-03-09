<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRepository.ascx.cs"
    Inherits="WealthERP.Admin.ViewRepository" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<style type="text/css">
    option
    {
        text-decoration: underline;
        color:Blue;
        cursor:pointer;
        cursor:hand;        
    }
</style>

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Repository"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr id="trNoRecords" runat="server">
        <td align="center">
            <div id="divNoRecords" runat="server" class="failure-msg">
                <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
    <tr id="trContent" runat="server">
        <td>
            <div style="text-align: center">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblCategory1" runat="server" Visible="false" CssClass="HeaderTextBig"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCategory2" runat="server" Visible="false" CssClass="HeaderTextBig"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="ListBox1" runat="server" Visible="false" Width="300px" Height="150px"
                                OnSelectedIndexChanged="ListBox_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>

                        </td>
                        <td>
                            <asp:ListBox ID="ListBox2" runat="server" Visible="false" Width="300px" Height="150px"
                                OnSelectedIndexChanged="ListBox_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCategory3" runat="server" Visible="false" CssClass="HeaderTextBig"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCategory4" runat="server" Visible="false" CssClass="HeaderTextBig"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="ListBox3" runat="server" Visible="false" Width="300px" Height="150px"
                                OnSelectedIndexChanged="ListBox_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                        </td>
                        <td>
                            <asp:ListBox ID="ListBox4" runat="server" Visible="false" Width="300px" Height="150px"
                                OnSelectedIndexChanged="ListBox_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
