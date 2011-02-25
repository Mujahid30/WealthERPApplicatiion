<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionsView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.AllTransactions" %>
<%@ Register Src="~/General/Pager.ascx" TagName="Pager" TagPrefix="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="View MF Transaction"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="HeaderCell">
            <asp:LinkButton ID="lbBack" runat="server" Text="Back" onclick="lbBack_Click" Visible="false" CssClass="FieldName"></asp:LinkButton>
        </td>
    </tr>
</table>
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
<table style="width: 100%;" class="TableBackground" id="tblGV" runat="server">
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
    <tr>
        <td>
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
                <asp:GridView ID="gvMFTransactions" runat="server" AllowSorting="True" AutoGenerateColumns="False"
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
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBx" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnDeleteSelected" CssClass="FieldName" OnClick="btnDeleteSelected_Click"
                                    runat="server" Text="Delete" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="Select" HeaderText="View Details" ShowHeader="True" Text="View Details"
                            ItemStyle-Wrap="false">
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:ButtonField>
                        <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" Visible="false" />
                        
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
                                <asp:Label ID="lblSchemeHeader" runat="server" Text='<%# Eval("Scheme Name").ToString() %>'
                                    ItemStyle-Wrap="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
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
                        <%--<asp:BoundField DataField="TransactionStatus" HeaderText="Transaction Status" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>--%>
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
                </asp:GridView>
            </td>
        </tr>
        </table>
    </asp:Panel>
   <table id="tblpager" class="TableBackground" width="100%" runat="server">
        <tr id="trPager" runat="server">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
        </table>
    
</div>
<asp:Button ID="btnTranSchemeSearch" runat="server" Text="" OnClick="btnTranSchemeSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSchemeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranType" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranTrigger" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranDate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSort" runat="server" Visible="false" Value="Name ASC" />
<asp:HiddenField ID="hdnStatus" runat="server" Visible="false"/>
