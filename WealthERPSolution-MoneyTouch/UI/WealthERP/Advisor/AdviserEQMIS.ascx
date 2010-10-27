<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserEQMIS.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserEQMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<table style="width: 100%;">
    <tr>
        <td class="HeaderTextBig" colspan="2">
            <asp:Label ID="lblMfMIS" runat="server" CssClass="HeaderTextBig" Text="EQ MIS"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table>
    <tr id="trRange" visible="false" runat="server">
        <td valign="top" align="right">
            <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td valign="top" align="right">
            <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="Please select a To Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
</table>
<table>
    <tr id="trPeriod" visible="false" runat="server">
        <td colspan="2">
            <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
            <asp:DropDownList ID="ddlPeriod" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td>
    <asp:Label ID="lblChooseBranchBM" runat="server" Font-Bold="true"  CssClass="FieldName"  Text="Branch: "></asp:Label>
    <asp:DropDownList ID="ddlBranchForEQ" style="vertical-align: middle"  CssClass="cmbField" runat="server" 
            AutoPostBack="true" 
            onselectedindexchanged="ddlBranchForEQ_SelectedIndexChanged">
    <%--<asp:ListItem Value="1086" Text="All"></asp:ListItem>
                 <asp:ListItem Value="1145" Text="AJAY SINGH"></asp:ListItem>
                 <asp:ListItem Value="1058" Text="INVESTPRO FINANCIAL  SERV"></asp:ListItem>--%>
    </asp:DropDownList>
    </td>
    <td>
    <asp:Label ID="lblChooseRM" runat="server" Font-Bold="true"  CssClass="FieldName"  Text="RM: "></asp:Label>
    <asp:DropDownList ID="ddlRMEQ" style="vertical-align: middle"  CssClass="cmbField" runat="server" 
            AutoPostBack="true" 
            onselectedindexchanged="ddlRMEQ_SelectedIndexChanged">
    </asp:DropDownList>
    </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="PCGButton"
                ValidationGroup="btnGo" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AdviserEQMIS_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AdviserEQMIS_btnGo', 'S');" />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td colspan="2">
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found..."></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gvEQMIS" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" EnableViewState="false" CssClass="GridViewStyle" GridLines="Both" ShowFooter="True">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="DeliveryBuy" HeaderText="Delivery Buy Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="DeliverySell" HeaderText="Delivery Sell Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="SpeculativeSell" HeaderText="Speculative Sell Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="SpeculativeBuy" HeaderText="Speculative Buy Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>

<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" />
<asp:HiddenField ID="hdnall" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
