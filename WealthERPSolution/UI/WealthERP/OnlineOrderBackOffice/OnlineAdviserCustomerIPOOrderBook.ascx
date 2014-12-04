<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineAdviserCustomerIPOOrderBook.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineAdviserCustomerIPOOrderBook" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            IPO Order Book
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExport_OnClick"
                                Height="25px" Width="25px" Visible="false"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div id="divConditional" runat="server" style="padding-top: 4px">
    <table class="TableBackground" cellpadding="2">
        <tr>
            <td id="td1" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Order Status:" ID="Label1"></asp:Label>
                <asp:DropDownList CssClass="cmbField" ID="ddlOrderStatus" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblIssueName" runat="server" class="FieldName" Text="Issue Name:"></asp:Label>
                <asp:DropDownList CssClass="cmbField" ID="ddlIssueName" runat="server">
                </asp:DropDownList>
            </td>
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server">
                <telerik:RadDatePicker ID="txtOrderFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="dvTransactionDate" runat="server" class="dvInLine">
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtOrderFrom"
                        ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtOrderFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtOrderTo" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="Div1" runat="server" class="dvInLine">
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrderTo"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtOrderTo" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtOrderTo"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtOrderFrom" CssClass="cvPCG" ValidationGroup="btnViewOrder"
                    Display="Dynamic">
                </asp:CompareValidator>
            </td>
            <td id="tdBtnOrder" runat="server">
                <asp:Button ID="btnViewOrder" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnViewOrder"
                    OnClick="btnViewOrder_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="99%" ScrollBars="Horizontal"
    Visible="false">
    <table id="tblCommissionStructureRule" runat="server">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvIPOOrderBook" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="gvIPOOrderBook_OnNeedDataSource"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" Width="120%" Height="400px" OnItemDataBound="gvIPOOrderBook_ItemDataBound"
                                OnUpdateCommand="gvIPOOrderBook_UpdateCommand">
                                <MasterTableView DataKeyNames="CO_OrderId,AIM_IssueId,AIM_IssueName,Amount,WES_Code,C_CustCode,WOS_OrderStep,AIM_IsCancelAllowed"
                                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None"
                                    EditMode="PopUp">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="AllDetailslink" runat="server" CommandName="ExpandAllCollapse"
                                                    Font-Underline="False" Font-Bold="true" UniqueName="AllDetailslink" Font-Size="Medium"
                                                    OnClick="btnExpand_Click">+</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                    Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CustomerName" SortExpression="CustomerName" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Customer Name" UniqueName="CustomerName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C_PANNum" SortExpression="C_PANNum" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="PAN" UniqueName="C_PANNum">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C_CustCode" SortExpression="C_CustCode" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Client Id" UniqueName="C_CustCode">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IssueName" SortExpression="AIM_IssueName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Scrip Name" UniqueName="AIM_IssueName">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="AIM_IssueId" SortExpression="AIM_IssueId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            AllowFiltering="true" HeaderText="Transaction Date" UniqueName="CO_OrderDate"
                                            SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                               
                                        <telerik:GridBoundColumn DataField="CO_OrderId" HeaderText="Transaction No." SortExpression="CO_OrderId"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="60px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_ApplicationNumber" AllowFiltering="true" HeaderText="Application No."
                                            UniqueName="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                            FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_OpenDate" AllowFiltering="true" HeaderText="Start Date"
                                            UniqueName="AIM_OpenDate" SortExpression="AIM_OpenDate" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                            FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_CloseDate" AllowFiltering="true" HeaderText="End Date"
                                            UniqueName="AIM_CloseDate" SortExpression="AIM_CloseDate" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                            FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="PAG_AssetGroupCode" AllowFiltering="true"
                                            HeaderText="Application No." UniqueName="PAG_AssetGroupCode" SortExpression="PAG_AssetGroupCode"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            HeaderStyle-Width="80px" FilterControlWidth="60px">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Amount" AllowFiltering="true" HeaderText="Amount Invested"
                                            UniqueName="Amount" CurrentFilterFunction="Contains" HeaderStyle-Width="77px"
                                            SortExpression="Amount" ShowFilterIcon="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BidAmount" AllowFiltering="true" HeaderText="Bid Amount"
                                            UniqueName="BidAmount" CurrentFilterFunction="Contains" HeaderStyle-Width="77px"
                                            SortExpression="BidAmount" ShowFilterIcon="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WOS_OrderStep" AllowFiltering="true" HeaderText="Status"
                                            HeaderStyle-Width="70px" UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Bidding_Exchange" AllowFiltering="false" HeaderText="Bidding Exchange"
                                            HeaderStyle-Width="70px" UniqueName="Bidding_Exchange" SortExpression="Bidding_Exchange"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="CO_CreatedBy" SortExpression="CO_CreatedBy" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Created By" UniqueName="CO_CreatedBy">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_CreatedOn" SortExpression="CO_CreatedOn" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                            HeaderStyle-Width="160px" HeaderText="Created On" UniqueName="CO_CreatedOn">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_ModifiedBy" SortExpression="CO_ModifiedBy"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Modified By" UniqueName="CO_ModifiedBy">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_ModifiedOn" SortExpression="CO_ModifiedOn"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Modified On" UniqueName="CO_ModifiedOn">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CustomerAssociate" SortExpression="CustomerAssociate"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="User Type" UniqueName="CustomerAssociate">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IsCancelAllowed" AllowFiltering="false" HeaderText="Is cancel"
                                            HeaderStyle-Width="70px" UniqueName="AIM_IsCancelAllowed" SortExpression="AIM_IsCancelAllowed"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COS_Reason" AllowFiltering="false" HeaderText="Reject Reason"
                                            HeaderStyle-Width="70px" UniqueName="COS_Reason" SortExpression="COS_Reason"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="MarkAsReject"
                                            EditText="Cancel" CancelText="Cancel" UpdateText="OK" HeaderText="Cancel">
                                        </telerik:GridEditCommandColumn>
                                        <%-- <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="100px" UniqueName="Action"
                                            HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgCancel" runat="server" CommandName="Update" ImageUrl="~/Images/Cancel.jpg"
                                                    ToolTip="Cancel" />&nbsp;
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%">
                                                        <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                            Width="100%" ScrollBars="Both" Visible="false">
                                                            <telerik:RadGrid ID="gvIPODetails" runat="server" AutoGenerateColumns="False" enableloadondemand="True"
                                                                PageSize="10" AllowPaging="false" EnableEmbeddedSkins="False" GridLines="None"
                                                                ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                                                                AllowFilteringByColumn="false">
                                                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId,CO_OrderId"
                                                                    AutoGenerateColumns="false" Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="COID_IssueBidNo" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Bid Option" UniqueName="COID_IssueBidNo"
                                                                            SortExpression="COID_IssueBidNo">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="COID_Price" SortExpression="COID_Price" AutoPostBackOnFilter="true"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                                            HeaderText="Price" UniqueName="COID_Price" HeaderStyle-Width="77px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="COID_Quantity" SortExpression="COID_Quantity"
                                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AllowFiltering="true" HeaderText="Quantity" UniqueName="COID_Quantity" HeaderStyle-Width="55px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="COID_AmountPayable" AllowFiltering="false" SortExpression="COID_AmountPayable"
                                                                            HeaderText="Amount Invested" UniqueName="COID_AmountPayable" HeaderStyle-Width="65px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="BidAmount" AllowFiltering="true" HeaderText="Bid Amount"
                                                                            UniqueName="BidAmount" CurrentFilterFunction="Contains" HeaderStyle-Width="77px"
                                                                            SortExpression="BidAmount" ShowFilterIcon="false">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="PriceDiscountType" AllowFiltering="false" SortExpression="PriceDiscountType"
                                                                            HeaderText="Discount Type" UniqueName="PriceDiscountType" HeaderStyle-Width="65px">
                                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings FormTableStyle-Height="40%" EditFormType="Template" FormMainTableStyle-Width="300px"
                                        CaptionFormatString=" Order canceling Request">
                                        <FormTemplate>
                                            <table style="background-color: White;" border="0">
                                                <tr>
                                                    <td class="leftField">
                                                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Request No.:"></asp:Label>
                                                    </td>
                                                    <td class="rightField">
                                                        <asp:TextBox ID="txtRejOrderId" runat="server" CssClass="txtField" Style="width: 250px;"
                                                            Text='<%# Bind("CO_OrderId") %>' ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftField">
                                                        <asp:Label ID="Label20" runat="server" Text="Remark:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightField">
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="txtField" Style="width: 250px;"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <%--  <td colspan="2">
                                                    &nbsp;
                                                </td>--%>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="Button1" Text="OK" runat="server" CssClass="PCGButton" CommandName="Update"
                                                            ValidationGroup="btnSubmit">
                                                            <%-- OnClientClick='<%# (Container is GridEditFormInsertItem) ?  " javascript:return ShowPopup();": "" %>'--%>
                                                        </asp:Button>
                                                        <%--</td>
                                                    <td  >--%>
                                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                            CommandName="Cancel"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnOrderStatus" runat="server" />
