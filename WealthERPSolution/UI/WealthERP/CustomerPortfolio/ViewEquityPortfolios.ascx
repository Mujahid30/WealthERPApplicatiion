<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEquityPortfolios.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewEquityPortfolios" %>

<script type="text/javascript">
    var tabberOptions = { 'onClick': function(argsObj) {

        var t = argsObj.tabber; /* Tabber object */
        var id = t.id; /* ID of the main tabber DIV */
        var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
        var e = argsObj.event; /* Event object */


        document.getElementById('<%= hdnSelectedTab.ClientID %>').value = i;

    }
    };
</script>

<script type="text/javascript" src="../Scripts/tabber.js"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

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
    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function GetSelectedTab(selectedTab) {
        alert(selectedTab);
    }
</script>

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
</table>
<asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
<table style="width: 100%;">
    <tr>
        <td colspan="3">
            <div class="tabber" id="divMain">
                <div class="tabbertab" runat="server" id="divAll">
                    <h6>
                        All</h6>
                    <table id="tblPortfolio" runat="server">
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
                                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export"
                                    CssClass="ButtonField" />
                                <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" CssClass="ButtonField" />
                                <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                                    BorderStyle="None" BackColor="Transparent" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <div id="dvEquityPortfolio" runat="server">
                                    <asp:Label ID="lblMessage" Visible="false" Text="No Record Exists" runat="server"
                                        CssClass="Field"></asp:Label>
                                    <asp:GridView ID="gvEquityPortfolio" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CellPadding="4" DataKeyNames="Sl.No." EnableViewState="true" CssClass="GridViewStyle"
                                        ShowFooter="True" OnSorting="gvEquityPortfolio_Sorting" OnRowCommand="gvEquityPortfolio_RowCommand"
                                        OnPageIndexChanging="gvEquityPortfolio_PageIndexChanging" OnDataBound="gvEquityPortfolio_DataBound"
                                        OnRowDataBound="gvEquityPortfolio_RowDataBound">
                                        <RowStyle CssClass="RowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select" />
                                            <asp:BoundField DataField="Sl.No." HeaderText="Sl.No." Visible="false" />
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblScripName" runat="server" Text="Scrip Name"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtScripNameSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_ViewEquityPortfolios_btnEQNPSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblScripNameHeader" runat="server" Text='<%# Eval("CompanyName").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Quantity" HeaderText="No of Shares" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="AveragePrice" HeaderText="Average Price (Rs)" ItemStyle-HorizontalAlign="Right" />
                                           <asp:BoundField DataField="CostOfPurchase" HeaderText="Cost Of Purchase (Rs)" ItemStyle-HorizontalAlign="Right" />
                                           <asp:BoundField DataField="MarketPrice" HeaderText="Current Price (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="CurrentValue" HeaderText="Current Value (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="UnRealizedPL" HeaderText="UnRealized P/L (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="RealizedPL" HeaderText="Realized P/L (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="XIRR" HeaderText="XIRR (%)" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab" runat="server" id="divRealized">
                    <h6>
                        Realized Delivery</h6>
                    <table id="tblDelivery" runat="server">
                        <tr id="Tr2" runat="server" visible="true">
                            <td>
                                <asp:RadioButton ID="rbtnDeliveryExcel" Text="Excel" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                                <asp:RadioButton ID="rbtnDeliveryPdf" Text="PDF" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                                <asp:RadioButton ID="rbtnDeliveryWord" Text="Word" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="btnExportRealizedDelivery_Click"
                                    Text="Export" CssClass="ButtonField" />
                                <asp:Button ID="btnPrintRealizedDelivery" runat="server" OnClick="btnPrintRealizedDelivery_Click"
                                    Text="Print" CssClass="ButtonField" />
                                <asp:Button ID="btnPrintRealizedDeliveryGrid" runat="server" Text="" OnClick="btnPrintRealizedDeliveryGrid_Click"
                                    BorderStyle="None" BackColor="Transparent" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <div id="dvEquityPortfolioDelivery" runat="server">
                                    <asp:Label ID="lblMessageD" Visible="false" Text="No Record Exists" runat="server"
                                        CssClass="Field"></asp:Label>
                                    <asp:GridView ID="gvEquityPortfolioDelivery" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CellPadding="4" DataKeyNames="Sl.No." EnableViewState="true" CssClass="GridViewStyle"
                                        ShowFooter="True" OnSorting="gvEquityPortfolioDelivery_Sorting" OnRowDataBound="gvEquityPortfolioDelivery_RowDataBound"
                                        OnRowCommand="gvEquityPortfolioDelivery_RowCommand" OnPageIndexChanging="gvEquityPortfolioDelivery_PageIndexChanging"
                                        OnDataBound="gvEquityPortfolioDelivery_DataBound">
                                        <RowStyle CssClass="RowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select" />
                                            <asp:BoundField DataField="Sl.No." HeaderText="Sl.No." Visible="false" />
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblScripNameRealized" runat="server" Text="Scrip Name"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtScripNameRealizedSearch" runat="server" CssClass="GridViewTxtField"
                                                        onkeydown="return JSdoPostback(event,'ctrl_ViewEquityPortfolios_btnEQRealizedSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblScripNameRealizedHeader" runat="server" Text='<%# Eval("CompanyName").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SaleQty" HeaderText="No of Shares Sold" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="RealizedSalesProceeds" HeaderText="Sale Proceeds (Rs)"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="CostOfSales" HeaderText="Cost Of Sales (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="RealizedPL" HeaderText="Realized P/L (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="XIRR" HeaderText="XIRR (%)" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab" runat="server">
                    <h6>
                        Realized Speculative</h6>
                    <table id="tblSpec" runat="server">
                        <tr id="Tr3" runat="server" visible="true">
                            <td>
                                <asp:RadioButton ID="rbtnSpecExcel" Text="Excel" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                                <asp:RadioButton ID="rbtnSpecPdf" Text="PDF" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                                <asp:RadioButton ID="rbtnSpecWord" Text="Word" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnExportRealizedSpeculativ" runat="server" OnClick="btnExportRealizedSpeculative_Click"
                                    Text="Export" CssClass="ButtonField" />
                                <asp:Button ID="btnPrintRealizedSpeculative" runat="server" OnClick="btnPrintRealizedSpeculative_Click"
                                    Text="Print" CssClass="ButtonField" />
                                <asp:Button ID="btnPrintRealizedSpeculativeGrid" runat="server" Text="" OnClick="btnPrintRealizedSpeculativeGrid_Click"
                                    BorderStyle="None" BackColor="Transparent" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <div id="dvEquityPortfolioSpeculative" runat="server">
                                    <asp:Label ID="lblMessageSpeculative" Visible="false" Text="No Record Exists" runat="server"
                                        CssClass="Field"></asp:Label>
                                    <asp:GridView ID="gvEquityPortfolioSpeculative" runat="server" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Sl.No." EnableViewState="true"
                                        CssClass="GridViewStyle" ShowFooter="True" OnSorting="gvEquityPortfolioSpeculative_Sorting"
                                        OnRowDataBound="gvEquityPortfolioSpeculative_RowDataBound" OnRowCommand="gvEquityPortfolioSpeculative_RowCommand"
                                        OnPageIndexChanging="gvEquityPortfolioSpeculative_PageIndexChanging" OnDataBound="gvEquityPortfolioSpeculative_DataBound">
                                        <RowStyle CssClass="RowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select" />
                                            <asp:BoundField DataField="Sl.No." HeaderText="Sl.No." Visible="false" />
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblScripNameRealizedSpec" runat="server" Text="Scrip Name"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtScripNameRealizedSpecSearch" runat="server" CssClass="GridViewTxtField"
                                                        onkeydown="return JSdoPostback(event,'ctrl_ViewEquityPortfolios_btnEQRealizedSpecSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblScripNameRealizedSpecHeader" runat="server" Text='<%# Eval("CompanyName").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SaleQty" HeaderText="No of Shares" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="RealizedSalesProceeds" HeaderText="Sale Proceeds (Rs)"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="CostOfSales" HeaderText="Cost Of Sales (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="RealizedPL" HeaderText="Realized P/L (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="XIRR" HeaderText="XIRR (%)" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab" runat="server">
                    <h6>
                        UnRealized</h6>
                    <table id="tblUnrealized" runat="server">
                        <tr id="Tr4" runat="server" visible="true">
                            <td>
                                <asp:RadioButton ID="rbtnUnrealExcel" Text="Excel" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                                <asp:RadioButton ID="rbtnUnrealPDF" Text="PDF" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                                <asp:RadioButton ID="rbtnUnrealWord" Text="Word" runat="server" GroupName="grpExport"
                                    CssClass="cmbField" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnExportUnrealized" runat="server" OnClick="btnExportUnrealized_Click"
                                    Text="Export" CssClass="ButtonField" />
                                <asp:Button ID="btnPrintUnrealized" runat="server" OnClick="btnPrintUnrealized_Click"
                                    Text="Print" CssClass="ButtonField" />
                                <asp:Button ID="btnPrintUnrealizedGrid" runat="server" Text="" OnClick="btnPrintUnrealizedGrid_Click"
                                    BorderStyle="None" BackColor="Transparent" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <div id="dvEquityPortfolioUnrealized" runat="server">
                                    <asp:Label ID="lblMessageUnrealized" Visible="false" Text="No Record Exists" runat="server"
                                        CssClass="Field"></asp:Label>
                                    <asp:GridView ID="gvEquityPortfolioUnrealized" runat="server" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Sl.No." EnableViewState="true"
                                        CssClass="GridViewStyle" ShowFooter="True" OnSorting="gvEquityPortfolioUnrealized_Sorting"
                                        OnRowCommand="gvEquityPortfolioUnrealized_RowCommand" OnPageIndexChanging="gvEquityPortfolioUnrealized_PageIndexChanging"
                                        OnDataBound="gvEquityPortfolioUnrealized_DataBound" OnRowDataBound="gvEquityPortfolioUnrealized_RowDataBound">
                                        <RowStyle CssClass="RowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select" />
                                            <asp:BoundField DataField="Sl.No." HeaderText="Sl.No." Visible="false" />
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblScripNameUnRealized" runat="server" Text="Scrip Name"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtScripNameUnRealizedSearch" runat="server" CssClass="GridViewTxtField"
                                                        onkeydown="return JSdoPostback(event,'ctrl_ViewEquityPortfolios_btnEQUnRealizedSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblScripNameUnRealizedHeader" runat="server" Text='<%# Eval("CompanyName").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Quantity" HeaderText="No of Shares" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="AveragePrice" HeaderText="Average Price (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="CostOfPurchase" HeaderText="Cost Of Purchase (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="MarketPrice" HeaderText="Current Price (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="CurrentValue" HeaderText="Current Value (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="UnRealizedPL" HeaderText="UnRealized P/L (Rs)" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="XIRR" HeaderText="XIRR (%)" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div>
                <table style="width: 30%;">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Equity Portfolio Summary"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Current Value (Rs)"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblCurrentValue" runat="server" CssClass="Field" Text="lblCurrentValue"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
</asp:Panel>
<asp:Button ID="btnEQNPSearch" runat="server" Text="" OnClick="btnEQNPSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnEQRealizedSearch" runat="server" Text="" OnClick="btnEQRealizedSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnEQRealizedSpecSearch" runat="server" Text="" OnClick="btnEQRealizedSpecSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnEQUnRealizedSearch" runat="server" Text="" OnClick="btnEQUnRealizedSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnScipNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedSpecScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnUnRealizedScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSelectedTab" runat="server" />
<table id="Table1" runat="server" visible="false">
    <tr id="Tr5" runat="server" visible="true">
        <td>
            <asp:RadioButton ID="rbtnTry" Text="Excel" runat="server" GroupName="grpExport" CssClass="cmbField" />
            <asp:RadioButton ID="RadioButton2" Text="PDF" runat="server" GroupName="grpExport"
                CssClass="cmbField" />
            <asp:RadioButton ID="RadioButton3" Text="Word" runat="server" GroupName="grpExport"
                CssClass="cmbField" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="Button2" runat="server" OnClick="btnExport_Click" Text="Export" CssClass="ButtonField" />
            <asp:Button ID="Button3" runat="server" OnClick="btnPrint_Click" Text="Print" CssClass="ButtonField" />
            <asp:Button ID="Button4" runat="server" Text="" OnClick="btnPrintGrid_Click" BorderStyle="None"
                BackColor="Transparent" />
        </td>
    </tr>
</table>
