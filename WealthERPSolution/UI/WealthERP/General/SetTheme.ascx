<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetTheme.ascx.cs" Inherits="WealthERP.General.SetTheme" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>


<%--<table width="100%" class="TableBackground">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">Theme</td>
        <td  align="right"style="padding-bottom:2px;">
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
    <tr>
        <td>
            <asp:Label ID="lblSelect" runat="server" Text="Select Theme :" CssClass="FieldName"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlTheme" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlTheme_SelectedIndexChanged">
                <asp:ListItem Value="0">Select a Theme</asp:ListItem>
                <asp:ListItem Value="BlackAndWhite">Black & white</asp:ListItem>
                <asp:ListItem Value="Blue">Blue</asp:ListItem>
                <asp:ListItem Value="Desert">Desert</asp:ListItem>
                <asp:ListItem Value="Green">Green</asp:ListItem>
                <asp:ListItem Value="Maroon">Maroon</asp:ListItem>
                <asp:ListItem Value="Purple">Purple</asp:ListItem>
                <asp:ListItem Value="Yellow">Yellow</asp:ListItem>
                 <asp:ListItem Value="LightPurple">LightPurple</asp:ListItem>
                
            </asp:DropDownList>
        </td>
    </tr>
</table>--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td style="width: 20%">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblTeamList" runat="server" Text="Team :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTeamList" AutoPostBack="true" CssClass="cmbField"
                                    OnSelectedIndexChanged="ddlTeamList_SelectedIndexChanged">
                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                    <asp:ListItem Value="Sales">Sales</asp:ListItem>
                                    <asp:ListItem Value="Branch">Branch</asp:ListItem>
                                    <asp:ListItem Value="RM">RM</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 80%">
                    <table width="100%">
                        <tr id="trSales" runat="server">
                            <td>
                                <asp:Label ID="lblSearchOn" runat="server" Text="Search On :" CssClass="FieldName"></asp:Label>
                     <%--       </td>
                            <td runat="server" align="left">--%>
                                <asp:DropDownList runat="server" ID="ddlSelectChannelOrTitle" AutoPostBack="true" CssClass="cmbField"
                                    OnSelectedIndexChanged="ddlSelectChannelOrTitle_SelectedIndexChanged">
                                     <asp:ListItem Value="0">---Select---</asp:ListItem>
                                    <asp:ListItem Value="Channel">Channel</asp:ListItem>
                                    <asp:ListItem Value="Title">Title</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            
                            <td id="tdChannelList" runat="server">
                                <asp:Label ID="lblChannelList" runat="server" Text="Channel :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td id="tdChannelListDDL" runat="server">
                                <asp:DropDownList runat="server" ID="ddlChannelList" AutoPostBack="true" CssClass="cmbField"
                                    OnSelectedIndexChanged="ddlChannelList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td id="tdTitleList" runat="server">
                                <asp:Label ID="lblTitle" runat="server" Text="Title :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td id="tdTitleListDDL" runat="server">
                                <asp:DropDownList runat="server" ID="ddlTitlesList" AutoPostBack="true" CssClass="cmbField"
                                    OnSelectedIndexChanged="ddlTitlesList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td id="tdSubBrokerCodeList" runat="server">
                                <asp:Label ID="lblSubBrokerCode" runat="server" Text="Name :" CssClass="FieldName"></asp:Label>
                            </td >
                            <td id="tdSubBrokerCodeListDDL" runat="server">
                                <asp:DropDownList runat="server" ID="ddlSubBrokerCodeList" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trBranch" runat="server">
                         <td align ="left" style="width:50px">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList runat="server" ID="ddlBranch" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trRM" runat="server">
                         <td align ="left" style="width:50px">
                                <asp:Label ID="lblRM" runat="server" Text="RM:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlRM" CssClass="cmbField" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
     
    </ContentTemplate>
</asp:UpdatePanel>
