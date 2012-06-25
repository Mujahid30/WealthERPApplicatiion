<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioFixedIncomeView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioFixedIncomeView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_PortfolioFixedIncomeView_hdnMsgValue").value = 1;
            document.getElementById("ctrl_PortfolioFixedIncomeView_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_PortfolioFixedIncomeView_hdnMsgValue").value = 0;
            document.getElementById("ctrl_PortfolioFixedIncomeView_hiddenassociation").click();
            return true;
        }
    }
</script>

<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record has been deleted Successfully.
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Fixed Income Portfolio"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <%--  <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found...!"></asp:Label>
        </td>--%>
    </tr>
    <%--<tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td>
            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td>
            <%--<asp:GridView ID="gvFixedIncomePortfolio" runat="server" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" DataKeyNames="FITransactionId" AllowSorting="True"
                OnSorting="gvFixedIncomePortfolio_Sorting" OnDataBound="gvFixedIncomePortfolio_DataBound"
                ShowFooter="True" 
             >
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange" CssClass="GridViewCmbField">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="Delete" Value="Delete">Delete</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Instrument Category" 
                        ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="Particulars"  ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Purchase Date" HeaderText="Purchase Date (dd/mm/yyyy)"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Date" HeaderText="Maturity Date (dd/mm/yyyy)"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Deposit Amount" HeaderText="Deposit Amount/ purchase Cost (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Interest Rate" HeaderText="Interest Rate (%)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Current Value" HeaderText="Current Value (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Value" HeaderText="Maturity Value (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>--%>
            <telerik:RadGrid ID="gvFixedIncomePortfolio" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false" OnNeedDataSource="gvFixedIncomePortfolio_OnNeedDataSource"
                >
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="FixedIncomeList">
                </ExportSettings>
                <MasterTableView DataKeyNames="FITransactionId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn ItemStyle-Width="80Px" HeaderText="Issue Code" AllowFiltering="false">
                            <ItemTemplate>
                                <telerik:RadComboBox ID="ddlAction" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange"
                                    CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                    AllowCustomText="true" Width="120px" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                        </telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                            runat="server"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                            runat="server"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Delete" Value="Delete"
                                            runat="server"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Category" AllowFiltering="false" HeaderText="Instrument Category"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" AllowFiltering="false" HeaderText="Particulars"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Purchase Date" AllowFiltering="false" HeaderText="Purchase Date (dd/mm/yyyy)"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Maturity Date" AllowFiltering="false" HeaderText="Maturity Date (dd/mm/yyyy)"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Deposit Amount" AllowFiltering="false" HeaderText="Deposit Amount/ purchase Cost (Rs)"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Interest Rate" DataFormatString="{0:dd/MM/yyyy}"
                            AllowFiltering="false" HeaderText="Interest Rate (%)" UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Current Value" DataFormatString="{0:dd/MM/yyyy}"
                            AllowFiltering="false" HeaderText="Current Value (Rs)" UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Maturity Value" DataFormatString="{0:dd/MM/yyyy}"
                            AllowFiltering="false" HeaderText="Maturity Value (Rs)" UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<%--<table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>--%>
<asp:HiddenField ID="hdnSort" runat="server" Value="Name ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />