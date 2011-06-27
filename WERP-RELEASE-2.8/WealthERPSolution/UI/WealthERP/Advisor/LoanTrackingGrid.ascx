<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoanTrackingGrid.ascx.cs"
    Inherits="WealthERP.Advisor.LoanTrackingGrid" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

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
</script>
<table class="TableBackground" width="100%" id="tblGV" runat="server">

    <tr>
        <td colspan="2" class="rightField">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Customer Loan Application Tracking Grid"></asp:Label>
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found..."></asp:Label>
        </td>
    </tr>
    <%--<tr id="Tr1" runat="server" visible="true">
        <td>
            <asp:RadioButton ID="rbtnExcel" Text="Excel" runat="server" GroupName="grpExport"
                CssClass="cmbField" />
            <asp:RadioButton ID="rbtnPDF" Text="PDF" runat="server" GroupName="grpExport" CssClass="cmbField" />
            <asp:RadioButton ID="rbtnWord" Text="Word" runat="server" GroupName="grpExport" CssClass="cmbField" />
        </td>
        <td>
        </td>
    </tr>--%>
    <%--<tr>
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
    </tr>--%>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="rightField">
            <asp:GridView ID="gvLoanProposals" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" DataKeyNames="LoanProposalId" AllowSorting="True" OnSorting="gvLoanProposals_Sort"
                ShowHeader="true" ShowFooter="true" EnableViewState="true">
                <FooterStyle CssClass="FooterStyle" />
                <PagerSettings Visible="False" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange"
                                CssClass="GridViewCmbField">
                                <asp:ListItem Text="Select" Value="Select" />
                                <asp:ListItem Text="View" Value="View" />
                                <asp:ListItem Text="Edit" Value="Edit" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustName" runat="server" Text="Customer Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_LoanTrackingGrid_btnNameSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCustNameHeader" runat="server" Text='<%# Eval("CustomerName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblLoanType" runat="server" Text="Loan Type"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlLoanType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged"
                                CssClass="GridViewCmbField">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLoanTypeHeader" runat="server" Text='<%# Eval("LoanType").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" DataFormatString="{0:f2}"
                        ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="LoanPartner" HeaderText="Lender Name" ItemStyle-Wrap="false" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblLoanStage" runat="server" Text="Stage"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlLoanStage" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlLoanStage_SelectedIndexChanged"
                                CssClass="GridViewCmbField">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLoanStageHeader" runat="server" Text='<%# Eval("LoanStage").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Commission" HeaderText="Commission" ItemStyle-Wrap="false" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="rightField">
            &nbsp;
        </td>
    </tr>
    <tr id="trPager" runat="server">
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnLoanTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnLoanStageFilter" runat="server" Visible="false" />
