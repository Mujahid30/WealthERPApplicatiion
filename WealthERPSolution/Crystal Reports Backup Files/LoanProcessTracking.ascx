<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoanProcessTracking.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.LoanProcessTracking" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script type="text/javascript" language="javascript">
    function hb1(clicked) //Show or hide the left_menu
    {
        if (clicked == 'left') {
            // Clicked on the left bar
            if (document.getElementById('left_menu').style.display == 'none') {
                // If left menu is invisble
                document.getElementById('left_menu').style.display = 'block';

                document.getElementById('splitter_bar_left').style.backgroundImage = 'url(Images/splitter_bar_left_pointer.jpg)';
                document.getElementById('splitter_bar_left').style.cssFloat = 'left';
                document.getElementById('splitter_bar_left').style.styleFloat = 'left';
                document.getElementById('content').style.cssFloat = 'left';
                document.getElementById('content').style.styleFloat = 'left';
                if (document.getElementById('right_menu').style.display == 'block') {
                    // if right menu is visible and left is visible
                    document.getElementById('content').style.width = "68.25%";
                }
                else if (document.getElementById('right_menu').style.display == 'none') {
                    // If right menu is invisible and left is invisible
                    document.getElementById('content').style.width = "81.25%";
                }
                document.getElementById('content').style.position = 'relative';
            }
            else if (document.getElementById('left_menu').style.display == 'block') {
                // If left menu is Visible
                document.getElementById('left_menu').style.display = 'none';

                document.getElementById('splitter_bar_left').style.backgroundImage = 'url(Images/splitter_bar_right_pointer.jpg)';
                document.getElementById('splitter_bar_left').style.cssFloat = 'left';
                document.getElementById('splitter_bar_left').style.styleFloat = 'left';
                document.getElementById('content').style.cssFloat = 'left';
                document.getElementById('content').style.styleFloat = 'left';
                if (document.getElementById('right_menu').style.display == 'block') {
                    // If right menu is visible and left is invisible
                    document.getElementById('content').style.width = "85.25%";
                }
                else if (document.getElementById('right_menu').style.display == 'none') {
                    // If right menu is invisible and left is visible
                    document.getElementById('content').style.width = "98.25%";
                }
                document.getElementById('content').style.position = 'relative';
            }
        }
        else if (clicked == 'right') {
            if (document.getElementById('right_menu').style.display == 'none') {
                document.getElementById('right_menu').style.display = 'block';

                document.getElementById('splitter_bar_right').style.backgroundImage = 'url(Images/splitter_bar_right_pointer.jpg)';
                document.getElementById('splitter_bar_right').style.cssFloat = 'left';
                document.getElementById('splitter_bar_right').style.styleFloat = 'left';
                document.getElementById('content').style.cssFloat = 'left';
                document.getElementById('content').style.styleFloat = 'left';
                if (document.getElementById('left_menu').style.display == 'block') {
                    document.getElementById('content').style.width = "68.25%";
                }
                else if (document.getElementById('left_menu').style.display == 'none') {
                    document.getElementById('content').style.width = "86.25%";
                }
                document.getElementById('content').style.position = 'relative';
            }
            else if (document.getElementById('right_menu').style.display == 'block') {
                document.getElementById('right_menu').style.display = 'none';

                document.getElementById('splitter_bar_right').style.backgroundImage = 'url(Images/splitter_bar_left_pointer.jpg)';
                document.getElementById('splitter_bar_right').style.cssFloat = 'left';
                document.getElementById('splitter_bar_right').style.styleFloat = 'left';
                document.getElementById('content').style.cssFloat = 'left';
                document.getElementById('content').style.styleFloat = 'left';
                if (document.getElementById('left_menu').style.display == 'block') {
                    document.getElementById('content').style.width = "81.25%";
                }
                else if (document.getElementById('left_menu').style.display == 'none') {
                    document.getElementById('content').style.width = "98.25%";
                }
                document.getElementById('content').style.position = 'relative';
            }
        }
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="divContainer" style="width: auto;height: 570px; background-color: AliceBlue;">
    <table class="TableBackground">
        <tr>
            <td colspan="5" class="HeaderCell">
                <asp:Label ID="lblHeader" Text="Loan Proposal Details" runat="server" CssClass="HeaderTextBig"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:LinkButton ID="lnkBtnBack" runat="server" Text="Back" OnClick="lnkBtnBack_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Edit" OnClick="lnkBtnEdit_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 500px;">
                <asp:Label ID="lblDecisionStatus" Text="Stage Decision Status" runat="server" CssClass="HeaderTextSmall">
                </asp:Label>
                <hr />
                <hr />
                <asp:Panel ID="pnlApplicationEntry" runat="server">
                    <table style="width: 375px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblApplicationEntry" Text="Stage1: Application Entry" runat="server"
                                    CssClass="HeaderTextSmall">
                                </asp:Label>
                                <asp:Image ID="imgApplicationEntry" runat="server" />
                                <img runat="server" id="imgApplicationStatus" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlApplicationEntryContent" runat="server">
                    <table style="width: 375px;">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkDocCollection" runat="server" Text="Document Collection" CssClass="txtField" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkEntry" runat="server" Text="Entry" CssClass="txtField" />
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblRemark" Text="Remark:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtApplicationEntryRemark" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnApplicationEntry" runat="server" CssClass="PCGButton" Text="Submit"
                                    OnClick="btnApplicationEntry_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LoanProcessTracking_btnApplicationEntry', 'S');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LoanProcessTracking_btnApplicationEntry', 'S');" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvApplEntryDecisionLog" runat="server" AutoGenerateColumns="False"
                                    CssClass="GridViewStyle" ShowFooter="True">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle CssClass="FooterStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="LogDetails" HeaderText="Decision Log Details" NullDisplayText="NA" />
                                        <asp:BoundField DataField="LogDate" HeaderText="Date" NullDisplayText="NA" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlApplicationEntryContent"
                    ExpandControlID="pnlApplicationEntry" CollapseControlID="pnlApplicationEntry"
                    ImageControlID="imgApplicationEntry" ExpandedImage="~/Images/arrow_double_up_7.gif"
                    CollapsedImage="~/Images/arrow_double_down_7.gif" Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
                <br />
                <asp:Panel ID="pnlEligibilityStatus" runat="server">
                    <table style="width: 375px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblEligibilityStatus" Text="Stage2: Eligibility Status" runat="server"
                                    CssClass="HeaderTextSmall">
                                </asp:Label>
                                <asp:Image ID="imgEligibilityStatus" runat="server" CssClass="" />
                                <asp:Image ID="imgEligibilityStatus2" runat="server" CssClass="" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlEligibilityStatusContent" runat="server">
                    <table style="width: 375px;">
                        <tr>
                            <td colspan="2">
                                <asp:RadioButton ID="rbtnEligibilityApproved" runat="server" Text="Approved" GroupName="Eligibility"
                                    CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbtnEligibility_CheckedChanged" />
                                <asp:RadioButton ID="rbtnEligibilityDeclined" runat="server" Text="Declined" GroupName="Eligibility"
                                    CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbtnEligibility_CheckedChanged" />
                                <asp:RadioButton ID="rbtnEligibilityAdditionalInfo" runat="server" Text="Additional Information Required"
                                    GroupName="Eligibility" CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbtnEligibility_CheckedChanged" />
                            </td>
                        </tr>
                        <tr id="trEligiDeclineReason" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblEligibilityDeclineReason" Text="Decline Reason:" runat="server"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlEligibilityDeclineReason" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblEligibilityRemark" Text="Remark:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtEligibilityRemark" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnEligibility" runat="server" CssClass="PCGButton" Text="Submit"
                                    OnClick="btnEligibility_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LoanProcessTracking_btnEligibility', 'S');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LoanProcessTracking_btnEligibility', 'S');" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvEligibilityDecisionLog" runat="server" AutoGenerateColumns="False"
                                    CssClass="GridViewStyle" ShowFooter="True">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle CssClass="FooterStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="LogDetails" HeaderText="Decision Log Details" NullDisplayText="NA" />
                                        <asp:BoundField DataField="LogDate" HeaderText="Date" NullDisplayText="NA" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="pnlEligibilityStatusContent"
                    ExpandControlID="pnlEligibilityStatus" CollapseControlID="pnlEligibilityStatus"
                    ImageControlID="imgEligibilityStatus" ExpandedImage="~/Images/arrow_double_up_7.gif"
                    CollapsedImage="~/Images/arrow_double_down_7.gif" Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
                <br />
                <asp:Panel ID="pnlBankLoanSanction" runat="server">
                    <table style="width: 375px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblBankLoanSanction" Text="Stage3: Bank Loan Sanction Decision" runat="server"
                                    CssClass="HeaderTextSmall">
                                </asp:Label>
                                <asp:Image ID="imgBankLoanSanction" runat="server" CssClass="" />
                                <asp:Image ID="imgSanctionStatus" runat="server" CssClass="" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlBankLoanSanctionContent" runat="server">
                    <table style="width: 375px;">
                        <tr>
                            <td colspan="2">
                                <asp:RadioButton ID="rbtnLoanSanctionApproved" runat="server" Text="Approved" GroupName="LoanSanction"
                                    CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbtnLoanSanction_CheckedChanged" />
                                <asp:RadioButton ID="rbtnLoanSanctionDeclined" runat="server" Text="Declined" GroupName="LoanSanction"
                                    CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbtnLoanSanction_CheckedChanged" />
                                <asp:RadioButton ID="rbtnLoanSanctionAdditionalInfo" runat="server" Text="Additional Information Required"
                                    GroupName="LoanSanction" CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbtnLoanSanction_CheckedChanged" />
                            </td>
                        </tr>
                        <tr id="trLoanSanctionReason" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblLoanSanctionDeclineReason" Text="Decline Reason:" runat="server"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlLoanSanctionDeclineReason" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblLoanSanctionRemark" Text="Remark:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtLoanSanctionRemark" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnLoanSanction" runat="server" CssClass="PCGButton" Text="Submit"
                                    OnClick="btnLoanSanction_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LoanProcessTracking_btnLoanSanction', 'S');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LoanProcessTracking_btnLoanSanction', 'S');" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvLoanSanctionDecisionLog" runat="server" AutoGenerateColumns="False"
                                    CssClass="GridViewStyle" ShowFooter="True">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle CssClass="FooterStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="LogDetails" HeaderText="Decision Log Details" NullDisplayText="NA" />
                                        <asp:BoundField DataField="LogDate" HeaderText="Date" NullDisplayText="NA" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlBankLoanSanctionContent"
                    ExpandControlID="pnlBankLoanSanction" CollapseControlID="pnlBankLoanSanction"
                    ImageControlID="imgBankLoanSanction" ExpandedImage="~/Images/arrow_double_up_7.gif"
                    CollapsedImage="~/Images/arrow_double_down_7.gif" Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
                <br />
                <asp:Panel ID="pnlLoanDisbursal" runat="server">
                    <table style="width: 375px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblLoanDisbursal" Text="Stage4: Loan Disbursal" runat="server" CssClass="HeaderTextSmall">
                                </asp:Label>
                                <asp:Image ID="imgLoanDisbursal" runat="server" CssClass="" />
                                <asp:Image ID="imgDisbursalStatus" runat="server" CssClass="" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlLoanDisbursalContent" runat="server">
                    <table style="width: 375px;">
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="chkLoanDisbursed" runat="server" Text="Loan Disbursed" CssClass="txtField" />
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblLoanDisbursedRemark" Text="Remark:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtLoanDisbursedRemark" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnLoanDisbursed" runat="server" CssClass="PCGButton" Text="Submit"
                                    OnClick="btnLoanDisbursed_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LoanProcessTracking_btnLoanDisbursed', 'S');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LoanProcessTracking_btnLoanDisbursed', 'S');" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvLoanDisbursal" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    ShowFooter="True">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle CssClass="FooterStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="LogDetails" HeaderText="Decision Log Details" NullDisplayText="NA" />
                                        <asp:BoundField DataField="LogDate" HeaderText="Date" NullDisplayText="NA" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" TargetControlID="pnlLoanDisbursalContent"
                    ExpandControlID="pnlLoanDisbursal" CollapseControlID="pnlLoanDisbursal" ImageControlID="imgLoanDisbursal"
                    ExpandedImage="~/Images/arrow_double_up_7.gif" CollapsedImage="~/Images/arrow_double_down_7.gif"
                    Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
                <br />
                <asp:Panel ID="pnlClosure" runat="server">
                    <table style="width: 375px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblClosure" Text="Stage5: Closure" runat="server" CssClass="HeaderTextSmall">
                                </asp:Label>
                                <asp:Image ID="imgClosure" runat="server" CssClass="" />
                                <asp:Image ID="imgClosureStatus" runat="server" CssClass="" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlClosureContent" runat="server">
                    <table style="width: 375px;">
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="chkLoanClosed" runat="server" Text="Closed" CssClass="txtField" />
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblClosureRemark" Text="Remark:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtClosureRemark" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnClosure" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnClosure_Click"
                                    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LoanProcessTracking_btnClosure', 'S');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LoanProcessTracking_btnClosure', 'S');" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvClosure" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    ShowFooter="True">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle CssClass="FooterStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="LogDetails" HeaderText="Decision Log Details" NullDisplayText="NA" />
                                        <asp:BoundField DataField="LogDate" HeaderText="Date" NullDisplayText="NA" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server" TargetControlID="pnlClosureContent"
                    ExpandControlID="pnlClosure" CollapseControlID="pnlClosure" ImageControlID="imgClosure"
                    ExpandedImage="~/Images/arrow_double_up_7.gif" CollapsedImage="~/Images/arrow_double_down_7.gif"
                    Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
            </td>
            <td id="splitter_bar_left" style="display: block" onclick="javascript:hb1('left');">
                &nbsp;
            </td>
            <td style="vertical-align: top; width: 475px;">
                <asp:Label ID="lblDetails" Text="Details" runat="server" CssClass="HeaderTextSmall">
                </asp:Label>
                <hr />
                <hr />
                <table style="width: 475px;">
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <%--<tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAction" Text="What do you want to do? :" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                            <asp:ListItem Text="Select Action" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Add Proposal" Value="add"></asp:ListItem>
                            <asp:ListItem Text="View Proposal" Value="view"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="trAssociate" runat="server">
                        <td class="leftField">
                            <asp:Label ID="lblAssociateName" Text="Associate Name:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" colspan="3">
                            <asp:Label ID="lblAssociate" Text="" runat="server" CssClass="Field"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblLoanPartner" Text="Loan Partner:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" colspan="3">
                            <asp:DropDownList ID="ddlLoanPartner" CssClass="cmbField" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblLoanType" Text="Loan Type:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:DropDownList ID="ddlLoanType" CssClass="cmbField" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="leftField">
                            <asp:Label ID="lblScheme" Text="Scheme:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:DropDownList ID="ddlScheme" CssClass="cmbField" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblClientName" Text="Client Name:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:DropDownList ID="ddlClientName" CssClass="cmbField" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" class="rightField">
                            <asp:CheckBox ID="chkIsMainBorrower" runat="server" Text="Is Main Borrower a Minor"
                                AutoPostBack="true" OnCheckedChanged="chkIsMainBorrower_CheckedChanged" CssClass="txtField" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblNoOfCustomers" Text="No. of Co-Borrowers:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td colspan="3" class="rightField">
                            <asp:TextBox ID="txtNoOfCustomers" runat="server" CssClass="txtField"></asp:TextBox>
                            <asp:Button ID="btnGo" runat="server" CssClass="txtField" OnClick="btnGo_Click" Text="Go"
                                Width="25px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="dvCoBorrowers" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlAsset" runat="server">
                    <table style="width: 475px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblAssetDetails" Text="Asset Details" runat="server" CssClass="HeaderTextSmall"></asp:Label>
                                <asp:Image ID="imgAsset" runat="server" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnAddAsset" runat="server" CssClass="PCGButton" Text="Add Asset"
                                    OnClick="btnAddAsset_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LoanProcessTracking_btnAddAsset', 'S');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LoanProcessTracking_btnAddAsset', 'S');" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlAssetContent" runat="server">
                    <table style="width: 475px;">
                        <tr>
                            <td>
                                &nbsp;
                                <%--<asp:GridView ID="gvAssets" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                DataKeyNames="AssetId" CssClass="GridViewStyle" ShowFooter="True">
                                <RowStyle CssClass="RowStyle" />
                                <FooterStyle CssClass="FooterStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="DecisionLog" HeaderText="Decision Log Details" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" />
                                </Columns>
                            </asp:GridView>--%>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnl" runat="server">
                    <table style="width: 475px;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblIntroducer" Text="Introducer:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtIntroducer" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblGuarantorName" Text="Guarantor Name:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlGuarantorName" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPickAsset" runat="server">
                    <table style="width: 475px;">
                        <tr id="trPickAsset" runat="server">
                            <td>
                                <asp:Label ID="lblPickAsset" Text="Pick an Existing Asset" runat="server" CssClass="HeaderTextSmall"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr id="trPickAssetCheckList" runat="server">
                            <td>
                                <asp:CheckBoxList ID="chklstAssets" runat="server" CssClass="txtField" RepeatDirection="Horizontal"
                                    RepeatColumns="10" RepeatLayout="Table">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPickAssets" runat="server">
                    <table style="width: 475px;">
                        <tr id="trExistingAssets" runat="server">
                            <td class="leftField">
                                <asp:Label ID="Label1" Text="Pick an Asset:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlExistingAssets" runat="server" CssClass="cmbField" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlExistingAssets_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trExistingAssetsSpace" runat="server">
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trExistingAssetsGrid" runat="server">
                            <td colspan="2">
                                <asp:GridView ID="gvCoBorrower" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    CssClass="GridViewStyle" DataKeyNames="CLA_LiabilitiesAssociationId,CustomerId,AssociationId"
                                    AllowSorting="true" OnRowDataBound="gvCoBorrower_RowDataBound" EnableViewState="true">
                                    <FooterStyle CssClass="FieldName" />
                                    <RowStyle CssClass="RowStyle" />
                                    <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Borrowers" SortExpression="Name" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="Asset Ownership">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAssetOwnership" runat="server" CssClass="txtField"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Obligation %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLoanObligation" CssClass="txtField" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMargin" CssClass="txtField" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <%--<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender9" runat="server" TargetControlID="pnlAssetContent"
                ExpandControlID="pnlAsset" CollapseControlID="pnlAsset" ImageControlID="imgAsset"
                ExpandedImage="~/Images/arrow_double_up_7.gif" CollapsedImage="~/Images/arrow_double_down_7.gif"
                Collapsed="false" SuppressPostBack="true">
            </cc1:CollapsiblePanelExtender>--%>
                <asp:Panel ID="pnlEligibilityQual" runat="server">
                    <table style="width: 475px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblEligibilityHeader" Text="Eligibility Qualification" runat="server"
                                    CssClass="HeaderTextSmall"></asp:Label>
                                <asp:Image ID="imgEligibilityQual" runat="server" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlEligibilityQualContent" runat="server">
                    <table style="width: 475px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblEligiCriteriaTableHeader" Text="Eligibility Criteria" runat="server"
                                    CssClass="HeaderTextSmall"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeDetailsTableHeader" Text="Scheme Details" runat="server"
                                    CssClass="HeaderTextSmall"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCustDetailsTableHeader" Text="Customer & Co-Borrower Details" runat="server"
                                    CssClass="HeaderTextSmall"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAge" Text="Age" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeAgeValue" Text="" runat="server" CssClass="Field"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAgeValue" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblResidence" Text="Residence Stability" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeResidenceValue" Text="" runat="server" CssClass="Field"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtResidenceValue" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblJobStab" Text="Job Stability" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeJobStabValue" Text="" runat="server" CssClass="Field"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJobStabValue" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIncome" Text="Income" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeIncomeValue" Text="" runat="server" CssClass="Field"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIncomeValue" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblExpense" Text="Expense" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeExpenseValue" Text="" runat="server" CssClass="Field"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExpenseValue" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNetworth" Text="Networth" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSchemeNetworthValue" Text="Networth" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNetworthValue" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server" TargetControlID="pnlEligibilityQualContent"
                    ExpandControlID="pnlEligibilityQual" CollapseControlID="pnlEligibilityQual" ImageControlID="imgEligibilityQual"
                    ExpandedImage="~/Images/arrow_double_up_7.gif" CollapsedImage="~/Images/arrow_double_down_7.gif"
                    Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
                <asp:Panel ID="pnlProposalDetails" runat="server">
                    <table style="width: 475px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblProposalDetailsHeader" Text="Proposal Details" runat="server" CssClass="HeaderTextSmall"></asp:Label>
                                <asp:Image ID="imgProposalDetails" runat="server" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlProposalDetailsContent" runat="server">
                    <table style="width: 475px;">
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblApplicationNo" Text="Application No.:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblAppliedLoanAmt" Text="Applied Loan Amount:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtAppliedLoanAmt" runat="server" CssClass="txtField" AutoPostBack="true"
                                    OnTextChanged="txtAppliedLoanAmt_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblAppliedLoanPeriod" Text="Applied Loan Period(months):" runat="server"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtAppliedLoanPeriod" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblInterestCategory" Text="Interest Category:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlInterestCat" runat="server" CssClass="cmbField" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlInterestCat_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" colspan="2">
                                <asp:Label ID="lblInterestType" Text="Is Floating Interest Rate?" runat="server"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="2">
                                <asp:RadioButton ID="rbtnYes" Text="Yes" runat="server" CssClass="txtField" GroupName="InterestType" />
                                &nbsp;<asp:RadioButton ID="rbtnNo" Text="No" runat="server" CssClass="txtField" GroupName="InterestType" />
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblInterestRate" Text="Interest Rate %:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtInterestRate" runat="server" CssClass="txtField" AutoPostBack="true"
                                    OnTextChanged="txtInterestRate_TextChanged"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblMargin" Text="Margin %:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMargin" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblProcessCharges" Text="Processing Charges(%):" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtProcessCharges" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblPreclosingCharges" Text="Preclosing Charges(%):" runat="server"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtPreclosingCharges" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender7" runat="server" TargetControlID="pnlProposalDetailsContent"
                    ExpandControlID="pnlProposalDetails" CollapseControlID="pnlProposalDetails" ImageControlID="imgProposalDetails"
                    ExpandedImage="~/Images/arrow_double_up_7.gif" CollapsedImage="~/Images/arrow_double_down_7.gif"
                    Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
                <asp:Panel ID="pnlSanction" runat="server">
                    <table style="width: 475px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblSanctionHeader" Text="Sanction Loan Details" runat="server" CssClass="HeaderTextSmall"></asp:Label>
                                <asp:Image ID="imgSanction" runat="server" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlSanctionContent" runat="server">
                    <table style="width: 475px;">
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblSanctionDate" Text="Sanction Date:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtSanctionDate" runat="server" CssClass="txtField"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtSanctionDate_CalendarExtender" runat="server" TargetControlID="txtSanctionDate">
                                </cc1:CalendarExtender>
                                <cc1:TextBoxWatermarkExtender ID="txtSanctionDate_TextBoxWatermarkExtender" runat="server"
                                    TargetControlID="txtSanctionDate" WatermarkText="dd/mm/yyyy">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblBankReference" Text="Bank Reference No:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBankReference" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblSanctionAmount" Text="Sanction Amount:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtSanctionAmount" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblSanctionInterestRate" Text="Sanction Interest Rate %:" runat="server"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtSanctionInterestRate" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender8" runat="server" TargetControlID="pnlSanctionContent"
                    ExpandControlID="pnlSanction" CollapseControlID="pnlSanction" ImageControlID="imgSanction"
                    ExpandedImage="~/Images/arrow_double_up_7.gif" CollapsedImage="~/Images/arrow_double_down_7.gif"
                    Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
                <asp:Panel ID="pnlEMI" runat="server">
                    <table style="width: 475px; cursor: pointer;">
                        <tr>
                            <td>
                                <asp:Label ID="lblEMIHeader" Text="EMI Details" runat="server" CssClass="HeaderTextSmall"></asp:Label>
                                <asp:Image ID="imgEMI" runat="server" />
                                <hr />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlEMIContent" runat="server">
                    <table style="width: 475px;">
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblEMIAmount" Text="EMI Amount:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblEMIDate" Text="EMI Date:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlEMIDate" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblRepaymentType" Text="Repayment Type:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlRepaymentType" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblEMIFrequency" Text="EMI Frequency:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlEMIFrequency" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblNoOfInstallments" Text="No. of Installments:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtNoOfInstallments" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblAmountPrepaid" Text="Amount Prepaid:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtAmountPrepaid" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblInstallStartDate" Text="Installment Start Date:" runat="server"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtInstallStartDate" runat="server" CssClass="txtField"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtInstallStartDate_CalendarExtender" runat="server" TargetControlID="txtInstallStartDate">
                                </cc1:CalendarExtender>
                                <cc1:TextBoxWatermarkExtender ID="txtInstallStartDate_TextBoxWatermarkExtender" runat="server"
                                    TargetControlID="txtInstallStartDate" WatermarkText="dd/mm/yyyy">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblInstallEndDate" Text="Installment End Date:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtInstallEndDate" runat="server" CssClass="txtField"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtInstallEndDate_CalendarExtender" runat="server" TargetControlID="txtInstallEndDate">
                                </cc1:CalendarExtender>
                                <cc1:TextBoxWatermarkExtender ID="txtInstallEndDate_TextBoxWatermarkExtender" runat="server"
                                    TargetControlID="txtInstallEndDate" WatermarkText="dd/mm/yyyy">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender10" runat="server" TargetControlID="pnlEMIContent"
                    ExpandControlID="pnlEMI" CollapseControlID="pnlEMI" ImageControlID="imgEMI" ExpandedImage="~/Images/arrow_double_up_7.gif"
                    CollapsedImage="~/Images/arrow_double_down_7.gif" Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
                <asp:Panel ID="pnlRemarks" runat="server">
                    <table style="width: 475px;">
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblRemarks" Text="Remarks:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnSubmit_Click"
                                    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LoanProcessTracking_btnSubmit', 'S');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LoanProcessTracking_btnSubmit', 'S');" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td id="splitter_bar_right" style="display: block" onclick="javascript:hb1('right');">
                &nbsp;&nbsp;
            </td>
            <td style="vertical-align: top;" width="475px">
                <asp:Label ID="lblDocumentDetails" Text="Document Details" runat="server" CssClass="HeaderTextSmall">
                </asp:Label>
                <hr />
                <hr />
                <div id="dvDocDropDown" runat="server">
                    <asp:Label ID="lblDocCust" runat="server" CssClass="FieldName" Text="Select the Customer:">
                    </asp:Label>
                    <asp:DropDownList ID="ddlDocCustomerList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDocCustomerList_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <br />
                <asp:GridView ID="gvDocuments" AutoGenerateColumns="false" OnRowDataBound="gvDocuments_RowDatabound"
                    CellPadding="5" runat="server" CssClass="GridViewStyle" DataKeyNames="ProposalDocId,ProofTypeCode"
                    ShowFooter="true" EnableViewState="true">
                    <%--EnableViewState="true"--%>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBx" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnSubmitDocument" CssClass="ButtonField" OnClick="btnSubmitDocument_Click"
                                    runat="server" Text="Submit" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proof Name">
                            <ItemTemplate>
                                <asp:Label ID="lblProofName" runat="server" CssClass="FieldName" Text='<%# Bind("ProofName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Submission Date">
                            <ItemTemplate>
                                <asp:Label ID="lblSubmissionDate" runat="server" CssClass="FieldName">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Accepted">
                            <%--<HeaderTemplate>
                            <asp:Label ID="lblIsAcceptedHeader" runat="server" Text="Folio"></asp:Label>
                        </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:RadioButton ID="rbtnAcceptedYes" Text="Yes" runat="server" GroupName="Accepted" />
                                <asp:RadioButton ID="rbtnAcceptedNo" Text="No" runat="server" GroupName="Accepted" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accepted Date">
                            <ItemTemplate>
                                <asp:Label ID="lblAcceptedDate" runat="server" CssClass="FieldName">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accepted By">
                            <%--<HeaderTemplate>
                            <asp:Label ID="lblIsAcceptedHeader" runat="server" Text="Folio"></asp:Label>
                        </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:TextBox ID="txtAcceptedBy" runat="server" CssClass="FieldName" Text='<%# Bind("AcceptedBy") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Copy Type">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCopyType" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="lblAddDocuments" Text="Add Documents" runat="server" CssClass="HeaderTextSmall">
                </asp:Label>
                <hr />
                <hr />
                <asp:Panel ID="pnlAddDocuments" runat="server">
                    <table style="width: 475px">
                        <tr>
                            <td>
                                <asp:GridView ID="gvAddDocs" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    DataKeyNames="DocumentId" AllowSorting="True" CssClass="GridViewStyle" AutoGenerateEditButton="True"
                                    OnRowEditing="gvAddDocs_RowEditing" OnRowUpdating="gvAddDocs_RowUpdating" OnRowCancelingEdit="gvAddDocs_RowCancelingEdit"
                                    OnRowDataBound="gvAddDocs_RowDataBound">
                                    <FooterStyle CssClass="FooterStyle" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Proof Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProofType" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="cmbField" ID="ddlProofType_Edit" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList CssClass="cmbField" ID="ddlProofType_Add" runat="server">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Proof Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProofName" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox CssClass="txtField" ID="txtProofName_Edit" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox class="smallTxt" ID="txtProofName_Add" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Submission Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubDate" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblSubDate_Edit" runat="server" CssClass="FieldName"></asp:Label>
                                            </EditItemTemplate>
                                            <%--<FooterTemplate>
                                            <asp:TextBox class="smallTxt" ID="txtSubDate_Add" runat="server"></asp:TextBox>
                                        </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Is Accepted
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsAccepted" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:RadioButton ID="rbtnAcceptedYes_Edit" Text="Yes" runat="server" GroupName="Accepted_Edit" />
                                                <asp:RadioButton ID="rbtnAcceptedNo_Edit" Text="No" runat="server" GroupName="Accepted_Edit" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:RadioButton ID="rbtnAcceptedYes_Add" Text="Yes" runat="server" GroupName="Accepted_Add" />
                                                <asp:RadioButton ID="rbtnAcceptedNo_Add" Text="No" runat="server" GroupName="Accepted_Add" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Accepted Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcceptedDate" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label CssClass="FieldName" ID="lblAcceptedDate_Edit" runat="server"></asp:Label>
                                            </EditItemTemplate>
                                            <%--<FooterTemplate>
                                            <asp:TextBox class="smallTxt" ID="txtAcceptedDate_Add" runat="server"></asp:TextBox>
                                        </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Accepted By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcceptedBy" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox CssClass="txtField" ID="txtAcceptedBy_Edit" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox class="smallTxt" ID="txtAcceptedBy_Add" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Copy Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCopyType" runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList CssClass="cmbField" ID="ddlCopyType_Edit" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList CssClass="cmbField" ID="ddlCopyType_Add" runat="server">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddDoc" runat="server" Text="Add" CssClass="PCGButton" OnClick="btnAddDoc_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>

<script language="javascript" type="text/javascript">
    document.getElementById('divContainer').style.height = (screen.availHeight) * 66.5 / 100 + 'px';
</script>

