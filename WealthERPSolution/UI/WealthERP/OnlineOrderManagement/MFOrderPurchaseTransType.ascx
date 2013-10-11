<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderPurchaseTransType.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOrderPurchaseTransactionType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script src="../Scripts/jquery.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<script type="text/javascript">
    function ShowPopup() {
        var i = 0;
        var form = document.forms[0];
        var folioId = "";
        var count = 0;
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                }
            }
        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            New Purchase
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<telerik:RadWindow VisibleOnPageLoad="false" ID="radwindowForNominee" runat="server"
    Height="30%" Width="550px" Modal="true" BackColor="#DADADA" Top="10px" Left="20px"
    Behaviors="Move,resize,close" Title="Add Nominee">
    <ContentTemplate>
        <div id="DivForNominee" style="width: 75%; text-align: center;" runat="server" class="failure-msg"
            align="center" visible="false">
            Records not found
        </div>
        <div style="padding: 20px">
            <telerik:RadGrid ID="gvNominees" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="false"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="500px" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="MemberCustomerId, AssociationId" NoDetailRecordsText="Records not found"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="15px" AllowFiltering="false" HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId0" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="110px" DataField="Name"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="100px" DataField="Relationship"
                            HeaderText="Relationship" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        <div style="padding: 20px">
            <asp:Button ID="btnAddNominee" runat="server" CssClass="PCGButton" Text="Associate"
                OnClick="btnAddNominee_Click" OnClientClick="return ShowPopup()" />
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow VisibleOnPageLoad="false" ID="radwindowForJointHolder" runat="server"
    Height="30%" Width="550px" Modal="true" BackColor="#DADADA" Top="10px" Left="20px"
    Behaviors="Move,resize,close" Title="Add Joint Holder">
    <ContentTemplate>
        <div id="DivForJH" style="width: 75%; text-align: center;" runat="server" class="failure-msg"
            align="center" visible="false">
            Records not found
        </div>
        <div style="padding: 20px">
            <telerik:RadGrid ID="gvJointHoldersList" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="false"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="500px" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView NoDetailRecordsText="Records not found" DataKeyNames="AssociationId"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    <NoRecordsTemplate>
                        <div>
                            There are no records to display</div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="15px" AllowFiltering="false" HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" ShowFilterIcon="false" DataField="Name"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" ShowFilterIcon="false" DataField="Relationship"
                            HeaderText="Relationship" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        <div style="padding: 20px">
            <asp:Button ID="btnAddJointHolder" runat="server" CssClass="PCGButton" Text="Associate"
                OnClick="btnAddJointHolder_Click" OnClientClick="return ShowPopup()" />
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<table id="tbpurchase" width="100%">
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAmc" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select an AMC" Display="Dynamic" ControlToValidate="ddlAmc"
                InitialValue="0" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Category"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnSubmit"
                Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lblScheme" runat="server" Text="Scheme" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScheme_onSelectedChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select a scheme"
                CssClass="rfvPCG" ControlToValidate="ddlScheme" ValidationGroup="btnSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblOption" runat="server" Text="Option" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblDividendType" runat="server" CssClass="txtField"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="lblDividendFrequency" runat="server" Text="Dividend Frequency" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lbldftext" runat="server" CssClass="txtField"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="lblDivType" runat="server" Text="Dividend Type" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlDivType_OnSelectedIndexChanged">
                <asp:ListItem>Dividend Reinvestement</asp:ListItem>
                <asp:ListItem>Dividend Payout</asp:ListItem>
            </asp:DropDownList>
        </td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="rfvPCG"
            ErrorMessage="Enter " Display="Dynamic" ControlToValidate="ddlDivType" InitialValue="Select"
            ValidationGroup="btnSubmit">
        </asp:RequiredFieldValidator>
        </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblMoh" runat="server" Text="Mode of Holding:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlMoh" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            
        </td>
        <td class="leftField">
            <asp:Label ID="lblAmt" runat="server" Text="Amount" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAmt" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
            ErrorMessage="Enter Amount" Display="Dynamic" ControlToValidate="txtAmt" InitialValue="Select"
            ValidationGroup="btnSubmit">
        </asp:RequiredFieldValidator>
        <td class="leftField">
            <asp:Label ID="lblMin" runat="server" Text="Min Amount:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMintxt" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblHolder" CssClass="FieldName" Text="Add Holder :" runat="server"></asp:Label>
        </td>
        <td colspan="5">
            <asp:ImageButton OnClick="imgAddJointHolder_Click" ID="imgAddJointHolder" Text="AddJTHolder"
                runat="server" ImageUrl="~/Images/user_add.png" runat="server" ToolTip="Click here to Add Joint Holder"
                Height="15px" Width="15px"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="5">
            <telerik:RadGrid Visible="false" ID="gvJoint2" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="false"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="500px" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView NoDetailRecordsText="Records not found" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="110px"
                            DataField="MemberCustomerId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="50px"
                            DataField="AssociationId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="100px" DataField="NAME"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="25px" DataField="XR_Relationship"
                            HeaderText="Relation" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblNominee" runat="server" Text="Add Nominee :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="5">
            <asp:ImageButton OnClick="imgAddNominee_Click" ImageUrl="~/Images/user_add.png" runat="server"
                ToolTip="Click here to Add Nominee" Height="15px" Width="15px" ID="imgAddNominee"
                Text="AddNominee"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="5">
            <telerik:RadGrid Visible="false" ID="gvNominee2" runat="server" GridLines="None"
                AutoGenerateColumns="False" PageSize="10" AllowSorting="false" AllowPaging="True"
                ShowStatusBar="false" ShowFooter="false" Skin="Telerik" EnableEmbeddedSkins="false"
                Width="500px" AllowFilteringByColumn="false" AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView NoDetailRecordsText="Records not found" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="110px"
                            DataField="MemberCustomerId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="50px"
                            DataField="AssociationId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="100px" DataField="NAME"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="25px" DataField="XR_Relationship"
                            HeaderText="Relation" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCutt" runat="server" Text="Cutt Off time" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lbltime" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="lblNav" runat="server" Text="Latest Nav" CssClass="FieldName"></asp:Label>
        </td>
        <%--<td class="leftField">
            <asp:Label ID="lblOffer" runat="server" Text="Offer Doc" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td class="leftField">
            <asp:Label ID="lblFact" runat="server" Text="Fact Sheet" CssClass="FieldName"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="lblMultiple" runat="server" Text="Multiple There after:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMulti" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="5">
        </td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="OnClick_Submit"
                CssClass="FieldName" ValidationGroup="btnSubmit"></asp:Button>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnAssociationIdForNominee" runat="server" />
<asp:HiddenField ID="hdnAssociationIdForJointHolder" runat="server" />
