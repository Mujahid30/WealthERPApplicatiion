<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserRoleToTreeNodeMapping.ascx.cs"
    Inherits="WealthERP.AdvsierPreferenceSettings.AdviserRoleToTreeNodeMapping" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .fsSubmitButton
    {
        background-image: url(signup.png);
        background-repeat: no-repeat;
        background-color: transparent;
        height: 66px;
        width: 182px;
        border: none;
        text-indent: -999em;
    }
</style>

<script type="text/javascript">

    function OnClientNodeChecked(sender, args) {
        var node = args.get_node();

        for (var i = 0; i < node.get_allNodes().length; i++) {
            var childNode = node.get_allNodes()[i];
            if (node.get_checked()) {
                childNode.set_checked(true);
            }
            else {
                childNode.set_checked(false);
            }
        }
    }
</script>

<script type="text/javascript">
    function OnClientNodeChecked1(sender, args) {
        if (args.get_node()._hasChildren() == true)
            alert("Child Node :" + args.get_node()._getChildElements().length);
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Adviser Role To TreeNode Maping
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="LinkButtons" Text="Edit"
                                OnClick="lnkBtnEdit_Click"></asp:LinkButton>
                            &nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Back" Visible="false"
                                OnClick="lnlBack_Click"></asp:LinkButton>&nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnkDelete" CssClass="LinkButtons" Text="Delete"
                                Visible="false" OnClientClick="javascript: return confirm('Are you sure you want to Delete the Order?')"></asp:LinkButton>&nbsp;
                            <%-- OnClick="lnkDelete_Click"--%>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" runat="server" visible="false" style="padding-top: 20px;">
    <tr id="trSumbitSuccess">
        <td align="center">
            <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
            </div>
        </td>
    </tr>
</table>
<table width="35%" runat="server" id="tbRole" align="center">
    <tr>
        <%-- <td class="LeftLabel">
            &nbsp;&nbsp;
        </td>--%>
        <td>
            <asp:Label ID="lb1Role" runat="server" Text="Role:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRole" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="205px" OnSelectedIndexChanged="ddlRole_Selectedindexchanged">
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Role"
                CssClass="rfvPCG" ControlToValidate="ddlRole" ValidationGroup="btnMapingSubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lb1Level" runat="server" Text="Level:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlLevel" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="205px">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Role"
                CssClass="rfvPCG" ControlToValidate="ddlRole" ValidationGroup="btnMapingSubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td></td>
            <td>
                <asp:CheckBox ID="chkRTVAdmin" runat="server" AutoPostBack="true" OnCheckedChanged="chkRTVAdmin_OnCheckedChanged"
                    Text="Select" CssClass="FieldName" Visible="false"/>
            </td>
            <td>
                <asp:Button ID="BtnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnMapingSubmit"
                    OnClick="btnGo_Click" />
            </td>
        </tr>
</table>
<table>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
</table>
<%-- OnNodeCheck="RTVAdmin_NodeCheck">
   OnNodeCheck="RTVRM_NodeCheck"
   OnNodeCheck="RTVBM_NodeCheck"
   OnNodeCheck="RTVCustomer_NodeCheck"
   OnNodeCheck="RTVOps_NodeCheck"
   OnNodeCheck="RTVResearch_NodeCheck"
   OnNodeCheck="RTVSuperAdmin_NodeCheck"
   OnNodeCheck="RTVAssociates_NodeCheck"  OnClick="btnAdminRemoveNodes_Click" --%>
<table align="center">
    <tr>
        <td>
            <asp:Panel runat="server" ID="PnlAdmin" Style="float: left; position: relative" Visible="false"
                Width="300px">
                <asp:Label ID="lb1Admin" runat="server" Text="Admin:" CssClass="FieldName" Visible="false"></asp:Label>
                <telerik:RadTreeView ID="RTVAdmin" runat="server" CheckBoxes="True" Height="280px"
                    TriStateCheckBoxes="true" CheckChildNodes="true" MultipleSelect="true" 
                    Skin="Vista">
                </telerik:RadTreeView>
                <div>
                    &nbsp;&nbsp; &nbsp;&nbsp;</div>
                <asp:Button ID="btnAdminRemoveNodes" runat="server" Text="Remove Admin Nodes" CssClass="PCGButton"
                    OnClick="btnAdminRemoveNodes_Click" Visible="false" />
            </asp:Panel>
            <%-- </td>
        <td>--%>
            <asp:Panel runat="server" ID="PnlRM" Style="float: left; position: relative" Visible="false"
                Width="300px">
                <asp:Label ID="lb1RM" runat="server" Text="RM:" CssClass="FieldName" Visible="false"></asp:Label>
                <telerik:RadTreeView ID="RTVRM" runat="server" CheckBoxes="True" Height="280px" Width="300px"
                    TriStateCheckBoxes="true" CheckChildNodes="true" Skin="Vista">
                </telerik:RadTreeView>
                <div>
                    &nbsp;&nbsp; &nbsp;&nbsp;</div>
                <asp:Button ID="btnRMRemoveNodes" runat="server" Text="Remove RM Nodes" CssClass="PCGMediumButton"
                    OnClick="btnRMRemoveNodes_Click" Width="120px" Visible="false" />
            </asp:Panel>
            <asp:Panel runat="server" ID="PnlBM" Style="float: left; position: relative" Visible="false"
                Width="300px">
                <asp:Label ID="lb1BM" runat="server" Text="BM:" CssClass="FieldName" Visible="false"></asp:Label>
                <telerik:RadTreeView ID="RTVBM" runat="server" CheckBoxes="True" Height="280px" TriStateCheckBoxes="true"
                    CheckChildNodes="true" Width="300px">
                </telerik:RadTreeView>
                <div>
                    &nbsp;&nbsp; &nbsp;&nbsp;</div>
                <asp:Button ID="btnBMRemoveNodes" runat="server" Text="Remove BM Nodes" CssClass="PCGMediumButton"
                    OnClick="btnBMRemoveNodes_Click" Width="120px" Visible="false" />
            </asp:Panel>
            <asp:Panel runat="server" ID="PnlCustomer" Style="float: left; position: relative"
                Visible="false" Width="300px">
                <asp:Label ID="lb1Customer" runat="server" Text="Customer:" CssClass="FieldName"
                    Visible="false"></asp:Label>
                <telerik:RadTreeView ID="RTVCustomer" runat="server" CheckBoxes="True" Height="280px"
                    TriStateCheckBoxes="true" CheckChildNodes="true" Skin="Vista">
                </telerik:RadTreeView>
                <div>
                    &nbsp;&nbsp; &nbsp;&nbsp;</div>
                <asp:Button ID="btnCustomerRemoveNodes" runat="server" Text="Remove Customer Nodes"
                    CssClass="PCGMediumButton" OnClick="btnCustomerRemoveNodes_Click" Visible="false" />
            </asp:Panel>
            <asp:Panel runat="server" ID="PnlOps" Style="float: left; position: relative" Visible="false"
                Width="300px">
                <asp:Label ID="lb1Ops" runat="server" Text="Ops:" CssClass="FieldName" Visible="false"></asp:Label>
                <telerik:RadTreeView ID="RTVOps" runat="server" CheckBoxes="True" Height="280px"
                    TriStateCheckBoxes="true" CheckChildNodes="true" Skin="Vista">
                </telerik:RadTreeView>
                <div>
                    &nbsp;&nbsp; &nbsp;&nbsp;</div>
                <asp:Button ID="btnOpsRemoveNodes" runat="server" Text="Remove Ops Nodes" CssClass="PCGMediumButton"
                    OnClick="btnOpsRemoveNodes_Click" Visible="false" />
            </asp:Panel>
            <asp:Panel runat="server" ID="PnlResearch" Style="float: left; position: relative"
                Visible="false" Width="300px">
                <asp:Label ID="lb1Research" runat="server" Text="Research:" CssClass="FieldName"></asp:Label>
                <telerik:RadTreeView ID="RTVResearch" runat="server" CheckBoxes="True" Height="280px"
                    TriStateCheckBoxes="true" CheckChildNodes="true">
                </telerik:RadTreeView>
                <div>
                    &nbsp;&nbsp; &nbsp;&nbsp;</div>
                <asp:Button ID="btnResearchRemoveNodes" runat="server" Text="Remove Research Nodes"
                    CssClass="PCGMediumButton" OnClick="btnResearchRemoveNodes_Click" Width="150px"
                    Visible="false" />
            </asp:Panel>
            <asp:Panel runat="server" ID="PnlSuperAdmin" Style="float: left; position: relative"
                Visible="false" Width="300px">
                <asp:Label ID="lb1SuperAdmin" runat="server" Text="SuperAdmin:" CssClass="FieldName"></asp:Label>
                <telerik:RadTreeView ID="RTVSuperAdmin" runat="server" CheckBoxes="True" Height="280px"
                    TriStateCheckBoxes="true" CheckChildNodes="true">
                </telerik:RadTreeView>
                <div>
                    &nbsp;&nbsp; &nbsp;&nbsp;</div>
                <asp:Button ID="btnSuperAdminRemoveNodes" runat="server" Text="Remove SuperAdmin Nodes"
                    CssClass="PCGMediumButton" OnClick="btnSuperAdminRemoveNodes_Click" Width="150px"
                    Visible="false" />
            </asp:Panel>
            <asp:Panel runat="server" ID="PnlAssociates" Style="float: left; position: relative"
                Visible="false" Width="300px">
                <asp:Label ID="lb1Associates" runat="server" Text="Associates:" CssClass="FieldName"></asp:Label>
                <telerik:RadTreeView ID="RTVAssociates" runat="server" CheckBoxes="True" Height="280px"
                    TriStateCheckBoxes="true" CheckChildNodes="true">
                </telerik:RadTreeView>
                <div>
                    &nbsp;&nbsp; &nbsp;&nbsp;</div>
                <asp:Button ID="btnAssociatesRemoveNodes" runat="server" Text="Remove Associates Nodes"
                    CssClass="PCGMediumButton" OnClick="btnAssociatesRemoveNodes_Click" Visible="false" />
            </asp:Panel>
        </td>
    </tr>
    <%--</table>
<table>--%>
</table>
<table>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
</table>
<table width="50%" align="center">
    <tr id="trBtnSubmit" runat="server" align="right">
        <td align="justify">
            <asp:Button ID="btnMapingSubmit" runat="server" Text="Submit" CssClass="PCGButton"
                ValidationGroup="btnMapingSubmit" OnClick="btnMapingSubmit_Click" Visible="false" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" ValidationGroup="btnMapingSubmit"
                OnClick="btnMapingUpdate_Click" Visible="false" />
        </td>
        <td class="rightData">
            &nbsp;
        </td>
    </tr>
</table>
