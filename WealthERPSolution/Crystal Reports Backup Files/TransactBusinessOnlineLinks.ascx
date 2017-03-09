<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactBusinessOnlineLinks.ascx.cs"
    Inherits="WealthERP.Admin.TransactBusinessOnlineLinks" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $(".flip").click(function() { $(".panel").slideToggle(); });
    });
</script>

<link href="../App_Themes/Maroon/Images/bubbletip.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/Maroon/Images/bubbletip-IE.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function CallWindow(URL) {
        window.open(URL, "'_blank'");
        return false;
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Transact/Business Links
                        </td>
                        <td align="right">
                            <img src="../Images/helpImage.png" height="15px" width="20px" style="float: right;"
                                class="flip" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    Please Contact customer care if you would like to add or remove links.
                </p>
            </div>
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
                    AutoGenerateColumns="False" CssClass="GridViewStyle" BorderStyle="None" Font-Size="Small"
                    HorizontalAlign="Center" EnableViewState="true" OnRowDataBound="gvAdviserLinks_RowDataBound"
                    ShowFooter="true">
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Transact/Business Links With Out Pin" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnLinks" ImageUrl='<%# Eval("WLM_LinkImagePath").ToString() %>'
                                    runat="server" />
                                <asp:Label ID="lblURL" Text='<%# Eval("AL_LinkWithOutPin").ToString() %>' runat="server"
                                    Visible="false">
                                </asp:Label>
                                <br />
                                <br />
                            </ItemTemplate>
                            <ItemStyle BorderColor="Transparent" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transact/Business Links With Pin" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-VerticalAlign="Middle">
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
