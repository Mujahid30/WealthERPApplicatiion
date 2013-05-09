﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProspectList.ascx.cs"
    Inherits="WealthERP.FP.AddProspectList" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%--<script type="text/javascript">
    function DateCheck() {
        if (document.getElementById("<%=dpDOB.ClientID%>").value == "") {
            alert("Please fill DOB");
            return false;
        }
    }
</script>--%>
<%--<script type="text/javascript">
    function ValidationError(txtbox) {
        var RegExp = "^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
        if (txtbox. == false) {
            alert(txtbox);
        }
    }
</script>--%>

<script type="text/javascript">
    function ConvertToCustomerConfirmation() {
        if (confirm("Are you sure you want to convert to Customer ?"))
            return true;
        else
            return false;
    }

</script>

<script type="text/javascript">
    //***********************Deletion of Child Customer**************
    function showmessage() {
        if (confirm("Are you sure  to delete this Family Member?")) {
            document.getElementById("ctrl_AddProspectList_hdnMsgValue").value = 1;
            document.getElementById("ctrl_AddProspectList_hiddenassociation").click();
            return true;
        }
        else {
            document.getElementById("ctrl_AddProspectList_hdnMsgValue").value = 0;
            document.getElementById("ctrl_AddProspectList_hiddenassociation").click();
            return false;
        }

    }

    function showassocation() {
        alert("Customer has associations, cannot be deleted");
    }

    //*************************Deletion of parent Customer********

    function showdeletemessage() {

        var bool = window.confirm('Are you sure you want to delete this Customer?');

        if (bool) {
            document.getElementById("ctrl_AddProspectList_hdnDeletemsgValue").value = 1;
            document.getElementById("ctrl_AddProspectList_hiddenassociation1").click();

            return false;
        }
        else {
            document.getElementById("ctrl_AddProspectList_hdnDeletemsgValue").value = 0;
            document.getElementById("ctrl_AddProspectList_hiddenassociation1").click();
            return true;
        }

    }

    
    
</script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All"
    Skin="Telerik" EnableEmbeddedSkins="false" />
<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Add Prospect"></asp:Label>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record created Successfully
            </div>
        </td>
    </tr>
</table>
<hr />
<telerik:RadToolBar ID="aplToolBar" runat="server" OnButtonClick="aplToolBar_ButtonClick"
    Skin="Telerik" EnableEmbeddedSkins="false" EnableShadows="true" EnableRoundedCorners="true"
    Width="100%" Visible="false" CausesValidation="false">
    <Items>
        <telerik:RadToolBarButton runat="server" Text="Back" Value="Back" ImageUrl="/Images/Telerik/BackButton.gif"
            ImagePosition="Left" ToolTip="Back">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<div style="float: left; width: 100%;" id="Div1" runat="server">
    <asp:Label ID="Label1" runat="server" CssClass="HeaderText" Text="Self"></asp:Label>
    <hr />
    <table width="100%">
        <%--<tr>
            <td colspan="6">
            <asp:ValidationSummary runat="server" ID="vsErrorSummary" EnableClientScript="true" Tooltip="Error Summary" ShowSummary="true" />
            </td>
        </tr>--%>
        <tr id="trBranchRM" runat="server">
            <td align="right">
                <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch : "></asp:Label>
            </td>
            <td align="left">
                <telerik:RadComboBox ID="ddlPickBranch" runat="server" ExpandAnimation-Type="Linear"
                    ShowToggleImage="True" EmptyMessage="Pick a Branch here" Skin="Telerik" 
                    EnableEmbeddedSkins="false" 
                    onselectedindexchanged="ddlPickBranch_SelectedIndexChanged">
                    <ExpandAnimation Type="InExpo"></ExpandAnimation>
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlPickBranch"
                    ErrorMessage="Please pick a branch" CssClass="validator" Style="display: none" />
            </td>
            <td align="right">
                <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM : "></asp:Label>
            </td>
            <td align="left">
                <telerik:RadComboBox ID="rcbRM" runat="server" ExpandAnimation-Type="Linear" ShowToggleImage="True"
                    EmptyMessage="Pick a RM" Skin="Telerik" EnableEmbeddedSkins="false">
                    <ExpandAnimation Type="InExpo"></ExpandAnimation>
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvRM" ControlToValidate="rcbRM" ErrorMessage="Please pick a RM"
                    CssClass="validator" Style="display: none" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblFirstName" runat="server" CssClass="FieldName" Text="First Name : "></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtFirstName" runat="server" Text="" />
                <span id="Span1" class="spnRequiredField">*</span>
            </td>
            <td align="right">
                <asp:Label ID="lblMiddleName" runat="server" CssClass="FieldName" Text="Middle Name : "></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtMiddleName" runat="server" Text="" />
            </td>
            <td align="right">
                <asp:Label ID="lblLastName" runat="server" Text="Last Name : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtLastName" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <telerik:RadDatePicker ID="dpDOB" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
            </td>
            <td align="right">
                <asp:Label ID="lblEmailId" runat="server" Text="Email Id : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtEmail" runat="server" Text="" />
                <!--<span id="Span2" class="spnRequiredField">*</span> -->
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                    ErrorMessage="<br>Please enter a valid Email ID" Display="Dynamic" runat="server"
                    EnableClientScript="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Font-Size="11px"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblPanNumber" runat="server" Text="PAN Number : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtPanNumber" runat="server" Text="" MaxLength="10" />
                <span id="Span6" class="spnRequiredField">*</span>
            </td>
            <td align="right">
                <asp:Label ID="lblAddress1" runat="server" Text="Address 1 : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtAddress1" runat="server" Text="" />
            </td>
            <td align="right">
                <asp:Label ID="lblAddress2" runat="server" Text="Address 2 : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtAddress2" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblCity" runat="server" Text="City : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtCity" runat="server" Text="" />
            </td>
            <td align="right">
                <asp:Label ID="lblState" runat="server" Text="State : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtState" runat="server" Text="" />
            </td>
            <td align="right">
                <asp:Label ID="lblCountry" runat="server" Text="Country : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtCountry" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblPinCode" runat="server" Text="PinCode : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtPinCode" runat="server" Text="" MaxLength="6" /><br />
                <asp:RegularExpressionValidator ID="RegExpValforPincode" ControlToValidate="txtPinCode"
                    ValidationExpression="\d+" Display="Static" EnableClientScript="true" ErrorMessage="Please enter numbers only"
                    Font-Size="11px" runat="server" />
            </td>
            <td align="right">
                <asp:Label ID="lblMobileNo" runat="server" Text="MobileNo : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtMobileNo" runat="server" Text="" MaxLength="10" /><br />
                <asp:RegularExpressionValidator ID="RegExpValforMobNo" ControlToValidate="txtMobileNo"
                    ValidationExpression="\d+" Display="Static" EnableClientScript="true" ErrorMessage="Please enter numbers only"
                    Font-Size="11px" runat="server" />
            </td>
            <td align="right">
                <asp:Label ID="lblProspectAddDate" runat="server" Text="Prospect Add Date : " CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <telerik:RadDatePicker ID="dpProspectAddDate" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right" class="leftField">
                <asp:Label ID="lblGender" runat="server" CssClass="FieldName" Text="Gender:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:RadioButton ID="rbtnMale" runat="server" CssClass="txtField" Text="Male" GroupName="rbtnGender" />
                <asp:RadioButton ID="rbtnFemale" runat="server" CssClass="txtField" Text="Female"
                    GroupName="rbtnGender" />
            </td>
            <td class="leftField">
                <asp:Label ID="lblSlabForOther" runat="server" CssClass="FieldName" Text="Tax slab applicable(%):"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtSlab" runat="server" CssClass="txtField"></asp:TextBox>
                <asp:CompareValidator ID="cmpareSlabForOther" ControlToValidate="txtSlab" runat="server"
                    Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Tax slab."
                    Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
            </td>
            <td>
                <asp:Button ID="btnGetSlab" runat="server" Text="Get the slab" CssClass="PCGMediumButton"
                    OnClick="btnGetSlab_Click" />
            </td>
        </tr>
    </table>
</div>
<telerik:RadInputManager ID="RadInputManager1" runat="server" Skin="Telerik" EnableEmbeddedSkins="false">
    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" Validation-IsRequired="true"
        ErrorMessage="Is Required">
        <TargetControls>
            <telerik:TargetInput ControlID="txtFirstName" />
            <telerik:TargetInput ControlID="txtChildFirstName" />
            <%-- <telerik:TargetInput ControlID="txtGridEmailId" />--%>
            <telerik:TargetInput ControlID="txtPanNumber" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:TextBoxSetting>
    <telerik:DateInputSetting BehaviorID="DateInputBehavior1" Validation-IsRequired="true"
        DateFormat="MM/dd/yyyy" ErrorMessage="Is Required">
        <TargetControls>
            <telerik:TargetInput ControlID="dpDOB" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:DateInputSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
        <TargetControls>
            <%--<telerik:TargetInput ControlID="txtGridEmailId" />--%>
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:RegExpTextBoxSetting>
</telerik:RadInputManager>
<telerik:RadAjaxLoadingPanel ID="FamilyMemberDetailsLoading" runat="server" Skin="Telerik"
    EnableEmbeddedSkins="false">
</telerik:RadAjaxLoadingPanel>
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" runat="server" id="tblChildCustomer">
        <tr>
            <td>
                <div style="float: left; width: 100%;" id="Div2" runat="server">
                    <asp:Label ID="Label2" runat="server" CssClass="HeaderText" Text="Family Member Details"></asp:Label>
                    <hr />
                    <telerik:RadAjaxPanel ID="ChildCustomerGridPanel" runat="server" Width="100%" HorizontalAlign="Center"
                        LoadingPanelID="FamilyMemberDetailsLoading" EnablePageHeadUpdate="False">
                        <telerik:RadGrid ID="RadGrid1" runat="server" Width="96%" GridLines="None" AutoGenerateColumns="False"
                            PageSize="13" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid1_NeedDataSource"
                            ShowStatusBar="True" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                            OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                            <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" />
                            <MasterTableView DataKeyNames="C_CustomerId" AllowMultiColumnSorting="True" Width="100%"
                                CommandItemDisplay="Top" AutoGenerateColumns="false" EditMode="InPlace">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                                        CancelText="Cancel" ButtonType="ImageButton" CancelImageUrl="../Images/Telerik/Cancel.gif"
                                        InsertImageUrl="../Images/Telerik/Update.gif" UpdateImageUrl="../Images/Telerik/Update.gif"
                                        EditImageUrl="../Images/Telerik/Edit.gif">
                                        <HeaderStyle Width="85px"></HeaderStyle>
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridDropDownColumn UniqueName="CustomerRelationship" HeaderText="Relationship"
                                        DataField="CustomerRelationship" DataSourceID="SqlDataSourceCustomerRelation"
                                        HeaderStyle-HorizontalAlign="Center" ColumnEditorID="GridDropDownColumnEditor1"
                                        ListTextField="XR_Relationship" ListValueField="XR_RelationshipCode" DropDownControlType="RadComboBox"
                                        ReadOnly="false">
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridTemplateColumn HeaderText="First Name" SortExpression="FirstName" UniqueName="FirstName"
                                        EditFormColumnIndex="1" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle Width="80px" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblChildFirstName" Text='<%# Eval("FirstName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtChildFirstName" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="MiddleName" HeaderText="Middle Name" DataField="MiddleName"
                                        HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn UniqueName="LastName" HeaderText="Last Name" DataField="LastName"
                                        HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn UniqueName="DOB" PickerType="DatePicker" HeaderText="Date of Birth"
                                        HeaderStyle-HorizontalAlign="Center" DataField="DOB" FooterText="DateTimeColumn footer"
                                        DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd MMMM, yyyy" MinDate="1900-01-01">
                                        <ItemStyle Width="120px" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridTemplateColumn HeaderText="Email-Id" SortExpression="Email-Id" UniqueName="EmailId"
                                        HeaderStyle-HorizontalAlign="Center" EditFormColumnIndex="1">
                                        <HeaderStyle Width="80px" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGridEmailId" Text='<%# Eval("EmailId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtGridEmailId" onchange="ValidationError(this.value);"
                                                Text='<%# Bind("EmailId") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RExpForValidEmail" Display="Dynamic" runat="server"
                                                Text="Invalid Email" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                                ControlToValidate="txtGridEmailId"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="PAN Number" SortExpression="PanNum" UniqueName="PanNum"
                                        EditFormColumnIndex="1" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle Width="80px" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPanNo" Text='<%# Bind("PanNum") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtChildPanNo" MaxLength="10" Text='<%# Bind("PanNum") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <%--<telerik:GridBoundColumn UniqueName="PanNum" HeaderText="PAN Number" DataField="PanNum"
                                    HeaderStyle-HorizontalAlign="Center" />--%>
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ImageUrl="../Images/Telerik/Delete.gif" ButtonType="ImageButton" />
                                </Columns>
                                <EditFormSettings CaptionFormatString="Edit details for employee with ID {0}" CaptionDataField="FirstName">
                                    <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                                    <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                                    <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                                    <EditColumn ButtonType="ImageButton" />
                                </EditFormSettings>
                            </MasterTableView>
                            <ClientSettings>
                            </ClientSettings>
                        </telerik:RadGrid>
                        <asp:SqlDataSource ID="SqlDataSourceCustomerRelation" runat="server" SelectCommand="SP_GetCustomerRelation"
                            SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:wealtherp %>">
                        </asp:SqlDataSource>
                    </telerik:RadAjaxPanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="100%">
    <tr>
        <td align="center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            &nbsp;<asp:Button ID="btnSubmitAddDetails" runat="server" Text="Add Finance Details"
                OnClick="btnSubmitAddDetails_Click" />
            &nbsp;
            <asp:Button ID="btnConvertToCustomer" runat="server" Text="Convert to Customer" OnClientClick="return ConvertToCustomerConfirmation()"
                OnClick="btnConvertToCustomer_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnIsActive" runat="server" />
<asp:HiddenField ID="hdnIsProspect" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdnassociationcount" runat="server" />
<asp:HiddenField ID="hdnDeletemsgValue" runat="server" />
<asp:HiddenField ID="hdnassociation" runat="server" Visible="true" />
<asp:HiddenField ID="hdnassociationdeletecount" runat="server" />
<asp:HiddenField ID="hdnGender" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAge" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTaxSlabValue" runat="server" Visible="false" />
<div style="visibility: hidden">
    <asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
        BorderStyle="None" BackColor="Transparent" ForeColor="Transparent" />
    <asp:Button ID="hiddenassociation1" runat="server" OnClick="hiddenassociation1_Click"
        BorderStyle="None" BackColor="Transparent" />
</div>
