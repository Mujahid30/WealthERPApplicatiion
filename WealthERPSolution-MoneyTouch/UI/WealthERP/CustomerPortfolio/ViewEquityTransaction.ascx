<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEquityTransaction.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewEquityTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlViewEquity" runat="server">
    <ContentTemplate>
        <table style="width: 100%;" class="TableBackground">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lebHeader" runat="server" Text="Equity Transaction Details" CssClass="HeaderTextBig"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                        OnClick="lnkBtnBack_Click"></asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="LinkButtons" OnClick="LinkButton1_Click"
                        Text="Edit">Edit</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="leftField" style="width: 25%;">
                    <asp:Label runat="server" CssClass="FieldName" Text="Scrip Particular:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtScrip" runat="server" CssClass="txtField" AutoPostBack="True"
                        OnTextChanged="txtScrip_TextChanged"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoCompleteExtender" TargetControlID="txtScrip"
                        ServicePath="AutoComplete.asmx" ServiceMethod="GetScripList" MinimumPrefixLength="1"
                        EnableCaching="true" CompletionSetCount="12" CompletionInterval="1000" CompletionListItemCssClass="txtField" />
                    <div id="dvScrip" runat="server" class="dvInLine">
                        <span id="Span1" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvScrip" ControlToValidate="txtScrip" ErrorMessage="Please enter a scrip"
                            runat="server" InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Transaction Type:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlTranType" runat="server" CssClass="cmbField" AutoPostBack="True">
                    </asp:DropDownList>
                    <div id="dvTransactionType" runat="server" class="dvInLine">
                        <span id="Span2" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlTranType"
                            ErrorMessage="Please select a Transaction Type" Operator="NotEqual" ValueToCompare="Select Transaction"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Exchange:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlExchange" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <div id="dvExchange" runat="server" class="dvInLine">
                        <span id="Span3" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlExchange"
                            ErrorMessage="Please select an Exchange" Operator="NotEqual" ValueToCompare="Select an Exchange"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Trade Account Number:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlTradeAcc" runat="server" CssClass="cmbField" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlTradeAcc_SelectedIndexChanged">
                    </asp:DropDownList>
                    <div id="dvTradeAccount" runat="server" class="dvInLine">
                        <span id="Span4" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlTradeAcc"
                            ErrorMessage="Please select your Trade Number" Operator="NotEqual" ValueToCompare="Select the Trade Number"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Ticker:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTicker" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvTicker" runat="server" class="dvInLine">
                        <span id="Span6" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTicker"
                            ErrorMessage="Please enter a Ticker" runat="server" InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Trade Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTradeDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtTradeDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtTradeDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtTradeDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtTradeDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <div id="dvTradeDate" runat="server" class="dvInLine">
                        <span id="Span7" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtTradeDate"
                            ErrorMessage="<br />Please select a Trade Date" Display="Dynamic" runat="server"
                            InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Rate:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtRate" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvRate" runat="server" class="dvInLine">
                        <span id="Span8" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtRate"
                            ErrorMessage="<br />Please enter a Rate" Display="Dynamic" runat="server" InitialValue=""
                            ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br />Enter a numeric value"
                            Type="Double" ControlToValidate="txtRate" Operator="DataTypeCheck" Display="Dynamic"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="No of Shares:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtNumShares" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvNumShares" runat="server" class="dvInLine">
                        <span id="Span9" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtNumShares"
                            ErrorMessage="<br />Please enter the number of shares" Display="Dynamic" runat="server"
                            InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="<br />Enter an integer value"
                            Type="Integer" ControlToValidate="txtNumShares" Operator="DataTypeCheck" Display="Dynamic"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Broker:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtBroker" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvBroker" runat="server" class="dvInLine">
                        <span id="Span10" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtNumShares"
                            ErrorMessage="<br />Please enter the number of shares" Display="Dynamic" runat="server"
                            InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br />Enter an integer value"
                            Type="Integer" ControlToValidate="txtNumShares" Operator="DataTypeCheck" Display="Dynamic"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Brokerage:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtBrokerage" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvBrokerage" runat="server" class="dvInLine">
                        <span id="Span11" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtBrokerage"
                            ErrorMessage="<br />Please enter the brokerage" Display="Dynamic" runat="server"
                            InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="<br />Enter a numeric value"
                            Type="Double" ControlToValidate="txtBrokerage" Operator="DataTypeCheck" Display="Dynamic"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label12" runat="server" CssClass="FieldName" Text="Other Charges:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOtherCharge" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvOtherCharge" runat="server" class="dvInLine">
                        <span id="Span12" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtOtherCharge"
                            ErrorMessage="<br />Please enter the other charges" Display="Dynamic" runat="server"
                            InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />Enter a numeric value"
                            Type="Double" ControlToValidate="txtOtherCharge" Operator="DataTypeCheck" Display="Dynamic"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="ButtonField"
                        OnClick="btnCalculate_Click" />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label13" runat="server" CssClass="FieldName" Text="Service Tax:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTax" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvTax" runat="server" class="dvInLine">
                        <span id="Span13" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtTax"
                            ErrorMessage="<br />Please enter the service tax" Display="Dynamic" runat="server"
                            InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="<br />Enter a numeric value"
                            Type="Double" ControlToValidate="txtTax" Operator="DataTypeCheck" Display="Dynamic"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label14" runat="server" CssClass="FieldName" Text="STT:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvSTT" runat="server" class="dvInLine">
                        <span id="Span14" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtSTT"
                            ErrorMessage="<br />Please enter the STT" Display="Dynamic" runat="server" InitialValue=""
                            ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="<br />Enter a numeric value"
                            Type="Double" ControlToValidate="txtSTT" Operator="DataTypeCheck" Display="Dynamic"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label15" runat="server" CssClass="FieldName" Text="Rate Inclusive of Brokerage:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtRateIncBrokerage" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvRateIncBrokerage" runat="server" class="dvInLine">
                        <span id="Span15" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtRateIncBrokerage"
                            ErrorMessage="<br />Please enter the rate inclusive of brokerage" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="<br />Enter a numeric value"
                            Type="Double" ControlToValidate="txtRateIncBrokerage" Operator="DataTypeCheck"
                            Display="Dynamic" ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label16" runat="server" CssClass="FieldName" Text="Trade Consideration Total:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="txtField"></asp:TextBox>
                    <div id="dvTotal" runat="server" class="dvInLine">
                        <span id="Span16" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtTotal"
                            ErrorMessage="<br />Please enter the total" Display="Dynamic" runat="server"
                            InitialValue="" ValidationGroup="EQ">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="<br />Enter a numeric value"
                            Type="Double" ControlToValidate="txtTotal" Operator="DataTypeCheck" Display="Dynamic"
                            ValidationGroup="EQ"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="SubmitCell" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrlEquityManualSingleTransaction__btnSubmit','S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrlEquityManualSingleTransaction__btnSubmit','S');"
                        OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
