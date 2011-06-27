<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityManualMultipleTransaction.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.EquityManualMultipleTransaction" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" tagprefix="ajaxToolkit"%>

<style type="text/css">
    .style1
    {
        width: 199px;
    }
    .style2
    {
        margin-left: 40px;
    }
    .style3
    {
        width: 142px;
    }
    .style5
    {
        width: 193px;
        height: 35px;
    }
    .style6
    {
        height: 35px;
        margin-left: 40px;
    }
    .style7
    {
    }
    .style8
    {
        width: 621px;
    }
    .style9
    {
        width: 137px;
        height: 35px;
    }
    .style10
    {
        width: 137px;
    }
</style>
<table style="width: 115%;">
    <tr>
        <td colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Equity Manual Multiple Transaction Entry Form"
                CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
    <td>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
            </Services>
        </asp:ScriptManager>
    </td>
    <tr>
        <td class="style3">
            <asp:Label ID="Label1" runat="server" Text="Scrip Particulars" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style1">
            <asp:TextBox ID="txtScrip" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxtoolkit:autocompleteextender runat="server" id="autoCompleteExtender" targetcontrolid="txtScrip"
                servicepath="AutoComplete.asmx" servicemethod="GetRMList" minimumprefixlength="1"
                enablecaching="true" />
        </td>
        <td class="style8">
            <asp:Button ID="btnSearch" runat="server" Style="margin-left: 0px" Text="Search"
                CssClass="ButtonField" Visible="False" />
        </td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;
        </td>
        <td class="style1">
            <asp:ListBox ID="lstScrip" runat="server" CssClass="cmbField" Visible="False"></asp:ListBox>
        </td>
        <td class="style8">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;
        </td>
        <td class="style1">
            &nbsp;
        </td>
        <td class="style8">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="3">
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                OnRowDataBound="GridView1_RowDataBound" Height="137px" Width="190px">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTotalBrokerage" runat="server">
                            </asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="3" align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAdd" runat="server" CssClass="ButtonField" Text="Add" OnClick="btnAdd_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnAddProrate" runat="server" CssClass="ButtonField" Text="Add Prorate"
                OnClick="btnAddProrate_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnSubmit_1" runat="server" CssClass="ButtonField" Text="Submit"
                OnClick="btnSubmit_1_Click" />
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="3" align="left">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="3" align="left">
            <div id="divEquityManualMultiple" runat="server">
                <table style="width: 526px">
                    <td class="style9" align="left">
                        &nbsp;
                    </td>
                    <td class="style5" align="left">
                        <asp:Label ID="Label3" runat="server" Text="Transaction Mode" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="style6" align="left">
                        <asp:DropDownList ID="ddlTransactionMode" runat="server" CssClass="cmbField">
                            <asp:ListItem>Delivery Trade</asp:ListItem>
                            <asp:ListItem>Speculation Trade</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label4" runat="server" Text="Exchange" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:DropDownList ID="ddlExchange" runat="server" CssClass="cmbField">
                <asp:ListItem>BSE</asp:ListItem>
                <asp:ListItem>NSE</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label5" runat="server" Text="Buy/Sell" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="cmbField">
                <asp:ListItem>Buy</asp:ListItem>
                <asp:ListItem>Sell</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label6" runat="server" Text="scrip Particulars" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:TextBox ID="txtScripParticular" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label7" runat="server" Text="Ticker" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:TextBox ID="txtTicker" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label9" runat="server" Text="Trade date" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:TextBox ID="txtTradeDate" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label10" runat="server" Text="No Of Shares" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:TextBox ID="txtNoOfShares" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label11" runat="server" Text="Rate" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:TextBox ID="txtRate" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label12" runat="server" Text="Broker" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:TextBox ID="txtBroker" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            <asp:Label ID="Label13" runat="server" Text="Brokerage" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            <asp:TextBox ID="txtBrokerage" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            &nbsp;
        </td>
        <td class="style2" align="left">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left">
            &nbsp;
        </td>
        <td class="style2" align="left">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style10" align="left">
            &nbsp;
        </td>
        <td class="style7" align="left" colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" CssClass="ButtonField" Text="Submit" OnClick="btnSubmit_Click" />
        </td>
    </tr>
</table>
</div> </td> </tr>
<tr>
    <td class="style2" colspan="3">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </td>
</tr>
<tr>
    <td class="style2" colspan="3">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </td>
</tr>
</table> 