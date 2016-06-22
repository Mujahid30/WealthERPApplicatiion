<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsuranceIssueSetup.ascx.cs"
    Inherits="WealthERP.OfflineOrderBackOffice.InsuranceIssueSetup" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Insurance Setup
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table style="margin-left: 20px">
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblType" runat="server" CssClass="FieldName" Text="Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Life Insurance" Value="LI"></asp:ListItem>
                <asp:ListItem Text="General Insurance" Value="GI"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlType"
                ErrorMessage="Select Type" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td style="text-align: right">
            <asp:Label ID="lblIssuer" runat="server" Text="Issure:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbField" Width="300px">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlIssuer"
                ErrorMessage="Select Issure" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblPolicyName" runat="server" CssClass="FieldName" Text="Policy Name:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtPolicyName" runat="server" Width="544px"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPolicyName"
                ErrorMessage="Policy Name" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged"
                AutoPostBack="true" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlSubCategory"
                ErrorMessage="Select Category" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td style="text-align: right">
            <asp:Label ID="lblSubCategory" runat="server" CssClass="FieldName" Text="Sub Category:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:Label ID="lblReq" runat="server" Text="*" CssClass="spnRequiredField"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlSubCategory"
                ErrorMessage="Select Sub Category" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trdate" runat="server">
        <td align="right">
            <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName" Text="From:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="dvTransactionDate" runat="server" class="dvInLine">
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtOrderFrom"
                    ErrorMessage="<br />Select a From Date" CssClass="cvPCG" Display="Dynamic" runat="server"
                    InitialValue="" ValidationGroup="btnSubmit">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
        </td>
        <td align="right">
            <asp:Label ID="lblTo" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderTo" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="Div1" runat="server" class="dvInLine">
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrderTo"
                    ErrorMessage="<br />Select a To Date" CssClass="cvPCG" Display="Dynamic" runat="server"
                    InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderTo" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtOrderTo"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtOrderFrom" CssClass="cvPCG" ValidationGroup="btnSubmit"
                Display="Dynamic">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td style="text-align: right">
            <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:CheckBox ID="chkActive" runat="server" CssClass="cmbFielde" Text="Active:" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Submit" ValidationGroup="btnSubmit"
                OnClick="Submit_OnClick" />
            <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" Text="Update" ValidationGroup="btnSubmit"
                OnClick="btnUpdate_OnClick" Visible="false" />
        </td>
    </tr>
</table>
