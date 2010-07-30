<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorRMCustIndiDashboard.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorRMCustIndiDashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<table width="100%">
    <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="Label9" runat="server" CssClass="HeaderTextBig" Text="Customer Dashboard"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td width="50%">
            <asp:Label ID="Label4" runat="server" Text="Current Value" Class="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
        <td width="50%">
            <asp:Label runat="server" CssClass="HeaderTextSmall" Text="Asset Class Wise Investments"
                ID="Label5"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="50%" valign="top">
            <br />
            <asp:Chart ID="Chart1" runat="server" Height="250px" Palette="SemiTransparent" Width="400px">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </td>
        <td width="50%" valign="top">
            <asp:Label runat="server" CssClass="FieldName" Text="You have not Added any Asset Details.." ID="lblAssetDetailsMsg"></asp:Label>
            <asp:GridView ID="gvAssetAggrCurrentValue" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="100%">
                <%--<FooterStyle HorizontalAlign="Center" CssClass="FooterStyle"/>--%>
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Asset Class" HeaderText="Asset Class" SortExpression="Asset Class" />
                    <asp:BoundField DataField="Current Value" HeaderText="Current Value" SortExpression="Current Value"
                        DataFormatString="{0:n2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td width="50%">
            <asp:Label ID="Label3" runat="server" CssClass="HeaderTextSmall" Text="Life Insurance"></asp:Label>
            <hr />
        </td>
        <td width="50%">
            <asp:Label runat="server" CssClass="HeaderTextSmall" Text="General Insurance" ID="Label6"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="50%" valign="top">
            <asp:Label ID="lblLifeInsurance" runat="server" CssClass="FieldName" Text="No Details to display..."></asp:Label>
            <asp:GridView ID="gvLifeInsurance" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="97%">
                <%--<FooterStyle HorizontalAlign="Center" CssClass="FooterStyle"/>--%>
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Policy" HeaderText="Policy" />
                    <asp:BoundField DataField="InsuranceType" HeaderText="Type" />
                    <asp:BoundField DataField="SumAssured" HeaderText="Sum Assured" />
                    <asp:BoundField DataField="PremiumAmount" HeaderText="Premium Amount" />
                    <asp:BoundField DataField="PremiumFrequency" HeaderText="Premium Frequency" />
                </Columns>
            </asp:GridView>
        </td>
        <td width="50%" valign="top">
            <asp:Label ID="lblGeneralInsurance" runat="server" CssClass="FieldName" Text="No Details to display..."></asp:Label>
            <asp:GridView ID="gvGeneralInsurance" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="100%">
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="PolicyIssuer" HeaderText="Policy Issuer" />
                    <asp:BoundField DataField="InsuranceType" HeaderText="Type" />
                    <asp:BoundField DataField="SumAssured" HeaderText="Sum Assured" />
                    <asp:BoundField DataField="PremiumAmount" HeaderText="Premium Amount" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td width="50%">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Alerts"></asp:Label>
            <hr />
        </td>
        <td width="50%">
            <asp:Label runat="server" CssClass="HeaderTextSmall" Text="Investment Maturity Schedule"
                ID="Label11"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="50%" valign="top">
            <asp:Label ID="lblAlertsMessage" runat="server" CssClass="FieldName" Text="No Alerts..."></asp:Label>
            <asp:GridView ID="gvCustomerAlerts" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="100%">
                <%--<FooterStyle HorizontalAlign="Center" CssClass="FooterStyle"/>--%>
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Details" HeaderText="Details" SortExpression="Details" />
                    <asp:BoundField DataField="EventMessage" HeaderText="EventMessage" SortExpression="EventMessage" />
                </Columns>
            </asp:GridView>
        </td>
        <td width="50%" valign="top">
            <asp:Label ID="lblMaturityMsg" runat="server" CssClass="FieldName" Text="No Upcoming Maturity Dates.."></asp:Label>
            <asp:GridView ID="gvMaturitySchedule" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="100%">
                <%--<FooterStyle HorizontalAlign="Center" CssClass="FooterStyle"/>--%>
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Asset Group" HeaderText="Asset Group" />
                    <asp:BoundField DataField="Asset Particulars" HeaderText="Asset Particulars" />
                    <asp:BoundField DataField="Maturity Date" HeaderText="Maturity Date (dd/mm/yyyy)" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <%--<FooterStyle HorizontalAlign="Center" CssClass="FooterStyle"/>--%>
        <td colspan="2">
        </td>
    </tr>
</table>
