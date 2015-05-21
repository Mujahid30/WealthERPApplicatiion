<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDematAccountDetails.ascx.cs"
    Inherits="WealthERP.Customer.AddDematAccountDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script runat="server">
  
    protected void rbtnNo_Load(object sender, EventArgs e)
    {
        if (rbtnNo.Checked)
        {
            ddlModeOfHolding.Enabled = false;
        }
    }
   
</script>

<table width=" 100%">
    <tr>
        <td colspan="4">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr id="lblLifeInsurance" runat="server" style="width: 100%">
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lbtnBackButton" runat="server" OnClick="lbtnBackButton_Click"
                                Visible="False" CssClass="LinkButtons">Edit</asp:LinkButton>
                            &nbsp; &nbsp;
                            <asp:LinkButton ID="lbtnBack2Button" runat="server" OnClick="lbtnBack2Button_Click"
                                Visible="true" CssClass="LinkButtons">Back</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr>
        <td class="leftField" style="width: 150px;">
            <asp:Label ID="lblDpName" runat="server" Text="DP Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtDpName" runat="server" CssClass="txtField" Width="250px"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDpName"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please enter DP Name"
                InitialValue="" ValidationGroup="btnsubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 150px;">
            &nbsp;<asp:Label ID="lblDepositoryName" runat="server" Text="Depository Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlDepositoryName" runat="server" CssClass="cmbField"  AutoPostBack="true"
                OnSelectedIndexChanged="ddlDepositoryName_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepositoryName"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select Depository Name"
                InitialValue="Select" ValidationGroup="btnsubmit" >
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            &nbsp;<asp:Label ID="lblDPId" runat="server" Text="DP Id:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField" MaxLength="8"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="lblDpClientId" runat="server" Text="Beneficiary Acct No:" CssClass="FieldName" EnableViewState="true"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtDpClientId" runat="server" CssClass="txtField" MaxLength="16" ></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
          <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtDpClientId"
                ValidationGroup="btnsubmit" ErrorMessage="Special Character are not allowed!"
                CssClass="cvPCG" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9]+(?:--?[a-zA-Z0-9]+)*$"/>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDpClientId"
                ErrorMessage="Beneficiary Acct No:" CssClass="cvPCG" ValidationGroup="btnsubmit"
                InitialValue="" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblIsHeldJointly" runat="server" Text="Is Held Jointly:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:RadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="IsHeldJointly"
                CssClass="txtField" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged" />
            <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="IsHeldJointly" CssClass="txtField"
                AutoPostBack="True" OnCheckedChanged="rbtnNo_CheckChanged" OnLoad="rbtnNo_Load"
                Checked="true" />
        </td>
        <td class="leftField">
            &nbsp;<asp:Label ID="lblAccountOpeningDate" runat="server" Text="Account Opening Date:"
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <!-- calAccountOpeningDate -->
            <telerik:RadDatePicker ID="txtAccountOpeningDate" CssClass="txtTo" runat="server"
                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                ShowAnimation-Type="Fade" MinDate="1900-01-01">
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
        <td class="leftField">
            &nbsp;<asp:Label ID="lblModeOfHolding" runat="server" Text="Mode Of Holding:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlModeOfHolding" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModeOfHolding_SelectedIndexChanged"
                CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="leftField">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;<asp:Label ID="lblisactive" runat="server" Text="Is Active" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="chk_isactive" runat="server" CssClass="cmbFielde" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
        </td>
        <td class="rightField">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                CssClass="PCGButton"  ValidationGroup="btnsubmit"
               />
        </td>
    </tr>
</table>
<table width="100%" class="TableBackground">
    <tr id="trAssociatePanel" runat="server" visible="false">
        <td>
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Nominee JointHolder Details
            </div>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadGrid ID="gvAssociate" runat="server" CssClass="RadGrid" GridLines="Both"
                Visible="false" Width="90%" AllowPaging="True" PageSize="20" AllowSorting="True"
                AutoGenerateColumns="false" ShowStatusBar="true" AllowAutomaticDeletes="True"
                Skin="Telerik" OnItemDataBound="gvFamilyAssociate_ItemDataBound" OnItemCommand="gvFamilyAssociate_ItemCommand"
                OnNeedDataSource="gvFamilyAssociate_NeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                    FileName="Family Associates" Excel-Format="ExcelML">
                </ExportSettings>
                <%--<MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None">--%>
                <MasterTableView DataKeyNames="CDAA_Id,CEDA_DematAccountId,CDAA_Name,CDAA_PanNum,Sex,CDAA_DOB,RelationshipName,AssociateType,CDAA_AssociateTypeNo,CDAA_IsKYC,SexShortName,CDAA_AssociateType,XR_RelationshipCode"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" EditMode="EditForms"
                    CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false" CommandItemSettings-AddNewRecordText="Add Family Associates">
                    <Columns>
                        <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="50px" EditText="View/Edit"
                            UniqueName="editColumn" CancelText="Cancel" UpdateText="Update">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="CDAA_Name" HeaderText="Member name" UniqueName="AssociateName"
                            SortExpression="AssociateName">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AssociateType" HeaderText="Associate Type" UniqueName="AssociateType"
                            SortExpression="AssociateType">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CDAA_PanNum" HeaderText="PAN Number" UniqueName="CDAA_PanNum"
                            SortExpression="CDAA_PanNum">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CDAA_IsKYC" HeaderText="IsKYC" UniqueName="CDAA_IsKYC"
                            SortExpression="CDAA_IsKYC">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CDAA_DOB" HeaderText="Date Of Birth" UniqueName="CDAA_DOB"
                            SortExpression="CDAA_DOB" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RelationshipName" HeaderText="Relationship" AllowFiltering="false"
                            UniqueName="RelationshipName" SortExpression="RelationshipName">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Association?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings FormTableStyle-Height="10px" EditFormType="Template" FormTableStyle-Width="1000px">
                        <FormTemplate>
                            <table width="100%" style="background-color: White">
                                <tr id="trNewCustHeader" runat="server">
                                    <td colspan="4">
                                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                                            Add Customer Associate
                                        </div>
                                    </td>
                                </tr>
                                <tr id="trNewCustomer" runat="server">
                                    <td colspan="4">
                                        <table width="50%">
                                            <tr>
                                                <td class="leftField" align="right">
                                                    <asp:Label ID="lblAssociate" runat="server" CssClass="FieldName" Text="Associate Type :"></asp:Label>
                                                </td>
                                                <td class="rightField">
                                                    <asp:DropDownList ID="ddlAssociate" runat="server" CssClass="cmbField">
                                                        <asp:ListItem Value="JH1">JointHolder-1</asp:ListItem>
                                                        <asp:ListItem Value="JH2">JointHolder-2</asp:ListItem>
                                                        <asp:ListItem Value="N1">Nominee-1</asp:ListItem>
                                                        <asp:ListItem Value="N2">Nominee-2</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <span id="Span3" class="spnRequiredField">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftField" align="right">
                                                    <asp:Label ID="lblNewName" runat="server" CssClass="FieldName" Text="Member Name:"></asp:Label>
                                                </td>
                                                <td class="rightField">
                                                    <asp:TextBox ID="txtNewName" runat="server" Text='<%# Bind("CDAA_Name") %>' CssClass="txtField"></asp:TextBox>
                                                    <span id="spnTransType" class="spnRequiredField">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNewName"
                                                        ErrorMessage="<br />Please Enter Associate Name" Display="Dynamic" runat="server"
                                                        CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkKYC" runat="server" Text="IS KYC" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftField" align="right">
                                                    <asp:Label ID="lblNewPan" runat="server" CssClass="FieldName" Text="PAN:"></asp:Label>
                                                </td>
                                                <td class="rightField">
                                                    <asp:TextBox ID="txtNewPan" runat="server" Text='<%# Bind("CDAA_PanNum") %>' CssClass="txtField"></asp:TextBox>
                                                    <span id="Span6" class="spnRequiredField">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNewPan"
                                                        ErrorMessage="<br />Please Enter PAN Number" Visible="false" runat="server" CssClass="rfvPCG"
                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <%--<asp:CheckBox ID="isRealInvestormem" runat="server" Text="ISRealInvestor" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftField" align="right">
                                                    <asp:Label ID="lblgender" runat="server" CssClass="FieldName" Text="Gender:"></asp:Label>
                                                </td>
                                                <td class="rightField">
                                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="cmbField">
                                                        <asp:ListItem Value="S">Select</asp:ListItem>
                                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                                        <asp:ListItem Value="F">FeMale</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="leftField" align="right">
                                                    <asp:Label ID="lblDOB" runat="server" CssClass="FieldName" Text="Date Of Birth:"></asp:Label>
                                                </td>
                                                <td class="rightField">
                                                    <telerik:RadDatePicker ID="txtDOB" CssClass="txtTo" runat="server" Culture="English (United States)"
                                                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                                        </Calendar>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtDOB"
                                                        ErrorMessage="<br />Please Enter Date of Birth" Display="Dynamic" runat="server"
                                                        InitialValue="Select" CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlNewRelationship"
                                                        ErrorMessage="<br />Please a relationship" Display="Dynamic" runat="server" InitialValue="Select"
                                                        CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Submit" : "Update" %>'
                                                        runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                        ValidationGroup="Submit"></asp:Button>
                                                    <%--   <asp:CustomValidator ID="OrFieldValidator" 
                        runat="server" 
                        Text="Enter bcjsdh" 
                        Display="Dynamic"
                        OnServerValidate="OrFieldValidator_ServerValidate" />--%>
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
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnSelectedBranches" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnSelectedModeOfHolding" runat="server" />
        </td>
    </tr>
</table>
