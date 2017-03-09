<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewGeneralInsuranceDetails.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewGeneralInsuranceDetails" %>
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
            document.getElementById("ctrl_ViewGeneralInsuranceDetails_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewGeneralInsuranceDetails_hiddenassociation").click();

            return false;
        }
        else {
            document.getElementById("ctrl_ViewGeneralInsuranceDetails_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewGeneralInsuranceDetails_hiddenassociation").click();
            return true;
        }
    }
</script>
<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">General Insurance</td>
        <td  align="right" id="tdExport" runat="server" style="padding-bottom:2px;">
        <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png" Visible="false"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                    OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
    </td>
        </tr>
    </table>
</div>
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
<asp:Panel ID="Panel1" runat="server" class="Landscape" Width="99%" ScrollBars="Horizontal">
    <table width="99%">
        <%--    <tr id="trPager" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>--%>
        <tr id="trExportFilteredData" runat="server">
            <td>
                
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvAll" runat="server" style="width: 640px">
                    <telerik:RadGrid ID="gvGeneralInsurance" runat="server" GridLines="None" AutoGenerateColumns="False"
                        Width="100%" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false" OnNeedDataSource="gvGeneralInsurance_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="GI Details">
                        </ExportSettings>
                        <MasterTableView AllowFilteringByColumn="false" DataKeyNames="InsuranceId,AccountId"
                            Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
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
                                 <telerik:GridBoundColumn DataField="PolicyNo" AllowFiltering="false" HeaderText="Policy No."
                                    UniqueName="PolicyNo">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="InsCompany" AllowFiltering="false" HeaderText="Insurer"
                                    UniqueName="ActiveLevel">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="scheme" AllowFiltering="false" HeaderText="Policy Name"
                                    UniqueName="ActiveLevel">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Category" AllowFiltering="false" FooterText="Grand Total:"
                                    HeaderText="Category" UniqueName="ActiveLevel">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SubCategory" AllowFiltering="false" HeaderText="Insurance Type"
                                    UniqueName="ActiveLevel">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nominee Name" DataField="NName"
                                   UniqueName="NName" SortExpression="NName" AutoPostBackOnFilter="true" AllowFiltering="true"
                                   ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                    DataField="InsuredAmount" AllowFiltering="false" HeaderText="Sum Assured" UniqueName="ActiveLevel"
                                    Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PremiumAmount" AllowFiltering="false" HeaderText="Premium Amount"
                                    UniqueName="ActiveLevel" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                    Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="frequency" AllowFiltering="false" HeaderText="Premium Frequency"
                                    UniqueName="frequency" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CommencementDate" DataFormatString="{0:dd/MM/yyyy}"
                                    AllowFiltering="false" HeaderText="Commencement Date" UniqueName="ActiveLevel">
                                    <ItemStyle Width="" HorizontalAlign="center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MaturityDate" DataFormatString="{0:dd/MM/yyyy}"
                                    AllowFiltering="false" HeaderText="Policy Renewal date" UniqueName="ActiveLevel">
                                    <ItemStyle Width="" HorizontalAlign="center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<%-- <asp:GridView ID="gvGeneralInsurance" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="InsuranceId" EnableViewState="true" CssClass="GridViewStyle" AllowPaging="true"
                ShowFooter="True" OnPageIndexChanging="gvGeneralInsurance_PageIndexChanging" PageSize="20">
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
                    <asp:BoundField DataField="InsCompany" HeaderText="Ins Company" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="SubCategory" HeaderText="Sub Category" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="InsuredAmount" HeaderText="Insured Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PremiumAmount" HeaderText="Premium Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CommencementDate" HeaderText="Commencement Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MaturityDate" HeaderText="Maturity Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>--%>
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
