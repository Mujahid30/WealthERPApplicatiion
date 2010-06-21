<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WERPAdminDownloadProcessLog.ascx.cs" 
        Inherits="WealthERP.Admin.WERPAdminDownloadProcessLog" %>
        <%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

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
                CssClass="GridViewStyle" >
                <FooterStyle CssClass="FieldName" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    
                    <asp:BoundField DataField="ProcessID" HeaderText="Process Id" />
                    <asp:BoundField DataField="AssetClass" HeaderText="Asset Class" />
                    <asp:BoundField DataField="SourceName" HeaderText="Source Name" />
                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="ADUL_StartTime" />
                    <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                    <asp:BoundField DataField="ForDate" HeaderText="For Date" />
                    <asp:BoundField DataField="NoOfRecordsDownloaded" HeaderText="No Of Records Downloaded" />
                    <asp:BoundField DataField="IsConnectionToSiteEstablished" HeaderText="Is Connection To Site Established" />
                    <asp:BoundField DataField="IsFileDownloaded" HeaderText="Is File Downloaded" />
                    <asp:BoundField DataField="IsConversiontoXMLComplete" HeaderText="Is Conversionto XML Complete" />
                    <asp:BoundField DataField="IsInsertiontoDBdone" HeaderText="Is Insertionto DB done" />
                    <asp:BoundField DataField="XMLFileName" HeaderText="XML FileName" />
                    <asp:BoundField DataField="NoOfRecordsInserted" HeaderText="No Of Records Inserted" />
                    <asp:BoundField DataField="DownloadDescription" HeaderText="Download Description" ItemStyle-Wrap="false"  />
                    
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
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="ADUL_StartTime DESC" />