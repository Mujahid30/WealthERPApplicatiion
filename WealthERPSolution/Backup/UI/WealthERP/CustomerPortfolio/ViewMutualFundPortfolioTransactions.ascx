<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewMutualFundPortfolioTransactions.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewMutualFundPortfolioTransactions" %>


<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
            &nbsp;
        </td>
        <td align="left" class="HeaderTextBig">
            MF Portfolio Transactions Valuation View
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Select the Portfolio Name:"></asp:Label>
        </td>
        <td colspan="4" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblScripLabel" CssClass="FieldName" Text="Scheme Plan:" runat="server"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblScrip" CssClass="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCustomerLabel" CssClass="FieldName" Text="Customer:" runat="server"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCustomer" CssClass="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAccountLabel" CssClass="FieldName" Text="Folio:" runat="server"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblAccount" CssClass="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
           &nbsp;
                    
            <asp:LinkButton ID="lnkBack" runat="server" Text="Back" CssClass="LinkButtons" OnClick="lnkBack_Click"></asp:LinkButton>
        </td>
        <td>
            <table style="width: 105%">
                <tr>
                    <td class="leftField">
                        &nbsp;</td>
                    <td class="leftField">
                        &nbsp;</td>
                    <td class="leftField">
                        &nbsp;</td>
                </tr>
            </table>
        </td>
        
    </tr>
</table>
<div id="tbl" runat="server">
    <table>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:GridView ID="gvMFPortfolio" runat="server"  AutoGenerateColumns="False"
                    CellPadding="4" EnableViewState="false" AllowPaging="True" OnPageIndexChanging="gvMFPortfolio_PageIndexChanging"
                    CssClass="GridViewStyle"  ShowFooter="True"
                    OnDataBound="gvMFPortfolio_DataBound" OnRowDataBound="gvMFPortfolio_RowDataBound">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        
                        <asp:BoundField DataField="Transaction Type" HeaderText="Transaction Type" 
                            ItemStyle-HorizontalAlign="Center" /> 
                        <asp:BoundField DataField="Buy Date" HeaderText="Purchase Date" 
                            ItemStyle-HorizontalAlign="Right" />                      
                        <asp:BoundField DataField="Buy Quantity" HeaderText="Purchase Qty" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Buy Price" HeaderText="Purchase Price (Rs)" 
                            ItemStyle-HorizontalAlign="Right" />
                      <asp:BoundField DataField="Cost Of Acquisition" HeaderText="Cost Of Acquisition (Rs)"
                             ItemStyle-HorizontalAlign="Right" />
                       <asp:BoundField DataField="Sell Date" HeaderText="Sell Date " 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Sell Quantity" HeaderText="Sell Qty " 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Sell Price" HeaderText="Sell Price (Rs)" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Realized Sales Value" HeaderText="Realized Sales Value (Rs)"
                             ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Current NAV" HeaderText="Current NAV"
                             ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Current Value" HeaderText="Current Value"
                             ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="AgeOfInvestment" HeaderText="Age Of Inv (Days)" 
                            ItemStyle-HorizontalAlign="Right" />
                        
                        <asp:BoundField DataField="ActualPL" HeaderText="Actual P/L (Rs)" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="NotionalPL" HeaderText="Notional P/L (Rs)" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TotalPL" HeaderText="Total P/L (Rs)" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="AbsReturn" HeaderText="Abs Return" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="AnnReturn" HeaderText="Ann Return" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="STT" HeaderText="STT" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="NetSalesProceed" HeaderText="Net Sales Proceed" 
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="STCG" HeaderText="STCG"                             
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="LTCG" HeaderText="LTCG" 
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</div>
