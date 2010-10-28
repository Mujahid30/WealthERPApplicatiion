<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomer.ascx.cs"
    Inherits="WealthERP.RMCustomer" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
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
    function DownloadScript() {
        if (document.getElementById('<%= gvCustomers.ClientID %>') == null) {
            alert("No records to export");
            return false;
        }
        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click();
    }
    function setPageType(pageType) {
        document.getElementById('<%= hdnDownloadPageType.ClientID %>').value = pageType;

    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
    //***********************************

    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this profile?');
  
        if (bool) {            
            document.getElementById("ctrl_RMCustomer_hdnMsgValue").value = 1;

            document.getElementById("ctrl_RMCustomer_hiddenassociation").click();

            return false;
        }
        else {
            document.getElementById("ctrl_RMCustomer_hdnMsgValue").value = 0;
            document.getElementById("ctrl_RMCustomer_hiddenassociation").click();
            return true;
        }
    }

    //***********************************************


    function showassocation() {

        var bool = window.confirm('Customer has associations,cannot be deteted');
        if (bool) {           
            document.getElementById("ctrl_RMCustomer_hdnassociation").value = 1;
            document.getElementById("ctrl_RMCustomer_hiddenassociationfound").click();
            return false;
        }
        else {
            document.getElementById("ctrl_RMCustomer_hdnassociation").value = 0;
            document.getElementById("ctrl_RMCustomer_hiddenassociationfound").click();
            return true;
        }
    }
</script>

<table id="Table1" class="TableBackground" width="100%" runat="server">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Customer List"></asp:Label>
            <hr />
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
<%--<table class="TableBackground" width="100%" id="tblGv" runat="server">
    <tr id="Tr1" runat="server" visible="true">
        <td>
            <asp:RadioButton ID="rbtnExcel" Text="Excel" runat="server" GroupName="grpExport"
                CssClass="cmbField" />
            <asp:RadioButton ID="rbtnPDF" Text="PDF" runat="server" GroupName="grpExport" CssClass="cmbField" />
            <asp:RadioButton ID="rbtnWord" Text="Word" runat="server" GroupName="grpExport" CssClass="cmbField" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
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
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
</table>--%>
<div id="tbl" runat="server">
    <table>
        <tr id="trModalPopup" runat="server">
            <td>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                    TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground"
                    Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();"
                    PopupDragHandleControlID="Panel1" X="280" Y="35">
                </cc1:ModalPopupExtender>
            </td>
        </tr>
        <tr id="trExportPopup" runat="server">
            <td>
                <asp:Panel ID="Panel1" runat="server" CssClass="ExortPanelpopup" style="display:none">
                    <br />
                    <table width="100%">
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="right">
                                <input id="rbCurrent" runat="server" name="Radio" onclick="setPageType('single')"
                                    type="radio" />
                            </td>
                            <td align="left">
                                <label for="rbCurrent" style="color: Black; font-family: Verdana; font-size: 8pt;
                                    text-decoration: none">
                                    Current Page</label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="right">
                                <input id="rbAll" runat="server" name="Radio" onclick="setPageType('multiple')" type="radio" />
                            </td>
                            <td align="left">
                                <label for="rbAll" style="color: Black; font-family: Verdana; font-size: 8pt; text-decoration: none">
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
                    OnClick="btnExportExcel_Click" Height="31px" Width="30px" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table style="width: 100%; border: none; margin: 0px; padding: 0px;" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgBtnExport" ImageUrl="../App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="leftField" align="right">
                            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="rightField" colspan="2">
                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ShowFooter="true" CssClass="GridViewStyle" DataKeyNames="CustomerId,UserId" OnSelectedIndexChanged="gvCustomers_SelectedIndexChanged"
                    AllowSorting="True" HorizontalAlign="Center" OnSorting="gvCustomers_Sort">
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange"
                                    CssClass="GridViewCmbField">
                                    <asp:ListItem Text="Select" />
                                    <asp:ListItem Text="Dashboard" />
                                    <asp:ListItem Text="Profile" />
                                    <asp:ListItem Text="Portfolio" />
                                    <asp:ListItem Text="User Details" />
                                    <asp:ListItem Text="Alerts" />
                                    <asp:ListItem Text="Delete Profile"/>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblCustName" runat="server" Text="Name"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMCustomer_btnNameSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNameHeader" runat="server" Text='<%# Eval("Cust_Comp_Name").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblParent" runat="server" Text="Group"></asp:Label>
                                <asp:DropDownList ID="ddlParent" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlParent_SelectedIndexChanged"
                                    CssClass="GridViewCmbField">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParentHeader" runat="server" Text='<%# Eval("Parent").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Parent" HeaderText="Parent" SortExpression="Parent" />--%>
                        <asp:BoundField DataField="PAN Number" HeaderText="PAN Number" />
                        <asp:BoundField DataField="Mobile Number" HeaderText="Mobile Number" />
                        <asp:BoundField DataField="Phone Number" HeaderText="Phone Number" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-Wrap="false" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                                <asp:TextBox ID="txtAreaSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMCustomer_btnAreaSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAreaHeader" runat="server" Text='<%# Eval("Area").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Area" HeaderText="Area" />--%>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                <asp:DropDownList ID="ddlCity" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                                    CssClass="GridViewCmbField">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCityHeader" runat="server" Text='<%# Eval("City").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="City" HeaderText="City" />--%>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblPincode" runat="server" Text="Pincode"></asp:Label>
                                <asp:TextBox ID="txtPincodeSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMCustomer_btnPincodeSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPincodeHeader" runat="server" Text='<%# Eval("Pincode").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Pincode" HeaderText="Pincode" />--%>
                         <asp:TemplateField HeaderText="IsActive">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsActive" runat="server" 
                                            Text='<%#Eval("IsActive") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:DropDownList ID="ddlActiveFilter" runat="server" AutoPostBack="true" 
                                            CssClass="cmbField"  
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
            </td>
        </tr>
        <tr id="trPager" runat="server">
            <td align="center" colspan="6">
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:Button ID="btnPincodeSearch" runat="server" Text="" OnClick="btnPincodeSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnAreaSearch" runat="server" Text="" OnClick="btnAreaSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
 <asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click" 
                BorderStyle="None" Visible="true" BackColor="Transparent"  />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnPincodeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAreaFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCityFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnParentFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnactive" runat="server" Visible="false" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdnassociation" runat="server" Visible="true" />
<asp:HiddenField ID="hdnassociationcount" runat="server" />