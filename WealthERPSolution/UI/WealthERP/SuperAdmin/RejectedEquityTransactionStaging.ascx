<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedEquityTransactionStaging.ascx.cs" Inherits="WealthERP.SuperAdmin.RejectedEquityTransactionStaging" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script>
    function ShowPopup() {
        var form = document.forms[0];
        var transactionId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("chk", "hdn");
                    hiddenFieldValues = document.getElementById(hiddenField).value;
                    var splittedValues = hiddenFieldValues.split("-");
                    if (count == 1) {
                        transactionId = splittedValues[0];
                    }
                    else {
                        transactionId = transactionId + "~" + splittedValues[0];
                    }                    
                    RejectReasonCode = splittedValues[1];
                }
            }
        }
//        if (count > 1) {
//            alert("You can select only one record at a time.")
//            return false;
//        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
        window.open('Uploads/EquityMapToCustomers.aspx?id=' + transactionId + '', 'mywindow', 'width=550,height=450,scrollbars=yes,location=no')
        return true;
    }
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvWERPTrans.Rows.Count %>');
        var gvControl = document.getElementById('<%= gvWERPTrans.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkBxWPTrans";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxWPTransAll");

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

<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Equity Transaction Staging Rejects"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    </table>
    <table style="width: 100%">
    
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr id="msgReprocessComplete" runat="server" visible="false">
        <td align="center">
            <div id="Div1" class="success-msg" align="center" runat="server">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
    <tr id="msgReprocessincomplete"  runat="server" visible="false">
        <td align="center"> 
            <div id="Div2"  runat="server" class="failure-msg" align="center">
                Reprocess Failed!
            </div>
        </td>
    </tr>
     </table>
     <table style="width: 100%" class="TableBackground">
    <tr>
        <td align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvWERPTrans" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ShowFooter="true" CssClass="GridViewStyle" DataKeyNames="WERPTransactionId" AllowSorting="true"
                OnSorting="gvWERPTrans_Sort" >
                <%--OnRowDataBound="gvWERPTrans_RowDataBound"--%>
                
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <%--- Check Boxes--%>
                    <asp:TemplateField HeaderText="Select">
                        <HeaderTemplate>
                            <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                            <input id="chkBxWPTransAll" name="chkBxWPTransAll" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBxWPTrans" runat="server" />
                            <asp:HiddenField ID="hdnBxWPTrans" runat="server" Value='<%# Eval("WERPTransactionId").ToString() + "-" +  Eval("RejectReasonCode").ToString()%>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <%--<asp:Button ID="btnEditSelectedWPTrans" CssClass="FieldName" OnClick="btnEditSelectedWPTrans_Click"
                                runat="server" Text="Save" />--%>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                    <HeaderTemplate>
                                <asp:Label ID="lblAdviserName" runat="server" Text="Adviser Org."></asp:Label>
                               <%-- <asp:DropDownList ID="ddlRejectReason" CssClass="cmbLongField" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAdviserOrg"  runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField>                    
                        <HeaderTemplate>
                            <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                            <asp:DropDownList ID="ddlRejectReason" AutoPostBack="true" CssClass="cmbLongField"
                                runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRejectReasonHeader" runat="server" Text='<%# Eval("RejectReason").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrProcessId" runat="server" Text="Process Id"></asp:Label>
                                <asp:DropDownList ID="ddlProcessId" AutoPostBack="true" CssClass="GridViewCmbField" runat="server" 
                                OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="ProcessId" HeaderText="Process Id" />--%>
                    <%-- <asp:TemplateField>
                       <HeaderTemplate>
                            <asp:Label ID="lblPanNumber" runat="server" Text="PAN Number"></asp:Label>
                            <asp:DropDownList ID="ddlPanNumber" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPanNumber_SelectedIndexChanged">
                            </asp:DropDownList>
                            <!--<asp:TextBox ID="txtPanNumberSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedEquityTransactionStaging_btnGridSearch');" /> -->
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtPanNumber" runat="server" Text='<%# Bind("PANNum") %>'></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPanNumberMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="TradeAccountNumber" HeaderText="Trade Account Number" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblScripCode" runat="server" Text="Scrip"></asp:Label>
                            <%--<asp:TextBox ID="txtScripCodeSearch" Text='<%# hdnScripFilter.Value %>' runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedEquityTransactionStaging_btnGridSearch');" />--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtScripCode" runat="server" Text='<%# Bind("ScripCode") %>' EnableViewState="true"></asp:Label>
                        </ItemTemplate>
                        <%--  <FooterTemplate>
                            <asp:TextBox ID="txtScripCodeMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="20">
                        <HeaderTemplate>
                            <asp:Label ID="lblExchange" runat="server" Text="Exchange"></asp:Label>
                            <asp:TextBox ID="txtExchangeSearch" Width="40" runat="server" Text='<%# hdnExchangeFilter.Value %>'
                                CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedEquityTransactionStaging_btnGridSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtExchange" Width="20" runat="server" Text='<%# Bind("Exchange") %>'></asp:Label>
                        </ItemTemplate>
                        <%-- <FooterTemplate>
                            <asp:TextBox ID="txtExchangeMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Share" HeaderText="Share" DataFormatString="{0:f4}" />
                    <%--<asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:f4}"/>--%>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtPrice" runat="server" Text='<%# Eval("Price","{0:f4}")  %>' Style="text-align: right"></asp:Label>
                        </ItemTemplate>
                        <%--  <FooterTemplate>
                            <asp:TextBox ID="txtPriceMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f4}"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:TemplateField HeaderStyle-Width="40">
                        <HeaderTemplate>
                            <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type"></asp:Label>
                            <%--<asp:TextBox ID="txtTransactionTypeSearch"   Text='<%# hdnTransactionTypeFilter.Value %>'   runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedEquityTransactionStaging_btnGridSearch');" />--%>
                            <asp:DropDownList ID="ddlTransactionType" Width="60" runat="server" AutoPostBack="true"
                                runat="server" CssClass="cmbLongField" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtTransactionType" Width="40" runat="server" Text='<%# Bind("TransactionType") %>'></asp:Label>
                            <%--<asp:HiddenField ID="hdnTransactionType" runat="server" Value='<%# Bind("TransactionTypeCode") %>' />
                            <asp:DropDownList ID="ddlTransactionType" runat="server">--%>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <%--  <FooterTemplate>
                            <asp:DropDownList ID="ddlTransactionType" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                    <asp:BoundField DataField="BrokerCode" HeaderText="Broker Code" />                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
                CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RejectedEquityTransactionStaging_btnReprocess','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RejectedEquityTransactionStaging_btnReprocess','L');" />
            <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
                OnClientClick="return ShowPopup()" Visible="false" />
            <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                OnClick="btnDelete_Click" />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyMsg" class="FieldName">
                There are no records to be displayed!</label>
        </td>
    </tr>
    <%--  <tr id="trErrorMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Message" runat="server">
            </asp:Label>
        </td>
    </tr>--%>
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
<asp:HiddenField ID="hdnSort" runat="server" Value="WERPCustomerName ASC" />
<asp:HiddenField ID="hdnPanNumberFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTradeAccountNumberFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnScripFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnExchangeFilter" runat="server" Visible="false" />
<%--<asp:HiddenField ID="hdnShareFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAmountFilter" runat="server" Visible="false" />--%>
<asp:HiddenField ID="hdnTransactionTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
 <asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<%-- <asp:HiddenField ID="hdnIsRejectedFilter" runat="server" Visible="false" />
--%>
<asp:Button ID="btnGridSearch" runat="server" Text="" OnClick="btnGridSearch_Click"
    BorderStyle="None" BackColor="Transparent" />