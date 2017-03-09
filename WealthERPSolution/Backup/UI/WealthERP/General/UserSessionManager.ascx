<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSessionManager.ascx.cs"
    Inherits="WealthERP.General.UserSessionManager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            User Session Manager
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <td>
        <td>
        &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;
        </td>
    </td>
</table>
<table width="40%" runat="server" visible="true" id="tblPasswordSection" style="padding-top: 20px;padding-left:100px;">
    <tr>
        <td align="right">
            <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtField" TextMode="Password"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="rfvtxtPassword" ValidationGroup="btnSubmit" ControlToValidate="txtPassword"
                ErrorMessage="Please enter password" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="btnSubmit"
                CssClass="PCGButton" OnClick="btnSubmit_Click" />
        </td>
    </tr>
    <tr id="trPassword" runat="server" visible="false">
        <td colspan="2">
            <asp:Label ID="Label1" runat="server" Text="Password does not match " CssClass="Error"></asp:Label>
        </td>
    </tr>
</table>
<table width="70%" runat="server" visible="false" id="tblUserSessionList" style="padding-left:5px;">
    <tr>
        <td>
            <telerik:RadGrid ID="RadGridSessionManager" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="102%" ClientSettings-AllowColumnsReorder="true"
                AllowAutomaticInserts="false" OnNeedDataSource="RadGridSessionManager_OnNeedDataSource"
                OnItemCommand="RadGridSessionManager_OnItemCommand">
                <%--  OnNeedDataSource="gvOrderList_OnNeedDataSource" OnItemDataBound="gvOrderList_ItemDataBound"--%>
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                </ExportSettings>
                <MasterTableView DataKeyNames="U_UserId" AllowFilteringByColumn="true" Width="102%"
                    AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                        ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="U_LoginId" HeaderText="LoginId" AllowFiltering="true"
                            SortExpression="U_LoginId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true" UniqueName="U_LoginId" FooterStyle-HorizontalAlign="Left"
                            HeaderStyle-Width="300px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UserName" HeaderText="Name" AllowFiltering="true"
                            SortExpression="UserName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true" UniqueName="UserName" FooterStyle-HorizontalAlign="Left"
                            HeaderStyle-Width="300px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn ItemStyle-Width="60px" AllowFiltering="false" HeaderText="Action"
                            Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnUserLogOff" runat="server" CommandName="View" ImageUrl="~/Images/User_LogOut.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Resizing AllowColumnResize="true" />
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
