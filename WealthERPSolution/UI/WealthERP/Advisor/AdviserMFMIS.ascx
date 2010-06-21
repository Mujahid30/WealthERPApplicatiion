<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserMFMIS.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserMFMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<table style="width: 100%;">
    <tr>
        <td class="HeaderTextBig" colspan="3">
            <asp:Label ID="lblMfMIS" runat="server" CssClass="HeaderTextBig" Text="MF MIS"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trMISTypeSelection" runat="server">
        <td colspan="3">
            <asp:Label ID="lblMISType" runat="server" CssClass="FieldName">MIS Type:</asp:Label>
            <asp:DropDownList ID="ddlMISType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMISType_SelectedIndexChanged"
                AutoPostBack="true" Visible="true">
                <asp:ListItem Value="AMCWiseAUM">AMC Wise AUM</asp:ListItem>
                <asp:ListItem Value="AMCSchemeWiseAUM">Scheme Wise AUM</asp:ListItem>
                <asp:ListItem Value="FolioWiseAUM">Folio Wise AUM</asp:ListItem>
                <asp:ListItem Value="TurnOverSummary" Selected="True">Turn Over Summary</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr id="trRange" runat="server">
                    <td align="right" valign="top">
                        <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnGo">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td align="right" valign="top">
                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnGo">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />To Date should not less than From Date"
                            Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span id="spnBranch" runat="server">
                    <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    </span>
        </td>
        <td align="right">
            <span id="spnRM" runat="server">&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            </span>
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr id="trPeriod" visible="false" runat="server">
        <td>
            <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPeriod" runat="server" AutoPostBack="true" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" ValidationGroup="btnGo"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AdviserMFMIS_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AdviserMFMIS_btnGo', 'S');" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <table id="ErrorMessage" align="center" runat="server">
                <tr>
                    <td>
                        <div class="failure-msg" align="center">
                            No Records found.....
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="gvMFMIS" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" EnableViewState="false" CssClass="GridViewStyle" ShowFooter="True">
                <%--OnSorting="gvMFMIS_Sorting" OnDataBound="gvMFMIS_DataBound"--%>
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="BuyValue" HeaderText="Buy Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="SellValue" HeaderText="Sell Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="NoOfTrans" HeaderText="No. of Transactions" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="SIPValue" HeaderText="SIP Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="NoOfSIPs" HeaderText="No. of SIPs" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
