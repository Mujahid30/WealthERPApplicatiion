<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExternalFieldMapping.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.ExternalFieldMapping" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            External Header Mapping
                        </td>
                        <%--<td align="right">
                            <asp:ImageButton ID="btnTradeBusinessDate" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="true" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnTradeBusinessDate_Click"></asp:ImageButton>
                        </td>--%>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="right">
            <asp:Label ID="lblproduct" CssClass="FieldName" runat="server" Text="Select Product"
                valign="top"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPrduct" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                <Items>
                    <asp:ListItem Text="Select" Value="Select" Selected="false" />
                    <asp:ListItem Text="Mutual Funds" Value="MF" />
                    <asp:ListItem Text="Bonds" Value="BO" />
                </Items>
            </asp:DropDownList>
        </td>
        <td align="right">
        </td>
        <td id="tdRandTMF" runat="server">
            <asp:Label ID="lbltoSrt" CssClass="FieldName" runat="server" Text="Select R&T:"></asp:Label>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <%--<asp:DropDownList ID="ddlRandTMF" runat="server" CssClass="cmbField">
                <Items>
                    <asp:ListItem Text="CAMS" Value="CA" />
                    <asp:ListItem Text="KARVY" Value="KV" />
                    <asp:ListItem Text="SUNDARAM" Value="SM" />
                    <asp:ListItem Text="TEMPLETON" Value="TN" />
                </Items>
            </asp:DropDownList>--%>
        </td>
        <%--  <td id="tdRndTBond" runat="server" visible="false">
            <asp:Label ID="Label1" CssClass="FieldName" runat="server" Text="Select R&T:"></asp:Label>
            <asp:DropDownList ID="ddlRndTBond" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="BSE" Value="BE" />
                <asp:ListItem Text="NSC" Value="NC" />
            </asp:DropDownList>
        </td>--%>
        <td align="left">
            <asp:Button ID="btngo" runat="server" Text="Go" CssClass="PCGButton" OnClick="Go_OnClick" />
        </td>
    </tr>
</table>
<asp:Panel ID="AdviserIssueList" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvHeaderMapping" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvHeaderMapping_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="WEEHM_Id" Width="99%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" PageSize="20">
                        <Columns>
                            <telerik:GridBoundColumn DataField="WEIH_ColumnName" UniqueName="WEIH_ColumnName"
                                HeaderText="Internal Column Nmae" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="WEIH_ColumnName"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WEEHM_HeaderName" UniqueName="WEEHM_HeaderName"
                                HeaderText="External Column Nmae" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="WEEHM_HeaderName"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WEEHM_HeaderSequence" UniqueName="WEEHM_HeaderSequence"
                                HeaderText="Header Sequence" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="WEEHM_HeaderSequence"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
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
</asp:Panel>
