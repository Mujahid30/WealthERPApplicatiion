<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="CustomerISAFolioMapping.ascx.cs"
    Inherits="WealthERP.Customer.CustomerISAFolioMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script type="text/javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }
</script>
<%--<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvAvailableFolio.Items.Count %>');
        var gvControl = document.getElementById('<%= gvAvailableFolio.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "cbRecons";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxWerpAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>
<script language="javascript" type="text/javascript">
    function checkAllBoxesForAttachedFolio() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvAvailableFolio.Items.Count %>');
        var gvControl = document.getElementById('<%= gvAvailableFolio.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "cbRecons";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxWerpAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>--%>
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
                        <asp:DropDownList ID="ddlMemberBranch" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlMemberBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblMember" runat="server" CssClass="FieldName" Text="Member Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                       <asp:TextBox ID="txtMember" runat="server" CssClass="txtField" AutoComplete="Off"
                 AutoPostBack="True">
            </asp:TextBox><%--<span id="spnCustomer" class="spnRequiredField">*</span>--%>
            <cc1:TextBoxWatermarkExtender ID="txtMember_water" TargetControlID="txtMember"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtMember_autoCompleteExtender" runat="server"
                TargetControlID="txtMember" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
              <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMember"
                            ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                            CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr id="trPan" runat="server">
                    <td class="leftField" align="right">
                        <asp:Label ID="lblPan" runat="server" CssClass="FieldName" Text="PAN:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblGetPan" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr id="trISAList" runat="server">
                    <td class="leftField" align="right">
                        <asp:Label ID="lblCustomerISAAccount" runat="server" CssClass="FieldName" Text="Pick ISA:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomerISAAccount" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerISAAccount_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trHoldings" runat="server">
                    <td class="leftField" align="right">
                        <asp:Label ID="lblModeofHolding" runat="server" CssClass="FieldName" Text="Mode Of Holdings:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblModeOfHoldingValue" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr id="trHoldingType" runat="server">
                    <td class="leftField" align="right">
                        <asp:Label ID="lblHoldingType" runat="server" CssClass="FieldName" Text="ISA Holding Type:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblGetISAHoldingType" runat="server" Text="" CssClass="FieldName"></asp:Label>
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
                        <asp:Label ID="lblNomineegv" runat="server" CssClass="FieldName" Text="Nominee List"></asp:Label>
                        <br /><br />
                        <asp:Panel ID="pnlNominiees" runat="server" >
                            <%--<asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
                            </asp:GridView>--%>
                            <telerik:RadGrid ID="gvNominees" runat="server" GridLines="None" AutoGenerateColumns="False"
                                Width="100%" PageSize="4" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Count" Visible="True" >
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name"
                                            SortExpression="Name">
                                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Relationship" HeaderText="Relationship" AllowFiltering="false" HeaderStyle-HorizontalAlign="Left"
                                            UniqueName="Relationship">
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </td>
                    <td id="tdJointHolders" align="left" style="padding-left: 30px;" runat="server">
                    <asp:Label ID="lblJointHoldersGv" runat="server" CssClass="FieldName" Text="JointHolders List"></asp:Label>
                        <br /><br />
                        <asp:Panel ID="pnlJointholders" runat="server">
                           <%-- <asp:GridView ID="gvJointHoldersList" runat="server" AutoGenerateColumns="False"
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
                            </asp:GridView>--%>
                              <telerik:RadGrid ID="gvJointHoldersList" runat="server" GridLines="None" AutoGenerateColumns="False"
                                Width="100%" PageSize="4" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AssociateName" HeaderText="Name" UniqueName="Name"
                                            SortExpression="Name">
                                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="XR_Relationship" HeaderText="Relationship" AllowFiltering="false" HeaderStyle-HorizontalAlign="Left"
                                            UniqueName="Relationship">
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
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
    <td colspan="4"></td>
    </tr>
    <tr>
    <td colspan="4"></td>
    </tr>
    <tr>
        <td>
        <telerik:RadGrid ID="gvAvailableFolio" runat="server" GridLines="None" AutoGenerateColumns="False"
                                Width="100%" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView Width="100%" DataKeyNames="CMFA_AccountId,PA_AMCCode,XMOH_ModeOfHoldingCode,CMFA_IsJointlyHeld,CP_PortfolioId" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Select">
                        <HeaderTemplate>
                        <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                         <%--<input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxesForAttachedFolio()" />--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderText="Folio" UniqueName="CMFA_FolioNum"
                                            SortExpression="CMFA_FolioNum">
                                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PA_AMCName" HeaderText="AMC" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="PA_AMCName">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
        </td>
        <td>
        <asp:Button ID="btnGo" runat="server" Text=">>" CssClass="PCGButton" 
            ValidationGroup="ButtonGo" onclick="btnGo_Click" />
        </td>
        <td>
        <telerik:RadGrid ID="gvAttachedFolio" runat="server" GridLines="None" AutoGenerateColumns="False"
                                Width="100%" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Goal MIS" Excel-Format="ExcelML">
                                </ExportSettings>
                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                    CommandItemDisplay="None">
                                    <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Select">
                        <HeaderTemplate>
                        <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                         <%--<input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxes()" />--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderText="Folio" UniqueName="CMFA_FolioNum"
                                            SortExpression="CMFA_FolioNum">
                                            <ItemStyle  HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PA_AMCName" HeaderText="AMC" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="PA_AMCName">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
