<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddAssociatesDetails.ascx.cs"
    Inherits="WealthERP.Associates.AddAssociatesDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>--%>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<title>test</title>
<style type="text/css">
    .horizontalListbox
    {
        border: 0px;
    }
    .horizontalListbox .rlbItem
    {
        float: left !important;
    }
    .horizontalListbox .rlbGroup, .RadListBox
    {
        width: auto !important;
    }
</style>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left" id="head" runat="server">
                            Add Associate
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="LinkButtons" Text="Edit"
                                Visible="false" OnClick="lnkBtnEdit_Click"></asp:LinkButton>
                            &nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Back" Visible="false"
                                OnClick="lnlBack_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="right">
            <asp:Label ID="lblTitleList" runat="server" Text="Title:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlTitleList" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlTitleList_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select Title"
                CssClass="rfvPCG" ControlToValidate="ddlTitleList" ValidationGroup="SubmitDetails"
                Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Staff:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlRM_SelectedIndexChanged" Style="vertical-align: middle">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="requddlRM" runat="server" ErrorMessage="Please select a RM"
                CssClass="rfvPCG" ControlToValidate="ddlRM" ValidationGroup="SubmitDetails" Display="Dynamic"
                InitialValue="0"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvRM" runat="server" ValidationGroup="Submit" ControlToValidate="ddlRM"
                ErrorMessage="Please select a RM" Operator="NotEqual" ValueToCompare="--Select--"
                CssClass="cvPCG" Display="Dynamic">
            </asp:CompareValidator>
        </td>
        <td align="right" id="tdCustomerSelection1" runat="server">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="false"
                CssClass="cmbField" Enabled="true">
            </asp:DropDownList>
            <span id="Span5" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator9" runat="server" ValidationGroup="SubmitDetails"
                ControlToValidate="ddlBranch" ErrorMessage="Please select a Branch" Operator="NotEqual"
                TextToCompare="--Select--" CssClass="cvPCG" Display="Dynamic">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr id="trBranchRM" runat="server">
        <%--<td align="right">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtBranch" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtRM" runat="server" CssClass="txtField"></asp:TextBox>
        </td>--%>
        <td align="right">
            <asp:Label ID="lblAssociateName" runat="server" CssClass="FieldName" Text="Associate Name: "></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAssociateName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="ReqtxtAssociateName" runat="server" ErrorMessage="Please Enter Associate Name"
                CssClass="rfvPCG" ControlToValidate="txtAssociateName" ValidationGroup="SubmitDetails"
                Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel" align="right">
            <asp:Label ID="lblAdviserAgentCode" runat="server" Text="SubBroker Code: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtAdviserAgentCode" MaxLength="20" runat="server" CssClass="txtField"
                Enabled="True"></asp:TextBox>
            <span id="Span9" class="spnRequiredField">*</span> &nbsp;
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAdviserAgentCode"
                ErrorMessage="Enter SubBroker code" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="SubmitDetails">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblPanNo" runat="server" Text="PAN No: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtPan" runat="server" MaxLength="10" CssClass="txtFieldUpper" Enabled="true"></asp:TextBox>
            <span id="Span7" class="spnRequiredField">*</span> &nbsp;
            <br />
            <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPan" ErrorMessage="Please enter a PAN Number"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="SubmitDetails">
            </asp:RequiredFieldValidator>
            <asp:Label ID="lblPanDuplicate" runat="server" Visible="false" CssClass="Error" Text="PAN Number already exists"></asp:Label>
            <asp:Label ID="lblPanlength" runat="server" Visible="false" CssClass="Error" Text="PAN Number should have 10 digits"></asp:Label>
            <asp:RegularExpressionValidator ID="revPan" runat="server" Display="Dynamic" ValidationGroup="btnEdit"
                CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPan"
                ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblAMFINo" CssClass="FieldName" runat="server" Text="AMFI/NISM number:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAMFINo" runat="server" CssClass="txtField" MaxLength="20"></asp:TextBox>
            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="btnEdit"  CssClass="rfvPCG"
                ErrorMessage="Enter only Numeric Values" ControlToValidate="txtAMFINo" ValidationExpression="[0-9]">
            </asp:RegularExpressionValidator>--%>
        </td>
        <td align="right">
            <asp:Label ID="lblAssociateExpiryDate" CssClass="FieldName" runat="server" Text="AMFI Number Expiry Date:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtAssociateExpDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar3" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput3" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtAssociateExpDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftLabel" align="right">
            <asp:Label ID="lblEUIN" runat="server" Text="EUIN: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtEUIN" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblStartDate" CssClass="FieldName" runat="server" Text="Agent Joining Date:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtStartDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar4" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput4" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtStartDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <span id="Span10" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtStartDate"
                ErrorMessage="<br />Please select Start Date" CssClass="cvPCG" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="SubmitDetails">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblEndDate" CssClass="FieldName" runat="server" Text="End Date:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtEndDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar5" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput5" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtEndDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" align="right">
            <asp:Label ID="lblAssociateType" runat="server" CssClass="FieldName" Text="Associate Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                Checked="true" GroupName="grpAssociateType" AutoPostBack="true" OnCheckedChanged="rbtnIndividual_CheckedChanged" />
            &nbsp;&nbsp;
            <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                GroupName="grpAssociateType" AutoPostBack="true" OnCheckedChanged="rbtnNonIndividual_CheckedChanged" />
        </td>
        <td class="leftField" align="right">
            <asp:Label ID="lblAssociateSubType" runat="server" CssClass="FieldName" Text="Associate Sub Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAssociateSubType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="ddlAssociateSubType"
                ErrorMessage="Please select a Sub-Type" Operator="NotEqual" ValueToCompare="Select"
                ValidationGroup="Submit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblDept" runat="server" Text="Privilege Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlDepart" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span11" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Department"
                CssClass="rfvPCG" ControlToValidate="ddlDepart" ValidationGroup="SubmitDetails"
                InitialValue="0" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" CssClass="txtField" />
        </td>
        <td colspan="2">
            <asp:Button ID="btnPreviewSend" runat="server" Visible="false" target="_blank" Text="Generate & Mail Welcome letter"
                CssClass="PCGButton" OnClick="lbtnPreviewSend_Click" />
                <br />
                 <br />
            <asp:LinkButton ID="lbtnPreviewSend" CssClass="LinkButtons" Visible="false" OnClientClick="window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);"
                runat="server" OnClick="btnPreviewSend_Click">View Welcome Letter</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label3" runat="server" Text="Privilege Role:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData" colspan="5">
            <asp:Panel ID="PnlDepartRole" runat="server" ScrollBars="Horizontal" Width="800px"
                Visible="false">
                <telerik:RadListBox ID="chkbldepart" runat="server" CheckBoxes="true" AutoPostBack="true"
                    CssClass="horizontalListbox">
                </telerik:RadListBox>
                <span id="Span12" class="spnRequiredField">*</span>
                <%--     <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please check multiple applications allowed"
                ClientValidationFunction="validateCheckBoxList" EnableClientScript="true" Display="Dynamic"
                ValidationGroup="Submit" CssClass="rfvPCG">
            </asp:CustomValidator>--%>
                <%--<asp:CheckBoxList ID="chkbldepart" runat="server" RepeatDirection="Horizontal" Width="100px" CssClass="cmbField" ></asp:CheckBoxList>--%>
                <%--  <asp:CustomValidator ID="Cvdchkbldepart" runat="server" ControlToValidate="chkbldepart"
                    CssClass="rfvPCG" EnableClientScript="true" ValidationGroup="Submit" ErrorMessage="Please Selected One of the Role"></asp:CustomValidator>--%>
                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ErrorMessage="Select a role!" ControlToValidate="chkbldepart" 
     ValidationGroup="btnSubmit" Display="Dynamic" CssClass="rfvPCG" />--%>
            </asp:Panel>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="BtnSave" runat="server" Visible="true" Text="Submit" CssClass="PCGButton"
                OnClick="btnSubmit_Click" ValidationGroup="SubmitDetails" />
            <asp:Button ID="btnAssociateUpdate" runat="server" Text="Update" CssClass="PCGButton"
                OnClick="btnAssociateUpdate_OnClick" Visible="false" ValidationGroup="SubmitDetails" />
        </td>
    </tr>
</table>
<telerik:RadTabStrip ID="RadTabStripAssociatesDetails" runat="server" EnableTheming="True"
    Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="AssociatesDetails" SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Contact Details" Value="ContactDetaild" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Address" Value="Address" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Other Info" Value="OtherInfo" TabIndex="2">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Bank Details" Value="BankDetails" TabIndex="3">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Agent Qualification" Value="Registration" TabIndex="4">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Nominee" Value="Nominee" TabIndex="5">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Category" Value="Category" TabIndex="6">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Business Details" Value="Business_Details" TabIndex="7">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Child Codes" Value="Child_Codes" Visible="false"
            TabIndex="8">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="AssociatesDetails" EnableViewState="true" runat="server">
    <telerik:RadPageView ID="rpvContactDetails" runat="server">
        <asp:Panel ID="pnlContactDetails" runat="server">
            <table style="width: 100%; height: 170px;">
                <tr>
                    <td class="tdSectionHeading" colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 200px; float: left">
                                &nbsp;
                                <asp:Label ID="Label5" runat="server" Text="Contact Details"></asp:Label>
                            </div>
                            <div class="divViewEdit" style="float: right; padding-right: 50px">
                                <asp:LinkButton ID="lnkContactDetails" runat="server" Text="Edit" CssClass="LinkButtons"
                                    OnClick="lnkContactDetails_OnClick" Visible="false"></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table id="tblMessage" width="100%" runat="server" visible="true" style="padding-top: 0px;">
                            <tr id="trSumbitSuccess">
                                <td align="center">
                                    <div id="msgRecordStatus" class="success-msg" align="center" runat="server" visible="false">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblResNo" CssClass="FieldName" runat="server" Text="Telephone No.(Res):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtResPhoneNoIsd" runat="server" Width="30px" CssClass="txtField"
                            Enabled="false" MaxLength="3">91</asp:TextBox>
                        <asp:TextBox ID="txtResPhoneNoStd" runat="server" Width="30px" CssClass="txtField"
                            MaxLength="3"></asp:TextBox>
                        <asp:TextBox ID="txtResPhoneNo" runat="server" Width="90px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                        <asp:CompareValidator ID="txtResPhoneNoIsd_CompareValidator" ControlToValidate="txtResPhoneNoIsd"
                            runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="txtResPhoneNoStd_CompareValidator" ControlToValidate="txtResPhoneNoStd"
                            runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD code."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="txtResPhoneNo_CompareValidator" ControlToValidate="txtResPhoneNo"
                            runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Phone number."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblResFax" CssClass="FieldName" runat="server" Text="Fax(Res):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtResFaxIsd" runat="server" Width="30px" CssClass="txtField" MaxLength="3"
                            Enabled="false">91</asp:TextBox>
                        <asp:TextBox ID="txtResFaxStd" runat="server" Width="30px" CssClass="txtField" MaxLength="3"></asp:TextBox>
                        <asp:TextBox ID="txtResFax" runat="server" Width="90px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                        <asp:CompareValidator ID="txtResFaxIsd_CompareValidator" ControlToValidate="txtResFaxIsd"
                            runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="txtResFaxStd_CompareValidator" ControlToValidate="txtResFaxStd"
                            runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD code."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="txtResFax_CompareValidator" ControlToValidate="txtResFax"
                            runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Fax number."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPhoneOfc" CssClass="FieldName" runat="server" Text="Telephone No.(Off):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtOfcPhoneNoIsd" runat="server" Width="30px" CssClass="txtField"
                            Enabled="false" MaxLength="3">91</asp:TextBox>
                        <asp:TextBox ID="txtOfcPhoneNoStd" runat="server" Width="30px" CssClass="txtField"
                            MaxLength="3"></asp:TextBox>
                        <asp:TextBox ID="txtOfcPhoneNo" runat="server" Width="90px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                        <asp:CompareValidator ID="txtOfcPhoneNoIsd_CompareValidator" ControlToValidate="txtOfcPhoneNoIsd"
                            runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="txtOfcPhoneNoStd_CompareValidator" ControlToValidate="txtOfcPhoneNoStd"
                            runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD code."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="txtOfcPhoneNo_CompareValidator" ControlToValidate="txtOfcPhoneNo"
                            runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Phone number."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblFaxOfc" CssClass="FieldName" runat="server" Text="Fax(Off):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtOfcFaxIsd" runat="server" Width="30px" CssClass="txtField" MaxLength="3"
                            Enabled="false">91</asp:TextBox>
                        <asp:TextBox ID="txtOfcFaxStd" runat="server" Width="30px" CssClass="txtField" MaxLength="3"></asp:TextBox>
                        <asp:TextBox ID="txtOfcFax" runat="server" Width="90px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                        <asp:CompareValidator ID="txtOfcFaxIsd_CompareValidator" ControlToValidate="txtOfcFaxIsd"
                            runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD Code."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="txtOfcFaxStd_CompareValidator" ControlToValidate="txtOfcFaxStd"
                            runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD Code."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="txtOfcFax_CompareValidator" ControlToValidate="txtOfcFax"
                            runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Fax Number."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblMobile1" CssClass="FieldName" runat="server" Text="Mobile1:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile1" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="rvMobile1" runat="server" ValidationGroup="Submit"
                            ControlToValidate="txtMobile1" Display="Dynamic" ErrorMessage="<br />Mobile Number must be 10 digit"
                            CssClass="rfvPCG" ValidationExpression="^((\+)?(\d{2}[-]))?(\d{10}){1}?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblEmail" CssClass="FieldName" runat="server" Text="Email:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="Span8" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmail"
                            ValidationGroup="Submit" ErrorMessage="Please enter an EmailId" Display="Dynamic"
                            runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                            ErrorMessage="Please enter a valid EmailId" Display="Dynamic" runat="server"
                            ValidationGroup="Submit" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            CssClass="revPCG"></asp:RegularExpressionValidator>
                        <%-- <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                            ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnContactDetails" runat="server" CssClass="PCGButton" Text="Submit"
                            OnClick="btnContactDetails_OnClick" Visible="false" />
                        <asp:Button ID="btnContactDetailsUpdate" runat="server" CssClass="PCGButton" Text="Update"
                            OnClick="btnContactDetailsUpdate_OnClick" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvAddress" runat="server">
        <asp:Panel ID="pnlAddress" runat="server">
            <table style="width: 100%; height: 196px;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 200px; float: left">
                                &nbsp; Corresponding Address
                            </div>
                            <div class="divViewEdit" style="float: right; padding-right: 50px">
                                <asp:LinkButton ID="lnkCrossPondingAddress" runat="server" CssClass="LinkButtons"
                                    Text="Edit" OnClick="lnkCrossPondingAddress_OnClick" Visible="false"></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table id="tblCrosspondance" width="100%" runat="server" visible="false" style="padding-top: 0px;">
                            <tr>
                                <td align="center">
                                    <div id="dvCrosspondance" class="success-msg" align="center" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCorLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorLine1" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblCorLine2" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorLine2" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCorLine3" runat="server" CssClass="FieldName" Text="Line3(Area):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorLine3" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblCorstate" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlCorState" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCorPin" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorPin" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtCorPin"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please enter a numeric value"
                            Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblCorCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorCity" runat="server" CssClass="txtField"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlCorCity" runat="server" CssClass="cmbField">
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCorCountry" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorCountry" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Permanent Address
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="chkAddressChk" runat="server" Text="Is permanent addess is same as correspondes address?"
                            CssClass="cmbFielde" OnCheckedChanged="chkAddressChk_CheckedChanged" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPermLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPermAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblPermLine2" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPermAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPermLine3" runat="server" CssClass="FieldName" Text="Line3(Area):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPermAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblPermState" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlPermAdrState" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPermPin" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPermAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                        <asp:CompareValidator ID="txtPermAdrPinCode_CompareValidator" runat="server" ControlToValidate="txtPermAdrPinCode"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please enter a numeric value"
                            Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblPermCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPermAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                        <%-- <asp:DropDownList ID="ddlPermAdrCity" runat="server" CssClass="cmbField">
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPermCountry" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPermAdrCountry" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCrossPondence" runat="server" CssClass="PCGButton" Text="Submit"
                            OnClick="btnCrossPondence_OnClick" Visible="false" />
                        <asp:Button ID="btnbtnCrossPondenceUpdate" runat="server" CssClass="PCGButton" Text="Update"
                            OnClick="btnbtnCrossPondenceUpdate_OnClick" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvOtherInformation" runat="server">
        <asp:Panel ID="pnlOtherInformation" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 200px; float: left">
                                &nbsp; Other Information
                            </div>
                            <div class="divViewEdit" style="float: right; padding-right: 50px">
                                <asp:LinkButton ID="lnkOtherInformation" runat="server" CssClass="LinkButtons" Text="Edit"
                                    OnClick="lnkOtherInformation_OnClick" Visible="false"></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <td colspan="4">
                    <table id="tblOther" width="100%" runat="server" visible="false" style="padding-top: 0px;">
                        <tr>
                            <td align="center">
                                <div id="dvOther" class="success-msg" align="center" runat="server">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label47" CssClass="FieldName" runat="server" Text="Marital Status:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <%-- <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="cmbField" OnChange="OnMaritalStatusChange(this)">
                    </asp:DropDownList>--%>
                        <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="cmbField" OnChange="OnMaritalStatusChange(this)">
                        </asp:DropDownList>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label11" CssClass="FieldName" runat="server" Text="Qualification:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlQualification" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" width="25%">
                        <asp:Label ID="Label9" runat="server" Text="Gender:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="cmbField">
                            <asp:ListItem Text="Male" Value="Male" Selected="True"> </asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"> </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Date of Birth:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:CompareValidator ID="cvDepositDate1" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtDOB" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        <telerik:RadDatePicker ID="txtDOB" CssClass="txtTo" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="OtherInformation" runat="server" CssClass="PCGButton" Text="Submit"
                            OnClick="OtherInformation_OnClick" Visible="false" />
                        <asp:Button ID="btnOtherInformationUpdate" runat="server" CssClass="PCGButton" Text="Update"
                            OnClick="btnOtherInformationUpdate_OnClick" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvBankDetails" runat="server">
        <asp:Panel ID="pnlBankDetails" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 200px; float: left">
                                &nbsp; Bank Details
                            </div>
                            <div class="divViewEdit" style="float: right; padding-right: 50px">
                                <asp:LinkButton ID="lnkBankDetails" runat="server" CssClass="LinkButtons" Text="Edit"
                                    OnClick="lnkBankDetails_OnClick" Visible="false"></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table id="tblBankDetails" width="100%" runat="server" visible="false" style="padding-top: 0px;">
                            <tr>
                                <td align="center">
                                    <div id="dvBankDetails" class="success-msg" align="center" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlBankName" CssClass="cmbField" runat="server">
                        </asp:DropDownList>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlBankName"
                            ValidationGroup="btnBank" ErrorMessage="<br />Please select a Bank Name" Operator="NotEqual"
                            ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblAccountType" runat="server" CssClass="FieldName" Text="Account Type:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span1" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAccountType"
                            ValidationGroup="btnBank" ErrorMessage="<br />Please select a Account Type" Operator="NotEqual"
                            ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblAccountNumber" runat="server" CssClass="FieldName" Text="Account Number:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="spAccountNumber" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="txtAccountNumber"
                            ValidationGroup="btnBank" ErrorMessage="<br />Please enter a Account Number"
                            Display="Dynamic" runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblBranchName" runat="server" Text="Branch Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBankBranchName" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="spBranchName" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvBranchName" ControlToValidate="txtBankBranchName"
                            ValidationGroup="btnBank" ErrorMessage="<br />Please enter a Branch Name" Display="Dynamic"
                            runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblAdrLine1" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField" Style="width: 250px;"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label6" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBankAdrLine2" runat="server" CssClass="txtField" Style="width: 250px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label12" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField" Style="width: 250px;"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label13" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCity" runat="server" CssClass="FieldName" Text="City:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlBankAdrCity" runat="server" CssClass="txtField" Width="150px">
                        </asp:DropDownList>--%>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblPinCode" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBankAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                        <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br />Enter a numeric value"
                            CssClass="rfvPCG" Type="Integer" ControlToValidate="txtBankAdrPinCode" ValidationGroup="btnBank"
                            Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblMicr" runat="server" Text="MICR:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtMicr" runat="server" CssClass="txtField" MaxLength="9"></asp:TextBox>
                        <asp:CompareValidator ID="cvMicr" runat="server" ErrorMessage="<br />Enter a numeric value"
                            CssClass="rfvPCG" Type="Integer" ValidationGroup="btnBank" ControlToValidate="txtMicr"
                            Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblIfsc" runat="server" Text="IFSC:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtIfsc" runat="server" CssClass="txtField" MaxLength="11"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnBankDetails" runat="server" CssClass="PCGButton" Text="Submit"
                            OnClick="btnBankDetails_OnClick" ValidationGroup="btnBank" Visible="false" />
                        <asp:Button ID="btnBankDetailsUpdate" runat="server" CssClass="PCGButton" Text="Update"
                            OnClick="btnBankDetailsUpdate_OnClick" ValidationGroup="btnBank" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvRegistration" runat="server">
        <asp:Panel ID="Panel1" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 200px; float: left">
                                &nbsp; Agent Qualification
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <telerik:RadGrid ID="gvRegistration" runat="server" CssClass="RadGrid" GridLines="Both"
                            Width="90%" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                            ShowStatusBar="true" AllowAutomaticDeletes="True" Skin="Telerik" OnItemCommand="gvRegistration_OnItemCommand"
                            OnNeedDataSource="gvRegistration_OnNeedDataSource" OnItemDataBound="gvRegistration_OnItemDataBound">
                            <MasterTableView DataKeyNames="" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                EditMode="EditForms" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                                CommandItemSettings-AddNewRecordText="Add Agent Qualification">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="PAG_assetGroupCode" HeaderStyle-Width="20px"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        HeaderText="Asset Group" UniqueName="PAG_assetGroupCode" SortExpression="PAG_assetGroupCode"
                                        AllowFiltering="true" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AAAR_Registrationumber" HeaderStyle-Width="20px"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        HeaderText="Registration Number" UniqueName="AAAR_Registrationumber" SortExpression="AAAR_Registrationumber"
                                        AllowFiltering="true" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AAAR_ExpiryDate" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Expiry Date" UniqueName="AAAR_ExpiryDate"
                                        SortExpression="AAAR_ExpiryDate" AllowFiltering="true" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template" PopUpSettings-Height="220px" PopUpSettings-Width="350px"
                                    CaptionFormatString="Add Registration">
                                    <FormTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblAssetCategory" runat="server" Text="Asset Category:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField">
                                                    </asp:DropDownList>
                                                    <span id="Span3" class="spnRequiredField">*</span>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="ReqddlLevel" runat="server" CssClass="rfvPCG" ErrorMessage="Please Select Asset Category"
                                                        Display="Dynamic" ControlToValidate="ddlCategory" ValidationGroup="btnOK" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblRegNo" runat="server" Text="Registration No:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRegNumber" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtRegNumber"
                                                        runat="server" Display="Dynamic" ErrorMessage="Enter Numeric Value" CssClass="cvPCG"
                                                        ValidationExpression="[0-9]\d*$" ValidationGroup="btnOK">     
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label4" runat="server" Text="Expiry Date:" CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtRegExpDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                                                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                                        <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                                        </Calendar>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="btnOK" Text="Submit" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                        CausesValidation="True" ValidationGroup="btnOK" />
                                                </td>
                                                <td class="rightData">
                                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                        CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                                </td>
                                                <td class="rightData">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FormTemplate>
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvNominee" runat="server">
        <asp:Panel ID="Panel2" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 200px; float: left">
                                &nbsp; Nominee
                            </div>
                            <div class="divViewEdit" style="float: right; padding-right: 50px">
                                <asp:LinkButton ID="lnkNominee" runat="server" CssClass="LinkButtons" Text="Edit"
                                    OnClick="lnkNominee_OnClick" Visible="false"></asp:LinkButton></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table id="tblNominee" width="100%" runat="server" visible="false" style="padding-top: 0px;">
                            <tr>
                                <td align="center">
                                    <div id="dvNominee" class="success-msg" align="center" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNomineeName" CssClass="FieldName" runat="server" Text="Name:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNomineeName" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblNomineeRel" CssClass="FieldName" runat="server" Text="Relationship:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNomineeRel" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNomineeAdress" runat="server" CssClass="FieldName" Text="Adress:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNomineeAdress" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblNomineePhone" runat="server" CssClass="FieldName" Text="Phone No:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNomineePhone" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left">
                        <asp:Label ID="lblGudHeader" runat="server" CssClass="FieldName" Text="Guardian details in case of minor nominee"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblGurdianName" CssClass="FieldName" runat="server" Text="Name:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGurdiannName" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblGuardianRel" CssClass="FieldName" runat="server" Text="Relationship:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGuardianRel" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblGuardianAdress" runat="server" CssClass="FieldName" Text="Adress:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGuardianAdress" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblGurdianPhone" runat="server" CssClass="FieldName" Text="Phone No:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGurdianPhone" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnNominee" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnNominee_OnClick"
                            Visible="false" />
                        <asp:Button ID="btnNomineeUpdate" runat="server" CssClass="PCGButton" Text="Update"
                            OnClick="btnNomineeUpdate_OnClick" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvCategory" runat="server">
        <asp:Panel ID="Panel3" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 200px; float: left">
                                &nbsp; Category
                            </div>
                            <div class="divViewEdit" style="float: right; padding-right: 50px">
                                <asp:LinkButton ID="lnkCategory" runat="server" CssClass="LinkButtons" Text="Edit"
                                    OnClick="lnkCategory_OnClick" Visible="false"></asp:LinkButton></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table id="tblCategory" width="100%" runat="server" visible="false" style="padding-top: 0px;">
                            <tr>
                                <td align="center">
                                    <div id="dvCategory" class="success-msg" align="center" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lbl" CssClass="FieldName" runat="server" Text="Adviser Category:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlAdviserCategory" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                    <td class="leftField">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCategory" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnCategory_OnClick"
                            Visible="false" />
                        <asp:Button ID="btnCategoryUpdate" runat="server" CssClass="PCGButton" Text="Update"
                            OnClick="btnCategoryUpdate_OnClick" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvBuisnessDetails" runat="server">
        <asp:Panel ID="Panel4" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 200px; float: left">
                                &nbsp; Business Details
                            </div>
                            <div class="divViewEdit" style="float: right; padding-right: 50px">
                                <asp:LinkButton ID="lnkBusinessDetails" runat="server" Text="Edit" CssClass="LinkButtons"
                                    OnClick="lnkBusinessDetails_OnClick" Visible="false"></asp:LinkButton>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table id="tblBusinessDetails" width="100%" runat="server" visible="false" style="padding-top: 0px;">
                            <tr>
                                <td align="center">
                                    <div id="dvBusinessDetails" class="success-msg" align="center" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblNoBranches" CssClass="FieldName" runat="server" Text="No of branches:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtNoBranches" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblNoofSales" CssClass="FieldName" runat="server" Text="No. of sales employees:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtNoofSales" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblNoofSubbrockers" runat="server" CssClass="FieldName" Text="No. of sub brokers:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtNoofSubBrokers" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblNoofClients" runat="server" CssClass="FieldName" Text="No. of clients:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtNoofClients" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblExpSelling" CssClass="FieldName" runat="server" Text="Experience in selling :"
                            Visible="false"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <%--<asp:CheckBox ID="chkAssociates" runat="server" Text="Insurance" CssClass="cmbField"
                            value="IN" />
                        <asp:CheckBox ID="chkMf" runat="server" Text="MF" CssClass="cmbField" value="MF" />
                        <asp:CheckBox ID="chlIpo" runat="server" Text="IPO" CssClass="cmbField" value="IPO" />
                        <asp:CheckBox ID="chkfd" runat="server" Text="FD" CssClass="cmbField" value="FD" />
                         <asp:CheckBox ID="chkEQ" runat="server" Text="EQ" CssClass="cmbField" value="EQ" />
                          <asp:CheckBox ID="chkDebt" runat="server" Text="Debt" CssClass="cmbField" value="Debt" />
                            <asp:CheckBox ID="chkPMS" runat="server" Text="PMS" CssClass="cmbField" value="PMS" />--%>
                        <asp:CheckBoxList ID="chkModules" runat="server" CssClass="FieldName" RepeatDirection="Horizontal"
                            Visible="false">
                            <asp:ListItem Text="MF" Value="MF"></asp:ListItem>
                            <asp:ListItem Text="IPO" Value="IP"></asp:ListItem>
                            <asp:ListItem Text="FD" Value="FD"></asp:ListItem>
                            <asp:ListItem Text="EQ" Value="DE"></asp:ListItem>
                            <asp:ListItem Text="Debt" Value="DT"></asp:ListItem>
                            <asp:ListItem Text="PMS" Value="PM"></asp:ListItem>
                            <asp:ListItem Text="Insurance" Value="IN"></asp:ListItem>
                            <asp:ListItem Text="Bond" Value="BO"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnBusinessDetails" runat="server" CssClass="PCGButton" Text="Submit"
                            OnClick="btnBusinessDetails_OnClick" Visible="false" />
                        <asp:Button ID="btnBusinessDetailsUpdate" runat="server" CssClass="PCGButton" Text="Update"
                            OnClick="btnBusinessDetailsUpdate_OnClick" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvChildCodes" runat="server">
        <asp:Panel ID="Panel5" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Child Codes
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <table id="tableGrid" runat="server" width="100%">
                            <tr>
                                <td align="center">
                                    <asp:LinkButton ID="lbkbtnAddChildCodes" Enabled="false" CssClass="LinkButtons" runat="server"
                                        OnClick="lnkBtnChildCodes_Click">Add Child Codes</asp:LinkButton>
                                    <%-- <telerik:RadGrid ID="gvChildCode" runat="server" Skin="Telerik" CssClass="RadGrid"
                                        GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                                        ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                                        OnNeedDataSource="gvChildCode_OnNeedDataSource" Visible="false" AllowAutomaticUpdates="false"
                                        HorizontalAlign="NotSet"  >
                                        <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="AAC_AdviserAgentId,AAC_AdviserAgentIdParent,AAC_AgentCode"
                                            CommandItemSettings-ShowRefreshButton="false" CommandItemSettings-AddNewRecordText="Add child codes">
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="AAC_AgentCode" HeaderText="Child Code" DataField="AAC_AgentCode">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridEditCommandColumn EditText="Update" UniqueName="editColumn" CancelText="Cancel"
                                                    UpdateText="Edit">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete the code?"
                                                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                    Text="Delete">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <ClientEvents />
                                        </ClientSettings>
                                    </telerik:RadGrid>--%>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblChildCodeList" runat="server" Text="ChildCode List" CssClass="FieldName"></asp:Label>
                                    <asp:Label ID="lblChildCodeListView" runat="server" CssClass="txtField"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<table>
    <tr>
        <td>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<asp:HiddenField ID="hdnAdviserID" runat="server" />
