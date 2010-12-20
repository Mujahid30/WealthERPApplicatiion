<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioFixedIncomeEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioFixedIncomeEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" src="../Scripts/Calender/calendar.js"> </script>

<script type="text/javascript" src="../Scripts/Calender/lang/calendar-en.js"> </script>

<script type="text/javascript" src="../Scripts/Calender/calendar-setup.js"></script>
<script type="text/javascript">
    function SetCurrentValue() {
        if (document.getElementById('<%=txtDepositAmount.ClientID %>').value > 0)
            document.getElementById('<%=txtCurrentValue.ClientID %>').value = document.getElementById('<%=txtDepositAmount.ClientID %>').value;

    };
</script>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <td class="HeaderCell" colspan="4">
                    <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Fixed Income"></asp:Label>
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
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Instrument Category:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:Label ID="lblInstrumentCategory" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <%--Account Details--%>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblAccountDetails" runat="server" CssClass="HeaderTextSmall" Text="Account Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCertificateNumber" runat="server" CssClass="FieldName" Text="Certificate Number:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAccountId" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblAccountOpeningDate" runat="server" CssClass="FieldName" Text="Account Opening Date:"
                        Visible="false"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAccOpenDate" runat="server" CssClass="txtField" Visible="false"></asp:TextBox>
                    <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAccOpenDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                        TargetControlID="txtAccOpenDate" WatermarkText="mm/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span13" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvAccOpenDate" ControlToValidate="txtAccOpenDate"
                        ErrorMessage="Please select a Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvAccOpenDate" runat="server" ErrorMessage="The date format should be mm/dd/yyyy"
                        Type="Date" ControlToValidate="txtAccOpenDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr id="trAccountSource" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblAccountWith" runat="server" CssClass="FieldName" Text="Account With:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <%--<asp:Label ID="lblAccountSource" runat="server" CssClass="Field"></asp:Label>--%>
                    <asp:TextBox ID="txtAccountWith" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvAccountWith" ControlToValidate="txtAccountWith"
                        ErrorMessage="Please enter the Account With" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Mode Of Holding:"></asp:Label>
                </td>
                <td class="rightField">
                    <%--<asp:Label ID="lblModeOfHolding" runat="server" CssClass="Field"></asp:Label>--%>
                    <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Asset Particulars:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAssetParticulars" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span9" class="spnRequiredField">*<br />
                    </span>
                    <asp:RequiredFieldValidator ID="rfvAssetParticulars" ControlToValidate="txtAssetParticulars"
                        ErrorMessage="Please enter the Asset Particulars" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblAssetIssuer" runat="server" CssClass="FieldName" Text="Asset Issuer:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlDebtIssuerCode" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*<br />
                    </span>
                    <asp:CompareValidator ID="cvDebtIssuerCode" runat="server" ControlToValidate="ddlDebtIssuerCode"
                        ErrorMessage="Please select a Asset Issuer Code" Operator="NotEqual" ValueToCompare="Select a Asset Issuer"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <%--Deposit Details--%>
            <tr id="trDepositDetails" runat="server">
                <td colspan="4">
                    <asp:Label ID="lblDepositDetails" runat="server" CssClass="HeaderTextSmall" Text="Deposit Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trDepoDateMatDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblDepositDate" runat="server" CssClass="FieldName" Text="Deposit Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDepositDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" TargetControlID="txtDepositDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtDepositDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtDepositDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDepositDate" ControlToValidate="txtDepositDate"
                        ErrorMessage="Please select a Deposit Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDepositDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtDepositDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblMaturityDate" runat="server" CssClass="FieldName" Text="Maturity Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtMaturityDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtMaturityDate_CalendarExtender" runat="server" TargetControlID="txtMaturityDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtMaturityDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtMaturityDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvMaturityDate" ControlToValidate="txtMaturityDate"
                        ErrorMessage="Please select a Maturity Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvMaturityDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtMaturityDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trFaceValDebNum" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblFaceValue" runat="server" CssClass="FieldName" Text="Face Value:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtFaceValue" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvFaceValue" ControlToValidate="txtFaceValue" ErrorMessage="Please enter the Face Value"
                        Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvFaceValue" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtFaceValue" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblNoofDebentures" runat="server" CssClass="FieldName" Text="No. of Debentures:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtNoofDebentures" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvNoofDebentures" ControlToValidate="txtNoofDebentures"
                        ErrorMessage="Please enter the No Of Debentures" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvNoofDebentures" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtNoofDebentures" Operator="DataTypeCheck"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDeposit" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblDepositAmount" runat="server" CssClass="FieldName" Text="Deposit Amount:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtDepositAmount" runat="server" OnBlur="SetCurrentValue();" CssClass="txtField"></asp:TextBox>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDepositAmount" ControlToValidate="txtDepositAmount"
                        ErrorMessage="Please enter the Deposit Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDepositAmount" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtDepositAmount" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDepositDetailsSpace" runat="server">
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <%--DDB Starts Here--%>
            <tr id="trDDBDetails" runat="server">
                <td colspan="4">
                    <asp:Label ID="lblDDBDetails" runat="server" CssClass="HeaderTextSmall" Text="DDB Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trDDBIssuePurchaseDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblDDBIssueDate" runat="server" CssClass="FieldName" Text="Issue Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDDBIssueDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDDBIssueDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtDDBIssueDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtDDBIssueDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtDDBIssueDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span14" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDDBIssueDate" ControlToValidate="txtDDBIssueDate"
                        ErrorMessage="Please enter an Issue Date" Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblDDBPurchaseDate" runat="server" CssClass="FieldName" Text="Purchase Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDDBPurchaseDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDDBPurchaseDate_CalendarExtender" runat="server" TargetControlID="txtDDBPurchaseDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtDDBPurchaseDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtDDBPurchaseDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span15" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDDBPurchaseDate" ControlToValidate="txtDDBPurchaseDate"
                        ErrorMessage="Please select Purchase Date" Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDDBPurchaseDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtDDBPurchaseDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDDBPurchPriceMatDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblDDBPurchasePrice" runat="server" CssClass="FieldName" Text="Purchase Price:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDDBPurchasePrice" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span16" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDDBPurchasePrice" runat="server" ControlToValidate="txtDDBPurchasePrice"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the Purchase Price">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDDBPurchasePrice" runat="server" ControlToValidate="txtDDBPurchasePrice"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblDDBMaturityDate" runat="server" CssClass="FieldName" Text="Maturity Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDDBMaturityDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDDBMaturityDate_CalendarExtender" runat="server" TargetControlID="txtDDBMaturityDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtDDBMaturityDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtDDBMaturityDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDDBMaturityDate" ControlToValidate="txtDDBMaturityDate"
                        ErrorMessage="Please select a Maturity Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDDBMaturityDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtDDBMaturityDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDDBFaceValue" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblDDBFaceValueIssue" runat="server" CssClass="FieldName" Text="Issue Price:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDDBFaceValueIssue" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span17" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDDBFaceValueIssue" runat="server" ControlToValidate="txtDDBFaceValueIssue"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the Issue Price">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDDBFaceValueIssue" runat="server" ControlToValidate="txtDDBFaceValueIssue"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblDDBFaceValueMaturity" runat="server" CssClass="FieldName" Text="Maturity Price:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDDBFaceValueMat" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span18" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDDBFaceValueMat" runat="server" ControlToValidate="txtDDBFaceValueMat"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the Maturity Price">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDDBFaceValueMat" runat="server" ControlToValidate="txtDDBFaceValueMat"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDDBNoofDebentures" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblDDBNUmofDebentures" runat="server" CssClass="FieldName" Text="No of Debentures:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDDBNoofDebentures" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span19" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDDBNoofDebentures" runat="server" ControlToValidate="txtDDBNoofDebentures"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the No Of Debentures">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDDBNoofDebentures" runat="server" ControlToValidate="txtDDBNoofDebentures"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblDDBPurchaseCost" runat="server" CssClass="FieldName" Text="Purchase Cost:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDDBPurchaseCost" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span20" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDDBPurchaseCost" runat="server" ControlToValidate="txtDDBPurchaseCost"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the Purchase Cost">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDDBPurchaseCost" runat="server" ControlToValidate="txtDDBPurchaseCost"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDDBSpace" runat="server">
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <%--Purchase Details Start Here--%>
            <tr id="trPurchaseDetails" runat="server">
                <td colspan="4">
                    <asp:Label ID="lblPurchaseDetails" runat="server" CssClass="HeaderTextSmall" Text="Purchase Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trIssueDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblIssueDate" runat="server" CssClass="FieldName" Text="Issue Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtIssueDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--<cc1:CalendarExtender ID="txtIssueDate_CalendarExtender" runat="server" TargetControlID="txtIssueDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtIssueDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtIssueDate" WatermarkText="mm/dd/yy">
                    </cc1:TextBoxWatermarkExtender>--%>
                    <span id="Span22" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvIssueDate" ControlToValidate="txtIssueDate" ErrorMessage="Please enter an Issue Date"
                        Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvIssueDate" runat="server" ErrorMessage="Issue Date has to be less than or equal to 31"
                        Type="Integer" ControlToValidate="txtIssueDate" Operator="LessThanEqual" ValueToCompare="31"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblPurchaseDate" runat="server" CssClass="FieldName" Text="Purchase Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtPurchaseDate_CalendarExtender" runat="server" TargetControlID="txtPurchaseDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtPurchaseDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtPurchaseDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span23" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPurchaseDate" ControlToValidate="txtPurchaseDate"
                        ErrorMessage="Please select Purchase Date" Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPurchaseDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtPurchaseDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPurchPrice" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblPurchasePrice" runat="server" CssClass="FieldName" Text="Purchase Price:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPurPurchasePrice" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span24" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPurPurchasePrice" runat="server" ControlToValidate="txtPurPurchasePrice"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the Purchase Price">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPurPurchasePrice" runat="server" ControlToValidate="txtPurPurchasePrice"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPurMaturityDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblPurMaturityDate" runat="server" CssClass="FieldName" Text="Maturity Date:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPMaturityDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtPMaturityDate_CalendarExtender" runat="server" TargetControlID="txtPMaturityDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtPMaturityDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtPMaturityDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span25" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPMaturityDate" ControlToValidate="txtPMaturityDate"
                        ErrorMessage="Please select a Maturity Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPMaturityDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtPMaturityDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trFaceValue" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblPurFaceValue" runat="server" CssClass="FieldName" Text="Face Value:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurFaceValue" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span26" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPurFaceValue" runat="server" ControlToValidate="txtPurFaceValue"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the Face Value">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPurFaceValue" runat="server" ControlToValidate="txtPurFaceValue"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblPurNoofDebentures" runat="server" CssClass="FieldName" Text="No of Debentures:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurNoOfDebentures" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span27" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPurNoOfDebentures" runat="server" ControlToValidate="txtPurNoOfDebentures"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the No Of Debentures">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPurNoOfDebentures" runat="server" ControlToValidate="txtPurNoOfDebentures"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPurchCost" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblPurchaseCost" runat="server" CssClass="FieldName" Text="Purchase Cost:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPurPurchaseCost" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span28" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPurPurchaseCost" runat="server" ControlToValidate="txtPurPurchaseCost"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter the Purchase Cost">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPurPurchaseCost" runat="server" ControlToValidate="txtPurPurchaseCost"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please enter a numeric value"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPurchaseSpace" runat="server">
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <%--Deposit Schedule--%>
            <tr id="trDepositSchedule" runat="server">
                <td colspan="4">
                    <asp:Label ID="lblDepositSchedule" runat="server" CssClass="HeaderTextSmall" Text="Deposit Schedule"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trDepositAmt" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblSubsequentDepositAmt" runat="server" CssClass="FieldName" Text="Subsequent Deposit Amt:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSubsequentDepositAmt" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvSubsequentDepositAmt" ControlToValidate="txtSubsequentDepositAmt"
                        ErrorMessage="Please enter the Deposit Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvSubsequentDepositAmt" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSubsequentDepositAmt" Operator="DataTypeCheck"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblFrequencyOfDeposit" runat="server" CssClass="FieldName" Text="Frequency of Deposit:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlFrequencyOfDeposit" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span21" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="cvFrequencyOfDeposit" runat="server" ControlToValidate="ddlFrequencyOfDeposit"
                        ErrorMessage="Please select a Frequency" Operator="NotEqual" ValueToCompare="Select a Frequency"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDepositDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblSubsequentDepositDate" runat="server" CssClass="FieldName" Text="Date(If Applicable):"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtDBScheduleDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDBScheduleDate_CalendarExtender" runat="server" TargetControlID="txtDBScheduleDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtDBScheduleDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtDBScheduleDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDBScheduleDate" ControlToValidate="txtDBScheduleDate"
                        ErrorMessage="Please select a Date" Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDBScheduleDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtDBScheduleDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trDepositScheduleSpace" runat="server">
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <%--Interest Details--%>
            <tr id="trInterestDetails" runat="server">
                <td colspan="4">
                    <asp:Label ID="lblInterestDetails" runat="server" CssClass="HeaderTextSmall" Text="Interest Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trIntRateIntBasis" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblInterestRate" runat="server" CssClass="FieldName" Text="Interest Rate Applicable(%):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtInterstRate" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvInterstRate" ControlToValidate="txtInterstRate"
                        ErrorMessage="Please enter the Interest Rate" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvInterstRate" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtInterstRate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblInterestBasis" runat="server" CssClass="FieldName" Text="Interest Calc Basis:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlInterestBasis" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlInterestBasis_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trIntFreqCompound" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblCompundingFreq" runat="server" CssClass="FieldName" Text="Frequency Of Compounding:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:DropDownList ID="ddlCompoundInterestFreq" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trIntFreqIntAmt" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblInterestFreqCode" runat="server" CssClass="FieldName" Text="Interest Payout Frequency:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPayableFrequencyCode" runat="server" CssClass="cmbField"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlPayableFrequencyCode_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span29" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="cvPayableFrequencyCode" runat="server" ControlToValidate="ddlPayableFrequencyCode"
                        ErrorMessage="Please select a Frequency" Operator="NotEqual" ValueToCompare="Select a Frequency"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblInterestAmtCredited" runat="server" CssClass="FieldName" Text="Interest Amt Credited:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtInterestAmtCredited" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr id="trInterestDetailsSpace" runat="server">
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <%--Valuation--%>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label8" runat="server" CssClass="HeaderTextSmall" Text="Valuation"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" Text="Current Value:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblMaturityValue" runat="server" CssClass="FieldName" Text="Maturity Value:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtMaturityValue" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <%--Remarks--%>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
                </td>
                <td class="rightField" rowspan="2" colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtField" Height="69px"></asp:TextBox>
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
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioFixedIncomeEntry_btnSubmit');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioFixedIncomeEntry_btnSubmit');" />
                    <asp:Button ID="btnSaveChanges" runat="server" Text="Update" CssClass="PCGButton"
                        OnClick="btnSaveChanges_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioFixedIncomeEntry_btnSaveChanges');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioFixedIncomeEntry_btnSaveChanges');" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
