<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioPersonal.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioPersonal" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_PortfolioPersonal_hdnMsgValue").value = 1;
            document.getElementById("ctrl_PortfolioPersonal_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_PortfolioPersonal_hdnMsgValue").value = 0;
            document.getElementById("ctrl_PortfolioPersonal_hiddenassociation").click();
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
        <td>
            <asp:Label ID="lblHeader" class="HeaderTextBig" runat="server" Text="Personal Portfolio"></asp:Label>
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
        <%--<td>
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found...!"></asp:Label>
        </td>--%>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" Visible="false" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" Visible="false" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <telerik:RadGrid ID="gvrPersonal" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" ExportSettings-FileName="Personal Portfolio Details"
                OnNeedDataSource="gvrPersonal_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="PersonalId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                         <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                                                    <ItemTemplate>
                                                           <asp:DropDownList ID="ddlMenu" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                                                Width="110px">
                                                                <Items>
                                                                    <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                                                    <asp:ListItem Text="View" Value="View" />
                                                                    <asp:ListItem Text="Edit" Value="Edit"/> 
                                                                   <asp:ListItem Text="Delete" Value="Delete" />

                                                                </Items>
                                                            </asp:DropDownList>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Name" DataField="Name" UniqueName="Name" SortExpression="Name"
                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Category" DataField="Category" UniqueName="Category"
                            SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Quantity" SortExpression="Quantity" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="EqualTo" AllowFiltering="true" HeaderText="Unit" UniqueName="Quantity"
                            DataFormatString="{0:dd-MMM-yy}" ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Current Value (Rs)" DataField="Current Value"
                            UniqueName="CateCurrent Valuegory" SortExpression="Current Value" AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Purchase Value (Rs)" DataField="Purchase Value"
                            UniqueName="Purchase Value" SortExpression="Purchase Value" AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Purchase Date (dd/mm/yyyy)" DataField="Purchase Date"
                            UniqueName="Purchase Date" SortExpression="Purchase Date" AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
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
<table class="TableBackground" width="100%">
    <tr id="trPager" runat="server" visible="false">
        <td align="center" colspan="3">
            <Pager:Pager ID="mypager" Visible="false" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="Name ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
<asp:HiddenField ID="hdnPersonalId" runat="server" />
