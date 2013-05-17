<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedMFAutoSIPTransactions.ascx.cs" Inherits="WealthERP.Uploads.RejectedMFAutoSIPTransactions" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script src="Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="Scripts/webtoolkit.jscrollable.js" type="text/javascript"></script>

<script src="Scripts/webtoolkit.scrollabletable.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Auto Systematic Transaction Exceptions
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px" 
                                onclick="btnExport_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>

<div id="divConditional" runat="server">
    <table class="TableBackground" cellspacing="0" cellpadding="6">
        <tr>                   
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromSIP" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server" align="left">
                <telerik:RadDatePicker ID="txtFromSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="dvTransactionDate" runat="server" class="dvInLine">
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtSIPDate" ControlToValidate="txtFromSIP" ErrorMessage="<br />Please select a From Date"
                        CssClass="cvPCG" Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtFromSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtToSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="Div1" runat="server" class="dvInLine">
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToSIP"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtToSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToSIP"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtFromSIP" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
                </asp:CompareValidator>
            </td>
            <td id="tdBtnViewRejetcs" runat="server">
                <asp:Button ID="btnViewSIP" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewSIP_Click" />
            </td>
        </tr>
    </table>
</div>
<br />
<asp:Panel ID="pnlAutoSIP" runat="server" class="Landscape" Width="100%"
    ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="6">
     <tr>
         <td>
                <telerik:RadGrid ID="gvAutoSIPReject" runat="server" GridLines="None" AutoGenerateColumns="False"
                    AllowFiltering="true" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                    Skin="Telerik" EnableEmbeddedSkins="false"
                    Width="80%" AllowFilteringByColumn="true" AllowAutomaticInserts="false" EnableViewState="true"
                    ShowFooter="true" OnNeedDataSource="gvSIPReject_NeedDataSource" OnItemDataBound="gvAutoSIPReject_ItemDataBound" >
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView TableLayout="Auto" AllowFilteringByColumn="true" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None">
                        <Columns>                           
                            <telerik:GridBoundColumn DataField="WRR_RejectReasonDescription" AllowFiltering="true"
                                HeaderText="Reject Reason" HeaderStyle-Width="200px" UniqueName="WRR_RejectReasonCode"
                                SortExpression="WRR_RejectReasonCode" AutoPostBackOnFilter="false" ShowFilterIcon="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxRR" Width="250px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents1_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("WRR_RejectReasonCode").CurrentFilterValue %>'
                                        runat="server">
                                        <%--OnPreRender="rcbContinents_PreRender"--%>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                        <script type="text/javascript">
                                            function InvesterNameIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                //////                                                    sender.value = args.get_item().get_value();
                                                tableView.filter("WRR_RejectReasonCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_InvName" AllowFiltering="true" HeaderText="Investor Name"
                                HeaderStyle-Width="85px" UniqueName="CMFSCS_InvName" SortExpression="CMFSCS_InvName"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderText="Folio" AllowFiltering="true"
                                HeaderStyle-Width="80px" UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="PA_AMCName" HeaderText="AMC"
                                AllowFiltering="false" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="PA_AMCName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="PA_AMCName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" ShowFilterIcon="false" FilterControlWidth="250px"
                                AllowFiltering="true" HeaderStyle-Width="250px" HeaderText="Scheme" UniqueName="PASP_SchemePlanName"
                                CurrentFilterFunction="Contains" SortExpression="PASP_SchemePlanName" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                           
                            <telerik:GridBoundColumn DataField="CMFSS_StartDate" HeaderText="Start Date" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSS_StartDate" DataFormatString="{0:d}"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSS_StartDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="CMFSS_EndDate" HeaderText="End Date" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSS_EndDate" DataFormatString="{0:d}"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSS_EndDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="CMFSS_SystematicDate" HeaderText="Systematic Date"
                                AllowFiltering="false" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSS_SystematicDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSS_SystematicDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="XF_Frequency" HeaderText="Frequency" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="XF_Frequency"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="XF_Frequency" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" DataField="CMFSS_Amount"
                                AllowFiltering="false" HeaderStyle-Width="75px" HeaderText="Amount" UniqueName="CMFSS_Amount"
                                Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="PTQ_SystematicDate" HeaderText="Transaction Date"
                                AllowFiltering="false" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="PTQ_SystematicDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"  DataFormatString="{0:d}"
                                UniqueName="PTQ_SystematicDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>                           
                            
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="false" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>