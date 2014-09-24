<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadRequestStatus.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.UploadRequestStatus" %>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 18%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
</style>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Request Upload Status
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="70%" runat="server" id="tbIssue">
    <tr>
        <td class="leftField" style="width: 70px">
            <asp:Label ID="lb1Type" runat="server" Text="Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 70px">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="240px">
            </asp:DropDownList>
        </td>
        <td style="width: 2px">
            <span id="Span7" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Type"
                CssClass="rfvPCG" ControlToValidate="ddlType" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lb1date" runat="server" Text="Requested Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <telerik:RadDatePicker ID="txtReqDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                TabIndex="17" Width="150px" AutoPostBack="true">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span18" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Enter Requested Date" Display="Dynamic" ControlToValidate="txtReqDate"
                InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftField">
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
    </tr>
</table>
<%--<table width="100%" id="btnexport" visible="false">
    <tr>
        <td>
            <div class="divPageHeading" visible="false">
                <table cellspacing="0" cellpadding="3" width="100%" visible="false">
                    <tr>
                        <td align="right" visible="false">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px" Visible="false"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>--%>
<table id="btnexport" runat="server" visible="false" style="width: 100%" cellpadding="2"
    cellspacing="5">
    <tr>
        <td class="tdSectionHeading">
            <div class="divSectionHeading" style="vertical-align: text-bottom;">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%-- OnItemCommand="rgEligibleInvestorCategories_ItemCommand" OnItemDataBound="rgEligibleInvestorCategories_ItemDataBound"--%>
<asp:Panel ID="pnlRequest" runat="server" CssClass="Landscape" Width="100%" Visible="false"
    ScrollBars="Both" class="leftLabel">
    <table id="Table1" runat="server" width="100%">
        <tr>
            <td class="leftLabel">
                &nbsp;
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="rgRequests" Width="1500px" runat="server" AllowSorting="True"
                                enableloadondemand="True" PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                                ShowStatusBar="True" Skin="Telerik" AllowFilteringByColumn="true" OnItemDataBound="rgRequests_ItemDataBound"
                                OnNeedDataSource="rgRequests_OnNeedDataSource">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                    Width="100%" DataKeyNames="ReqId">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                    Font-Bold="true" UniqueName="DetailsCategorieslink" OnClick="btnCategoriesExpandAll_Click"
                                                    Font-Size="Medium"> View Rejects</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ReqId" HeaderText="Req Id" SortExpression="ReqId"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="ReqId" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReqDate" HeaderText="Request Date" SortExpression="ReqDate"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="ReqDate" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="filename" HeaderText="File Name" SortExpression="filename"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="filename" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalNoOfRecords" HeaderText="Total Records"
                                            SortExpression="TotalNoOfRecords" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="TotalNoOfRecords" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="InputRejects" HeaderText="Duplicate Rejects"
                                            SortExpression="InputRejects" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="InputRejects" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StagingRejects" HeaderText="DataType Rejects"
                                            SortExpression="StagingRejects" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="StagingRejects" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Staging" HeaderText="DataTranslation Rejects"
                                            SortExpression="Staging" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="Staging" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Success" HeaderText="Success" SortExpression="Success"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="Success" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="0.2%">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                            ScrollBars="Both" Visible="false">
                                                            <telerik:RadGrid ID="rgRequestRejects" runat="server" AutoGenerateColumns="true"
                                                                enableloadondemand="True" PageSize="5" EnableEmbeddedSkins="False" GridLines="None"
                                                                ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                                                                AllowFilteringByColumn="true" OnNeedDataSource="rgRequestRejects_OnNeedDataSource"
                                                                AllowPaging="false">
                                                                <%-- <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIIC_InvestorCatgeoryId,AIICST_Id"
                                                                    AutoGenerateColumns="false">--%>
                                                                <%--<Columns>
                                                                        <telerik:GridBoundColumn DataField="WCMV_Name" HeaderStyle-Width="30px" UniqueName="WCMV_Name"
                                                                            CurrentFilterFunction="Contains" HeaderText="Investor Type" SortExpression="WCMV_Name"
                                                                            AllowFiltering="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>--%>
                                                                <%--   </MasterTableView>--%>
                                                            </telerik:RadGrid>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<table>
    <tr id="trNote" runat="server">
        <td colspan="2">
            <div id="Div2" class="Note">
                <p>
                    <span style="font-weight: bold">Note:</span><br />
                    <span style="font-weight: bold">Input Reject Reason</span><br />
                    1. Duplicate Customer found in the file or already Existing Customer.<br />
                    2. Customer not Found.(Modification)<br />
                    <span style="font-weight: bold">Staging Reject Reason</span><br />
                    3.Datatype Mismatch.<br />
                </p>
            </div>
        </td>
        <td colspan="2">
        </td>
    </tr>
</table>
