<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="WealthERP.Pager" %>
<table id="tblPager" width="100%" cellpadding="0" cellspacing="0" border="0px">
    <tr>
        <td align="center" valign="middle">
            <table cellpadding="0" cellspacing="0" style="width: 175px">
                <tr>
                    <td align="right" valign="middle" style="padding-top: 4px">
                        <asp:ImageButton ID="btnFirst" runat="server" ImageUrl="~/Images/ImgFirstMD.gif"
                            OnClick="btnFirst_Click" Height="16px" Width="15px" />
                    </td>
                    <td align="right" valign="middle" style="padding-top: 4px">
                        <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="~/Images/ImgPrevMD.gif" OnClick="btnPrev_Click"
                            Height="16px" Width="15px" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtPageNo" MaxLength="10" Style="height: 16px; text-align: center;
                            font-size: 10px" OnTextChanged="txtPageNo_TextChanged" AutoPostBack="true" Width="75px"></asp:TextBox>
                    </td>
                    <td align="left" valign="middle" style="padding-top: 4px">
                        <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Images/ImgNextMN.gif" OnClick="btnNext_Click"
                            Height="16px" Width="15px" />
                    </td>
                    <td align="left" valign="middle" style="padding-top: 4px">
                        <asp:ImageButton ID="btnLast" runat="server" ImageUrl="~/Images/ImgLastMN.gif" OnClick="btnLast_Click"
                            Height="16px" Width="15px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<input id="currentPage" runat="server" type="hidden" value="1" />
<input id="pageCount" runat="server" type="hidden" value="1" />
<input id="pageSize" runat="server" type="hidden" value="1" />