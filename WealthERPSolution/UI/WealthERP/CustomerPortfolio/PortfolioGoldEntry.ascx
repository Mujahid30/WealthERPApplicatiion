﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioGoldEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioGoldEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script type="text/javascript" src="../Scripts/Calender/calendar.js"> </script>

<script type="text/javascript" src="../Scripts/Calender/lang/calendar-en.js"> </script>

<script type="text/javascript" src="../Scripts/Calender/calendar-setup.js"></script>
<script language="javascript" type="text/javascript">
function showassocation() {

    alert('Please enter Quantity');
        
    }
</script>

<script type="text/javascript">
    function ChkForMainPortFolio(source, args) {

        var hdnIsCustomerLogin = document.getElementById('ctrl_PortfolioGoldEntry_hdnIsCustomerLogin').value;
        var hdnIsMainPortfolio = document.getElementById('ctrl_PortfolioGoldEntry_hdnIsMainPortfolio').value;

        if (hdnIsCustomerLogin == "Customer" && hdnIsMainPortfolio == "1") {

            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }

    }    
</script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upPnl" runat="server">
    <ContentTemplate>
        <table style="width: 100%">
            <tr>
                <td class="HeaderCell">
                    <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Add Gold"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                        OnClick="lnkBtnBack_Click" CausesValidation="false"></asp:LinkButton>
                    &nbsp; &nbsp; &nbsp;
                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click"
                        CausesValidation="false">Edit</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Select the Portfolio Name:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                      <br />
                                <asp:CustomValidator ID="cvCheckForManageOrUnManage" runat="server"  ValidationGroup="btnSubmit"
                                    Display="Dynamic" ClientValidationFunction="ChkForMainPortFolio" CssClass="revPCG"
                                    ErrorMessage="CustomValidator">Permisssion denied for Manage Portfolio !!</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="btn_categorycheck" AutoPostBack="true" CssClass="cmbField">
                    </asp:DropDownList>
                    <asp:Label ID="lblAssetCategory" runat="server" CssClass="Field"></asp:Label>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCategory"
                        ErrorMessage="Please select a Category" Operator="NotEqual" ValueToCompare="Select a Category"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblAssetParticulars" runat="server" CssClass="FieldName" Text="Asset Particulars:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--<span id="Span1" class="spnRequiredField">*</span>--%>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtName"
                        ErrorMessage="Please enter a Name" Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblPurchaseDetails" runat="server" CssClass="HeaderTextSmall" Text="Purchase Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPurchaseDate" runat="server" CssClass="FieldName" Text="Purchase Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="txtField" Width="110px" ></asp:TextBox>
                    <span id="Span23" class="spnRequiredField">*</span>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPurchaseDate"
                        ErrorMessage="Please enter the Purchase Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                         </asp:RequiredFieldValidator>
                    <cc1:CalendarExtender ID="txtPurchaseDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtPurchaseDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtPurchaseDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtPurchaseDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <%--<span id="Span2" class="spnRequiredField">*</span>--%>
                    <%-- <asp:RequiredFieldValidator ID="rfvPurchaseDate" ControlToValidate="txtPurchaseDate"
                        ErrorMessage="<br />Please select a Purchase Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtPurchaseDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:CompareValidator ID="cvPurchaseDate" runat="server" ErrorMessage="<br/>The purchase date should not be greater than current date."
                        Type="Date" ControlToValidate="txtPurchaseDate" CssClass="cvPCG" Operator="LessThanEqual"
                        ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblQuantity" runat="server" CssClass="FieldName" Text="Quantity(Grams):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtField" MaxLength="10" AutoPostBack="true"  OnTextChanged="btn_costUpdate"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtQuantity"
                        ErrorMessage="<br />Please enter a Quantity" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="<br />Please enter an Integer value"
                        Type="Integer" ControlToValidate="txtQuantity" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPurchasePrice" runat="server" CssClass="FieldName" Text="Purchase Rate per Unit(Rs):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurchasePrice" runat="server" CssClass="txtField" 
                        OnTextChanged="btn_costUpdate" AutoPostBack="true" Width="121px"></asp:TextBox>
                    <asp:DropDownList ID="ddlMeasureCode" runat="server" CssClass="cmbField" Width="50px" Visible ="false"  >
                    </asp:DropDownList>
                    <asp:Label ID="lblMeasureCode" runat="server" CssClass="FieldName" Text="Grams"></asp:Label>
              
                    <span id="Span4" class="spnRequiredField">*</span>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPurchasePrice"
                        ErrorMessage="<br />Please enter a Purchase Price" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Please enter a numeric value"
                        Type="Double" ControlToValidate="txtPurchasePrice" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                    <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlMeasureCode"
                        ErrorMessage="<br />Please select a Measure Code" Operator="NotEqual" ValueToCompare="Select a Measure Code"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
                
                
                    <asp:Button ID="btnUsePrice" runat="server" Text="Use Price" Visible ="false"  CssClass="PCGMediumButton"
                          OnClick="btnUsePrice_Click" />
                </td>
                <td class="leftField">
                    <asp:Label ID="lblPurchaseValue" runat="server" CssClass="FieldName" Text="Purchase Cost(Rs):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurchaseValue" runat="server" Enabled="false" CssClass="txtField"></asp:TextBox>
                    <span id="Span5" class="spnRequiredField">*</span>
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPurchaseValue"
                        ErrorMessage="<br />Please enter a Purchase Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br />Please enter a numeric value"
                        Type="Double" ControlToValidate="txtPurchaseValue" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblCurrentValuation" runat="server" CssClass="HeaderTextSmall" Text="Current Valuation"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCurrentPrice" runat="server" CssClass="FieldName" Text="Current Rate per Unit(Rs):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCurrentPrice" runat="server" AutoPostBack="true" OnTextChanged= "Current_PriceChange" CssClass="txtField"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Grams"></asp:Label>
                    <%--<span id="Span6" class="spnRequiredField">*</span>--%>
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPurchaseDate"
                        ErrorMessage="<br />Please select a Purchase Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br />Please enter a numeric value"
                        Type="Double" ControlToValidate="txtCurrentPrice" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                        <asp:Button ID="btnSellPrice" runat="server" Text="Use Price" Visible ="False"  CssClass="PCGMediumButton"
                          OnClick="btnUseSellPrice_Click" />
                </td>
                <td class="leftField">
                    <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" Text="Current Value(Rs):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCurrentValue" runat="server" Enabled="false" CssClass="txtField"></asp:TextBox>
                    <%-- <span id="Span7" class="spnRequiredField">*</span>--%>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPurchaseDate"
                        ErrorMessage="<br />Please select a Purchase Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="<br />Please enter a numeric value"
                        Type="Double" ControlToValidate="txtCurrentValue" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblR" runat="server" CssClass="HeaderTextSmall" Text="Sale Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPurchaseDate0" runat="server" CssClass="FieldName" Text="Sale Date:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtSaleDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtSaleDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtSaleDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSaleDate"
                        WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtSaleDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                       <%-- <asp:CompareValidator ID="cvAccopenDateCheckCurrent" runat="server" ErrorMessage="<br />Account Opening Date should not be more the current date"
                        Type="Date" ControlToValidate="txtSaleDate" ValueToCompare= '<%# DateTime.Today.ToString("dd/MM/yyyy") %>' Operator="LessThanEqual" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblSaleRate" runat="server" CssClass="FieldName" Text="Sale Rate per unit(Rs):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSaleRate" runat="server" AutoPostBack="true" OnTextChanged="Button_SaleRate" CssClass="txtField"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Grams"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSaleRate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                         <asp:Button ID="btnSaleCost" runat="server" Text="Use Price" Visible ="false"  CssClass="PCGMediumButton"
                          OnClick="btnUseSellCost_Click" />
                </td>
                <td class="leftField">
                    <asp:Label ID="lblSaleValue" runat="server" CssClass="FieldName" Text="Sale Value(Rs):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSaleValue" runat="server" Enabled="false" CssClass="txtField"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="<br />Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSaleValue" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
                </td>
                <td rowspan="2" class="rightField" colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="txtField"></asp:TextBox>
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
        </table>
        <table style="width: 100%;" class="TableBackground">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSaveChanges" runat="server" CssClass="PCGButton" Text="Update" ValidationGroup="btnSubmit"
                        OnClick="btnSaveChanges_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioGoldEntry_btnSubmit');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioGoldEntry_btnSubmit');" />
                    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="btnSubmit"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioGoldEntry_btnSubmit');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioGoldEntry_btnSubmit');" />
                </td>
            </tr>
        </table>
           <asp:HiddenField ID="hdnIsMainPortfolio" runat="server"/>
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
