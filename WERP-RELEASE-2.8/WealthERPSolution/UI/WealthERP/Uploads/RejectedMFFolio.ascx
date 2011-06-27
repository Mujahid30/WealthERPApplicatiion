<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedMFFolio.ascx.cs" Inherits="WealthERP.Uploads.RejectedMFFolio" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


<script type="text/javascript" src="../Scripts/JScript.js"></script>

<asp:Panel ID="pnl" DefaultButton="btnGridSearch" runat="server">
    <table style="width: 100%" class="TableBackground">
        <tr>
            <td>
               <asp:Label Text="Mutual Fund Folio Rejects" ID="lblHeader" CssClass="HeaderTextBig" runat="server"></asp:Label>
            </td>
        </tr>
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
            <asp:LinkButton ID="LinkInputRejects" runat="server" 
                Text="View Input Rejects" CssClass="LinkButtons" 
                onclick="LinkInputRejects_Click"></asp:LinkButton>
                <%--<asp:LinkButton runat="server" ID="LinkButton1" CssClass="LinkButtons" Text="Back"
                    OnClick="lnkProfile_Click"></asp:LinkButton>--%>
                <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvCAMSProfileReject" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ShowFooter="true" CssClass="GridViewStyle" AllowSorting="true"
                    DataKeyNames="MFFolioStagingId,MainStagingId,ProcessID" >
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                      <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBx" runat="server" />
                            </ItemTemplate>
                           <FooterTemplate>
                                <asp:Button ID="btnSave" CssClass="FieldName" OnClick="btnSave_Click" runat="server"
                                    Text="Save" />
                            </FooterTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                                <asp:DropDownList ID="ddlRejectReason" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRejectReasonHeader" runat="server" Text='<%# Eval("RejectReason").ToString() %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                      
                        <asp:BoundField DataField="ProcessID" HeaderText="ProcessId" />
                        <asp:BoundField DataField="WERPCUstomerName" HeaderText="WERP Name" SortExpression="WERPCUstomerName" />
                        <%--<asp:BoundField DataField="CustomerExists" HeaderText="Is Customer Existing" />--%>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblCustomerExists" runat="server" Text="Is Customer Existing"></asp:Label>
                                <asp:DropDownList ID="ddlCustomerExists" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCustomerExists_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerExistsHeader" runat="server" Text='<%# Eval("CustomerExists").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                <asp:TextBox ID="txtNameSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedCAMSProfile_btnGridSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNameHeader" runat="server" Text='<%# Eval("WERPCUstomerName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="NAME" HeaderText="Name" />--%>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblPan" runat="server" Text="PAN Number"></asp:Label>
                                <asp:TextBox ID="txtPanSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedCAMSProfile_btnGridSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtPan" runat="server" CssClass="txtField" Text='<%# Bind("PANNumber") %>'></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPanMultiple" CssClass="txtField" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblFolio" runat="server" Text="Folio"></asp:Label>
                                <asp:TextBox ID="txtFolioSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedCAMSProfile_btnGridSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtFolio" CssClass="txtField" runat="server" Text='<%# Bind("Folio") %>'></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFolioMultiple" CssClass="txtField" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AMC" HeaderText="AMC" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblIsRejected" runat="server" Text="Is Rejected"></asp:Label>
                                <asp:DropDownList ID="ddlIsRejected" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlIsRejected_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIsRejectedHeader" runat="server" Text='<%# Eval("IsRejected").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr id="trReprocess" runat="server">
            <td class="SubmitCell">
                <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
                    CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RejectedMFFolio_btnReprocess','S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RejectedMFFolio_btnReprocess','S');" />
            </td>
        </tr>
        <tr id="trProfileMessage" runat="server" visible="false">
            <td class="Message">
                <asp:Label ID="lblEmptyMsg" runat="server" Text="There are no records to be displayed!">
                </asp:Label>
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
    <asp:Button ID="btnGridSearch" runat="server" Text="" OnClick="btnGridSearch_Click"
        BorderStyle="None" BackColor="Transparent" />
    <asp:HiddenField ID="hdnRecordCount" runat="server" />
    <asp:HiddenField ID="hdnCurrentPage" runat="server" />
    <asp:HiddenField ID="hdnSortProcessID" runat="server" Value="WERPCUstomerName ASC" />
    <asp:HiddenField ID="hdnPANFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnIsCustomerExistingFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnIsRejectedFilter" runat="server" Visible="false" />
</asp:Panel>
