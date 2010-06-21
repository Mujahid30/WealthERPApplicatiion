<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StandardProfileInputRejects.ascx.cs" 
Inherits="WealthERP.Uploads.StandardProfileInputRejects" %>

<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<link href="../CSS/GridViewCss.css" rel="stylesheet" type="text/css" />


<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
        </td>
    </tr>
     <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Standard Input Profile Rejects"></asp:Label>
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
                CssClass="GridViewStyle" ShowFooter="true" DataKeyNames="StandardProfileInputId" >
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
                    <asp:BoundField DataField="CUST_ADDRESS" HeaderText="ADDRESS" />
                    <asp:BoundField DataField="CITY" HeaderText="CITY"  />
                    <asp:BoundField DataField="PIN_CODE" HeaderText="PIN CODE" />
                    <asp:BoundField DataField="PIN_CODE2" HeaderText="PIN CODE2" />
                    <asp:BoundField DataField="RES_ISD_CODE" HeaderText="RES ISD CODE" />
                    <asp:BoundField DataField="RES_STD_CODE" HeaderText="RES STD CODE" />
                    <asp:BoundField DataField="RES_PHONE_NUM" HeaderText="RES PHONE NUM" />
                    <asp:BoundField DataField="OFC_ISD_C0DE" HeaderText="OFFICE ISD C0DE" />
                    <asp:BoundField DataField="OFC_STD_CODE" HeaderText="OFFICE STD CODE" />
                    <asp:BoundField DataField="OFC_PHONE_NUM" HeaderText="OFFICE PHONE NUM" />
                    <asp:BoundField DataField="MOBILE1" HeaderText="MOBILE1" />
                    <asp:BoundField DataField="MOBILE2" HeaderText="MOBILE2" />
                    <asp:BoundField DataField="ISD_FAX" HeaderText="ISD FAX" />
                    <asp:BoundField DataField="STD_FAX" HeaderText="STD FAX" />
                    <asp:BoundField DataField="FAX" HeaderText="FAX" />
                    <asp:BoundField DataField="OFC_FAX" HeaderText="OFFICE_FAX" />
                    <asp:BoundField DataField="OFC_FAX_ISD" HeaderText="OFFICE FAX ISD" />
                    <asp:BoundField DataField="OFC_STD_FAX" HeaderText="OFFICE STD FAX" />
                    <asp:BoundField DataField="OFC_PIN_CODE" HeaderText="OFFICE PIN CODE" />
                    <asp:BoundField DataField="BRANCH_PIN_CODE" HeaderText="BRANCH PIN CODE" />
                    <asp:BoundField DataField="MICR" HeaderText="MICR" />
                    <asp:BoundField DataField="COMMENCEMENT_DATE" HeaderText="COMMENCEMENT DATE" />
                    <asp:BoundField DataField="REGISTRATION_DATE" HeaderText="REGISTRATION DATE" />
                    <asp:BoundField DataField="RBI_APPROVAL_DATE" HeaderText="RBI APPROVAL DATE" />
                    <asp:BoundField DataField="MARRIAGE_DATE" HeaderText="MARRIAGE DATE" />
                    <asp:BoundField DataField="DATE_OF_BIRTH" HeaderText="DATE OF BIRTH" />
                    
                    
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
