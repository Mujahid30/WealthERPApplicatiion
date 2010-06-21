<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMMultipleEqTransactionsView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.RMMultipleEqTransactionsView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="scptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<script type="text/javascript" language="javascript">
    function GetParentCustomerId(source, eventArgs) {
        document.getElementById("<%= txtParentCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
    function setPageType(pageType) {
        document.getElementById('<%= hdnDownloadPageType.ClientID %>').value = pageType;
    }
    function DownloadScript() {
        if (document.getElementById('<%= gvMFTransactions.ClientID %>') == null) {
            alert("No records to export");
            return false;
        }
        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click();
    }
</script>

<style type="text/css" media="print">
    ..noDisplay
    {
    }
    .noPrint
    {
        display: none;
    }
    .landScape
    {
        width: 100%;
        height: 100%;
        margin: 0% 0% 0% 0%;
        filter: progid:DXImageTransform.Microsoft.BasicImage(Rotation=3);
    }
    .pageBreak
    {
        page-break-before: always;
    }
</style>

<style type="text/css">
    .style2
    {
        width: 276px;
    }
    .style6
    {
        width: 323px;
    }
    .style7
    {
        width: 342px;
    }
    .style8
    {
        width: 348px;
    }
    .style9
    {
        width: 354px;
    }
</style>
<table style="width: 100%;">
    <tr>
        <td class="HeaderTextBig" colspan="2">
            <asp:Label ID="lblMfMIS" runat="server" CssClass="HeaderTextBig" Text="Equity Transactions View"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr runat="server" id="Tr2">
      <td>
        
            <asp:ImageButton ID="imgBtnExport" ImageUrl="~/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click" CausesValidation="false" 
                 />
                 
                 &nbsp;<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground" 
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();"
                PopupDragHandleControlID="Panel1">
            </ajaxToolkit:ModalPopupExtender>  
           
        </td>
    </tr>
    
    <tr>
        <td>
         <asp:Panel ID="Panel1" runat="server" Width="208px" Height="112px" BackColor="Wheat"
                BorderColor="AliceBlue" Font-Bold="true" ForeColor="Black">
                <br />
                &nbsp;&nbsp;
                <input id="rbtnSin" runat="server" name="Radio" onclick="setPageType('single')" type="radio" />
                <label for="rbtnSin" style="font-family: Times New Roman; font-size: medium; font-stretch: wider;
                    font-weight: 500">Current Page</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                &nbsp;&nbsp;
                <input id="Radio1" runat="server" name="Radio" onclick="setPageType('multiple')"
                    type="radio" />
                <label for="Radio1" style="font-family: Times New Roman; font-size: medium; font-stretch: wider;
                    font-weight: 500">All Pages</label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <br />
                <div align="center">
                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                </div>
            </asp:Panel>
            <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none" 
                OnClick="btnExportExcel_Click" Height="31px" Width="35px" />
        
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
        </td>
        &nbsp;
        <td>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        </td>
        <td>
            <asp:Label ID="lblCustomerGroup" runat="server" CssClass="FieldName" Text="Customer :"></asp:Label>
            &nbsp;
            <asp:RadioButton ID="rbtnAll" AutoPostBack="true" Checked="true" runat="server" GroupName="GroupAll"
                Text="All" CssClass="cmbField" OnCheckedChanged="rbtnAll_CheckedChanged" />
            <asp:RadioButton ID="rbtnGroup" AutoPostBack="true" runat="server" GroupName="GroupAll"
                Text="Group" CssClass="cmbField" OnCheckedChanged="rbtnAll_CheckedChanged" />
        </td>
    </tr>
</table>
<table>
    <tr id="trRange" visible="true" runat="server">
        <td align="right" valign="top">
            <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                Format="dd/MM/yyyy">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
            </ajaxToolkit:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right" valign="top">
            <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
        </td>
        <td valign="top">
            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                Format="dd/MM/yyyy">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
            </ajaxToolkit:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trPeriod" visible="false" runat="server">
        <td align="right" valign="top">
            <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                ValidationGroup="btnGo">
            </asp:CompareValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td id="tdGroupHead" runat="server" align="right" valign="top">
            <asp:Label ID="lblGroupHead" runat="server" CssClass="FieldName" Text="Group Head :"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtParentCustomer" runat="server" CssClass="txtField" AutoPostBack="true"
                AutoComplete="Off"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvGroupHead" ControlToValidate="txtParentCustomer"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a group head" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtParentCustomer_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtParentCustomer" WatermarkText="Type the Customer Name">
            </ajaxToolkit:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtParentCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtParentCustomer" ServiceMethod="GetParentCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetParentCustomerId" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio : "></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
                <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="PCGButton"
                ValidationGroup="btnGo" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMMultipleTransactionView_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMMultipleTransactionView_btnGo', 'S');" />
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
<html>
<body class="Landscape">
    <div id="tbl" runat="server">
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                    <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvMFTransactions" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CellPadding="4" EnableViewState="true" AllowPaging="false" CssClass="GridViewStyle"
                        ShowFooter="True" DataKeyNames="TransactionId" OnRowDataBound="gvMFTransactions_RowDataBound">
                        <FooterStyle CssClass="FooterStyle" />
                        <RowStyle CssClass="RowStyle" Wrap="False" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="center" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" Visible="false" />
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Customer Name">
                                <HeaderTemplate>
                                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtNameSearch" Text='<%# hdnCustomerNameSearch.Value %>' runat="server"
                                        CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMEQMultipleTransactionsView_btnCustomerSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNameHeader" runat="server" Text='<%# Eval("Customer Name").ToString() %>'
                                        ItemStyle-Wrap="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Scrip">
                                <HeaderTemplate>
                                    <asp:Label ID="lblScheme" runat="server" Text="Scrip"></asp:Label>
                                    <asp:TextBox Text='<%# hdnSchemeSearch.Value %>' ID="txtSchemeSearch" runat="server"
                                        CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMEQMultipleTransactionsView_btnScripSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeHeader" runat="server" Text='<%# Eval("Scrip").ToString() %>'
                                        ItemStyle-Wrap="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Trade Account">
                                <HeaderTemplate>
                                    <asp:Label ID="lblFolio" runat="server" Text="Trade Acc No"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTradeAccNumber" runat="server" Text='<%# Eval("TradeAccNumber").ToString() %>'
                                        ItemStyle-Wrap="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Type">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTranType" runat="server" Text="Type"></asp:Label>
                                    <asp:DropDownList ID="ddlTranType" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                        OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTranTypeHeader" runat="server" Text='<%# Eval("Type").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundField>
                            <asp:BoundField DataField="Rate" HeaderText="Rate(Per Unit)" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Brokerage" HeaderText="Brokerage" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OtherCharges" HeaderText="OtherCharges" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="STT" HeaderText="STT" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Gross Price">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGrossPrice" runat="server" Text="Gross Price"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblGrossPrice" runat="server" Text='' ItemStyle-Wrap="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Speculative Or Delivery" HeaderText="Speculative/Delivery">
                            </asp:BoundField>
                            <asp:BoundField DataField="Portfolio Name" HeaderText="Portfolio Name"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr id="trPager" runat="server">
                <td>
                    <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                </td>
            </tr>
            <tr id="trMessage" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found..."></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
<asp:Button ID="btnCustomerSearch" runat="server" Text="" OnClick="btnCustomerSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnScripSearch" runat="server" Text="" BorderStyle="None" BackColor="Transparent"
    OnClick="btnScripSearch_Click" />
<asp:HiddenField ID="hdnRecordCount" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnCustomerNameSearch" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeSearch" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranType" runat="server" Visible="false" />
<%--<asp:HiddenField ID="hdnFolioNumber" runat="server" Visible="false" />--%>
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
<asp:HiddenField ID="txtParentCustomerId" runat="server" />
