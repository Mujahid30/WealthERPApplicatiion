<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayableStructureToAgentCategoryMapping.ascx.cs"
    Inherits="WealthERP.CommisionManagement.PayableStructureToAgentCategoryMapping" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
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
                            Payable Structure To Agent Category Mapping
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
                                Width="25px" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%" >
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
    <tr>
        <td class="leftLabel">
            <asp:Label ID="Label1" runat="server" Text="Mapping For:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlMapping" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlMapping_Selectedindexchanged">
                <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                <asp:ListItem Text="Associate" Value="Associate"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftLabel">
            <asp:Label ID="Label2" runat="server" Text="Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlType" runat="server" enabled="false" CssClass="cmbField" OnSelectedIndexChanged="ddlType_Selectedindexchanged"
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
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblAssetCategory" CssClass="FieldName" runat="server" Text="Associate Category:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlAdviserCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table>
    <tr runat="server" id="trListControls" visible="false">
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
                        AllowTransfer="true" TransferToID="RadListBoxSelectedAgentCodes" CssClass="cmbFielde"
                        Visible="true">
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
    <tr>
        <td class="rightData" colspan="2">
            <asp:Button ID="btnGo" ValidationGroup="btnSubmitRule" CssClass="PCGButton" OnClick="btnGo_Click"
                Text="Submit" runat="server" CausesValidation="true"></asp:Button>&nbsp;
            <%--  <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="false"
                                                        CommandName="Cancel"></asp:Button>--%>
        </td>
    </tr>
</table>
