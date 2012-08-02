﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KarvyProfileFolioInputRejects.ascx.cs" 
Inherits="WealthERP.Uploads.KarvyProfileFolioInputRejects" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<link href="../CSS/GridViewCss.css" rel="stylesheet" type="text/css" />

<div class="divPageHeading">
    <table cellspacing="0" cellpadding="3" width="100%">
        <tr>
            <td class="HeaderCell">               
                <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Karvy Profile Input Rejects"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
        </td>
    </tr>    
    <%-- <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Karvy Profile Input Rejects"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View ProcessLog"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" class="Field" runat="server"></asp:Label>
            <asp:Label ID="Label2" class="Field" runat="server"></asp:Label>
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
                CssClass="GridViewStyle" ShowFooter="true" DataKeyNames="KarvyProfileInputId" >
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
                    <asp:BoundField DataField="PAN_NO" HeaderText = "PAN NUMBER" />
                    <asp:BoundField DataField="BROKER_CODE" HeaderText = "BROKER CODE" />
                    <asp:BoundField DataField="CUST_ADDRESS" HeaderText="ADDRESS" />
                    <asp:BoundField DataField="CITY" HeaderText="CITY"  />
                    <asp:BoundField DataField="PIN_CODE" HeaderText="PIN CODE" />
                    <asp:BoundField DataField="PHONE_OFF" HeaderText="PHONE OFFICE" />
                    <asp:BoundField DataField="PHONE_OFF1" HeaderText="PHONE OFFICE 1" />
                    <asp:BoundField DataField="PHONE_OFF2" HeaderText="PHONE OFFICE 2" />
                    <asp:BoundField DataField="PHONE_RES" HeaderText="PHONE RESIDENCE" />
                    <asp:BoundField DataField="PHONE_RES1" HeaderText="PHONE RESIDENCE 1" />
                    <asp:BoundField DataField="PHONE_RES2" HeaderText="PHONE RESIDENCE 2" />
                    <asp:BoundField DataField="FAX_RES" HeaderText="FAX RESIDENCE" />
                    <asp:BoundField DataField="FAX_OFF" HeaderText="FAX OFFICE" />
                    <asp:BoundField DataField="BANK_PHONE" HeaderText="BANK PHONE" />
                    <asp:BoundField DataField="MOBILE_NO" HeaderText="MOBILE NO" />
                    <asp:BoundField DataField="DATE_OF_BIRTH" HeaderText="DATE OF BIRTH" />
                    <asp:BoundField DataField="REPORT_DATE" HeaderText="REPORT DATE" />
                    
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
