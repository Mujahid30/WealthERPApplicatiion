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
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    Manage Lookup
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
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                        CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnAdd" Display="Dynamic"
                        InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblView" runat="server" Text="View:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="right">
                    <asp:DropDownList ID="ddlView" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlView_OnSelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="Select">Select</asp:ListItem>
                        <asp:ListItem Value="1">Lookup</asp:ListItem>
                        <asp:ListItem Value="2">Mapping</asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select Lookup View" Display="Dynamic" ControlToValidate="ddlView"
                        InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table id="tblwerpGrd" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="90%" ScrollBars="Horizontal">
                        <telerik:RadGrid ID="rgWerp" runat="server" CssClass="RadGrid" GridLines="Both" AllowPaging="True"
                            PageSize="10" AllowSorting="True" AutoGenerateColumns="false" ShowStatusBar="true"
                            AllowFilteringByColumn="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                            AllowAutomaticUpdates="false" Skin="Telerik" OnNeedDataSource="rgWerp_NeedDataSource"
                            OnItemCommand="rgWerp_ItemCommand" Width="100%" OnUpdateCommand="rgWerp_UpdateCommand">
                            <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="CommissionStructureRule">
                            </ExportSettings>
                            <MasterTableView CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                                EditMode="PopUp" CommandItemSettings-AddNewRecordText="Create New Internal Code Value"
                                DataKeyNames="WCMV_LookupId,WCMV_Name">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="WCMV_LookupId" HeaderText="Internal ID" DataField="WCMV_LookupId"
                                        SortExpression="WCMV_LookupId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="true">
                                        <HeaderStyle></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WCMV_Name" HeaderText="Internal Code Value"
                                        DataField="WCMV_Name" SortExpression="WCMV_Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        AutoPostBackOnFilter="true">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table cellspacing="2" cellpadding="2" width="100%">
                                            <tr>
                                                <td colspan="5" class="tdSectionHeading">
                                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                        <div class="fltlft" style="width: 200px;">
                                                            &nbsp;
                                                            <asp:Label ID="lblNewWERPname" runat="server" Text="Internal Name"></asp:Label>
                                                            <asp:TextBox ID="txtNewWERPName" runat="server" CssClass="txtField"></asp:TextBox>
                                                            <span id="Span6" class="spnRequiredField">*</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtNewWERPName"
                                                                ErrorMessage="<br />Please enter a Name" Display="Dynamic" CssClass="rfvPCG"
                                                                runat="server" InitialValue="" ValidationGroup="Button1">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:Button ID="Button1" Text="OK" runat="server" CssClass="PCGButton" CommandName="Update" />
                                                            <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                                CommandName="Cancel"></asp:Button>
                                                        </div>
                                                    </div>
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
        <table width="100%" id="tblMap" runat="server">
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lb1RTA" runat="server" Text="Source Type:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlRTA" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRTA_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Source/RTA"
                        CssClass="rfvPCG" ControlToValidate="ddlRTA" ValidationGroup="btnAdd" Display="Dynamic"
                        InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                </td>
                <td class="rightData">
                </td>
            </tr>
        </table>
        <table id="tblExtMapGrd" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="90%" ScrollBars="Horizontal">
                        <telerik:RadGrid ID="rgMaping" runat="server" CssClass="RadGrid" GridLines="Both"
                            AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
                            ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                            AllowFilteringByColumn="true" AllowAutomaticUpdates="false" Skin="Telerik" Width="100%"
                            OnUpdateCommand="rgMaping_UpdateCommand" OnNeedDataSource="rgMaping_NeedDataSource"
                            OnItemDataBound="rgMaping_ItemDataBound">
                            <MasterTableView DataKeyNames="WCMVXM_Id" Width="120%" AllowMultiColumnSorting="True"
                                AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                                EditMode="PopUp" CommandItemSettings-AddNewRecordText="Create New External Code Values">
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
                                            <%-- <tr>
                                                <td class="leftLabel">
                                                </td>
                                                <td class="rightData">
                                                </td>
                                            </tr>--%>
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
                <td>
                    <asp:Button ID="Button3" CssClass="PCGLongButton" Text="Remove Maping" runat="server"
                        OnClick="Button3_OnClick" />
                </td>
            </tr>
        </table>
        <div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
