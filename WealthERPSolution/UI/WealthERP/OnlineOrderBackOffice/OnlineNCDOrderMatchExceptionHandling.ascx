<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineNCDOrderMatchExceptionHandling.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineNCDOrderMatchExceptionHandling" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>--%>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 25%;
        text-align: right;
    }
    .rightData
    {
        width: 20%;
        text-align: left;
    }
    .txtField
    {
        margin-left: 0px;
    }
</style>
<table width="100%">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            NCD/IPO Allotment
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnNcdIpoExport" runat="server" ImageUrl="~/Images/Export_Excel.png"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnNcdIpoExport_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr id="trOrderDates" runat="server" visible="false">
        <td align="right">
            <asp:Label ID="lblFrom" runat="server" Text=" Order From Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rvFromdate" ControlToValidate="txtFromDate" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a  Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="btnGo"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG" ControlToValidate="txtFromDate"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnGo" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblTo" runat="server" Text="Order To Date: " CssClass="FieldName"></asp:Label>
        </td>
        <%-- </td>
        <td class="rightData"  >--%>
        <td>
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rvtoDate" ControlToValidate="txtToDate" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="btnGo"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="rfvPCG" ControlToValidate="txtToDate"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnGo" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
            <%--<asp:CompareValidator ID="cvtodate" runat="server" ErrorMessage="<br/>To Date should not less than From Date"
                Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label2" runat="server" Text="Product:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="FI">BOND</asp:ListItem>
                <asp:ListItem Value="IP">IPO</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Product Type"
                CssClass="rfvPCG" ControlToValidate="ddlProduct" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            &nbsp;&nbsp
            <asp:Label ID="lb1BChannel" runat="server" Text="Business Channel:" CssClass="FieldName" Visible="false"></asp:Label>
        </td>
        <%--  </td>
        <td class="rightLabelData">--%>
        <td>
            <asp:DropDownList ID="ddlBChannnel" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBChannnel_SelectedIndexChanged" Width="150px" Visible="false">
            </asp:DropDownList>
            <%-- <span id="Span10" class="spnRequiredField">*</span>--%>
            <%--<span id="Span20" class="spnRequiredField">*</span>--%>
            <br />
            <asp:RequiredFieldValidator ID="REqBChannel" runat="server" ErrorMessage="Please Select Business Channel"
                CssClass="rfvPCG" ControlToValidate="ddlBChannnel" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select" Visible="false"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="lblType" runat="server" Text="Select Type" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Online" Value="1"></asp:ListItem>
                <asp:ListItem Text="Offline" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td id="tdlblSubCategory" runat="server" visible="false">
            <asp:Label ID="Label1" runat="server" Text="Select Category" CssClass="FieldName"></asp:Label>
        </td>
        <td id="tdSubCategory" runat="server" visible="false">
            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                Width="150px" OnSelectedIndexChanged="ddlSubCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <%-- <span id="Span10" class="spnRequiredField">*</span>--%>
            <span id="Span6" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                CssClass="rfvPCG" ControlToValidate="ddlSubCategory" ValidationGroup="btnGo"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            &nbsp;&nbsp
            <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: " CssClass="FieldName"
                Visible="false"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged" Visible="false">
            </asp:DropDownList>
            <span id="Span5" class="spnRequiredField" visible="false"></span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select Status"
                CssClass="rfvPCG" ControlToValidate="ddlOrderStatus" ValidationGroup="btnGo"
                Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trOrderStatus" runat="server">
        <td align="right">
            <asp:Label ID="lb1Issue" runat="server" Text="Issue:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:DropDownList ID="ddlIssue" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                Width="300px">
            </asp:DropDownList>
            <span id="Span10" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Issue"
                CssClass="rfvPCG" ControlToValidate="ddlIssue" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
        <%-- <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>--%>
    </tr>
</table>
<asp:Panel ID="PnlMultiSeries" runat="server" ScrollBars="Horizontal" Visible="false"
    CssClass="table">
    <table width="100%" align="left">
        <tr>
            <td>
                <telerik:RadGrid ID="RadMultiSeries" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false" AllowAutomaticInserts="false"
                    ExportSettings-FileName="NCD Order Recon" OnNeedDataSource="RadMultiSeries_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="Order" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,WOS_OrderStepCode,AIM_IssueId" Width="100%"
                        AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CO_OrderId" HeaderText="Order No" SortExpression="CO_OrderId"
                                ShowFilterIcon="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left" AllowSorting="True">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--    <telerik:GridTemplateColumn DataField="CO_OrderId" HeaderText="Order/Transaction No."
                                SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkOrderNo" Font-Underline="false" runat="server" CssClass="cmbFielde" Text='<%# Eval("CO_OrderId") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="OrderDetID" AllowFiltering="false" DataField="COID_DetailsId"
                                Visible="false">
                                <ItemStyle />
                            </telerik:GridTemplateColumn>
                            <telerik:GridDateTimeColumn DataField="CO_OrderDate" HeaderText="Order Date" SortExpression="CO_OrderDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_OrderDate" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer" SortExpression="Customer_Name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNo" HeaderText="Application No"
                                SortExpression="CO_ApplicationNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CO_ApplicationNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderText="Status" SortExpression="WOS_OrderStep"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="WOS_OrderStep" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridTemplateColumn DataField="WOS_OrderStep" HeaderText="Status" SortExpression="WOS_OrderStep"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="WOS_OrderStep" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderStep" runat="server" Text='<%#Eval("WOS_OrderStep").ToString() %>'> </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn DataField="CEDA_DPId" HeaderText="Dp Id" SortExpression="CEDA_DPId"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CEDA_DPId" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderdQuantity" HeaderText="Qty" SortExpression="OrderdQuantity"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="OrderdQuantity" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderText="Face value" SortExpression="AIM_FaceValue"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AIM_FaceValue" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="purchaseAmt" HeaderText="Purchase Amt" SortExpression="purchaseAmt"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="purchaseAmt" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_Price" HeaderText="Price" SortExpression="COID_Price"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="COID_Price" FooterStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_AllotmentDate" HeaderText="Allotment Date"
                                SortExpression="AIM_AllotmentDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AIM_AllotmentDate" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotmentQty" HeaderText="Alloted Qty" SortExpression="AllotmentQty"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AllotmentQty" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn Visible="false" DataField="AIA_Price" HeaderText="Alloted Price"
                                SortExpression="AIA_Price" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AIA_Price" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="AllotmentAmt" HeaderText="Alloted Amt" SortExpression="AllotmentAmt"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AllotmentAmt" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COAD_CertificateNo" HeaderText="Certificate No"
                                SortExpression="COAD_CertificateNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COAD_CertificateNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_SeriesQuantity01" HeaderText="SeriesQuantity01"
                                SortExpression="COID_SeriesQuantity01" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity01" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId01" HeaderText="Sequence Id1"
                                SortExpression="AID_SeqId01" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId01" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_SeriesQuantity02" HeaderText="SeriesQuantity02"
                                SortExpression="COID_SeriesQuantity02" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity02" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId02" HeaderText="Sequence Id2"
                                SortExpression="AID_SeqId02" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId02" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_SeriesQuantity03" HeaderText="SeriesQuantity03"
                                SortExpression="COID_SeriesQuantity03" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity03" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId03" HeaderText="Sequence Id3"
                                SortExpression="AID_SeqId03" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId03" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="COID_SeriesQuantity04" HeaderText="SeriesQuantity04"
                                SortExpression="COID_SeriesQuantity04" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity04" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId04" HeaderText="Sequence Id4"
                                SortExpression="AID_SeqId04" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId04" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="COID_SeriesQuantity05" HeaderText="SeriesQuantity05"
                                SortExpression="COID_SeriesQuantity05" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity05" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId05" HeaderText="Sequence Id5"
                                SortExpression="AID_SeqId05" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId05" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_SeriesQuantity06" HeaderText="SeriesQuantity06"
                                SortExpression="COID_SeriesQuantity06" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity06" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId06" HeaderText="Sequence Id6"
                                SortExpression="AID_SeqId06" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId06" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="COID_SeriesQuantity07" HeaderText="SeriesQuantity07"
                                SortExpression="COID_SeriesQuantity07" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity07" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId07" HeaderText="Sequence Id7"
                                SortExpression="AID_SeqId07" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId07" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_SeriesQuantity08" HeaderText="SeriesQuantity08"
                                SortExpression="COID_SeriesQuantity08" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity08" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId08" HeaderText="Sequence Id8"
                                SortExpression="AID_SeqId08" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId08" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_SeriesQuantity09" HeaderText="SeriesQuantity09"
                                SortExpression="COID_SeriesQuantity09" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity09" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId09" HeaderText="Sequence Id9"
                                SortExpression="AID_SeqId09" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId09" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_SeriesQuantity10" HeaderText="SeriesQuantity10"
                                SortExpression="COID_SeriesQuantity10" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity10" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId10" HeaderText="Sequence Id10"
                                SortExpression="AID_SeqId10" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId10" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_SeriesQuantity11" HeaderText="SeriesQuantity11"
                                SortExpression="COID_SeriesQuantity11" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COID_SeriesQuantity11" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AID_SeqId11" HeaderText="Sequence Id11"
                                SortExpression="AID_SeqId11" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AID_SeqId11" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <%--<clientsettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </clientsettings>--%>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlGrid" runat="server" ScrollBars="Horizontal" Visible="false" CssClass="table">
    <table width="100%" align="left">
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrders" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false" AllowAutomaticInserts="false"
                    ExportSettings-FileName="NCD Order Recon" OnItemDataBound="gvOrders_ItemDataBound"
                    OnItemCommand="gvOrders_ItemCommand" OnNeedDataSource="gvOrders_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="Order" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,WOS_OrderStepCode,AIM_IssueId" Width="100%"
                        AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CO_OrderId" HeaderText="Order No" SortExpression="CO_OrderId"
                                ShowFilterIcon="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left" AllowSorting="True">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--    <telerik:GridTemplateColumn DataField="CO_OrderId" HeaderText="Order/Transaction No."
                                SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkOrderNo" Font-Underline="false" runat="server" CssClass="cmbFielde" Text='<%# Eval("CO_OrderId") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="OrderDetID" AllowFiltering="false" DataField="COID_DetailsId"
                                Visible="false">
                                <ItemStyle />
                            </telerik:GridTemplateColumn>
                            <telerik:GridDateTimeColumn DataField="CO_OrderDate" HeaderText="Order Date" SortExpression="CO_OrderDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_OrderDate" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer" SortExpression="Customer_Name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNo" HeaderText="Application No"
                                SortExpression="CO_ApplicationNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CO_ApplicationNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderText="Status" SortExpression="WOS_OrderStep"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="WOS_OrderStep" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridTemplateColumn DataField="WOS_OrderStep" HeaderText="Status" SortExpression="WOS_OrderStep"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="WOS_OrderStep" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderStep" runat="server" Text='<%#Eval("WOS_OrderStep").ToString() %>'> </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn DataField="CEDA_DPId" HeaderText="Dp Id" SortExpression="CEDA_DPId"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CEDA_DPId" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderdQuantity" HeaderText="Qty" SortExpression="OrderdQuantity"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="OrderdQuantity" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderText="Face value" SortExpression="AIM_FaceValue"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AIM_FaceValue" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="purchaseAmt" HeaderText="Purchase Amt" SortExpression="purchaseAmt"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="purchaseAmt" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_Price" HeaderText="Price" SortExpression="COID_Price"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="COID_Price" FooterStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_AllotmentDate" HeaderText="Allotment Date"
                                SortExpression="AIM_AllotmentDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AIM_AllotmentDate" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotmentQty" HeaderText="Alloted Qty" SortExpression="AllotmentQty"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AllotmentQty" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn Visible="false" DataField="AIA_Price" HeaderText="Alloted Price"
                                SortExpression="AIA_Price" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AIA_Price" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="AllotmentAmt" HeaderText="Alloted Amt" SortExpression="AllotmentAmt"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AllotmentAmt" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COAD_CertificateNo" HeaderText="Certificate No"
                                SortExpression="COAD_CertificateNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COAD_CertificateNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="Match"
                                EditText="Match" CancelText="Cancel" UpdateText="OK" Visible=">
                            </telerik:GridEditCommandColumn>--%>
                            <%-- <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="MarkAsReject"
                                    EditText="Mark As Reject" CancelText="Cancel" UpdateText="OK">
                                </telerik:GridEditCommandColumn>--%>
                        </Columns>
                        <EditFormSettings EditFormType="Template" PopUpSettings-Height="200px" PopUpSettings-Width="550px">
                            <FormTemplate>
                                <table>
                                    <tr id="trUnMatchedddl" runat="server">
                                        <td class="leftLabel">
                                            <asp:Label ID="lb1Issue" runat="server" Text="Issue:" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="rightData">
                                            <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbField">
                                            </asp:DropDownList>
                                            <span id="Span1" class="spnRequiredField">*</span>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Issuer"
                                                CssClass="rfvPCG" ControlToValidate="ddlIssuer" Display="Dynamic" InitialValue="Select"
                                                ValidationGroup="btnManualMatchGo"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="rightData">
                                            <asp:Button ID="btnManualMatchGo" runat="server" Text="GO" CssClass="PCGButton" OnClick="btnManualMatchGo_Click"
                                                ValidationGroup="btnManualMatchGo" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trUnMatchedGrd">
                                        <td colspan="3">
                                            <telerik:RadGrid ID="gvUnmatchedAllotments" runat="server" AllowSorting="True" enableloadondemand="True"
                                                PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                                Skin="Telerik" AllowFilteringByColumn="true" DataKeyNames="AIA_Id" OnNeedDataSource="gvUnmatchedAllotments_OnNeedDataSource">
                                                <%--OnNeedDataSource="gvUnmatchedAllotments_OnNeedDataSource"--%>
                                                <MasterTableView>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Select" ShowFilterIcon="false" AllowFiltering="false">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbUnMatched" runat="server" Checked="false" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="AIA_Id" HeaderText="Alotment No" SortExpression="AIA_Id"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="AIA_Id" FooterStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Issue Name" SortExpression="AIM_IssueName"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="AIM_IssueName" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer Name" SortExpression="Customer_Name"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CEDA_DPId" HeaderText="DPId" SortExpression="CEDA_DPId"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="CEDA_DPId" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CO_ApplicationNo" HeaderText="Application No"
                                                            SortExpression="CO_ApplicationNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                            AutoPostBackOnFilter="true" UniqueName="CO_ApplicationNo" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AIA_Quantity" HeaderText="Quantity" SortExpression="AIA_Quantity"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="AIA_Quantity" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AIA_Price" HeaderText="Price" SortExpression="AIA_Price"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="AIA_Price" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <%--  <telerik:GridBoundColumn DataField="Co_OrderId" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="LookupId" UniqueName="AIA_AllotmentDate"
                                                                SortExpression="AIA_AllotmentDate" Visible="false">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                            </telerik:GridBoundColumn>--%>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trUnMatchedBtns">
                                        <td class="leftLabel">
                                            <asp:Button ID="btnOK" Text="OK" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                CausesValidation="True" ValidationGroup="btnOK" />
                                        </td>
                                        <td class="rightData">
                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </FormTemplate>
                        </EditFormSettings>
                    </MasterTableView>
                    <%--<clientsettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </clientsettings>--%>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlOfflineNCDIPO" runat="server" ScrollBars="Horizontal" Visible="false"
    CssClass="table">
    <table width="100%" align="left">
        <tr>
            <td>
                <telerik:RadGrid ID="gvOfflineAllotment" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="gvOfflineAllotment_OnNeedDataSource" OnItemDataBound="gvOfflineAllotment_OnItemDataBound">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="Order" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,WOS_OrderStepCode,AIM_IssueId" Width="100%"
                        AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CO_OrderId" HeaderText="Order No" SortExpression="CO_OrderId"
                                ShowFilterIcon="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left" AllowSorting="True">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="CO_OrderDate" HeaderText="Order Date" SortExpression="CO_OrderDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_OrderDate" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer" SortExpression="Customer_Name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNo" HeaderText="Application No"
                                SortExpression="CO_ApplicationNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CO_ApplicationNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderText="Status" SortExpression="WOS_OrderStep"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="WOS_OrderStep" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CEDA_DPId" HeaderText="Dp Id" SortExpression="CEDA_DPId"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CEDA_DPId" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderdQuantity" HeaderText="Qty" SortExpression="OrderdQuantity"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="OrderdQuantity" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderText="Face value" SortExpression="AIM_FaceValue"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AIM_FaceValue" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="purchaseAmt" HeaderText="Purchase Amt" SortExpression="purchaseAmt"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="purchaseAmt" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_Price" HeaderText="Price" SortExpression="COID_Price"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="COID_Price" FooterStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_AllotmentDate" HeaderText="Allotment Date"
                                SortExpression="AIM_AllotmentDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AIM_AllotmentDate" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotmentQty" HeaderText="Alloted Qty" SortExpression="AllotmentQty"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AllotmentQty" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotmentAmt" HeaderText="Alloted Amt" SortExpression="AllotmentAmt"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AllotmentAmt" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COAD_CertificateNo" HeaderText="CertificateNo"
                                SortExpression="COAD_CertificateNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="COAD_CertificateNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_SubBrokerCode" HeaderText="Sub-Broker Code"
                                AllowFiltering="true" SortExpression="CMFT_SubBrokerCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_SubBrokerCode"
                                FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AssociatesName" AllowFiltering="true" HeaderText="SubBroker Name"
                                Visible="true" UniqueName="AssociatesName" SortExpression="AssociatesName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UserType" AllowFiltering="true" HeaderText="Type"
                                Visible="true" UniqueName="UserType" SortExpression="UserType" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ChannelName" AllowFiltering="true" HeaderText="Channel"
                                UniqueName="ChannelName" SortExpression="ChannelName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Titles" AllowFiltering="true" HeaderText="Title"
                                Visible="true" UniqueName="Titles" SortExpression="Titles" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ReportingManagerName" AllowFiltering="true" HeaderText="Reporting Manager"
                                Visible="true" UniqueName="ReportingManagerName" SortExpression="ReportingManagerName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="ClusterManager" AllowFiltering="true"
                                HeaderText="Cluster Manager" UniqueName="ClusterManager" SortExpression="ClusterManager"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AreaManager" AllowFiltering="true" HeaderText="Area Manager"
                                UniqueName="AreaManager" SortExpression="AreaManager" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ZonalManagerName" AllowFiltering="true" HeaderText="Zonal Manager"
                                UniqueName="ZonalManagerName" SortExpression="ZonalManagerName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeuptyHead" AllowFiltering="true" HeaderText="Deupty Head"
                                UniqueName="DeuptyHead" SortExpression="DeuptyHead" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlBtns" runat="server" Width="70%" Visible="false" Height="50px"
    BorderStyle="Solid" BorderWidth="0px">
    <table>
        <tr>
            <td class="leftField">
                <asp:Button ID="btnAutoMatch" runat="server" Text="Auto Match" CssClass="PCGMediumButton"
                    OnClick="btnAutoMatch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnMannualMatch" runat="server" Text="Manual Match" CssClass="PCGMediumButton"
                    Visible="false" />
            </td>
        </tr>
    </table>
</asp:Panel>
