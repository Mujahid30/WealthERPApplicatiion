<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewMFTransaction.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewMFTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script type="text/javascript">
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

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table style="width: 100%;" class="TableBackground">
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="MF Transaction "></asp:Label>
        </td>
        <td colspan="2" align="center">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>--%>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Mutual Fund Transaction Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
        <td>
            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" CssClass="LinkButtons"> Edit</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblSchemeName" runat="server" CssClass="FieldName" Text="Scheme Name:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:Label ID="lblScheme" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" Text="Transaction Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:Label ID="lblTransactionType" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:Label ID="lblFolioNumber" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" Text="Transaction Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtTransactionDate_CalendarExtender" runat="server"
                Enabled="True" TargetControlID="txtTransactionDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </ajaxToolkit:CalendarExtender>
            <div id="dvTransactionDate" runat="server" class="dvInLine">
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtTransactionDate"
                    ErrorMessage="<br />Please select a Transaction Date" CssClass="Error" Display="Dynamic" runat="server"
                    InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtTransactionDate" CssClass="Error" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
            </div>
        </td>
    </tr>
    <tr id="trDividendRate" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" Text="Divident Rate :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtDividentRate" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
            <div id="dvDividentRate" runat="server" class="dvInLine">
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDividentRate"
                    ErrorMessage="<br />Please enter a Dividend Rate" Display="Dynamic" runat="server"
                    InitialValue="" CssClass="Error">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDividentRate"
                    Display="Dynamic" runat="server" CssClass="Error" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" Text="NAV :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtNAV" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
            <div id="dvNAV" runat="server" class="dvInLine">
                <span id="Span3" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNAV"
                    ErrorMessage="<br />Please enter the NAV" CssClass="Error" Display="Dynamic" runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtNAV"
                    Display="Dynamic" runat="server" CssClass="Error" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label18" runat="server" Text="Price :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtPrice" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
            <div id="dvPrice" runat="server" class="dvInLine">
                <span id="Span4" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPrice"
                    ErrorMessage="<br />Please enter the price" CssClass="Error" Display="Dynamic" runat="server"
                    InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPrice"
                    Display="Dynamic" runat="server" ErrorMessage="Not acceptable format" CssClass="Error" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label9" runat="server" Text="Amount :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" MaxLength="18" 
                OnTextChanged="txtAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
            <div id="dvAmount" runat="server" class="dvInLine">
                <span id="Span5" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtAmount"
                    ErrorMessage="<br />Please enter the amount" CssClass="Error" Display="Dynamic" runat="server"
                    InitialValue="" >
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtAmount"
                    Display="Dynamic" runat="server" CssClass="Error" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" Text="Units :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtUnits" runat="server" CssClass="txtField" MaxLength="18" OnTextChanged="txtUnits_TextChanged"></asp:TextBox>
            <div id="dvUnits" runat="server" class="dvInLine">
                <span id="Span6" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtUnits"
                    ErrorMessage="<br />Please enter the units" Display="Dynamic" runat="server"
                    InitialValue="" CssClass="Error">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtUnits"
                    Display="Dynamic" runat="server" CssClass="Error" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
            </div>
        </td>
    </tr>
    <tr id="trSTT" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" Text="STT :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
            <div id="dvSTT" runat="server" class="dvInLine">
                <span id="Span7" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtSTT"
                    ErrorMessage="<br />Please enter the STT" CssClass="Error" Display="Dynamic" runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtSTT"
                    Display="Dynamic" runat="server" CssClass="Error" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblTransactionStatus" runat="server" Text="Transaction Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:Label ID="lblTransactionStatusValue" runat="server" Text="" CssClass="Field"></asp:Label>
       </td>
    </tr>
    <tr>
        <td class="SubmitCell">
            &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="PCGButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewMFTransaction_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewMFTransaction_btnSubmit', 'S');"
                OnClick="btnSubmit_Click" />
        </td>
        <td>                <asp:Button ID="btnCancel" runat="server" 
                Text="Cancel Transaction" CssClass="PCGLongButton" Visible="false" 
                onclick="btnCancel_Click" />
</td>
    </tr>
</table>
