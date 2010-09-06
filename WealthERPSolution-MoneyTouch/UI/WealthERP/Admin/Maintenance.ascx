<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Maintenance.ascx.cs"
    Inherits="WealthERP.Admin.Maintenance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table style="width: 100%">
    <tr>
        <td colspan="2" class="HeaderCell">
            <label id="lblheader" class="HeaderTextBig" title="Product Master Maintenance">
                Product 
                <asp:GridView ID="gvMFMaintenance" CellPadding="4" CssClass="GridViewStyle" DataKeyNames="WERPCODE"
                    runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    AllowPaging="true" OnPageIndexChanging="gvMFMaintenance_PageIndexChanging">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="WERPCODE" HeaderText="CODE" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="AMFICode" HeaderText="AMFI CODE" />
                        <asp:BoundField DataField="KARVYCode" HeaderText="KARVY CODE" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMFSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMFSelect_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                    <asp:ListItem Text="View" Value="View"></asp:ListItem>
                                    <asp:ListItem Text="Edit" Value="Edit"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            Master Maintenance</label>
        </td>
    </tr>
    <tr> 
        <td class="leftField">
            <label id="lbl" class="FieldName" title="Choose the product Master:">
                Choose the product Master:</label>
        </td>
        <td class="rightField">
            <asp:DropDownList CssClass="cmbField" ID="ddlProductMaster" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ProductMaster_SelectedIndexedChanged">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="ScripMaster" Value="ScripMaster"></asp:ListItem>
                <asp:ListItem Text="SchemeMaster" Value="SchemeMaster"></asp:ListItem>
                <asp:ListItem Text="EquityCorpAction" Value="EquityCorpAction"></asp:ListItem>
                <asp:ListItem Text="MFCorpAction" Value="MFCorpAction"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnAdd" Text="Add New" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Maintenance_btnAdd');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Maintenance_btnAdd');" OnClick="OnClick_AddNew" />
            <asp:RequiredFieldValidator ID="AddValidate" runat="server" ControlToValidate="ddlProductMaster"
                ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<br />
<div id="DivEquity" runat="server">
    <table style="width: 100%">
        <tr>
            <td class="leftField">
                <asp:Label ID="lblgvEquityMaintenanceCurrentPage" class="Field" runat="server"></asp:Label>
                <asp:Label ID="lblgvEquityMaintenanceTotalRows" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:GridView ID="gvEquityMaintenance" runat="server" CellPadding="4" CssClass="GridViewStyle"
                    AllowSorting="True" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvEquityMaintenance_PageIndexChanging">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="WERPCODE" HeaderText="CODE" />
                        <asp:BoundField DataField="NAME" HeaderText="NAME" />
                        <asp:BoundField DataField="BSECODE" HeaderText="BSE CODE" />
                        <asp:BoundField DataField="NSECODE" HeaderText="NSE CODE" />
                        <asp:BoundField DataField="CERCCODE" HeaderText="CERC CODE" />
                        <asp:BoundField DataField="ISINNO" HeaderText="ISINNO" />
                        <asp:BoundField DataField="IncorporationDate" HeaderText="Incorportion Date"  />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                    <asp:ListItem Text="View" Value="View"></asp:ListItem>
                                    <asp:ListItem Text="Edit" Value="Edit"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
<br />
<div id="DivMF" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td class="leftField">
                <asp:Label ID="lblgvMFMaintenanceCurrentPage" class="Field" runat="server"></asp:Label>
                <asp:Label ID="lblgvMFMaintenanceTotalRows" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</div>

<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr>
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>

<asp:HiddenField ID="hdnEquityCount" runat="server" />
<asp:HiddenField ID="hdnMFCount" runat="server" />
