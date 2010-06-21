<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddLOB.ascx.cs" Inherits="WealthERP.Advisor.AddLOB" %>
<asp:ScriptManager ID="scriptmanager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>--%>

<script type="text/javascript">
    function checkSelection() {
        var form = document.forms[0];
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                }
            }
        }
        if (count == 0) {
            alert("Please select atleast one LOB")
            return false;
        }
        return true;
    }
</script>

<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Add LOB"></asp:Label>
            <hr />
        </td>
    </tr>
</table>


<table class="TableBackground" style="width: 100%;">
    <%--<tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Choose LOB" CssClass="HeaderTextBig"></asp:Label>
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>--%>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 250px">
            <asp:CheckBox ID="chkMf" runat="server" Text="Mutual Fund" AutoPostBack="True" OnCheckedChanged="chkMF_CheckedChanged"
                CssClass="txtField" />
        </td>
        <td style="width: 250px">
            <asp:CheckBox ID="chkIntermediary" runat="server" Text="Intermediary" CssClass="txtField" />
        </td>
        <td>
            <asp:Label ID="lblErrorMsg" runat="server" CssClass="Error" Text="Already Exists, Please add variations"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkEquity" runat="server" Text="Equity" AutoPostBack="True" OnCheckedChanged="chkEQ_CheckedChanged"
                CssClass="txtField" />
        </td>
        <td>
            <asp:CheckBox ID="chkBroker" runat="server" Text="Broker" CssClass="txtField" />
        </td>
        <td>
            <asp:CheckBox ID="chkCash" runat="server" Text="Cash Segment" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkSubbroker" runat="server" Text="Sub Broker" CssClass="txtField" />
        </td>
        <td>
            <asp:CheckBox ID="chkDerivative" runat="server" Text="Derivative Segment" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkRemissary" runat="server" Text="Remissary" CssClass="txtField" />
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkInsurance" runat="server" Text="Insurance" CssClass="txtField"
                AutoPostBack="True" OnCheckedChanged="chkInsurance_CheckedChanged" />
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkInsuranceAgent" runat="server" Text="Agent" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkPostalSavings" runat="server" Text="Postal Savings" CssClass="txtField"
                AutoPostBack="True" OnCheckedChanged="chkPostalSavings_CheckedChanged" />
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkPostalAgent" runat="server" Text="Agent" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkLiabilities" runat="server" Text="Liabilities : Direct Sale Products"
                AutoPostBack="True" CssClass="txtField" OnCheckedChanged="chkLiabilities_CheckedChanged" />
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkLiabilitiesAgent" runat="server" Text="Agent" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkPMS" runat="server" Text="PMS" CssClass="txtField" OnCheckedChanged="chkPMS_CheckedChanged"
                AutoPostBack="True" />
        </td>
        <td>
            <asp:CheckBox ID="chkPMSBroker" runat="server" Text="Broker" CssClass="txtField" />
        </td>
        <td>
            <asp:CheckBox ID="chkPMSCash" runat="server" Text="Cash Segment" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkPMSSubBroker" runat="server" Text="Sub Broker" CssClass="txtField" />
        </td>
        <td>
            <asp:CheckBox ID="chkPMSDerivative" runat="server" Text="Derivative Segment" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkPMSRemissary" runat="server" Text="Remissary" CssClass="txtField" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkRealEstate" runat="server" Text="Real Estate" CssClass="txtField"
                AutoPostBack="True" OnCheckedChanged="chkRealEstate_CheckedChanged" />
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkRealEstateAgent" runat="server" Text="Agent" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkCommodities" runat="server" Text="Commodities" CssClass="txtField"
                AutoPostBack="True" OnCheckedChanged="chkCommodities_CheckedChanged" />
        </td>
        <td>
            <asp:CheckBox ID="chkCommBroker" runat="server" Text="Broker" CssClass="txtField" />
        </td>
        <td>
            <asp:CheckBox ID="chkCommCash" runat="server" Text="Cash Segment" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkCommSubBroker" runat="server" Text="Sub Broker" CssClass="txtField" />
        </td>
        <td>
            <asp:CheckBox ID="chkCommDerivative" runat="server" Text="Derivative" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkCommRemissary" runat="server" Text="Remissary" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkFixedIncome" runat="server" Text="Fixed Income" CssClass="txtField"
                AutoPostBack="True" OnCheckedChanged="chkFixedIncome_CheckedChanged" />
        </td>
        <td colspan="2">
            <asp:CheckBox ID="chkFIAgent" runat="server" Text="Agent" CssClass="txtField" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnAddLOB" runat="server" Text="Add" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddLOB_btnAddLOB', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddLOB_btnAddLOB', 'S');"
                OnClick="btnAddLOB_Click" OnClientClick="return checkSelection()" />
        </td>
    </tr>
</table>
</ContentTemplate> </asp:UpdatePanel>
<p>
    &nbsp;</p>
