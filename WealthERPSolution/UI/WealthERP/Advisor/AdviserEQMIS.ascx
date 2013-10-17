<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserEQMIS.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserEQMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        height: 25px;
    }
    .style2
    {
        height: 26px;
    }
    .style3
    {
        width: 599px;
    }
    .style4
    {
        height: 49px;
    }
    .style5
    {
        width: 363px;
    }
</style>

<script type="text/javascript">

    function checkdate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var mssge = "";

        if (selectedDate > todayDate) {

            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future. Date value is reset to the current date");
        }
    }
</script>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            EQ Allocation
                        </td>
                        <td align="right">
                        <asp:ImageButton ID="imgEQAllocatio" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" onclick="imgEQAllocatio_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr id="trEQMISTypeSelection" runat="server">
        <td>
            <asp:Label ID="lblMISType" runat="server" Width="90" CssClass="FieldName">MIS Type:</asp:Label>
            <asp:DropDownList ID="ddlMISType" Style="vertical-align: middle" runat="server" CssClass="cmbField"
                AutoPostBack="true">
                <%--<asp:ListItem Value="TurnOverSummery" Text="Turn Over Summary"></asp:ListItem>--%>
                <asp:ListItem Value="CompanyWise" Text="Company Wise" Selected="True"></asp:ListItem>
                <asp:ListItem Value="SectorWise" Text="Sector Wise"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table runat="server" id="tableComSecWiseOptions">
    <tr runat="server" id="trComSecWiseOptions">
        <td valign="top">
            <asp:Label ID="lblEQDate" runat="server" Width="90" CssClass="FieldName">As on Date:</asp:Label>
            <asp:TextBox ID="txtEQDate" runat="server" Width="180" Style="vertical-align: middle"
                CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="EQcalEX" runat="server" TargetControlID="txtEQDate" OnClientDateSelectionChanged="checkdate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="EQTxtWatermark" runat="server" TargetControlID="txtEQDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEQDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="lblEQPortfolio" runat="server" Text="Portfolio :" CssClass="FieldName"></asp:Label>
            <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField" Width="180">
                <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
                <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table>
    <tr runat="server" id="trBranchRmDpRow">
        <td align="left">
            <asp:Label ID="lblChooseBranchBM" runat="server" Font-Bold="true" Width="89" CssClass="FieldName"
                align="left" Text="Branch:     "></asp:Label>
            <asp:DropDownList ID="ddlBranchForEQ" Style="vertical-align: middle" Width="185"
                CssClass="cmbField" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBranchForEQ_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Image Visible="false" runat="server" Width="10px" />
            <asp:Label ID="lblChooseRM" runat="server" Font-Bold="true" Width="60" CssClass="FieldName"
                Text="RM: "></asp:Label>
            <asp:DropDownList ID="ddlRMEQ" Style="vertical-align: middle" Width="180" CssClass="cmbField"
                runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="PCGButton"
                ValidationGroup="btnGo" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AdviserEQMIS_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AdviserEQMIS_btnGo', 'S');" />
        </td>
    </tr>
</table>
<table style="width: 100%">
    <tr>
        <td colspan="2" align="Left" style="width: 100%" class="style3">
            <telerik:RadGrid ID="gvEQMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="80%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvEQMIS_OnNeedDataSource"
                EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">
                                    </exportsettings>
                <mastertableview width="100%" allowmulticolumnsorting="True" autogeneratecolumns="false"
                    commanditemdisplay="None" groupsdefaultexpanded="false" expandcollapsecolumn-groupable="true"
                    grouploadmode="Client" showgroupfooter="true">
                                        <Columns>
                                           <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Company" DataField="Company"
                                                UniqueName="Company" SortExpression="Company" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Industry" DataField="Industry"
                                                UniqueName="Industry" SortExpression="Industry" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="No. Of Share" DataField="NoOfShare"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="NoOfShare" SortExpression="NoOfShare"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Market Value" DataField="MarketValue"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="MarketValue" SortExpression="MarketValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="% to net Assets" DataField="NetAssets"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="NetAssets" SortExpression="NetAssets"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                      </Columns>
                                    </mastertableview>
                <headerstyle width="150px" />
                <clientsettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </clientsettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<table width="100%">
            <tr>
                <td>
                    <asp:Label ID="LabelMainNote" runat="server" Font-Size="Small" CssClass="cmbFielde" 
                    Text="Note: 1.You can group/ungroup or hide/unhide fields by a right click on the grid label and then making the selection."></asp:Label>
                </td>
            </tr>
</table>
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" />
<asp:HiddenField ID="hdnall" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<asp:HiddenField ID="hdnValuationDate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnEQMISType" runat="server" Visible="false" />
<asp:HiddenField ID="hdnPortfolioType" runat="server" Visible="false" />
