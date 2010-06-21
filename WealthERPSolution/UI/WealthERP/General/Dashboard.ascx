<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="WealthERP.General.Dashboard" %>
<link href="/CSS/Dashboard.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script src="/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="/Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="/Scripts/Dashboard.js" type="text/javascript"></script>
<script language="javascript">
    var SortOrderId = '<%=SortOrder.ClientID %>';
</script>
<asp:Label ID="lblError" runat="server" Text="lblError" Visible="False"></asp:Label>
<div id="CLink" class="cLink">
    <a href="#">Customize</a></div>
<div runat="server" id="CPanel" class="cPanel">
    <h2 class="cPanelHeader">
        Customize</h2>
    <div class="cPanelBody">
        <asp:CheckBoxList ID="PartList" class="PartList" runat="server" RepeatColumns="3"
            RepeatDirection="Horizontal">
        </asp:CheckBoxList>
        <p />
        <div align="right">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnClose" runat="server" Text="Close" class="btnClose" />
        </div>
    </div>
</div>
<div runat="server" id="BPanel" class="bPanel">
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" OnLoad="UpdatePanel1_Load">
    <ContentTemplate>
        <asp:HiddenField ID="SortOrder" runat="server" Value=""/>
    </ContentTemplate>
</asp:UpdatePanel>
