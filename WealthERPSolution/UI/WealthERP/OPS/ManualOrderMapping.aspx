<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManualOrderMapping.aspx.cs" Inherits="WealthERP.OPS.ManualOrderMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Loan Structure with Partner"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<table width="100%">
<tr>
<td align="left" style="width:25%;">
<asp:Label ID="lblOrderNumber" runat="server" Text="Order Number: "  CssClass="FieldName"></asp:Label>
<asp:Label ID="lblGetOrderNo" runat="server" Text=" "  CssClass="FieldName"></asp:Label>
</td>
<td align="left" style="width:25%;">
<asp:Label ID="lblOrderDate" runat="server" Text="Order Date: "  CssClass="FieldName"></asp:Label>
<asp:Label ID="lblGetOrderDate" runat="server" Text=" "  CssClass="FieldName"></asp:Label>
</td>
<td colspan="4"></td>
</tr>

<tr>
<td align="left" style="width:25%;">
<asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: "  CssClass="FieldName"></asp:Label>
<asp:Label ID="lblGetOrderStatus" runat="server" Text=" "  CssClass="FieldName"></asp:Label>
</td>
<td align="left" style="width:25%;">
<asp:Label ID="lblOrderType" runat="server" Text="Order Type: "  CssClass="FieldName"></asp:Label>
<asp:Label ID="lblGetOrderType" runat="server" Text=" "  CssClass="FieldName"></asp:Label>
</td>
<td colspan="4"></td>
</tr>

<tr>
<td colspan="6">
<asp:GridView ID="gvMannualMatch" CssClass="GridViewStyle"  runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
ShowFooter="True">
<RowStyle CssClass="RowStyle" />
<AlternatingRowStyle CssClass="AltRowStyle" />
 <Columns>
 <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
 <HeaderTemplate>
<asp:Label ID="lblPendingReason" runat="server" Text="Pending/Reject Reason"></asp:Label>
<br />
<asp:TextBox ID="txtPendingRejectSearch" runat="server" CssClass="GridViewTxtField" />
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblPendingRejectSearchCol" runat="server" Text='<%# Eval("Pending/Reject").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

 <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblProcessId" runat="server" Text="Process Id"></asp:Label>
<br />
<asp:TextBox ID="txtProcessIdSearch" runat="server" CssClass="GridViewTxtField" />
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblProcessIDCol" runat="server" Text='<%# Eval("ProcessID").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

 <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lbltransactionNo" runat="server" Text="Transaction No"></asp:Label>
<br />
<asp:TextBox ID="txtTransactionNoSearch" runat="server" CssClass="GridViewTxtField" />
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblTransactionNoCol" runat="server" Text='<%# Eval("TransactionNumber").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

 <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
<br />
<asp:TextBox ID="txtCustomerSearch" runat="server" CssClass="GridViewTxtField" />
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblCustomerCol" runat="server" Text='<%# Eval("Customer").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

 <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblAssetClass" runat="server" Text="Asset Class"></asp:Label>
<br />
<asp:TextBox ID="txtAssetClassSearch" runat="server" CssClass="GridViewTxtField" />
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblAssetClassCol" runat="server" Text='<%# Eval("Assetclass").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type"></asp:Label>
<br />
<asp:TextBox ID="txtTransactionTypeSearch" runat="server" CssClass="GridViewTxtField" />
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblTransactionTypeCol" runat="server" Text='<%# Eval("Type").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblFolioNo" runat="server" Text="Folio Number"></asp:Label>
<br />
<asp:TextBox ID="txtFolioNoSearch" runat="server" CssClass="GridViewTxtField" />
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblFolioNoCol" runat="server" Text='<%# Eval("FolioNo").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label>
<br />
<asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="GridViewTxtField" />
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblSchemeCol" runat="server" Text='<%# Eval("Scheme").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Transaction Date">
  <ItemTemplate>
     <asp:Label ID="lblTransactionDateCol" runat="server" Text='<%# Eval("TransactionDate").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Price">
  <ItemTemplate>
     <asp:Label ID="lblPriceCol" runat="server" Text='<%# Eval("Price").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Units">
  <ItemTemplate>
     <asp:Label ID="lblUnitsCol" runat="server" Text='<%# Eval("Units").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Amount">
  <ItemTemplate>
     <asp:Label ID="lblAmountCol" runat="server" Text='<%# Eval("Amount").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Map Transaction">
  <ItemTemplate>
  <asp:CheckBox ID="chkMapTransaction" runat="server" />
     <%--<asp:Label ID="lblMapTransactionCol" runat="server" Text='<%# Eval("MapTransaction").ToString() %>'></asp:Label>--%>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

 </Columns>
 <EditRowStyle CssClass="EditRowStyle" />
<FooterStyle CssClass="FooterStyle" />
<HeaderStyle CssClass="HeaderStyle" />
<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
<SelectedRowStyle CssClass="SelectedRowStyle" />
</asp:GridView>
</td>
</tr>
<td colspan="6" align="left">
<asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton"  Text="Submit" />
</td>
</table>

    </div>
    </form>
</body>
</html>
