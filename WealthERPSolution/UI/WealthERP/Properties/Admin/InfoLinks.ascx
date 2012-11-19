<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfoLinks.ascx.cs" Inherits="WealthERP.Admin.InfoLinks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Info Links
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
        <td>
            <br />
            <asp:GridView ID="gvAdviserLinks" ShowHeader="false" runat="server" DataKeyNames="AL_LinkId"
                AutoGenerateColumns="False" BorderStyle="None" BorderColor="Transparent" Font-Size="Small"
                HorizontalAlign="Center" EnableViewState="true" OnRowCommand="gvAdviserLinks_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkLinks" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                Text='<%# Eval("WLM_AltLinkName").ToString() %>' CommandName="NavigateToLink"></asp:LinkButton>
                            <br />
                            <br />
                        </ItemTemplate>
                        <ItemStyle BorderColor="Transparent" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
