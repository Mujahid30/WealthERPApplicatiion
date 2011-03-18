<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFFAdd.ascx.cs" Inherits="WealthERP.SuperAdmin.IFFAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1">

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<style>
    .error
    {
        color: Red;
        font-weight: bold;
        font-size: 12px;
    }
    .success
    {
        color: Green;
        font-weight: bold;
        font-size: 12px;
    }
    input
    {
        border: 1px solid #ccc;
        color: #333333;
        font-size: 12px;
        margin-top: 2px;
        padding: 3px;
        width: 50px;
    }
    .left-td
    {
        text-align: right;
        width: 52%;
        padding-left: 100px;
        color: #16518A;
    }
    .right-td
    {
        text-align: left;
    }
    .spnRequiredField
    {
        color: #FF0033;
        font-size: x-small;
    }    
</style>

<script type="text/javascript" language="javascript">
    function checkSelection() {
        var form = document.forms[0];
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                }
            }
        }
        if (count == 0) {
            alert("Please select atleast one LOB")
            return false;
        }
        return true;
    }

    function checkIsDelete(selectObj) {
        if (selectObj.selectedIndex == 3) //3- Delete Option
        {
            if (confirm('Are you sure you want to delete ?')) {
                this.document.forms[0].submit();
            }
            else
                selectObj.selectedIndex = 0
        }
        else {
            this.document.forms[0].submit();
        }
    }
    function Validation() {
        if (document.getElementById('hidStatus.ClientId').value == "0") {
            if (document.getElementById('hidValid').value == 0) {
                alert('Your Selected Login id is not available. Please choose some other Login Id');
                return false;
            }
        }
    }
   
</script>

<script type="text/javascript">
    function checkLoginId() {
        $("#hidValid").val("0");
        if ($("#<%=txtLoginId.ClientID %>").val() == "") {
            $("#spnLoginStatus").html("");
            return;
        }
        $("#spnLoginStatus").html("<img src='/Images/loader.gif' />");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "ControlHost.aspx/CheckLoginIdAvailability",
            data: "{ 'loginId': '" + $("#<%=txtLoginId.ClientID %>").val() + "' }",
            error: function(xhr, status, error) {
                alert("Sorry. Something went wrong!");
            },
            success: function(msg) {

                if (msg.d) {
                    $("#hidValid").val("1");
                    $("#spnLoginStatus").removeClass();
                    $("#spnLoginStatus").addClass("success");
                    $("#spnLoginStatus").html("available");
                }
                else {
                    $("#hidValid").val("0");
                    $("#spnLoginStatus").removeClass();
                    $("#spnLoginStatus").addClass("error");
                    $("#spnLoginStatus").html("Not available");
                }
            }

        });
    }
    // To validate whether the current changes have been validated or not
    function isValid() {
        if (document.getElementById('hidValid').value == '1') {
            Page_ClientValidate();
            return Page_IsValid;
        }
        else {
            Page_ClientValidate();
            return false;
        }
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<div>
    <asp:Label ID="lblIFFAdd" runat="server" Text="Add IFF" CssClass="HeaderTextBig"></asp:Label>
    <hr />
</div>
<table width="100%">
    <tr>
        <td align="center">
            <div class="success-msg" id="CreationSuccessMessage" runat="server" visible="false"
                align="center">
                IFF Successfully Created
            </div>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div class="success-msg" id="MailSentSuccessMessage" runat="server" visible="false"
                align="center">
                <asp:Label ID="loginIdSendMsg" runat="server" Visible="true"></asp:Label>
            </div>
        </td>
    </tr>
</table>
<div>
    <asp:LinkButton ID="lbtnIFFEdit" CssClass="LinkButtons" runat="server" Visible="false"
        OnClick="lbtnIFFEdit_Click">Edit</asp:LinkButton>
</div>
<table width="100%">
    <tr>
        <td align="right">
            <asp:Label ID="lblNameOfIFF" Text="Name of IFF:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNameofIFF" CssClass="txtField" runat="server" Width="175px"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="NameOfIFFRequiredFieldValidator" runat="server" ControlToValidate="txtNameofIFF"
                ErrorMessage="Name Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblContactPerson" Text="Contact Person:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtContactPerson" CssClass="txtField" runat="server" Width="175px"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="ContactPersonRequiredFieldValidator" runat="server"
                ControlToValidate="txtContactPerson" ErrorMessage="Contact Person Required" CssClass="cvPCG"
                Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblAddressLine1" Text="Address Line 1:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAddressLine1" CssClass="txtField" runat="server" Width="175px"
                MaxLength="75"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="AddressLine1RequiredFieldValidator" runat="server"
                ControlToValidate="txtAddressLine1" ErrorMessage="Atleast Address Line 1 Required"
                CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblAddressLine2" Text="Address Line 2:" CssClass="FieldName" runat="server" MaxLength="75"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAddressLine2" CssClass="txtField" runat="server" Width="175px"
                MaxLength="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblAddressLine3" Text="Area:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAddressLine3" CssClass="txtField" runat="server" Width="175px"
                MaxLength="75"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblCountry" Text="Country:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCountry" CssClass="txtField" runat="server" Width="175px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblCity" Text="City:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCity" CssClass="txtField" runat="server" Width="175px"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblPinCode" Text="PinCode:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPinCode" CssClass="txtField" runat="server" Width="175px" MaxLength="6"></asp:TextBox>
            <span id="Span8" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPinCode"
                ErrorMessage="Pincode Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
            <%--            <asp:CompareValidator ID="pincodeCompare" CssClass="cvPCG" ControlToValidate="txtPinCode"
                runat="server" Display="Dynamic" ErrorMessage="Please give only numbers" Operator="DataTypeCheck"
                Type="Integer"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtPinCode"
                Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblEmailId" Text="Email Id:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEmailId" CssClass="txtField" runat="server" Width="175px"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="txtEmailId"
                ErrorMessage="Email Id Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                CssClass="cvPCG" ControlToValidate="txtEmailId" ErrorMessage="Type Valid Email Id"
                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblMobileNo" Text="Mobile No:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMobileNo" CssClass="txtField" runat="server" MaxLength="10" ToolTip="Enter 10 digit mobile number neglecting 0 or +91"
                Width="175px"></asp:TextBox>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMobileNo"
                ErrorMessage="Mobile No Required" CssClass="cvPCG" Display="Dynamic" ToolTip="Enter "></asp:RequiredFieldValidator>
            <%--            <asp:CompareValidator ID="mobileCompareValidator" CssClass="cvPCG" ControlToValidate="txtMobileNo"
                runat="server" Display="Dynamic" ErrorMessage="Please give only numbers" Operator="DataTypeCheck"
                Type="Double"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtMobileNo"
                Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblLoginId" Text="Login Id:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtLoginId" CssClass="txtField" runat="server" Width="175px" onchange="checkLoginId()"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span><span id="spnLoginStatus"></span>
            <asp:RequiredFieldValidator ID="LoginIdRequiredFieldValidator" runat="server" ControlToValidate="txtLoginId"
                ErrorMessage="Login Id Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblTelephoneNumber" Text="Telephone Number:" CssClass="FieldName"
                runat="server"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtTelephoneNumber" CssClass="txtField" runat="server" MaxLength="8"
                Width="175px"></asp:TextBox>
            <%--<span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTelephoneNumber"
                ErrorMessage="Telephone No Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
            <%--            <asp:CompareValidator ID="telephoneValidator" ControlToValidate="txtTelephoneNumber"
                runat="server" Display="Dynamic" ErrorMessage="Please give only numbers" Operator="DataTypeCheck"
                Type="Integer"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="cvPCG"
                ErrorMessage="Please give only Numbers" ValidationExpression="\d+" ControlToValidate="txtTelephoneNumber"
                Display="Dynamic"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblCategory" Text="Category:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" Width="175px">
            </asp:DropDownList>
            <span id="Span11" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidatorForCategory" runat="server" CssClass="cvPCG"
                ControlToValidate="ddlCategory" ValueToCompare="Select" Operator="NotEqual" ErrorMessage="Please Select a Category"
                SetFocusOnError="true"></asp:CompareValidator>
            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCategory" SetFocusOnError="true"
                ErrorMessage="Select a category" CssClass="cvPCG" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>--%>
        </td>
        <td runat="server" id="lblActivation" align="right">
            <asp:Label ID="lblActivationDate" Text="Activation Date:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td runat="server" id="Activation">
            <asp:TextBox ID="txtActivationDate" CssClass="txtField" runat="server" Width="175px"></asp:TextBox>
            <ajaxToolKit:CalendarExtender runat="server" Format="dd/MM/yyyy" TargetControlID="txtActivationDate"
                ID="calExeActivationDate" Enabled="true">
            </ajaxToolKit:CalendarExtender>
            <ajaxToolKit:TextBoxWatermarkExtender TargetControlID="txtActivationDate" WatermarkText="dd/mm/yyyy"
                runat="server" ID="wmtxtActivationDate">
            </ajaxToolKit:TextBoxWatermarkExtender>
            <span id="Span9" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtActivationDate"
                ErrorMessage="ActivationDate Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td id="Td1" runat="server" align="right">
            <asp:Label ID="lblStatus" Text="Status:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlStatus" CssClass="cmbField" AutoPostBack="true" runat="server"
                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Width="175px">
            </asp:DropDownList>
        </td>
        <td runat="server" id="lblDeactivation" align="right">
            <asp:Label ID="lblDeactivationDate" Text="Deactivation Date:" CssClass="FieldName"
                runat="server"></asp:Label>
        </td>
        <td runat="server" id="Deactivation">
            <asp:TextBox ID="txtDeactivationDate" CssClass="txtField" runat="server" Width="175px"></asp:TextBox>
            <ajaxToolKit:CalendarExtender runat="server" Format="dd/MM/yyyy" TargetControlID="txtDeactivationDate"
                ID="calExeDeactivationDate" Enabled="true">
            </ajaxToolKit:CalendarExtender>
            <ajaxToolKit:TextBoxWatermarkExtender TargetControlID="txtDeactivationDate" WatermarkText="dd/mm/yyyy"
                runat="server" ID="wmtxtDeactivationDate">
            </ajaxToolKit:TextBoxWatermarkExtender>
            <span id="Span10" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDeactivationDate"
                ErrorMessage="DeActivationDate Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator CssClass="cvPCG" Display="Dynamic" ID="dateCompareValidator"
                runat="server"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            &nbsp;<asp:Label ID="lblLOBDetails" runat="server" Text="LOB Details" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td align="center">
                        <div class="failure-msg" id="lblMsg" runat="server" visible="false" align="center">
                            LOB List is Empty..
                        </div>
                    </td>
                </tr>
            </table>
            <%--<asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label>--%>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvLOBList" runat="server" AllowSorting="False" AutoGenerateColumns="False"
                DataKeyNames="LOBId" ShowFooter="true" AllowPaging="true" CssClass="GridViewStyle"
                OnPageIndexChanging="gvLOBList_PageIndexChanging">
                <FooterStyle CssClass="FieldName" HorizontalAlign="Center" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle " />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEditLOB" runat="server" OnClick="lnkEditLOB_OnClick" Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IsDependent" ItemStyle-HorizontalAlign="Center">
                        
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsDependent" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkIsDependent" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Broker Name" HeaderText="Broker Name" />
                    <asp:BoundField DataField="Business Type" HeaderText="Business Type" />
                    <asp:BoundField DataField="Identifier" HeaderText="Identifier" />
                    <asp:BoundField DataField="Identifier Type" HeaderText="Identifier Type" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click"
                OnClientClick="return Validation()" />
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnAddLOB" runat="server" Text="Add Lob" CssClass="PCGMediumButton"
                OnClick="btnAddLOB_Click" Width="112px" />
        </td>
        <td>
            <asp:Button ID="btnSendLoginId" runat="server" Text="Send Login Id" CssClass="PCGMediumButton"
                OnClick="btnSendLoginId_Click" />
        </td>
        <td>
            <asp:Button ID="btnSubscription" runat="server" Text="Add/Edit Subscription" CssClass="PCGMediumButton" OnClick="btnSubscription_Click"
                />
        </td>
    </tr>
</table>
<input type="hidden" id="hidValid" />
<input type="hidden" id="hidStatus" runat="server" />
<asp:TextBox ID="txtActivationHidden" runat="server" Style="display: none" />