<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeutscheTransInputRejects.ascx.cs" 
Inherits="WealthERP.Uploads.DeutscheTransInputRejects" %>


<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<link href="../CSS/GridViewCss.css" rel="stylesheet" type="text/css" />


<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Deutsche Transaction Input Rejects"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View ProcessLog"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
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
            <asp:GridView ID="gvRejectedRecords" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" ShowFooter="true" DataKeyNames="DeutscheTransInputId" >
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="ProcessId" HeaderText="PROCESS ID" />
                    <asp:BoundField DataField="INV_NAME" HeaderText="INVESTOR NAME" />
                    <asp:BoundField DataField="FOLIO_NUMBER" HeaderText = "FOLIO NUMBER" />
                    <asp:BoundField DataField="TRANSACTION_NUMBER" HeaderText = "TRANSACTION NUMBER" />
                    <asp:BoundField DataField="PRODUCT_CODE" HeaderText="PRODUCT CODE" />
                    <asp:BoundField DataField="SCHEME_NAME" HeaderText="SCHEME NAME"  />
                    <asp:BoundField DataField="TRANSACTION_DATE" HeaderText="TRANSACTION DATE" />
                    <asp:BoundField DataField="PRICE" HeaderText="PRICE" />
                    <asp:BoundField DataField="UNITS" HeaderText="UNITS" />
                    <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                    <asp:BoundField DataField="STT" HeaderText="STT" />
                    <asp:BoundField DataField="POST_DATE" HeaderText="POST DATE" />
                    <asp:BoundField DataField="BROK_PERCENTAGE" HeaderText="BROK PERCENTAGE" />
                    <asp:BoundField DataField="BROK_COMM" HeaderText="BROK COMM" />
                    <asp:BoundField DataField="ENTRY_LOAD" HeaderText="ENTRY LOAD" />
                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trInputNullMessage" runat="server" visible="false">
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
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="ADUL_StartTime DESC" />



