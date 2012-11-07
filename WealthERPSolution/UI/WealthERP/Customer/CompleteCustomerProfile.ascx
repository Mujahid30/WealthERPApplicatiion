<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompleteCustomerProfile.ascx.cs"
    Inherits="WealthERP.Customer.CompleteCustomerProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td colspan="5">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left" style="width: 30%">
                                    <asp:Label ID="lblCustProfilePageName" Text="Customer Add Edit" runat="server"></asp:Label>
                                </td>
                                <td align="center" style="width: 40%">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Image ID="imgProgress" ImageUrl="~/Images/ajax-loader.gif" AlternateText="Processing"
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="trMessage" visible="false">
                <td align="center" colspan="5">
                    <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                        <asp:Label ID="lblMessage" Text="" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr id="trCustomerSection" runat="server">
                <td colspan="5">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div style="float: left">
                            Customer Details</div>
                        <div style="float: right">
                            <asp:LinkButton ID="lnkCustomerBasicProfileEdit" Text="View" 
                                CssClass="FieldName" runat="server"  
                                onclick="lnkCustomerBasicProfileEdit_Click">
                            </asp:LinkButton>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                        GroupName="grpCustomerType" OnCheckedChanged="rbtnIndividual_CheckedChanged"
                        AutoPostBack="true" Checked="true" />
                    <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                        GroupName="grpCustomerType" OnCheckedChanged="rbtnNonIndividual_CheckedChanged"
                        AutoPostBack="true" />
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                        ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select"
                        CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgCustomerProfileBasic"></asp:CompareValidator>
                </td>
                <td style="width: 25%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblBranchName" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlAdviserBranchList" AutoPostBack="true" runat="server" CssClass="cmbField"
                        OnSelectedIndexChanged="ddlAdviserBranchList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="ddlAdviserBranchList_CompareValidator2" runat="server"
                        ControlToValidate="ddlAdviserBranchList" ErrorMessage="Please select a Branch"
                        Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"
                        ValidationGroup="vgCustomerProfileBasic">
                    </asp:CompareValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="Select RM:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlAdviseRMList" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAdviseRMList"
                        ErrorMessage=" " Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG"
                        Display="Dynamic" ValidationGroup="vgCustomerProfileBasic">
                    </asp:CompareValidator>
                </td>
                <td style="width: 25%">
                    &nbsp;
                </td>
            </tr>
            <tr id="trCustomerIndivisualOne" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblGender" runat="server" CssClass="FieldName" Text="Gender:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:RadioButton ID="rbtnMale" runat="server" CssClass="txtField" Text="Male" GroupName="rbtnGender"
                        Checked="true" />
                    <asp:RadioButton ID="rbtnFemale" runat="server" CssClass="txtField" Text="Female"
                        GroupName="rbtnGender" />
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Salutation:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="cmbField">
                        <asp:ListItem>Select a Salutation</asp:ListItem>
                        <asp:ListItem>Mr.</asp:ListItem>
                        <asp:ListItem>Mrs.</asp:ListItem>
                        <asp:ListItem>Ms.</asp:ListItem>
                        <asp:ListItem>M/S.</asp:ListItem>
                        <asp:ListItem>Dr.</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 20%">
                    &nbsp;
                </td>
            </tr>
            <tr id="trCustomerIndivisualTwo" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblFirstName" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="90%" CssClass="txtField"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                        Enabled="True" TargetControlID="txtFirstName" WatermarkText="FirstName">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFirstName"
                        ErrorMessage="<br />Please enter the First Name" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="vgCustomerProfileBasic">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:TextBox ID="txtMiddleName" runat="server" Width="90%" CssClass="txtField"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                        Enabled="True" TargetControlID="txtMiddleName" WatermarkText="MiddleName">
                    </cc1:TextBoxWatermarkExtender>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtLastName" runat="server" Width="90%" CssClass="txtField"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                        Enabled="True" TargetControlID="txtLastName" WatermarkText="LastName">
                    </cc1:TextBoxWatermarkExtender>
                </td>
                <td style="width: 25%">
                    &nbsp;
                </td>
            </tr>
            <tr id="trCustomerNonIndivisualOne" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblCompanyName" runat="server" CssClass="FieldName" Text="Company Name:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtCompanyName" CssClass="txtField" Style="width: 90%" runat="server"></asp:TextBox>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCompanyName"
                        ErrorMessage="Please enter the Company Name" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="vgCustomerProfileBasic">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="Label14" runat="server" CssClass="FieldName" Text="Company Website:"></asp:Label>
                </td>
                <td style="width: 20%" class="rightField">
                    <asp:TextBox ID="txtCompanyWebsite" runat="server" Width="90%" CssClass="txtField"></asp:TextBox>
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            <tr>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtPanNumber" runat="server" Width="90%" CssClass="txtField" MaxLength="10"></asp:TextBox>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                        Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="vgCustomerProfileBasic">
                    </asp:RequiredFieldValidator>
                    <%-- <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="PAN Number already exists"></asp:Label>--%>
                </td>
                <td class="leftField" style="width: 20%">
                    <div style="float: left">
                        <asp:CheckBox ID="chkdummypan" runat="server" CssClass="txtField" Text=" Use Dummy"
                            AutoPostBack="false" />
                    </div>
                    <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%" colspan="2">
                    <asp:TextBox ID="txtEmail" runat="server" Style="width: 45%" CssClass="txtField"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                        ErrorMessage="Not a valid Email" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        CssClass="revPCG" ValidationGroup="vgCustomerProfileBasic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblCustomerCategory" runat="server" CssClass="FieldName" Text="Customer Category:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlCustomerCategory" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblCustomerCode" runat="server" CssClass="FieldName" Text="Customer Code:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td style="width: 20%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblDOProfile" runat="server" CssClass="FieldName" Text="Date of Profiling:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtDateofProfiling" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="rightField" style="width: 60%" colspan="3">
                    <asp:Button ID="btnSubmitAddMore" runat="server" Text="Submit and Add More" s CssClass="PCGLongButton"
                        OnClick="btnSubmitAddMore_Click" ValidationGroup="vgCustomerProfileBasic" />
                    &nbsp;
                    <asp:Button ID="btnSaveAddDeatils" runat="server" Text="Save and Add Details" CssClass="PCGLongButton"
                        OnClick="btnSaveAddDeatils_Click" ValidationGroup="vgCustomerProfileBasic" />
                    &nbsp;
                    <asp:Button ID="btnUpdateProfileBasic" runat="server" Text="Update" CssClass="PCGButton"
                        OnClick="btnUpdateProfileBasic_Click" ValidationGroup="vgCustomerProfileBasic" />
                </td>
            </tr>
            <tr id="trCustomerISASection" runat="server">
                <td colspan="5">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Customer ISA
                    </div>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
