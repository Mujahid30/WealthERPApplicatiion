<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DematAccountDetails.ascx.cs"
    Inherits="WealthERP.Customer.DematAccountDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<%--<table width="100">
    <tr>
        <td>
            <%--            <asp:GridView ID="gvDematDetails" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                DataKeyNames="CEDA_DematAccountId" CssClass="GridViewStyle" OnPageIndexChanging="gvDematDetails_PageIndexChanging"
                OnSorting="gvDematDetails_Sorting">
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                CssClass="GridViewCmbField">
                                <asp:ListItem Text="Select"></asp:ListItem>
                                <asp:ListItem Text="View"></asp:ListItem>
                                <asp:ListItem Text="Edit"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="DP Name" DataField="CEDA_DPName" />
                    <asp:BoundField HeaderText="Beneficiary Acct No" DataField="CEDA_DPClientId" />
                    <asp:BoundField HeaderText="DP Id" DataField="CEDA_DPId" />
                    <asp:BoundField HeaderText="Depository Name" DataField="CEDA_DepositoryName" />
                    <asp:BoundField HeaderText="Mode of holding" DataField="XMOH_ModeOfHolding" />
                    <asp:BoundField HeaderText="Account Opening Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" DataField="CEDA_AccountOpeningDate" />
                </Columns>
                <FooterStyle CssClass="FooterStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>--%>
     <%--       <div id="Div1" runat="server" style="width: 150%; padding-left: 5px;"
                visible="false">
            </div>
        </td>
    </tr>
</table>--%>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Demat Details
                        </td>
                       <td align="right">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="true" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>

                            <%--<telerik:RadGrid ID="gvDematDetailsTeleR" runat="server" fAllowAutomaticDeletes="false"
                                EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                                ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                                GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                                Style="margin-right: 3px" Width="80%" OnNeedDataSource="gvDematDetailsTeleR_NeedDataSource">--%>
                            <telerik:RadGrid ID="gvDematDetailsTeleR" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvDematDetailsTeleR_NeedDataSource">
                                <%--<HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>--%>
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <MasterTableView DataKeyNames="CEDA_DematAccountId" Width="99%" AllowMultiColumnSorting="True"
                                    AutoGenerateColumns="false">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                            HeaderStyle-Width="140px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                                    AutoPostBack="true" Width="120px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged">
                                                    <Items>
                                                        <asp:ListItem Text="Select" />
                                                        <asp:ListItem Text="View" />
                                                        <asp:ListItem Text="Edit" />
                                                    </Items>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle Width="140px"></HeaderStyle>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CEDA_DPName" UniqueName="CEDA_DPName" HeaderText="DP Name"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true"  SortExpression="CEDA_DPName" AllowFiltering="true" HeaderStyle-Width="67px"
                                            FilterControlWidth="50px" CurrentFilterFunction="Contains" >
                                            <HeaderStyle Width="67px"></HeaderStyle>
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CEDA_DepositoryName" UniqueName="CEDA_DepositoryName"
                                            HeaderText="Depository Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" HeaderStyle-Width="140px" SortExpression="CEDA_DepositoryName"
                                            FilterControlWidth="120px" CurrentFilterFunction="Contains">
                                            <HeaderStyle Width="140px"></HeaderStyle>
                                            <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CEDA_DPClientId" UniqueName="CEDA_DPClientId"
                                            HeaderText="Beneficiary Acct No" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="CEDA_DPClientId" FilterControlWidth="50px"
                                            CurrentFilterFunction="Contains">
                                            <HeaderStyle Width="80px"></HeaderStyle>
                                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CEDA_DPId" UniqueName="CEDA_DPId" HeaderText="DP Id"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                            SortExpression="CEDA_DPId" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                            <%--<HeaderStyle Width="67px"></HeaderStyle>--%>
                                            <%--<ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="CEDA_DepositoryName" UniqueName="CEDA_DepositoryName" HeaderText="Depository Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="140px"
                                SortExpression="Cust_Comp_Name" FilterControlWidth="120px" CurrentFilterFunction="Contains">--%>
                                            <HeaderStyle Width="140px"></HeaderStyle>
                                            <ItemStyle Width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="XMOH_ModeOfHolding" UniqueName="XMOH_ModeOfHolding"
                                            HeaderText="Mode of holding" AutoPostBackOnFilter="true" SortExpression="XMOH_ModeOfHolding"
                                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="145px">
                                            <HeaderStyle Width="145px"></HeaderStyle>
                                            <ItemStyle Width="145px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="CEDA_AccountOpeningDate" UniqueName="CEDA_AccountOpeningDate"
                                            HeaderText="Account Opening Date" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" AllowFiltering="true" HeaderStyle-Width="100px"
                                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="190px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn DataField="CEDA_AccountOpeningDate" UniqueName="CEDA_AccountOpeningDate"
                                            HeaderText="Account Opening Date" AutoPostBackOnFilter="true" SortExpression="CEDA_AccountOpeningDate"
                                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="145px" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle Width="145px"></HeaderStyle>
                                            <ItemStyle Width="145px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" EditImageUrl="Edit.gif"
                                            CancelImageUrl="Cancel.gif">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                              <%--  <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="false" ScrollHeight="380px" />
                                    <ClientEvents OnGridCreated="GridCreated" />
                                    <Resizing AllowColumnResize="true" />
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                                <FilterMenu EnableEmbeddedSkins="true">
                                </FilterMenu>--%>
                            </telerik:RadGrid>



<table width="100%">
    <tr>
        <td width="100%">
            <%--<asp:Label ID="lblDematDetails" runat="server" Text="Demat Details" CssClass="HeaderTextBig"></asp:Label>--%>
            
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr id="lblLifeInsurance" runat="server">
                        <td align="left">
                            
                            &nbsp;</td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
            
        </td>
    </tr>
    <%--    <tr>
        <td>
            <asp:Label ID="lblDematDetails" runat="server" Text="Demat Details" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td>
            <asp:Label ID="lblError" runat="server" Visible="False" CssClass="Error"></asp:Label>
        </td>
    </tr>
</table>
<%--<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblCurrentPage" runat="server" class="Field" Text=""></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblTotalRows" runat="server" class="Field" Text=""></asp:Label>
        </td>
    </tr>
</table>--%>
<table width="100">
    <tr>
        <td>
            <%--            <asp:GridView ID="gvDematDetails" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                DataKeyNames="CEDA_DematAccountId" CssClass="GridViewStyle" OnPageIndexChanging="gvDematDetails_PageIndexChanging"
                OnSorting="gvDematDetails_Sorting">
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                CssClass="GridViewCmbField">
                                <asp:ListItem Text="Select"></asp:ListItem>
                                <asp:ListItem Text="View"></asp:ListItem>
                                <asp:ListItem Text="Edit"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="DP Name" DataField="CEDA_DPName" />
                    <asp:BoundField HeaderText="Beneficiary Acct No" DataField="CEDA_DPClientId" />
                    <asp:BoundField HeaderText="DP Id" DataField="CEDA_DPId" />
                    <asp:BoundField HeaderText="Depository Name" DataField="CEDA_DepositoryName" />
                    <asp:BoundField HeaderText="Mode of holding" DataField="XMOH_ModeOfHolding" />
                    <asp:BoundField HeaderText="Account Opening Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" DataField="CEDA_AccountOpeningDate" />
                </Columns>
                <FooterStyle CssClass="FooterStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>--%>
            <div id="gvDematDetailsTele" runat="server" style="width: 150%; padding-left: 5px;"
                visible="false">
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
