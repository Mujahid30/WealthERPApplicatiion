<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadProcessLog.ascx.cs"
    Inherits="WealthERP.Admin.UploadProcessLog1" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Upload Process Log"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
            <asp:GridView ID="gvProcessLog" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ShowFooter="true" CssClass="GridViewStyle" AllowSorting="true" OnSelectedIndexChanged="gvProcessLog_SelectedIndexChanged"
                DataKeyNames="ProcessId,assetClass">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <%--Check Boxes--%>
                    <asp:TemplateField HeaderText="Select Records">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlActions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Manage Rejects</asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:CheckBox ID="chkBx" runat="server" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="processId" HeaderText="ProcessId" />
                    <asp:BoundField DataField="assetClass" HeaderText="Asset Class" />
                    <asp:BoundField DataField="startTime" HeaderText="Start Time" />
                    <asp:BoundField DataField="endTime" HeaderText="End Time" />
                    <asp:BoundField DataField="isXmlCreated" HeaderText="Xml Created" />
                    <asp:BoundField DataField="isSnapshotUpdated" HeaderText="Snapshots Updated" />
                    <asp:BoundField DataField="isHistoryUpdated" HeaderText="History Updated" />
                    <asp:BoundField DataField="totalSnapshotsUpdated" HeaderText="Total snapshots updated" />
                    <asp:BoundField DataField="totalHistoryUpdated" HeaderText="Total history updated" />
                    <asp:BoundField DataField="totalRejectedRecords" HeaderText="Total rejected records" />
                    <asp:BoundField DataField="createdOn" HeaderText="Created On" />
                    <%-- <asp:BoundField DataField="RejectReason" HeaderText="Reject Reason" />--%>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <div id="DivPager" runat="server" >
    <table style="width: 100%">
        <tr align="center">
            <td>
                <uc1:Pager ID="mypager" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    <tr id="trMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyMsg" class="FieldName">
                There are no records to be displayed!</label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
