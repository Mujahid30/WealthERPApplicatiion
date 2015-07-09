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
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="false"
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
                TabIndex="17" Width="150px" AutoPostBack="false">
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
                                    Width="100%" DataKeyNames="ReqId,XMLStatus,IsOnl">
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
                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="Status" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IsOnl" HeaderText="Offline/online" SortExpression="IsOnl"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="IsOnl" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RTA" HeaderText="R&T Name" SortExpression="RTA"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="RTA" FooterStyle-HorizontalAlign="Left">
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
                                        <telerik:GridTemplateColumn DataField="Cutomercreated" HeaderText="No of Customer Created"
                                            SortExpression="Cutomercreated" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="Cutomercreated" FooterStyle-HorizontalAlign="Left"
                                            Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCustmCreated" runat="server" OnClick="lnkCustmCreated_Click"
                                                    CommandName="Select" Text='<%# Eval("Cutomercreated").ToString() %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="FolioCreated" HeaderText="No of Folio Created"
                                            SortExpression="FolioCreated" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="FolioCreated" FooterStyle-HorizontalAlign="Left"
                                            Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFolioCreated" runat="server" OnClick="lnkFolioCreated_Click"
                                                    CommandName="Select" Text='<%# Eval("FolioCreated").ToString() %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="TransactionCreated" HeaderText="No of Transaction Created"
                                            SortExpression="TransactionCreated" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="TransactionCreated" FooterStyle-HorizontalAlign="Left"
                                            Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTransactionCreated" runat="server" OnClick="lnkTransactionCreated_Click"
                                                    CommandName="Select" Text='<%# Eval("TransactionCreated").ToString() %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="RejectReseaon" HeaderText="External Header Mapping Message"
                                            SortExpression="RejectReseaon" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="RejectReseaon" FooterStyle-HorizontalAlign="Left"
                                            Visible="false">
                                             <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                   
                                            <telerik:GridBoundColumn DataField="AIAUL_ProcessId" HeaderText="ProcessId" SortExpression="AIAUL_ProcessId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_ProcessId" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_Status" HeaderText="Status" SortExpression="AIAUL_Status"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Status" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Issue Name" SortExpression="AIM_IssueName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIM_IssueName" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_ApplicationNumber" 
                                    HeaderText="AppNbr" SortExpression="AIAUL_ApplicationNumber"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_ApplicationNumber" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_Shares" HeaderText="AlltQty" SortExpression="AIAUL_Shares"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Shares" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_Certificate_No" HeaderText="CertNo" SortExpression="AIAUL_Certificate_No"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Certificate_No" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_Pangir" HeaderText="PAN" SortExpression="AIAUL_Pangir"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Pangir" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_InvestorName" 
                                    HeaderText="InvestorName" SortExpression="AIAUL_InvestorName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_InvestorName" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_RfndNo" HeaderText="RTARefNum" SortExpression="AIAUL_RfndNo"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_RfndNo" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_IssueCode" HeaderText="IssueCode" SortExpression="AIAUL_IssueCode"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_IssueCode" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_BrokerCode" HeaderText="BrokerCode " SortExpression="AIAUL_BrokerCode"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_BrokerCode" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="AIAUL_SubBrokerCode" 
                                    HeaderText="SubBrokerCode " SortExpression="AIAUL_SubBrokerCode"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_SubBrokerCode" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Reason" HeaderText="RTARejectRemark " SortExpression="AIAUL_Reason"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Reason" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Remark_Aot" 
                                    HeaderText="RTAOtherRemarks " SortExpression="AIAUL_Remark_Aot"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Remark_Aot" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Brk1_Rec" HeaderText="BrokerageRate " SortExpression="AIAUL_Brk1_Rec"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Brk1_Rec" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Brk1_Rec_Rate" 
                                    HeaderText="BrokerageAmt " SortExpression="AIAUL_Brk1_Rec_Rate"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Brk1_Rec_Rate" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Brk2_Rec_Rate" 
                                    HeaderText="BrokerageRate2 " SortExpression="AIAUL_Brk2_Rec_Rate"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Brk2_Rec_Rate" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Brk2_Rec" HeaderText="BrokerageAmt2 " SortExpression="AIAUL_Brk2_Rec"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Brk2_Rec" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Brk3_Rec_Rate" 
                                    HeaderText="BrokerageRate3 " SortExpression="AIAUL_Brk3_Rec_Rate"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Brk3_Rec_Rate" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Brk3_Rec" HeaderText="BrokerageAmt3 " SortExpression="AIAUL_Brk3_Rec"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Brk3_Rec" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="AIAUL_Total_Brk_rec" 
                                    HeaderText="NetBrokerage " SortExpression="AIAUL_Total_Brk_rec"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Total_Brk_rec" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_SvcTaxAM" HeaderText="SvcTaxAMT " SortExpression="AIAUL_SvcTaxAM"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_SvcTaxAM" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_Tds" HeaderText="TDSAMT " SortExpression="AIAUL_Tds"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_Tds" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_Total_Receivable" 
                                    HeaderText="GrossBrokerge " SortExpression="AIAUL_Total_Receivable"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    Visible="false"
                                    UniqueName="AIAUL_Total_Receivable" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIAUL_AllotmentDate" 
                                    HeaderText="AllotmentDate " SortExpression="AIAUL_AllotmentDate"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIAUL_AllotmentDate" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top"  />
                                </telerik:GridBoundColumn>
                                
                                           
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
<%--<table>
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
</table>--%>
