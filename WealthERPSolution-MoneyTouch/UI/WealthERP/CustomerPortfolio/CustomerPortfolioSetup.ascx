<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerPortfolioSetup.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerPortfolioSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
</script>

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Customer Portfolio Setup"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="lblCustomer" runat="server" CssClass="FieldName" Text="Customer:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomer_TextChanged" />
            <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="true"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtCustomer_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtCustomer" WatermarkText="Type the Customer Name">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="2" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetCustomerId" />
            <span id="Span1" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                ErrorMessage="Please Enter Customer Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPanCustomer" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPanCustomer" runat="server" CssClass="txtField" BackColor="Transparent"
                BorderStyle="None"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblAddressCustomer" runat="server" CssClass="FieldName" Text="Address:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAddressCustomer" runat="server" CssClass="txtField" BackColor="Transparent"
                BorderStyle="None" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPortfolioName" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPortfolioName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="spFirstName" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="rfvPortfolioName" ControlToValidate="txtPortfolioName"
                ErrorMessage="Please Enter Portfolio Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="lblPortfolioType" runat="server" CssClass="FieldName" Text = "Type:"></asp:Label>
        </td>
        <td>
        <asp:DropDownList ID = "ddlPortfolioType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="ddlPortfolioType_CompareValidator" runat="server" ControlToValidate="ddlPortfolioType"
                ValidationGroup="btnSubmit" ErrorMessage="Please Select a Portfolio Type" Operator="NotEqual"
                ValueToCompare="Select Portfolio Type" CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPMSIdentifier" runat="server" CssClass="FieldName" Text="PMS Identifier:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPMSIdentifier" runat="server" CssClass="txtField" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                ValidationGroup="btnSubmit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_FamilyDetailsChild_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_FamilyDetailsChild_btnSubmit', 'S');" />
        </td>
        <td class="SubmitCell">
            <asp:Button ID="btnSave" runat="server" Text="Save and Add More" OnClick="btnSave_Click"
                ValidationGroup="btnSubmit" CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_FamilyDetailsChild_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_FamilyDetailsChild_btnSubmit', 'S');" />
        </td>
    </tr>
</table>
