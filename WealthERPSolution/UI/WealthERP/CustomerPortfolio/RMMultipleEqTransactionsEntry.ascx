<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMMultipleEqTransactionsEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.RMMultipleEqTransactionsEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript" language="javascript">

    function GetParentCustomerId(source, eventArgs) {
        document.getElementById("<%= txtParentCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }
    function CheckIfCustomerSelected() {
        var customerId = document.getElementById("<%= txtParentCustomerId.ClientID %>").value;
        if (customerId > 0)
            return true;
        else {
            alert("Please pick Customer");
            return false;
        }
    }
    function SetValues() {
    
        var otherCharges = .05;
        
        var brokerage = 0.0;
        var stt = 0.0;
        
        var rate = document.getElementById("<%= txtRate.ClientID %>").value
        isDelivery = document.getElementById("<%= rdoDelivery.ClientID %>").checked;
        var quantity = document.getElementById("<%= txtQuantity.ClientID %>").value

        if (quantity > 0 && rate > 0) {

            if (isDelivery) {
                brokerage = 0.5;
                stt = 0.125;
                document.getElementById("<%= txtBrokerage.ClientID %>").value = ((quantity * rate * brokerage)/100).toFixed(4);
                document.getElementById("<%= txtSTT.ClientID %>").value = ((quantity * rate * stt) /100).toFixed(4);
            }
            else {
                brokerage = 0.05;
                stt = 0.025;
                document.getElementById("<%= txtBrokerage.ClientID %>").value = ((quantity * rate * brokerage)/100).toFixed(4);
                document.getElementById("<%= txtSTT.ClientID %>").value = ((quantity * rate * stt)/100).toFixed(4);
            }
            
            document.getElementById("<%= txtOtherCharges.ClientID %>").value = ((quantity * otherCharges)).toFixed(4);
            
            var grossPrice = (parseFloat(rate) * parseFloat(quantity)) + parseFloat(document.getElementById("<%= txtSTT.ClientID %>").value) +
                             parseFloat(document.getElementById("<%= txtBrokerage.ClientID %>").value) +
                             parseFloat(document.getElementById("<%= txtOtherCharges.ClientID %>").value);
        
            if (!isNaN(grossPrice))
                document.getElementById("<%= txtGrossPrice.ClientID %>").value = grossPrice.toFixed(4);
        }
        else {
            document.getElementById("<%= txtGrossPrice.ClientID %>").value = "";
            document.getElementById("<%= txtBrokerage.ClientID %>").value = "";
            document.getElementById("<%= txtSTT.ClientID %>").value = "";
        }
    }
</script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
   


</script>

<table border="0" width="100%">
    <tr>
        <td class="HeaderTextBig" colspan="4">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Equity Multiple Transaction entry"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblParentCustomer" runat="server" CssClass="FieldName" Text="Pick Customer:"></asp:Label>
        </td>
        <td>
            <asp:HiddenField ID="txtParentCustomerId" runat="server" OnValueChanged="txtParentCustomerId_ValueChanged" />
            <%--<asp:HiddenField ID="txtParentCustomerType" runat="server" />--%>
            <asp:TextBox ID="txtParentCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="true"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtParentCustomer_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtParentCustomer" WatermarkText="Type the Customer Name">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtParentCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtParentCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetParentCustomerId" />
            <span id="Span1" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtParentCustomer"
                ErrorMessage="Please Enter Customer Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>            
        </td>        
        <td align="right">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Trade Account No:"></asp:Label>
        </td>
        <td>
            <%--<asp:TextBox ID="txtTradeAccountNo" runat="server" CssClass="txtField"></asp:TextBox>--%>
            <asp:DropDownList ID="ddlTradeAccountNos" runat="server" CssClass="cmbField">
                <asp:ListItem Text="" Value="-1"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnAddTradeAccount" runat="server" Text="Add"  CssClass="PCGButton" style=" width: 40px;"
                onclick="btnAddTradeAccount_Click"  OnClientClick="return CheckIfCustomerSelected()" CausesValidation="false" />
            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvTradeAccountNos" runat="server"
                ControlToValidate="ddlTradeAccountNos" CssClass="rfvPCG" ErrorMessage="<br>Select Trade Account No"
                InitialValue="-1"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr><td align="right"><asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="PAN :"></asp:Label>
            </td><td><asp:TextBox ID="txtPanParent" runat="server" CssClass="txtField" BackColor="Transparent"
                BorderStyle="None"></asp:TextBox></td></tr>
            <tr>
                <td align="right">
                    <!-- class="leftField" -->
                    <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Scrip Name:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtScrip" runat="server" CssClass="txtField" AutoPostBack="True"
                        OnTextChanged="txtScrip_TextChanged" AutoComplete="Off"></asp:TextBox>  <span id="Span2" class="spnRequiredField">*</span>
                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoCompleteExtender" TargetControlID="txtScrip"
                        ServicePath="~/CustomerPortfolio/AutoComplete.asmx" ServiceMethod="GetScripList"
                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="12" CompletionInterval="1000"
                        CompletionListItemCssClass="txtField" />
                    <div id="dvScrip" runat="server" class="dvInLine">
                       
                        <asp:RequiredFieldValidator ID="rfvScrip" ControlToValidate="txtScrip" ErrorMessage="Please enter a scrip"
                            runat="server" InitialValue="" CssClass="rfvPCG"  >
                        </asp:RequiredFieldValidator>
                    </div>
                </td>
                <td align="right">
                    <asp:Label ID="lbl" runat="server" CssClass="FieldName" Text="Transaction Type:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="cmbField">
                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Purchase" Value="Purchase"></asp:ListItem>
                        <asp:ListItem Text="Sell" Value="Sell"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvTransactionType" runat="server"
                        ControlToValidate="ddlTransactionType" ErrorMessage="<br>Select Transaction Type"
                        CssClass="rfvPCG" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>

    <tr>
        <td align="right">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Quantity:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtField" onblur="SetValues()"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="txtQuantity_E" runat="server" Enabled="True"
                TargetControlID="txtQuantity" FilterType="Numbers">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
        <td align="right">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Brokerage:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtBrokerage" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="txtBrokerage_Ex" runat="server" Enabled="True"
                TargetControlID="txtBrokerage" FilterType="Custom, Numbers" ValidChars=".">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Transaction Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <ajaxToolkit:CalendarExtender ID="txtTransactionDate_CalendarExtender" runat="server"
                TargetControlID="txtTransactionDate" Format="dd/MM/yyyy">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtTransactionDate_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtTransactionDate" WatermarkText="dd/mm/yyyy">
            </ajaxToolkit:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTransactionDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Transaction Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvTransactionDate" runat="server" ErrorMessage="Transaction Date should not be greater than current date."
                Type="Date" ControlToValidate="txtTransactionDate" Operator="LessThanEqual" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td align="right">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="STT charges:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="txtSTT_Ex" runat="server" Enabled="True"
                TargetControlID="txtSTT" FilterType="Custom, Numbers" ValidChars=".">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Rate(in Rs per unit) :"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtRate" runat="server" onblur="SetValues()" CssClass="txtField" ></asp:TextBox>
            <span id="Span6" class="spnRequiredField">*</span>
            <ajaxToolkit:FilteredTextBoxExtender ID="txtRate_Ex" runat="server" Enabled="True"
                TargetControlID="txtRate" FilterType="Custom, Numbers" ValidChars=".">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
        <td align="right">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Other charges:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="txtOtherCharges_Ex" runat="server" Enabled="True"
                TargetControlID="txtOtherCharges" FilterType="Custom, Numbers" ValidChars=".">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
    </tr>

    <tr>
        <td align="right">
            <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Transaction Mode :"></asp:Label>
        </td>
        <td>
            <asp:RadioButton ID="rdoDelivery" GroupName="Delivery" runat="server" Text="Delivery"
                CssClass="txtField" onchange="SetValues()" />
            <asp:RadioButton Checked="true" ID="rdoSpeculative" GroupName="Delivery" runat="server"
                Text="Speculative:" CssClass="txtField" onchange="SetValues()" />
        </td>
        <td align="right">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Gross Price :"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtGrossPrice" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <ajaxToolkit:FilteredTextBoxExtender ID="txtGrossPrice_Ex" runat="server" Enabled="True"
                TargetControlID="txtGrossPrice" FilterType="Custom, Numbers" ValidChars=".">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 50px">
            <asp:Button ID="btnSaveAndAdd" runat="server" Text="Save & Add More" CssClass="PCGMediumButton"
                OnClick="btnSaveAndAdd_Click1" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="PCGMediumButton" OnClick="btnSave_Click" />
        </td>
    </tr>
    <asp:HiddenField ID="hidScrip" runat="server" />
    <asp:HiddenField ID="hidCustomerId" runat="server" />
</table>
