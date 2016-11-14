<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineAdviserCustomerTransctionBook.ascx.cs" Inherits="WealthERP.OnlineOrderBackOffice.OnlineAdviserCustomerTransctionBook" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:ScriptManager ID="ScriptManager" runat="server">
<Services >
<asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
</Services>
</asp:ScriptManager>
<script type="text/javascript">

    var isItemSelected = false;

    //Handler for textbox blur event
    function checkItemSelected(txtPanNumber) {
        var returnValue = true;
        if (!isItemSelected) {
            if (txtPanNumber.value != "") {
                txtPanNumber.focus();
                alert("Please select Pan Number from the Pan list only");
                txtPanNumber.value = "";
                returnValue = false;
            }
        }
        return returnValue;
    }
    
</script>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
     
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }

    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
    }
    function GetRealInvester(source, eventArgs) {
        isItemSelected = true;
      
        document.getElementById("<%= hdnIsRealInvester.ClientID %>").value = eventArgs.get_value();

        return false;
    }
    
    
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Transaction Book
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table >
    <tr id="trAMC" runat="server">
        <td id="tdlblAmc" runat="server" align="left">
            <asp:Label runat="server" class="FieldName" Text="AMC:" ID="lblAccount"></asp:Label>
        </td>
        <td id="tdlblFromDate" runat="server" align="right">
            <asp:DropDownList CssClass="cmbField" ID="ddlAmc" runat="server" AutoPostBack="false"
                Width="300px">
            </asp:DropDownList>
        </td>
       
       <td align="left" width="10%">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"
                Visible="false"></asp:Label>
            <asp:Label ID="lblSchemeList" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right" >
            <asp:DropDownList ID="ddlSchemeList" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="500px">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="false"
                Visible="false">
            </asp:DropDownList>
        </td>
      
    </tr>
    </table>
    <table>
    <tr>
      <td>
            <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
        </td>
        <td  id="tdTxtFromDate" runat="server">
            <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtFrom"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
          
        </td>
       
      
          <td  id="tdlblToDate" runat="server">
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
         
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTo"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtTo" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtTo"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtFrom" CssClass="cvPCG" ValidationGroup="btnViewTransaction"
                Display="Dynamic">
            </asp:CompareValidator>
        </td>
       
         <td id="tdCustomerGroup" runat="server" colspan="2">
            <asp:Label ID="lblCustomerGroup" runat="server" CssClass="FieldName" Text="Search Customer:"></asp:Label>
            <asp:DropDownList ID="ddlsearchcustomertype" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlsearchcustomertype_OnSelectedIndexChanged">
                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
            </asp:DropDownList>
        </td>
       
        <td align="left">
            <asp:Label ID="lblCustomerSearch" Visible="false" runat="server" CssClass="FieldName"
                Text="Customer:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOptionSearch" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlOptionSearch_OnSelectedIndexChanged" Width="150" Visible="false">
                <asp:ListItem Selected="true" Text="Select" Value="Select" />
                <asp:ListItem Text="Name" Value="Name" />
                <asp:ListItem Text="PAN" Value="Panno" />
                <asp:ListItem Text="Client Code" Value="Clientcode" />
            </asp:DropDownList>
        </td>
        <td align="left" id="tdtxtPansearch" runat="server" visible="false">
            <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="false" onclientClick="ShowIsa()" Width="150px">
            </asp:TextBox><span id="Span3" class="spnRequiredField"></span>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtPansearch"
                WatermarkText="Enter few characters of Pan" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtPansearch_autoCompleteExtender" runat="server"
                TargetControlID="txtPansearch" ServiceMethod="GetAdviserCustomerPan" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="1" CompletionInterval="0"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPansearch"
                ErrorMessage="<br />Please Enter Pan number" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td align="left" id="tdtxtClientCode" runat="server" visible="false">
            <asp:TextBox ID="txtClientCode" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="false" onclientClick="ShowIsa()" ></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtClientCode"
                WatermarkText="Enter few characters of Client Code" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtClientCode_autoCompleteExtender" runat="server"
                TargetControlID="txtClientCode" ServiceMethod="GetCustCode" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtClientCode"
                ErrorMessage="<br />Please Enter Client Code" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td align="left" id="tdtxtCustomerName" runat="server" visible="false">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" Width="250px">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" TargetControlID="txtCustomerName"
                WatermarkText="Enter Three Characters of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomerName" ServiceMethod="GetAdviserAllCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="3" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCustomerName"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
       
    </tr>
    <tr>
        <td id="tdBtnOrder" runat="server" colspan="4">
            <asp:Button ID="btnViewTransaction" runat="server" CssClass="PCGButton" Text="Go"
                ValidationGroup="btnViewTransaction" OnClick="btnViewTransaction_Click" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlTransactionBook" runat="server" class="Landscape" Width="100%"
    ScrollBars="Horizontal" Visible="false">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <div id="dvTransactionsView" runat="server" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid ID="gvTransationBookMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                        allowfiltering="true" AllowFilteringByColumn="true" PageSize="10" AllowSorting="true"
                        AllowPaging="True" ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                        Width="120%" AllowAutomaticInserts="false" AllowCustomPaging="true" OnNeedDataSource="gvTransationBookMIS_OnNeedDataSource"
                        OnItemCommand="gvTransationBookMIS_ItemCommand">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="CMFT_MFTransId" Width="100%" AllowMultiColumnSorting="True"
                            AllowFilteringByColumn="true" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridBoundColumn Visible="true" DataField="Name" HeaderText="Customer" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Name" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="c_CustCode" HeaderText="Client Code" AllowFiltering="true"
                                    SortExpression="c_CustCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="c_CustCode" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="c_PANNum" HeaderText="PAN" AllowFiltering="true"
                                    SortExpression="c_PANNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="c_PANNum" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CMFT_MFTransId" HeaderText="Transaction ID"
                                    AllowFiltering="false" SortExpression="CMFT_MFTransId" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_MFTransId"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%-- <telerik:GridBoundColumn DataField="OrderNo" AllowFiltering="true" HeaderText="Order No."
                                    UniqueName="OrderNo" SortExpression="OrderNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="PA_AMCName" HeaderText="Fund Name" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="PA_AMCName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="PA_AMCName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderText="Folio No." AllowFiltering="true"
                                    SortExpression="CMFA_FolioNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CMFA_FolioNum" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="PAIC_AssetInstrumentCategoryName"
                                    HeaderText="Category" AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PAIC_AssetInstrumentCategoryName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PAIC_AssetInstrumentCategoryName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="Scheme Name"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PASP_SchemePlanName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PASP_SchemePlanName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" HeaderText="Category"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="PAIC_AssetInstrumentCategoryName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PAIC_AssetInstrumentCategoryName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridTemplateColumn AllowFiltering="true" DataField="Scheme Name" AutoPostBackOnFilter="true"
                                                        HeaderText="Scheme" ShowFilterIcon="false" FilterControlWidth="280px">
                                                        <ItemStyle Wrap="false" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Scheme" Text='<%# Eval("Scheme Name").ToString() %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn DataField="WMTT_TransactionClassificationName" HeaderText="Type"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="WMTT_TransactionClassificationName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="WMTT_TransactionClassificationName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_Price" HeaderText="Actioned NAV" AllowFiltering="false"
                                    SortExpression="CMFT_Price" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    HeaderStyle-Wrap="false" AutoPostBackOnFilter="true" UniqueName="CMFT_Price"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n4}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_TransactionDate" HeaderText="Date" AllowFiltering="false"
                                    AllowSorting="true" HeaderStyle-Wrap="false" SortExpression="CMFT_TransactionDate"
                                    ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CMFT_TransactionDate" FooterStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFOD_DividendOption" HeaderText="Dividend Type"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFOD_DividendOption"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFOD_DividendOption" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn Visible="false" DataField="DivedendFrequency" HeaderText="Divedend Frequency"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="DivedendFrequency"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="DivedendFrequency" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="CMFT_Units" HeaderText="Units" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CMFT_Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CMFT_Units" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n3}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_Amount" HeaderText="Amount (Rs)" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CMFT_Amount" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_Amount"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_ExternalBrokerageAmount" HeaderText="Brokerage(Rs)" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CMFT_ExternalBrokerageAmount" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_ExternalBrokerageAmount"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_STT" HeaderText="STT (Rs)" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CMFT_STT" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_STT"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CP_PortfolioName" HeaderText="Portfolio Name" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CP_PortfolioName" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CP_PortfolioName"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_Area" HeaderText="AREA" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CMFT_Area" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_Area"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="CMFT_EUIN" HeaderText="EUIN" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CMFT_EUIN" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_EUIN"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IsT15" HeaderText="City Group" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="IsT15" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="IsT15"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="WTS_TransactionStatus" HeaderText="TransactionStatus" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="WTS_TransactionStatus" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="WTS_TransactionStatus"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <%-- <telerik:GridBoundColumn Visible="false" DataField="CurrentNav" HeaderText="Current NAV"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CurrentNav" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CurrentNav"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n3}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--   <telerik:GridBoundColumn Visible="false" DataField="CMFT_ExternalBrokerageAmount"
                                    HeaderText="Brokerage(Rs)" AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CMFT_ExternalBrokerageAmount"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFT_ExternalBrokerageAmount" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn Visible="false" DataField="STT" HeaderText="STT (Rs)" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="STT" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="STT" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="Channel" HeaderText="Channel" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="Channel" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Channel" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%--     <telerik:GridBoundColumn Visible="false" DataField="ADUL_ProcessId" HeaderText="Process ID"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="ADUL_ProcessId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="ADUL_ProcessId" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%-- <telerik:GridBoundColumn Visible="false" DataField="CMFT_Area" HeaderText="Area"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_Area" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_Area"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn Visible="false" DataField="CMFT_EUIN" HeaderText="EUIN"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_EUIN" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_EUIN"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--  <telerik:GridBoundColumn Visible="false" DataField="TransactionStatus" HeaderText="Transaction Status"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="TransactionStatus"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="TransactionStatus"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                    <%-- <FilterTemplate>
                                                            <telerik:RadComboBox ID="RadComboBoxTS" AutoPostBack="true" AllowFiltering="true"
                                                                CssClass="cmbField" Width="100px" IsFilteringEnabled="true" AppendDataBoundItems="true"
                                                                OnPreRender="Transaction_PreRender" EnableViewState="true" OnSelectedIndexChanged="RadComboBoxTS_SelectedIndexChanged"
                                                                SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("TransactionStatus").CurrentFilterValue %>'
                                                                runat="server">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="ALL" Value="" Selected="false"></telerik:RadComboBoxItem>
                                                                    <telerik:RadComboBoxItem Text="OK" Value="OK" Selected="false"></telerik:RadComboBoxItem>
                                                                    <telerik:RadComboBoxItem Text="Cancel" Value="Cancel" Selected="false"></telerik:RadComboBoxItem>
                                                                    <telerik:RadComboBoxItem Text="Original" Value="Original" Selected="false"></telerik:RadComboBoxItem>
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                                                <script type="text/javascript">
//                                                                    function TransactionIndexChanged(sender, args) {
//                                                                        var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
//                                                                        //////                                                    sender.value = args.get_item().get_value();
//                                                                        tableView.filter("RadComboBoxTS", args.get_item().get_value(), "EqualTo");
                                                                    }
                                                                </script>
                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="CMFT_ELSSMaturityDate" DataFormatString="{0:dd/MM/yyyy}"
                                    AllowFiltering="false" HeaderText="Maturity Date" UniqueName="CMFT_ELSSMaturityDate"
                                    SortExpression="CMFT_ELSSMaturityDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"
                                    AllowFiltering="false" HeaderText="Request Date/Time" UniqueName="CO_OrderDate"
                                    SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Co_OrderId" AllowFiltering="false" HeaderText="Order No."
                                    UniqueName="Co_OrderId" SortExpression="Co_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CMFT_CreatedOn" HeaderText="Add Date (System)"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CMFT_CreatedOn"
                                    ShowFilterIcon="false" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFT_CreatedOn" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdncustomername" runat="server" />
<asp:HiddenField ID="hdnamcname" runat="server" />
<asp:HiddenField ID="hdnfoliono" runat="server" />
<asp:HiddenField ID="hdnschemename" runat="server" />
<asp:HiddenField ID="hdncustcode" runat="server" />
<asp:HiddenField ID="hdnpanno" runat="server" />
<asp:HiddenField ID="hdntype" runat="server" />
<asp:HiddenField ID="hdndividenttype" runat="server" />
<asp:HiddenField ID="hdnOrderno" runat="server" />
<asp:HiddenField ID="txtCustomerId" runat="server" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
<asp:HiddenField ID="hdnIsMFKYC" runat="server" />
<asp:HiddenField ID="hdnIsActive" runat="server" />
<asp:HiddenField ID="hdnIsProspect" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
<asp:HiddenField ID="hdnSystemId" runat="server" />
<asp:HiddenField ID="hdnClientId" runat="server" />
<asp:HiddenField ID="hdnName" runat="server" />
<asp:HiddenField ID="hdnGroup" runat="server" />
<asp:HiddenField ID="hdnPAN" runat="server" />
<asp:HiddenField ID="hdnBranch" runat="server" />
<asp:HiddenField ID="hdnArea" runat="server" />
<asp:HiddenField ID="hdnCity" runat="server" />
<asp:HiddenField ID="hdnProcessId" runat="server" />
<asp:HiddenField ID="hdnSystemAddDate" runat="server" />
<asp:HiddenField ID="hdncustomerCategoryFilter" runat="server" />
<asp:HiddenField ID="hdnPincode" runat="server" />
<asp:HiddenField ID="hdnIsRealInvester" runat="server" />
<asp:HiddenField ID="hdnRequestId" runat="server" />
  <asp:HiddenField ID="hdnCustomerNameSearch" runat="server" Visible="false" />
                    <asp:HiddenField ID="hdnSchemeSearch" runat="server" Visible="false" />
