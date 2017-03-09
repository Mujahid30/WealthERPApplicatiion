<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageLookups.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.ManageLookups" %>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
    .divViewEdit
    {
        padding-right: 15px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: hand;
    }
</style>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Manage Lookup
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="lbBack" CssClass="LinkButtons" Text="Back" Visible="false"></asp:LinkButton>
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnWERPExport" runat="server" ImageUrl="~/Images/Export_Excel.png"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnWERPExport_Click"></asp:ImageButton>
                                
                            <asp:ImageButton ID="btnMapingExport" runat="server" ImageUrl="~/Images/Export_Excel.png"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnMapingExport_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <%--***********************************************Manage Lookups********************************--%>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lb1Category" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="LookUPSubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblView" runat="server" Text="View:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="right">
            <asp:DropDownList ID="ddlView" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlView_OnSelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="Lookup">Lookup</asp:ListItem>
                <asp:ListItem Value="Mapping">Mapping</asp:ListItem>
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Lookup View" Display="Dynamic" ControlToValidate="ddlView"
                InitialValue="Select" ValidationGroup="LookUPSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<table width="100%" id="tblMap" runat="server">
    <tr id="trSourceType" runat="server" visible="false">
        <td class="leftLabel">
            <asp:Label ID="lb1RTA" runat="server" Text="Source Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlRTA" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlRTA_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Source/RTA"
                CssClass="rfvPCG" ControlToValidate="ddlRTA" ValidationGroup="LookUPSubmit" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
        </td>
        <td class="rightData">
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="LookUPSubmit"
                OnClick="btnGo_Click" />
        </td>
        <td class="rightData">
        </td>
        <td align="right">
        </td>
        <td class="rightData">
        </td>
    </tr>
</table>
<table id="tblwerpGrd" runat="server" width="99%">
    <tr>
        <td class="leftLabel">
        </td>
        <td>
            <asp:Panel ID="Panel2" runat="server" Width="50%" ScrollBars="Horizontal">
                <telerik:RadGrid ID="rgWerp" runat="server" GridLines="Both" AllowPaging="True" ShowFooter="true"
                    PageSize="10" AllowSorting="True" AutoGenerateColumns="false" ShowStatusBar="true"
                    AllowFilteringByColumn="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                    AllowAutomaticUpdates="false" Skin="Telerik" OnNeedDataSource="rgWerp_NeedDataSource"
                    EnableEmbeddedSkins="false" Width="100%" OnItemCommand="rgWerp_ItemCommand">
                    <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="Manage Lookups">
                    </ExportSettings>
                    <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                        EditMode="PopUp" CommandItemSettings-AddNewRecordText="Create New Internal Code Value"
                        DataKeyNames="WCMV_LookupId,WCMV_Name">
                        <Columns>
                            <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                UpdateText="Update">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn UniqueName="WCMV_LookupId" HeaderText="Internal ID" DataField="WCMV_LookupId"
                                SortExpression="WCMV_LookupId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" ItemStyle-Wrap="false">
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WCMV_Name" HeaderText="Internal Code Value"
                                AllowFiltering="true" DataField="WCMV_Name" SortExpression="WCMV_Name" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                Text="Delete">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>
                                <table cellspacing="2" cellpadding="2" width="100%">
                                    <tr>
                                        <td colspan="2" class="tdSectionHeading">
                                            <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                <div class="fltlft" style="width: 250px;">
                                                    &nbsp;
                                                    <asp:Label ID="lblNewWERPname" runat="server" Text="Internal Name"></asp:Label>
                                                    <asp:TextBox ID="txtNewWERPName" runat="server" CssClass="txtField"></asp:TextBox>
                                                    <span id="Span6" class="spnRequiredField">*</span> &nbsp; &nbsp; &nbsp; &nbsp;
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtNewWERPName"
                                                        ErrorMessage="<br />Please Enter a Name" Display="Dynamic" CssClass="rfvPCG"
                                                        runat="server" InitialValue="" ValidationGroup="btnOK">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ValidationGroup="btnOK" ID="btnOK" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                runat="server" CssClass="PCGButton" CausesValidation="True" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                            </asp:Button>&nbsp;
                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                            &nbsp; &nbsp; &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </FormTemplate>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings>
                    </ClientSettings>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tblExtMapGrd" runat="server" width="99%">
    <tr>
        <td class="leftLabel">
        </td>
        <td>
            <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="70%" ScrollBars="Horizontal">
                <telerik:RadGrid ID="rgMaping" runat="server" GridLines="None" ShowFooter="true"
                    AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
                    ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                    AllowFilteringByColumn="true" AllowAutomaticUpdates="false" Skin="Telerik" Width="100%"
                    OnUpdateCommand="rgMaping_UpdateCommand" OnNeedDataSource="rgMaping_NeedDataSource"
                    OnItemDataBound="rgMaping_ItemDataBound" EnableHeaderContextMenu="false" EnableHeaderContextFilterMenu="false">
                    <MasterTableView DataKeyNames="WCMVXM_Id" AllowMultiColumnSorting="True" GroupsDefaultExpanded="false"
                        ExpandCollapseColumn-Groupable="false" AutoGenerateColumns="false" CommandItemDisplay="Top"
                        CommandItemSettings-ShowRefreshButton="false" EditMode="PopUp" GroupLoadMode="Client"
                        CommandItemSettings-AddNewRecordText="Create New External Code Values">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Select" ShowFilterIcon="false" AllowFiltering="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbMap" runat="server" Checked="false" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="WCMV_LookupId" HeaderText="Internal ID" DataField="WCMV_LookupId"
                                SortExpression="WCMV_LookupId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WCMV_Name" HeaderText="Internal Name" DataField="WCMV_Name"
                                SortExpression="WCMV_Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WCMVXM_ExternalCode" HeaderText="External Code"
                                DataField="WCMVXM_ExternalCode" SortExpression="WCMVXM_ExternalCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WCMVXM_ExternalName" HeaderText="External Name"
                                DataField="WCMVXM_ExternalName" SortExpression="WCMVXM_ExternalName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>
                                <table width="75%" cellspacing="2" cellpadding="2">
                                    <tr>
                                        <td class="leftLabel">
                                            <asp:Label ID="lb1InternalName" runat="server" Text="Internal Name:" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="rightData">
                                            <asp:DropDownList ID="ddlWerp" runat="server" CssClass="cmbField">
                                            </asp:DropDownList>
                                            <span id="Span2" class="spnRequiredField">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftLabel">
                                        </td>
                                        <td class="rightData">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfvPCG"
                                                ErrorMessage="Please Select Internal Name" Display="Dynamic" ControlToValidate="ddlWerp"
                                                InitialValue="Select" ValidationGroup="btnOK">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftLabel">
                                            <asp:Label ID="Label3" runat="server" Text="External Code:" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="rightData">
                                            <asp:TextBox ID="txtExternalCode" runat="server" CssClass="txtField"></asp:TextBox>
                                            <span id="Span3" class="spnRequiredField">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
                                                ErrorMessage="Please Enter External Code" Display="Dynamic" ControlToValidate="txtExternalCode"
                                                ValidationGroup="btnOK">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftLabel">
                                            <asp:Label ID="lb1ExtName" runat="server" Text="External Name:" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="rightData">
                                            <asp:TextBox ID="txtNewEXtName" runat="server" CssClass="txtField"></asp:TextBox>
                                            <span id="Span5" class="spnRequiredField">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="rfvPCG"
                                                ErrorMessage="Please Enter External Name" Display="Dynamic" ControlToValidate="txtNewEXtName"
                                                ValidationGroup="btnOK">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftLabel">
                                            <asp:Button ID="btnOK" Text="OK" runat="server" CssClass="PCGButton" CommandName="Update"
                                                CausesValidation="True" ValidationGroup="btnOK" />
                                        </td>
                                        <td class="rightData">
                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </FormTemplate>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings>
                    </ClientSettings>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
        </td>
        <td>
            <asp:Button ID="btnRemoveMaping" CssClass="PCGLongButton" Text="Remove Maping" runat="server"
                OnClientClick="return confirm('Do you want to Remove Maping?');" OnClick="btnRemoveMaping_OnClick" />
        </td>
    </tr>
</table>
<div>
</div>
