<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioProperty.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioProperty" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_PortfolioProperty_hdnMsgValue").value = 1;
            document.getElementById("ctrl_PortfolioProperty_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_PortfolioProperty_hdnMsgValue").value = 0;
            document.getElementById("ctrl_PortfolioProperty_hiddenassociation").click();
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

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                           Property Portfolio
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnExportFilteredData" Visible="true" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                OnClick="btnExportFilteredData_OnClick" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" Visible="false" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
</table>





   <asp:Panel ID ="pnl" runat="server"   ScrollBars="Vertical">
<table width="100%" cellspacing="0" cellpadding="3">
    <tr>
        <td colspan="3">
            <telerik:RadGrid ID="gvrProperty" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" ExportSettings-Excel-Format="ExcelML" OnNeedDataSource="gvrProperty_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="Portfolio Property">
                </ExportSettings>
                <MasterTableView DataKeyNames="PropertyId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" Width="110px">
                                    <Items>
                                       <asp:ListItem>Select </asp:ListItem>
                                        <asp:ListItem Text="View" Value="View" />
                                        <asp:ListItem Text="Edit" Value="Edit" />
                                        <asp:ListItem Text="Delete" Value="Delete" />
                                    </Items>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Asset Category" DataField="Sub Category" UniqueName="Sub Category"
                            SortExpression="Sub Category" AutoPostBackOnFilter="true" AllowFiltering="true"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Particulars" DataField="Particulars" UniqueName="Particulars"
                            SortExpression="Particulars" AutoPostBackOnFilter="true" AllowFiltering="true"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="City" SortExpression="City" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="EqualTo" AllowFiltering="true" HeaderText="City" UniqueName="City"
                            ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Area" SortExpression="Area" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="EqualTo" AllowFiltering="true" HeaderText="Area" UniqueName="Area"
                            ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Measurement Unit" SortExpression="Measurement Unit"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" AllowFiltering="true"
                            HeaderText="Measurement Unit" UniqueName="Measurement Unit" ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Purchase Date" SortExpression="Purchase Date"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" AllowFiltering="true"
                            HeaderText="Purchase Date(dd/mm/yyyy)" UniqueName="Purchase Date" DataFormatString="{0:dd-MMM-yy}"
                            ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Purchase Cost" SortExpression="Purchase Cost"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" AllowFiltering="true"
                            HeaderText="Purchase Cost (Rs)" UniqueName="Purchase Cost" ShowFilterIcon="false"
                            FooterStyle-HorizontalAlign="Right"  DataFormatString="{0:N0}" Aggregate="Sum" >
                            <ItemStyle Width="110px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Current Value" SortExpression="Current Value"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Aggregate="Sum" AllowFiltering="true"
                            HeaderText="Current Value (Rs)" UniqueName="Current Value" ShowFilterIcon="false"
                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"  >
                            <ItemStyle Width="110px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn HeaderText="Nominee Name" DataField="NName"
                            UniqueName="NName" SortExpression="NName" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="JointHolder Name" DataField="JntName"
                            UniqueName="JntName" SortExpression="JntName" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
</asp:Panel>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" Visible="false" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="SubCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />