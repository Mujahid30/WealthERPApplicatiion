<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calculators.ascx.cs" Inherits="WealthERP.General.Calculators" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<script  id="myScript" language="javascript" type="text/javascript">
    function OnChanged(sender, args) {
        document.getElementById("<%= hidTabIndex.ClientID %>").value = sender.get_activeTab()._tabIndex;
        //uncheckallCehckBoxes();
        return false;
    }
</script>
<table>
<tr><td>
   <cc1:TabContainer ID="tabCalculators" runat="server" ActiveTabIndex="0"
                            Width="100%" Style="visibility: visible" 
                            OnClientActiveTabChanged="OnChanged">
                            <cc1:TabPanel ID="tabpnlEMICalculator" runat="server" HeaderText="EMI Calculator"
                                Width="100%">
                                <HeaderTemplate>
                                    EMI Calculators</HeaderTemplate>
                                <ContentTemplate>
    <table>
    <tr><td align="right"><asp:Label ID="lblLOanAmount" runat="server" Text="Loan Amount:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtLoanAmount" runat="server" Text="" CssClass="Field"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Label ID="lblTenure" runat="server" Text="Tenure:" CssClass="FieldName"></asp:Label></td>
    <td align="left">
    <asp:TextBox ID="txtTenureYears" runat="server" Text="" CssClass="Field"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="txtTenureYears_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtTenureYears" WatermarkText="Years">
                    </cc1:TextBoxWatermarkExtender>
    <asp:TextBox ID="txtTenureMonths" runat="server" Text="" CssClass="Field"></asp:TextBox>
    <cc1:TextBoxWatermarkExtender ID="txtTenureMonths_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtTenureMonths" WatermarkText="Months">
                    </cc1:TextBoxWatermarkExtender>
    </td></tr>
    <tr><td align="right"><asp:Label ID="lblInterestRate" runat="server" Text="Interest Rate(%):" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtInterest" runat="server" Text="" CssClass="Field"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Label ID="lblInstallmentFrequency" runat="server" Text="Installment Frequency:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:DropDownList ID="ddlFrequency" runat="server" CssClass="cmbField">
    <asp:ListItem Text="Daily" Value="DA"></asp:ListItem>
    <asp:ListItem Text="Weekly" Value="WK"></asp:ListItem>
    <asp:ListItem Text="FortNightly" Value="FN"></asp:ListItem>
    <asp:ListItem Text="Monthly" Value="MN"></asp:ListItem>
    <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>
    <asp:ListItem Text="Half Yearly" Value="HY"></asp:ListItem>
    <asp:ListItem Text="Yearly" Value="YR"></asp:ListItem>
    </asp:DropDownList></td></tr>
    <tr><td align="right"><asp:Label ID="lblStartDate" runat="server" Text="Installment Start Date:" CssClass="FieldName"></asp:Label></td>
    <td align="left">
    <asp:TextBox ID="txtStartDate" runat="server" Text="" CssClass="Field"></asp:TextBox>
    <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server"
                        TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtStartDate_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtStartDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
    </td></tr>
    <tr>
    <td align="right">
    <asp:Label ID="lblEndDate" runat="server" Text="Installment End Date:" CssClass="FieldName"></asp:Label>
    </td>
    <td align="left">
    <asp:TextBox ID="txtEndDate" runat="server" Text="" CssClass="Field"></asp:TextBox>
    <cc1:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server"
                        TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtEndDate_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtEndDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
    </td></tr>
    <tr><td colspan="2"><asp:Button ID="btnCalculateEMI" runat="server" 
            Text="Calculate" onclick="btnCalculateEMI_Click" CssClass="PCGButton" /> </td></tr>

    </table>
    <table id="tblResult" runat="server" visible="false">
    
    <tr><td colspan="2"><table><tr><td align="right"><asp:Label ID="lblEMIAmount" runat="server" Text="EMI Amount:" CssClass="FieldName"></asp:Label> </td>
    <td align="left"><asp:Label ID="lblEMIAmountValue" runat="server" Text="" CssClass="Field"></asp:Label></td></tr>
     <tr><td align="right"><asp:Label ID="lblNoOfInstallments" runat="server" Text="Number Of Installments:" CssClass="FieldName"></asp:Label> </td>
    <td align="left"><asp:Label ID="lblNoOfInstallmentsValue" runat="server" Text="" CssClass="Field"></asp:Label></td></tr></table></td></tr>
    <tr><td colspan="2"><asp:Label ID="lblPaymentScheduleHeader" runat="server" Text="Payment Schedule" CssClass="HeaderTextSmall"></asp:Label></td></tr>
     <tr><td colspan="2">
     <asp:GridView ID="gvRepaymentSchedule" EnableViewState="true" AllowPaging="false" CssClass="GridViewStyle"
                runat="server" AutoGenerateColumns="False" ShowFooter="True" 
                >
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    
                    <asp:BoundField DataField="Period" HeaderText="Period"  ItemStyle-HorizontalAlign="Left"   HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="InstallmentDate" HeaderText="Installment Date"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="InstallmentValue" HeaderText="Installment Value"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Principal" HeaderText="Principal" 
                        ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CummulativePrincipal" HeaderText="Cummulative Principal Paid" 
                        ItemStyle-HorizontalAlign="right" HeaderStyle-HorizontalAlign="right">
                        <ItemStyle HorizontalAlign="right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Interest" HeaderText="Interest" 
                        ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CummulativeInterest" HeaderText="Cummulative Interest Paid" 
                        ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Balance" HeaderText="Balance" 
                        ItemStyle-HorizontalAlign="right" HeaderStyle-HorizontalAlign="right">
                        <ItemStyle HorizontalAlign="right"></ItemStyle>
                    </asp:BoundField>
                   
                </Columns>
            </asp:GridView> </td>
    </tr>
    </table>
    </ContentTemplate></cc1:TabPanel>
    <cc1:TabPanel  ID="tabpnlPV" runat="server" HeaderText="Present Value Calculator"
                                Width="100%">
    <HeaderTemplate>Present Value Calculator</HeaderTemplate>
    <ContentTemplate>
    <table>
    <tr><td align="right"><asp:Label ID="lblPVInterestRate" runat="server" Text="Interest Rate(%):" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtPVInterestRate" runat="server" 
            CssClass="Field"></asp:TextBox></td></tr>
            <tr><td align="right"><asp:Label ID="lblPaymentFrequency" runat="server" Text="Payment Frequency:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:DropDownList ID="ddlPVPaymentFrequency" runat="server" CssClass="cmbField">
    <asp:ListItem Text="Daily" Value="DA"></asp:ListItem>
    <asp:ListItem Text="Weekly" Value="WK"></asp:ListItem>
    <asp:ListItem Text="FortNightly" Value="FN"></asp:ListItem>
    <asp:ListItem Text="Monthly" Value="MN"></asp:ListItem>
    <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>
    <asp:ListItem Text="Half Yearly" Value="HY"></asp:ListItem>
    <asp:ListItem Text="Yearly" Value="YR"></asp:ListItem>
    </asp:DropDownList></td></tr>
    <tr><td align="right"><asp:Label ID="lblPVNoOfPayments" runat="server" Text="No. Of Payments:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtPVNoOfPayments" runat="server" 
            CssClass="Field"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Label ID="lblPVPaymentMade" runat="server" Text="Payment Made:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtPVPaymentMade" runat="server" CssClass="Field"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Label ID="lblPVFutureValue" runat="server" Text="Future Value:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtPVFutureValue" runat="server" CssClass="Field"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Label ID="txtPVType" runat="server" Text="Payments Are Due at:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:RadioButtonList ID="rblPVType" runat="server" CssClass="Field" RepeatDirection="Horizontal">
    <asp:ListItem Text="the end of the period" Value="0" Selected="True"></asp:ListItem>
    <asp:ListItem Text="the beginning of the period" Value="1"></asp:ListItem>
    </asp:RadioButtonList></td></tr>
    <tr><td colspan="2"><asp:Button ID="btnCalculatePV" runat="server" Text="Calculate" 
            CssClass="PCGButton" onclick="btnCalculatePV_Click" /></td></tr>
    <tr id="trPVResult" runat="server" visible="False"><td align="right" runat="server"><asp:Label ID="lblPV" runat="server" Text="Present Value:" CssClass="FieldName"></asp:Label> </td>
    <td align="left" runat="server"><asp:Label ID="lblPVValue" runat="server" 
            CssClass="Field"></asp:Label></td></tr>
    </table>
    </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel  ID="tabpnlFV" runat="server" HeaderText="Future Value Calculator"
                                Width="100%">
    <HeaderTemplate>Future Value Calculator</HeaderTemplate>
    <ContentTemplate>
    <table>
    <tr><td align="right"><asp:Label ID="lblFVInterestRate" runat="server" Text="Interest Rate(%):" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtFVInterestRate" runat="server" 
            CssClass="Field"></asp:TextBox></td></tr>
            <tr><td align="right"><asp:Label ID="lblFVPaymentFrequency" runat="server" Text="Payment Frequency:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:DropDownList ID="ddlFVPaymentFrequency" runat="server" CssClass="cmbField">
    <asp:ListItem Text="Daily" Value="DA"></asp:ListItem>
    <asp:ListItem Text="Weekly" Value="WK"></asp:ListItem>
    <asp:ListItem Text="FortNightly" Value="FN"></asp:ListItem>
    <asp:ListItem Text="Monthly" Value="MN"></asp:ListItem>
    <asp:ListItem Text="Quarterly" Value="QT"></asp:ListItem>
    <asp:ListItem Text="Half Yearly" Value="HY"></asp:ListItem>
    <asp:ListItem Text="Yearly" Value="YR"></asp:ListItem>
    </asp:DropDownList></td></tr>
    <tr><td align="right"><asp:Label ID="lblFVNoOfPayments" runat="server" Text="No. Of Payments:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtFVNoOfPayments" runat="server" 
            CssClass="Field"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Label ID="lblFVPaymentMade" runat="server" Text="Payment Made:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtFVPaymentMade" runat="server" CssClass="Field"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Label ID="lblFVPresentValue" runat="server" Text="Present Value:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:TextBox ID="txtFVFutureValue" runat="server" CssClass="Field"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Label ID="txtFVType" runat="server" Text="Payments Are Due at:" CssClass="FieldName"></asp:Label></td>
    <td align="left"><asp:RadioButtonList ID="rblFVType" runat="server" CssClass="Field" RepeatDirection="Horizontal">
    <asp:ListItem Text="the end of the period" Value="0" Selected="True"></asp:ListItem>
    <asp:ListItem Text="the beginning of the period" Value="1"></asp:ListItem>
    </asp:RadioButtonList></td></tr>
    <tr><td colspan="2">
        <asp:Button ID="btnCalculateFV" runat="server" Text="Calculate" 
            CssClass="PCGButton" onclick="btnCalculateFV_Click" /></td></tr>
    <tr id="trFVResult" runat="server" visible="False"><td id="Td1" align="right" runat="server"><asp:Label ID="lblFV" runat="server" Text="Future Value:" CssClass="FieldName"></asp:Label> </td>
    <td id="Td2" align="left" runat="server"><asp:Label ID="lblFVValue" runat="server" 
            CssClass="Field"></asp:Label></td></tr>
    </table>
    </ContentTemplate>
    </cc1:TabPanel>
    </cc1:TabContainer>
    </td></tr>
</table>
<asp:HiddenField ID="hidTabIndex" Value="0" runat="server" />