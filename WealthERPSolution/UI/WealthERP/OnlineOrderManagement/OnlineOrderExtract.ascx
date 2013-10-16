<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineOrderExtract.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.OnlineOrderExtract" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Order Extract
                        </td>
                        <%--<td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>--%>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div id="divConditional" runat="server">
    <table class="TableBackground" cellpadding="2">
        <tr>
            <td id="tdlblRejectReason" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Product For MF:" ID="lblProduct"></asp:Label>
            </td>
            <td id="tdProduct" runat="server">
                <asp:DropDownList CssClass="cmbField" ID="ddlProduct" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td id="td1" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Extraxt Type:" ID="Label1"></asp:Label>
            </td>
            <td id="td2" runat="server">
                <asp:DropDownList CssClass="cmbField" ID="ddlExtractType" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td id="td3" runat="server">
                <asp:Label runat="server" class="FieldName" Text="R&T:" ID="Label2"></asp:Label>
            </td>
            <td id="td4" runat="server">
                <asp:DropDownList CssClass="cmbField" ID="ddlTandT" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server">
                <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
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
                    <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtFrom"
                        ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtTo" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar def ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="Div1" runat="server" class="dvInLine">
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTo"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtTo" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtTo"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtFrom" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
                </asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td id="tdBtnOrder" runat="server">
                <asp:Button ID="btdExtract" runat="server" CssClass="PCGButton" Text="Start Extract"
                    OnClick="btnExtract_Click" />
            </td>
        </tr>
    </table>
</div>
&nbsp;
&nbsp;
<table>
    <tr>
        <td>
        </td>
    </tr>
</table>
<div id="divconditionalStatus" runat="server">
    <table class="TableBackground" cellpadding="2">
        <tr>
            <td id="td5" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Extract Status:" ID="Label3"></asp:Label>
            </td>
            <td id="td6" runat="server">
                <td>
                    <asp:Label ID="lblExtractStatus" runat="server" CssClass="FieldName"></asp:Label>
                </td>
            </td>
            <td id="td15" runat="server">
                <asp:Button ID="btnPreview" runat="server" CssClass="PCGButton" Text="Preview file"
                    OnClick="btnPreview_Click" />
            </td>
        </tr>
    </table>
</div>
&nbsp;
&nbsp;
<div id="divConditionalGenerate" runat="server">
    <table class="TableBackground" cellpadding="2">
        <tr>
            <td id="td10" runat="server">
                <asp:Label runat="server" class="FieldName" Text="File format:" ID="Label6"></asp:Label>
            </td>
            <td id="td11" runat="server">
                <asp:DropDownList CssClass="cmbField" ID="ddlfileFormat" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td id="td7" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Location to store the file:" ID="Label4"></asp:Label>
            </td>
            <td id="td8" runat="server">
                <td>
                    <asp:Label ID="lbllocation" runat="server" CssClass="FieldName"></asp:Label>
                </td>
            </td>
            <tr>
                <td id="td9" runat="server">
                    <asp:Button ID="btnGenerate" runat="server" CssClass="PCGButton" Text="Generate file"
                        OnClick="btnGenerateFile_Click" />
                </td>
            </tr>
        </tr>
    </table>
</div>
&nbsp;
&nbsp;
&nbsp;
&nbsp;
<%--&nbsp--%>
<div id="DivMISGrid" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table cellspacing="0" cellpadding="3" width="100%">
                        <tr>
                            <td align="left">
                                Extract MIS
                            </td>
                            <td align="right" style="width: 10px">
                                <asp:ImageButton Visible="false" ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                    OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlExtractMIS" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
        Visible="false">
        <table width="100%">
            <tr id="trExportFilteredDupData" runat="server">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="gvExtractMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" Width="102%"
                        AllowAutomaticInserts="false">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                        </ExportSettings>
                        <MasterTableView Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                            CommandItemDisplay="None">
                            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                                ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="" AllowFiltering="true" HeaderText="Product"
                                    UniqueName="" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="" AllowFiltering="true" HeaderText="R&T" UniqueName=""
                                    SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    HeaderStyle-Width="75px" FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="" HeaderText="File Name" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="100px" UniqueName="" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="" AllowFiltering="true" HeaderText="Extract Period/Time"
                                    UniqueName="" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="" AllowFiltering="false" HeaderText="No. of Records Extracted"
                                    UniqueName="" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="" AllowFiltering="false" HeaderText="Category"
                                    UniqueName="" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="" HeaderText="Sub Category" AllowFiltering="true"
                                    SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="300px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="" HeaderText="Sub Sub Category" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="" FooterStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
