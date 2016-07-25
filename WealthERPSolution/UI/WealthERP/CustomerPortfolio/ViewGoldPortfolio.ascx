<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewGoldPortfolio.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewGoldPortfolio" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_ViewGoldPortfolio_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewGoldPortfolio_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewGoldPortfolio_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewGoldPortfolio_hiddenassociation").click();
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
<table class="TableBackground" style="width: 100%">
    <tr>
        <td colspan="4" class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Gold"></asp:Label>
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
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField" runat="server" visible="false">
            <asp:Label ID="lblCurrentPage" Visible="false" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" Visible="false" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            
            <telerik:RadGrid ID="gvGoldPortfolio" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" ExportSettings-FileName="Gold Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="GoldNPId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange"
                                    Width="110px">
                                    <Items>
                                        <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                        <asp:ListItem Text="View" Value="View" />
                                        <asp:ListItem Text="Edit" Value="Edit" />
                                        <asp:ListItem Text="Delete" Value="Delete" />
                                    </Items>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Instrument Category" DataField="Instrument Category"
                            UniqueName="Instrument Category" SortExpression="Instrument Category " AutoPostBackOnFilter="true"
                            AllowFiltering="true"  ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Purchase Date (dd/mm/yyyy)" DataField="Purchase Date"
                            UniqueName="Purchase Date" DataFormatString="{0:dd-MMM-yy}" SortExpression="Purchase Date" AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Purchase Value" SortExpression="Purchase Value"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" AllowFiltering="true"
                            HeaderText="Purchase Value (Rs)" UniqueName="Purchase Value" FooterStyle-HorizontalAlign="Right"   Aggregate="Sum" DataFormatString="{0:N0}" 
                            ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Current Value" SortExpression="Current Value"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" AllowFiltering="true"
                            HeaderText="Current Value (Rs)" UniqueName="Current Value" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" DataFormatString="{0:N0}" 
                            ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="EqualTo" AllowFiltering="true" HeaderText="Remarks" UniqueName="Remarks"
                             ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
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
<div id="DivPager" runat="server" visible="false" style="display: none">
    <table style="width: 100%">
        <tr>
            <td>
                <Pager:Pager ID="mypager" Visible="false" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" visible="false" runat="server" align="center">
            </div>
        </td>
    </tr>
</table>
<tr>
    <asp:Button ID="btnUpdateNP" runat="server" Text="Update Current Value" OnClick="btn_Update"
        CssClass="PCGLongButton" AutoPostBack="true" />
</tr>
<asp:HiddenField ID="hdnSort" runat="server" Value="InstrumentCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />