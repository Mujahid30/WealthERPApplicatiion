<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UTIAMCManage.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.UTIAMCManage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            UTI AMC
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td id="tdlblFromDate" runat="server" align="right">
            <asp:Label class="FieldName" ID="lblFromTran" Text="Extract From:" runat="server" />
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
            <asp:Label ID="lblToTran" Text="Extract To:" CssClass="FieldName" runat="server" />
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
        <td>
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" OnClick="btnGo_OnClick"
                Text="Go" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlSeries" runat="server" Width="100%" Visible="false">
    <table id="tblSeries" runat="server" width="80%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvUATAMCList" runat="server" AllowSorting="True" enableloadondemand="True"
                    PageSize="10" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                    ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                    Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="gvAdviserList_OnNeedDataSource">
                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames=""
                        AutoGenerateColumns="false" Width="100%" CommandItemSettings-ShowRefreshButton="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="PASC_AMC_ExternalCode" HeaderStyle-Width="20px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Scheme Code" UniqueName="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode"
                                AllowFiltering="true" Visible="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UFCCode" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="UFC Code" UniqueName="UFCCode"
                                SortExpression="UFCCode" AllowFiltering="true" Visible="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_UinNo" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Batch No." UniqueName="AMFE_UinNo"
                                SortExpression="AMFE_UinNo" AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RemittanceDate" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Remittance Date"
                                UniqueName="RemittanceDate" SortExpression="RemittanceDate" AllowFiltering="true"
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Investmentdate" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Investment date"
                                UniqueName="Investmentdate" SortExpression="Investmentdate" AllowFiltering="true"
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NS" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="N/S" UniqueName="NS"
                                SortExpression="NS" AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="investedAmount" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Amount" UniqueName="investedAmount"
                                SortExpression="investedAmount" AllowFiltering="true" DataFormatString="{0:N4}">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
