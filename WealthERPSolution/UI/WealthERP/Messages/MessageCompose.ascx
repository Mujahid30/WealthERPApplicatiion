<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageCompose.ascx.cs"
    Inherits="WealthERP.Messages.MessageCompose" %>
<%@ Register TagPrefix="ajaxList" Namespace="DanLudwig.Controls.Web" Assembly="DanLudwig.Controls.AspAjax.ListBox" %>

<script language="javascript" type="text/javascript">
    function CheckBoxListSelect(cbControl, source) {
        var chkBoxList = document.getElementById(cbControl);
        var chkBoxAll = document.getElementById(source);
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = chkBoxAll.checked;
        }
        return false;
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Compose"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr>
        <td class="leftField">
            <asp:Label ID="lblSubject" Text="Subject:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox CssClass="txtField" ID="txtSubject" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
            <span id="span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvSubject" runat="server"
                ControlToValidate="txtSubject" ErrorMessage="<br/>Enter Message Subject" ValidationGroup="main"
                CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblMessage" Text="Message:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox CssClass="txtField" ID="txtMessage" MaxLength="2000" runat="server"
                TextMode="MultiLine" Rows="3" Style="width: 300px"></asp:TextBox>
            <span id="span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvMessage" runat="server"
                ControlToValidate="txtMessage" ErrorMessage="<br/>Enter Message Body" ValidationGroup="main"
                CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revMessage" runat="server" ValidationExpression="(\s|.){0,10000}"
                ControlToValidate="txtMessage" SetFocusOnError="true" ValidationGroup="main"
                ErrorMessage="<br/>Message body length should not be greater than 10,000 characters!"
                CssClass="cvPCG">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trSelectStaff" runat="server">
        <td class="leftField">
            <asp:Label ID="lblStaffRole" Text="Select Staff Role:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:CheckBox ID="chkbxAll" CssClass="cmbField" runat="server" Text="All" onclick="javascript:CheckBoxListSelect('ctrl_MessageCompose_ChkBxRoleList', 'ctrl_MessageCompose_chkbxAll')"
                OnCheckedChanged="ChkBxRoleList_SelectedIndexChanged" AutoPostBack="true" />
            <asp:CheckBoxList ID="ChkBxRoleList" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                CssClass="cmbField" RepeatLayout="Flow" OnSelectedIndexChanged="ChkBxRoleList_SelectedIndexChanged"
                AutoPostBack="true">
                <%--<asp:ListItem Value="0" onclick="javascript:CheckBoxListSelect('ctrl_MessageCompose_ChkBxRoleList')">All</asp:ListItem>--%>
                <asp:ListItem Value="1000">Advisor</asp:ListItem>
                <asp:ListItem Value="1002">BM</asp:ListItem>
                <asp:ListItem Value="1001">RM</asp:ListItem>
                <asp:ListItem Value="1005">Research</asp:ListItem>
                <asp:ListItem Value="1004">Ops</asp:ListItem>
                <asp:ListItem Value="1003">Customer</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
        </td>
        <td class="rightField">
            <table>
                <tr>
                    <td>
                        <div class="clearfix" style="margin-bottom: 1em;">
                            <asp:Panel ID="PLUser" runat="server" DefaultButton="AddSelected" Style="float: left">
                                <asp:Label ID="lblSelectUser" runat="server" CssClass="FieldName" Text="Select User"></asp:Label>
                                <asp:UpdatePanel ID="UPUser" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AddSelected" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="RemoveSelected" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="SelectAll" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="RemoveAll" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <ajaxList:ListBox ID="LBUser" runat="server" CssClass="DemoListBox" HorizontalScrollEnabled="true"
                                            Rows="12" ScrollStateEnabled="true" SelectionMode="Multiple">
                                        </ajaxList:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <div style="float: left; margin: 0.5em; width: 138px; font-size: 0.9em; text-align: center;">
                                <p>
                                    <asp:Button ID="AddSelected" runat="server" CssClass="PCGButton" Font-Bold="True"
                                        OnClick="AddSelected_Click" Text="&gt;" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_MessageCompose_AddSelected', 'S');"
                                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_MessageCompose_AddSelected', 'S');" />
                                </p>
                                <p>
                                    <asp:Button ID="RemoveSelected" runat="server" CssClass="PCGButton" Font-Bold="True"
                                        OnClick="RemoveSelected_Click" Text="&lt;" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_MessageCompose_RemoveSelected', 'S');"
                                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_MessageCompose_RemoveSelected', 'S');" />
                                </p>
                                <p>
                                    <asp:Button ID="SelectAll" runat="server" CssClass="PCGButton" Font-Bold="True" OnClick="SelectAll_Click"
                                        Text="&gt;&gt;" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_MessageCompose_SelectAll', 'S');"
                                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_MessageCompose_SelectAll', 'S');" />
                                </p>
                                <p>
                                    <asp:Button ID="RemoveAll" runat="server" CssClass="PCGButton" Font-Bold="True" OnClick="RemoveAll_Click"
                                        Text="&lt;&lt;" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_MessageCompose_RemoveAll', 'S');"
                                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_MessageCompose_RemoveAll', 'S');" />
                                </p>
                            </div>
                            <asp:Panel ID="PLSelectedUser" runat="server" DefaultButton="RemoveSelected" Style="float: left">
                                <asp:Label ID="lblSelectedUser" runat="server" CssClass="FieldName" Text="Selected Users"></asp:Label>
                                <asp:UpdatePanel ID="UPSelectedUser" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AddSelected" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="RemoveSelected" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="SelectAll" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="RemoveAll" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <ajaxList:ListBox ID="LBSelectedUser" runat="server" CssClass="DemoListBox" HorizontalScrollEnabled="true"
                                            Rows="12" ScrollStateEnabled="true" SelectionMode="Multiple">
                                        </ajaxList:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send" CssClass="PCGButton"
                ValidationGroup="main" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_MessageCompose_btnSend', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_MessageCompose_btnSend', 'S');" />
        </td>
    </tr>
</table>
