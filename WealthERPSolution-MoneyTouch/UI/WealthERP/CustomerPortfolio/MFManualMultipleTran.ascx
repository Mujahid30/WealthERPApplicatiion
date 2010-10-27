<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFManualMultipleTran.ascx.cs" Inherits="WealthERP.CustomerPortfolio.MFManualMultipleTran" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" tagprefix="ajaxToolkit"%>

<style type="text/css">
    .style1
    {
    }
    .style2
    {
        width: 83px;
    }
    .style3
    {
        width: 231px;
    }
</style>
<table style="width:100%;">
    <tr>
        <td colspan="2" align="center">
            <asp:Label ID="Label1" runat="server" 
                Text="MF Transaction Manual Multiple Entry Form" CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>

                <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />

            </Services>
            </asp:ScriptManager>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2" align="left">
            <asp:Label ID="Label2" runat="server" Text="Scheme" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoCompleteExtender" TargetControlID="txtSchemeSearch" ServicePath="AutoComplete.asmx" ServiceMethod="GetRMList" MinimumPrefixLength="1" EnableCaching="true" />
        </td>
    </tr>
    <tr>
        <td class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1" colspan="2">
            <asp:GridView ID="gvMFTransactions" runat="server" CellPadding="4" 
                ForeColor="#333333" GridLines="None">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="style1" colspan="2" align="center">
            <asp:Button ID="btnAddTransaction" runat="server" Text="Add" 
                CssClass="ButtonField" onclick="btnAddTransaction_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="ButtonField" 
                onclick="btnSubmit_Click"/>
        </td>
    </tr>
</table>
<p>
    <br />
</p>
<div id="divTransaction" runat="server">
    <table style="width:50%;" align="center">
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label3" runat="server" Text="Scheme Name" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtSearchName" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label4" runat="server" Text="Transaction Type" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="cmbField" AutoPostBack="true"
                onselectedindexchanged="ddlTransactionType_SelectedIndexChanged">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Sell</asp:ListItem>
                <asp:ListItem>Buy</asp:ListItem>
                <asp:ListItem>Divident Reinvestment</asp:ListItem>
                <asp:ListItem>Divident Payout</asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label6" runat="server" Text="Transaction Date" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:DropDownList ID="ddlTransactionDateDay" runat="server" Height="22px" 
                Width="50px" CssClass="cmbField">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
                <asp:ListItem>24</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>26</asp:ListItem>
                <asp:ListItem>27</asp:ListItem>
                <asp:ListItem>28</asp:ListItem>
                <asp:ListItem>29</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>31</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlTransactionDateMonth" runat="server" Height="22px" Width="50px" 
                CssClass="cmbField">
                <asp:ListItem>Jan</asp:ListItem>
                <asp:ListItem>Feb</asp:ListItem>
                <asp:ListItem>Mar</asp:ListItem>
                <asp:ListItem>Apr</asp:ListItem>
                <asp:ListItem>May</asp:ListItem>
                <asp:ListItem>Jun</asp:ListItem>
                <asp:ListItem>Jul</asp:ListItem>
                <asp:ListItem>Aug</asp:ListItem>
                <asp:ListItem>Sept</asp:ListItem>
                <asp:ListItem>Oct</asp:ListItem>
                <asp:ListItem>Nov</asp:ListItem>
                <asp:ListItem>Dec</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlTransactionDateYear" runat="server" Height="22px" 
                Width="58px" CssClass="cmbField">
                <asp:ListItem>2006</asp:ListItem>
                <asp:ListItem>2007</asp:ListItem>
                <asp:ListItem>2008</asp:ListItem>
                <asp:ListItem>2009</asp:ListItem>
                <asp:ListItem>2010</asp:ListItem>
                <asp:ListItem>2011</asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label7" runat="server" Text="Divident Rate" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtDividentRate" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label8" runat="server" Text="NAV" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtNAV" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label18" runat="server" Text="Price" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtPrice" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label9" runat="server" Text="Amount" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label10" runat="server" Text="Units" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtUnits" runat="server" CssClass="txtField" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" class="style3">
            <asp:Label ID="Label11" runat="server" Text="STT"  CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="ButtonField" 
                    onclick="btnAdd_Click" />
            </td>
        </tr>
    </table>
</div>

