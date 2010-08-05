<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityManualSingleTransaction.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.EquityManualSingleTransaction" %>
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
<asp:UpdatePanel ID="upnlEQTran" runat="server">
    <ContentTemplate>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Add Equity Transaction"></asp:Label>
            <hr />
        </td>
    </tr>


    <tr>
        <td colspan="3" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with a ' * ' are compulsory</label>
        </td>
    </tr>
    <%--  <tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Select the Portfolio Name:"></asp:Label>
        </td>
        <td colspan="4" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>--%>
</table>
<table width="100%" class="TableBackground">
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Select the Portfolio Name :"></asp:Label>
        </td>
        <td colspan="4" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label2" runat="server" Text="Scrip Particulars :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtScrip" runat="server" CssClass="txtField" AutoPostBack="true"
                OnTextChanged="txtScrip_TextChanged" AutoCompleteType="Disabled" 
                ondatabinding="txtScrip_DataBinding"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoCompleteExtender" TargetControlID="txtScrip"  
                ServiceMethod="GetScripList" ServicePath="AutoComplete.asmx" MinimumPrefixLength="1" OnDataBinding="txtScrip_DataBinding"
                EnableCaching="false" CompletionSetCount="5" CompletionInterval="1000" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem" />
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvScrip" ControlToValidate="txtScrip" ErrorMessage="Please enter a scrip" CssClass="rfvPCG"
                runat="server" InitialValue="" ValidationGroup="EQ">
            </asp:RequiredFieldValidator>
        </td>
      
    </tr>
    <tr>
      <td class="rightField" colspan="2">
            <asp:Label ID="lblScripName" runat="server" Text="Scrip Particulars:" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr id="trTransactionMode" runat="server">
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" Text="Transaction Mode :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:RadioButton ID="rbtnDelivery" runat="server" CssClass="txtField" Text="For Delivery "
                GroupName="TransactionMode" />
            <asp:RadioButton ID="rbtnSpeculation" runat="server" CssClass="txtField" Text="For Speculation"
                GroupName="TransactionMode" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblTranType" runat="server" Text="Transaction type :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:DropDownList ID="ddlTranType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlTranType" CssClass="rfvPCG"
                ErrorMessage="Please select a Transaction Type" Operator="NotEqual" ValueToCompare="Select Transaction"
                ValidationGroup="EQ"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblExchange" runat="server" Text="Exchange :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:DropDownList ID="ddlExchange" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlExchange" CssClass="rfvPCG"
                ErrorMessage="Please select an Exchange" Operator="NotEqual" ValueToCompare="Select an Exchange"
                ValidationGroup="EQ"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblDPAcc" runat="server" Text="Trade Account Number :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlTradeAcc" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddlTradeAcc_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlTradeAcc"
                ErrorMessage="Please select your Trade Number" Operator="NotEqual" ValueToCompare="Select the Trade Number"
                ValidationGroup="EQ"></asp:CompareValidator>
        </td>
        <td>
            <asp:Button ID="btnAddDP" runat="server" Text="Add Trade Account" CssClass="PCGButton"
                OnClick="btnAddDP_Click" />
        </td>
    </tr>
    <%--  <tr id="trScrip" runat="server">
        <td class="leftField">
            <asp:Label ID="lblScrip" runat="server" Text="Scrip Particular:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtScripParticular" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>--%>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblTicker" runat="server" Text="Ticker :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtTicker" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span6" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTicker" CssClass="rfvPCG"
                ErrorMessage="Please enter a Ticker" runat="server" InitialValue="" ValidationGroup="EQ">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <tr id="trTradeDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblTrade" runat="server" Text="Trade Date :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtTradeDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtTradeDate_CalendarExtender" runat="server" TargetControlID="txtTradeDate"
                        Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtTradeDate_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtTradeDate" WatermarkText="dd/mm/yyyy">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtTradeDate" CssClass="rfvPCG"
                        ErrorMessage="<br />Please select a Trade Date" Display="Dynamic" runat="server"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trRate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblRate" runat="server" Text="Rate (Rs) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtRate" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtRate" CssClass="rfvPCG"
                        ErrorMessage="<br />Please enter a Rate" Display="Dynamic" runat="server" InitialValue=""
                        ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtRate" CssClass="rfvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trNoShares" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblNumShares" runat="server" Text="No of Shares :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtNumShares" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <span id="Span9" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtNumShares" CssClass="rfvPCG"
                        ErrorMessage="<br />Please enter the number of shares" Display="Dynamic" runat="server"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtNumShares" CssClass="rfvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trBroker" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblBroker" runat="server" Text="Broker:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtBroker" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr id="trBrokerage" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblBrokerage" runat="server" Text="Brokerage (Per Unit) :" CssClass="FieldName" MaxLength="18"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtBrokerage" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtBrokerage" CssClass="rfvPCG"
                        ErrorMessage="<br />Please enter the Brokerage" Display="Dynamic" runat="server"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtBrokerage" CssClass="rfvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Not acceptable format." ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trOthers" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblOther" runat="server" Text="Other Charges (Per Unit) :" CssClass="FieldName" MaxLength="18"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOtherCharge" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtOtherCharge"
                        ErrorMessage="<br />Please enter the other charges" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtOtherCharge" CssClass="rfvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Not acceptable format." ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="PCGButton"
                        OnClick="btnCalculate_Click" />
                </td>
            </tr>
            <tr id="trServiceTax" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblServiceTax" runat="server" Text="Service Tax + Education Cess :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtTax" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span13" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtTax" CssClass="rfvPCG"
                        ErrorMessage="<br />Please enter the service tax" Display="Dynamic" runat="server"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="<br />Enter a numeric value"
                        Type="Double" ControlToValidate="txtTax" Operator="DataTypeCheck" Display="Dynamic"
                        ValidationGroup="EQ"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSTT" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblSTT" runat="server" Text="STT :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span14" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtSTT" CssClass="rfvPCG"
                        ErrorMessage="<br />Please enter the STT" Display="Dynamic" runat="server" InitialValue=""
                        ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                  <%--  <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="<br />Enter a numeric value"
                        Type="Double" ControlToValidate="txtSTT" Operator="DataTypeCheck" Display="Dynamic"
                        ValidationGroup="EQ"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr id="trRateInc" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblRateIncluBrokerage" runat="server" Text="Rate Inclusive of Brokerage :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtRateIncBrokerage" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span15" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtRateIncBrokerage"
                        ErrorMessage="<br />Please enter the rate inclusive of brokerage" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="<br />Enter a numeric value"
                        Type="Double" ControlToValidate="txtRateIncBrokerage" Operator="DataTypeCheck" CssClass="rfvPCG"
                        Display="Dynamic" ValidationGroup="EQ"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trTotal" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblTotal" runat="server" Text="Trade Consideration total :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span16" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtTotal"
                        ErrorMessage="<br />Please enter the total" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="<br />Enter a numeric value"
                        Type="Double" ControlToValidate="txtTotal" Operator="DataTypeCheck" Display="Dynamic" CssClass="rfvPCG"
                        ValidationGroup="EQ"></asp:CompareValidator>
                </td>
            </tr>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
</table>
</ContentTemplate> </asp:UpdatePanel>
<table width="100%" class="TableBackground">
    <tr class="SubmitCell">
        <td colspan="3">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EquityManualSingleTransaction__btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EquityManualSingleTransaction__btnSubmit', 'S');"
                OnClick="btnSubmit_Click" ValidationGroup="EQ" />
        </td>
    </tr>
</table>
