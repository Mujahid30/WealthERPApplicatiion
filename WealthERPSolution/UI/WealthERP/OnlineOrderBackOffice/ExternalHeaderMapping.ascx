<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExternalHeaderMapping.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.ExternalHeaderMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<%--<script type="text/javascript" language="javascript">
    (function() {
        var demo = window.demo = {};
        var grid;

        demo.GridCreated = function(sender) {
            grid = sender;
        };
        demo.HierarchyExpanded = function(sender, args) {
            var firstClientDataKeyName = args.get_tableView().get_clientDataKeyNames()[0];
            //            alert(firstClientDataKeyName + ":'" + args.getDataKeyValue(firstClientDataKeyName) + "' expanded.");
        }

        demo.HierarchyCollapsed = function(sender, args) {
            var firstClientDataKeyName = args.get_tableView().get_clientDataKeyNames()[0];
            //            alert(firstClientDataKeyName + ":'" + args.getDataKeyValue(firstClientDataKeyName) + "' collapsed.");
        }


        demo.ExpandCollapseFirstMasterTableViewItem = function() {
            var firstMasterTableViewRow = grid.get_masterTableView().get_dataItems()[0];
            if (firstMasterTableViewRow.get_expanded()) {
                firstMasterTableViewRow.set_expanded(false);
            }
            else {
                firstMasterTableViewRow.set_expanded(true);
            }
        }
    })();
</script>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            External Header Mapping
                        </td>
                        <td align="right" visible="true">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
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
<table>
    <tr>
        <td align="left">
            <asp:Label ID="lblType" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlMappingType" runat="server" OnSelectedIndexChanged="ddlMappingType_OnSelectedIndexChanged"
                AutoPostBack="true" CssClass="cmbLongField">
                <asp:ListItem Value="0">Select Type</asp:ListItem>
                <asp:ListItem Value="50">Client Modification</asp:ListItem>
                <asp:ListItem Value="51">Profile And Folio</asp:ListItem>
                <asp:ListItem Value="52">MF Transaction</asp:ListItem>
                <asp:ListItem Value="53">MF RTA Unit Recon</asp:ListItem>
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span> </br>
            <asp:RequiredFieldValidator ID="ddlUploadType_RequiredFieldValidator" ControlToValidate="ddlMappingType"
                ValidationGroup="btnViewOrder" ErrorMessage="Please select a type" InitialValue="0"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td align="left" id="tdlblRTA">
            <asp:Label ID="lblRTA" runat="server" Text="Select RTA Type:" Visible="false" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" id="tdddlRTA">
            <asp:DropDownList ID="ddlRTA" Width="270px" AutoPostBack="false" Visible="false"
                runat="server" CssClass="cmbField">
                <asp:ListItem Value="0">Select RTA</asp:ListItem>
                <asp:ListItem Value="CA">CAMS</asp:ListItem>
                <asp:ListItem Value="KA">KARVY</asp:ListItem>
                <asp:ListItem Value="TN">TEMPLTON</asp:ListItem>
                <asp:ListItem Value="SU">SUNDARAM</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ddlRTA_RequiredFieldValidator" ControlToValidate="ddlRTA"
                ValidationGroup="btnViewOrder" ErrorMessage="</br>Please select a RTA type" InitialValue="0"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td align="left">
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnViewOrder"
                OnClick="btnViewOrder_Click" />
        </td>
    </tr>
</table>
<%--<asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="99%" ScrollBars="Horizontal"
    Visible="false">
    <table id="tblHeaderMapping" runat="server">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvHeaderMapping" runat="server" Width="99%" OnNeedDataSource="gvHeaderMapping_OnNeedDataSource"
                                OnItemCommand="gvHeaderMapping_ItemCommand" OnDetailTableDataBind="gvHeaderMapping_DetailTableDataBind" OnItemDataBound="gvHeaderMapping_ItemDataBound"
                                GridLines="None" AutoGenerateColumns="False" PageSize="10" AllowSorting="true"
                                AllowPaging="True" ShowStatusBar="True" ShowFooter="true"  Skin="Telerik" EnableEmbeddedSkins="false"
                                AllowFilteringByColumn="true" AllowAutomaticInserts="false" SortingSettings-SortedDescToolTip="true">
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                <ClientSettings>
                                    <ClientEvents OnGridCreated="demo.GridCreated" OnHierarchyExpanded="demo.HierarchyExpanded"
                                        OnHierarchyCollapsed="demo.HierarchyCollapsed"></ClientEvents>
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="WUXHM_XMLHeaderId" DataKeyNames="WUXHM_XMLHeaderId"
                                    AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false" HierarchyLoadMode="Client">
                                    <DetailTables>
                                        <telerik:GridTableView Name="gvTableview" ClientDataKeyNames="WUXHM_XMLHeaderId"
                                            DataKeyNames="WUXHM_XMLHeaderId" HierarchyLoadMode="Client" AllowSorting="true"
                                            EditMode="PopUp" AutoGenerateColumns="false" CommandItemSettings-AddNewRecordText="Add External Header"
                                            CommandItemDisplay="Top">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="WUXHM_XMLHeaderId" MasterKeyField="WUXHM_XMLHeaderId">
                                                </telerik:GridRelationFields>
                                            </ParentTableRelation>
                                            <Columns>
                                                <telerik:GridBoundColumn SortExpression="WEHXHM_ExternalHeaderName" HeaderText="External Header Name"
                                                    HeaderButtonType="TextButton" DataField="WEHXHM_ExternalHeaderName">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <SortExpressions>
                                                <telerik:GridSortExpression FieldName="WEHXHM_ExternalHeaderName"></telerik:GridSortExpression>
                                            </SortExpressions>
                                            <EditFormSettings EditFormType="Template" PopUpSettings-Height="200px" PopUpSettings-Width="400px">
                                                <FormTemplate>
                                                    <table width="100%" cellspacing="3" cellpadding="3">
                                                        <tr>
                                                            <td class="LeftField" style="width: 1200px;">
                                                                <asp:Label ID="lb1ExHeader" runat="server" Text="External Header Name:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="RightField" style="width: 700px;">
                                                                <asp:TextBox ID="txtExHeader" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="Span14" class="spnRequiredField" runat="server" visible="true">*</span>
                                                                <asp:RequiredFieldValidator runat="server" ID="ReqRuleName" ValidationGroup="rgApllOk"
                                                                    Display="Dynamic" ControlToValidate="txtExHeader" ErrorMessage="<br />Enter External Header Name"
                                                                    Text="" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="RightField">
                                                                <asp:Button ID="btnOK" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' CausesValidation="True"
                                                                    ValidationGroup="rgApllOk"></asp:Button>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                                    CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FormTemplate>
                                            </EditFormSettings>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridBoundColumn SortExpression="WUXFT_XMLFileTypeId" HeaderText="XML File TypeId"
                                            HeaderButtonType="TextButton" DataField="WUXFT_XMLFileTypeId" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="WUXHM_XMLHeaderId" HeaderText="XMLHeader Id"
                                            HeaderButtonType="TextButton" DataField="WUXHM_XMLHeaderId" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="XMLHeaderName" HeaderText="XML Header Name"
                                            HeaderButtonType="TextButton" UniqueName="WUXHM_XMLHeaderName" DataField="WUXHM_XMLHeaderName">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <SortExpressions>
                                        <telerik:GridSortExpression FieldName="WUXHM_XMLHeaderId"></telerik:GridSortExpression>
                                    </SortExpressions>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>--%>
<%--<div>
    <asp:HiddenField ID="hdnHeaderID" runat="server" />
</div>--%>
<asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="99%" ScrollBars="Horizontal"
    Visible="false">
    <table id="tblCommissionStructureRule" runat="server">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvHeaderMapping" runat="server" OnNeedDataSource="gvHeaderMapping_OnNeedDataSource"
                                OnItemCommand="gvHeaderMapping_ItemCommand" OnItemDataBound="gvHeaderMapping_ItemDataBound"
                                GridLines="None" AutoGenerateColumns="False" PageSize="10" AllowSorting="true"
                                AllowPaging="True" ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                                AllowFilteringByColumn="true" AllowAutomaticInserts="false">
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <MasterTableView DataKeyNames="WUXHM_XMLHeaderId,WEHXHM_ExternalHeaderName,WEHXHM_Id" Width="100%" AllowMultiColumnSorting="True"
                                    AutoGenerateColumns="false" EditMode="PopUp" CommandItemSettings-AddNewRecordText="Add External Header"
                                    CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                            UpdateText="Update">
                                        </telerik:GridEditCommandColumn>
                                        <%-- <telerik:GridTemplateColumn AllowFiltering="false">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="AllDetailslink" runat="server" CommandName="ExpandAllCollapse"
                                                    Font-Underline="False" Font-Bold="true" UniqueName="AllDetailslink" Font-Size="Medium"
                                                    OnClick="btnExpand_Click">+</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                    Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" Visible="false">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbViewDetails" runat="server" CommandName="ViewDetails" Font-Underline="true"
                                                    Font-Bold="false" UniqueName="ViewDetailslink" Text="View" Visible="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                        <telerik:GridBoundColumn SortExpression="WUXFT_XMLFileTypeId" HeaderText="Internal File Type Id"
                                            HeaderButtonType="TextButton" ShowFilterIcon="false" DataField="WUXFT_XMLFileTypeId"
                                            Visible="true" AllowFiltering="true" HeaderStyle-Width="90px" FilterControlWidth="70px"
                                            CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="WUXHM_XMLHeaderId" HeaderText="Internal Header Id"
                                            HeaderButtonType="TextButton" DataField="WUXHM_XMLHeaderId" Visible="true" AllowFiltering="true"
                                            HeaderStyle-Width="90px" FilterControlWidth="70px" CurrentFilterFunction="EqualTo"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="XMLHeaderName" HeaderText="Internal Header Name"
                                            HeaderButtonType="TextButton" UniqueName="WUXHM_XMLHeaderName" DataField="WUXHM_XMLHeaderName"
                                            AllowFiltering="true" HeaderStyle-Width="90px" FilterControlWidth="70px" CurrentFilterFunction="EqualTo"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="ExternalHeaderName" HeaderText="External Header Name"
                                            HeaderButtonType="TextButton" UniqueName="WEHXHM_ExternalHeaderName" DataField="WEHXHM_ExternalHeaderName"
                                            AllowFiltering="true" HeaderStyle-Width="90px" FilterControlWidth="70px" CurrentFilterFunction="EqualTo"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                            Text="Delete">
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template" PopUpSettings-Height="200px" PopUpSettings-Width="400px">
                                        <FormTemplate>
                                            <table width="100%" cellspacing="3" cellpadding="3">
                                                <tr>
                                                    <td class="LeftField" style="width: 130px;">
                                                        <asp:Label ID="lb1ExHeader" runat="server" Text="External Header Name:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="RightField" style="width: 150px;">
                                                        <asp:TextBox ID="txtExHeader" runat="server" CssClass="txtField"></asp:TextBox>
                                                        <span id="Span14" class="spnRequiredField" runat="server" visible="true">*</span>
                                                        <asp:RequiredFieldValidator runat="server" ID="ReqRuleName" CssClass="rfvPCG" ValidationGroup="rgApllOk"
                                                            Display="Dynamic" ControlToValidate="txtExHeader" ErrorMessage="<br />Enter External Header Name"
                                                            Text="" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="LeftField" style="width: 130px;">
                                                        <asp:Label ID="Label10" runat="server" Text="XML Header Name:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="RightField" style="width: 180px;">
                                                        <asp:DropDownList ID="ddlXMLHeaderName" runat="server" CssClass="cmbField" AutoPostBack="true"
                                                            Width="205px">
                                                        </asp:DropDownList>
                                                        <span id="Span12" class="spnRequiredField">*</span>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Xml Header Name"
                                                            CssClass="rfvPCG" ControlToValidate="ddlXMLHeaderName" ValidationGroup="rgApllOk"
                                                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="RightField">
                                                        <asp:Button ID="btnOK" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                            Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' CausesValidation="True"
                                                            ValidationGroup="rgApllOk"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                            CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                    <%--<telerik:GridTemplateColumn AllowFiltering="false">
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%">
                                                        <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                            Width="100%" ScrollBars="Both" Visible="false">
                                                            <telerik:RadGrid ID="gvHeaderMappingDetails" runat="server" AutoGenerateColumns="False"
                                                                enableloadondemand="True" PageSize="10" AllowPaging="false" EnableEmbeddedSkins="False"
                                                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                                                Skin="Telerik" AllowFilteringByColumn="false">
                                                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="WUXHM_XMLHeaderId"
                                                                    AutoGenerateColumns="false" Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn SortExpression="WEHXHM_ExternalHeaderName" HeaderText="External Header Name"
                                                                            HeaderButtonType="TextButton" DataField="WEHXHM_ExternalHeaderName">
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnPrevEHName" runat="server" />