<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedFoliosTransactionUpload.ascx.cs" 
Inherits="WealthERP.Uploads.RejectedFoliosTransactionUpload" %>

<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<link href="../CSS/GridViewCss.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script>
    function ShowPopup() {
       
        var form = document.forms[0];
       
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
////                    hiddenField = form.elements[i].id.replace("chk","hdn");
////                    transactionId = document.getElementById(hiddenField).value;


//                    hiddenField = form.elements[i].id.replace("chk", "hdn");
//                    hiddenFieldValues = document.getElementById(hiddenField).value;
//                    transactionId[i] = hiddenFieldValues;
                    
                    }
                }
            }
        
        
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
        //        window.open('Uploads/MapToCustomers.aspx?id=' + transactionId + '', 'mywindow', 'width=550,height=450,scrollbars=yes,location=no')
        return true;
    }
</script>

<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Map Folios to Customers"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Upload History"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvRejectedFolios" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" ShowFooter="true" >
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                        <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBxSelFolio" runat="server" ToolTip="Select folios for mapping" />
                            <asp:HiddenField ID="hdnBxSelStagingTabId" runat="server" Value='<%# Eval("ID").ToString() %>' />
                            <asp:HiddenField ID="hdnBxSelAMCCode" runat="server" Value='<%# Eval("AMCCode").ToString() %>' />
                        </ItemTemplate>
                        
                        </asp:TemplateField>
                        <asp:BoundField DataField="InvestorName" HeaderText="INVESTOR NAME" />
                        <%--<asp:BoundField DataField="PanNumber" HeaderText="PAN NUMBER" />--%>
                        <asp:BoundField DataField="FolioNumber" HeaderText = "FOLIO NUMBER" />
                    
                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trInputNullMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyTranEmptyMsg" class="FieldName">
                There are no records to be displayed !
            </label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
                OnClientClick="return ShowPopup()" onclick="btnMapToCustomer_Click" />
        </td>
    </tr>
    <tr id="trError" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Error" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="ADUL_StartTime DESC" />
