<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioDashboard.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioDashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%--<meta http-equiv="cache-control" content="no-cache"/>
<meta http-equiv="expires" content="0"/>
<meta http-equiv="pragma" content="no-cache"/>--%>
<table><tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" 
                Text="Portfolio Name:"></asp:Label>
        </td>
        <td colspan="3" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" 
                AutoPostBack="True" onselectedindexchanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr></table>
<table>
 
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Panel ID="pnlAssetSummary" runat="server">
                <table border="1" style="border-collapse: collapse; width: 100%;">
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label1" CssClass="HeaderTextSmall" Text="Total Assets Pie Chart" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" CssClass="HeaderTextSmall" Text="Net Income Summary" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" class="style1">
                            <asp:Chart ID="chrtTotalAssets" runat="server" BackColor="#EBEFF9" Palette="Pastel" Width="500px" Height="250px">
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
                    </tr>
                    <tr>
                        <td valign="top">
                            <table>
                            <tr><td align="left"><asp:Label ID="lblMF" CssClass="HeaderTextSmall" Text="MF" runat="server"></asp:Label></td></tr>
                             <tr>
                                    <td align="right">
                                        <asp:Label ID="lblMFRPL" CssClass="FieldName" Text="Realised P/L:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblMFRPLValue" CssClass="Field" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                        <asp:Label ID="lblMFSubTotal" CssClass="FieldName" Text="MF SubTotal:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" 
                                         style="border-style: solid none solid none; border-width: thin; border-color: #000000">
                                        <asp:Label ID="lblMFSubTotalValue" CssClass="Field" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr><td align="left"><asp:Label ID="lblEQ" CssClass="HeaderTextSmall" Text="Equity" runat="server"></asp:Label></td></tr>

                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label3" CssClass="FieldName" Text="Realised P/L - Delivery:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblRealisedDeliv" CssClass="Field" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td  align="right">
                                        <asp:Label ID="Label4" CssClass="FieldName" Text="Realised P/L - Speculative:" runat="server"></asp:Label>
                                    </td>
                                    <td  align="left">
                                        <asp:Label ID="lblRealisedSpec" CssClass="Field" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label6" CssClass="FieldName" Text="Equity SubTotal:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" style="border-style: solid none solid none; border-width: thin; border-color: #000000">
                                        <asp:Label ID="lblEQSubTotalValue" CssClass="Field" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                       
                                    </td>
                                    <td>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label11" CssClass="HeaderTextSmaller" Text="Total:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" style="border-style: solid none solid none; border-width: thin; border-color: #000000">
                                        <asp:Label ID="lblTotalValue" CssClass="Field" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" CssClass="HeaderTextSmall" Text="Dividend Income" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr><td align="right"><asp:Label ID="Label13" CssClass="FieldName" Text="Equity:" runat="server"></asp:Label></td>
                                <td align="left"><asp:Label ID="lblEQDidvidend" CssClass="Field" runat="server"></asp:Label></td></tr>
                                <tr><td align="right"><asp:Label ID="Label12" CssClass="FieldName" Text="MF:" runat="server"></asp:Label></td>
                                <td align="left"><asp:Label ID="lblDividend" CssClass="Field" runat="server"></asp:Label></td></tr>
                                 <tr><td align="right"><asp:Label ID="Label14" CssClass="FieldName" Text="Total:" runat="server"></asp:Label></td>
                                <td align="left" style="border-style: solid none solid none; border-width: thin; border-color: #000000">
                                <asp:Label ID="lblDivIncomeTotal" CssClass="Field" runat="server"></asp:Label>
                                </td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMFHeader" runat="server" CssClass="HeaderTextSmall" Text="MF Investments"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trMFNoRecords" runat="server" visible="false">
        <td style="background-color: White; color: Black;" align="center" colspan="2">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
        </td>
    </tr>
    <tr id="trMFDate" runat="server" visible="false">
        <td style="background-color: White; color: Black;" align="center" colspan="2">
            <asp:Label ID="lblMFDate" runat="server" CssClass="FieldName" Text="Valuation Date"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr id="trMFData" runat="server">
        <td valign="top">
            <asp:Panel ID="pnlMFInvestments" runat="server">
                <asp:GridView ID="gvMFInv" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" EnableViewState="false" SAllowPaging="True" CssClass="GridViewStyle"
                    OnRowDataBound="gvMFInv_RowDataBound" OnSorting="gvMFInv_Sorting" Width="100%" ShowFooter="true">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="SchemeType" HeaderText="Scheme Type"  />
                        <asp:BoundField DataField="Scheme" HeaderText="Scheme" />
                        <asp:BoundField DataField="AmortisedCost" HeaderText="Amortised Cost (Rs)" 
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" />
                        <asp:BoundField DataField="CurrentValue" HeaderText="Current Value (Rs)" 
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </td>
        <td style="width: 30%;" valign="top">
            <asp:Chart ID="chrtMFInv" runat="server">
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
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblEquityHeader" runat="server" CssClass="HeaderTextSmall" Text="Equity - Direct"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trEQNoRecords" runat="server" visible="false">
        <td style="background-color: White; color: Black;" align="center" colspan="2">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
        </td>
    </tr>
    <tr id="trEQDate" runat="server" visible="false">
        <td style="background-color: White; color: Black;" align="center" colspan="2">
            <asp:Label ID="lblEQDate" runat="server" CssClass="FieldName" Text="Valuation Date"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr id="trEQData" runat="server">
        <td colspan="2">
            <%--Pie Chart Not Available!--%>
            <asp:Panel ID="pnlEquityDirect" runat="server">
                <asp:GridView ID="gvEquity" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" EnableViewState="false" AllowPaging="True" CssClass="GridViewStyle"
                    OnRowDataBound="gvEquity_RowDataBound" OnSorting="gvEquity_Sorting" Width="100%" ShowFooter="true">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="Script" HeaderText="Scrip"  />
                        <asp:BoundField DataField="NetHoldings" HeaderText="Net Holdings" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="AmortisedCost" HeaderText="Amortised Cost (Rs)" 
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" />
                        <asp:BoundField DataField="CurrentValue" HeaderText="CurrentValue (Rs)" 
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblGovHeader" runat="server" CssClass="HeaderTextSmall" Text="FI, Govt Savings, Insurance (Top 2 Each)"></asp:Label>
            &nbsp;<hr />
        </td>
    </tr>
    <tr id="trGovNoRecords" runat="server" visible="false">
        <td style="background-color: White; color: Black;" align="center" colspan="2">
            <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
        </td>
    </tr>
    <tr id="trGovData" runat="server">
        <td colspan="2">
            <asp:Panel ID="pnlFIGovIns" runat="server">
                <asp:GridView ID="gvFIGovIns" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" EnableViewState="false" AllowPaging="True" CssClass="GridViewStyle"
                    OnRowDataBound="gvFIGovIns_RowDataBound" OnSorting="gvFIGovIns_Sorting" ShowFooter="true">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="AssetType" HeaderText="Asset Type"  />
                        <asp:BoundField DataField="AssetParticulars" HeaderText="Asset Particulars"  />
                        <asp:BoundField DataField="PurchaseCost" HeaderText="Purchase Cost (Rs)" 
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" />
                        <asp:BoundField DataField="CurrentValue" HeaderText="Current Value (Rs)" 
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblOtherAssets" runat="server" CssClass="HeaderTextSmall" Text="Other Assets"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trOtherNoRecords" runat="server" visible="false">
        <td style="background-color: White; color: Black;" align="center" colspan="2">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
        </td>
    </tr>
    <tr id="trOtherData" runat="server">
        <td colspan="2">
            <asp:Panel ID="pnlOtherAssets" runat="server">
                <asp:GridView ID="gvOtherAssets" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" EnableViewState="false" CssClass="GridViewStyle"
                    OnRowDataBound="gvOtherAssets_RowDataBound" OnSorting="gvOtherAssets_Sorting" ShowFooter="true">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="AssetType" HeaderText="Asset Type"  />
                        <asp:BoundField DataField="AssetParticulars" HeaderText="Asset Particulars"  />
                        <asp:BoundField DataField="PurchaseCost" HeaderText="Purchase Cost (Rs)" 
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" />
                        <asp:BoundField DataField="CurrentValue" HeaderText="Current Value (Rs)" 
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HtmlEncode="false" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 100%;" colspan="2">
            <table style="width: 500px;">
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblTotalAssets" Text="Total Assets:" CssClass="HeaderTextSmall" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblTotalAssetsValue" CssClass="Field" runat="server"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <asp:Label ID="lblTotalLiabilities" Text="Total Liabilities:" CssClass="HeaderTextSmall" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        N/A
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblNetWorth" CssClass="HeaderTextSmall" Text="Net Worth:" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblNetWorthValue" CssClass="Field" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
