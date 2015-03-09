<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayableStructureToAgentCategoryMapping.ascx.cs"
    Inherits="WealthERP.CommisionManagement.PayableStructureToAgentCategoryMapping" %>

<telerik:RadScriptManager ID="radScriptManager" runat="server" AsyncPostBackTimeout="3600000">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</telerik:RadScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 18%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            Payable Mapping
                            <asp:HiddenField ID="hdnStructId" runat="server" />
                            <asp:HiddenField ID="hdnProductId" runat="server" />
                            <asp:HiddenField ID="hdnStructValidFrom" runat="server" />
                            <asp:HiddenField ID="hdnStructValidTill" runat="server" />
                            <asp:HiddenField ID="hdnIssuerId" runat="server" />
                            <asp:HiddenField ID="hdnCategoryId" runat="server" />
                            <asp:HiddenField ID="hdnSubcategoryIds" runat="server" />
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="25px"
                                Width="25px" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr runat="server" id="trMappings">
        <td class="leftLabel">
            <asp:Label ID="Label1" runat="server" Text="Mapping For:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlMapping" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlMapping_Selectedindexchanged">
                <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                <asp:ListItem Text="Associate" Value="Associate"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftLabel">
            <asp:Label ID="Label2" runat="server" Text="Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlType_Selectedindexchanged"
                AutoPostBack="true">
                <asp:ListItem Text="Custom" Value="Custom"></asp:ListItem>
                <asp:ListItem Text="UserCategory" Value="UserCategory"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <%--  <td class="leftLabel">
            <asp:Label ID="lblAssetCategory" CssClass="FieldName" runat="server" Text="Asset Category:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlAdviserCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>--%>
    </tr>
    <tr id="trAssetCategory" runat="server">
        <td class="leftLabel">
            <asp:Label ID="lblAssetCategory" CssClass="FieldName" runat="server" Text="Associate Category:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlAdviserCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfvddlAdviserCategory" ControlToValidate="ddlAdviserCategory"
                ErrorMessage="Please Select Category" ValidationGroup="btnSubmitRule" Display="Dynamic"
                InitialValue="Select" runat="server" Visible="true" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<table>
    <tr runat="server" id="trListControls" visible="true">
        <td>
            <div class="clearfix" style="margin-bottom: 1em;">
                <asp:Panel ID="PLCustomer" runat="server" Style="float: left; padding-left: 150px;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblSelectBranch" runat="server" CssClass="FieldName" Text="Existing AgentCodes">
                    </asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Mapped AgentCodes">
                    </asp:Label>
                    <br />
                    <telerik:RadListBox SelectionMode="Multiple" EnableDragAndDrop="true" AccessKey="y"
                        AllowTransferOnDoubleClick="true" AllowTransferDuplicates="false" EnableViewState="true"
                        EnableMarkMatches="true" runat="server" ID="LBAgentCodes" Height="200px" Width="250px"
                        OnTransferred="ListBoxSource_Transferred" AllowTransfer="true" TransferToID="RadListBoxSelectedAgentCodes"
                        CssClass="cmbFielde" Visible="true">
                    </telerik:RadListBox>
                    <telerik:RadListBox runat="server" AutoPostBackOnTransfer="true" SelectionMode="Multiple"
                        ID="RadListBoxSelectedAgentCodes" Height="200px" Width="220px" CssClass="cmbField">
                    </telerik:RadListBox>
                </asp:Panel>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr style="margin-left: 10%;">
        <td>
        </td>
        <td style="margin-left: 10%;">
            <div class="clearfix" style="margin-left: 150px;">
                <telerik:RadGrid ID="rgPayableMapping" runat="server" AllowSorting="True" enableloadondemand="True"
                    PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                    ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="false" ShowStatusBar="True"
                    Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgPayableMapping_OnNeedDataSource"
                    Width="70%" OnItemDataBound="rgPayableMapping_OnItemDataBound">
                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                        Width="100%" DataKeyNames="ACSR_CommissionStructureRuleId">
                        <Columns>
                            <%-- <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                        UpdateText="Update">--%>
                            <%--</telerik:GridEditCommandColumn>--%>
                            <%--  <telerik:GridBoundColumn DataField="CSRD_StructureRuleDetailsId" HeaderStyle-Width="10px"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        HeaderText="Rule Detail Id" UniqueName="CSRD_StructureRuleDetailsId" SortExpression="CSRD_StructureRuleDetailsId"
                                        AllowFiltering="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="10px" Wrap="false" />
                                    </telerik:GridBoundColumn>--%>
                            <%-- <telerik:GridBoundColumn DataField="ACSR_CommissionStructureRuleId" HeaderStyle-Width="10px"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        HeaderText="RuleId" UniqueName="ACSR_CommissionStructureRuleId" SortExpression="ACSR_CommissionStructureRuleId"
                                        AllowFiltering="true" Visible="false">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="ACSR_CommissionStructureRuleName" HeaderStyle-Width="20px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Rule Name" UniqueName="ACSR_CommissionStructureRuleName" SortExpression="ACSR_CommissionStructureRuleName"
                                AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="CSRD_RateName" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Rate Name" UniqueName="CSRD_RateName"
                                        SortExpression="CSRD_RateName" AllowFiltering="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="WCT_CommissionType" HeaderStyle-Width="20px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Commission Type" UniqueName="WCT_CommissionType" SortExpression="WCT_CommissionType"
                                AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WCMV_Name" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Commission Sub Type"
                                UniqueName="WCMV_Name" SortExpression="WCMV_Name" AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridBoundColumn DataField="CSRD_BrokageValue" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Brokage Value"
                                        UniqueName="CSRD_BrokageValue" SortExpression="CSRD_BrokageValue" AllowFiltering="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn DataField="WCU_UnitCode" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Brokage Unit"
                                        UniqueName="WCU_UnitCode" SortExpression="WCU_UnitCode" AllowFiltering="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="Broker/Rate(%)" AllowFiltering="true" DataField="CSRD_StructureRuleDetailsId">
                                <%--<ItemTemplate>
                                            <asp:Repeater ID="repradiobutton" runat="server"  >--%>
                                <ItemTemplate>
                                    <asp:CheckBoxList ID="chkListrate" runat="server" RepeatDirection="Vertical">
                                    </asp:CheckBoxList>
                                    <%-- <asp:RadioButtonList ID="rbtnListRate" runat="server" RepeatDirection="Vertical">
                                </asp:RadioButtonList>--%>
                                </ItemTemplate>
                                <%-- <asp:Repeater ID="rptUnitValue" runat="server" DataMember="WCU_UnitCode"></asp:Repeater>--%>
                                <%--     </asp:Repeater>
                                        </ItemTemplate>--%>
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="110px"
                                        UniqueName="Action" HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="llPurchase" runat="server" Text="Map" OnClick="llPurchase_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>
                            <%--    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="110px"
                                        UniqueName="Action" HeaderText="Action"  >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="llViewUnMapping" runat="server" Text="UNMAP" OnClick="llViewUnMapping_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>
                            <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                Text="Delete" Visible="false">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr runat="server" visible="false">
        <td class="leftLabel">
            <asp:Label ID="lblStructName" runat="server" Text="Structure:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:LinkButton ID="lbtStructureName" runat="server" CssClass="txtField" AutoPostBack="True"
                OnClick="lbtStructureName_Click" Visible="false">StructureName</asp:LinkButton>
            <asp:DropDownList ID="ddlStructs" runat="server" Visible="false" CssClass="cmbField"
                AutoPostBack="true" OnSelectedIndexChanged="ddlStructs_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblProductName" runat="server" Text="Product: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtProductName" runat="server" CssClass="txtField" Enabled="false"
                AutoPostBack="true" />
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblSubcats" runat="server" Text="Sub-Category: " CssClass="FieldName"></asp:Label>
        </td>
        <td rowspan="4">
            <telerik:RadListBox ID="rlbAssetSubCategory" runat="server" CssClass="cmbField" Width="200px"
                Height="90px" AutoPostBack="true">
                <ButtonSettings TransferButtons="All"></ButtonSettings>
            </telerik:RadListBox>
        </td>
    </tr>
    <tr runat="server" visible="false">
        <td class="leftLabel">
            <asp:Label ID="lblCategory" runat="server" Text="Category: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtCategory" runat="server" CssClass="txtField" Enabled="false"
                AutoPostBack="true" />
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblIssuerName" runat="server" Text="Issuer: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtIssuerName" runat="server" CssClass="txtField" Enabled="false"
                AutoPostBack="true" />
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr runat="server" visible="false">
        <td class="leftLabel">
            <asp:Label ID="lblStructValidFrom" runat="server" Text="Validity From: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtValidFrom" runat="server" Enabled="false" CssClass="txtField"
                AutoPostBack="true"></asp:TextBox>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblStrucValidTo" runat="server" Text="Valid To: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtValidTo" runat="server" Enabled="false" CssClass="txtField" AutoPostBack="true"></asp:TextBox>
        </td>
    </tr>
    <%--</table>
<table width="100%">--%>
</table>
<table>
    <tr>
        <td class="rightData" colspan="2">
            <asp:Button ID="btnGo" ValidationGroup="btnSubmitRule" CssClass="PCGButton" OnClick="btnGo_Click"
                Text="Submit" runat="server" CausesValidation="true"></asp:Button>&nbsp;
            <%--  <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="false"
                                                        CommandName="Cancel"></asp:Button>--%>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdneligible" runat="server" />
