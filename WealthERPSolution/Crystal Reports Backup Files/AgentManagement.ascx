<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgentManagement.ascx.cs"
    Inherits="WealthERP.UserManagement.AgentManagement" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        var totalChkBoxes = parseInt('<%= gvAssoMgt.Items.Count %>');
        var gvControl = document.getElementById('<%= gvAssoMgt.ClientID %>');


        var gvChkBoxControl = "cbRecons";


        var mainChkBox = document.getElementById("chkBxWerpAll");

        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<table width="100%">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Associate User Management
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="success-msg" id="SuccessMsg" runat="server" visible="false" align="center">
            </div>
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="tbgvMIS" runat="server" class="Landscape" Width="100%" Height="80%">
    <table width="100%">
        <tr>
            <td>
                <div id="dvAssoMgt" runat="server" style="width: 840px;">
                    <telerik:RadGrid ID="gvAssoMgt" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="gvAssoMgt_OnNeedDataSource"
                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="U_userId" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblchkBxSelect" runat="server"></asp:Label>
                                        <input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxes()" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="AA_ContactPersonName" HeaderText="Associate Name" AllowFiltering="true"
                                    SortExpression="AA_ContactPersonName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="AA_ContactPersonName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="AAC_AgentCode" HeaderText="Sub Broker Code" AllowFiltering="true"
                                    SortExpression="AAC_AgentCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="AAC_AgentCode" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="U_userId" HeaderText="UserId" SortExpression="U_userId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="U_userId" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="U_LoginId" HeaderText="Login Id" SortExpression="U_LoginId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="U_LoginId" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AA_Email" HeaderText="Email Id" SortExpression="AA_Email"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AA_Email" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReset" runat="server" CssClass="cmbFielde" Text="Reset Password"
                                            OnClick="lnkReset_Click">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid></div>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
<table>
    <tr>
        <td>
            <asp:Button ID="btnGenerate" runat="server" Text="Reset & Send Login Details" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMUserDetails_btnGenerate', 'L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMUserDetails_btnGenerate', 'L');"
                CssClass="loadme PCGLongButton" onclick="btnGenerate_Click" />
        </td>
    </tr>
</table>
