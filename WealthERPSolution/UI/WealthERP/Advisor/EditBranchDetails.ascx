<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditBranchDetails.ascx.cs"
    Inherits="WealthERP.Advisor.EditBranchDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');
        if (bool) {
            document.getElementById("ctrl_EditBranchDetails_hdnMsgValue").value = 1;
            document.getElementById("ctrl_EditBranchDetails_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_EditBranchDetails_hdnMsgValue").value = 0;
            document.getElementById("ctrl_EditBranchDetails_hiddenDelete").click();
            return true;
        }
    }
   
</script>

<style type="text/css">
.txtGridMediumField
{
    font-family: Verdana,Tahoma;
    font-weight: normal;
    font-size: x-small;
    color: #16518A;
    width: 80px;
}
</style>
<table class="TableBackground" style="width:100%">
    <tr>
        <td colspan="2">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Edit Branch Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:ScriptManager ID="scriptmanager1" runat="server">
            </asp:ScriptManager>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Branch Code :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBranchCode" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ControlToValidate="txtBranchCode" ErrorMessage="Please enter the Branch Code"
                Display="Dynamic" ID="rfvBranchCode" ValidationGroup="btnSubmit" runat="server"
                CssClass="rfvPCG"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Branch Name :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ControlToValidate="txtBranchName" ErrorMessage="Please enter the Branch Name"
                Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="btnSubmit" runat="server"
                CssClass="rfvPCG"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Branch/Associate Type:"></asp:Label>
        </td>
        <td class="rightfield">
            <asp:DropDownList ID="ddlBranchAssociateType" runat="server" CssClass="cmbField"
                OnSelectedIndexChanged="ddlBranchAssociateType_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span5" class="spnRequiredField">*</span>
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
        <td class="rightfield">
            <asp:DropDownList ID="ddlAssociateCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:Label ID="Label13" runat="server" CssClass="FieldName" Text=" Please setup the category if u dont find any data here"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label5" runat="server" CssClass="HeaderTextSmall" Text="Branch Address"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblLine1" runat="server" CssClass="FieldName" Text="Line1 (House No/Building) :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtLine1" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ControlToValidate="txtLine1" ErrorMessage="Please enter the Address Line1"
                Display="Dynamic" ID="RequiredFieldValidator2" ValidationGroup="btnSubmit" runat="server"
                CssClass="rfvPCG"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Line2 (Street) :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtLine2" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Line3 (Area) :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtLine3" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label21" runat="server" CssClass="FieldName" Text="City :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCity" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPinCode" runat="server" CssClass="FieldName" Text="Pincode :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPinCode"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="State :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlState" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label12" runat="server" CssClass="FieldName" Text="Country :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="cmbField">
                <asp:ListItem>India</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblHeadName0" runat="server" CssClass="FieldName" Text="Name of the Branch Head :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlRmlist" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddlRmlist_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Please Select a Branch Head"
                ValidationGroup="btnSubmit" CssClass="cvPCG" ControlToValidate="ddlRmlist" Operator="NotEqual"
                ValueToCompare="Select Branch head" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFax0" runat="server" CssClass="FieldName" Text="Branch Head Mobile Number :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtMobileNumber" CssClass="txtField" runat="server" MaxLength="10"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="cvPCG" Type="Integer" ControlToValidate="txtMobileNumber"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
    <td>&nbsp;&nbsp;</td>
        <td>
           
            <asp:Label ID="lblISD" runat="server" CssClass="FieldName" Text="ISD"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblSTD" runat="server" CssClass="FieldName" Text="STD"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblPhone" runat="server" CssClass="FieldName" Text="Phone Number"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhoneNumber" runat="server" CssClass="FieldName" Text="Telephone Number1 :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtIsdPhone1" runat="server" Width="55px" MaxLength="3" CssClass="txtField"></asp:TextBox>
            <asp:TextBox ID="txtStdPhone1" runat="server" Width="55px" MaxLength="3" CssClass="txtField"></asp:TextBox>
            <asp:TextBox ID="txtPhone1" runat="server" Width="119px" MaxLength="8" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ControlToValidate="txtPhone1" ErrorMessage="<br />Please enter the Contact Number"
                Display="Dynamic" ID="RequiredFieldValidator3" ValidationGroup="btnSubmit" runat="server"
                CssClass="rfvPCG"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPhone1"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtStdPhone1"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhoneNumber2" runat="server" CssClass="FieldName" Text="Telephone Number2 :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtIsdPhone2" runat="server" Width="55px" MaxLength="3" CssClass="txtField"></asp:TextBox>
            <asp:TextBox ID="txtStdPhone2" runat="server" Width="55px" MaxLength="3" CssClass="txtField"></asp:TextBox>
            <asp:TextBox ID="txtPhone2" runat="server" Width="119px" MaxLength="8" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPhone2"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtStdPhone2"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtIsdPhone2"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFax" runat="server" CssClass="FieldName" Text="Fax :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtIsdFax" runat="server" Width="55px" MaxLength="3" CssClass="txtField"></asp:TextBox>
            <asp:TextBox ID="txtStdFax" runat="server" Width="55px" CssClass="txtField" MaxLength="3"></asp:TextBox>
            <asp:TextBox ID="txtFax" runat="server" Width="119px" MaxLength="8" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtFax"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtStdFax"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtIsdFax"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEmail"
                ValidationGroup="btnSubmit" ErrorMessage="Please enter an Email ID" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                ValidationGroup="btnSubmit" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="CommSharingStructureHdr" runat="server">
        <td colspan="4">
            <asp:Label ID="Label10" runat="server" CssClass="HeaderTextSmall" Text="Commision Sharing Structure"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="CommSharingStructureGv" runat="server">
        <td colspan="4">
            <asp:GridView ID="gvCommStructure" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                ShowFooter="True" CellPadding="4" OnRowDataBound="gvCommStructure_RowDataBound"
                DataKeyNames="AACS_Id" EnableViewState="true" Visible="true">
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
                             <asp:CompareValidator ID="cmpValidatorAssetGroup" runat="server" ControlToValidate="ddlAssetGroup"
                ErrorMessage="Please select a asset group" Operator="NotEqual" ValueToCompare="Select Asset Group" Display="Dynamic"
                CssClass="cvPCG" ValidationGroup="btnAdd">
                 </asp:CompareValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Commission Fee(%)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCommFee" runat="server" CssClass="txtGridMediumField" Text='<%# Bind("CommissionFee","{0:f2}") %>'></asp:TextBox>
                            <cc1:filteredtextboxextender id="txtCommFee_E" runat="server" enabled="True" targetcontrolid="txtCommFee"
                                filtertype="Custom,Numbers" validchars=".">
                            </cc1:filteredtextboxextender>
                            <asp:RequiredFieldValidator ControlToValidate="txtCommFee" ErrorMessage="Please enter Commission Fee(%)"
                             CssClass="rfvPCG" Display="Dynamic" ID="reqtxtCommFee" ValidationGroup="btnAdd"
                                runat="server"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" SetFocusOnError="true" Type="Double" ErrorMessage="Margin should be 0 - 100%"
                                MinimumValue="0" MaximumValue="100" Display="Dynamic" ControlToValidate="txtCommFee"  ValidationGroup="btnAdd"
                                runat="server"></asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Revenue Upper Limit">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRevUpperLimit" runat="server" CssClass="txtGridMediumField" Text='<%# Bind("RevenueUpperLimit","{0:f2}") %>'></asp:TextBox>
                            <cc1:filteredtextboxextender id="txtRevUpperLimit_E" runat="server" enabled="True"
                                targetcontrolid="txtRevUpperLimit" filtertype="Custom, Numbers" validchars=".">
                            </cc1:filteredtextboxextender>
                            <asp:RequiredFieldValidator ControlToValidate="txtRevUpperLimit" ErrorMessage="Please enter Commission upper limit"
                             CssClass="rfvPCG" Display="Dynamic" ID="reqtxtRevUpperLimit" ValidationGroup="btnAdd"
                                runat="server"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtRevUpperLimit"
                                Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format" ValidationGroup="btnAdd"
                                ValidationExpression="^\d*(\.(\d{0,2}))?$"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Revenue Lower Limit">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRevLowerLimit" runat="server" CssClass="txtGridMediumField" Text='<%# Bind("RevenueLowerLimit","{0:f2}") %>'></asp:TextBox>
                             <cc1:filteredtextboxextender id="txtRevLowerLimit_E" runat="server" enabled="True"
                                targetcontrolid="txtRevLowerLimit" filtertype="Custom, Numbers" validchars=".">
                            </cc1:filteredtextboxextender>
                             <asp:RequiredFieldValidator ControlToValidate="txtRevLowerLimit" ErrorMessage="Please enter Commission lower limit"
                             CssClass="rfvPCG" Display="Dynamic" ID="reqtxtRevLowerLimit" ValidationGroup="btnAdd"
                                runat="server"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtRevLowerLimit"
                                ErrorMessage="Lower Limit should be less than the Upper Limit" Type="Double"
                                Operator="LessThan" ControlToCompare="txtRevUpperLimit" CssClass="cvPCG" ValidationGroup="btnAdd"
                                Display="Dynamic"></asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtRevLowerLimit"
                                Display="Dynamic" CssClass="rfvPCG" runat="server" ValidationGroup="btnAdd" ErrorMessage="Not acceptable format"
                                ValidationExpression="^\d*(\.(\d{0,2}))?$"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtGridMediumField" Text='<%# Eval("StartDate", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                            <cc1:calendarextender id="txtStartDate_CalendarExtender" runat="server" targetcontrolid="txtStartDate"
                                format="dd/MM/yyyy">
                            </cc1:calendarextender>
                            <cc1:textboxwatermarkextender id="txtStartDate_TextBoxWatermarkExtender" runat="server"
                                targetcontrolid="txtStartDate" watermarktext="dd/mm/yyyy">
                            </cc1:textboxwatermarkextender>
                            <asp:RequiredFieldValidator ID="reqtxtStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic"
                            CssClass="rfvPCG" ValidationGroup="btnAdd" ErrorMessage="Please select a start Date"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtGridMediumField" Text='<%# Eval("EndDate", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                            <cc1:calendarextender id="txtEndDate_CalendarExtender" runat="server" targetcontrolid="txtEndDate"
                                format="dd/MM/yyyy">
                            </cc1:calendarextender>
                            <asp:RequiredFieldValidator ID="reqtxtEndDate" runat="server" ControlToValidate="txtEndDate" Display="Dynamic"
                            CssClass="rfvPCG" ValidationGroup="btnAdd" ErrorMessage="Please select a End Date"></asp:RequiredFieldValidator>
                            <cc1:textboxwatermarkextender id="txtEndDate_TextBoxWatermarkExtender" runat="server"
                                targetcontrolid="txtEndDate" watermarktext="dd/mm/yyyy">
                            </cc1:textboxwatermarkextender>
                            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtEndDate"
                                ErrorMessage="End Date should be greater than Start Date" Type="Date" Operator="GreaterThanEqual"
                                ControlToCompare="txtStartDate" CssClass="cvPCG" ValidationGroup="btnSubmit"
                                Display="Dynamic">
                            </asp:CompareValidator>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" CssClass="PCGButton" OnClick="ButtonAdd_Click"
                                Text="Add More" ValidationGroup="btnAdd"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%--   <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <asp:Button ID="btnAddMore" runat="server" CssClass="ButtonField" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="AssociateLogoHdr" runat="server">
        <td colspan="4">
            <asp:Label ID="Label14" runat="server" CssClass="HeaderTextSmall" Text="Logo"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="AssociateLogoRow" runat="server">
        <td>
            <asp:LinkButton ID="lnklogoChange" runat="server" CssClass="LinkButtons" OnClick="DisplayLogoControl">Click to change Logo</asp:LinkButton>
        </td>
        <td>
            <asp:Label ID="lblLogoChange" runat="server" CssClass="FieldName" Text="Change Associate's Logo:"
                Visible="false"></asp:Label>
            <asp:FileUpload ID="logoChange" runat="server" Height="22px" Visible="false" />
        </td>
        <td>
            &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="SubmitCell" colspan="2">
            <asp:Button ID="btnSaveChanges" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditBranchDetails_btnSaveChanges');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditBranchDetails_btnSaveChanges');"
                ValidationGroup="btnSubmit" Text="Save" OnClick="btnSaveChanges_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewBranchDetails_btnDelete');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewBranchDetails_btnDelete');"
                OnClick="btnDelete_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="hiddenDelete" runat="server" OnClick="hiddenDelete_Click" Text=""
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
</table>
