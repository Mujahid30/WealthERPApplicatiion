<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="WealthERP.OnlineOrder.Banner" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ OutputCache Duration="3600" VaryByParam="None" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

<%--
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>--%>

<script type="text/jscript">
    function SetBannerImageDetails(id) {
        $('.bxslider').html(id);
        if (id == "") {

            $('#tblSlider').css("display", "none");
        }
       
    }
    jQuery(document).ready(function($) {
        $('.bxslider').bxSlider(
    {
        auto: true,
        autoControls: true
    }
    );
});

        
</script>

<table  id="tblSlider" width="100%" style="padding: 25px;">
    <tr>
        <td align="center">
            <div style="float: left; width: 98%">
                <ul class="bxslider">
                </ul>
            </div>
        </td>
    </tr>
</table>
