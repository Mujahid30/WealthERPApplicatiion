<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsuranceIssueList.ascx.cs"
    Inherits="WealthERP.OfflineOrderBackOffice.InsuranceIssueList" %>
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
                            View Policies
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" Height="22px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblType" runat="server" CssClass="FieldName" Text="Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Life Insurance" Value="IN"></asp:ListItem>
                <asp:ListItem Text="General Insurance" Value="GI"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlType"
                ErrorMessage="Select Type" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td style="text-align: right">
            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server"  CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlCategory"
                ErrorMessage="Select Category" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="lblStatus" runat="server" CssClass="FieldName" Text="Staus:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="cmbField">
                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                <asp:ListItem Text="Close" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" OnClick="btnGo_OnClick" Text="Go" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="99%" ScrollBars="Horizontal"
    Visible="false">
    <table id="tblCommissionStructureRule" runat="server">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvInsuranceList" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="gvInsuranceList_OnNeedDataSource"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" Width="120%" Height="400px">
                                <MasterTableView DataKeyNames="IS_SchemeId" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    AllowFilteringByColumn="true" EditMode="PopUp">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                            HeaderStyle-Width="110px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                                    Width="110px">
                                                    <Items>
                                                        <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                                        <asp:ListItem Text="View" Value="View" />
                                                        <asp:ListItem Text="Edit" Value="Edit" />
                                                    </Items>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="IS_SchemeName" SortExpression="IS_SchemeName" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Policy Name" UniqueName="IS_SchemeName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="XII_InsuranceIssuerName" AllowFiltering="true" HeaderText="Issuer"
                                            UniqueName="XII_InsuranceIssuerName" SortExpression="XII_InsuranceIssuerName" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                            AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="75px"
                                            DataType="System.Int64">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" AllowFiltering="true" HeaderText="Category"
                                            UniqueName="PAIC_AssetInstrumentCategoryName" SortExpression="PAIC_AssetInstrumentCategoryName" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                            FilterControlWidth="75px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" SortExpression="PAISC_AssetInstrumentSubCategoryName" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Sub Category" UniqueName="PAISC_AssetInstrumentSubCategoryName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="IsActive" SortExpression="IsActive" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="IsActive" UniqueName="IsActive">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IS_OpenDate" SortExpression="IS_OpenDate" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Start Date" UniqueName="IS_OpenDate">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IS_CloseDate" SortExpression="IS_CloseDate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Close Date" UniqueName="IS_CloseDate">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IS_CreatedBy" SortExpression="IS_CreatedBy"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Create By" UniqueName="IS_CreatedBy">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IS_CreatedOn" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            AllowFiltering="true" HeaderText="Create On" UniqueName="IS_CreatedOn" SortExpression="IS_CreatedOn"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            HeaderStyle-Width="120px" FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IS_ModifiedBy" SortExpression="IS_ModifiedBy"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Modified By" UniqueName="IS_ModifiedBy">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IS_ModifiedOn" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            AllowFiltering="true" HeaderText="Modified On" UniqueName="IS_ModifiedOn" SortExpression="IS_ModifiedOn"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            HeaderStyle-Width="120px" FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
