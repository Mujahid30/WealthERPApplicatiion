<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewUploadProcessLog.ascx.cs"
    Inherits="WealthERP.Uploads.ViewUploadProcessLog" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Upload History"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<table style="width: 100%;" class="TableBackground">
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvProcessLog" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" DataKeyNames="ADUL_ProcessId,WUXFT_XMLFileTypeId,XUET_ExtractTypeCode" AllowSorting="true"
                OnSorting="gvProcessLog_Sort" ShowFooter="true">
                <FooterStyle CssClass="FieldName" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                <asp:ListItem Text="Select" Value="Select" />
                                <asp:ListItem Text="Reprocess" Value="Reprocess" />
                                <asp:ListItem Text="RollBack" Value="RollBack" />
                                <asp:ListItem Text="Manage Rejects" Value="Manage Rejects" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ADUL_ProcessId" HeaderText="Process Id" />
                    <asp:BoundField DataField="ADUL_FileName" HeaderText="Actual FileName" />
                    <asp:BoundField DataField="WUXFT_XMLFileName" HeaderText = "File Type" />
                    <asp:BoundField DataField="XUET_ExtractType" HeaderText = "Extract Type" />
                    <asp:BoundField DataField="ADUL_XMLFileName" HeaderText="XML FileName" />
                    <asp:BoundField DataField="ADUL_StartTime" HeaderText="Start Time" SortExpression="ADUL_StartTime" />
                    <asp:BoundField DataField="ADUL_EndTime" HeaderText="End Time" />
                    <asp:BoundField DataField="ADUL_TotalNoOfRecords" HeaderText="Total No of Records" />
                    <asp:BoundField DataField="ADUL_NoOfCustomersCreated" HeaderText="No of Customers Created" />
                    <asp:BoundField DataField="ADUL_NoOfAccountsCreated" HeaderText="No of Accounts Created" />
                    <asp:BoundField DataField="ADUL_NoOfTransactionsCreated" HeaderText="No of Transactions Created" />
                    <asp:BoundField DataField="ADUL_NoOfCustomerDuplicates" HeaderText="No of Duplicate Customers" />
                    <asp:BoundField DataField="ADUL_NoOfAccountDuplicate" HeaderText="No of Duplicate Accounts" />
                    <asp:BoundField DataField="ADUL_NoOfTransactionDuplicate" HeaderText="No of Duplicate Transactions" />
                    <asp:BoundField DataField="ADUL_NoOfRejectRecords" HeaderText="No of Rejected Records" />
                    <asp:BoundField DataField="ADUL_NoOfRecordsInserted" HeaderText="No of Records Inserted" />
                    <asp:BoundField DataField="ADUL_NoOfInputRejects" HeaderText="No of Input Rejects" />
                    <asp:BoundField DataField="ADUL_IsXMLConvesionComplete" HeaderText="XML Conversion Status" />
                    <asp:BoundField DataField="ADUL_IsInsertionToInputComplete" HeaderText="Input Insertion Status" />
                    <asp:BoundField DataField="ADUL_IsInsertionToFirstStagingComplete" HeaderText="First Staging Status" />
                    <asp:BoundField DataField="ADUL_IsInsertionToSecondStagingComplete" HeaderText="Second Staging Status" />
                    <asp:BoundField DataField="ADUL_IsInsertionToWerpComplete" HeaderText="WERP Insertion Status" />
                    <asp:BoundField DataField="ADUL_IsInsertionToXtrnlComplete" HeaderText="External Insertion Status" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trTransactionMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyTranEmptyMsg" class="FieldName">
                There are no records to be displayed !
            </label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr id="trError" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Error" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<table style="width: 100%" id="tblReprocess" runat="server">
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblSummary" Text="Summary of Reprocess" CssClass="HeaderTextSmall"
                runat="server"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trUploadedCustomers" runat="server">
        <td class="leftField">
            <asp:Label ID="lblUploadedCustomers" Text="No. of Customers Created:" CssClass="FieldName"
                runat="server">
            </asp:Label>
        </td>
        <td colspan="3" class="rightField">
            <asp:TextBox ID="txtUploadedCustomers" CssClass="txtField" runat="server" Enabled="false">
            </asp:TextBox>
        </td>
    </tr>
    <tr id="trUploadedFolios" runat="server">
        <td class="leftField">
            <asp:Label ID="lblUploadedFolios" Text="No. of Folios Created:" CssClass="FieldName"
                runat="server">
            </asp:Label>
        </td>
        <td colspan="3" class="rightField">
            <asp:TextBox ID="txtUploadedFolios" CssClass="txtField" runat="server" Enabled="false">
            </asp:TextBox>
        </td>
    </tr>
    <tr id="trUploadedTransactions" runat="server">
        <td class="leftField">
            <asp:Label ID="Label1" Text="No. of Transactions Uploaded:" CssClass="FieldName"
                runat="server">
            </asp:Label>
        </td>
        <td colspan="3" class="rightField">
            <asp:TextBox ID="txtUploadedTransactions" CssClass="txtField" runat="server" Enabled="false">
            </asp:TextBox>
        </td>
    </tr>
    <tr id="trRejectedRecords" runat="server">
        <td class="leftField" style="width: 30%;">
            <asp:Label ID="lblRejectedRecords" Text="No. of Records Rejected:" CssClass="FieldName"
                runat="server">
            </asp:Label>
        </td>
        <td colspan="3" class="rightField">
            <asp:TextBox ID="txtRejectedRecords" CssClass="txtField" runat="server" Enabled="false">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="ADUL_StartTime DESC" />
