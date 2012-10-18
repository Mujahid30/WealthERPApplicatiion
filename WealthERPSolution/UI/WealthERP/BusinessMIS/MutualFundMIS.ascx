<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MutualFundMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.MutualFundMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

    function SetWindowBehavior(sender) {
        oWnd = sender;
        oWnd.setActive(true);
    }
</script>

<script type="text/javascript" language="javascript">
    function CheckValuationDate() {
        var valuationDate = document.getElementById("<%=hdnValuationDate.ClientID %>").value;
        var txtDate = document.getElementById("<%=txtDate.ClientID %>").value;
        txtDate = new Date(txtDate);
        valuationDate = new Date(valuationDate);
        txtDate.setHours(0, 0, 0, 0);
        valuationDate.setHours(0, 0, 0, 0);


        if (txtDate <= valuationDate) {
            return true;
        }
        else {
            alert("Please Select Prior Business Date");
            return false;
        }

    }
</script>

<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0" cellpadding="3" width="100%">
        <tr>
        <td align="left">Mutual Fund MIS</td>
        <td  align="right">
                        <asp:ImageButton ID="imgBtnGvFolioWiseAUM" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnGvFolioWiseAUM_OnClick"
                        Height="20px" Width="25px" Visible="false"></asp:ImageButton>
                    <asp:ImageButton ID="imgBtnGvAmcWiseAUM" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnGvAmcWiseAUM_OnClick"
                        Height="20px" Width="25px" Visible="false"></asp:ImageButton>
                    <asp:ImageButton ID="imgBtnGvSchemeWiseAUM" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnGvSchemeWiseAUM_OnClick"
                        Height="20px" Width="25px" Visible="false"></asp:ImageButton>
                    <asp:ImageButton ID="imgBtnGvTurnOverSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnGvTurnOverSummary_OnClick"
                        Height="20px" Width="25px" Visible="false"></asp:ImageButton>        
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>

<div style="margin: 16px;">
    <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:" Visible="false"></asp:Label>
    <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
        CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Visible="false">
    </asp:DropDownList>
    <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:" Visible="false"></asp:Label>
    <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" Style="vertical-align: middle"
        Visible="false">
    </asp:DropDownList>
    <asp:Label ID="lblDate" runat="server" CssClass="FieldName">As on Date:</asp:Label>
    <telerik:RadDatePicker ID="txtDate" CssClass="txtTo" runat="server" Culture="English (United States)"
        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
        </Calendar>
        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
        <DateInput ID="DateInput1" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
            DateFormat="d/M/yyyy">
        </DateInput>
    </telerik:RadDatePicker>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDate"
        CssClass="rfvPCG" ErrorMessage="Please select a Date" Display="Dynamic" runat="server"
        InitialValue="" ValidationGroup="vgBtnGo">
    </asp:RequiredFieldValidator>
</div>
<div style="margin: 16px;">
    <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Select MIS:"></asp:Label>
    <asp:LinkButton ID="lnkBtnAMCWISEAUM" Text="AMC WISE AUM" CssClass="LinkButtonsWithoutUnderLine"
        runat="server" OnClientClick="return CheckValuationDate();" OnClick="lnkBtnAMCWISEAUM_OnClick"
        ValidationGroup="vgBtnGo"></asp:LinkButton>
    <span>|</span>
    <asp:LinkButton ID="lnkBtnSCHEMEWISEAUM" Text="SCHEME WISE AUM" CssClass="LinkButtonsWithoutUnderLine"
        runat="server" OnClientClick="return CheckValuationDate();" OnClick="lnkBtnSCHEMEWISEAUM_OnClick"
        ValidationGroup="vgBtnGo"></asp:LinkButton>
    <span>|</span>
    <asp:LinkButton ID="lnkBtnFOLIOWISEAUM" Text="FOLIO WISE AUM" CssClass="LinkButtonsWithoutUnderLine"
        runat="server" OnClientClick="return CheckValuationDate();" OnClick="lnkBtnFOLIOWISEAUM_OnClick"
        ValidationGroup="vgBtnGo"></asp:LinkButton>
    <span>|</span>
    <asp:LinkButton ID="lnkBtnTURNOVERAUM" Text="TURNOVER AUM" CssClass="LinkButtonsWithoutUnderLine"
        runat="server" OnClick="lnkBtnTURNOVERAUM_OnClick"></asp:LinkButton>
</div>
<div class="divSectionHeading" style="vertical-align: middle; margin:2px">
    <asp:Label ID="lblMFMISType" runat="server" CssClass="LinkButtons"></asp:Label>
</div>
<br />
<div align="center">
    <asp:Label ID="lblErrorMsg" runat="server" CssClass="failure-msg" Visible="false">
    </asp:Label>
</div>
<br />
<div runat="server" id="divGvAmcWiseAUM" visible="false" style="margin: 2px;">
    <telerik:RadGrid ID="gvAmcWiseAUM" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="700px" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="AmcWiseAUM Details" OnNeedDataSource="gvAmcWiseAUM_OnNeedDataSource"
        OnItemCommand="gvAmcWiseAUM_OnItemCommand">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="100%" DataKeyNames="AMCCode" AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="AMC" HeaderStyle-Width="353px" DataField="AMC"
                    UniqueName="AMC" SortExpression="AMC" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="AUM" HeaderStyle-Width="150px" DataField="AUM"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="AUM %" DataField="Percentage"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div>
    <telerik:RadWindow ID="rWTurnOverAUM" runat="server" VisibleOnPageLoad="false" Height="30%"
        Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move,resize,close"
        OnClientShow="SetWindowBehavior" Title="TURN OVER AUM">
        <ContentTemplate>
            <div style="margin: 4px">
                <asp:RadioButton ID="rbtnPickDate" Checked="true" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                    runat="server" GroupName="Date" />
                <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
                <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                    runat="server" GroupName="Date" />
                <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
            </div>
            <br />
            <div style="margin: 4px" id="divPickAdateRange" runat="server">
                <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox>
                <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                    TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
                </cc1:TextBoxWatermarkExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFromDate"
                    CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnGo">
                </asp:RequiredFieldValidator>
                <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
                <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <cc1:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                    TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
                </cc1:TextBoxWatermarkExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtToDate"
                    CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnGo">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />To Date should not less than From Date"
                    Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                    CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
            </div>
            <br />
            <div style="margin: 4px" id="divPickAPeriod" runat="server">
                <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
                <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                </asp:DropDownList>
            </div>
            <br />
            <div style="margin: 4px">
                <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="PCGButton" />
            </div>
            <br />
        </ContentTemplate>
    </telerik:RadWindow>
</div>
<div runat="server" id="divGvFolioWiseAUM" style="overflow: scroll;" visible="false">
    <telerik:RadGrid OnPreRender="gvFolioWiseAUM_PreRender" ID="gvFolioWiseAUM" runat="server"
        GridLines="None" AutoGenerateColumns="False" PageSize="10" AllowSorting="true"
        AllowPaging="True" ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
        Width="1050px" AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-FileName="FolioWiseAUM Details"
        OnNeedDataSource="gvFolioWiseAUM_OnNeedDataSource" OnItemCommand="gvFolioWiseAUM_OnItemCommand">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="FolioNum" Width="100%" AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandName="Select" runat="server" Text="Details"
                            ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Customer" DataField="CustomerName" UniqueName="CustomerName"
                    SortExpression="CustomerName" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="RM" DataField="RmName" UniqueName="RmName" SortExpression="RmName"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Branch" DataField="BranchName" UniqueName="BranchName"
                    SortExpression="BranchName" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Folio" DataField="FolioNum" UniqueName="FolioNum"
                    SortExpression="FolioNum" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="AMC" DataField="AMC" UniqueName="AMC" SortExpression="AMC"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Scheme" DataField="Scheme" UniqueName="Scheme"
                    SortExpression="Scheme" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Category" DataField="Category"
                    UniqueName="Category" SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Curr NAV" DataField="MarketPrice"
                    UniqueName="MarketPrice" SortExpression="MarketPrice" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Units" DataField="Units"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM" DataField="AUM"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM %" DataField="Percentage"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div runat="server" style="overflow: scroll;" id="divGvSchemeWiseAUM" visible="false">
    <telerik:RadGrid ID="gvSchemeWiseAUM" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="SchemeWiseAUM Details"
        OnNeedDataSource="gvSchemeWiseAUM_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="SchemePlanCode" Width="100%" AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandName="Select" runat="server" Text="Details"
                            ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="AMC" DataField="AMC" UniqueName="AMC" SortExpression="AMC"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" HeaderText="Scheme" DataField="Scheme"
                    UniqueName="Scheme" SortExpression="Scheme" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Category" DataField="Category" UniqueName="Category"
                    SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="SubCategory" DataField="SubCategory" UniqueName="SubCategory"
                    SortExpression="SubCategory" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Curr NAV" DataField="MarketPrice"
                    UniqueName="MarketPrice" SortExpression="MarketPrice" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Units" HeaderStyle-Width="100px" DataField="Units"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM" DataField="AUM"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM %" DataField="Percentage"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div runat="server" id="divGvTurnOverSummary" visible="false" style="margin: 2px">
    <telerik:RadGrid ID="gvTurnOverSummary" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="TurnOverSummary Details"
        OnNeedDataSource="gvTurnOverSummary_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None">
            <Columns>
                <telerik:GridBoundColumn HeaderText="Category" DataField="Category" UniqueName="Category"
                    SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Buy Value" DataField="BuyValue" UniqueName="BuyValue"
                    SortExpression="BuyValue" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Sell Value" DataField="SellValue" UniqueName="SellValue"
                    SortExpression="SellValue" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="No. of Transactions" DataField="NoOfTrans" UniqueName="NoOfTrans"
                    SortExpression="NoOfTrans" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="SIP Value" DataField="SIPValue" UniqueName="SIPValue"
                    SortExpression="SIPValue" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="No. of SIPs" DataField="NoOfSIPs" UniqueName="NoOfSIPs"
                    SortExpression="NoOfSIPs" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="125px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<br />
<div style="margin: 6px" class="Note">
    <p>
        <b>Note: For TURNOVER AUM please donot select "As on Date:" </b>
    </p>
</div>
<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnXWise" runat="server" Visible="false" />
<asp:HiddenField ID="hdnValuationDate" runat="server" />
