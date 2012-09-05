<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactBusinessOnlineLinks.ascx.cs"
    Inherits="WealthERP.Admin.TransactBusinessOnlineLinks" %>

<script type="text/javascript">
    function CallWindow(URL) {
        window.open(URL, "'_blank'");
        return false;
    }
</script>

<table width="100%">
    <%--<tr>
        <td class="HeaderTextBig" colspan="2">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Transact/Business Links"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Transact/Business Links
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNote" runat="server" Font-Size="Small" CssClass="cmbField" Text="Note: Please Contact customer care if you would like to add or remove links."></asp:Label>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgNoRecords" runat="server" class="failure-msg" align="center">
            </div>
        </td>
    </tr>
</table>
<table width="100%" class="TableBackground">
    <tr>
        <table width="80%" class="TableBackground">
            <td>
                <br />
                <asp:GridView ID="gvAdviserLinks" ShowHeader="true" runat="server" DataKeyNames="AL_LinkId,WLM_Id"
                    AutoGenerateColumns="False" CssClass="GridViewStyle" BorderStyle="None" 
                    Font-Size="Small" HorizontalAlign="Center" EnableViewState="true" OnRowDataBound="gvAdviserLinks_RowDataBound" ShowFooter="true">
                    
                    
                    
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" 
                        VerticalAlign="Top" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Transact/Business Links With Out Pin" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnLinks" ImageUrl='<%# Eval("WLM_LinkImagePath").ToString() %>'
                                    runat="server" />
                                <asp:Label ID="lblURL" Text='<%# Eval("AL_LinkWithOutPin").ToString() %>' runat="server" Visible="false">
                                </asp:Label>
                                <br />
                                <br />
                            </ItemTemplate>
                            <ItemStyle BorderColor="Transparent" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transact/Business Links With Pin" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnLinks1" ImageUrl='<%# Eval("WLM_LinkImagePath").ToString() %>'
                                    runat="server" />
                                <asp:Label ID="lblURL1" Text='<%# Eval("AL_LinkWithPin").ToString() %>' runat="server"
                                    Visible="false">
                                </asp:Label>
                                <br />
                                <br />
                            </ItemTemplate>
                            <ItemStyle BorderColor="Transparent" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </table>
    </tr>
</table>
