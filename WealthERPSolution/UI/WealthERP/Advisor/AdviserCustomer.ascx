<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserCustomer.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserCustomer" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

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

<style>
    .HeaderStyle1
    {
        background-image: url(Images/PCGGridViewHeaderGlass2.jpg);
        background-position: center;
        position: relative;
        background-repeat: repeat-x;
        vertical-align: top;
        top: expression(this.offsetParent.scrollTop-3);
    }
</style>
<table id="Table1" class="TableBackground" width="100%" runat="server">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Customer/Prospect List"></asp:Label>
                                <hr />
        </td>
    </tr>
</table>
<table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage1" runat="server" visible="true" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<table id="tblExport" class="TableBackground" width="100%" runat="server" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" align="left">
        <asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" />
        </td>
        <td width="50%" align="left">
        
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
       
        </td>
    </tr>
</table>
<asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">      
        
        <tr>
            <td class="rightField" width="100%">
                <asp:GridView ID="gvCustomers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" CssClass="GridViewStyle" DataKeyNames="CustomerId,UserId,RMId"
                    OnSelectedIndexChanged="gvCustomers_SelectedIndexChanged" OnSorting="gvCustomers_Sort"
                    ShowFooter="true" ShowHeader="true" width="100%">
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" 
                        VerticalAlign="Top" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                    <asp:ListItem Text="Select" Value="Select" />
                                    <asp:ListItem Text="Dashboard" Value="Dashboard" />
                                    <asp:ListItem Text="Profile" Value="Profile" />
                                    <asp:ListItem Text="Portfolio" Value="Portfolio" />
                                   <%-- <asp:ListItem Text="User Details" Value="User Details" />--%>
                                    <asp:ListItem Text="Alerts" Value="Alerts" />
                                    <asp:ListItem Text="Financial Planning" Value="FinancialPlanning" />
                                </asp:DropDownList>
                            </ItemTemplate>
                            <%-- <FooterTemplate>
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
                            </FooterTemplate>--%>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Parent" HeaderText="Parent" SortExpression="Parent" ItemStyle-Wrap="false" />--%>
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblCustName" runat="server" Text="Name"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnNameSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNameHeader" runat="server" 
                                    Text='<%# Eval("Cust_Comp_Name").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="true">
                            <HeaderTemplate>
                                <asp:Label ID="lblParent" runat="server" Text="Group"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlParent" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlParent_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParenteHeader" runat="server" 
                                    Text='<%# Eval("Parent").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="PAN" HeaderStyle-Wrap="false" HeaderText="PAN Number" />--%>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblPAN" runat="server" Text="PAN"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPAN" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnPANSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPANHeader" runat="server" 
                                    Text='<%# Eval("PAN Number").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Mobile Number" HeaderText="Mobile Number" />
                        <asp:BoundField DataField="Phone Number" HeaderText="Phone Number" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Address" HeaderText="Address" 
                            ItemStyle-Wrap="false" />
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtAreaSearch" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnAreaSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAreaHeader" runat="server" 
                                    Text='<%# Eval("Area").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Area" HeaderText="Area" />--%>
                        <asp:BoundField DataField="City" HeaderText="City" />
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblPincode" runat="server" Text="Pincode"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPincodeSearch" runat="server" CssClass="GridViewTxtField" 
                                    
                                    onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomer_btnPincodeSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPincodeHeader" runat="server" 
                                    Text='<%# Eval("Pincode").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                       <%-- <asp:BoundField DataField="IsFPClient" HeaderText="Is FPClient" />--%>
                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="Is Prospect" 
                            ItemStyle-Wrap="false">
                            <HeaderTemplate>
                             <asp:Label ID="lblHeaderIsProspect" runat="server" Text="Is Prospect"></asp:Label>
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
                                <asp:Label ID="lblIsFpClient" runat="server" Text='<%# Eval("IsProspect").ToString() %>'></asp:Label>
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
                                    Text='<%# Eval("Assigned RM").ToString() %>'></asp:Label>
                                
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Assigned RM" HeaderText="Assigned RM" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" />--%>
                        <asp:TemplateField HeaderText="IsActive">
                            <ItemTemplate>
                                <asp:Label ID="lblIsActive" runat="server" Text='<%#Eval("IsActive") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <br />
                                <asp:DropDownList ID="ddlActiveFilter" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlActiveFilter_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="2">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1">
                                    </asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="0">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            <%--<hr />
        </td>
    </tr>
</table>
<table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage1" runat="server" visible="true" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<table id="tblExport" class="TableBackground" width="100%" runat="server" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" align="left">
        <asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" />
        </td>
        <td width="50%" align="left">
        
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
       
        </td>
    </tr>
</table>
<asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">      
        
        <tr>
            <td class="rightField" width="100%">--%>
                &nbsp;</td>
        </tr>        
    </table>
</asp:Panel>
<table id="tblpager" class="TableBackground" width="100%" runat="server">
    <tr id="trPager" runat="server">
        <td width="100%" align="center">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>        
     </tr>
</table>


<table class="TableBackground" width="100%" id="tblGV" runat="server" cellspacing="0" cellpadding="0">
    <tr>
        <td>
            
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();">
            </cc1:ModalPopupExtender>
            <%--<asp:ImageButton ID="imgBtnWord" ImageUrl="~/App_Themes/Maroon/Images/Export_Word.png"
                runat="server" AlternateText="Word" ToolTip="Export To Word" OnClick="imgBtnWord_Click"
                OnClientClick="setFormat('word')" />
            <asp:ImageButton ID="imgBtnPdf" ImageUrl="~/App_Themes/Maroon/Images/Export_Pdf.png"
                runat="server" AlternateText="PDF" OnClientClick="setFormat('pdf')" ToolTip="Export To PDF"
                OnClick="imgBtnPdf_Click" />
            <asp:ImageButton ID="imgBtnPrint" ImageUrl="~/App_Themes/Maroon/Images/Print.png"
                runat="server" AlternateText="Print" OnClientClick="setFormat('print')" ToolTip="Print"
                OnClick="imgBtnPrint_Click" />--%>
            <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                BorderStyle="None" BackColor="Transparent" ToolTip="Print" />
        </td>
    </tr>
    <tr id="Tr1" runat="server">
        <td>
            <%--<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="150px">
                <input type="radio" id="rbtnSin" runat="server" name="Radio" onclick="setPageType('single')" />
                <label for="rbtnSin" class="cmbField">Current Page</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <input type="radio" id="Radio1" runat="server" name="Radio" onclick="setPageType('multiple')" />
                <label for="Radio1" class="cmbField">All Pages</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <div align="center">
                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                </div>--%>
            <asp:Panel ID="Panel1" runat="server" CssClass="ExortPanelpopup" Style="display: none">
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
            <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none"
                OnClick="btnExportExcel_Click" Height="31px" Width="35px" />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found..."></asp:Label>
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
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnPincodeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hndPAN" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAreaFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRMFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnParentFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnReassignRM" runat="server" Visible="false" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
<asp:HiddenField ID="hdnactive" runat="server" Visible="false" />
<asp:HiddenField ID="hdnIsProspect" runat="server" Visible="false" />
