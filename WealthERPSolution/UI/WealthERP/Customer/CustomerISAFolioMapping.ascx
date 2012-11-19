<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="CustomerISAFolioMapping.ascx.cs"
    Inherits="WealthERP.Customer.CustomerISAFolioMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="4">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            ISA to Folio mapping
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Customer Selection
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="width: 50%">
            <table width="100%">
                <tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblMemberBranch" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMemberBranch" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMemberBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblMember" runat="server" CssClass="FieldName" Text="Member Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtMember" runat="server" CssClass="txtField" AutoComplete="Off"
                            AutoPostBack="True"></asp:TextBox>
                        <span id="Span8" class="spnRequiredField">*</span>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtMember_water" TargetControlID="txtMember"
                            WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="txtMember_autoCompleteExtender" runat="server"
                            TargetControlID="txtMember" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                            MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                            CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                            CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                            Enabled="True" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMember"
                            ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                            CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblPan" runat="server" CssClass="FieldName" Text="PAN:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblGetPan" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblCustomerISAAccount" runat="server" CssClass="FieldName" Text="Pick ISA:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomerISAAccount" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlCustomerISAAccount_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblISAHoldingType" runat="server" CssClass="FieldName" Text="ISA Holding Type:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblISAHoldingTypeValue" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td colspan="2" style="width: 50%">
            <table style="width: 100%;" id="tblAssociate" runat="server">
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr id="trAssociate" runat="server">
                    <td id="tdNominees" align="left" style="padding-left: 30px;" runat="server">
                        <asp:Panel ID="pnlNominiees" runat="server" Height="140px">
                            <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                DataKeyNames="MemberCustomerId, AssociationId" CssClass="GridViewStyle">
                                <FooterStyle CssClass="FooterStyle" />
                                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                    <td id="tdJointHolders" align="left" style="padding-left: 30px;" runat="server">
                        <asp:Panel ID="pnlJointholders" runat="server" Height="140px">
                            <asp:GridView ID="gvJointHoldersList" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" DataKeyNames="AssociationId" CssClass="GridViewStyle">
                                <FooterStyle CssClass="FooterStyle" />
                                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Folio ISA Mapping
            </div>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
