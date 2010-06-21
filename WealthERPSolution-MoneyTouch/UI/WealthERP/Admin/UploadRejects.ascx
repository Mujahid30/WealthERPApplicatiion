<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadRejects.ascx.cs"
    Inherits="WealthERP.Admin.UploadReject" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<style>
    .message
    {
        width: 500px;
        color: #000000;
        border: 3px solid #A8504F;
        background-color: #ADC3F7;
        padding: 5px 5px 10px 50px;
    }
</style>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Price  Rejects"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View Upload Process Log"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div id="divStatus" runat="server" visible="false" class="message" style="text-align: left;
                font-size: 12px;">
            </div>
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
            <asp:GridView ID="gvUploadReject" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ShowFooter="true" CssClass="GridViewStyle" AllowSorting="true">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <%--Check Boxes--%>
                    <%--<asp:TemplateField HeaderText="Select Records">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnEditSelected" CssClass="FieldName" 
                                runat="server" Text="Save" />
                        </FooterTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="SchemeOrScripCode" HeaderText="Scheme/Scrip Code" />
                    <asp:BoundField DataField="SchemeOrScripName" HeaderText="Scheme /Scrip Name" />
                    <asp:BoundField DataField="SourceName" HeaderText="Source Name" />
                    <asp:BoundField DataField="RejectReason" HeaderText="Reject Reason" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trReprocess" runat="server">
        <td align="left">
            <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
                CssClass="PCGButton" />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyMsg" class="FieldName">
                There are no records to be displayed!</label>
        </td>
    </tr>
    <tr id="trErrorMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Message" runat="server">
            </asp:Label>
        </td>
    </tr>
</table>
<div id="DivPager" runat="server">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <uc1:Pager ID="mypager" runat="server" />
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
