﻿ <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewGovtSavings.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewGovtSavings" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<%--<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>--%>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_ViewGovtSavings_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewGovtSavings_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewGovtSavings_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewGovtSavings_hiddenassociation").click();
            return true;

        }
    }
</script>

<%--<table style="width: 100%">
 <tr>
        <td class="HeaderCell" colspan="4">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Government Savings"></asp:Label>
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
       <%-- <td colspan="2">
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found!"></asp:Label>
        </td>--%>
<%-- </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>--%>
<%--<tr>
        <td class="leftField">
            <asp:GridView ID="gvrGovtSavings" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="GovtSavingId" AllowPaging="True" CssClass="GridViewStyle"
                OnRowDataBound="gvrGovtSavings_RowDataBound" OnSorting="gvrGovtSavings_Sorting"
                OnDataBound="gvrGovtSavings_DataBound" ShowFooter="True">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" 
                                CssClass="GridViewCmbField">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="Delete" Value="Delete">Delete</asp:ListItem>
                            </asp:DropDownList>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Category" 
                        ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Particulars" HeaderText="Particulars" 
                        ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Deposit Date" HeaderText="Deposit Date (dd/mm/yyyy)" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Date" HeaderText="Maturity Date (dd/mm/yyyy)" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Deposit Amount" HeaderText="Deposit Amount (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Rate Of Interest" HeaderText="Rate Of Interest (%)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Current Value" HeaderText="Current Value (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Value" HeaderText="Maturity Value (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Government Savings
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnExportFilteredData" Visible="false" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
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
</table>
<table width="100%" cellspacing="0" cellpadding="3">
    <tr>
        <td>           
            <telerik:RadGrid ID="gvGovtSavings" runat="server" AllowAutomaticDeletes="false"
                EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                ShowStatusBar="false" ShowFooter="true" AllowPaging="true" AllowSorting="true" OnNeedDataSource="gvGovtSavings_OnNeedDataSource"
                Skin="Telerik" GridLines="none" AllowAutomaticInserts="false" OnItemCommand="gvGovtSavings_ItemCommand">               
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="GovtSavingId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                            HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                    CssClass="cmbField" Width="80px">
                                    <asp:ListItem>Select </asp:ListItem>
                                    <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                    <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                    <asp:ListItem Text="Delete" Value="Delete">Delete</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Category" UniqueName="Category" HeaderText="Category"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="125px"
                            SortExpression="Category" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Particulars" UniqueName="Particulars" HeaderText="Particulars"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="160px"
                            SortExpression="Particulars" CurrentFilterFunction="Contains" FilterControlWidth="95px">
                            <ItemStyle Width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Deposit Date" UniqueName="Deposit Date" HeaderText="Deposit Date (dd/mm/yyyy)" DataFormatString="{0:d}"
                            AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="145px" HorizontalAlign="Right" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Maturity Date" UniqueName="Maturity Date" HeaderText="Maturity Date (dd/mm/yyyy)"
                            SortExpression="Maturity Date" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:d}"
                            AllowFiltering="true" HeaderStyle-Width="100px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="Right" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Deposit Amount" UniqueName="Deposit Amount" HeaderText="Deposit Amount (Rs)"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="105px" DataFormatString="{0:n0}"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="67px" HorizontalAlign="Right" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Rate Of Interest" UniqueName="Rate Of Interest" FilterControlWidth="60px"
                            HeaderText="Rate Of Interest (%)" AutoPostBackOnFilter="true" ShowFilterIcon="false" 
                            AllowFiltering="true" HeaderStyle-Width="76px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="Right" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Current Value" UniqueName="Current Value" HeaderText="Current Value (Rs)"
                            AutoPostBackOnFilter="true" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderStyle-Width="115px">
                            <ItemStyle Width="140px" HorizontalAlign="Right" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Maturity Value" UniqueName="Maturity Value" HeaderText="Maturity Value (Rs)"
                            AllowFiltering="false" HeaderStyle-Width="100px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemStyle Width="80px" HorizontalAlign="Right" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Resizing AllowColumnResize="true" />
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
            <%--  </div>--%>
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
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record has been deleted Successfully.
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="CGSNP_CreatedOn DESC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
