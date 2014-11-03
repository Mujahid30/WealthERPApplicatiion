<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="EditCustomerIndividualProfile.ascx.cs"
    Inherits="WealthERP.Customer.EditCustomerIndividualProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script type="text/javascript" src="../Scripts/tabber.js"></script>

<script type="text/javascript">
    function ButtonClick() {
        return true;
    }
</script>

<script type="text/javascript">
    function CloseWindowsPopUp() {
        window.close();
    }
</script>

<script type="text/javascript">
    function DisplayPanId(val) {
        document.getElementById('ctrl_EditCustomerIndividualProfile_gvFamilyAssociate_ctl00_ctl02_ctl03_lblGetPan').innerHTML = val;
    }
    function Realinvestor() {
        if (document.getElementById('<%=chkRealInvestor.ClientID%>').checked == false) {
            var con = confirm("Are you sure about the change?\nCustomer will be mark as non investor and will not be accessible.!!");

            if (con == true) {
                document.getElementById('<%=chkRealInvestor.ClientID%>').checked = false;
                return true;
            }
            if (con == false) {
                document.getElementById('<%=chkRealInvestor.ClientID%>').checked = true;
                return false;
            }
        }
    }
</script>

<%--
<script type="text/javascript" language="javascript">
    function Compare() {
        var birthday = document.getElementById('<%=txtDate.ClientID%>').value;
        var marriageday = document.getElementById('<%=txtMarriageDate.ClientID%>').value;
        if (birthday >= marriageday) {
            alert("Please fill Birthday Date less than Marriage Date");
            document.getElementById('<%=txtDate.ClientID%>').value = ""; 
        }
    }

</script>--%>

<script type="text/javascript" language="javascript">
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            //sender._selectedDate = todayDate;
            //sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            sender._textbox.set_Value('');
            alert("Warning! - Date cannot be in the future");
            document.getElementById('<%= btnEdit.ClientID %>').focus(); // to make the focus out of the textbox
        }
    }
    function OnMaritalStatusChange(ddlMaritalStatus) {
        var selectedValue = document.getElementById(ddlMaritalStatus.id).value;
        document.getElementById('<%= txtMarriageDate.ClientID %>').value = 'dd/mm/yyyy';
        var dateinput = $find("<%= txtMarriageDate.ClientID %>");
        if (selectedValue == 'MA') {
            dateinput.set_enabled(true);
        }
        else {
            dateinput.set_enabled(false);
        }
    }
</script>

<script type="text/javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Complete Profile
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="rightField" style="width: 75%;">
            <table style="width: 100%;">
                <tr>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                            GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnIndividual_CheckedChanged" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                            GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnNonIndividual_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblBranchName" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAdviserBranchList" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="ddlAdviserBranchList_CompareValidator" runat="server" ControlToValidate="ddlAdviserBranchList"
                            ErrorMessage="Please select a Branch" Operator="NotEqual" ValueToCompare="Select a Branch"
                            CssClass="cvPCG">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="RM Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM Name:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCustomerSubType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span id="Span2" class="spnRequiredField">*</span>
                        <asp:CheckBox ID="chkRealInvestor" runat="server" CssClass="txtField" Text="Investor"
                            AutoPostBack="false" OnCheckedChanged="chkRealInvestor_OnCheckedChanged" onClick="Realinvestor();" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                            ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select a Sub-Type"
                            CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date of Profiling:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <%--<asp:TextBox ID="txtProfilingDate" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>--%>
                        <telerik:RadDatePicker ID="txtProfilingDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Client Code:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="txtField" MaxLength="16"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revCustCode" runat="server" Display="Dynamic"
                            CssClass="cvPCG" ControlToValidate="txtCustomerCode" ValidationExpression="^[0-9a-zA-Z ]+$"
                            ErrorMessage="Please enter alphanumeric of max 16 digit" ValidationGroup="btnEdit"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGender" runat="server" CssClass="FieldName" Text="Gender:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:RadioButton ID="rbtnMale" runat="server" CssClass="txtField" Text="Male" GroupName="rbtnGender" />
                        <asp:RadioButton ID="rbtnFemale" runat="server" CssClass="txtField" Text="Female"
                            GroupName="rbtnGender" />
                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                        <span id="Span4" class="spnRequiredField">*</span> &nbsp; &nbsp;&nbsp;
                        <asp:CheckBox ID="chkdummypan" runat="server" OnCheckedChanged="chkdummypan_click"
                            CssClass="txtField" Text="Dummy PAN" AutoPostBack="true" />
                        <br />
                        <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                            Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnEdit">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revPan" runat="server" Display="Dynamic" ValidationGroup="btnEdit"
                            ErrorMessage="Please check PAN Format" ControlToValidate="txtPanNumber" CssClass="rfvPCG"
                            ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                        </asp:RegularExpressionValidator>
                        <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="PAN Number already exists"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                    </td>
                    <td class="rightField">
                        <asp:CheckBox ID="chkKYC" runat="server" CssClass="txtField" Text="MF KYC" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Salutation:"></asp:Label>
                    </td>
                    <td class="rightField" width="75%">
                        <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="cmbField">
                            <asp:ListItem>Select a Salutation</asp:ListItem>
                            <asp:ListItem>Mr.</asp:ListItem>
                            <asp:ListItem>Mrs.</asp:ListItem>
                            <asp:ListItem>Ms.</asp:ListItem>
                            <asp:ListItem>M/S.</asp:ListItem>
                            <asp:ListItem>Dr.</asp:ListItem>
                        </asp:DropDownList>
                        <%--            <span id="Span5" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cmpddlSalutation" runat="server" ControlToValidate="ddlSalutation"
                ErrorMessage="Please select a Salutation for customer" Operator="NotEqual" ValueToCompare="Select a Salutation"
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                        <span id="Span1" class="spnRequiredField">*</span>
                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtFirstName" ErrorMessage="Please enter the First Name"
                            Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnEdit">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trGuardianName" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Guardian Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtGuardianFirstName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                        <asp:TextBox ID="txtGuardianMiddleName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                        <asp:TextBox ID="txtGuardianLastName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
        <td class="leftField">
            <%--<asp:Label ID="lblMfKYC" runat="server" CssClass="FieldName" Text="MF KYC:"></asp:Label>--%>
                <%-- </td>
        <td class="rightField">
            <asp:RadioButton ID="rbtnkycYes" runat="server" CssClass="txtField" Text="Yes" GroupName="rbtnKYC" />
            <asp:RadioButton ID="rbtnKycNo" runat="server" CssClass="txtField" Text="No" Checked="true" GroupName="rbtnKYC" />
        </td>
    </tr>--%>
                <tr id="trForResidence" runat="server">
                    <td align="right">
                        <asp:Label ID="lblFatherHusband" runat="server" Text="Father/Husband Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFatherHusband" runat="server" CssClass="txtField" Width="300px"></asp:TextBox>
                        <%-- </td>
        <td  style="width: 30%;">--%>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblSlabForOther" runat="server" CssClass="FieldName" Text="Tax slab applicable(%):"></asp:Label>
                        <%-- </td>
        <td>--%>
                        <asp:TextBox ID="txtSlab" runat="server" CssClass="txtField" Width="157px"></asp:TextBox>
                        <asp:CompareValidator ID="cmpareSlabForOther" ControlToValidate="txtSlab" runat="server"
                            Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Tax slab."
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnGetSlab" runat="server" Text="Get the slab" CssClass="PCGMediumButton"
                            OnClick="btnGetSlab_Click" />
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="cvPCG" ValidationGroup="btnEdit"
                    DisplayMode="List" />
            </div>
        </td>
    </tr>
</table>
<telerik:RadTabStrip ID="RadTabStripCustomerProfile" runat="server" EnableTheming="True"
    Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="CustomerProfileDetails"
    SelectedIndex="2">
    <Tabs>
        <telerik:RadTab runat="server" Text="Family Associates" Value="FamilyAssociates"
            TabIndex="0" Visible="false">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="ISA Account" Value="ISAAccount" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Bank Details" Value="BankDeatils" TabIndex="2">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Correspondence Address" Value="CorrespondenceAddress"
            TabIndex="3">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Permanent Address" Value="PermanentAddress"
            TabIndex="4">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Office Address" Value="OfficeAddress" TabIndex="5">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Contact Details" Value="ContactDetails" TabIndex="6">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Additional Information" Value="AdditionalInformation"
            TabIndex="7">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="CustomerProfileDetails" EnableViewState="true" runat="server">
    <telerik:RadPageView ID="rpvFamilyAssociates" runat="server" Visible="false">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="width: 100%;" visible="false">
                    <tr>
                        <td>
                            <div class="divSectionHeading" style="vertical-align: text-bottom">
                                Family Associates
                            </div>
                        </td>
                    </tr>
                    <tr id="trFamilyAssociates" runat="server">
                        <td style="width: 45%">
                            <div id="dvFamilyAssociate" runat="server" style="width: 95%; padding-top: 5px; padding-left: 10px;">
                                <asp:Panel runat="server" ID="pnlFamily" Width="80%" Height="300px" ScrollBars="Both">
                                    <telerik:RadGrid ID="gvFamilyAssociate" runat="server" CssClass="RadGrid" GridLines="Both"
                                        Width="90%" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                                        ShowStatusBar="true" AllowAutomaticDeletes="True" Skin="Telerik" OnItemDataBound="gvFamilyAssociate_ItemDataBound"
                                        OnItemCommand="gvFamilyAssociate_ItemCommand" OnNeedDataSource="gvFamilyAssociate_NeedDataSource">
                                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                            FileName="Family Associates" Excel-Format="ExcelML">
                                        </ExportSettings>
                                        <%--<MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None">--%>
                                        <MasterTableView DataKeyNames="CA_AssociationId,XR_RelationshipCode,C_PANNum,AB_BranchId,C_AssociateCustomerId,C_IsKYCAvailable,C_IsRealInvestor"
                                            Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" EditMode="EditForms"
                                            CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false" CommandItemSettings-AddNewRecordText="Add Family Associates">
                                            <Columns>
                                                <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="50px" EditText="View/Edit"
                                                    UniqueName="editColumn" CancelText="Cancel" UpdateText="Update">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="AssociateName" HeaderText="Member name" UniqueName="AssociateName"
                                                    SortExpression="AssociateName">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="C_PANNum" HeaderText="PAN" UniqueName="C_PANNum"
                                                    SortExpression="C_PANNum">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="XR_Relationship" HeaderText="Relationship" AllowFiltering="false"
                                                    UniqueName="XR_Relationship" SortExpression="XR_Relationship">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Association?"
                                                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                    Text="Delete">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings FormTableStyle-Height="10px" EditFormType="Template" FormTableStyle-Width="1000px"
                                                PopUpSettings-ScrollBars="Both">
                                                <FormTemplate>
                                                    <table width="100%" style="background-color: White">
                                                        <tr id="trCustomerTypeSelection" runat="server">
                                                            <td align="right">
                                                                <asp:Label ID="lblSelectCustomerType" runat="server" CssClass="FieldName" Text="Add Associate From:"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButton ID="rbtnExisting" AutoPostBack="true" runat="server" GroupName="Date"
                                                                    OnCheckedChanged="rbtnType_CheckedChanged" />
                                                                <asp:Label ID="lblNew" runat="server" Text="Existing Customer" CssClass="Field"></asp:Label>
                                                                &nbsp;
                                                                <asp:RadioButton ID="rbtnNew" AutoPostBack="true" OnCheckedChanged="rbtnType_CheckedChanged"
                                                                    runat="server" GroupName="Date" />
                                                                <asp:Label ID="lblPickPeriod" runat="server" Text="New Customer" CssClass="Field"></asp:Label>
                                                            </td>
                                                            <td colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr id="trExCustHeader" runat="server">
                                                            <td colspan="4">
                                                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                                    Add Associate From Existing Customer
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trExCustomerType" runat="server">
                                                            <td colspan="4">
                                                                <table width="50%">
                                                                    <tr>
                                                                        <td class="leftField" align="right">
                                                                            <asp:Label ID="lblMemberBranch" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlMemberBranch" runat="server" CssClass="cmbField" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlMemberBranch_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="leftField" align="right">
                                                                            <asp:Label ID="lblMember" runat="server" CssClass="FieldName" Text="Member Name:"></asp:Label>
                                                                        </td>
                                                                        <td class="rightField">
                                                                            <asp:TextBox ID="txtMember" runat="server" CssClass="txtField" Text='<%# Bind("AssociateName") %>'
                                                                                AutoComplete="Off" AutoPostBack="True"></asp:TextBox>
                                                                            <span id="Span8" class="spnRequiredField">*</span>
                                                                            <cc1:TextBoxWatermarkExtender ID="txtMember_water" TargetControlID="txtMember" WatermarkText="Enter few chars of Customer"
                                                                                runat="server" EnableViewState="false">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                            <ajaxToolkit:AutoCompleteExtender ID="txtMember_autoCompleteExtender" runat="server"
                                                                                TargetControlID="txtMember" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                                                                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                                                                                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                                                                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                                                                                Enabled="True" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtMember"
                                                                                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                                                                                CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chKInsideKyc" runat="server" Text=" IS KYC" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td class="leftField" align="right">
                                                                            <asp:Label ID="lblPan" runat="server" CssClass="FieldName" Text="PAN:"></asp:Label>
                                                                        </td>
                                                                        <td class="rightField" runat="server">
                                                                            <asp:Label ID="lblGetPan" runat="server" Text='<%# Bind("C_PANNum") %>' CssClass="FieldName"></asp:Label>
                                                                            <asp:TextBox ID="txtPan" runat="server" Text='<%# Bind("C_PANNum") %>' CssClass="txtField"></asp:TextBox>
                                                                            <%--<span id="Span10" class="spnRequiredField">*</span>--%><asp:Label ID="lblspan"
                                                                                runat="server" CssClass="spnRequiredField">*</asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPan"
                                                                                ErrorMessage="<br />Please Enter PAN Number" Display="Dynamic" runat="server"
                                                                                CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkIsinvestmem" Text="IS Real Investor" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label18" runat="server" CssClass="FieldName" Text="Date of birth:"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%-- <asp:TextBox ID="txtCustomerDOB1" runat="server" CssClass="txtField" Text='<%#Convert.ToDateTime(Eval("C_DOB")!=null ? Eval("C_DOB"): "0")%>'></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtCustomerDOB1_CalendarExtender" runat="server" TargetControlID="txtCustomerDOB1"
                                                                    Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                                                                </cc1:CalendarExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="txtCustomerDOB1_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                                                                    TargetControlID="txtCustomerDOB1" runat="server">
                                                                </cc1:TextBoxWatermarkExtender>--%>
                                                                <telerik:RadDatePicker ID="RadDatePicker1" CssClass="txtField" runat="server" Culture="English (United States)"
                                                                    AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                                                                    Visible="false" MinDate="1900-01-01" TabIndex="5" SelectedDate='<%# (Eval("C_DOB") != null && Eval("C_DOB") is DateTime) ?Convert.ToDateTime(Eval("C_DOB")) : (DateTime?)null %>'>
                                                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                        Skin="Telerik" EnableEmbeddedSkins="false">
                                                                    </Calendar>
                                                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                                                    </DateInput>
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftField" align="right">
                                                                <asp:Label ID="lblRelation" runat="server" CssClass="FieldName" Text="RelationShip:"></asp:Label>
                                                            </td>
                                                            <td class="rightField">
                                                                <asp:DropDownList ID="ddlRelation" runat="server" CssClass="cmbField">
                                                                </asp:DropDownList>
                                                                <span id="Span7" class="spnRequiredField">*</span>
                                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlRelation"
                                                                    CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select Relation"
                                                                    Operator="NotEqual" ValidationGroup="Submit" ValueToCompare="Select"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="trExSubmit" runat="server">
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="Button3" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    ValidationGroup="Submit"></asp:Button>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="Button4" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                                    CommandName="Cancel"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    </td> </tr>
                                                    <tr id="trNewCustHeader" runat="server">
                                                        <td colspan="4">
                                                            <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                                Add Associate From New Customer
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="trNewCustomer" runat="server">
                                                        <td colspan="4">
                                                            <table width="50%">
                                                                <tr>
                                                                    <td class="leftField" align="right">
                                                                        <asp:Label ID="lblNewName" runat="server" CssClass="FieldName" Text="Member Name:"></asp:Label>
                                                                    </td>
                                                                    <td class="rightField">
                                                                        <asp:TextBox ID="txtNewName" runat="server" Text='<%# Bind("AssociateName") %>' CssClass="txtField"></asp:TextBox>
                                                                        <span id="spnTransType" class="spnRequiredField">*</span>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNewName"
                                                                            ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                                                                            CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chKInsideKyc1" runat="server" Text="IS KYC" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftField" align="right">
                                                                        <asp:Label ID="lblNewPan" runat="server" CssClass="FieldName" Text="PAN:"></asp:Label>
                                                                    </td>
                                                                    <td class="rightField">
                                                                        <asp:TextBox ID="txtNewPan" runat="server" Text='<%# Bind("C_PANNum") %>' CssClass="txtField"></asp:TextBox>
                                                                        <span id="Span6" class="spnRequiredField">*</span>
                                                                        <asp:Label ID="Label10" runat="server" CssClass="Error" Text="PAN Number already exists"
                                                                            Visible="false"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNewPan"
                                                                            ErrorMessage="<br />Please Enter PAN Number" Display="Dynamic" runat="server"
                                                                            CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="isRealInvestormem" runat="server" Text="ISRealInvestor" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftField" align="right">
                                                                        <asp:Label ID="lblNewRelationShip" runat="server" CssClass="FieldName" Text="RelationShip :"></asp:Label>
                                                                    </td>
                                                                    <td class="rightField">
                                                                        <asp:DropDownList ID="ddlNewRelationship" runat="server" CssClass="cmbField">
                                                                        </asp:DropDownList>
                                                                        <span id="Span5" class="spnRequiredField">*</span>
                                                                        <asp:CompareValidator ID="CVTrxType" runat="server" ControlToValidate="ddlNewRelationship"
                                                                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select Relation"
                                                                            Operator="NotEqual" ValidationGroup="Submit" ValueToCompare="Select"></asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="lblCustomerDOB" runat="server" CssClass="FieldName" Text="Date of birth:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadDatePicker ID="txtCustomerDOB" CssClass="txtField" runat="server" Culture="English (United States)"
                                                                            AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                                                                            MinDate="1900-01-01" TabIndex="5">
                                                                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                                Skin="Telerik" EnableEmbeddedSkins="false">
                                                                            </Calendar>
                                                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        <span id="Span3" class="spnRequiredField">*</span>
                                                                        <asp:RequiredFieldValidator ID="reqCustomerDOB" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Enter date of birth" ControlToValidate="txtCustomerDOB" ValidationGroup="Submit"
                                                                            InitialValue="" CssClass="cvPCG">
                                                                        </asp:RequiredFieldValidator>
                                                                        <%-- <asp:TextBox ID="txtCustomerDOB1" runat="server" CssClass="txtField"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtCustomerDOB_CalendarExtender" runat="server" TargetControlID="txtCustomerDOB"
                                                                            Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                                                                        </cc1:CalendarExtender>--%>
                                                                        <%-- <cc1:TextBoxWatermarkExtender ID="txtLivingSince_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                                                                            TargetControlID="txtLivingSince" runat="server">
                                                                        </cc1:TextBoxWatermarkExtender>--%>
                                                                    </td>
                                                                    <%--<tr>
                                                <td>
                                                </td>
                                                <td align="leftField">
                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="PCGButton" ValidationGroup="Submit"
                                                        OnClick="btnSave_Click" />
                                                </td>
                                            </tr>--%>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                            runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                            ValidationGroup="Submit"></asp:Button>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                                            CommandName="Cancel"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                </FormTemplate>
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvISAAccount" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%-- <table width="100%">--%>
                <%--<tr>
                        <td align="left">
                            <div class="divSectionHeading" style="vertical-align: text-bottom">
                                <table>
                                    <tr>
                                        <td>
                                            <span>ISA Account List</span>
                                            <asp:ImageButton ID="btnImgAddCustomer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                                                AlternateText="Add" runat="server" ToolTip="Click here to Add New ISA Account"
                                                Height="15px" Width="15px" OnClick="btnImgAddCustomer_Click"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>--%>
                <%--<tr>
                        <td>--%>
                <div style="width: 100%; padding-left: 10px; padding-top: 5px;" class="divSectionHeading">
                    <table>
                        <tr>
                            <td>
                                <span>ISA Account List</span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 100%; padding-left: 10px; padding-top: 5px;">
                    <telerik:RadGrid ID="gvISAAccountList" runat="server" CssClass="RadGrid" GridLines="Both"
                        Width="70%" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
                        ShowStatusBar="true" AllowAutomaticDeletes="True" Skin="Telerik" OnItemDataBound="gvISAAccountList_ItemDataBound"
                        OnNeedDataSource="gvISAAccountList_NeedDataSource" OnItemCommand="gvISAAccountList_ItemCommand">
                        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="CISAA_accountid" EditMode="EditForms" CommandItemDisplay="Top"
                            CommandItemSettings-ShowRefreshButton="false" CommandItemSettings-AddNewRecordText="Add New ISA Account">
                            <Columns>
                                <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="50px" EditText="View/Edit"
                                    UniqueName="editColumn" CancelText="Cancel" UpdateText="Update">
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn UniqueName="CISAA_AccountNumber" HeaderStyle-Width="100px"
                                    HeaderText="ISA Number" DataField="CISAA_AccountNumber" SortExpression="CISAA_AccountNumber"
                                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                    <HeaderStyle></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="XMOH_ModeOfHolding" HeaderStyle-Width="100px"
                                    HeaderText="Mode Of Holding" DataField="XMOH_ModeOfHolding" SortExpression="XMOH_ModeOfHolding"
                                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                    <HeaderStyle></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="AISAQ_RequestQueueid" HeaderStyle-Width="100px"
                                    HeaderText="Request Id" DataField="AISAQ_RequestQueueid" SortExpression="AISAQ_RequestQueueid"
                                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                    <HeaderStyle></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" UniqueName="CISAA_accountid" HeaderStyle-Width="100px"
                                    HeaderText="AccountId" DataField="CISAA_accountid" SortExpression="CISAA_accountid"
                                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                    <HeaderStyle></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this ISA Account?"
                                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                    Text="Delete">
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings FormTableStyle-Height="10px" EditFormType="Template" FormTableStyle-Width="1000px">
                                <FormTemplate>
                                    <table width="100%" style="background-color: White">
                                        <tr id="trNewISAAccountSection" runat="server">
                                            <td>
                                                <div class="divSectionHeading" style="vertical-align: text-bottom; width: 97%">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 33%" align="left">
                                                                <asp:Label ID="lblISAAccountheading" Text="ISA Account Set up" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 30%" align="left">
                                                                <asp:Label ID="lblNomineeListHeading" Text="Nominee List" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                <asp:Label ID="JointHolderHeading" Text="JointHolder List" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 17%" align="left">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="width: 30%; float: left; padding-left: 10px; padding-top: 5px;">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <br />
                                                            <td class="leftField">
                                                                <asp:Label ID="lblJointHolding" runat="server" CssClass="FieldName" Text="Joint Holding :"></asp:Label>
                                                            </td>
                                                            <td class="rightField">
                                                                <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                                                                    Text="Yes" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged" />
                                                                <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                                                                    Text="No" AutoPostBack="true" OnCheckedChanged="rbtnNo_CheckedChanged" Checked="true" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftField">
                                                                <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding :"></asp:Label>
                                                            </td>
                                                            <td class="rightField">
                                                                <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField">
                                                                </asp:DropDownList>
                                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlModeOfHolding"
                                                                    CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select Mode Of Holding"
                                                                    Operator="NotEqual" ValidationGroup="GenerateISA" ValueToCompare="Select Mode of Holding"></asp:CompareValidator>
                                                                <span id="Span9" class="spnRequiredField">*</span>
                                                            </td>
                                                        </tr>
                                                        <tr id="trISAAccountNo" runat="server" visible="false">
                                                            <td class="leftField">
                                                                <asp:Label ID="lblISAAccountNo" runat="server" CssClass="FieldName" Text="ISA Number :"></asp:Label>
                                                            </td>
                                                            <td class="rightField">
                                                                <asp:TextBox ID="txtISANumber" runat="server" CssClass="txtField"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftField">
                                                                <asp:Label ID="Label51" runat="server" CssClass="FieldName" Text="Is Operated With POA :"></asp:Label>
                                                            </td>
                                                            <td class="rightField">
                                                                <asp:RadioButton ID="rbtnPOAYes" runat="server" CssClass="cmbField" GroupName="rbtnPOA"
                                                                    Text="Yes" />
                                                                <asp:RadioButton ID="rbtnPOANo" runat="server" CssClass="cmbField" GroupName="rbtnPOA"
                                                                    Checked="true" Text="No" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="width: 60%" id="divAssociate" runat="server">
                                                    <table style="width: 100%;" id="tblAssociate" runat="server">
                                                        <tr id="trAssociate" runat="server">
                                                            <td id="tdNominees" align="left" style="padding-left: 30px;" runat="server">
                                                                <asp:Panel ID="pnlNominiees" runat="server" Height="145px" ScrollBars="Vertical">
                                                                    <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                        DataKeyNames="MemberCustomerId, AssociationId" CssClass="GridViewStyle">
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
                                                                            <asp:BoundField DataField="C_AssociateCustomerId" HeaderText="CA_AssociationId" Visible="false" />
                                                                            <asp:BoundField DataField="CISAAA_Associationtype" HeaderText="CISAAA_Associationtype"
                                                                                Visible="false" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                            <td id="tdJointHolders" align="left" style="padding-left: 30px;" runat="server">
                                                                <asp:Panel ID="pnlJointholders" runat="server" Height="145px" ScrollBars="Vertical">
                                                                    <asp:GridView ID="gvJointHoldersList" runat="server" AutoGenerateColumns="False"
                                                                        CellPadding="4" DataKeyNames="MemberCustomerId, AssociationId" CssClass="GridViewStyle">
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
                                                                            <asp:BoundField DataField="C_AssociateCustomerId" HeaderText="CA_AssociationId" Visible="false" />
                                                                            <asp:BoundField DataField="CISAAA_Associationtype" HeaderText="CISAAA_Associationtype"
                                                                                Visible="false" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="padding-left: 230px; padding-top: 5px;">
                                                <asp:Button ID="btnGenerateISA" runat="server" Text="Generate ISA" CssClass="PCGLongButton"
                                                    OnClick="btnGenerateISA_Click" ValidationGroup="GenerateISA" />
                                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update ISA" %>'
                                                    runat="server" CssClass="PCGLongButton" OnClick="btnUpdateISA_Click" ValidationGroup="GenerateISA">
                                                </asp:Button>
                                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                    CommandName="Cancel"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </FormTemplate>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <%--<asp:GridView ID="gvISAAccountList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    DataKeyNames="CISAA_accountid" CssClass="GridViewStyle">
                                    <FooterStyle CssClass="FooterStyle" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="CISAA_AccountNumber" HeaderText="ISA Number" />
                                        <asp:BoundField DataField="XMOH_ModeOfHolding" HeaderText="Mode Of Holding" />
                                    </Columns>
                                </asp:GridView>--%>
                </div>
                <%--  </td>
                    </tr>
                </table>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvBankDeatils" runat="server">
        <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>--%>
        <table width="100%">
            <tr style="padding-top: 5px;">
                <td>
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Customer Bank Details
                    </div>
                </td>
            </tr>
        </table>
        <div style="width: 100%; padding-left: 10px; padding-top: 5px;">
            <telerik:RadGrid ID="gvBankDetails" runat="server" CssClass="RadGrid" GridLines="Both"
                Width="90%" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
                ShowStatusBar="true" AllowAutomaticDeletes="True" Skin="Telerik" OnItemCommand="gvBankDetails_ItemCommand"
                OnNeedDataSource="gvBankDetails_NeedDataSource">
                <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
                </ExportSettings>
                <MasterTableView DataKeyNames="CB_CustBankAccId,CB_AccountNum,WCMV_BankName,CB_HoldingAmount,XMOH_ModeOfHoldingCode,WCMV_LookupId_AccType,WCMV_Lookup_BranchAddStateId,WCMV_LookupId_BankId"
                    CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="true" CommandItemSettings-AddNewRecordText="Add New Bank Details">
                    <EditFormSettings EditFormType="Template" FormTableStyle-BorderStyle="None">
                        <FormTemplate>
                        </FormTemplate>
                    </EditFormSettings>
                    <Columns>
                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Edit/View" UniqueName="ButtonColumn"
                            CommandName="Edit">
                            <HeaderStyle Width="100px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn UniqueName="WCMV_BankName" HeaderStyle-Width="100px" HeaderText="Bank Name"
                            DataField="WCMV_BankName" SortExpression="WCMV_BankName" AllowFiltering="true"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CB_BranchName" HeaderStyle-Width="100px" HeaderText="Branch Name"
                            DataField="CB_BranchName" SortExpression="CB_BranchName" AllowFiltering="true"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WCMV_BankAccountType" HeaderStyle-Width="100px"
                            HeaderText="Account Type" DataField="WCMV_BankAccountType" SortExpression="WCMV_BankAccountType"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="XMOH_ModeOfHoldingCode" HeaderStyle-Width="100px"
                            HeaderText="Mode Of Operation" DataField="XMOH_ModeOfHoldingCode" SortExpression="XMOH_ModeOfHoldingCode"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CB_AccountNum" HeaderStyle-Width="100px" HeaderText="Account Number"
                            DataField="CB_AccountNum" SortExpression="CB_AccountNum" AllowFiltering="true"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn Visible="false" UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Scheme?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_IFSC" HeaderStyle-Width="150px"
                            HeaderText="IFSC" DataField="CB_IFSC" SortExpression="CB_IFSC" AllowFiltering="true"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_MICR" HeaderStyle-Width="150px"
                            HeaderText="MICR" DataField="CB_MICR" SortExpression="CB_MICR" AllowFiltering="true"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrCountry" HeaderStyle-Width="150px"
                            HeaderText="Branch Country" DataField="CB_BranchAdrCountry" SortExpression="CB_BranchAdrCountry"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrState" HeaderStyle-Width="150px"
                            HeaderText="Branch State" DataField="CB_BranchAdrState" SortExpression="CB_BranchAdrState"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrCity" HeaderStyle-Width="150px"
                            HeaderText="Branch City" DataField="CB_BranchAdrCity" SortExpression="CB_BranchAdrCity"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrPinCode" HeaderStyle-Width="150px"
                            HeaderText="PinCode" DataField="CB_BranchAdrPinCode" SortExpression="CB_BranchAdrPinCode"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine3" HeaderStyle-Width="150px"
                            HeaderText="Branch Line3" DataField="CB_BranchAdrLine3" SortExpression="CB_BranchAdrLine3"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine2" HeaderStyle-Width="150px"
                            HeaderText="Branch Line2" DataField="CB_BranchAdrLine2" SortExpression="CB_BranchAdrLine2"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine1" HeaderStyle-Width="150px"
                            HeaderText="Branch Line1" DataField="CB_BranchAdrLine1" SortExpression="CB_BranchAdrLine1"
                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                    </Columns>
                    <%--<EditFormSettings FormTableStyle-Height="10px" EditFormType="Template" FormTableStyle-Width="1000px">
                                <FormTemplate>
                                    <table width="100%" style="background-color: White">
                                        <tr>
                                            <td colspan="4">
                                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                    Bank Details
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblAccountType" runat="server" CssClass="FieldName" Text="Account Type:"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbField">
                                                </asp:DropDownList>
                                                <span id="Span1" class="spnRequiredField">*</span>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAccountType"
                                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Account Type"
                                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblAccountNumber" runat="server" CssClass="FieldName" Text="Account Number:"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField" Text='<%# Bind("CB_AccountNum") %>'></asp:TextBox>
                                                <span id="spAccountNumber" class="spnRequiredField">*</span>
                                                <asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="txtAccountNumber"
                                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Account Number"
                                                    Display="Dynamic" runat="server" CssClass="rfvPCG">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblModeOfOperation" runat="server" Text="Mode of Operation:" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:DropDownList ID="ddlModeOfOperation" CssClass="cmbField" runat="server">
                                                </asp:DropDownList>
                                                <span id="Span2" class="spnRequiredField">*</span>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlModeOfOperation"
                                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a mode of holding"
                                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                              
                                                <asp:DropDownList ID="ddlBankName" CssClass="cmbField" runat="server">
                                                </asp:DropDownList>
                                                <span id="spBankName" class="spnRequiredField">*</span>
                                              
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblBranchName" runat="server" Text="Branch Name:" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField" Style="width: 250px;"
                                                    Text='<%# Bind("CB_BranchName") %>'></asp:TextBox>
                                                <span id="spBranchName" class="spnRequiredField">*</span>
                                                <asp:RequiredFieldValidator ID="rfvBranchName" ControlToValidate="txtBranchName"
                                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Branch Name" Display="Dynamic"
                                                    runat="server" CssClass="rfvPCG">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                    Branch Details
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblAdrLine1" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField" Style="width: 250px;"
                                                    Text='<%# Bind("CB_BranchAdrLine1") %>'></asp:TextBox>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtBankAdrLine2" runat="server" CssClass="txtField" Style="width: 250px;"
                                                    Text='<%# Bind("CB_BranchAdrLine2") %>'></asp:TextBox>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField" Style="width: 250px;"
                                                    Text='<%# Bind("CB_BranchAdrLine3") %>'></asp:TextBox>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblCity" runat="server" CssClass="FieldName" Text="City:"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField" Text='<%# Bind("CB_BranchAdrCity") %>'></asp:TextBox>
                                            </td>
                                            <td class="leftField">
                                                <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblPinCode" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtBankAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"
                                                    Text='<%# Bind("CB_BranchAdrPinCode") %>'></asp:TextBox>
                                                <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br />Enter a numeric value"
                                                    CssClass="rfvPCG" Type="Integer" ControlToValidate="txtBankAdrPinCode" ValidationGroup="btnSubmit"
                                                    Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                            </td>
                                            <td class="leftField">
                                                <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:DropDownList ID="ddlBankAdrCountry" runat="server" CssClass="cmbField">
                                                    <asp:ListItem>India</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftField">
                                                <asp:Label ID="lblMicr" runat="server" Text="MICR:" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtMicr" runat="server" CssClass="txtField" MaxLength="9" Text='<%# Bind("CB_MICR") %>'></asp:TextBox>
                                                <asp:CompareValidator ID="cvMicr" runat="server" ErrorMessage="<br />Enter a numeric value"
                                                    CssClass="rfvPCG" Type="Integer" ValidationGroup="btnSubmit" ControlToValidate="txtMicr"
                                                    Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                            </td>
                                            <td class="leftField">
                                                <asp:Label ID="lblIfsc" runat="server" Text="IFSC:" CssClass="FieldName"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:TextBox ID="txtIfsc" runat="server" CssClass="txtField" MaxLength="11" Text='<%# Bind("CB_IFSC") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                    ValidationGroup="btnSubmit"></asp:Button>
                                            </td>
                                            <td visible="false">
                                                <asp:Button Visible="false" ID="btnYes" runat="server" Text="Submit and Addmore"
                                                    CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBankDetails_btnYes','L');"
                                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBankDetails_btnYes','L');"
                                                    ValidationGroup="btnSubmit" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                    CommandName="Cancel"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </FormTemplate>
                            </EditFormSettings>--%>
                    <%-- OnCommand="ButtonClick"--%>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
            </telerik:RadGrid>
            </br> </br> </br> </br> </br> </br> </br> </br> </br> </br> </br>
        </div>
        <%--</ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>--%>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvCorrespondenceAddress" runat="server">
        <asp:Panel ID="pnlCorrespondenceAddress" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Correspondence Address
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label11" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                        <span id="spBranchName" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="reqtxtCorrAdrLine1" runat="server" ControlToValidate="txtCorrAdrLine1"
                            ValidationGroup="btnEdit" InitialValue="" Display="Dynamic" ErrorMessage="Please fill address line1"
                            CssClass="rfvPCG"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label12" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label13" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField" Style="width: 75%"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblResidenceLivingDate" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtLivingSince" runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtLivingSince_CalendarExtender" runat="server" TargetControlID="txtLivingSince"
                            Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtLivingSince_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                            TargetControlID="txtLivingSince" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:CompareValidator ID="txtLivingSince_CompareValidator" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtLivingSince" CssClass="cvPCG" Operator="DataTypeCheck"
                            Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label14" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlCorrAdrCity" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span10" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="reqddlCorrAdrCity" runat="server" CssClass="cvPCG"
                            ControlToValidate="ddlCorrAdrCity" ErrorMessage="Select city" Display="Dynamic"
                            ValidationGroup="btnEdit" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label16" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span11" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="cvPCG"
                            ControlToValidate="ddlCorrAdrState" ErrorMessage="Select state" Display="Dynamic"
                            ValidationGroup="btnEdit" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label15" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                        <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                            runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <span id="Span12" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="cvPCG"
                            ControlToValidate="txtCorrAdrPinCode" ErrorMessage="Enter PIN code" Display="Dynamic"
                            ValidationGroup="btnEdit" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label17" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCorrAdrCountry" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvPermanentAddress" runat="server">
        <asp:Panel ID="pnlPermanentAddress" runat="server">
            <table style="width: 100%; height: 196px;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Permanent Address
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="3">
                        <asp:CheckBox ID="chkCorrPerm" runat="server" CssClass="FieldName" Text="Same as Correspondence Address" />
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label19" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtPermAdrLine1" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label20" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtPermAdrLine2" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label21" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtPermAdrLine3" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label22" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlPermAdrCity" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label23" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlPermAdrState" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label24" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPermAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                        <asp:CompareValidator ID="txtPermAdrPinCode_CompareValidator" ControlToValidate="txtPermAdrPinCode"
                            runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label25" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPermAdrCountry" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvOfficeAddress" runat="server">
        <asp:Panel ID="pnlOfficeAddress" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Office Address
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label34" CssClass="FieldName" runat="server" Text="Company Name:"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtOfcCompanyName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label27" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtOfcAdrLine1" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label28" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtOfcAdrLine2" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label29" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtOfcAdrLine3" runat="server" CssClass="txtField" Style="width: 78%"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblJobStartDate" CssClass="FieldName" runat="server" Text="Job Start Date:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtJobStartDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtJobStartDate_CalendarExtender" runat="server" TargetControlID="txtJobStartDate"
                            Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtJobStartDate_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                            TargetControlID="txtJobStartDate" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:CompareValidator ID="cvJobStartDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtJobStartDate" CssClass="cvPCG" Operator="DataTypeCheck"
                            Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label30" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlOfcAdrCity" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label31" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlOfcAdrState" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label32" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtOfcAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                        <asp:CompareValidator ID="txtOfcAdrPinCode_CompareValidator" ControlToValidate="txtOfcAdrPinCode"
                            runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                            Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label33" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtOfcAdrCountry" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
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
                        <asp:Label ID="Label36" CssClass="FieldName" runat="server" Text="Telephone No.(Res):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtResPhoneNoIsd" runat="server" Width="30px" CssClass="txtField"
                            MaxLength="3">91</asp:TextBox>
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
                        <asp:Label ID="Label41" CssClass="FieldName" runat="server" Text="Fax(Res):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtResFaxIsd" runat="server" Width="30px" CssClass="txtField" MaxLength="3">91</asp:TextBox>
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
                        <asp:Label ID="Label37" CssClass="FieldName" runat="server" Text="Telephone No.(Off):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtOfcPhoneNoIsd" runat="server" Width="30px" CssClass="txtField"
                            MaxLength="3">91</asp:TextBox>
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
                        <asp:Label ID="Label42" CssClass="FieldName" runat="server" Text="Fax(Off):"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtOfcFaxIsd" runat="server" Width="30px" CssClass="txtField" MaxLength="3">91</asp:TextBox>
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
                        <asp:Label ID="Label38" CssClass="FieldName" runat="server" Text="Mobile1:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile1" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ValidationGroup="btnEdit" ControlToValidate="txtMobile1"
                            Display="Dynamic" ErrorMessage="Telephone Number must be 7-11 digit" ValidationExpression="^((\+)?(\d{2}[-]))?(\d{10}){1}?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label43" CssClass="FieldName" runat="server" Text="Mobile2:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile2" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ValidationGroup="btnEdit" ControlToValidate="txtMobile2"
                            Display="Dynamic" ErrorMessage="Telephone Number must be 7-11 digit" ValidationExpression="^((\+)?(\d{2}[-]))?(\d{10}){1}?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label39" CssClass="FieldName" runat="server" Text="Email:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                            ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label40" CssClass="FieldName" runat="server" Text="Alternate Email:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtAltEmail" runat="server" CssClass="txtField"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="txtAltEmail_RegularExpressionValidator" ControlToValidate="txtAltEmail"
                            ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblOfcPhoneExt" CssClass="FieldName" runat="server" Text="Office Extension No:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfcPhoneExt" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvAdditionalInformation" runat="server">
        <asp:Panel ID="pnlAdditionalInformation" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Additional Information
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label45" CssClass="FieldName" runat="server" Text="Occupation:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlOccupation" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label46" CssClass="FieldName" runat="server" Text="Qualification:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlQualification" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label48" CssClass="FieldName" runat="server" Text="Nationality:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlNationality" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
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
                        <asp:Label ID="lblSubBroker" CssClass="FieldName" runat="server" Text="SubBroker:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtSubBroker" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblAnnualIncome" CssClass="FieldName" runat="server" Text="AnnualIncome:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtAnnualIncome" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblMinno1" CssClass="FieldName" runat="server" Text="MinNo1:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtMinNo1" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblMinno2" CssClass="FieldName" runat="server" Text="MinNo2:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtMinNo2" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblMinno3" CssClass="FieldName" runat="server" Text="MinNo3:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtMinNo3" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblESCNo" CssClass="FieldName" runat="server" Text="ESCNo:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtESCNo" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblUINNo" CssClass="FieldName" runat="server" Text="UINNo:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtUINNo" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblPOA" CssClass="FieldName" runat="server" Text="POA:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtPOA" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblGuardianName" CssClass="FieldName" runat="server" Text="GuardianName:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtGuardianName" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblGuardianRelation" CssClass="FieldName" runat="server" Text="GuardianRelation:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtGuardianRelation" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblContactGuardianPANNum" CssClass="FieldName" runat="server" Text="GuardianPANNum:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtContactGuardianPANNum" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" width="25%">
                        <asp:Label ID="Label9" runat="server" Text="Alert Preferences:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:CheckBox ID="chkmail" runat="server" CssClass="txtField" Text="Via Mail" AutoPostBack="false"
                            Enabled="true" />
                        &nbsp; &nbsp;
                        <asp:CheckBox ID="chksms" runat="server" CssClass="txtField" Text="Via SMS" Checked="true"
                            AutoPostBack="false" Enabled="true" />
                    </td>
                    <td class="leftField" width="25%">
                        <asp:Label ID="lblGuardianMinNo" CssClass="FieldName" runat="server" Text="GuardianMinNo:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtGuardianMinNo" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblGuardianDOB" runat="server" CssClass="FieldName" Text="Guardian Date Of Birth:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtGuardianDOB" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        <telerik:RadDatePicker ID="txtGuardianDOB" CssClass="txtTo" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar ID="Calendar3" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput3" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <%-- <asp:CompareValidator ID="cvMatDate" runat="server" ErrorMessage="Maturatiy Date Should be greater than Deposit date"
                        Type="Date" ControlToValidate="txtDate" ControlToCompare="txtMarriageDate" Operator="GreaterThan"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
                </tr>
                <tr>
                    <td class="leftField" width="25%">
                        <asp:Label ID="Label6" CssClass="FieldName" runat="server" Text="Marriage Date:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <telerik:RadDatePicker ID="txtMarriageDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span14" class="spnRequiredField">*</span>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Date of Birth:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:CompareValidator ID="cvDepositDate1" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtDate" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        <telerik:RadDatePicker ID="txtDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" onChange="Compare();"
                            MinDate="1900-01-01">
                            <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span13" class="spnRequiredField">*</span>
                        <asp:Label ID="lblBirthMsg" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvtxtDate" ControlToValidate="txtDate" ValidationGroup="btnEdit"
                            ErrorMessage="<br />Please enter DOB" Display="Dynamic" runat="server" CssClass="rfvPCG"
                            InitialValue="">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="leftField" width="25%">
                        <asp:Label ID="lblMotherMaidenName" CssClass="FieldName" runat="server" Text="Mother's Maiden Name:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtMotherMaidenName" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trRBIRefNo" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblRBIRefNo" CssClass="FieldName" runat="server" Text="RBI Reference Number:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtRBIRefNo" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblRBIRefDate" CssClass="FieldName" runat="server" Text="RBI Reference Date:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtRBIRefDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtRBIRefDate_CalendarExtender" runat="server" TargetControlID="txtRBIRefDate">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtRBIRefDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtRBIRefDate" WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <%--  <td class="leftField">
                        <asp:Label ID="lblTaxStatus" CssClass="FieldName" runat="server" Text="Tax Status:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <%-- <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="cmbField" OnChange="OnMaritalStatusChange(this)">
                    </asp:DropDownList>--%>
                    <%--      <asp:DropDownList ID="ddlTaxStatus" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                    </td>--%>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblOtherBankName" CssClass="FieldName" runat="server" Text="Other BankName:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtOtherBankName" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblTaxStatus" CssClass="FieldName" runat="server" Text="TaxStatus:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtTaxStatus" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblCategory" CssClass="FieldName" runat="server" Text="Category:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtCategory" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblAdr1City" CssClass="FieldName" runat="server" Text="Other City:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtAdr1City" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblAdr1State" CssClass="FieldName" runat="server" Text="Other State:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtAdr1State" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblOtherCountry" CssClass="FieldName" runat="server" Text="Other Country:"></asp:Label>
                    </td>
                    <td class="rightField" width="25%">
                        <asp:TextBox ID="txtOtherCountry" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<table style="width: 100%;">
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="SubmitCell">
            <asp:Button ID="btnEdit" runat="server" ValidationGroup="btnEdit" Text="Save" CssClass="PCGButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditCustomerIndividualProfile_btnEdit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditCustomerIndividualProfile_btnEdit');"
                OnClick="btnEdit_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnGender" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAge" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTaxSlabValue" runat="server" Visible="false" />
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnPannum" runat="server" />
<asp:HiddenField ID="hdnRequestId" runat="server" />
