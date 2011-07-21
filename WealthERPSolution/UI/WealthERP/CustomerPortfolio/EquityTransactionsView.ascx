<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityTransactionsView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.EquityTransactionsView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>


<script type="text/javascript" src="../Scripts/JScript.js"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

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
     function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }    }
</script>

<table  style="width: 100%;" class="TableBackground"  runat="server">
 <tr>
        <td colspan="2">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Equity Transactions"></asp:Label>
            <hr/>
        </td>
    </tr>
    <tr>
        <td class="rightField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
      <%--  </td>
        <td  class="rightField">--%>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
       <%-- <td colspan="2" >
            <asp:Label ID="lblMsg" runat="server" CssClass="Error" Text="You dont have any transaction..!"></asp:Label>
        </td>--%>
    </tr>
</table>
<table class="TableBackground">
    <tr>
        <td>
            <asp:Label ID="lblFromTran" Text="From" CssClass="Field" runat="server" />
        </td>
        <td>
            <asp:TextBox ID="txtFromTran" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtFromTran_CalendarExtender" runat="server" TargetControlID="txtFromTran"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtFromTran_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFromTran" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
        </td>
        <td>
            <asp:Label ID="lblToTran" Text="To" CssClass="Field" runat="server" />
        </td>
        <td>
            <asp:TextBox ID="txtToTran" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtToTran_CalendarExtender" runat="server" TargetControlID="txtToTran"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtToTran_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtToTran" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToTran"
                ErrorMessage="To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtFromTran" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
            </asp:CompareValidator>
        </td>
        <td>
            <asp:Button ID="btnViewTran" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewTran_Click" />
        </td>
    </tr>
</table>
<table id="ErrorMessage" align="center" runat="server">
    <tr>
        <td>
            <div class="failure-msg" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<table style="width: 100%;" class="TableBackground" id="tblGv" runat="server">
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
                CssClass="cmbField" AutoPostBack="true" OnCheckedChanged="rbtnSingle_CheckedChanged" />
            <asp:RadioButton ID="rbtnMultiple" Text="All Pages" runat="server" GroupName="grpPage"
                CssClass="cmbField" AutoPostBack="true" OnCheckedChanged="rbtnMultiple_CheckedChanged" />
        </td>
        <td>
            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export"
                CssClass="ButtonField" />
            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" CssClass="ButtonField" />
            <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <table style="width: 100%">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                        <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div id="tbl" runat="server">
<asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:GridView ID="gvEquityTransactions" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" DataKeyNames="TransactionId" EnableViewState="true" CssClass="GridViewStyle"
                    AllowSorting="True" ShowFooter="True" OnRowCommand="gvEquityTransactions_RowCommand"
                    OnRowDataBound="gvEquityTransactions_RowDataBound" OnDataBound="gvEquityTransactions_DataBound"
                    OnSorting="gvEquityTransactions_Sort">
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle HorizontalAlign="Left" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle BackColor="White" CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBx" runat="server" CssClass="cmbField"/>
                            </ItemTemplate>
                        <%--    <FooterTemplate>
                                <asp:Button ID="btnDeleteSelected" CssClass="FieldName" OnClick="btnDeleteSelected_Click"
                                    runat="server" Text="Delete" />
                            </FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="Select" ShowHeader="True" Text="View Details" ItemStyle-Wrap="false" />
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblTradeNum" runat="server" Text="Trade Number" ></asp:Label>
                                <asp:TextBox ID="txtTradeNumSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_EquityTransactionsView_btnTradeNumSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTradeNumHeader" runat="server" Text='<%# Eval("TradeNum").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblScrip" runat="server" Text="Scrip Name"></asp:Label>
                                <asp:TextBox ID="txtScripSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_EquityTransactionsView_btnScripSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblScripHeader" runat="server" Text='<%# Eval("Scheme Name").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblTranType" runat="server" Text="Transaction Type"></asp:Label>
                                <asp:DropDownList ID="ddlTranType" AutoPostBack="true" runat="server" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTranTypeHeader" runat="server" Text='<%# Eval("Transaction Type").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblExchange" runat="server" Text="Exchange"></asp:Label>
                                <asp:DropDownList ID="ddlExchange" AutoPostBack="true" runat="server" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlExchange_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblExchangeHeader" runat="server" Text='<%# Eval("Exchange").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" SortExpression="TradeDate" >
                            <HeaderTemplate>
                                <asp:Label ID="lblTradeDate" runat="server" Text="Trade Date   (dd/mm/yyyy)" ></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlTradeDate" AutoPostBack="true" runat="server" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlTradeDate_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTradeDateHeader" runat="server" Text='<%# Eval("TradeDate").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate(Rs)" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" HtmlEncode="false" SortExpression="Rate" />
                        <asp:BoundField DataField="Quantity" HeaderText="No of Shares" ItemStyle-HorizontalAlign="Right"
                            SortExpression="Quantity" />
                        <asp:BoundField DataField="Brokerage" HeaderText="Brokerage(Rs)" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" HtmlEncode="false" SortExpression="Brokerage" />
                        <asp:BoundField DataField="OtherCharges" HeaderText="Other Charges(Rs)" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" HtmlEncode="false" SortExpression="OtherCharges" />
                        <asp:BoundField DataField="TradeTotal" HeaderText="Trade Total(Rs)" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" HtmlEncode="false" SortExpression="TradeTotal" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        </table>
           </asp:Panel>
        <table width="100%">
        <tr id="trPager" runat="server">
            <td>
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
</div>
<asp:Button ID="btnScripSearch" runat="server" Text="" OnClick="btnScripSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnTradeNumSearch" runat="server" Text="" OnClick="btnTradeNumSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" Visible="false" />
<asp:HiddenField ID="hdnScripFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTradeNum" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranType" runat="server" Visible="false" />
<asp:HiddenField ID="hdnExchange" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTradeDate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSort" runat="server" Visible="false" Value="Name ASC" />
