﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEquityPortfolios.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewEquityPortfolios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


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

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<div class="divPageHeading">
<table cellspacing="0" cellpadding="3" width="100%">
    <tr>
        <td align="left">
            <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Equity Net position"></asp:Label>
        </td>
    </tr>
</table>
</div>

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

<table>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>

<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" Width="100%" MultiPageID="EQPortfolioTabPages" SelectedIndex="0" EnableViewState="true">
    <Tabs>
        
        <telerik:RadTab runat="server" Text="UnRealized"
            Value="UnRealized" TabIndex="3">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Realized Delivery"
            Value="Realized Delivery" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Realized Speculative"
            Value="Realized Speculative" TabIndex="2">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="All"
            Value="All" TabIndex="0">
        </telerik:RadTab>
        
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadMultiPage ID="EQPortfolioTabPages" runat="server" EnableViewState="true"
SelectedIndex="0">

    <telerik:RadPageView ID="EQPortfolioUnRealizedTabPage" runat="server">
    <asp:Panel ID="pnlEQPortfolioUnRealized" runat="server">
        <%--<table id="tblUnrealized" runat="server">
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
                    </table>--%>
        <table style="width: 100%; border: none; margin: 0px; padding: 0px;" cellpadding="0"
            cellspacing="0">
            <tr>
                <td>
                    <asp:ImageButton ID="imgBtnExport3" ImageUrl="../App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                        OnClick="imgBtnExport3_Click" style="width: 20px" />
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
    </asp:Panel>
    </telerik:RadPageView>
    
    <telerik:RadPageView ID="EQPortfolioRealizedDelTabPage" runat="server">
    <asp:Panel ID="pnlEQPortfolioRealizedDel" runat="server">
        <%--<table id="tblDelivery" runat="server">
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
                    </table>--%>
        <table style="width: 100%; border: none; margin: 0px; padding: 0px;" cellpadding="0"
            cellspacing="0">
            <tr>
                <td>
                    <asp:ImageButton ID="imgBtnExport1" ImageUrl="../App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport1_Click" />
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
    </asp:Panel>
    </telerik:RadPageView>
    
    <telerik:RadPageView ID="EQPortfolioRealizedSpecTabPage" runat="server">
    <asp:Panel ID="pnlEQPortfolioRealizedSpec" runat="server">
        <%--<table id="tblSpec" runat="server">
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
                    </table>--%>
        <table style="width: 100%; border: none; margin: 0px; padding: 0px;" cellpadding="0"
            cellspacing="0">
            <tr>
                <td>
                    <asp:ImageButton ID="imgBtnExport2" ImageUrl="../App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport2_Click" />
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
    </asp:Panel>
    </telerik:RadPageView>
        
    <telerik:RadPageView ID="EQPortfolioAllTabPage" runat="server">
    <asp:Panel ID="pnlEQPortfolioAll" runat="server">
        <%--<table id="tblPortfolio" runat="server">
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
                    </table>--%>
        <table style="width: 100%; border: none; margin: 0px; padding: 0px;" cellpadding="0"
            cellspacing="0">
            <tr>
                <td>
                    <asp:ImageButton ID="imgBtnExport" ImageUrl="../App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click" />
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
    </asp:Panel>
    </telerik:RadPageView>

</telerik:RadMultiPage>

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
<asp:HiddenField ID="hdnNoOfRecords" runat="server" />
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
            <%--<asp:Button ID="Button2" runat="server" OnClick="btnExport_Click" Text="Export" CssClass="ButtonField" />
            <asp:Button ID="Button3" runat="server" OnClick="btnPrint_Click" Text="Print" CssClass="ButtonField" />
            <asp:Button ID="Button4" runat="server" Text="" OnClick="btnPrintGrid_Click" BorderStyle="None"
                BackColor="Transparent" />--%>
        </td>
    </tr>
</table>
