<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCashFlow.ascx.cs"
    Inherits="WealthERP.FP.CustomerCashFlow" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

<table width="100%">
    <tr>
        <td colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Cash Flow Recommendation
            </div>
        </td>
    </tr>
</table>
<tr>
    <td>
        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click"
            CommandName="EditClick" Visible="False">Edit</asp:LinkButton>
        <asp:LinkButton ID="lnkBack" runat="server" CommandName="BackClick" CssClass="LinkButtons"
            OnClick="lnkBack_Click" Visible="False">Back</asp:LinkButton>
    </td>
</tr>
<table width="100%">
    <tr>
        <td align="right" style="width: 15%;">
            <asp:Label ID="lblptype" runat="server" CssClass="FieldName" Text="Product:"></asp:Label>
        </td>
        <td style="width: 23.5%">
            <asp:DropDownList ID="ddlptype" runat="server" CssClass="cmbField" AutoPostBack="true"
                TabIndex="1" OnSelectedIndexChanged="ddlptype_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="tr1" visible="false">
        <td align="right" style="width: 15%;">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="scheme"></asp:Label>
        </td>
        <td style="width: 30px;">
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField" AutoPostBack="false">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="tr2" visible="false">
        <td align="right" style="width: 15%;">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Buy/Sell"></asp:Label>
        </td>
        <td style="width: 30px;">
            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="cmbField" AutoPostBack="false"
                OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 15%;">
            <asp:Label ID="lblpaytype" runat="server" CssClass="FieldName" Text="Payment Type:"></asp:Label>
        </td>
        <td style="width: 23.5%">
            <asp:DropDownList ID="ddlpaytyppe" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlpaytyppe_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Installment" Value="1"></asp:ListItem>
                <asp:ListItem Text="lump sum amt" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
         <td align="right">
            <asp:Label ID="lbltenure" runat="server" Text="tenure:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txttenure" runat="server" CssClass="txtField" TabIndex="28"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span><span id="Span2"></span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txttenure" ErrorMessage="Please enter the tenure"
                ValidationGroup="Vg" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtStartDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                AutoPostBack="false" TabIndex="10">
                <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span8" class="spnRequiredField">*</span><span id="Span9"></span>
            <asp:RequiredFieldValidator ID="rfvStartDate" ControlToValidate="txtStartDate" ErrorMessage="Please enter the StartDate"
                ValidationGroup="Vg" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtEndDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                AutoPostBack="false" TabIndex="10">
                <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr id="trfq">
        <td align="right" style="width: 15%;">
            <asp:Label ID="lblfrequncy" runat="server" CssClass="FieldName" Text="Recurring freq:"></asp:Label>
        </td>
        <td style="width: 23.5%">
            <asp:DropDownList ID="ddlfrequncy" runat="server" CssClass="cmbField" AutoPostBack="false"
                TabIndex="1">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Weekly" Value="WK"></asp:ListItem>
                <asp:ListItem Text="Yearly" Value="YR"></asp:ListItem>
                <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>
                <asp:ListItem Text="Monthly" Value="MN"></asp:ListItem>
                <asp:ListItem Text="Daily" Value="DA"></asp:ListItem>
                <asp:ListItem Text="FortNightly" Value="FN"></asp:ListItem>
                <asp:ListItem Text="HalfYearly" Value="HY"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" TabIndex="28"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span><span id="Span5"></span>
            <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" ErrorMessage="Please enter the Amount"
                ValidationGroup="Vg" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblsumassure" runat="server" Text="Sum Assured:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtsumassure" runat="server" CssClass="txtField" TabIndex="28"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblRecomendationDate" runat="server" Text="Recommendation Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtRecomendationDate" CssClass="txtField" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01" AutoPostBack="false" TabIndex="10">
                <Calendar ID="Calendar3" UseRowHeadersAsSelectors="False" runat="server" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput3" DisplayDateFormat="dd/MM/yyyy" runat="server" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span10" class="spnRequiredField">*</span><span id="Span11"></span>
            <asp:RequiredFieldValidator ID="rfvRecomendationDate" ControlToValidate="txtRecomendationDate"
                ErrorMessage="Please enter the RecomendationDate" ValidationGroup="Vg" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtRemarks" Width="200px" TextMode="MultiLine" MaxLength="300" Height="65px"
                onkeydown="return (event.keyCode!=13);" runat="server" CssClass="txtField" TabIndex="51"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span><span id="spnLoginStatus"></span>
            <asp:RequiredFieldValidator ID="rfvRemarks" ControlToValidate="txtRemarks" ErrorMessage="Please enter the remarks"
                ValidationGroup="Vg" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CheckBox ID="chkIsactive" runat="server" Text="Is active" CssClass="FieldName"
                onClick="return UseProfileName()" AutoPostBack="false" />
        </td>
         <td align="right">
            <asp:Label ID="lblRecurringamt" runat="server" Text="Recurring amt:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtRecurringamt" runat="server" CssClass="txtField" TabIndex="28"></asp:TextBox>
            <span id="Span6" class="spnRequiredField">*</span><span id="Span7"></span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRecurringamt" ErrorMessage="Please enter the Recurringamt"
                ValidationGroup="Vg" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="rightField">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnSubmit_Click"
                ValidationGroup="Vg" />
            <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" Text="Update" OnClientClick="return isValidInUpdateCase()"
                Visible="False" OnClick="btnUpdate_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
