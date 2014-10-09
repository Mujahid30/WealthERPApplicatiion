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
        var txtDate = document.getElementById('ctrl_MutualFundMIS_txtDate_dateInput_text').value;
        var datearray = txtDate.split("/");
        var newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        txtDate = new Date(newdate);
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
                        <td align="left">
                            Mutual Fund AUM MIS
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnGvFolioWiseAUM" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnGvFolioWiseAUM_OnClick"
                                Height="20px" Width="25px" Visible="false"></asp:ImageButton>
                            <asp:ImageButton ID="imgBtnGvAmcWiseAUM" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnGvAmcWiseAUM_OnClick"
                                Height="20px" Width="25px" Visible="false"></asp:ImageButton>
                            <asp:ImageButton ID="imgBtnGvSchemeWiseAUM" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnGvSchemeWiseAUM_OnClick"
                                Height="20px" Width="25px" Visible="false"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div style="margin: 16px;" visible="false" id="divAdviserList" runat="server">
    <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
    <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true"
        OnSelectedIndexChanged="ddlAdviser_SelectedIndexChanged">
    </asp:DropDownList>
</div>
<div style="margin: 16px;" id="divDateDetails" runat="server" visible="false">
    <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:" Visible="false"></asp:Label>
    <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
        CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Visible="false">
    </asp:DropDownList>
    <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:" Visible="false"></asp:Label>
    <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" Style="vertical-align: middle"
        Visible="false">
    </asp:DropDownList>
    <asp:Label ID="lblDate" runat="server" CssClass="FieldName">As on Date</asp:Label>
    <asp:DropDownList ID="ddlFilterSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterSelection_SelectedIndexChanged"
        CssClass="cmbField" Style="vertical-align: middle" Visible="false">
        <asp:ListItem Text="As on Date" Value="0"></asp:ListItem>
        <asp:ListItem Text="Date Range" Value="1" Enabled="false"></asp:ListItem>
    </asp:DropDownList>
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
    <asp:Label ID="lblType" runat="server" CssClass="FieldName" Text="Select Type:"></asp:Label>
    <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Online" Value="1" Selected=True>
        </asp:ListItem>
        <asp:ListItem Text="Offlline" Value="0">
        </asp:ListItem>
    </asp:DropDownList>
</div>
<div style="margin: 16px;">
    <asp:Label ID="lblFrom" runat="server" Visible="false" CssClass="FieldName" Text="From:"></asp:Label>
    <telerik:RadDatePicker ID="rdpFrom" Visible="false" CssClass="txtTo" runat="server"
        Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
        ShowAnimation-Type="Fade" MinDate="1900-01-01">
        <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
        </Calendar>
        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
        <DateInput ID="DateInput2" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
            DateFormat="d/M/yyyy">
        </DateInput>
    </telerik:RadDatePicker>
    <asp:Label ID="lblTo" Visible="false" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
    <telerik:RadDatePicker Visible="false" ID="rdpTo" CssClass="txtTo" runat="server"
        Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
        ShowAnimation-Type="Fade" MinDate="1900-01-01">
        <Calendar ID="Calendar3" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
        </Calendar>
        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
        <DateInput ID="DateInput3" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
            DateFormat="d/M/yyyy">
        </DateInput>
    </telerik:RadDatePicker>
    <asp:Label ID="LstValDt" runat="server" CssClass="FieldName" Text="Available Valuation Date:"></asp:Label>
    <asp:Label ID="lblValDt" runat="server" CssClass="txtField"></asp:Label>
</div>
<div style="margin: 16px;" id="divSelectionDetails" runat="server" visible="false">
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
</div>
<div id="divSection" runat="server" class="divSectionHeading" style="vertical-align: middle;
    margin: 2px">
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
                <telerik:GridTemplateColumn Visible="false" HeaderStyle-Width="100px" AllowFiltering="false"
                    UniqueName="action" DataField="action" FooterText="Grand Total:">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Select" Text="View Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="AMC" HeaderStyle-Width="353px" DataField="AMC"
                    UniqueName="AMC" SortExpression="AMC" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" DataField="AUM" AutoPostBackOnFilter="true"
                    HeaderText="AUM" ShowFilterIcon="false" CurrentFilterFunction="Contains" Aggregate="Sum"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text='<%# Eval("AUM").ToString() %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn HeaderText="AUM" HeaderStyle-Width="150px" DataField="AUM"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="AUM %" DataField="Percentage"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Percentage" SortExpression="Percentage"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N4}" Aggregate="Sum" FooterStyle-HorizontalAlign="Center">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="customer" HeaderText="Customer Count" DataField="customer"
                    HeaderStyle-Width="80px" SortExpression="customer" AllowFiltering="false" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
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
<div runat="server" id="divGvFolioWiseAUM" style="overflow: scroll;" visible="false">
    <telerik:RadGrid OnPreRender="gvFolioWiseAUM_PreRender" ID="gvFolioWiseAUM" runat="server"
        GridLines="None" AutoGenerateColumns="False" PageSize="10" AllowSorting="true"
        OnItemDataBound="gvFolioWiseAUM_ItemDataBound" AllowPaging="True" ShowStatusBar="True"
        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="FolioWiseAUM Details"
        OnNeedDataSource="gvFolioWiseAUM_OnNeedDataSource" OnItemCommand="gvFolioWiseAUM_OnItemCommand">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="CMFA_AccountId,FolioNum,SchemePlanCode" Width="100%"
            AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn Visible="false" HeaderStyle-Width="100px" AllowFiltering="false"
                    UniqueName="action" DataField="action" FooterText="Grand Total:">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandName="Select" runat="server" Text="View Details"
                            ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Customer" DataField="CustomerName" UniqueName="CustomerName"
                    SortExpression="CustomerName" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="PAN" DataField="C_PANNum" UniqueName="C_PANNum"
                    SortExpression="C_PANNum" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
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
                <telerik:GridBoundColumn HeaderText="R & T Name" DataField="RnTName" UniqueName="RnTName"
                    SortExpression="RnTName" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="AMC" DataField="AMC" UniqueName="AMC" SortExpression="AMC"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Product Code" DataField="ProductCode" UniqueName="ProductCode"
                    SortExpression="ProductCode" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
                    HeaderStyle-HorizontalAlign="Right" UniqueName="MarketPrice" SortExpression="MarketPrice"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="NAV Date" DataFormatString="{0:d}"
                    DataField="NAVDate" HeaderStyle-HorizontalAlign="Right" UniqueName="NAVDate"
                    SortExpression="NAVDate" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Unit Balance" DataField="Units"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Units" SortExpression="Units"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" DataField="AUM" AutoPostBackOnFilter="true"
                    HeaderText="AUM" ShowFilterIcon="false" CurrentFilterFunction="Contains" Aggregate="Sum"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text='<%# Eval("AUM").ToString() %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM" DataField="AUM"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM %" DataField="Percentage"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Percentage" SortExpression="Percentage"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N4}" Aggregate="Sum" FooterStyle-HorizontalAlign="Center">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMFA_SubBrokerCode" HeaderText="Sub-Broker Code"
                    AllowFiltering="true" SortExpression="CMFA_SubBrokerCode" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFA_SubBrokerCode"
                    FooterStyle-HorizontalAlign="Left">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AssociatesName" AllowFiltering="true" HeaderText="SubBroker Name"
                    Visible="true" UniqueName="AssociatesName" SortExpression="AssociatesName" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Titles" AllowFiltering="true" HeaderText="Title"
                    Visible="true" UniqueName="Titles" SortExpression="Titles" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReportingManagerName" AllowFiltering="true" HeaderText="Reporting Manager"
                    Visible="true" UniqueName="ReportingManagerName" SortExpression="ReportingManagerName"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="120px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserType" AllowFiltering="true" HeaderText="Type"
                    Visible="true" UniqueName="UserType" SortExpression="UserType" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChannelName" AllowFiltering="true" HeaderText="Channel"
                    UniqueName="ChannelName" SortExpression="ChannelName" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="true" DataField="ClusterManager" AllowFiltering="true"
                    HeaderText="Cluster Manager" UniqueName="ClusterManager" SortExpression="ClusterManager"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="130px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AreaManager" AllowFiltering="true" HeaderText="Area Manager"
                    UniqueName="AreaManager" SortExpression="AreaManager" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ZonalManagerName" AllowFiltering="true" HeaderText="Zonal Manager"
                    UniqueName="ZonalManagerName" SortExpression="ZonalManagerName" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="130px" HeaderText="Deputy Head" DataField="DeputyHead"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="DeputyHead" SortExpression="DeputyHead"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
        OnItemDataBound="gvSchemeWiseAUM_ItemDataBound" OnNeedDataSource="gvSchemeWiseAUM_OnNeedDataSource"
        OnItemCommand="gvSchemeWiseAUM_OnItemCommand">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="SchemePlanCode" Width="100%" AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn Visible="false" HeaderStyle-Width="100px" AllowFiltering="false"
                    UniqueName="action" DataField="action" FooterText="Grand Total:">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandName="Select" runat="server" Text="View Details"
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
                <telerik:GridBoundColumn HeaderText="Product Code" DataField="PASC_AMC_ExternalCode"
                    UniqueName="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Customer Count" DataField="customer" UniqueName="customer"
                    SortExpression="customer" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
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
                <telerik:GridBoundColumn HeaderText="R & T Name" DataField="RnTName" UniqueName="RnTName"
                    SortExpression="RnTName" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Curr NAV" DataField="MarketPrice"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="MarketPrice" SortExpression="MarketPrice"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="NAV Date" DataFormatString="{0:d}"
                    DataField="NAVDate" HeaderStyle-HorizontalAlign="Right" UniqueName="NAVDate"
                    SortExpression="NAVDate" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Unit Balance" HeaderStyle-Width="100px" DataField="Units"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Units" SortExpression="Units"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" DataField="AUM" AutoPostBackOnFilter="true"
                    HeaderText="AUM" ShowFilterIcon="false" CurrentFilterFunction="Contains" Aggregate="Sum"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text='<%# Eval("AUM").ToString() %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--  <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM" DataField="AUM"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM %" DataField="Percentage"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Percentage" SortExpression="Percentage"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N4}" Aggregate="Sum" FooterStyle-HorizontalAlign="Center">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="CMFA_SubBrokerCode" HeaderText="Sub-Broker Code"
                    AllowFiltering="true" SortExpression="CMFA_SubBrokerCode" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFA_SubBrokerCode"
                    FooterStyle-HorizontalAlign="Left">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="AssociatesName" AllowFiltering="true"
                    HeaderText="SubBroker Name" UniqueName="AssociatesName" SortExpression="AssociatesName"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="120px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="Titles" AllowFiltering="true"
                    HeaderText="Title" UniqueName="Titles" SortExpression="Titles" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="ReportingManagerName" AllowFiltering="true"
                    HeaderText="Reporting Manager" UniqueName="ReportingManagerName" SortExpression="ReportingManagerName"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="120px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="UserType" AllowFiltering="true"
                    HeaderText="Type" UniqueName="UserType" SortExpression="UserType" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="ChannelName" AllowFiltering="true"
                    HeaderText="Channel" UniqueName="ChannelName" SortExpression="ChannelName" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="ClusterManager" AllowFiltering="true"
                    HeaderText="Cluster Manager" UniqueName="ClusterManager" SortExpression="ClusterManager"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="130px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="AreaManager" AllowFiltering="true"
                    HeaderText="Area Manager" UniqueName="AreaManager" SortExpression="AreaManager"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="130px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="ZonalManagerName" AllowFiltering="true"
                    HeaderText="Zonal Manager" UniqueName="ZonalManagerName" SortExpression="ZonalManagerName"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="130px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="130px" HeaderText="Deputy Head"
                    DataField="DeputyHead" HeaderStyle-HorizontalAlign="Right" UniqueName="DeputyHead"
                    SortExpression="DeputyHead" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
<br />
<div runat="server" id="divRgvFolioWiseAUM" style="overflow: scroll;" visible="false">
    <telerik:RadGrid ID="rgvFolioWiseAUM" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="rgvFolioWiseAUM_OnNeedDataSource"
        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="FolioWiseAUM Details">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None">
            <Columns>
                <%--  <telerik:GridTemplateColumn HeaderStyle-Width="100px" AllowFiltering="false" UniqueName="action"
                    DataField="action" FooterText="Grand Total:">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandName="Select" runat="server" Text="Details"
                            ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridBoundColumn HeaderText="Customer" DataField="CustomerName" UniqueName="CustomerName"
                    SortExpression="CustomerName" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="PAN" DataField="C_PANNum" UniqueName="C_PANNum"
                    SortExpression="C_PANNum" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
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
                <telerik:GridBoundColumn HeaderText="R & T Name" DataField="RnTName" UniqueName="RnTName"
                    SortExpression="RnTName" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="AMC" DataField="AMC" UniqueName="AMC" SortExpression="AMC"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Product Code" DataField="ProductCode" UniqueName="ProductCode"
                    SortExpression="ProductCode" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Scheme" DataField="SchemePlanCode" UniqueName="Scheme"
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
                    HeaderStyle-HorizontalAlign="Right" UniqueName="MarketPrice" SortExpression="MarketPrice"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="NAV Date" DataFormatString="{0:d}"
                    DataField="NAVDate" HeaderStyle-HorizontalAlign="Right" UniqueName="NAVDate"
                    SortExpression="NAVDate" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Unit Balance" DataField="Units"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Units" SortExpression="Units"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM" DataField="AUM"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM %" DataField="Percentage"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Percentage" SortExpression="Percentage"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Center">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="navFrom" HeaderStyle-HorizontalAlign="Right"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    HeaderText="From Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="navTo" HeaderStyle-HorizontalAlign="Right"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    HeaderText="To Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="diff" HeaderStyle-HorizontalAlign="Right"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    HeaderText="Diff. Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
<div runat="server" style="overflow: scroll;" id="divRgvSchemeWiseAUM" visible="false">
    <telerik:RadGrid ID="rgvSchemeWiseAUM" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true"
        OnNeedDataSource="rgvSchemeWiseAUM_OnNeedDataSource" AllowAutomaticInserts="false"
        ExportSettings-FileName="SchemeWiseAUM Details">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None">
            <Columns>
                <%-- <telerik:GridTemplateColumn HeaderStyle-Width="100px" AllowFiltering="false" UniqueName="action"
                    DataField="action" FooterText="Grand Total:">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandName="Select" runat="server" Text="Details"
                            ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <%-- <telerik:GridBoundColumn HeaderText="AMC" DataField="AMC" UniqueName="AMC" SortExpression="AMC"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" HeaderText="Scheme" DataField="Scheme"
                    UniqueName="Scheme" SortExpression="Scheme" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Product Code" DataField="PASC_AMC_ExternalCode"
                    UniqueName="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Category" DataField="Category" UniqueName="Category"
                    SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="SubCategory" DataField="Subcategory" UniqueName="SubCategory"
                    SortExpression="SubCategory" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="R & T Name" DataField="RnTName" UniqueName="RnTName"
                    SortExpression="RnTName" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Curr NAV" DataField="MarketPrice"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="MarketPrice" SortExpression="MarketPrice"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="NAV Date" DataFormatString="{0:d}"
                    DataField="CMFNP_NAVDate" HeaderStyle-HorizontalAlign="Right" UniqueName="NAVDate"
                    SortExpression="NAVDate" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <%--  <telerik:GridBoundColumn HeaderText="Unit Balance" HeaderStyle-Width="100px" DataField="Units"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Units" SortExpression="Units"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM" DataField="AUM"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="AUM %" DataField="Percentage"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Percentage" SortExpression="Percentage"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Center">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <%-- <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Units" HeaderStyle-HorizontalAlign="Right"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N3}"
                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="navFrom" HeaderStyle-HorizontalAlign="Right"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    HeaderText="From Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="navTo" HeaderStyle-HorizontalAlign="Right"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    HeaderText="To Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="diff" HeaderStyle-HorizontalAlign="Right"
                    UniqueName="Units" SortExpression="Units" AutoPostBackOnFilter="true" AllowFiltering="false"
                    HeaderText="Diff. in Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
<div style="margin: 6px" id="divNote" runat="server">
    <label id="lbl" class="HeaderTextSmall">
        Note:<br />
        1: Only historical data is accessible from this screen. Recent data for the last
        2 Business day will not be available. To view the recent data View Dashboards &
        Net Positions.<br />
        2: Please verify the correctness of To and From date inorder to get the valid data</label>
</div>
<asp:Button ID="btnasp" Visible="false" runat="server" />
<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAgentId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnXWise" runat="server" Visible="false" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnValuationDate" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
