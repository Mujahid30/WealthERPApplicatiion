 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceivableSetup.ascx.cs" Inherits="WealthERP.Receivable.ReceivableSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 9%;
        text-align: right;
    }
    .rightData
    {
        width: 16%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    Commission receivable Structure setup
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%">
            <%--***********************************************Commission receivable Structure setup********************************--%>
            <tr id="trStepOneHeading" runat="server">
                <td class="tdSectionHeading" colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divSectionHeadingNumber fltlftStep">
                            1
                        </div>
                        <div class="fltlft">
                            &nbsp;
                            <asp:Label ID="Label2" runat="server" Text="Comission Structure"></asp:Label>
                        </div>
                        <div class="divCollapseImage">
                            <img id="imgCEStepOne" src="../Images/Section-Expand.png" alt="Collapse/Expand" class="imgCollapse" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblStatusStage2" runat="server" Text="Pick Product:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="cmbField">
                        <asp:ListItem Value="MF" Text="Mutual Fund" Selected="True">
                        </asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select Product Type" Display="Dynamic" ControlToValidate="ddlProductType"
                        InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblIssuer" runat="server" Text="Issuer :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select Product Type" Display="Dynamic" ControlToValidate="ddlIssuer"
                        InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblCommissionApplicableLevel" runat="server" Text="Level:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlCommissionApplicableLevel" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblCommissionType" runat="server" Text="Commission Type:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlCommissionType" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblValidityFrom" runat="server" Text="Validity From :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtValidityFrom" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtValidityFrom"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtValidityFrom"
                        WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtValidityFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValidationGroup="vgBtnSubmitTemp" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtValidityFrom"
                        ErrorMessage="<br />Please enter a validity from Date" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblValidityTo" runat="server" Text="To:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtValidityTo" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtValidityTo"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtValidityTo"
                        WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CVReceivedDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtValidityTo" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValidationGroup="vgBtnSubmitTemp" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtValidityTo"
                        ErrorMessage="<br />Please enter a validity to Date" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:CheckBox ID="chkMoneytaryReward" Text="" runat="server" />
                </td>
                <td class="rightData">
                    <asp:Label ID="Label1" runat="server" Text="Is non moneytary reward" CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblStructureName" runat="server" Text="Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightDataTwoColumn" colspan="2">
                    <asp:TextBox ID="txtStructureName" runat="server" CssClass="txtField" Style="width: 80% !Important"></asp:TextBox>
                </td>
                <td colspan="3" class="rightDataThreeColumn">
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblValue" runat="server" Text="Value:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtValue" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblUnit" runat="server" Text="Unit:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblCalculatedOn" runat="server" Text="Calculated On:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlCommisionCalOn" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblApplyTaxes" runat="server" Text="Apply Taxes:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightDataThreeColumn" colspan="2">
                    <asp:CheckBoxList ID="chkListApplyTax" runat="server" CssClass="txtField" RepeatLayout="Flow"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                        <asp:ListItem Text="Service Tax" Value="ServiceTax"></asp:ListItem>
                        <asp:ListItem Text="TDS" Value="TDS"></asp:ListItem>
                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td class="rightData">
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblAUMFor" runat="server" Text="AUM for:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtAUMFor" runat="server" CssClass="txtField" Style="width: 70px !Important"></asp:TextBox>
                    <asp:DropDownList ID="ddlAUMFor" runat="server" CssClass="cmbField" Style="width: 70px !Important">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblAppCityGroup" runat="server" Text="App for city group:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlAppCityGroup" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblReceivableFrequency" runat="server" Text="Receivable Fre:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlReceivableFrequency" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:CheckBox ID="chkHasClawBackOption" Text="" runat="server" />
                </td>
                <td class="rightData">
                    <asp:Label ID="lblHasClawBackOption" runat="server" Text="Has claw back option:"
                        CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblNote" runat="server" Text="Note:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightDataFourColumn" colspan="4">
                    <asp:TextBox ID="txtNote" runat="server" CssClass="txtField" TextMode="MultiLine"
                        Width="75%"></asp:TextBox>
                </td>
                <td class="rightDataTwoColumn">
                </td>
            </tr>
            <tr id="trStepTwoHeading" runat="server">
                <td class="tdSectionHeading" colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divSectionHeadingNumber fltlftStep">
                            2
                        </div>
                        <div class="fltlft" style="width: 200px;">
                            &nbsp;
                            <asp:Label ID="lblStage" runat="server" Text="Structure Details"></asp:Label>
                        </div>
                        <div class="divCollapseImage">
                            <img id="img1" src="../Images/Section-Expand.png" alt="Collapse/Expand" class="imgCollapse" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblMinInvestmentAmount" runat="server" Text="Min Investment Amount:"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtMinInvestmentAmount" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblMaxInvestmentAmount" runat="server" Text="Max Investment Amount:"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtMaxInvestmentAmount" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td colspan="2" class="rightDataFourColumn">
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblMinTenure" runat="server" Text="Min Tenure:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtMinTenure" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblMaxTenure" runat="server" Text="Max Tenure:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtMaxTenure" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td colspan="2" class="rightDataFourColumn">
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblMinInvestAge" runat="server" Text="Min Investment age (Trail):"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtMinInvestAge" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblMaxInvestAge" runat="server" Text="Max Investment age (Trail):"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtMaxInvestAge" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td colspan="2" class="rightDataFourColumn">
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblInvestorType" runat="server" Text="Investor type:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                      <asp:DropDownList ID="ddlInvestorType" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblTransactionType" runat="server" Text="Transaction type:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                   <asp:CheckBoxList ID="chkListTtansactionType" runat="server" CssClass="txtField" RepeatLayout="Flow"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Text="BUY" Value="BUY"></asp:ListItem>
                        <asp:ListItem Text="STP" Value="STP"></asp:ListItem>
                        <asp:ListItem Text="SELL" Value="SELL"></asp:ListItem>
                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
               <td class="leftLabel">
                    <asp:Label ID="lblMinNumberOfApplication" runat="server" Text="Min number of applications:"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtMinNumberOfApplication" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
