<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PortfolioWebpart.ascx.cs" Inherits="PortfolioWebpart" %>
<table width="360px" cellpadding="4" cellspacing="0">
    <tr>
        <td align="left" valign="top">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataSourceID="portfoliodata" AllowPaging="True" Height="279px" AllowSorting="True" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <Columns>
                    <asp:BoundField DataField="TICKER_NAME" HeaderText="Ticker_Name" SortExpression="TICKER_NAME" >
                        <HeaderStyle Font-Size="Small" Wrap="False" />
                        
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Left"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="EXPOSURE" HeaderText="Exp" SortExpression="EXPOSURE" DataFormatString="{0:f2}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="INSTR" HeaderText="Instr" SortExpression="INSTR">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ACTION" HeaderText="Act" SortExpression="ACTION">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QUANTITY" HeaderText="Qty" SortExpression="QUANTITY" DataFormatString="{0:f0}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AVERAGE_PRICE" HeaderText="Avg_Prc" SortExpression="AVERAGE_PRICE" DataFormatString="{0:f2}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CASH_INVESTED" HeaderText="Cash_Invstd" SortExpression="CASH_INVESTED" DataFormatString="{0:f2}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MTM" HeaderText="Mkt_Prc" SortExpression="MTM" DataFormatString="{0:f2}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MTM_CASH" HeaderText="Mkt_Val" SortExpression="MTM_CASH" DataFormatString="{0:f2}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PROFIT_LOSS" HeaderText="PnL" SortExpression="PROFIT_LOSS" DataFormatString="{0:f2}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BOOKED_P_L_THOUSANDS" HeaderText="Bkd_PnL"
                        SortExpression="BOOKED_P_L_THOUSANDS" DataFormatString="{0:f2}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TOTAL_P_L" HeaderText="Tot_PnL" SortExpression="TOTAL_P_L" DataFormatString="{0:f2}" HtmlEncode="False">
                        <HeaderStyle Font-Size="Small" />
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DATE" HeaderText="DATE" SortExpression="DATE" Visible="False">
                        <HeaderStyle Font-Size="Small" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            </asp:GridView>
            <asp:SqlDataSource ID="portfoliodata" runat="server" ConnectionString="<%$ ConnectionStrings:tcg_dbConnectionString %>"
                SelectCommand="SELECT TICKER.TICKER_NAME, NP_EOD_DATA.EXPOSURE, NP_EOD_DATA.INSTR, NP_EOD_DATA.ACTION, NP_EOD_DATA.QUANTITY, NP_EOD_DATA.AVERAGE_PRICE, NP_EOD_DATA.CASH_INVESTED, NP_EOD_DATA.MTM, NP_EOD_DATA.MTM_CASH, NP_EOD_DATA.PROFIT_LOSS, NP_EOD_DATA.BOOKED_P_L_THOUSANDS, NP_EOD_DATA.TOTAL_P_L, NP_EOD_DATA.DATE FROM NP_EOD_DATA INNER JOIN TICKER ON NP_EOD_DATA.TICKER_ID = TICKER.TICKER_ID WHERE (NP_EOD_DATA.DATE = '08-01-2008')">
            </asp:SqlDataSource>
           
        </td>
    </tr>
</table>