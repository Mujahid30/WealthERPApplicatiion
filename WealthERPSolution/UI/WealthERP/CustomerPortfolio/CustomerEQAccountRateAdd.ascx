<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerEQAccountRateAdd.ascx.cs" Inherits="WealthERP.CustomerPortfolio.CustomerEQAccountRateAdd" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Add Rates
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click"
                CommandName="EditClick" Visible="False">Edit</asp:LinkButton>
            <asp:LinkButton ID="lnkBack" runat="server" CommandName="BackClick" CssClass="LinkButtons"
                OnClick="lnkBack_Click" Visible="False">Back</asp:LinkButton>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="leftField"  >
            <asp:Label ID="lblTradeAccNo" runat="server" CssClass="FieldName" Text="Trade Account No:"></asp:Label>
        </td>
        <td class="rightField" >
            <asp:DropDownList ID="ddlAccountNo" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAccountNo"
                ErrorMessage="Please select an Account" Operator="NotEqual" ValueToCompare="Select the Trade Number"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td >
        </td>
        
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="lblTransactionMode" runat="server" CssClass="FieldName" Text="Transaction Mode :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlTransactionMode" runat="server" CssClass="cmbField">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="1">Speculative</asp:ListItem>
                <asp:ListItem Value="0">Delivery</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="LblTrType" runat="server" CssClass="FieldName" Text="Transaction Type :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="Ddl_Type" AutoPostBack="true" runat="server" CssClass="cmbField">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="B">Buy</asp:ListItem>
                <asp:ListItem Value="S">Sell</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="lblRate" runat="server" CssClass="FieldName" Text="Rate :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="TxtRate" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Percentage should not be greater than 100"
                MaximumValue="100" MinimumValue="0" ControlToValidate="TxtRate" Type="Double"
                Display="Dynamic"></asp:RangeValidator>
                <asp:Label ID="Lblstaxapplicable" runat="server" Text="Is ServiceTax Applicable" CssClass="FieldName"></asp:Label>
                   <asp:CheckBox ID="Chkbrk" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Lbl_sebiturnoverfee" runat="server" Text="SEBI TurnOver Fee" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="Txt_SEBITrnfee" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Percentage should not be greater than 100"
                MaximumValue="100" MinimumValue="0" ControlToValidate="Txt_SEBITrnfee" Type="Double" Display="Dynamic"></asp:RangeValidator>
                   <asp:Label ID="Lbl_sebistax" runat="server" Text="Is ServiceTax Applicable" CssClass="FieldName"></asp:Label>
        <asp:CheckBox ID="chksebi" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Lbl_Transcharges" runat="server" Text="Transaction Charges" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="Txt_Transcharges" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="Percentage should not be greater than 100"
                MaximumValue="100" MinimumValue="0" ControlToValidate="Txt_Transcharges" Type="Double" Display="Dynamic"></asp:RangeValidator>
              <asp:Label ID="Lbl_trxnstax" runat="server" Text="Is ServiceTax Applicable" CssClass="FieldName"></asp:Label>
        <asp:CheckBox ID="ChkTrxn" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Lbl_stampcharges" runat="server" Text="Stamp Charges" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="Txt_stampcharges" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Percentage should not be greater than 100"
                MaximumValue="100" MinimumValue="0" ControlToValidate="Txt_stampcharges" Type="Double"
                Display="Dynamic"></asp:RangeValidator>
         <asp:Label ID="Lbl_stampstax" runat="server" Text="Is ServiceTax Applicable" CssClass="FieldName"></asp:Label>
        <asp:CheckBox ID="Chkstamp" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Lbl_STT" runat="server" Text="STT" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="Txt_STT" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator7" runat="server" ErrorMessage="Percentage should not be greater than 100"
                MaximumValue="100" MinimumValue="0" ControlToValidate="Txt_STT" Type="Double"></asp:RangeValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Lbl_ServiceTax" runat="server" Text="Service Tax" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="Txt_ServiceTax" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator8" runat="server" ErrorMessage="Percentage should not be greater than 100"
                MaximumValue="100" MinimumValue="0" ControlToValidate="Txt_ServiceTax" Type="Double"></asp:RangeValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="lblStartingDate" runat="server" CssClass="FieldName" Text="Start Date:"></asp:Label>
        </td>
        <td class="rightField">
            <telerik:RadDatePicker ID="txtstartdate" runat="server" CssClass="txtField" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtstartdate"
                ValidationGroup="submitgroup" InitialValue="" CssClass="cvPCG" ErrorMessage="please select the start date"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="lblEndDate" runat="server" CssClass="FieldName" Text="End Date:"></asp:Label>
        </td>
        <td class="rightField">
            <telerik:RadDatePicker ID="txtendDate" runat="server" CssClass="txtField" Culture="English (United States)"
                AutoPostBack="true" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01">
                <Calendar ID="calender1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="cvEndDate" runat="server" ValidationGroup="submitgroup"
                ControlToValidate="txtendDate" ControlToCompare="txtstartdate" Type="Date" Operator="GreaterThan"
                CssClass="cvPCG" ErrorMessage="End date should be after start date"></asp:CompareValidator>
        </td>
    </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 25%">
                &nbsp;
            </td>
            <td class="rightField">
                <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" ValidationGroup="submitgroup"
                    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerEQAccountAdd_btnSubmit', 'S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerEQAccountAdd_btnSubmit', 'S');"
                    Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnUpdate" runat="server" ValidationGroup="submitgroup" CssClass="PCGButton"
                    Text="Update" OnClick="btnUpdate_Click" Visible="False" />
            </td>
        </tr>
    </table>
