<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedWERPProfile.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedWERPProfile" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<link href="/YUI/build/container/assets/container.css" rel="stylesheet" type="text/css" />
<link href="/YUI/build/menu/assets/skins/sam/menu.css" rel="stylesheet" type="text/css" />

<script src="/YUI/build/utilities/utilities.js" type="text/javascript"></script>

<script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/yahoo/yahoo-min.js"></script>

<script src="/YUI/build/container/container-min.js" type="text/javascript"></script>
<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script type="text/javascript">
    function pageLoad() {
        InitDialogs();
        Loading(false);
    }

    function UpdateImg(ctrl, imgsrc) {
        var img = document.getElementById(ctrl);
        img.src = imgsrc;
    }

    // sets up all of the YUI dialog boxes
    function InitDialogs() {
        DialogBox_Loading = new YAHOO.widget.Panel("waitBox",
	{ fixedcenter: true, modal: true, visible: false,
	    width: "230px", close: false, draggable: true
	});
        DialogBox_Loading.setHeader("Processing, please wait...");
        DialogBox_Loading.setBody('<div style="text-align:center;"><img src="/Images/Wait.gif" id="Image1" /></div>');
        DialogBox_Loading.render(document.body);
    }
    function Loading(b) {
        if (b == true) {
            DialogBox_Loading.show();
        }
        else {
            DialogBox_Loading.hide();
        }
    }
</script>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Profile Rejects"></asp:Label>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessComplete" runat="server" class="success-msg" align="center"
                visible="false">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessincomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Reprocess Failed!
            </div>
        </td>
    </tr>
</table>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:LinkButton ID="lnkProfile" runat="server" OnClick="lnkProfile_Click" CssClass="LinkButtons"></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="LinkInputRejects" runat="server" Text="View Input Rejects"
                CssClass="LinkButtons" OnClick="LinkInputRejects_Click"></asp:LinkButton>
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvWERPProfileReject" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ShowFooter="true" CssClass="GridViewStyle" DataKeyNames="WERPProfileStagingId"
                AllowSorting="true" OnSorting="gvWERPProfileReject_Sort">
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
                            <asp:CheckBox ID="chkBxWerp" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnEditSelectedWerp" CssClass="FieldName" OnClick="btnEditSelectedWerp_Click"
                                runat="server" Text="Save" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                            <asp:DropDownList ID="ddlRejectReason" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged"
                                CssClass="cmbLongField">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRejectReasonHeader" runat="server" Text='<%# Eval("RejectReason").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="WERPCustomerName" HeaderText="CustomerName" SortExpression="WERPCustomerName" />--%>
                    <%--<asp:BoundField DataField="CustomerExists" HeaderText="Does Customer Exist" />--%>
                    <%--<asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblCustomerExists" runat="server" Text="Is Customer Existing"></asp:Label>
                            <asp:DropDownList ID="ddlCustomerExists" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCustomerExists_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                           <!-- <asp:Label ID="lblCustomerExistsHeader" runat="server" Text='<%# Eval("CustomerExists").ToString() %>'></asp:Label> -->
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:BoundField DataField="NAME" HeaderText="Customer Name" />--%>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblName" runat="server" Text="Customer Name"></asp:Label>
                            <asp:TextBox ID="txtNameSearch" Text='<%# hdnCustomerNameFilter.Value %>' runat="server"
                                CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedWERPProfile_btnGridSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNameHeader" runat="server" Text='<%# Eval("Name").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblPan" runat="server" Text="PAN Number"></asp:Label>
                            <asp:DropDownList ID="ddlPanNumber" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPanNumber_SelectedIndexChanged"
                                CssClass="GridViewCmbField">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtPanWerp" runat="server" Text='<%# Bind("PANNumber") %>' CssClass="txtField"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPanMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblBroker" runat="server" Text="Broker Code"></asp:Label>
                            <asp:TextBox ID="txtBrokerSearch" Text='<%# hdnBrokerCodeFilter.Value %>' runat="server"
                                CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedWERPProfile_btnGridSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtBroker" runat="server" Text='<%# Bind("BrokerCode") %>' CssClass="txtField"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtBrokerMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CUSTADDRESS" HeaderText="Address" />
                    <asp:BoundField DataField="Pincode" HeaderText="PinCode" />
                    <asp:BoundField DataField="ProcessID" HeaderText="ProcessId" />
                    <%--<asp:BoundField DataField="Scheme" HeaderText="Scheme" />--%>
                    <%--<asp:BoundField DataField="IsRejected" HeaderText="Is Rejected" />--%>
                    <%--      <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblIsRejected" runat="server" Text="Is Rejected"></asp:Label>
                            <asp:DropDownList ID="ddlIsRejected" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlIsRejected_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIsRejectedHeader" runat="server" Text='<%# Eval("IsRejected").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:BoundField DataField="RejectReason" HeaderText="Reject Reason" />--%>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
                CssClass="PCGButton" OnClientClick="Loading(true);"     />
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
<asp:HiddenField ID="hdnSort" runat="server" Value="WERPCustomerName ASC" />
<asp:HiddenField ID="hdnPANFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnBrokerCodeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCustomerNameFilter" runat="server" Visible="false" />
<asp:Button ID="btnGridSearch" runat="server" Text="" OnClick="btnGridSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
