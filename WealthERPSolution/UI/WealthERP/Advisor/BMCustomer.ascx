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
<table id="Table1" class="TableBackground" width="100%" runat="server">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Customer/Prospect List"></asp:Label>
        </td>
        <td width="50%" align="left">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
        <td align="right">
            <asp:ImageButton ID="imageExcel" 
                ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png" runat="server"
            AlternateText="Excel" ToolTip="Export to Excel" 
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
                OnClick="imageExcel_Click" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imageExcel" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();">
            </cc1:ModalPopupExtender>
            <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                BorderStyle="None" BackColor="Transparent" ToolTip="Print" />
        </td>
    </tr>
     <tr>
    <td>
    <hr />           
    </td>
    <td>
    <hr />           
    </td>
    <td>
    <hr />           
    </td>
    </tr>
</table>

<table class="TableBackground" width="100%" id="tblGv" runat="server">

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
        </td>
    </tr>
     </table>
<div id="tbl" runat="server">
    <table style="vertical-align: top;">
    <tr>
        <td style="vertical-align: top;">
            <asp:Label ID="lblChooseBr" runat="server" Font-Bold="true" Font-Size="Small" CssClass="FieldName" Text="Branch: "></asp:Label>
            <asp:DropDownList ID="ddlBMBranchList" CssClass="cmbField"  runat="server" onselectedindexchanged="ddlBMBranchList_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    </table>
    <table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" align="center" runat="server">
                <tr>
                    <td style="width: 100%" align="center">
                        <div class="failure-msg" style="text-align: center" align="center">
                            No Records found.....
                        </div>
                    </td>
                </tr>
    </table>
        
    
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td class="rightField">
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
                                <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" Width="80px" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                    <asp:ListItem Text="Select" />
                                    <asp:ListItem Text="Dashboard" />
                                    <asp:ListItem Text="Profile" />
                                    <asp:ListItem Text="Portfolio" />
                                   <%-- <asp:ListItem Text="User Details" />--%>
                                    <asp:ListItem Text="Alerts" />
                                    <asp:ListItem Text="Financial Planning" Value="FinancialPlanning" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Width="25px">
                            <HeaderTemplate>
                                <asp:Label ID="lblCustName" runat="server" Text="Name"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_BMCustomer_btnNameSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNameHeader" runat="server" Text='<%# Eval("Cust_Comp_Name").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="18px">
                            <HeaderTemplate>
                                <asp:Label ID="lblParent" runat="server" Text="Group"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlParent" AutoPostBack="true" CssClass="GridViewCmbField" runat="server" Width="170px" OnSelectedIndexChanged="ddlParent_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParentHeader" runat="server" Text='<%# Eval("Parent").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblPAN" runat="server" Text="PAN"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPAN" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_BMCustomer_btnPANSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPANHeader" runat="server" Text='<%# Eval("PAN Number").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="Phone Number" HeaderText="Phone" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="90px" />
                        <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-Width="250px"/>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="80px">
                            <HeaderTemplate>
                                <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtAreaSearch" runat="server" Width="120px" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_BMCustomer_btnAreaSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAreaHeader" runat="server" Text='<%# Eval("Area").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Width="170px">
                            <HeaderTemplate>
                                <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlCity" AutoPostBack="true" CssClass="GridViewCmbField"  Width="100px" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCityHeader" runat="server" Text='<%# Eval("City").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblAssignedRM" runat="server" Text="Assigned RM"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlAssignedRM" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlAssignedRM_SelectedIndexChanged">
                                </asp:DropDownList>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAssignedRMHeader" runat="server" 
                                    Text='<%# Eval("RMAssigned").ToString() %>'></asp:Label>
                                
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="Is Prospect" 
                            ItemStyle-Wrap="false">
                            <HeaderTemplate>
                             <asp:Label ID="lblHeaderddlIsProspect" runat="server" Text="Is Prospect"></asp:Label>
                             <br />
                             <asp:DropDownList ID="ddlIsProspect" runat="server" OnPreRender="SetValue" OnSelectedIndexChanged="ddlIsProspect_SelectedIndexChanged" AutoPostBack="true" CssClass="GridViewCmbField">
                                    <asp:ListItem Text="All" Value="2">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1">
                                    </asp:ListItem>
                                    <asp:ListItem Text="No" Value="0">
                                    </asp:ListItem>
                             </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblddlIsProspect" runat="server" Text='<%# Eval("IsProspect").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Panel>
</div>
<table width="100%">
    <tr id="trPager" style="text-align: center" runat="server">
        <td colspan="2">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>

<asp:Button ID="btnPANSearch" runat="server" Text="" OnClick="btnPANSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
    
<asp:Button ID="btnPincodeSearch" runat="server" Text="" OnClick="btnPincodeSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnAreaSearch" runat="server" Text="" OnClick="btnAreaSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
    
<asp:HiddenField ID="hndPAN" runat="server" Visible="false" />    
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
<asp:HiddenField ID="hdnIsProspect" runat="server" Visible="false" />