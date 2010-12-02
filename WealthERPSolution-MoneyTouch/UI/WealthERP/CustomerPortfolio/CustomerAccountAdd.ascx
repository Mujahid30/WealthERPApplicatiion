<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAccountAdd.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerAccountAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
    function checkDate(sender, args) {
        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
</script>

<asp:ScriptManager ID="scrptMgr" runat="server" EnableScriptLocalization="true">
</asp:ScriptManager>

<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="lblCustAccountHeader" class="HeaderTextBig" runat="server" Text="Portfolio Account Entry"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdRequiredText">
                    <label id="lbl" class="lblRequiredText">
                        Note: Fields marked with ' * ' are compulsory</label>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trAssetGroup" runat="server">
                <td>
                    <asp:Label ID="lblAssetGroup" runat="server" CssClass="FieldName" Text="Asset Group"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblAssetGroupName" runat="server" CssClass="FieldName" Text="Asset Group"></asp:Label>
                </td>
            </tr>
            <tr id="trCategory" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Asset Category:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please select a category"
                        ControlToValidate="ddlCategory" Operator="NotEqual" ValueToCompare="Select a Category"
                        Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSubcategory" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblSubCategory" runat="server" CssClass="FieldName" Text="SubCategory:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please select a sub-category"
                        ControlToValidate="ddlSubCategory" Operator="NotEqual" ValueToCompare="Select a Sub-Category"
                        Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trAccountNum" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblAccountNum" runat="server" CssClass="FieldName" Text="Account Number:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtLongAddField"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccountNumber"
                        ErrorMessage="Please enter a Number" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trAccountSource" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblAccountSource" runat="server" CssClass="FieldName" Text="Account Source:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccountSource" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--  <span id="Span5" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAccountSource"
                        ErrorMessage="Please enter an Account Source" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr id="trJoingHolding" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Joint Holding:"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                        Text="Yes" OnCheckedChanged="rbtnYes_CheckedChanged" AutoPostBack="true" />
                    <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                        Text="No" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged" Checked="true" />
                </td>
            </tr>
            <tr id="trModeOfHolding" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Please select a Mode of Holding"
                        ControlToValidate="ddlModeOfHolding" Operator="NotEqual" ValueToCompare="Select Mode of Holding"
                        Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
            <%--<tr id="trAccountOpeningDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblAccOpeningDate" runat="server" CssClass="FieldName" Text="Registration date:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccountOpeningDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="txtAccountOpeningDate_CalendarExtender" runat="server"
                        TargetControlID="txtAccountOpeningDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtAccountOpeningDate_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtAccountOpeningDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:RequiredFieldValidator ID="rfvAccountOpeningDate" ControlToValidate="txtAccountOpeningDate"
                        ErrorMessage="<br>Please select an Account Opening Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br>Please enter a valid date"
                        Type="Date" ControlToValidate="txtAccountOpeningDate" Operator="DataTypeCheck"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>--%>
            <tr id="trJoinHolders" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblJointHolders" runat="server" CssClass="HeaderTextSmall" Text="Joint Holders"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trJointHolderGrid" runat="server">
                <td colspan="2">
                    <asp:GridView ID="gvJointHoldersList" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyNames="MemberCustomerId, AssociationId" AllowSorting="True"
                        ShowFooter="true" CssClass="GridViewStyle">
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="trNoJointHolders" runat="server" visible="false">
                <td class="Message" colspan="2">
                    <asp:Label ID="lblNoJointHolders" runat="server" Text="You have no Joint Holder"
                        CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr id="trNomineeCaption" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblNominees" runat="server" CssClass="HeaderTextSmall" Text="Nominees"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trNominees" runat="server">
                <td colspan="2">
                    <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ShowFooter="true" DataKeyNames="MemberCustomerId, AssociationId" AllowSorting="True"
                        CssClass="GridViewStyle">
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId0" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="trNoNominee" runat="server" visible="false">
                <td class="Message" colspan="2">
                    <asp:Label ID="lblNoNominee" runat="server" Text="You have no Associations" CssClass="FieldName"></asp:Label>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<table style="width: 100%;">
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr id="trError" runat="server" visible="false">
        <td colspan="2" class="SubmitCell">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                Text="Submit" OnClick="btnSubmit_Click" />
        </td>
    </tr>
</table>
