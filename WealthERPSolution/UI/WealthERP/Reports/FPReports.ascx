<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FPReports.ascx.cs" Inherits="WealthERP.Reports.FPReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function validate(type) {

        window.document.forms[0].target = '_blank';
        if (type == 'mail')
            window.document.forms[0].action = "/Reports/Display.aspx?mail=1";
        else
            window.document.forms[0].action = "/Reports/Display.aspx?mail=0";
        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "";
        }, 500);
        return true;
    }
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
   
    
</script>

<table border="0" width="100%">
    <tr>
        <td>
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="Financial Planning Reports"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblSelectCustomer" runat="server" CssClass="HeaderTextSmall" Style='font-weight: normal;'
                Text="Select Customer"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" id="tblIndividual">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Customer Name :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                            AutoPostBack="true"></asp:TextBox><ajaxToolkit:TextBoxWatermarkExtender ID="txtCustomer_TextBoxWatermarkExtender"
                                runat="server" TargetControlID="txtCustomer" WatermarkText="Type the Customer Name">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="txtCustomer_autoCompleteExtender" runat="server"
                            TargetControlID="txtCustomer" ServiceMethod="GetAdviserCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                            MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                            CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                            CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            UseContextKey="true" OnClientItemSelected="GetCustomerId" />
                        <span id="Span1" class="spnRequiredField">*<br />
                        </span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                            ErrorMessage="Please Enter Customer Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                            ValidationGroup="btnSubmit">
                        </asp:RequiredFieldValidator><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                            few characters of customer name.</span>
                    </td>
                </tr>
                <tr id="trCustomerDetails" runat="server" visible="false">
                    <td>
                        <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="PAN :"></asp:Label>
                        <asp:TextBox ID="txtPanParent" runat="server" CssClass="txtField" BackColor="Transparent"
                            BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblAddress" runat="server" CssClass="FieldName" Text="Address:"></asp:Label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="txtField" BackColor="Transparent"
                            BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnView" runat="server" Text="View Report" OnClientClick="return validate('')"
                            PostBackUrl="~/Reports/Display.aspx" CssClass="PCGMediumButton" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
