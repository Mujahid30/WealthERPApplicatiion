<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorRMCustGroupDashboard.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorRMCustGroupDashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<table width="100%">
    <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="lblGrpDashboard" runat="server" CssClass="HeaderTextBig" Text="Group Dashboard"></asp:Label>
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
            <asp:Label ID="Label5" runat="server" Text="Family Details" Class="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="50%" valign="top">
            <asp:Chart ID="Chart1" runat="server" Height="250px" Palette="SemiTransparent" Width="450px">
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
            <asp:Label ID="Label1" runat="server" Text="No. of Family Members" Class="HeaderTextSmall"> </asp:Label>
            <asp:Label ID="lblFamilyMembersNum" runat="server" Text="Label" CssClass="Field"></asp:Label>
            <asp:Label ID="lblMessage" runat="server" Text="You have not Added Family Details.."
                Class="FieldName"> </asp:Label>
            <asp:GridView ID="gvCustomerFamily" runat="server" CellPadding="4" CssClass="GridViewStyle"
                Width="100%" DataKeyNames="CustomerId" AutoGenerateColumns="False" Style="margin-bottom: 0px">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Member Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCustomerName" runat="server" CssClass="GridViewCmbField" OnClick="lnkCustomerNameFamilyGrid_Click"
                                Text='<%# Eval("Member Name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2" width="100%" valign="top">
            <br />
            <asp:Label runat="server" CssClass="FieldName" Text="No details to display.." ID="lblAssetDetailsMsg"></asp:Label>
            <div id="div1" style="overflow-y: scroll; height: 350px; overflow-x: scroll; width: 100%">
                <asp:GridView ID="gvAssetAggrCurrentValue" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" HorizontalAlign="Center"
                    Width="100%" DataKeyNames="CustomerId" ShowFooter="true" 
                    onrowdatabound="gvAssetAggrCurrentValue_RowDataBound">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" HorizontalAlign="Right"/>
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCustomerName" runat="server" CssClass="GridViewCmbField" OnClick="lnkCustomerNameAssetsGrid_Click"
                                    Text='<%# Eval("Customer_Name") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Equity" HeaderText="Equity" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Mutual_Fund" HeaderText="Mutual Fund" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Fixed_Income" HeaderText="Fixed Income" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Government_Savings" HeaderText="Govt. Savings" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Property" HeaderText="Property" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Pension_and_Gratuity" HeaderText="Pension and Gratuity"
                            DataFormatString="{0:n2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Personal_Assets" HeaderText="Personal Assets" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Gold_Assets" HeaderText="Gold" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Collectibles" HeaderText="Collectibles" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Cash_and_Savings" HeaderText="Cash and Savings" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Assets_Total" HeaderText="Assets Total" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Liabilities_Total" HeaderText="Liabilities Total" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Net_Worth" HeaderText="Net Worth" DataFormatString="{0:n2}"
                            HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                <%--</asp:Panel>--%>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
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
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="97%"
                DataKeyNames="CustomerId">
                <%--<FooterStyle HorizontalAlign="Center" CssClass="FooterStyle"/>--%>
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCustomerName" runat="server" CssClass="GridViewCmbField" OnClick="lnkCustomerNameLifeInsuranceGrid_Click"
                                Text='<%# Eval("CustomerName") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />--%>
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
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="100%"
                DataKeyNames="CustomerId">
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCustomerName" runat="server" CssClass="GridViewCmbField" OnClick="lnkCustomerNameGeneralInsuranceGrid_Click"
                                Text='<%# Eval("CustomerName") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td width="50%">
            <asp:Label ID="lblAlerts" runat="server" CssClass="HeaderTextSmall" Text="Alerts"></asp:Label>
            <hr />
        </td>
        <td width="50%">
            <asp:Label runat="server" CssClass="HeaderTextSmall" Text="Investment Maturity Schedule"
                ID="Label2"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="50%" valign="top">
            <asp:Label ID="lblAlertsMessage" runat="server" CssClass="FieldName" Text="No Alerts..."></asp:Label>
            <asp:GridView ID="gvCustomerAlerts" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="97%"
                DataKeyNames="CustomerId">
                <%--<FooterStyle HorizontalAlign="Center" CssClass="FooterStyle"/>--%>
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCustomerName" runat="server" CssClass="GridViewCmbField" OnClick="lnkCustomerNameAlertsGrid_Click"
                                Text='<%# Eval("CustomerName") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />--%>
                    <asp:BoundField DataField="Details" HeaderText="Details" />
                    <asp:BoundField DataField="EventMessage" HeaderText="EventMessage" />
                    <asp:BoundField DataField="PopulatedDate" HeaderText="Date" />
                </Columns>
            </asp:GridView>
        </td>
        <td width="50%" valign="top">
            <asp:Label ID="lblMaturityMsg" runat="server" CssClass="FieldName" Text="No Upcoming Maturity Dates.."></asp:Label>
            <asp:GridView ID="gvMaturitySchedule" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" EnableViewState="false" Width="100%"
                DataKeyNames="CustomerId">
                <%--<FooterStyle HorizontalAlign="Center" CssClass="FooterStyle"/>--%>
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCustomerName" runat="server" CssClass="GridViewCmbField" OnClick="lnkCustomerNameMaturityDatesGrid_Click"
                                Text='<%# Eval("Customer Name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="Customer Name" HeaderText="Customer Name" />--%>
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
