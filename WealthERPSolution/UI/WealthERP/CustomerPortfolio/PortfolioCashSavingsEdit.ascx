<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioCashSavingsEdit.ascx.cs" Inherits="WealthERP.CustomerPortfolio.PortfolioCashSavingsEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div style="float: left;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnlEditCnS" runat="server">
        <table style="width: 100%;" class="TableBackground">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextSmall" Text="Edit Cash & Savings Details">
                    </asp:Label>
                </td>
            </tr>
            <tr id="trError" runat="server" visible="false">
                <td colspan="2" align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="Error" Text="Error Editing Details!">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblAssetIntrumentCategory" runat="server" CssClass="FieldName" Text="Asset Category"
                        AssociatedControlID="ddlAssetIC">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlAssetIC" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblAccountId" runat="server" CssClass="FieldName" Text="Account Id"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlAccountID" runat="server" CssClass="">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name" AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblDepositAmount" runat="server" CssClass="FieldName" Text="Deposit Amount"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDepositAmount" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblDepositDate" runat="server" CssClass="FieldName" Text="Deposit Date"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDepositDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtDepositDate">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" Text="Current Value"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblInterestRate" runat="server" CssClass="FieldName" Text="Interest Rate %"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtInterestRate" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblDebtIssuer" runat="server" CssClass="FieldName" Text="Debt Issuer"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlDebtIssuer" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblInterestBasis" runat="server" CssClass="FieldName" Text="Interest Basis Type"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlInterestBasis" runat="server" OnSelectedIndexChanged="ddlInterestBasis_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trCIFrequency" visible="false" runat="server">
                <td align="left">
                    <asp:Label ID="lblCompoundInterestFrequency" runat="server" CssClass="FieldName"
                        Text="Frequency" AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlCIFrequency" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trIPFrequency" runat="server" visible="false">
                <td align="left">
                    <asp:Label ID="lblInterestPayableFrequency" runat="server" CssClass="FieldName" Text="Interest Payable Frequency"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlIPFrequency" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblIsInterestAccumulated" runat="server" CssClass="FieldName" Text="Is Interest Accumulated?"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:RadioButton ID="rbtnInterestAccumYes" GroupName="Interest Accumulated" runat="server"
                        Text="Yes"  OnCheckedChanged="rbtnInterestAccum_CheckChanged" />
                    <asp:RadioButton ID="rbtnInterestAccumNo" GroupName="Interest Accumulated" runat="server"
                        Text="No" OnCheckedChanged="rbtnInterestAccum_CheckChanged" />
                </td>
            </tr>
            <tr id="trIA" runat="server" visible="false">
                <td align="left">
                    <asp:Label ID="lblInterestAmtAccumulated" runat="server" CssClass="FieldName" Text="Interest Amount Accumulated"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtInterestAmtAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr id="trIP" runat="server" visible="false">
                <td align="left">
                    <asp:Label ID="lblInterestAmtPaidOut" runat="server" CssClass="FieldName" Text="Interest Amount Paid Out"
                        AssociatedControlID="">
                    </asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtInterestAmtPaidOut" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td colspan="2" class="SubmitCell">
                    <asp:Button ID="btnUpdateDetails" runat="server" Text="Update Details" OnClick="btnUpdateDetails_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>