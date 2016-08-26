<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadRequestStatus.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.UploadRequestStatus" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

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

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {


        var gvControl = document.getElementById('<%= rgBondsGrid.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0 && (!inputTypes[i].disabled))
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

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
<table width="99%" runat="server" id="tbIssue">
    <tr>
        <td class="leftField" style="width: 70px">
            <asp:Label ID="lb1Type" runat="server" Text="Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 70px">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true" TabIndex="41"
                Width="240px" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Type"
                CssClass="rfvPCG" ControlToValidate="ddlType" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td id="tdProduct" runat="server" visible="false">
            <span class="FieldName">Product:</span>
            <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProduct_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Bonds" Value="FI"></asp:ListItem>
                <asp:ListItem Text="IPO" Value="IP"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Type"
                CssClass="rfvPCG" ControlToValidate="ddlProduct" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="0"></asp:RequiredFieldValidator>
        </td>
        <td id="tdCategory" runat="server" visible="false">
            <span class="FieldName">Category:</span>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Type"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="0"></asp:RequiredFieldValidator>
        </td>
        <td id="tdProductType" runat="server" visible="false">
            <span class="FieldName">Product Type:</span>
            <asp:DropDownList ID="ddlIsonline" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select" Value="2"></asp:ListItem>
                <asp:ListItem Text="Online" Value="1"></asp:ListItem>
                <asp:ListItem Text="Offline" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Product Type"
                CssClass="rfvPCG" ControlToValidate="ddlIsonline" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="2"></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td id="td1" runat="server" >
            <span class="FieldName">Filter On:</span>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Request Id" Value="1"></asp:ListItem>
                <asp:ListItem Text="Request Date" Value="2"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Product Filter On"
                CssClass="rfvPCG" ControlToValidate="DropDownList1" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="0"></asp:RequiredFieldValidator>
        </td>
        <td id="tdreqId" runat="server" visible="false">
        <asp:TextBox ID="txtreqId" runat="server" CssClass="txtField" ></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtreqId" ErrorMessage="<br />Please Enter the request Id"
                Display="Dynamic" runat="server" ValidationGroup="btnGo" CssClass="rfvPCG" >
            </asp:RequiredFieldValidator>
        </td>
        <td id="tdFromD" runat="server" visible="false">
         <span class="FieldName">Requested From Date:</span>
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
     
     <td id="tdToDate" runat="server" visible="false">
        <span class="FieldName">Requested From Date:</span>
            <telerik:RadDatePicker ID="rdpToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                TabIndex="17" Width="150px" AutoPostBack="false">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select a Date" Display="Dynamic" ControlToValidate="rdpToDate"
                InitialValue="" ValidationGroup="btnGo">
            
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG" ErrorMessage="To date must be greater than from date."
                ControlToCompare="txtReqDate" ValidationGroup="btnGo" ControlToValidate="rdpToDate"
                Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
    </tr>
</table>
<table>
    <tr id="tblMessagee" runat="server" visible="false">
        <td colspan="6">
            <table class="tblMessage">
                <tr>
                    <td align="center">
                        <div id="divMessage" align="center">
                        </div>
                        <div style="clear: both">
                        </div>
                    </td>
                </tr>
            </table>
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
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px" Visible="false">
                            </asp:ImageButton>
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
                            <telerik:RadGrid ID="rgRequests" Visible="false" Width="1500px" runat="server" AllowSorting="True"
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
                                                    Font-Size="Medium" Visible="false"> View Rejects</asp:LinkButton>
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
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <telerik:RadGrid Visible="false" ID="rgBondsGrid" Width="1500px" runat="server" AllowSorting="True"
                                enableloadondemand="True" PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                                ShowStatusBar="True" Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgBondsGrid_OnNeedDataSource">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                    Width="100%" DataKeyNames="AIM_IssueName,AIAUL_ApplicationNumber,AIM_IssueName,AIAUL_ApplicationNumber,AIAUL_Shares,AIAUL_ProcessId,AIAUL_Certificate_No,AIAUL_Pangir,AIAUL_InvestorName,AIAUL_IssueCode,AIAUL_BrokerCode,AIAUL_SubBrokerCode,AIAUL_Reason,AIAUL_Remark_Aot,AIAUL_Brk1_Rec,AIAUL_Brk1_Rec_Rate,AIAUL_Brk2_Rec_Rate,AIAUL_Brk2_Rec,
AIAUL_Brk3_Rec_Rate,AIAUL_Brk3_Rec,AIAUL_Total_Brk_rec,AIAUL_SvcTaxAM,AIAUL_Tds,AIAUL_Total_Receivable,AIAUL_AllotmentDate,AIAUL_RfndNo,AIAPL_IssueId,AIAUL_Id">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <HeaderTemplate>
                                                <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkId" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_ProcessId" HeaderText="ProcessId" SortExpression="AIAUL_ProcessId"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_ProcessId" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="Remarks" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Status" HeaderText="Status" SortExpression="AIAUL_Status"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_Status" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Issue Name" SortExpression="AIM_IssueName"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIM_IssueName" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="AIAUL_ApplicationNumber" HeaderText="AppNbr"
                                            SortExpression="AIAUL_ApplicationNumber" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_ApplicationNumber" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtApplicationNo" runat="server" Text='<%# Eval("AIAUL_ApplicationNumber") %>'></asp:TextBox></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="AIAUL_Shares" HeaderText="AlltQty" SortExpression="AIAUL_Shares"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_Shares" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAlltQty" runat="server" Text='<%# Eval("AIAUL_Shares") %>'></asp:TextBox></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="AIAUL_Certificate_No" HeaderText="CertNo"
                                            SortExpression="AIAUL_Certificate_No" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_Certificate_No" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCertificate_No" runat="server" Text='<%# Eval("AIAUL_Certificate_No") %>'></asp:TextBox></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="AIAUL_Pangir" HeaderText="PAN" SortExpression="AIAUL_Pangir"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_Pangir" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPangir" runat="server" Text='<%# Eval("AIAUL_Pangir") %>'></asp:TextBox></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_InvestorName" HeaderText="InvestorName"
                                            SortExpression="AIAUL_InvestorName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_InvestorName" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_RfndNo" HeaderText="RTARefNum" SortExpression="AIAUL_RfndNo"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_RfndNo" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_IssueCode" HeaderText="IssueCode" SortExpression="AIAUL_IssueCode"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_IssueCode" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_BrokerCode" HeaderText="BrokerCode " SortExpression="AIAUL_BrokerCode"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_BrokerCode" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_SubBrokerCode" HeaderText="SubBrokerCode "
                                            SortExpression="AIAUL_SubBrokerCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_SubBrokerCode" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Reason" HeaderText="RTARejectRemark " SortExpression="AIAUL_Reason"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_Reason" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Remark_Aot" HeaderText="RTAOtherRemarks "
                                            SortExpression="AIAUL_Remark_Aot" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_Remark_Aot" FooterStyle-HorizontalAlign="Left"
                                            Visible="false">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Brk1_Rec" HeaderText="BrokerageRate " SortExpression="AIAUL_Brk1_Rec"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_Brk1_Rec" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Brk1_Rec_Rate" HeaderText="BrokerageAmt "
                                            SortExpression="AIAUL_Brk1_Rec_Rate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_Brk1_Rec_Rate" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Brk2_Rec_Rate" HeaderText="BrokerageRate2 "
                                            SortExpression="AIAUL_Brk2_Rec_Rate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_Brk2_Rec_Rate" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Brk2_Rec" HeaderText="BrokerageAmt2 " SortExpression="AIAUL_Brk2_Rec"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_Brk2_Rec" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Brk3_Rec_Rate" HeaderText="BrokerageRate3 "
                                            SortExpression="AIAUL_Brk3_Rec_Rate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_Brk3_Rec_Rate" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Brk3_Rec" HeaderText="BrokerageAmt3 " SortExpression="AIAUL_Brk3_Rec"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_Brk3_Rec" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Total_Brk_rec" HeaderText="NetBrokerage "
                                            SortExpression="AIAUL_Total_Brk_rec" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_Total_Brk_rec" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_SvcTaxAM" HeaderText="SvcTaxAMT " SortExpression="AIAUL_SvcTaxAM"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_SvcTaxAM" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Tds" HeaderText="TDSAMT " SortExpression="AIAUL_Tds"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="AIAUL_Tds" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_Total_Receivable" HeaderText="GrossBrokerge "
                                            SortExpression="AIAUL_Total_Receivable" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_Total_Receivable" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIAUL_AllotmentDate" HeaderText="AllotmentDate "
                                            SortExpression="AIAUL_AllotmentDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="AIAUL_AllotmentDate" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <telerik:RadGrid Visible="false" ID="radGridOrderDetails" Width="1200px" runat="server"
                                AllowSorting="True" enableloadondemand="True" PageSize="5" AutoGenerateColumns="False"
                                EnableEmbeddedSkins="False" GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true"
                                AllowPaging="true" ShowStatusBar="True" Skin="Telerik" AllowFilteringByColumn="true"
                                OnNeedDataSource="radGridOrderDetails_OnNeedDataSource">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                    DataKeyNames="processid">
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="processid" HeaderText="Processid" SortExpression="processid"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="processid" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="30px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="txtPangir" runat="server" Text='<%# Eval("processid") %>' OnClick="txtPangir_OnClick"></asp:LinkButton></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="accepted" HeaderText="Accepted " SortExpression="accepted"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="accepted" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="30px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="rejected" HeaderText="Rejected " SortExpression="rejected"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="rejected" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="30px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="issueId" HeaderText="IssueId " Visible="false"
                                            SortExpression="issueId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            AutoPostBackOnFilter="true" UniqueName="issueId" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="30px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="total" HeaderText="Total " SortExpression="total"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                            UniqueName="total" FooterStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="30px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
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
<asp:HiddenField ID="hdnProcessId" runat="server" />
<table>
    <tr>
        <td>
            <asp:Button ID="btnReprocess" runat="server" CssClass="PCGButton" Visible="false"
                Text="Reprocess" OnClick="btnReprocess_OnClick" />
        </td>
    </tr>
</table>
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
