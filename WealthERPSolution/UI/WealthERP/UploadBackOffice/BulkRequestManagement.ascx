<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BulkRequestManagement.ascx.cs"
    Inherits="WealthERP.UploadBackOffice.BulkRequestManagement" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 18%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
</style>
<asp:ScriptManager ID="scrptMgr" runat="server">
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
                            Bulk Request Management
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<telerik:RadTabStrip ID="RadTabStripBulkOrderRequest" runat="server" EnableTheming="True"
    Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="multipageBulkOrderRequest"
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Request Bulk Order" Value="RequestBulkOrder"
            TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Bulk Order Status" Value="BulkOrderStatus">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="multipageBulkOrderRequest" EnableViewState="true" runat="server">
    <telerik:RadPageView ID="rpvRequestBulkOrder" runat="server">
        <asp:Panel ID="panelRequestBulkOrder" runat="server">
            <table width="100%">
                <tr>
                    <td width="100%" align="center">
                        <div id="msgUploadComplete" runat="server" class="success-msg" align="center" visible="false">
                            Uploading successfully Completed
                        </div>
                    </td>
                </tr>
            </table>
            <table width="60%" runat="server" id="Table1">
                <tr>
                    <td class="leftLabel">
                        <asp:Label ID="Label1" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlSelectType" runat="server" CssClass="cmbField" AutoPostBack="true"
                            Width="205px" OnSelectedIndexChanged="Product_SelectedIndexChanged" InitialValue="Select"
                            TabIndex="1">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem Value="FI">Order Book NCD</asp:ListItem>
                            <asp:ListItem Value="IP">Order Book IPO</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span1" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvType1" runat="server" ErrorMessage="Please Select Order Type"
                            CssClass="rfvPCG" ControlToValidate="ddlSelectType" ValidationGroup="btnGo1"
                            Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trSelectIssueRow" visible="false">
                    <td class="leftLabel">
                        <asp:Label ID="Label3" runat="server" Text="Select Issue:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlSelectIssue" runat="server" CssClass="cmbField" AutoPostBack="true"
                            Width="205px" Visible="false" TabIndex="2">
                            
                        </asp:DropDownList>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvIssue" runat="server" ErrorMessage="Please Select Issue"
                            CssClass="rfvPCG" ControlToValidate="ddlSelectIssue" ValidationGroup="btnGo1"
                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="tr2" runat="server">
                    <td class="leftLabel">
                        <asp:Button ID="btnGo1" runat="server" Text="Go" TabIndex="3" CssClass="PCGButton"
                            ValidationGroup="btnGo1" OnClick="btnGo_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="rightData">
                        &nbsp;
                    </td>
                    <td class="leftLabel">
                        &nbsp;
                    </td>
                    <td class="rightData">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvBulkOrderStatus" runat="server">
        <asp:Panel ID="panelBulkOrderStatus" runat="server">
            <table width="70%" runat="server" id="tbIssueType">
                <tr>
                    <td class="leftLabel">
                        <asp:Label ID="lbtype" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" AutoPostBack="true"
                            Width="205px" InitialValue="Select">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem Value="FI">Order Book NCD</asp:ListItem>
                            <asp:ListItem Value="IP">Order Book IPO</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span2" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvType2" runat="server" ErrorMessage="Please Select Order Type"
                            CssClass="rfvPCG" ControlToValidate="ddlIssueType" ValidationGroup="btnGo2" Display="Dynamic"
                            InitialValue="Select"></asp:RequiredFieldValidator>
                    </td>
                    <td class="leftLabel">
                        <asp:Label ID="lbdate" runat="server" Text="Requested Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightData">
                        <telerik:RadDatePicker ID="txtReqDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            TabIndex="3" Width="150px" AutoPostBack="true">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span18" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Requested Date"
                            Display="Dynamic" ControlToValidate="txtReqDate" InitialValue="" ValidationGroup="btnGo2">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <table width="60%">
                <tr id="trBtnSubmit" runat="server">
                    <td class="leftLabel">
                        <asp:Button ID="btnGo2" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo2" />
                    </td>
                    <td class="rightData">
                    </td>
                    <td class="leftLabel">
                        &nbsp;
                    </td>
                    <td class="rightData">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
