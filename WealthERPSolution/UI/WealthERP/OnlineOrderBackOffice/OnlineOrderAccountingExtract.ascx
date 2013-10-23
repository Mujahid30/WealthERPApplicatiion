<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineOrderAccountingExtract.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineOrderAccountingExtract" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table id="tblMessage" width="100%" runat="server" visible="false">
    <tr id="trSumbitSuccess">
        <td align="center">
            <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
            </div>
        </td>
    </tr>
</table>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Order Extract
                        </td>
                        
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<br />
<table style="display:none" width="100%">
    <tr align="center">
        <td align="center">
            <div id="divValidationError" runat="server" class="failure-msg" align="center" visible="true">
                <asp:ValidationSummary ID="vsSummary" runat="server" Visible="true" ValidationGroup="btnSubmit" />
            </div>
        </td>
    </tr>
</table>
<table>
    <tr class="spaceUnder">
        <td>
        </td>
        <td align="right" style="vertical-align: top;">
            <asp:Label ID="lblAmc" runat="server" Text="Extract Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlExtractType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvAmc" runat="server" CssClass="rfvPCG" ErrorMessage="Please Select an AMC"
                Display="Dynamic" ControlToValidate="ddlExtractType" InitialValue="0" ValidationGroup="btnSubmit">Please Select an AMC</asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td align="right">
            <asp:Label ID="Label1" runat="server" Text="Extract Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtExtractDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vgBtnSubmitTemp"
                runat="server" CssClass="cvPCG" ErrorMessage="Enter A Date" Display="Dynamic"
                ControlToValidate="txtExtractDate"></asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td align="right">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Extract and Save As:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlSaveAs" runat="server" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Value="1" Text="txt"></asp:ListItem>
                <%--<asp:ListItem Value="2" Text="dbf"></asp:ListItem>--%>
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
            <asp:Button ID="btnExtract" runat="server" Text="Extract" CssClass="PCGButton" OnClick="btnExtract_Click" />
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
