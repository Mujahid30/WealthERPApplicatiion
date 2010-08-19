<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBranch.ascx.cs" Inherits="WealthERP.Advisor.AddBranch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html>
<head>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE">
</head>
<body>
</body>
</html>
<%--<script>
    function ViewAssociateRows(BranchAssociateType) {
        var content_Prefix = "ctrl_AddBranch_";
        CategoryRow = content_Prefix + "AssociateCategoryRow";
        LogoRow = content_Prefix + "AssociateLogoRow";
        AssociateCategoryRow = document.getElementById(CategoryRow);
        AssociateLogoRow = document.getElementById(LogoRow);
        if (BranchAssociateType.options[BranchAssociateType.selectedIndex].text == "Associate") {
            AssociateCategoryRow.style.display = '';
            AssociateLogoRow.style.display = '';
        }
        else {
            AssociateCategoryRow.style.display = 'none';
            AssociateLogoRow.style.display = 'none';
        }
    }
</script>--%>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="upnl" runat="server">
<ContentTemplate>--%>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Add Branch"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table style="width: 100%;" class="TableBackground">
    <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Branch/Associate Code :"></asp:Label>
        </td>
        <td class="rightField" width="25%">
            <asp:TextBox ID="txtBranchCode" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ControlToValidate="txtBranchCode" ErrorMessage="Please enter the Branch Code"
                CssClass="rfvPCG" Display="Dynamic" ID="rfvBranchCode" ValidationGroup="btnSubmit"
                runat="server"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" width="25%">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Branch/Associate Name :"></asp:Label>
        </td>
        <td class="rightField" width="25%">
            <asp:TextBox ID="txtBranchName" CssClass="txtField" runat="server" MaxLength="25"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ControlToValidate="txtBranchName" ErrorMessage="Please enter the Branch/Associate Name"
                CssClass="rfvPCG" Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="btnSubmit"
                runat="server"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Branch/Associate Type:"></asp:Label>
        </td>
        <td class="rightfield" colspan="3">
            <asp:DropDownList ID="ddlBranchAssociateType" runat="server" CssClass="cmbField"
                OnSelectedIndexChanged="ddlBranchAssociateType_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="ddlBranchAssociateType_CompareValidator" runat="server"
                ControlToValidate="ddlBranchAssociateType" ErrorMessage="Please select a Branch/Associate Type"
                Operator="NotEqual" ValueToCompare="Select a Type" CssClass="cvPCG" ValidationGroup="btnSubmit">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr id="AssociateCategoryRow" runat="server">
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Associate Category:"></asp:Label>
        </td>
        <td class="rightfield" colspan="3">
            <asp:DropDownList ID="ddlAssociateCategory" runat="server" CssClass="cmbField" ToolTip="Please setup the category if u dont find any data here">
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="ddlAssociateCategory_CompareValidator" runat="server" ControlToValidate="ddlAssociateCategory"
                ErrorMessage="Please select an Associate Category" Operator="NotEqual" ValueToCompare="Select Associate Category"
                CssClass="cvPCG" ValidationGroup="btnSubmit">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label5" runat="server" CssClass="HeaderTextSmall" Text="Branch/Associate Address"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblLine1" runat="server" CssClass="FieldName" Text="Line 1 ( House No/ Building) :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtLine1" CssClass="txtField" runat="server"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ControlToValidate="txtLine1" ErrorMessage="Please enter the Address Line1"
                CssClass="rfvPCG" Display="Dynamic" ID="RequiredFieldValidator2" ValidationGroup="btnSubmit"
                runat="server"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label21" runat="server" CssClass="FieldName" Text="City :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCity" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Line 2 (Street) :"> </asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtLine2" CssClass="txtField" runat="server"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="lblPinCode" runat="server" CssClass="FieldName" Text="Pin Code :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6" CssClass="txtField"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPinCode"
                ErrorMessage="Pincode Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtPinCode"
                Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Line 3 (Area) :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtLine3" CssClass="txtField" runat="server"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="State :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlState" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="3">
            <asp:Label ID="Label12" runat="server" CssClass="FieldName" Text="Country :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="cmbField">
                <asp:ListItem>India</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label9" runat="server" CssClass="HeaderTextSmall" Text="Contact Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblHeadName0" runat="server" CssClass="FieldName" Text="Name of the Branch/Associate Head :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlRmlist" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlRmlist_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="ddlRmlist"
                ErrorMessage="Please select a Branch Head" Operator="NotEqual" ValueToCompare="Select Branch head"
                CssClass="cvPCG" ValidationGroup="btnSubmit">
            </asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lblFax0" runat="server" CssClass="FieldName" Text="Branch/Associate Head Mobile Number :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtMobileNumber" CssClass="txtField" runat="server" MaxLength="10"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMobileNumber"
                ErrorMessage="Pincode Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtMobileNumber"
                Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trNoOfTerminals" runat="server">
        <td class="leftField">
            <asp:Label ID="Label22" runat="server" CssClass="FieldName" Text="Number Of Terminals :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtTerminalCount" CssClass="txtField" runat="server" MaxLength="3"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtTerminalCount"
                Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:Button ID="btnAddTerminal" runat="server" OnClick="btnAddTerminal_Click" Text="Add Terminal Id"
                CssClass="PCGMediumButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBranch_btnAddTerminal','M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBranch_btnAddTerminal','M');" />
        </td>
    </tr>
    <%--<tr>
        <td class="style4">
        </td>
        <td class="style2">
        </td>
    </tr>--%>
    <tr>
        <td colspan="4">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;<asp:Label ID="lblISD" runat="server" CssClass="FieldName" Text="ISD"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblSTD" runat="server" Names="Arial" CssClass="FieldName" Size="X-Small"
                Text="STD"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblPhone" runat="server" Names="Arial" CssClass="FieldName" Size="X-Small"
                Text="Phone Number"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhoneNumber" runat="server" CssClass="FieldName" Text="Telephone Number 1 :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtIsdPhone1" CssClass="txtField" runat="server" Width="55px" MaxLength="5">91</asp:TextBox>
            <asp:TextBox ID="txtStdPhone1" CssClass="txtField" runat="server" Width="55px" MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtStdPhone1"
                ErrorMessage="Please enter STD Code" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
            <asp:TextBox ID="txtPhone1" CssClass="txtField" runat="server" MaxLength="8"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPhone1"
                ErrorMessage="Please enter the Contact Number" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>            
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtIsdPhone1"
                Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtStdPhone1"
                Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtPhone1"
                Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhone2" runat="server" CssClass="FieldName" Text="Telephone Number 2 :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtIsdPhone2" CssClass="txtField" runat="server" Width="55px" MaxLength="5">91</asp:TextBox>
            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="cvPCG" Type="Integer" ControlToValidate="txtIsdPhone2" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
            <asp:TextBox ID="txtStdPhone2" CssClass="txtField" runat="server" Width="55px" MaxLength="4"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="cvPCG" Type="Integer" ControlToValidate="txtStdPhone2" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
            <asp:TextBox ID="txtPhone2" CssClass="txtField" runat="server" MaxLength="8"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="cvPCG" Type="Integer" ControlToValidate="txtPhone2" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFax" runat="server" CssClass="FieldName" Text="Fax :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtIsdFax" CssClass="txtField" runat="server" Width="55px" MaxLength="5">91</asp:TextBox>
            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="cvPCG" Type="Integer" ControlToValidate="txtIsdFax" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
            <asp:TextBox ID="txtStdFax" CssClass="txtField" runat="server" Width="55px" MaxLength="4"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="cvPCG" Type="Integer" ControlToValidate="txtStdFax" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
            <asp:TextBox ID="txtFax" CssClass="txtField" runat="server" MaxLength="8"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="cvPCG" Type="Integer" ControlToValidate="txtFax" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtEmail" CssClass="txtField" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ValidationGroup="btnSubmit" ErrorMessage="Please enter a valid Email ID" Display="Dynamic"
                runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="CommSharingStructureHdr" runat="server">
        <td colspan="4">
            <asp:Label ID="Label10" runat="server" CssClass="HeaderTextSmall" Text="Commision Sharing Structure"></asp:Label>
            <asp:GridView ID="gvCommStructure" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                ShowFooter="True" CellPadding="4" OnRowDataBound="gvCommStructure_RowDataBound">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Asset Group">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAssetGroup" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Commission Fee(%)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCommFee" runat="server" CssClass="txtField" MaxLength="5"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCommFee_E" runat="server" Enabled="True" TargetControlID="txtCommFee"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="RangeValidator1" SetFocusOnError="true" Type="Double" ErrorMessage="Margin should be 0 - 100%"
                                MinimumValue="0" MaximumValue="100" Display="Dynamic" ControlToValidate="txtCommFee"
                                runat="server"></asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upper Limit of Revenue">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRevUpperLimit" runat="server" CssClass="txtField"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtRevUpperLimit_E" runat="server" Enabled="True"
                                TargetControlID="txtRevUpperLimit" FilterType="Custom, Numbers" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtRevUpperLimit"
                                Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                                ValidationExpression="^\d*(\.(\d{0,2}))?$"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lower Limit of Revenue">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRevLowerLimit" runat="server" CssClass="txtField"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtRevLowerLimit_E" runat="server" Enabled="True"
                                TargetControlID="txtRevLowerLimit" FilterType="Custom, Numbers" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                            <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtRevLowerLimit"
                                ErrorMessage="Lower Limit should be less than the Upper Limit" Type="Double"
                                Operator="LessThan" ControlToCompare="txtRevUpperLimit" CssClass="cvPCG" ValidationGroup="btnSubmit"
                                Display="Dynamic"></asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtRevLowerLimit"
                                Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                                ValidationExpression="^\d*(\.(\d{0,2}))?$"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtField"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" TargetControlID="txtStartDate"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="txtStartDate_TextBoxWatermarkExtender" runat="server"
                                TargetControlID="txtStartDate" WatermarkText="dd/mm/yyyy">
                            </cc1:TextBoxWatermarkExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtField"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" TargetControlID="txtEndDate"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="txtEndDate_TextBoxWatermarkExtender" runat="server"
                                TargetControlID="txtEndDate" WatermarkText="dd/mm/yyyy">
                            </cc1:TextBoxWatermarkExtender>
                            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtEndDate"
                                ErrorMessage="End Date should be greater than Start Date" Type="Date" Operator="GreaterThanEqual"
                                ControlToCompare="txtStartDate" CssClass="cvPCG" ValidationGroup="btnSubmit"
                                Display="Dynamic">
                            </asp:CompareValidator>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" CssClass="PCGLongButton" OnClick="ButtonAdd_Click"
                                Text="Add More" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%--   <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <asp:Button ID="btnAddMore" runat="server" CssClass="ButtonField" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <hr />
        </td>
    </tr>
    <tr id="CommSharingStructureGv" runat="server">
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr id="AssociateLogoHdr" runat="server">
        <td colspan="4">
            <asp:Label ID="Label14" runat="server" CssClass="HeaderTextSmall" Text="Logo"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="AssociateLogoRow" runat="server">
        <td class="leftField">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Upload Associate's Logo:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:FileUpload ID="FileUpload" runat="server" Height="22px" />
        </td>
    </tr>
    <%--<tr>
        <td class="style4">
        </td>
        <td class="style2">
        </td>
    </tr>--%>
</table>
</ContentTemplate> </asp:UpdatePanel>
<table class="TableBackground" style="width: 100%">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="SubmitCell" colspan="2">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBranch_btnSignIn','S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBranch_btnSignIn','S');"
                Text="Submit" OnClick="btnSaveChanges_Click" ValidationGroup="btnSubmit" />
        </td>
    </tr>
</table>
