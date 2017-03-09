<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionsView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.AllTransactions" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagName="Pager" TagPrefix="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script language="javascript" type="text/javascript">

    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
</script>

<asp:ScriptManager ID="scriptTransactionView" runat="server">
</asp:ScriptManager>

<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">View MF Transaction</td>
        <td align="right"  style="padding-bottom:2px;">
        <asp:LinkButton ID="lbBack" runat="server" Text="Back" onclick="lbBack_Click" Visible="false" CssClass="FieldName"></asp:LinkButton>
        <asp:ImageButton ID="btnViewTransaction" ImageUrl="~/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Visible="false"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
            onclick="btnViewTransaction_Click"></asp:ImageButton>
        </td>
        </tr>
       
    </table>
</div>
</td>
</tr>
</table>

<%--<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="View MF Transaction"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="HeaderCell">
            
        </td>
    </tr>
</table>--%>
<table>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td colspan="4" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        
    </tr>
    <tr>
    <td align="right">
                                <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date Type :"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                                    runat="server" GroupName="Date" />
                                <asp:Label ID="lblPickDate" runat="server" Text="Date Range" CssClass="Field"></asp:Label>
                                &nbsp;
                                <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                                    runat="server" GroupName="Date" />
                                <asp:Label ID="lblPickPeriod" runat="server" Text="Period" CssClass="Field"></asp:Label>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCustomerGroup" runat="server" CssClass="FieldName" Text="Customer :"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtnAll" AutoPostBack="true" Checked="true" runat="server" GroupName="GroupAll"
                                    Text="All" CssClass="cmbField" OnCheckedChanged="rbtnAll_CheckedChanged" />
                                &nbsp;
                                <asp:RadioButton ID="rbtnGroup" AutoPostBack="true" runat="server" GroupName="GroupAll"
                                    Text="Group" CssClass="cmbField" OnCheckedChanged="rbtnAll_CheckedChanged" />
                            </td>
    </tr>
</table>
<table class="TableBackground">
<tr id="trPeriod" visible="false" runat="server">
                            <td align="right" valign="top">
                                <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                                <span id="Span4" class="spnRequiredField"></span>
                                <br />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                                    CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                                    ValidationGroup="btnGo"> </asp:CompareValidator>
                            </td>
                            <td>
                            &nbsp;&nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
    <tr>
        <td>
            <asp:Label ID="lblFromTran" Text="From" CssClass="Field" runat="server" />
        </td>
        <td>
           <%-- <asp:TextBox ID="txtFromTran" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtFromTran_CalendarExtender" runat="server" TargetControlID="txtFromTran"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" >
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtFromTran_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFromTran" WatermarkText="dd/mm/yyyy" >
            </cc1:TextBoxWatermarkExtender> --%> 
            <telerik:RadDatePicker ID="txtFromTran" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1"  runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFromTran"
                                    CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                                    runat="server" InitialValue="" ValidationGroup="btnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="lblToTran" Text="To" CssClass="Field" runat="server" />
        </td>
        <td>
            <%--<asp:TextBox ID="txtToTran" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtToTran_CalendarExtender" runat="server" TargetControlID="txtToTran"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtToTran_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtToTran" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>--%>
             <telerik:RadDatePicker ID="txtToTran" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" >
                <Calendar ID="Calendar2"  runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtToTran"
                                    CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                                    runat="server" InitialValue="" ValidationGroup="btnSubmit"> </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToTran"
                ErrorMessage="<br />To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtFromTran" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
            </asp:CompareValidator>
        </td>
        <td>
            <asp:Button ID="btnViewTran" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnSubmit" OnClick="btnViewTran_Click" />
        </td>
    </tr>
</table>
<%--<table style="width: 100%;" class="TableBackground" id="tblGV" runat="server">
    <tr id="Tr1" runat="server" visible="true">
        <td>
            <asp:RadioButton ID="rbtnExcel" Text="Excel" runat="server" GroupName="grpExport"
                CssClass="cmbField" />
            <asp:RadioButton ID="rbtnPDF" Text="PDF" runat="server" GroupName="grpExport" CssClass="cmbField" />
            <asp:RadioButton ID="rbtnWord" Text="Word" runat="server" GroupName="grpExport" CssClass="cmbField" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButton ID="rbtnSingle" Text="Current Page" runat="server" GroupName="grpPage"
                CssClass="cmbField" />
            <asp:RadioButton ID="rbtnMultiple" Text="All Pages" runat="server" GroupName="grpPage"
                CssClass="cmbField" />
        </td>
        <td>
            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export"
                CssClass="ButtonField" />
            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" CssClass="ButtonField" />
            <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>    
</table>--%>

<table id="ErrorMessage" align="center" runat="server">
    <tr>
        <td>
            <div class="failure-msg" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<div id="tbl" runat="server" style="margin: 2px;width: 100%;">
   <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="98%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <%--<asp:GridView ID="gvMFTransactions" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" EnableViewState="true" AllowPaging="false" CssClass="GridViewStyle"
                    OnRowCommand="gvMFTransactions_RowCommand" ShowFooter="True" DataKeyNames="TransactionId"
                    OnPageIndexChanging="gvMFTransactions_PageIndexChanging" OnDataBound="gvMFTransactions_DataBound"
                    OnRowDataBound="gvMFTransactions_RowDataBound" OnSorting="gvMFTransactions_Sort">
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" Wrap="False" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle CssClass="PagerStyle" HorizontalAlign="center" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                       
                                                              
                        <asp:ButtonField CommandName="Select" HeaderText="View Details" ShowHeader="True" Text="View Details"
                            ItemStyle-Wrap="false">
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:ButtonField>
                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="ProcessId">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblProcessId" runat="server" Text="ProcessId"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtProcessId" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_TransactionsView_btnTranSchemeSearch');" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcessIdHeader" runat="server" Text='<%# Eval("ADUL_ProcessId").ToString() %>'
                                                ItemStyle-Wrap="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                        <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" Visible="false" />
                        <asp:BoundField DataField="CMFT_SubBrokerCode" HeaderText="SubBrokerCode">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Folio No">
                            <HeaderTemplate>
                                <asp:Label ID="lblFolio" runat="server" Text="Folio No"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtFolioSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_TransactionsView_btnTranSchemeSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFolioNum" runat="server" Text='<%# Eval("Folio Number").ToString() %>'
                                    ItemStyle-Wrap="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Scheme">
                        
                            <HeaderTemplate>
                                <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label>
                               <br />
                                <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_TransactionsView_btnTranSchemeSearch');" />
                            </HeaderTemplate>
                             
                            <ItemTemplate>
                                <asp:LinkButton ID="lblSchemeHeader" CommandName="NavigateToMarketData" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" Text='<%# Eval("Scheme Name").ToString() %>'>
                                </asp:LinkButton>   
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="SubCategoryName">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Transaction Type">
                            <HeaderTemplate>
                                <asp:Label ID="lblTranType" runat="server" Text="Type"></asp:Label>
                                <asp:DropDownList ID="ddlTranType" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTranTypeHeader" runat="server" Text='<%# Eval("Transaction Type").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"
                            HeaderText="Transaction Date">
                            <HeaderTemplate>
                                <asp:Label ID="lblTranDate" runat="server" Text="Date"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlTranDate" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlTranDate_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTranDateHeader" runat="server" Text='<%# Eval("Transaction Date").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Price" HeaderText="Price (Rs)" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount (Rs)" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STT" HeaderText="STT (Rs)" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text="Transaction Status"></asp:Label>
                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" runat="server" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="OK" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Cancel" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Original" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionStatus" runat="server" Text='<%# Eval("Transaction Status").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>
                <div id="dvTransactionsView" runat="server" style="margin: 2px;width: 640px;">
                <telerik:RadGrid ID="gvMFTransactions" runat="server" GridLines="None" AutoGenerateColumns="False" AllowFiltering="true" AllowFilteringByColumn="true"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnItemCommand="gvMFTransactions_OnItemCommand"
                    OnNeedDataSource="gvMFTransactions_OnNeedDataSource" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%"
                    AllowAutomaticInserts="false" ExportSettings-FileName="MF Transaction" > 
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" FileName="MF Transaction" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="TransactionId" 
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                          <telerik:GridTemplateColumn HeaderText="View Details" AllowFiltering="false" FooterText="Grand Total:" HeaderStyle-Wrap="false">
                            <ItemStyle Wrap="false" />
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkView" runat="server" CssClass="cmbField" Text="View Details"
                                    OnClick="lnkView_Click">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                       <%-- <telerik:GridDateTimeColumn DataField="ADUL_ProcessId" HeaderText="ProcessId" AllowFiltering="true"
                                SortExpression="ADUL_ProcessId" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="ADUL_ProcessId" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridDateTimeColumn>--%>
                        
                        <telerik:GridBoundColumn DataField="ADUL_ProcessId" HeaderText="ProcessId" AllowFiltering="true" HeaderStyle-Wrap="false"
                                SortExpression="ADUL_ProcessId" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="ADUL_ProcessId" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                      
                        <telerik:GridBoundColumn DataField="TransactionId" HeaderText="TransactionId" AllowFiltering="false" Visible="false"
                                SortExpression="TransactionId" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="TransactionId" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CMFT_SubBrokerCode" HeaderText="SubBrokerCode" AllowFiltering="false" Visible="false"
                                SortExpression="CMFT_SubBrokerCode" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="CMFT_SubBrokerCode" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Folio Number" HeaderText="Folio No" AllowFiltering="true"
                                SortExpression="Folio Number" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="Folio Number" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridTemplateColumn   AllowFiltering="true" HeaderText="Scheme" ShowFilterIcon="false">
                        <ItemStyle Wrap="false" />
                           <ItemTemplate>
                           <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Scheme" Text='<%# Eval("Scheme Name").ToString() %>' />
                           </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category Name" AllowFiltering="false" HeaderStyle-Wrap="false"
                                SortExpression="PAISC_AssetInstrumentSubCategoryName" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left">
                         <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Transaction Type" HeaderText="Type" AllowFiltering="true" HeaderStyle-Wrap="false"
                                SortExpression="Transaction Type" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="Transaction Type" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Transaction Date" HeaderText="Date" AllowFiltering="true" HeaderStyle-Wrap="false"
                                SortExpression="Transaction Date" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="Transaction Date" FooterStyle-HorizontalAlign="Center">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Price" HeaderText="Price (Rs)" AllowFiltering="false"
                                SortExpression="Price" ShowFilterIcon="false" CurrentFilterFunction="Contains" HeaderStyle-Wrap="false"
                                AutoPostBackOnFilter="true" UniqueName="Price" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n}" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Units" HeaderText="Units" AllowFiltering="false" HeaderStyle-Wrap="false"
                                SortExpression="Units" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="Units" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n}" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="Amount" HeaderText="Amount (Rs)" AllowFiltering="false" HeaderStyle-Wrap="false"
                                SortExpression="Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="Amount" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n}" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="STT" HeaderText="STT (Rs)" AllowFiltering="false" HeaderStyle-Wrap="false"
                                SortExpression="STT" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="STT" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n}" Aggregate="Sum">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Transaction Status" HeaderText="Transaction Status" AllowFiltering="true" HeaderStyle-Wrap="false"
                                SortExpression="Transaction Status" ShowFilterIcon="false" CurrentFilterFunction="Contains" 
                                AutoPostBackOnFilter="true" UniqueName="Transaction Status" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid></div>
            </td>
        </tr>
        </table>
    </asp:Panel>
    
</div>
<%--<asp:Button ID="btnTranSchemeSearch" runat="server" Text="" OnClick="btnTranSchemeSearch_Click"
    BorderStyle="None" BackColor="Transparent" />--%>
<asp:HiddenField ID="hdnRecordCount" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSchemeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranType" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranTrigger" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranDate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSort" runat="server" Visible="false" Value="Name ASC" />
<asp:HiddenField ID="hdnStatus" runat="server" Visible="false"/>
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnProcessIdSearch" runat="server" />