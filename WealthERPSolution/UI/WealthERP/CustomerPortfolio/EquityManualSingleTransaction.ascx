<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityManualSingleTransaction.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.EquityManualSingleTransaction" EnableViewState="true" %>
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

    function GetSchemeCode(source, eventArgs) {

        document.getElementById("<%= txtScripCode.ClientID %>").value = eventArgs.get_value();

        return false;
    };
    
</script>

<script type="text/javascript">
    function ChkForMainPortFolio(source, args) {

        var hdnIsCustomerLogin = document.getElementById('ctrl_EquityManualSingleTransaction_hdnIsCustomerLogin').value;
        var hdnIsMainPortfolio = document.getElementById('ctrl_EquityManualSingleTransaction_hdnIsMainPortfolio').value;

        if (hdnIsCustomerLogin == "Customer" && hdnIsMainPortfolio == "1") {

            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }

    }    
</script>

<script type="text/javascript">
    function openpopupAddTradeAccNum() {
        window.open('PopUp.aspx?PageId=CustomerEQAccountAdd&Equitypopup=1', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return true;
    }
</script>

<script type="text/javascript">
    function openpopupAddDematAccNum() {
        window.open('PopUp.aspx?PageId=AddDematAccountDetails&Equitypopup=1', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return true;
    }
</script>

<script type="text/javascript">
    function AddDividend() {
        var txtscripcode = document.getElementById("<%= txtScrip.ClientID %>").value;
        if (txtscripcode == "") {
            alert("Please Select a Scrip")
        }
    }
</script>

<script type="text/javascript">

    function BrokerageCalculation(sender, args) {
        var Sebi = document.getElementById('ctrl_EquityManualSingleTransaction_hdnSebiCharge').value;
        if (Sebi == "")
            Sebi = 0;
        var hdnstax = document.getElementById('ctrl_EquityManualSingleTransaction_hdnServiceTax').value;
        if (hdnstax == "")
            hdnstax = 0;
        var hdntrxn = document.getElementById('ctrl_EquityManualSingleTransaction_hdnTrxnCharge').value;
        if (hdntrxn == "")
            hdntrxn = 0;
        var hdnstamp = document.getElementById('ctrl_EquityManualSingleTransaction_hdnStampCharge').value;
        if (hdnstamp == "")
            hdnstamp = 0;
        var hdnstt = document.getElementById('ctrl_EquityManualSingleTransaction_hdnStt').value;
        if (hdnstt == "")
            hdnstt = 0;
        var hdnDiffernceInBrkrg = document.getElementById("<%= TxtDiffInBrkrg.ClientID %>").value;
        var rate = document.getElementById("<%= txtRate.ClientID %>").value;

        var trtype = document.getElementById('<%=ddlTransactionType.ClientID %>').value;
        var NoOfShares = document.getElementById("<%= txtNumShares.ClientID %>").value;
        var Brokerage = document.getElementById("<%=txtBrokerage.ClientID %>").value;

        var a = parseFloat(Brokerage) * parseInt(NoOfShares);

        document.getElementById("<%=txtbrokerageamt.ClientID %>").value = a.toFixed(4);
        if (trtype != 2) {
            var rateinc = parseFloat(rate) + parseFloat(parseFloat(Brokerage));
        }
        else {
            var rateinc = parseFloat(rate) - parseFloat(parseFloat(Brokerage));
        }
        document.getElementById("<%=txtRateIncBrokerage.ClientID %>").value = parseFloat(rateinc).toFixed(6);
        var Total = parseFloat(rateinc) * parseInt(NoOfShares);
        document.getElementById("<%=TxtTradetotalIncBrkrg.ClientID %>").value = parseFloat(Total).toFixed(4);

        if (hdnDiffernceInBrkrg == "")
        { hdnDiffernceInBrkr = 0 }
        if (trtype != 2) {
            var Allcharges = parseFloat(rate) + parseFloat(Brokerage) + parseFloat(Sebi) +
                parseFloat(hdnstax) + parseFloat(hdntrxn) + parseFloat(hdnstamp) + parseFloat(hdnstt) + parseFloat(hdnDiffernceInBrkrg)
        }
        else {
            var Allcharges = parseFloat(rate) - parseFloat(Brokerage) - parseFloat(Sebi) +
                parseFloat(hdnstax) - parseFloat(hdntrxn) - parseFloat(hdnstamp) - parseFloat(hdnstt) - parseFloat(hdnDiffernceInBrkrg)
        }
        document.getElementById("<%=TxtRateInBrkrgAllCharges.ClientID %>").value = parseFloat(Allcharges).toFixed(6);
        document.getElementById("<%=txtTotal.ClientID %>").value = parseFloat(parseFloat(Allcharges) * parseInt(NoOfShares)).toFixed(4);

    }
         
</script>

<script type="text/javascript">

    function SebiTurnOverCalculation(sender, args) {

        var Sebi = document.getElementById("<%= txtSebiTurnOver.ClientID %>").value;
        var hdnstax = document.getElementById('ctrl_EquityManualSingleTransaction_hdnstax').value;
        if (hdnstax == "") {
            hdnstax = 0;
        }
        var trans = document.getElementById("<%= TxtTrxnChrgs.ClientID %>").value;
        var stamp = document.getElementById("<%= TxtStampCharges.ClientID %>").value;
        var stt = document.getElementById("<%= txtSTT.ClientID %>").value
        var trtype = document.getElementById('<%=ddlTransactionType.ClientID %>').value;
        var hdnDiffernceInBrkrg = document.getElementById("<%= TxtDiffInBrkrg.ClientID %>").value;
        var rate = document.getElementById("<%= txtRate.ClientID %>").value;

        var NoOfShares = document.getElementById("<%= txtNumShares.ClientID %>").value;


        var Brokerage = document.getElementById("<%=txtBrokerage.ClientID %>").value;

        var a = parseFloat(Brokerage) + parseFloat(Sebi) + parseFloat(trans) + parseFloat(stamp);
        var stax = parseFloat(parseFloat(hdnstax) * parseFloat(a));
        var Total = parseFloat(Sebi) * parseInt(NoOfShares);
        document.getElementById("<%=txtSebiTurnOverAmount.ClientID %>").value = parseFloat(Total).toFixed(4);


        document.getElementById("<%=txtTax.ClientID %>").value = parseFloat(stax).toFixed(6);
        var Totstax = parseFloat(stax) * parseInt(NoOfShares);
        document.getElementById("<%=txtServiceAmt.ClientID %>").value = parseFloat(Totstax).toFixed(4);

        var OtherCharge = parseFloat(Sebi) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(stax);
        var TotOthercharge = parseFloat(OtherCharge) * parseFloat(NoOfShares);

        document.getElementById("<%=txt_OtherCharges.ClientID %>").value = parseFloat(OtherCharge).toFixed(6);
        document.getElementById("<%=TxtOtherChargeAmt.ClientID %>").value = parseFloat(TotOthercharge).toFixed(4);

        if (hdnDiffernceInBrkrg == "") {
            hdnDiffernceInBrkrg = 0;
        }
        if (trtype != 2) {
            var Allcharges = parseFloat(rate) + parseFloat(Brokerage) + parseFloat(Sebi) +
        parseFloat(stax) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(hdnDiffernceInBrkrg)
        }
        else {
            var Allcharges = parseFloat(rate) - parseFloat(Brokerage) - parseFloat(Sebi) -
        parseFloat(stax) - parseFloat(trans) - parseFloat(stamp) - parseFloat(stt) - parseFloat(hdnDiffernceInBrkrg)
        }

        document.getElementById("<%=TxtRateInBrkrgAllCharges.ClientID %>").value = parseFloat(Allcharges).toFixed(6);
        var AllchargTot = parseFloat(Allcharges) * parseInt(NoOfShares);

        document.getElementById("<%=txtTotal.ClientID %>").value = parseFloat(AllchargTot).toFixed(4);
        return false;
    }
        
      
</script>

<script type="text/javascript">

    function TrxnChargeCalculation(sender, args) {


        var Sebi = document.getElementById("<%= txtSebiTurnOver.ClientID %>").value;
        var hdnstax = document.getElementById('ctrl_EquityManualSingleTransaction_hdnstax').value;
        if (hdnstax == "")
            hdnstax = 0;
        var trans = document.getElementById("<%= TxtTrxnChrgs.ClientID %>").value;
        var stamp = document.getElementById("<%= TxtStampCharges.ClientID %>").value;
        var stt = document.getElementById("<%= txtSTT.ClientID %>").value
        var trtype = document.getElementById('<%=ddlTransactionType.ClientID %>').value;
        var hdnDiffernceInBrkrg = document.getElementById("<%= TxtDiffInBrkrg.ClientID %>").value;
        var rate = document.getElementById("<%= txtRate.ClientID %>").value;

        var NoOfShares = document.getElementById("<%= txtNumShares.ClientID %>").value;


        var Brokerage = document.getElementById("<%=txtBrokerage.ClientID %>").value;

        var a = parseFloat(Brokerage) + parseFloat(Sebi) + parseFloat(trans) + parseFloat(stamp);
        var stax = parseFloat(parseFloat(hdnstax) * parseFloat(a));
        var Total = parseFloat(trans) * parseInt(NoOfShares);
        document.getElementById("<%=txtTrxnAmount.ClientID %>").value = parseFloat(Total).toFixed(6);


        document.getElementById("<%=txtTax.ClientID %>").value = parseFloat(stax).toFixed(4);
        var Totstax = parseFloat(stax) * parseInt(NoOfShares);
        document.getElementById("<%=txtServiceAmt.ClientID %>").value = parseFloat(Totstax).toFixed(4);

        var OtherCharge = parseFloat(Sebi) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(stax);
        var TotOthercharge = parseFloat(OtherCharge) * parseFloat(NoOfShares);

        document.getElementById("<%=txt_OtherCharges.ClientID %>").value = parseFloat(OtherCharge).toFixed(6);
        document.getElementById("<%=TxtOtherChargeAmt.ClientID %>").value = parseFloat(TotOthercharge).toFixed(4);

        if (hdnDiffernceInBrkrg == "") {
            hdnDiffernceInBrkrg = 0;
        }
        if (trtype != 2) {
            var Allcharges = parseFloat(rate) + parseFloat(Brokerage) + parseFloat(Sebi) +
        parseFloat(stax) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(hdnDiffernceInBrkrg)
        }
        else {
            var Allcharges = parseFloat(rate) - parseFloat(Brokerage) - parseFloat(Sebi) -
        parseFloat(stax) - parseFloat(trans) - parseFloat(stamp) - parseFloat(stt) - parseFloat(hdnDiffernceInBrkrg)
        }

        document.getElementById("<%=TxtRateInBrkrgAllCharges.ClientID %>").value = parseFloat(Allcharges).toFixed(6);
        var AllchargTot = parseFloat(Allcharges) * parseInt(NoOfShares);

        document.getElementById("<%=txtTotal.ClientID %>").value = parseFloat(AllchargTot).toFixed(4);

        return false;
    }
</script>

<script type="text/javascript">

    function StampChargeCalculation(sender, args) {

        var Sebi = document.getElementById("<%= txtSebiTurnOver.ClientID %>").value;
        var hdnstax = document.getElementById('ctrl_EquityManualSingleTransaction_hdnstax').value;
        if (hdnstax == "") {
            hdnstax = 0;
        }
        var trans = document.getElementById("<%= TxtTrxnChrgs.ClientID %>").value;
        var stamp = document.getElementById("<%= TxtStampCharges.ClientID %>").value;
        var stt = document.getElementById("<%= txtSTT.ClientID %>").value
        var trtype = document.getElementById('<%=ddlTransactionType.ClientID %>').value;
        var hdnDiffernceInBrkrg = document.getElementById("<%= TxtDiffInBrkrg.ClientID %>").value;
        var rate = document.getElementById("<%= txtRate.ClientID %>").value;

        var NoOfShares = document.getElementById("<%= txtNumShares.ClientID %>").value;


        var Brokerage = document.getElementById("<%=txtBrokerage.ClientID %>").value;

        var a = parseFloat(Brokerage) + parseFloat(Sebi) + parseFloat(trans) + parseFloat(stamp);
        var stax = parseFloat(parseFloat(hdnstax) * parseFloat(a));
        var Total = parseFloat(stamp) * parseInt(NoOfShares);
        document.getElementById("<%=txtStampChargeAmt.ClientID %>").value = parseFloat(Total).toFixed(6);


        document.getElementById("<%=txtTax.ClientID %>").value = parseFloat(stax).toFixed(6);
        var Totstax = parseFloat(stax) * parseInt(NoOfShares);
        document.getElementById("<%=txtServiceAmt.ClientID %>").value = parseFloat(Totstax).toFixed(4);

        var OtherCharge = parseFloat(Sebi) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(stax);
        var TotOthercharge = parseFloat(OtherCharge) * parseFloat(NoOfShares);

        document.getElementById("<%=txt_OtherCharges.ClientID %>").value = parseFloat(OtherCharge).toFixed(6);
        document.getElementById("<%=TxtOtherChargeAmt.ClientID %>").value = parseFloat(TotOthercharge).toFixed(4);

        if (hdnDiffernceInBrkrg == "") {
            hdnDiffernceInBrkrg = 0;
        }
        if (trtype != 2) {
            var Allcharges = parseFloat(rate) + parseFloat(Brokerage) + parseFloat(Sebi) +
        parseFloat(stax) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(hdnDiffernceInBrkrg)
        }
        else {
            var Allcharges = parseFloat(rate) - parseFloat(Brokerage) - parseFloat(Sebi) -
        parseFloat(stax) - parseFloat(trans) - parseFloat(stamp) - parseFloat(stt) - parseFloat(hdnDiffernceInBrkrg)
        }

        document.getElementById("<%=TxtRateInBrkrgAllCharges.ClientID %>").value = parseFloat(Allcharges).toFixed(6);
        var AllchargTot = parseFloat(Allcharges) * parseInt(NoOfShares);

        document.getElementById("<%=txtTotal.ClientID %>").value = parseFloat(AllchargTot).toFixed(4);

        return false;
    }
</script>

<script type="text/javascript">

    function StaxCalculation(sender, args) {


        var Sebi = document.getElementById("<%= txtSebiTurnOver.ClientID %>").value;
        var hdnstax = document.getElementById('ctrl_EquityManualSingleTransaction_hdnstax').value;
        if (hdnstax == "") {
            hdnstax = 0;
        }
        var trans = document.getElementById("<%= TxtTrxnChrgs.ClientID %>").value;
        var stamp = document.getElementById("<%= TxtStampCharges.ClientID %>").value;
        var stt = document.getElementById("<%= txtSTT.ClientID %>").value
        var trtype = document.getElementById('<%=ddlTransactionType.ClientID %>').value;
        var hdnDiffernceInBrkrg = document.getElementById("<%= TxtDiffInBrkrg.ClientID %>").value;
        var rate = document.getElementById("<%= txtRate.ClientID %>").value;
        var stax = document.getElementById("<%=txtTax.ClientID %>").value

        var NoOfShares = document.getElementById("<%= txtNumShares.ClientID %>").value;


        var Brokerage = document.getElementById("<%=txtBrokerage.ClientID %>").value;


        var Total = parseFloat(stax) * parseInt(NoOfShares);
        document.getElementById("<%=txtServiceAmt.ClientID %>").value = parseFloat(Total).toFixed(6);

        var OtherCharge = parseFloat(Sebi) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(stax);
        var TotOthercharge = parseFloat(OtherCharge) * parseFloat(NoOfShares);

        document.getElementById("<%=txt_OtherCharges.ClientID %>").value = parseFloat(OtherCharge).toFixed(6);
        document.getElementById("<%=TxtOtherChargeAmt.ClientID %>").value = parseFloat(TotOthercharge).toFixed(4);

        if (hdnDiffernceInBrkrg == "") {
            hdnDiffernceInBrkrg = 0;
        }
        if (trtype != 2) {
            var Allcharges = parseFloat(rate) + parseFloat(Brokerage) + parseFloat(Sebi) +
        parseFloat(stax) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(hdnDiffernceInBrkrg)
        }
        else {
            var Allcharges = parseFloat(rate) - parseFloat(Brokerage) - parseFloat(Sebi) -
        parseFloat(stax) - parseFloat(trans) - parseFloat(stamp) - parseFloat(stt) - parseFloat(hdnDiffernceInBrkrg)
        }

        document.getElementById("<%=TxtRateInBrkrgAllCharges.ClientID %>").value = parseFloat(Allcharges).toFixed(6);
        var AllchargTot = parseFloat(Allcharges) * parseInt(NoOfShares);

        document.getElementById("<%=txtTotal.ClientID %>").value = parseFloat(AllchargTot).toFixed(4);

        return false;

    }
</script>

<script type="text/javascript">

    function SttCalculation(sender, args) {

        var Sebi = document.getElementById("<%= txtSebiTurnOver.ClientID %>").value;

        var trans = document.getElementById("<%= TxtTrxnChrgs.ClientID %>").value;
        var stamp = document.getElementById("<%= TxtStampCharges.ClientID %>").value;
        var stt = document.getElementById("<%= txtSTT.ClientID %>").value
        var trtype = document.getElementById('<%=ddlTransactionType.ClientID %>').value;
        var hdnDiffernceInBrkrg = document.getElementById("<%= TxtDiffInBrkrg.ClientID %>").value;
        var rate = document.getElementById("<%= txtRate.ClientID %>").value;
        var stax = document.getElementById("<%=txtTax.ClientID %>").value

        var NoOfShares = document.getElementById("<%= txtNumShares.ClientID %>").value;


        var Brokerage = document.getElementById("<%=txtBrokerage.ClientID %>").value;


        var Total = parseFloat(stt) * parseInt(NoOfShares);
        document.getElementById("<%=txtSttAmt.ClientID %>").value = parseFloat(Total).toFixed(6);

        var OtherCharge = parseFloat(Sebi) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(stax);
        var TotOthercharge = parseFloat(OtherCharge) * parseFloat(NoOfShares);

        document.getElementById("<%=txt_OtherCharges.ClientID %>").value = parseFloat(OtherCharge).toFixed(6);
        document.getElementById("<%=TxtOtherChargeAmt.ClientID %>").value = parseFloat(TotOthercharge).toFixed(4);

        if (hdnDiffernceInBrkrg == "") {
            hdnDiffernceInBrkrg = 0;
        }
        if (trtype != 2) {
            var Allcharges = parseFloat(rate) + parseFloat(Brokerage) + parseFloat(Sebi) +
        parseFloat(stax) + parseFloat(trans) + parseFloat(stamp) + parseFloat(stt) + parseFloat(hdnDiffernceInBrkrg)
        }
        else {
            var Allcharges = parseFloat(rate) - parseFloat(Brokerage) - parseFloat(Sebi) -
        parseFloat(stax) - parseFloat(trans) - parseFloat(stamp) - parseFloat(stt) - parseFloat(hdnDiffernceInBrkrg)
        }

        document.getElementById("<%=TxtRateInBrkrgAllCharges.ClientID %>").value = parseFloat(Allcharges).toFixed(6);
        var AllchargTot = parseFloat(Allcharges) * parseInt(NoOfShares);

        document.getElementById("<%=txtTotal.ClientID %>").value = parseFloat(AllchargTot).toFixed(4);

        return false;
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td width="100%" colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" style="padding-top: 2px;" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Add Equity Transaction
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click"
                CausesValidation="false" CommandName="EditClick" Visible="False">Edit</asp:LinkButton>
            <asp:LinkButton ID="lnkBack" runat="server" CommandName="BackClick" CssClass="LinkButtons"
                CausesValidation="false" OnClick="lnkBack_Click" Visible="False">Back</asp:LinkButton>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Transaction Added Successfully
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgUpdatedStatus" runat="server" class="success-msg" align="center" visible="false">
                Transaction Updated Successfully
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="upnlEQTran" runat="server">
    <ContentTemplate>
        <table width="100%" class="TableBackground">
            <tr>
                <td style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Account Details
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%" class="TableBackground">
            </tr>
            </tr>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:CustomValidator ID="cvCheckForManageOrUnManage" runat="server" ValidationGroup="EQ"
                        Display="Dynamic" ClientValidationFunction="ChkForMainPortFolio" CssClass="revPCG"
                        ErrorMessage="CustomValidator">Permisssion denied for Manage Portfolio !!</asp:CustomValidator>
                </td>
                <div id="DivManaged" visible="false" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Lbl_Managedby" runat="server" CssClass="FieldName" Text="Managed By :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddl_Managedby" runat="server" Width="154px" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </div>
                <td>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Lbl_DematAccnt" runat="server" Text="Demat Account:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="Ddl_dematAcc" runat="server" CssClass="cmbField" AutoPostBack="True"
                        Width="150px" OnSelectedIndexChanged="ddlDematAcc_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:ImageButton ID="img_adddeamt" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                        AlternateText="Add" runat="server" ToolTip="Click here to Add Demat Account Number"
                        OnClientClick="return openpopupAddDematAccNum()" Height="15px" Width="15px" Style="margin-left: 0px">
                    </asp:ImageButton>
                    <asp:ImageButton ID="ImageButton3" ImageUrl="../Images/reprocess.png" AlternateText="Refresh"
                        runat="server" ToolTip="Refresh" OnClick="BtnDmatRefresh_Click" Height="15px"
                        Width="15px" Style="margin-left: 0px"></asp:ImageButton>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label4" runat="server" Text="Trade Account:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 190px">
                    <asp:DropDownList ID="ddlTradeAcc" runat="server" CssClass="cmbField" AutoPostBack="True"
                        Width="154px" OnSelectedIndexChanged="ddlTradeAcc_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="reqField" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a Tade Number"
                        InitialValue="0" ControlToValidate="ddlTradeAcc" ValidationGroup="EQ" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:ImageButton ID="btnImgAddtrdacc" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                        AlternateText="Add" runat="server" ToolTip="Click here to Add Trade Account Number"
                        OnClientClick="return openpopupAddTradeAccNum()" Height="15px" Width="15px" OnClick="Addtrdacc_Click">
                    </asp:ImageButton>
                    <asp:ImageButton ID="ImageButton4" ImageUrl="../Images/reprocess.png" AlternateText="Refresh"
                        runat="server" ToolTip="Refresh" OnClick="BtnTradeRefresh_Click" Height="15px"
                        Width="15px" Style="margin-left: 0px"></asp:ImageButton>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="LblBillNo" runat="server" CssClass="FieldName" 
                        Text="Bill Number :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="TxtBillNo" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="LblSettlmntNo" runat="server" CssClass="FieldName" 
                        Text="Settlement Number:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="TxtSettlemntNo" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            <tr>
                <td class="leftField" id="tdLblSettlmntDate" runat="server" visible="true">
                    <asp:Label ID="LblSettlmntDate" runat="server" Text="Settlement Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="tdtxtSettlmntDate" runat="server" visible="true">
                    <telerik:RadDatePicker ID="txtSettlmntDate" runat="server" CssClass="txtField" Culture="English (United States)"
                        AutoPostBack="true" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                        MinDate="1900-01-01" OnSelectedDateChanged="txtSettlmntDate_SelectedDateChanged">
                        <Calendar ID="calender1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblBroker" runat="server" CssClass="FieldName" Text="Broker:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtBroker" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Transaction Details
                    </div>
                </td>
            </tr>
                <div id="DivType" runat="server">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label5" runat="Server" Text="Purpose:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddl_type" runat="server" Width="150px" CssClass="cmbField">
                            <asp:ListItem Value="2" Text="Select">Select</asp:ListItem>
                            <asp:ListItem Text="Core Investment" Value="1">Core Investment</asp:ListItem>
                            <asp:ListItem Text=">Portfolio Investment" Value="0">Portfolio Investment</asp:ListItem>
                        </asp:DropDownList>
                      
                    </td>
            </div>
            <tr id="trtxtScrip" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label2" runat="server" Text="Scrip:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtScrip" runat="server" CssClass="txtField" AutoPostBack="true"
                        AutoCompleteType="Disabled" OnDataBinding="txtScrip_DataBinding"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoCompleteExtender" TargetControlID="txtScrip"
                        ServiceMethod="GetScripList" ServicePath="AutoComplete.asmx" MinimumPrefixLength="1"
                        OnDataBinding="txtScrip_DataBinding" EnableCaching="false" CompletionSetCount="5"
                        CompletionInterval="1000" OnClientItemSelected="GetSchemeCode" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                        CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem" />
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:HiddenField ID="txtScripCode" runat="server" OnValueChanged="txtScripCode_ValueChanged" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtScrip"
                        ErrorMessage="Enter few characters of Scrip Particular" CssClass="rfvPCG" runat="server"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                </td>
                <td colspan="2" align="center">
                    <asp:Label ID="lblScripName" runat="server" Text="Scrip Particulars:" CssClass="FieldName"></asp:Label>
                    <asp:Label ID="lblticker" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr id="trextype" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label8" runat="server" Text="MarketType:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:RadioButton ID="rbtnDomestic" AutoPostBack="true" runat="server" CssClass="FieldName"
                        Text="Domestic" GroupName="ExchangeType" Checked="true" OnCheckedChanged="rbtnDomestic_CheckedChanged" />
                  
                    <asp:RadioButton ID="rbtnInternational" runat="server" AutoPostBack="true" CssClass="FieldName"
                        Text="International" GroupName="ExchangeType" OnCheckedChanged="rbtnInternational_CheckedChanged" />
                </td>
                <td class="leftField">
                </td>
                <td class="rightField">
                    <asp:Label ID="lblDollarPrice" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr id="trFxRate" runat="server">
                <td class="leftField" runat="server" id="tdFxRate" visible="true">
                    <asp:Label ID="Label9" runat="server" Text="FX Rate" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" runat="server" id="tdFxRateDetails" visible="true">
                    <asp:DropDownList ID="ddlInternationalCurrency" runat="server" CssClass="cmbField"
                        Width="40%" AutoPostBack="true">
                        <asp:ListItem>USD</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtInternationalRates" runat="server" AutoPostBack="true" Width="50px"
                        CssClass="txtField"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtInternationalRates"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$">
                     </asp:RegularExpressionValidator>    
                </td>
            </tr>
            <tr id="trexchng" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblExchange" runat="server" Text="Exchange :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlExchange" runat="server" CssClass="cmbField" Width="40%"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlExchange_OnselectedChange">
                    </asp:DropDownList>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label3" runat="server" Text="Transaction Mode :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlTransactionMode" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlTrMode_SelectedIndexChanged">
                        <asp:ListItem Value="Select">Select</asp:ListItem>
                        <asp:ListItem Value="1">Speculative</asp:ListItem>
                        <asp:ListItem Value="0">Delivery</asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator Display="Dynamic" ID="CompareValidator3" runat="server" ControlToValidate="ddlTransactionMode"
                        CssClass="rfvPCG" ErrorMessage="Please select a Transaction Mode" Operator="NotEqual"
                        ValueToCompare="Select" ValidationGroup="EQ"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trTransactionType" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblTranType" runat="server" Text="Transaction type :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="cmbField" AutoPostBack="True"
                        Width="40%" EnableViewState="true" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator Display="Dynamic" ID="CompareValidator1" runat="server" ControlToValidate="ddlTransactionType"
                        CssClass="rfvPCG" ErrorMessage="Please select a Transaction Type" Operator="NotEqual"
                        ValueToCompare="Select" ValidationGroup="EQ"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <div id="divtradedate" runat="server">
                        <asp:Label ID="lblTrade" runat="server" CssClass="FieldName" Text="Trade Date :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTradeDate" runat="server" AutoPostBack="true" CssClass="txtField"
                        OnTextChanged="txtTradeDate_TextChanged"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                        OnClientDateSelectionChanged="checkDate" TargetControlID="txtTradeDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                        TargetControlID="txtTradeDate" WatermarkText="dd/mm/yyyy">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTradeDate"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please select a Trade Date"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
                <div id="DivDividend" runat="server" visible="false">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblbankrefno" runat="server" CssClass="FieldName" Text="Bank Reference No: "></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtbankrefno" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trdivhistory" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Lbl_divhistry" runat="server" CssClass="FieldName" Text="Past Dividend Declared History: "></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddl_divhistory" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddl_divhistory_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ImageButton ID="btnAdddividend" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                            AlternateText="Add" runat="server" ToolTip="Click here to Add Dividend" OnClick="Adddividend_click"
                            Height="15px" Width="15px" Style="margin-left: 0px"></asp:ImageButton>
                      
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Lbl_NoofShares" runat="server" CssClass="FieldName" Text="No of Shares ( Eligible for Dividend): "></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="Txt_NoOfSharesForDiv" runat="server" CssClass="txtField"></asp:TextBox>
                        <asp:Button ID="BtnCalculateDiv" runat="server" Text="Cal.Dividend" CssClass="PCGMediumButton"
                            OnClick="BtnCalculateDiv_Click" Visible="false" />
                    </td>
                </tr>
                <tr id="trdivamt" runat="server" visible="false">
                    <td class="leftField">
                        <asp:Label ID="Lbl_DivAmount" runat="server" CssClass="FieldName" Text="Dividend Amount: "></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="Txt_DivAmount" runat="server" CssClass="txtField"></asp:TextBox>
                         <span id="Span7" class="spnRequiredField">*</span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_DivAmount"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please enter Dividend Amount"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trdivtype" runat="server" visible="false">
                    <td class="leftField">
                        <asp:Label ID="Lbl_DividenStatus" runat="server" CssClass="FieldName" Text="Dividend Status"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddl_DivStatus" runat="server" CssClass="cmbField">
                            <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                            <asp:ListItem Value="true" Text="Recieved">Recieved</asp:ListItem>
                            <asp:ListItem Value="false" Text="Recievable">Recievable</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </div>
  
            <tr id="trRate" runat="server">
                <td align="right">
                    <asp:Label ID="lblPrice" runat="server" CssClass="FieldName" Text="Closing Price :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="txtField"></asp:TextBox>
              
                    <asp:Button ID="btnUsePrice" runat="server" CssClass="PCGButton" OnClick="btnUsePrice_Click"
                        Text="Use price" />
                </td>
                <td class="leftField">
                    <asp:Label ID="lblRate" runat="server" CssClass="FieldName" Text="Rate (Rs) :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtRate" runat="server" AutoPostBack="true" CssClass="txtField"
                        MaxLength="18" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRate"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please enter a Rate"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtRate"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trqnty" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblNumShares" runat="server" CssClass="FieldName" Text="Quantity :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtNumShares" runat="server" AutoPostBack="true" CssClass="txtField"
                        MaxLength="18" OnTextChanged="txtNumShares_TextChanged"></asp:TextBox>
                    <span id="Span9" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNumShares"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please enter the number of shares"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNumShares"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblGrossConsideration" runat="server" CssClass="FieldName" Text="Gross Consideration :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtGrossConsideration" runat="server" AutoPostBack="true" CssClass="txtField"
                        MaxLength="18"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtGrossConsideration"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format." ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trBroker" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblBrokerage" runat="server" CssClass="FieldName" MaxLength="18" Text="Brokerage (Per Unit) :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtBrokerage" runat="server" CssClass="txtField" onchange="return BrokerageCalculation();"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBrokerage"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format." ValidationExpression="^\d*(\.(\d{0,7}))?$"></asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblbrokerageamt" runat="server" CssClass="FieldName" Text="Brokerage Amount :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtbrokerageamt" runat="server" AutoPostBack="true" CssClass="txtField"
                        MaxLength="18" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trbrokerage" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Rate Inclusive of Brokerage :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtRateIncBrokerage" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRateIncBrokerage"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please enter the rate inclusive of brokerage"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtRateIncBrokerage"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Enter a numeric value"
                        Operator="DataTypeCheck" Type="Double" ValidationGroup="EQ"></asp:CompareValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtRateIncBrokerage"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                        </asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="LblTradetotalIncBrkrg" runat="server" CssClass="FieldName" Text="Gross Consideration(With Brokerage):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="TxtTradetotalIncBrkrg" runat="server" CssClass="txtField"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtTradetotalIncBrkrg"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format." ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trsebi" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblSebiTurnOver" runat="server" CssClass="FieldName" MaxLength="18"
                        Text="Sebi TurnOver Fee(per unit): "></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSebiTurnOver" runat="server" CssClass="txtField" onchange="return SebiTurnOverCalculation();"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtSebiTurnOver"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                        </asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblSebiTurnOverAmount" runat="server" CssClass="FieldName" MaxLength="18"
                        Text="Sebi TurnOver Fee Amount: "></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSebiTurnOverAmount" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trtrxn" runat="server">
                <td class="leftField">
                    <asp:Label ID="LblTnxnChrgs" runat="server" CssClass="FieldName" Text="Transaction Charges(per unit): "></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="TxtTrxnChrgs" runat="server" CssClass="txtField" onchange="return TrxnChargeCalculation();"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TxtTrxnChrgs"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                    </asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblTrxnAmount" runat="server" CssClass="FieldName" Text="Transaction Charge Amount: "></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTrxnAmount" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trStampCharg" runat="server">
                <td class="leftField">
                    <asp:Label ID="LblStampCharges" runat="server" CssClass="FieldName" Text="Stamp Charges(per unit): "></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="TxtStampCharges" runat="server" CssClass="txtField" onchange="return StampChargeCalculation();"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="TxtStampCharges"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                    </asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblStampChargeAmt" runat="server" CssClass="FieldName" Text="Stamp Charge Amount: "></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtStampChargeAmt" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trService" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblServiceTax" runat="server" CssClass="FieldName" Text="Service Tax + Education Cess(per unit) :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTax" runat="server" CssClass="txtField" onchange="return StaxCalculation();"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtTax"
                        Display="Dynamic" ErrorMessage="&lt;br /&gt;Enter a numeric value" Operator="DataTypeCheck"
                        Type="Double" ValidationGroup="EQ"></asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtTax"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                    </asp:RegularExpressionValidator>    
                </td>
                <td class="leftField">
                    <asp:Label ID="lblServiceAmt" runat="server" CssClass="FieldName" Text="Service Tax And Education Cess Amount"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtServiceAmt" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trSTT" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblSTT" runat="server" CssClass="FieldName" Text="STT(per unit): "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField" onchange="return SttCalculation();"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtSTT"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                    </asp:RegularExpressionValidator>  
                </td>
                <td class="leftField">
                    <asp:Label ID="lblSttAmt" runat="server" CssClass="FieldName" Text="STT Amount: "></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSttAmt" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trothercharge" runat="server" visible="false">
                <td class="leftField">
                    <asp:Label ID="lblOthercharge" runat="server" Text="Other Charges(per unit): " CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txt_OtherCharges" runat="server"  CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txt_OtherCharges"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                    </asp:RegularExpressionValidator>  
                </td>
                <td class="leftField">
                    <asp:Label ID="lblOtherChargeAmt" runat="server" Text="Other Charge Amount " CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="TxtOtherChargeAmt" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                    
                </td>
            </tr>
            <tr id="trTotal" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Additional Adjustment :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="TxtDiffInBrkrg" runat="server" CssClass="txtField" AutoPostBack="true"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="TxtDiffInBrkrg"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                    </asp:RegularExpressionValidator> 
                </td>
                <td class="leftField">
                    <asp:Label ID="lblAdditionalAdjAmt" runat="server" CssClass="FieldName" Text="Additional Adjustment Amount :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAdditionalAdjAmt" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="trnet" runat="server">
                <td class="leftField">
                    <asp:Label ID="LblRateInBrkrgAllCharges" runat="server" CssClass="FieldName" Text="Net Rate :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="TxtRateInBrkrgAllCharges" runat="server" CssClass="txtField">
                    </asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="TxtRateInBrkrgAllCharges"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                    </asp:RegularExpressionValidator> 
                </td>
                <td class="leftField">
                    <asp:Label ID="lblTotal" runat="server" CssClass="FieldName" Text="Net Consideration of all charges :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span16" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTotal"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please enter the total"
                        InitialValue="" ValidationGroup="EQ">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtTotal"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Enter a numeric value"
                        Operator="DataTypeCheck" Type="Double" ValidationGroup="EQ"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDematAndOtherCharg" runat="server" visible="false">
                <td class="leftField">
                    <asp:Label ID="Lbl_DematCharges" runat="server" CssClass="FieldName" Text="Demat Charge :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="Txt_DematCharge" runat="server" CssClass="txtField"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="Txt_DematCharge"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,6}))?$">
                    </asp:RegularExpressionValidator> 
                </td>
                <td class="leftField">
                    <asp:Label ID="lblDematChargesAmt" runat="server" CssClass="FieldName" Text="Demat Charges Amount:"
                        Visible="false"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDematChargesAmt" runat="server" CssClass="txtField" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr ID="trRemark" runat="server" visible="false">
                <td class="leftField">
                    <asp:Label ID="lblRemark" runat="server" CssClass="FieldName" Text="Remark :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                </td>
                </tr>
        </table>
        <asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
        <asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
        <asp:HiddenField ID="hdnScripcode" runat="server" />
        <asp:HiddenField ID="hdnSebiCharge" runat="server" />
        <asp:HiddenField ID="hdnTrxnCharge" runat="server" />
        <asp:HiddenField ID="hdnStampCharge" runat="server" />
        <asp:HiddenField ID="hdnStt" runat="server" />
        <asp:HiddenField ID="hdnServiceTax" runat="server" />
        <asp:HiddenField ID="hdnOtherCharge" runat="server" />
        <asp:HiddenField ID="hdnstax" runat="server" />
        <asp:HiddenField ID="hdnAccountId" runat="server" />
        <telerik:RadWindow ID="radAplicationPopUp" runat="server" VisibleOnPageLoad="false"
            Height="30%" Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
            Behaviors="close,Move" Title="Add Dividend">
            <ContentTemplate>
                <div style="padding: 20px">
                    <table width="100%" class="TableBackground">
                        <tr>
                            <td class="leftField" align="right">
                                <asp:Label ID="lbl_divDeclrdDt" runat="server" CssClass="FieldName" Text="Dividend Declared Date:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <telerik:RadDatePicker ID="txtFromDate" runat="server" CssClass="txtField" Culture="English (United States)"
                                    AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                                    MinDate="1900-01-01">
                                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" align="right">
                                <asp:Label ID="Lbl_DivType" runat="server" CssClass="FieldName" Text="Dividend Percentage:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txt_Percentage" runat="server" CssClass="txtField"></asp:TextBox>
                                <span id="Span3" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Percentage"
                                    CssClass="rfvPCG" Display="Dynamic" ValidationGroup="Submit" ErrorMessage="&lt;br /&gt;Please enter a Rate"
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_Percentage"
                                    CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" align="right">
                                <asp:Label ID="Lbl_FaceValue" runat="server" CssClass="FieldName" Text="Face Value:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txt_FaceValue" runat="server" CssClass="txtField"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_FaceValue"
                                    CssClass="rfvPCG" Display="Dynamic" ValidationGroup="Submit" ErrorMessage="&lt;br /&gt;Please enter a FaceValue"
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="PCGButton" Text="Submit" ValidationGroup="Submit"
                                    OnClick="btnSubmitDiv_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </ContentTemplate>
</asp:UpdatePanel>
<table width="100%" class="TableBackground">
    <tr class="SubmitCell">
        <td>
        </td>
        <td align="right">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EquityManualSingleTransaction_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EquityManualSingleTransaction_btnSubmit', 'S');"
                OnClick="btnSubmit_Click" ValidationGroup="EQ" />
        </td>
        <td>
            <asp:Button ID="Bttn_SubmitAddmore" runat="server" Text="Submit & Add More" CssClass="PCGMediumButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EquityManualSingleTransaction_Bttn_SubmitAddmore', 'M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EquityManualSingleTransaction_Bttn_SubmitAddmore', 'M');"
                OnClick="btnSubmit_AddMore_Click" ValidationGroup="EQ" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" OnClick="btnupdate_Click"
                ValidationGroup="EQ" Visible="false" />
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lbl_note" runat="server" Text="Note:Other charges=Sebi TurnOver Fee+Transaction Charges+Stamp Charges"
                CssClass="FieldName"></asp:Label>
        </td>
    </tr>
</table>
