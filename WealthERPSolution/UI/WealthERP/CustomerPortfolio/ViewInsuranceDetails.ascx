<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewInsuranceDetails.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewInsuranceDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script language="javascript" type="text/javascript">
    function Print_Click(div, btnID) {
        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=200,height=100,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        // myWindowToPrint.document.write(ContentToPrint.innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();
        var btn2 = document.getElementById(btnID);
        btn2.click();
    }
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this policy?');

        if (bool) {
            document.getElementById("ctrl_ViewInsuranceDetails_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewInsuranceDetails_hiddenassociation").click();

            return false;
        }
        else {
            document.getElementById("ctrl_ViewInsuranceDetails_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewInsuranceDetails_hiddenassociation").click();
            return true;
        }
    }
</script>

<table width="100%">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" class="HeaderTextBig" runat="server" Text="Life Insurance Portfolio"></asp:Label>
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
        <td>
            <br />
        </td>
    </tr>
</table>
<table id="ErrorMessage" align="center" runat="server">
    <tr>
        <td>
            <div class="failure-msg" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadGrid ID="gvrLifeInsurance" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                OnNeedDataSource="gvrLifeInsurance_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="InsuranceId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn ItemStyle-Width="80Px" AllowFiltering="false">
                            <ItemTemplate>
                                <telerik:RadComboBox ID="ddlMenu" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                    CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                    AllowCustomText="true" Width="120px" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                        </telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                            runat="server"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                            runat="server"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete"
                                            runat="server"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Category" AllowFiltering="false" HeaderText="Category"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Particulars" AllowFiltering="false" HeaderText="Particulars"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sum Assured" AllowFiltering="false" HeaderText="Sum Assured"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Premium Amount" AllowFiltering="false" HeaderText="Premium Amount"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Commencement Date" AllowFiltering="false" HeaderText="Commencement Date"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Maturity Value" AllowFiltering="false" HeaderText="Maturity Value"
                            UniqueName="ActiveLevel">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Maturity Date" AllowFiltering="false" HeaderText="Maturity Date"
                            UniqueName="ActiveLevel">
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
<%--<table class="TableBackground" width="100%">
    <tr id="trPager" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="gvrLifeInsurance" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="InsuranceId" EnableViewState="true" CssClass="GridViewStyle"
                OnSorting="gvrLifeInsurance_Sorting" OnDataBound="gvrLifeInsurance_DataBound"
                ShowFooter="True">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" Wrap="false" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Delete" Value="Delete">Delete</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Particulars" HeaderText="Particulars" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Sum Assured" HeaderText="Sum Assured (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Premium Amount" HeaderText="Premium Amount (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Commencement Date" HeaderText="Commencement Date (dd/mm/yyyy)"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Value" HeaderText="Maturity Value (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Date" HeaderText="Maturity Date (dd/mm/yyyy)"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>--%>
<%--<table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>--%>
<asp:HiddenField ID="hdnSort" runat="server" Value="Particulars ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />
