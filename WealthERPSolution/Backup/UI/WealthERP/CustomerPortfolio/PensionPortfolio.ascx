<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PensionPortfolio.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioViewDashboard" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_PensionPortfolio_hdnMsgValue").value = 1;
            document.getElementById("ctrl_PensionPortfolio_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_PensionPortfolio_hdnMsgValue").value = 0;
            document.getElementById("ctrl_PensionPortfolio_hiddenassociation").click();
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
<table style="width: 100%;">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblHeader" Text="Pension & Gratuities Portfolio" runat="server" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
        <td align="right">
            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                Height="20px" Width="25px" OnClick="btnExportFilteredData_OnClick"></asp:ImageButton>
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
    </tr>
    <tr id="trTotalRows" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" Visible="false" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" Visible="false" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <telerik:RadGrid ID="gvrPensionAndGratuities" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" OnNeedDataSource="gvrPensionAndGratuities_OnNeedDataSource"  AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" ExportSettings-FileName="Pension And Gratuities Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="PortfolioId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" Width="110px">
                                    <Items>
                                        <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                        <asp:ListItem Text="View" Value="View" />
                                        <asp:ListItem Text="Edit" Value="Edit" />
                                        <asp:ListItem Text="Delete" Value="Delete" />
                                    </Items>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Organization Name" DataField="Organization Name"
                            UniqueName="Organization Name" SortExpression="Organization Name " AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            FooterText="Grand Total:">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Category" DataField="Category" UniqueName="Category"
                            SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Deposit Amount" SortExpression="Deposit Amount"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" AllowFiltering="true"
                            HeaderText="Deposit Amount (Rs)" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"
                            UniqueName="Deposit Amount" Aggregate="sum" ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Current Value" SortExpression="Current Value"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" AllowFiltering="true"
                            HeaderText="Current Value (Rs)" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"
                            UniqueName="Current Value" Aggregate="Sum" ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Nominee Name" DataField="NName"
                            UniqueName="NName" SortExpression="NName" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn HeaderText="JointHolder Name" DataField="JntName" UniqueName="JntName"
                            SortExpression="JntName" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains">
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
<table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" Visible="false" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="InstrumentCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnPortfolioID" runat="server" />
<asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />