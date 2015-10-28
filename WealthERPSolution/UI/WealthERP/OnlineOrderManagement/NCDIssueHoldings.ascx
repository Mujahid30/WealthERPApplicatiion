<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueHoldings.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.NCDIssueHoldings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
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
</style>
<asp:UpdatePanel ID="upBHGrid" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    Holding
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExportSummary_OnClick"
                                        Height="25px" Width="25px" Visible="false"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="divConditional" runat="server" style="padding-top: 4px" visible="false">
            <table class="TableBackground" cellpadding="2">
                <tr>
                    <td id="td1" runat="server">
                        <asp:Label runat="server" class="FieldName" Text="Account:" ID="Label1"></asp:Label>
                        <asp:DropDownList CssClass="cmbField" ID="ddlAccount" runat="server" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td id="tdlblFromDate" runat="server" align="right">
                        <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
                    </td>
                    <td id="tdTxtFromDate" runat="server">
                        <telerik:RadDatePicker ID="txtOrderFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <div id="dvTransactionDate" runat="server" class="dvInLine">
                            <span id="Span1" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtOrderFrom"
                                ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                                runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                                Type="Date" ControlToValidate="txtOrderFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                                Display="Dynamic"></asp:CompareValidator>
                        </div>
                    </td>
                    <td id="tdlblToDate" runat="server">
                        <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
                    </td>
                    <td id="tdTxtToDate" runat="server">
                        <telerik:RadDatePicker ID="txtOrderTo" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <div id="Div1" runat="server" class="dvInLine">
                            <span id="Span2" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrderTo"
                                ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                                runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                                Type="Date" ControlToValidate="txtOrderTo" CssClass="cvPCG" Operator="DataTypeCheck"
                                Display="Dynamic"></asp:CompareValidator>
                        </div>
                        <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtOrderTo"
                            ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                            ControlToCompare="txtOrderFrom" CssClass="cvPCG" ValidationGroup="btnViewOrder"
                            Display="Dynamic">
                        </asp:CompareValidator>
                    </td>
                    <td id="tdBtnOrder" runat="server">
                        <asp:Button ID="btnViewOrder" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnViewOrder"
                            OnClick="btnViewOrder_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <table id="tblCommissionStructureRule" runat="server" width="100%">
            <tr>
                <td>
                    <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="gvBHList" runat="server" GridLines="None" AutoGenerateColumns="False"
                                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                        Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                                        OnNeedDataSource="gvBHList_OnNeedDataSource" OnItemCommand="gvBHList_OnItemCommand"
                                        OnItemDataBound="gvBHList_OnItemDataBound">
                                        <ExportSettings FileName="Details" HideStructureColumns="true" ExportOnlyData="true">
                                        </ExportSettings>
                                        <MasterTableView DataKeyNames="AIM_IssueId,CO_OrderId" Width="100%" AllowMultiColumnSorting="True"
                                            AutoGenerateColumns="false" CommandItemDisplay="None">
                                            <Columns>
                                                <telerik:GridTemplateColumn ItemStyle-Width="80Px" AllowFiltering="false" Visible="false"
                                                    HeaderText="Action">
                                                    <ItemTemplate>
                                                        <telerik:RadComboBox ID="ddlMenu" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                                            Skin="Telerik" AllowCustomText="true" Width="120px" AutoPostBack="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                                                </telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                                                    runat="server"></telerik:RadComboBoxItem>
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" SortExpression="AIM_IssueId"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                               
                                                <telerik:GridBoundColumn DataField="AIM_IssueName" SortExpression="AIM_IssueName"
                                                    AutoPostBackOnFilter="true" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" AllowFiltering="false" HeaderText="Issue Name" UniqueName="AIM_IssueName">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AIM_AllotmentDate" SortExpression="AIM_AllotmentDate"
                                                    AutoPostBackOnFilter="true" HeaderStyle-Width="40px" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" AllowFiltering="false" HeaderText="Allotment Date" UniqueName="AIM_AllotmentDate">
                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AllotedQty" SortExpression="AllotedQty" AutoPostBackOnFilter="true"
                                                    HeaderStyle-Width="50px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    AllowFiltering="false" HeaderText="Alloted Quantity" UniqueName="AllotedQty">
                                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="OrderedQty" SortExpression="OrderedQty" AutoPostBackOnFilter="true"
                                                    HeaderStyle-Width="50px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    AllowFiltering="false" HeaderText="Order Quantity" UniqueName="OrderedQty">
                                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                               
                                                <telerik:GridBoundColumn DataField="HoldingAmount" AllowFiltering="false" HeaderText="Alloted Amount"
                                                    HeaderStyle-Width="100px" UniqueName="HoldingAmount">
                                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                
                                           
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnAccount" runat="server" />
