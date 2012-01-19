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
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $('#ctrl_ManualOrderMapping_btnSubmit').bubbletip($('#div1'), { deltaDirection: 'right' });
    });
</script>

<script type="text/javascript" language="javascript">

    function CheckedOnlyOneRadioButton(spanChk) {
        var IsChecked = spanChk.checked;
        var CurrentRdbID = spanChk.id;
        var Chk = spanChk;
        Parent = document.getElementById("<%=gvMannualMatch.ClientID%>");
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {
            if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                if (items[i].checked) {
                    items[i].checked = false;

                }
            }
        }
    }

</script>
    <div>
    <table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell" style="width:100%;">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Manual Order Mapping"></asp:Label>
            <hr style="width:100%;" />
        </td>
</tr>
</table>

<table width="100%">
<%--<tr>
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
</tr>--%>

<tr>
<td colspan="6">
<asp:GridView ID="gvMannualMatch" CssClass="GridViewStyle"  runat="server" AutoGenerateColumns="False" DataKeyNames="CMFT_MFTransId,CMFA_AccountId,PASP_SchemePlanCode,CMFT_Amount,WMTT_TransactionClassificationCode"
ShowFooter="True">
<RowStyle CssClass="RowStyle" />
<AlternatingRowStyle CssClass="AltRowStyle" />
 <Columns>


<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Map Transaction">
  <ItemTemplate>
  <%--<asp:CheckBox ID="chkMapTransaction" runat="server" />--%>
  <asp:RadioButton  ID="rbtnMatch" runat="server" onclick="javascript:CheckedOnlyOneRadioButton(this);" />
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

 <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblProcessId" runat="server" Text="Process Id"></asp:Label>
<%--<br />
<asp:TextBox ID="txtProcessIdSearch" runat="server" CssClass="GridViewTxtField" />--%>
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblProcessIDCol" runat="server" Text='<%# Eval("ADUL_ProcessId").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

 <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lbltransactionNo" runat="server" Text="Transaction No"></asp:Label>
<%--<br />
<asp:TextBox ID="txtTransactionNoSearch" runat="server" CssClass="GridViewTxtField" />--%>
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblTransactionNoCol" runat="server" Text='<%# Eval("CMFT_TransactionNumber").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

 <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblCustomer" runat="server" Text="Folio Number"></asp:Label>
<%--<br />
<asp:TextBox ID="txtCustomerSearch" runat="server" CssClass="GridViewTxtField" />--%>
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblCustomerCol" runat="server" Text='<%# Eval("CMFA_FolioNum").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<%-- <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
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
</asp:TemplateField>--%>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblTransactionType" runat="server" Text="Scheme Name"></asp:Label>
<%--<br />
<asp:TextBox ID="txtTransactionTypeSearch" runat="server" CssClass="GridViewTxtField" />--%>
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblTransactionTypeCol" runat="server" Text='<%# Eval("PASP_SchemePlanName").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblFolioNo" runat="server" Text="Transaction Date"></asp:Label>
<%--<br />
<asp:TextBox ID="txtFolioNoSearch" runat="server" CssClass="GridViewTxtField" />--%>
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblFolioNoCol" runat="server" Text='<%# Eval("CMFT_TransactionDate").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
  <HeaderTemplate>
<asp:Label ID="lblScheme" runat="server" Text="Price"></asp:Label>
<%--<br />
<asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="GridViewTxtField" />--%>
</HeaderTemplate>
  <ItemTemplate>
     <asp:Label ID="lblSchemeCol" runat="server" Text='<%# Eval("CMFT_Price").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Amount">
  <ItemTemplate>
     <asp:Label ID="lblTransactionDateCol" runat="server" Text='<%# Eval("CMFT_Amount").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Units">
  <ItemTemplate>
     <asp:Label ID="lblPriceCol" runat="server" Text='<%# Eval("CMFT_Units").ToString() %>'></asp:Label>
  </ItemTemplate>
  <HeaderStyle Wrap="False" />
   <ItemStyle Wrap="False" />
</asp:TemplateField>

<asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="Type">
  <ItemTemplate>
     <asp:Label ID="lblUnitsCol" runat="server" Text='<%# Eval("WMTT_TransactionClassificationCode").ToString() %>'></asp:Label>
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
<tr>
<td colspan="6" align="left">
<asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton"  Text="Submit" 
        onclick="btnSubmit_Click" />
        <div id="div1" style="display: none;">
                <p class="tip">
                    Please check a radio button.
                </p>
            </div>
</td></tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
    <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    </div>
    </td>
    </tr>
 </table>
    </div>
    </form>
</body>
</html>
