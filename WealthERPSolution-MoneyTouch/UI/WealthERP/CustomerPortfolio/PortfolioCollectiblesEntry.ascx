<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioCollectiblesEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioCollectiblesEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<table class="TableBackground" style="width: 100%;">
    <tr>
        <td class="HeaderCell" colspan="4">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Add Collectibles"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
        </td>
    </tr>
</table>
<table class="TableBackground" style="width: 100%;">
    <tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <td class="leftField">
            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Asset Category:"></asp:Label>
        </td>
        <td colspan="3" class="rightField">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="cvInsuranceIssuerCode" runat="server" ControlToValidate="ddlCategory"
                ValidationGroup="btnSubmit" ErrorMessage="Please select an Category" Operator="NotEqual"
                ValueToCompare="Select a Category" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Asset Particulars:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPurchaseDate" runat="server" CssClass="FieldName" Text="Purchase Date:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender_txtPurchaseDate" runat="server" TargetControlID="txtPurchaseDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_txtPurchaseDate" runat="server"
                TargetControlID="txtPurchaseDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Purchase Value(Rs):"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPurchaseValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPurchaseValue"
                        ErrorMessage="Please enter the Purchase Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPurchaseValue"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" Text="Current Value(Rs):"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCurrentValue"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter the Current Value"
                Display="Dynamic" runat="server" CssClass="cvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" CssClass="cvPCG"
                ControlToValidate="txtCurrentValue" ValidationGroup="btnSubmit" Display="Dynamic"
                runat="server" ErrorMessage="Please Enter Numeric Values" ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td class="rightField" rowspan="2" colspan="3">
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtField" Height="64px" Width="208px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="SubmitCell" colspan="4">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnSubmit_Click"
                ValidationGroup="btnSubmit" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioCollectiblesEntry_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioCollectiblesEntry_btnSubmit');" />
            <asp:Button ID="btnSaveChanges" runat="server" Text="Update" CssClass="PCGButton"
                ValidationGroup="btnSubmit" OnClick="btnSaveChanges_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioCollectiblesEntry_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioCollectiblesEntry_btnSubmit');" />
        </td>
    </tr>
</table>
