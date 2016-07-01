<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityTransactionsView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.EquityTransactionsView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>


<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script language="javascript" type="text/javascript">
    function FilterRows(sender, args) {
     
        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
        alert(tableView);
        tableView.filter("TradeDate", args.get_item().get_value(), "EqualTo");
    }
</script>

<script language="javascript" type="text/javascript">
    function ShowAlertToDelete() {

        var bool = window.confirm('Are you sure you want to delete this transaction?');

        if (bool) {

            document.getElementById("ctrl_EquityTransactionsView_hdnStatusValue").value = 1;
            document.getElementById("ctrl_EquityTransactionsView_btndelete").click();

            return false;

        }
        else {

            document.getElementById("ctrl_EquityTransactionsView_hdnStatusValue").value = 0;
            document.getElementById("ctrl_EquityTransactionsView_btndelete").click();
            return true;
        }
    }
</script>

<script type="text/javascript">
    function check(sender, eventArgs) {
        var d = new Date();
        var datepicker = $find("<%= txtFromTran.ClientID %>");
        var d1 = document.getElementById('<%=txtFromTran.ClientID %>').value;
        d1 = d1.split("-");
        var s1 = new Date(d1[0], d1[1] - 1, d1[2], 0, 0, 0).getTime();
        var s = d.getTime();

        if (s1 > s) {

            alert("Warning! - Date Cannot be in the future");
            datepicker.set_selectedDate(d);

        }

    }
</script>

<script type="text/javascript">
    function check1(sender, eventArgs) {
        var d = new Date();
        var datepicker = $find("<%= txtToTran.ClientID %>");
        var d1 = document.getElementById('<%=txtToTran.ClientID %>').value;
        d1 = d1.split("-");
        var s1 = new Date(d1[0], d1[1] - 1, d1[2], 0, 0, 0).getTime();
        var s = d.getTime();

        if (s1 > s) {

            alert("Warning! - Date Cannot be in the future");
            datepicker.set_selectedDate(d);

        }

    }
</script>

<script language="javascript" type="text/javascript">
    function Print_Click(div, btnID) {
        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=200,height=100,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();

        var btn = document.getElementById(btnID);
        btn.click();
    } function DownloadScript() {
        if (document.getElementById('<%= gvEquityTransactions.ClientID %>') == null) {
            alert("No records to export");
            return false;
        }

        btn.click();
    }

    function setPageType(pageType) {
        document.getElementById('<%= hdnDownloadPageType.ClientID %>').value = pageType;
    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
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

<script language="javascript" type="text/javascript">
    function checkAllBoxesMf() {
        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvEquityTransactions.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chk_id";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<style type="text/css">
    .font
    {
        font-family: Calibri;
        font-size: 12px;
    }
    .Newfont
    {
        font-family: Calibri;
        font-size: 12px;
    }
</style>
<table id="Table1" style="width: 100%;" class="TableBackground" runat="server">
    <tr>
        <td colspan="2">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            View Equity Transactions
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                Height="20px" Width="25px" OnClick="imgBtnGvEquityTransactions_OnClick" Visible="false">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
</table>
<table class="TableBackground">
    <tr>
        <td class="rightField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="ddlPortfolio" runat="server" CssClass="cmbField" EnableEmbeddedSkins="false"
                Skin="Telerik" AllowCustomText="true" Width="120px">
            </telerik:RadComboBox>
        </td>
        <td>
        </td>
        <td align="right">
            <asp:Label ID="lblFromTran" Text="From :" CssClass="FieldName" runat="server" />
        </td>
        <td>
            <telerik:RadDatePicker ID="txtFromTran" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                    DateFormat="d/M/yyyy">
                </DateInput>
                <ClientEvents OnDateSelected="check" />
            </telerik:RadDatePicker>
            <div id="dvTransactionDate" runat="server" class="dvInLine">
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtFromTran"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtFromTran" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
        </td>
        <td>
            <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
        </td>
        <td>
            <telerik:RadDatePicker ID="txtToTran" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
                    DateFormat="d/M/yyyy">
                </DateInput>
                <ClientEvents OnDateSelected="check1" />
            </telerik:RadDatePicker>
            <div id="Div1" runat="server" class="dvInLine">
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToTran"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtToTran" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToTran"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtFromTran" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
            </asp:CompareValidator>
        </td>
        <td>
            <asp:Label ID="LblType" Text="Currency :" CssClass="FieldName" runat="server" />
        </td>
        <td>
            <asp:DropDownList ID="ddl_type" runat="server" Width="150px" CssClass="cmbField" >
                <asp:ListItem Text="INR" Value="INR">INR</asp:ListItem>
                <asp:ListItem Text="Dollar" Value="$">USD</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnViewTran" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewTran_Click" />
        </td>
    </tr>
</table>
<table id="ErrorMessage" align="center" runat="server" visible="false">
    <tr>
        <td>
            <div class="failure-msg" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<br />
<div id="tbl" runat="server">
    <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
        <telerik:RadGrid ID="gvEquityTransactions" runat="server" GridLines="None" AutoGenerateColumns="False"
            OnItemCommand="gvEquityTransactions_OnItemDataBound" PageSize="10" AllowSorting="true"
            AllowPaging="True" ShowStatusBar="True" ShowFooter="true" OnItemDataBound="RadGrid1_DataBound"
            Skin="Telerik" EnableEmbeddedSkins="false" Width="200%" AllowFilteringByColumn="true"
            AllowAutomaticInserts="false" ExportSettings-FileName="Equity Transaction Details"
            OnNeedDataSource="gvEquityTransactions_OnNeedDataSource" HeaderStyle-CssClass="Newfont">
            <ExportSettings HideStructureColumns="true">
            </ExportSettings>
            <MasterTableView DataKeyNames="TransactionId" Width="100%" AllowMultiColumnSorting="True"
                AutoGenerateColumns="false" CommandItemDisplay="None">
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"  HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <telerik:RadComboBox ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                Skin="Telerik" AllowCustomText="true" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="Select"
                                        Selected="true"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                        runat="server"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                        runat="server"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem ImageUrl="~/Images/delete.png" Text="Delete" Value="Delete"
                                        runat="server"></telerik:RadComboBoxItem>
                                </Items>
                            </telerik:RadComboBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                        HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <input id="chkIdAll" name="chkIdAll" type="checkbox"  onclick="checkAllBoxesMf()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_id" runat="server" />
                            <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("TransactionId").ToString()%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="Scrip Name" DataField="Scheme Name" UniqueName="Scheme Name"
                        FooterText="Grand Total" HeaderStyle-Width="250px" SortExpression="Scheme Name" AllowFiltering="true" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TradeDate" SortExpression="TradeDate" AutoPostBackOnFilter="true"
                        HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" AllowFiltering="true" 
                        HeaderText="TradeDate" UniqueName="TradeDate" DataFormatString="{0:d-MMM-yyy}" ShowFilterIcon="false"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        <FilterTemplate>
                            <telerik:RadDatePicker ID="calFilter" runat="server">
                            </telerik:RadDatePicker>
                        </FilterTemplate>
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn HeaderText="Type" DataField="Transaction Type" UniqueName="Transaction Type"
                         HeaderStyle-Width="110px" SortExpression="Transaction Type" AutoPostBackOnFilter="true" AllowFiltering="true"
                        CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="Quantity" HeaderText="Shares"
                         HeaderStyle-Width="110px" Aggregate="Sum" DataFormatString="{0:0}" ItemStyle-HorizontalAlign="Right" SortExpression="Quantity"
                        FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" FooterStyle-CssClass="Newfont">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Rate (Rs)" DataField="Rate" UniqueName="Rate" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" SortExpression="Rate" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N4}"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    
                    <telerik:GridBoundColumn HeaderText="Gross Consideration (Rs)" DataField="TradeTotal" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="140px" UniqueName="TradeTotal" SortExpression="TradeTotal" AutoPostBackOnFilter="true"
                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" Aggregate="Sum"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        <FooterStyle />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Brokerage (Rs)" DataField="Brokerage" UniqueName="Brokerage" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" SortExpression="Brokerage" AutoPostBackOnFilter="true" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        DataFormatString="{0:N2}" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Rate Inclusive Brokerage (Rs)" DataField="Net Rate With Brokerage" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" UniqueName="Net Rate With Brokerage" SortExpression="Net Rate With Brokerage"
                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn> 
                    <telerik:GridBoundColumn HeaderText="Gross Consideration with Brokerage (Rs)" DataField="Net Trade Consideration With Brokerage" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="140px" UniqueName="Net Trade Consideration With Brokerage" SortExpression="Net Trade Consideration With Brokerage"
                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        FooterStyle-HorizontalAlign="Right" Aggregate="Sum" DataFormatString="{0:N2}"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Net Rate Inclusive All Charges (Rs)" DataField="Net Rate With All Charges" FooterStyle-CssClass="Newfont"
                       ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="110px" UniqueName="Net Rate With All Charges" SortExpression="Net Rate With All Charges"
                        AutoPostBackOnFilter="true" DataFormatString="{0:N4}" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Net Consideration Of All Charges (Rs)" DataField="Gross Trade Consideration" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="140px" UniqueName="Gross Trade Consideration" SortExpression="Gross Trade Consideration"
                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" Aggregate="Sum"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="FOREx Rate" DataField="ForExRate" UniqueName="ForExRate" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" SortExpression="ForExRate" AutoPostBackOnFilter="true" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        DataFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Locked FOREx Rate" DataField="FXCurencyRate" UniqueName="FXCurencyRate" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" SortExpression="FXCurencyRate" AutoPostBackOnFilter="true" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        DataFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Mkt Closing FOREx Rate" DataField="MktClosingForexRate" UniqueName="MktClosingForexRate" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" SortExpression="MktClosingForexRate" AutoPostBackOnFilter="true" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        DataFormatString="{0:N4}" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Mkt Closing FOREx Date" DataField="ForExDate" UniqueName="ForExDate"
                        HeaderStyle-Width="90px" SortExpression="ForExDate" AutoPostBackOnFilter="true" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Exchange" DataField="Exchange" UniqueName="Exchange"
                        HeaderStyle-Width="90px" SortExpression="Exchange" AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Sebi TurnOver Fee (Rs)" DataField="SebiTurnOverFee" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" UniqueName="SebiTurnOverFee" SortExpression="SebiTurnOverFee" AutoPostBackOnFilter="true"
                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        DataFormatString="{0:N4}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Transaction Charges (Rs)" DataField="TransactionCharges" FooterStyle-CssClass="Newfont"
                         ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" UniqueName="TransactionCharges" SortExpression="TransactionCharges" AutoPostBackOnFilter="true"
                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        DataFormatString="{0:N4}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Stamp Charges (Rs)" DataField="StampCharges" UniqueName="StampCharges" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" SortExpression="StampCharges" AutoPostBackOnFilter="true" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N4}"
                        FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="STT (Rs)" DataField="STT" UniqueName="STT" SortExpression="STT" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" AutoPostBackOnFilter="true" DataFormatString="{0:N4}" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Service Tax (Rs)" DataField="ServiceTax" DataFormatString="{0:N4}" FooterStyle-CssClass="Newfont"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" UniqueName="ServiceTax" SortExpression="ServiceTax" AutoPostBackOnFilter="true"
                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="OtherCharges (Rs)" DataField="OtherCharges" FooterStyle-CssClass="Newfont"
                       ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" DataFormatString="{0:N4}" UniqueName="OtherCharges" SortExpression="OtherCharges"
                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Demat Charge (Rs)" DataField="Demat Charge" UniqueName="Demat Charge" FooterStyle-CssClass="Newfont"
                       ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" SortExpression="Demat Charge" AutoPostBackOnFilter="true" Aggregate="Sum" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N4}"
                        FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Difference In Amount (Rs)" DataField="Difference In Amount" FooterStyle-CssClass="Newfont"
                       ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="110px" UniqueName="Difference In Amount" DataFormatString="{0:N4}" SortExpression="Difference In Amount"
                        AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        Aggregate="Sum" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Trade Account No." DataField="TradeAccountNum"
                        HeaderStyle-Width="100px" UniqueName="TradeAccountNum" SortExpression="TradeAccountNum" AllowFiltering="true"
                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Demat Account No" DataField="DematAccountNo"
                        HeaderStyle-Width="100px" UniqueName="DematAccountNo" SortExpression="DematAccountNo" AllowFiltering="true"
                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Settlement No" DataField="SettlementNo" UniqueName="SettlementNo"
                        HeaderStyle-Width="100px" SortExpression="SettlementNo" AllowFiltering="true" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Bill No" DataField="BillNo" UniqueName="BillNo"
                        HeaderStyle-Width="100px" SortExpression="BillNo" AllowFiltering="true" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn  HeaderText="Investor Name" DataField="InvestorName"
                        HeaderStyle-Width="180px" UniqueName="InvestorName" SortExpression="InvestorName" AllowFiltering="true"
                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Managed By" DataField="ManagerName" UniqueName="ManagerName"
                        HeaderStyle-Width="180px" SortExpression="ManagerName" AllowFiltering="true" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="PAN No" DataField="PanNo" UniqueName="PanNo"
                        HeaderStyle-Width="180px" SortExpression="PanNo" AllowFiltering="true" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Broker Name" DataField="BrokerName" UniqueName="BrokerName"
                        HeaderStyle-Width="180px" SortExpression="BrokerName" AllowFiltering="true" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Transaction Status" DataField="TransactionStatus"
                        HeaderStyle-Width="100px" UniqueName="TransactionStatus" SortExpression="TransactionStatus" AutoPostBackOnFilter="true"
                        AllowFiltering="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Dividend Status" DataField="Dividend Status"
                        HeaderStyle-Width="100px" UniqueName="Dividend Status" SortExpression="Dividend Status" AutoPostBackOnFilter="true"
                        AllowFiltering="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    <%----------------------------------------------------------%>
                   
                    <%--    <telerik:GridBoundColumn HeaderText="Transaction Code" DataField="Transaction Code"
                        UniqueName="Transaction Code" SortExpression="Transaction Code" AutoPostBackOnFilter="true"
                        AllowFiltering="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                        Visible="false">
                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    </telerik:GridBoundColumn>
                    --%>
                    <telerik:GridBoundColumn HeaderText="Purpose" DataField="Purpose" UniqueName="Purpose"
                        HeaderStyle-Width="100px" SortExpression="Purpose" AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                    
                     <telerik:GridBoundColumn HeaderText="System TransactionId" DataField="TransactionId"
                        HeaderStyle-Width="100px" UniqueName="TransactionId" SortExpression="TransactionId" AllowFiltering="true"
                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridBoundColumn>
                     <telerik:GridDateTimeColumn HeaderText="Created On" DataField="CET_CreatedOn"
                        HeaderStyle-Width="150px" UniqueName="CET_CreatedOn" SortExpression="CET_CreatedOn" AllowFiltering="true"
                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" DataFormatString="{0:d-MMM-yyy}"> 
                        <ItemStyle Width="" Wrap="false" VerticalAlign="Top" CssClass="font" />
                    </telerik:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                <Resizing AllowColumnResize="true" />
            </ClientSettings>
        </telerik:RadGrid>
        <%-- </td>
        </tr>
    </table>--%>
    </asp:Panel>
    <%-- <table width="100%">
        <tr id="trPager" runat="server">
            <td>
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>--%>
</div>
<%--<asp:Button ID="btnScripSearch" runat="server" Text="" OnClick="btnScripSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnTradeNumSearch" runat="server" Text="" OnClick="btnTradeNumSearch_Click"
    BorderStyle="None" BackColor="Transparent" />--%>
<table style="width: 100%">
    <tr id="trSelectAction" runat="server" visible="false">
        <td style="width: 150px" align="right">
            <asp:Label ID="lblSelectAction" runat="server" CssClass="FieldName" Text="Select Action:"></asp:Label>
        </td>
        <td style="width: 280px">
            <asp:DropDownList ID="ddlAction" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlActionSec_SelectedIndexChanged">
                <asp:ListItem Value="0">Select Action</asp:ListItem>
                <asp:ListItem Value="Del">Delete All</asp:ListItem>
                <asp:ListItem Value="MapToCi">Mapped to Core Investment</asp:ListItem>
                <asp:ListItem Value="MapToPi">Mapped to Portfolio Investment</asp:ListItem>
                <asp:ListItem Value="MapToManager">Managed By- Mapped to Same Investor </asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 150px">
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnRecordCount" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" Visible="false" />
<asp:HiddenField ID="hdnScripFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTradeNum" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranType" runat="server" Visible="false" />
<asp:HiddenField ID="hdnExchange" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTradeDate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSort" runat="server" Visible="false" Value="Name ASC" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnStatusValue" runat="server" />
<asp:HiddenField ID="hddeletiontype" runat="server" />
<asp:Button ID="btndelete" runat="server" BorderStyle="None" BackColor="Transparent"
    OnClick="BtnDelete_Click" />