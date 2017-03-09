<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityTurnover.ascx.cs"
    Inherits="WealthERP.BusinessMIS.EquityTurnover" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            EQ Turnover MIS
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgProductGridExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="imgProductGridExport_Click"></asp:ImageButton>
                            <asp:ImageButton ID="imgOrgGridExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="imgOrgGridExport_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr runat="server" id="trBranchRmDpRow">
        <td align="left" style="width: 25%;">
            <asp:Label ID="lblChooseBranchBM" runat="server" CssClass="FieldName" align="left"
                Text="Branch:     "></asp:Label>
            <asp:DropDownList ID="ddlBranchForEQ" CssClass="cmbField" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBranchForEQ_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td style="width: 25%;">
            <asp:Label ID="lblChooseRM" runat="server" CssClass="FieldName" Text="RM: "></asp:Label>
            <asp:DropDownList ID="ddlRMEQ" CssClass="cmbField" runat="server">
            </asp:DropDownList>
        </td>
        <td align="left" style="width: 25%;">
            <asp:Label ID="lblAction" runat="server" CssClass="FieldName" Text="Action: "></asp:Label>
            <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" AutoPostBack="true">
                <asp:ListItem Text="Product Level" Value="Product" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Organization Level" Value="Organization"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" class="rightData" style="width: 50%;" colspan="2">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date Type:"></asp:Label>
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickDate" runat="server" Text="Date Range" CssClass="Field"></asp:Label>
            &nbsp;
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Period" CssClass="Field"></asp:Label>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td valign="top" style="width: 50%" colspan="2" align="left">
            <div id="divDateRange" runat="server" visible="false" style="float: left;">
                <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <calendar id="Calendar1" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                        viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                    </calendar>
                    <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                    <dateinput id="DateInput1" runat="server" displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                    </dateinput>
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                    CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="vgBtnGo"> </asp:RequiredFieldValidator>
                <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                <telerik:RadDatePicker ID="txtToDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <calendar id="Calendar2" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                        viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                    </calendar>
                    <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                    <dateinput id="DateInput2" runat="server" displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                    </dateinput>
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                    CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="vgBtnGo"> </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                    Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                    CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgBtnGo"></asp:CompareValidator>
            </div>
            <div id="divDatePeriod" visible="false" runat="server" style="float: left;">
                <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
                &nbsp; &nbsp;
                <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                </asp:DropDownList>
                <span id="Span4" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                    CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                    ValidationGroup="vgBtnGo"> </asp:CompareValidator>
            </div>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AdviserEQMIS_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AdviserEQMIS_btnGo', 'S');"
                OnClick="btnGo_Click" />
        </td>
    </tr>
</table>
<table style="width: 100%">
    <tr>
        <td>
            <div class="divSectionHeading" style="vertical-align: middle; margin: 2px; padding-top: 2px;">
                <table width="100%" cellspacing="0" cellpadding="0" style="padding-top: 0px;">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblMFMISType" runat="server" CssClass="LinkButtons"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr id="trEQProductLevel" runat="server">
        <td colspan="4">
            <asp:Panel ID="pnlEQTurnover" runat="server" ScrollBars="Horizontal" Width="98%"
                Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divEQTurnover" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvEQTurnover" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowAutomaticInserts="false"
                                    ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvEQTurnover_OnNeedDataSource">
                                    <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                        filename="ProductLevel EqTurnover" excel-format="ExcelML">
                                    </exportsettings>
                                    <mastertableview groupsdefaultexpanded="false" expandcollapsecolumn-groupable="true"
                                        grouploadmode="Client" editmode="EditForms" showgroupfooter="true" width="100%"
                                        allowmulticolumnsorting="True" autogeneratecolumns="false" commanditemdisplay="None">
                                        <Columns>
                                           <telerik:GridBoundColumn HeaderText="Company/Scrip" DataField="PEM_CompanyName" UniqueName="PEM_CompanyName"
                                                SortExpression="PEM_CompanyName" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Sector" DataField="PGSC_SectorCategoryName" UniqueName="PGSC_SectorCategoryName"
                                                SortExpression="PGSC_SectorCategoryName" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="70px" HeaderText="Exchange" DataField="Exchange" UniqueName="Exchange"
                                                SortExpression="Exchange" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DeliveryBuy Amt" DataField="DeliveryBuy" UniqueName="DeliveryBuy"
                                                SortExpression="DeliveryBuy" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DeliveryBuy Cnt" DataField="DeliveryBuyCount"
                                                UniqueName="DeliveryBuyCount" SortExpression="DeliveryBuyCount" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DeliverySell Amt" DataField="DeliverySell" UniqueName="DeliverySell"
                                                SortExpression="DeliverySell" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DeliverySell Cnt" DataField="DeliverySellCount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="DeliverySellCount" SortExpression="DeliverySellCount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="SpeculativeBuy Amt" DataField="SpeculativeBuy"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="SpeculativeBuy" SortExpression="SpeculativeBuy"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="SpeculativeBuy Cnt" DataField="SpeculativeBuyCount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="SpeculativeBuyCount" SortExpression="SpeculativeBuyCount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="SpeculativeSell Amt" DataField="SpeculativeSell"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="SpeculativeSell" SortExpression="SpeculativeSell"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="SpeculativeSell Cnt" DataField="SpeculativeSellCount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="SpeculativeSellCount" SortExpression="SpeculativeSellCount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr id="trOrgLevel" runat="server">
        <td colspan="4">
            <asp:Panel ID="pnlOrgLevel" runat="server" ScrollBars="Horizontal" Width="98%" Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divOrgLevel" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvOrgLevel" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowAutomaticInserts="false"
                                    ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvOrgLevel_OnNeedDataSource">
                                    <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                        filename="OrganizationLevel EQTurnover" excel-format="ExcelML">
                                    </exportsettings>
                                    <mastertableview groupsdefaultexpanded="false" expandcollapsecolumn-groupable="true"
                                        grouploadmode="Client" editmode="EditForms" showgroupfooter="true" width="100%"
                                        allowmulticolumnsorting="True" autogeneratecolumns="false" commanditemdisplay="None">
                                        <Columns>
                                           <telerik:GridBoundColumn HeaderText="Branch" DataField="AB_BranchName" HeaderStyle-HorizontalAlign="left"
                                                UniqueName="AB_BranchName" SortExpression="AB_BranchName" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="left" FooterText="Grand Total:">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="RM" DataField="AR_FirstName" UniqueName="AR_FirstName"
                                                SortExpression="AR_FirstName" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Customer" DataField="Customer" UniqueName="Customer"
                                                SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains" >
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DeliveryBuy Amt" DataField="DeliveryBuy" UniqueName="DeliveryBuy"
                                                SortExpression="DeliveryBuy" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DeliveryBuy Cnt" DataField="DeliveryBuyCount"
                                                UniqueName="DeliveryBuyCount" SortExpression="DeliveryBuyCount" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DeliverySell Amt" DataField="DeliverySell" UniqueName="DeliverySell"
                                                SortExpression="DeliverySell" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="DeliverySell Cnt" DataField="DeliverySellCount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="DeliverySellCount" SortExpression="DeliverySellCount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="SpeculativeBuy Amt" DataField="SpeculativeBuy"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="SpeculativeBuy" SortExpression="SpeculativeBuy"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="SpeculativeBuy Cnt" DataField="SpeculativeBuyCount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="SpeculativeBuyCount" SortExpression="SpeculativeBuyCount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="SpeculativeSell Amt" DataField="SpeculativeSell"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="SpeculativeSell" SortExpression="SpeculativeSell"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="SpeculativeSell Cnt" DataField="SpeculativeSellCount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="SpeculativeSellCount" SortExpression="SpeculativeSellCount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
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
<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
