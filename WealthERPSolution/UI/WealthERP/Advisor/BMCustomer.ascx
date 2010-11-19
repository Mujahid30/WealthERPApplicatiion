<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BMCustomer.ascx.cs"
    Inherits="WealthERP.Advisor.BMCustomer" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<style type="text/css">
    .style1
    {
        height: 30px;
    }
</style>

<script language="javascript" type="text/javascript">

    function DownloadScript() {

        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click(
        );

    }
    function setPageType(pageType) {
        document.getElementById('<%= hdnDownloadPageType.ClientID %>').value = pageType;

    }
    function setFormat(format) {
        document.getElementById('<%= hdnDownloadFormat.ClientID %>').value = format;
    }
    function Print_Click(div, btnID) {


        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=200,height=100,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();

        var btn = document.getElementById(btnID);
        btn.click();
    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function getScrollBottom(p_oElem) {
        return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
    }

</script>

<table class="TableBackground" width="100%" id="tblGv" runat="server">
<tr>
<td>
            <asp:ImageButton ID="imageExcel" 
                ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png" runat="server"
            AlternateText="Excel" ToolTip="Export to Excel" 
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
                OnClick="imageExcel_Click" />

            <%--<asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" />--%>
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imageExcel" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();">
            </cc1:ModalPopupExtender>
            <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                BorderStyle="None" BackColor="Transparent" ToolTip="Print" />
        </td>
        </tr>
         <tr id="Tr1" runat="server">
        <td>
            <asp:Panel ID="Panel1" runat="server" CssClass="ExortPanelpopup" style="display:none">
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <input id="rbtnSin" runat="server" name="Radio" onclick="setPageType('single')" type="radio" />
                        </td>
                        <td align="left">
                            <label for="rbtnSin" style="color: Black; font-family: Verdana; font-size: 8pt; text-decoration: none">
                                Current Page</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <input id="Radio1" runat="server" name="Radio" onclick="setPageType('multiple')"
                                type="radio" />
                        </td>
                        <td align="left">
                            <label for="Radio1" style="color: Black; font-family: Verdana; font-size: 8pt; text-decoration: none">
                                All Pages</label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                        </td>
                        <td align="left">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
           <asp:Button CssClass="ExportButton" ID="btnExportExcel" Style="display: none" 
                runat="server" Height="31px" Width="35px" onclick="btnExportExcel_Click2" />
           
            <%--<asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none" 
                Height="31px" Width="35px" onclick="btnExportExcel_Click1" />--%>
        </td>
    </tr>

    <%--<tr id="Tr1" runat="server" visible="true">
        <td>
            <asp:RadioButton ID="rbtnExcel" Text="Excel" runat="server" GroupName="grpExport"
                CssClass="cmbField" />
            <asp:RadioButton ID="rbtnPDF" Text="PDF" runat="server" GroupName="grpExport" CssClass="cmbField" />
            <asp:RadioButton ID="rbtnWord" Text="Word" runat="server" GroupName="grpExport" CssClass="cmbField" />
        </td>
        <td>
        </td>
    </tr>--%>
    <%--<tr>
        <td class="style1">
            <asp:RadioButton ID="rbtnSingle" Text="Current Page" runat="server" GroupName="grpPage"
                CssClass="cmbField" AutoPostBack="true" OnCheckedChanged="rbtnSingle_CheckedChanged" />
            <asp:RadioButton ID="rbtnMultiple" Text="All Pages" runat="server" GroupName="grpPage"
                CssClass="cmbField" AutoPostBack="true" OnCheckedChanged="rbtnMultiple_CheckedChanged" />
        </td>
        <td>
            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export"
                CssClass="ButtonField" />
            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" CssClass="ButtonField" />
            <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>--%>
        
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
     </table>
<div id="tbl" runat="server">
    <table>
    <tr>
        <td>
            <asp:Label ID="lblChooseBr" runat="server" Font-Bold="true" Font-Size="Small" CssClass="FieldName" Text="Branch: "></asp:Label>
            <asp:DropDownList ID="ddlBMBranchList" CssClass="cmbField"  runat="server" onselectedindexchanged="ddlBMBranchList_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <table id="ErrorMessage" width="100%" align="center" runat="server">
                <tr>
                    <td style="width: 100%">
                        <div class="failure-msg" style="text-align: center" align="center">
                            No Customers found for selected Branch...
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
        <tr>
            <td class="rightField" colspan="2">
                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    CssClass="GridViewStyle" DataKeyNames="CustomerId,UserId" OnSelectedIndexChanged="gvCustomers_SelectedIndexChanged"
                    AllowSorting="True" HorizontalAlign="Center" OnSorting="gvCustomers_Sort" GridLines="Both" Width="1480px">
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="25px">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" Width="80px" CssClass="cmbField" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                    <asp:ListItem Text="Select" />
                                    <asp:ListItem Text="Dashboard" />
                                    <asp:ListItem Text="Profile" />
                                    <asp:ListItem Text="Portfolio" />
                                    <asp:ListItem Text="User Details" />
                                    <asp:ListItem Text="Alerts" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Width="25px">
                            <HeaderTemplate>
                                <asp:Label ID="lblCustName" runat="server" Text="Name"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_BMCustomer_btnNameSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNameHeader" runat="server" Text='<%# Eval("Cust_Comp_Name").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="18px">
                            <HeaderTemplate>
                                <asp:Label ID="lblParent" runat="server" Text="Group"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlParent" AutoPostBack="true" CssClass="cmbField" runat="server" Width="170px" OnSelectedIndexChanged="ddlParent_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParentHeader" runat="server" Text='<%# Eval("Parent").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="Phone Number" HeaderText="Phone" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="90px" />
                        <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-Width="250px"/>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="80px">
                            <HeaderTemplate>
                                <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtAreaSearch" runat="server" Width="120px" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_BMCustomer_btnAreaSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAreaHeader" runat="server" Text='<%# Eval("Area").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="170px">
                            <HeaderTemplate>
                                <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlCity" AutoPostBack="true" CssClass="cmbField"  Width="100px" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCityHeader" runat="server" Text='<%# Eval("City").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
<table width="100%">
    <tr id="trPager" style="text-align: center" runat="server">
        <td colspan="2">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:Button ID="btnPincodeSearch" runat="server" Text="" OnClick="btnPincodeSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnAreaSearch" runat="server" Text="" OnClick="btnAreaSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnPincodeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAreaFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCityFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnParentFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRMFilter" runat="server" Visible="false" />
<%--For BM--%>
<asp:HiddenField ID="hndBranchID" runat="server" Visible="false" />
<asp:HiddenField ID="hndBranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hndAll" runat="server" Visible="false" />
<%-- End For BM--%>
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />