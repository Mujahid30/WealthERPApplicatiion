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

<script type="text/javascript">
    function chkPanExists() {
        $("#<%= hidValidCheck.ClientID %>").val("0");
        if ($("#<%=txtPan.ClientID %>").val() == "") {
            $("#spnLoginStatus").html("");
            return;
        }
        $("#spnLoginStatus").html("<img src='Images/loader.gif' />");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "ControlHost.aspx/CheckPANNoAvailabilityForAssociates",
            data: "{ 'PanNumber': '" + $("#<%=txtPan.ClientID %>").val() + "','adviserId': '" + $("#<%=hdnAdviserID.ClientID %>").val() + "' }",
            error: function(xhr, status, error) {

            },
            success: function(msg) {

                if (msg.d) {

                    $("#<%= hidValidCheck.ClientID %>").val("1");
                    $("#spnLoginStatus").html("");
                }
                else {

                    $("#<%= hidValidCheck.ClientID %>").val("0");
                    $("#spnLoginStatus").removeClass();
                    alert("Pan Number Already Exists");
                    return false;
                }

            }
        });
    }
</script>

<script language="javascript" type="text/javascript">
    
    function validateCheckBoxList(sender, args) {
        var isAnyCheckBoxChecked = false;
        var checkBoxes = document.getElementById("<%= chkbldepart.ClientID %>").getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox") {
                if (checkBoxes[i].checked) {
                    isAnyCheckBoxChecked = true;
                    break;
                }
            }
        }
        if (!isAnyCheckBoxChecked) {
            alert("Please Select Role for the associates!!");
        }
        return isAnyCheckBoxChecked;
    }
</script>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left" id="head" runat="server">
                            Add Associates
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
                CssClass="rfvPCG" ControlToValidate="ddlTitleList" ValidationGroup="Submit" Display="Dynamic"
                InitialValue="0"></asp:RequiredFieldValidator>
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
                CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span5" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator9" runat="server" ValidationGroup="Submit"
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
                CssClass="rfvPCG" ControlToValidate="txtAssociateName" ValidationGroup="Submit"
                Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel" align="right">
            <asp:Label ID="lblAdviserAgentCode" runat="server" Text="Adviser Agent Code: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtAdviserAgentCode" runat="server" CssClass="txtField" Enabled="False"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblPanNo" runat="server" Text="PAN No: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:TextBox ID="txtPan" runat="server" CssClass="txtFieldUpper" Enabled="true" onblur="return chkPanExists()"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblAMFINo" CssClass="FieldName" runat="server" Text="AMFI/NISM number:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAMFINo" runat="server" CssClass="txtField" MaxLength="20"></asp:TextBox>
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
            <asp:Label ID="lblStartDate" CssClass="FieldName" runat="server" Text="Start Date:"></asp:Label>
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
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblDept" runat="server" Text="Department Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlDepart" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span11" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Department"
                CssClass="rfvPCG" ControlToValidate="ddlDepart" ValidationGroup="Submit" InitialValue="0"
                Display="Dynamic">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label3" runat="server" Text=" Department Roles :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData" colspan="5">
            <asp:Panel ID="PnlDepartRole" runat="server" ScrollBars="Horizontal" Width="800px"
                Visible="false">
                <telerik:RadListBox ID="chkbldepart" runat="server" CheckBoxes="true" AutoPostBack="true"
                    CssClass="horizontalListbox">
                </telerik:RadListBox>
                <span id="Span12" class="spnRequiredField">*</span>
                 <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please check multiple applications allowed"
                ClientValidationFunction="validateCheckBoxList" EnableClientScript="true" Display="Dynamic"
                ValidationGroup="Submit" CssClass="rfvPCG">
            </asp:CustomValidator>
                <%--<asp:CheckBoxList ID="chkbldepart" runat="server" RepeatDirection="Horizontal" Width="100px" CssClass="cmbField" ></asp:CheckBoxList>--%>
              <%--  <asp:CustomValidator ID="Cvdchkbldepart" runat="server" ControlToValidate="chkbldepart"
                    CssClass="rfvPCG" EnableClientScript="true" ValidationGroup="Submit" ErrorMessage="Please Selected One of the Role"></asp:CustomValidator>--%>
                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ErrorMessage="Select a role!" ControlToValidate="chkbldepart" 
     ValidationGroup="btnSubmit" Display="Dynamic" CssClass="rfvPCG" />--%>
            </asp:Panel>
        </td>
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
        <telerik:RadTab runat="server" Text="Registration" Value="Registration" TabIndex="4">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Nominee" Value="Nominee" TabIndex="5">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Category" Value="Category" TabIndex="6">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Business Details" Value="Business_Details" TabIndex="7">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="AssociatesDetails" EnableViewState="true" runat="server">
    <telerik:RadPageView ID="rpvContactDetails" runat="server">
        <asp:Panel ID="pnlContactDetails" runat="server">
            <table style="width: 100%; height: 170px;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Contact Details
                        </div>
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
                        <asp:RegularExpressionValidator ID="rvMobile1" runat="server" ValidationGroup="btnEdit"
                            ControlToValidate="txtMobile1" Display="Dynamic" ErrorMessage="<br />Telephone Number must be 7-11 digit"
                            ValidationExpression="^((\+)?(\d{2}[-]))?(\d{10}){1}?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblEmail" CssClass="FieldName" runat="server" Text="Email:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                            ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
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
                            Corresponding Address
                        </div>
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
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvOtherInformation" runat="server">
        <asp:Panel ID="pnlOtherInformation" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Other Information
                        </div>
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
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvBankDetails" runat="server">
        <asp:Panel ID="pnlBankDetails" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Bank Details
                        </div>
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
                            ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Bank Name" Operator="NotEqual"
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
                            ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Account Type"
                            Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
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
                            ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Account Number"
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
                            ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Branch Name" Display="Dynamic"
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
                            CssClass="rfvPCG" Type="Integer" ControlToValidate="txtBankAdrPinCode" ValidationGroup="btnSubmit"
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
                            CssClass="rfvPCG" Type="Integer" ValidationGroup="btnSubmit" ControlToValidate="txtMicr"
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
                    </td>
                    <td colspan="3">
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
                            Registration
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblAssetCategory" CssClass="FieldName" runat="server" Text="Asset Category:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblRegNo" CssClass="FieldName" runat="server" Text="Registration No:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Expiry Date:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <telerik:RadDatePicker ID="txtRegExpDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtRegExpDate" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td>
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
                            Nominee
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblNomineeName" CssClass="FieldName" runat="server" Text="Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtNomineeName" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblNomineeRel" CssClass="FieldName" runat="server" Text="Relationship:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlNomineeRel" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblNomineeAdress" runat="server" CssClass="FieldName" Text="Adress:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtNomineeAdress" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblNomineePhone" runat="server" CssClass="FieldName" Text="Phone No:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtNomineePhone" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left">
                        <asp:Label ID="lblGudHeader" runat="server" CssClass="FieldName" Text="Guardian details in case of minor nominee"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGurdianName" CssClass="FieldName" runat="server" Text="Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGurdiannName" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblGuardianRel" CssClass="FieldName" runat="server" Text="Relationship:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlGuardianRel" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGuardianAdress" runat="server" CssClass="FieldName" Text="Adress:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGuardianAdress" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblGurdianPhone" runat="server" CssClass="FieldName" Text="Phone No:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGurdianPhone" runat="server" CssClass="txtField"></asp:TextBox>
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
                            Category
                        </div>
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
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvBuisnessDetails" runat="server">
        <asp:Panel ID="Panel4" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Business Details
                        </div>
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
                        <asp:Label ID="lblExpSelling" CssClass="FieldName" runat="server" Text="Experience in selling :"></asp:Label>
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
                        <asp:CheckBoxList ID="chkModules" runat="server" CssClass="FieldName" RepeatDirection="Horizontal">
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
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<table>
    <tr>
        <td>
        </td>
        <td colspan="3">
            <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="PCGButton" OnClick="Update_Click"
                Visible="false" />
            <asp:Button ID="BtnSave" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click"
                ValidationGroup="Submit" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<asp:HiddenField ID="hdnAdviserID" runat="server" />
