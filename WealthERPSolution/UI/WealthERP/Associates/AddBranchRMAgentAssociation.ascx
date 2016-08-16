<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBranchRMAgentAssociation.ascx.cs"
    Inherits="WealthERP.Associates.AddBranchRMAgentAssociation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Add SubBroker (Child Code)
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<table width="100%">
    <tr>
        <td colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Step-3
            </div>
        </td>
    </tr>
</table>--%>
<table width="60%">
    <tr>
        <td align="leftField">
            &nbsp;
        </td>
        <%--        <td class="rightField">
            <asp:RadioButton ID="rbtBM" Class="cmbField" runat="server" GroupName="BMRMAgent"   AutoPostBack="true"
                Checked="True" Text="BM" oncheckedchanged="rbtBM_CheckedChanged"  /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rbtRM" Class="cmbField" runat="server" GroupName="BMRMAgent"  AutoPostBack="true"
                Text="RM" oncheckedchanged="rbtRM_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rbtnAgent" Class="cmbField" runat="server" GroupName="BMRMAgent"  AutoPostBack="true"
                Text="Agent" oncheckedchanged="rbtnAgent_CheckedChanged" />
        </td>--%>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" Width="150px" CssClass="FieldName" Text="Generate code for:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Associates" Value="Associates"></asp:ListItem>
                <asp:ListItem Text="Branch" Value="BM"></asp:ListItem>
                <asp:ListItem Text="Employee" Value="RM"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CVTrxType" runat="server" ControlToValidate="ddlUserType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an user type"
                Operator="NotEqual" ValidationGroup="Submit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lblSelectType" runat="server" CssClass="FieldName" Text="Select:"></asp:Label>
        </td>
        <td class="leftField">
            <asp:DropDownList ID="ddlSelectType" runat="server" Width="350px" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSelectType_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlSelectType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an item from the list"
                Operator="NotEqual" ValidationGroup="Submit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
   
    <%--<tr>
        <td class="leftField">
            <asp:Label ID="lblNoOfCode" CssClass="FieldName" runat="server" Text="No. of code to be generated:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtNoOfCode" runat="server" CssClass="txtField" Text="1"></asp:TextBox>
        </td>
    </tr>--%>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAgentCode" CssClass="FieldName" runat="server" Text="SubBroker Code:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAgentCode" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAgentCode"
                ValidationGroup="Submit" ErrorMessage="Please enter an Code" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="SubBroker Code already exists"></asp:Label>
        </td>
    </tr>
    </table>
    <%--<tr>
        <td class="leftField">
            <asp:Label ID="lblNoOfCodes" CssClass="FieldName" runat="server" Text="AddMultiple Codes:"
                Visible="false"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNoOfCodes" runat="server" CssClass="txtField" Visible="false"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnAddCode" runat="server" Text="AddCode" CssClass="PCGButton" Visible="false"
                ValidationGroup="Submit" OnClick="btnAddCode_Click" />
        </td>
    </tr>--%>
    <table>
    <tr>
        <td>
        </td>
        <td>
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Visible="false" EnableTheming="True"
                Skin="Telerik" EnableEmbeddedSkins="False" Width="100%" MultiPageID="SubbroTabPages"
                SelectedIndex="0" EnableViewState="true">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Child Codes" Value="rpvChildCode"
                        TabIndex="0">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Add codes in bulk" Value="rpvBulkChildCode"
                        TabIndex="1">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="SubbroTabPages" EnableViewState="true" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="rpvChildCode" runat="server">
                    <table id="tableGrid" runat="server" width="100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="gvChildCode" runat="server" Skin="Telerik" CssClass="RadGrid"
                                    GridLines="None" AllowPaging="True" Width="600px" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                                    OnItemCommand="gvChildCode_ItemCommand" OnItemDataBound="gvChildCode_ItemDataBound"
                                    OnNeedDataSource="gvChildCode_OnNeedDataSource" Visible="false" AllowAutomaticUpdates="false"
                                    HorizontalAlign="NotSet" DataKeyNames="AAC_AdviserAgentId,AAC_AdviserAgentIdParent,AAC_AgentCode,U_UserId">
                                    <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="AAC_AdviserAgentId,AAC_AdviserAgentIdParent,AAC_AgentCode,U_UserId,RoleIds"
                                        CommandItemSettings-ShowRefreshButton="false" CommandItemSettings-AddNewRecordText="Add child codes">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="AAC_AgentCode" HeaderText="Child Code" DataField="AAC_AgentCode">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="EmailId" HeaderText="EmailId" DataField="EmailId">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ChildName" HeaderText="Name" DataField="ChildName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                                UpdateText="Edit">
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete the code?"
                                                ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                Text="Delete">
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings InsertCaption="Add" FormTableStyle-HorizontalAlign="Center" CaptionFormatString="Edit"
                                            FormCaptionStyle-CssClass="TableBackground" PopUpSettings-Modal="true" PopUpSettings-ZIndex="20"
                                            EditFormType="Template" FormCaptionStyle-Width="100%" PopUpSettings-Height="250px"
                                            PopUpSettings-Width="700px">
                                            <FormTemplate>
                                                <table>
                                                    <tr id="trRiskClassTxt" runat="server">
                                                        <td class="leftField">
                                                            <asp:Label ID="lblChildCode"  runat="server" Text="Child Code :" CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td class="rightField" onkeypress="return keyPress(this, event)">
                                                            <asp:TextBox ID="txtChildCode" Width="230px" CssClass="txtField" Text='<%# Bind( "AAC_AgentCode") %>'
                                                                runat="server">
                                                            </asp:TextBox><span id="Span6" class="spnRequiredField">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvChildcode" ControlToValidate="txtChildCode" ErrorMessage="<br />Please Enter Child Code "
                                                                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="Button1">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField">
                                                            <asp:Label ID="lblChildName" runat="server" Text="Name:" CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td class="rightField" onkeypress="return keyPress(this, event)">
                                                            <asp:TextBox ID="txtChildName"  Width="230px" CssClass="txtField" Text='<%# Bind( "ChildName") %>'
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField">
                                                            <asp:Label ID="lblChildEmailId" runat="server" Text="EmailId:" CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td class="rightField" onkeypress="return keyPress(this, event)">
                                                            <asp:TextBox ID="txtChildEmailId"  Width="230px" CssClass="txtField" Text='<%# Bind( "EmailId") %>'
                                                                runat="server">
                                                            </asp:TextBox><span id="Span3" class="spnRequiredField">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtChildEmailId"
                                                                ErrorMessage="<br />Please enter a valid EmailId" Display="Dynamic" runat="server"
                                                                ValidationGroup="Button1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                CssClass="revPCG"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtChildEmailId"
                                                                ErrorMessage="<br />Please Enter EmailId " Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                                ValidationGroup="Button1">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label3" runat="server" Text="Privilege Role:" CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td class="rightData" colspan="5">
                                                            <telerik:RadListBox ID="chkbldepart"  Width="230px" runat="server" CheckBoxes="true" AutoPostBack="true"
                                                                CssClass="horizontalListbox">
                                                            </telerik:RadListBox>
                                                            <span id="Span12" class="spnRequiredField">*</span>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ErrorMessage="Select a role!"
                                                            ControlToValidate="chkbldepart" ValidationGroup="Button1" Display="Dynamic" 
                                                            CssClass="rfvPCG" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2">
                                                            <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                ValidationGroup="Button1" CssClass="PCGButton" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                            </asp:Button>&nbsp;
                                                            <asp:Button ID="Button2" CssClass="PCGButton" Text="Cancel" runat="server" CausesValidation="False"
                                                                CommandName="Cancel"></asp:Button>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvBulkChildCode" runat="server">
                    <table>
                       
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="Label2" runat="server" Text="Enter Start Code :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartingchild" runat="server"></asp:TextBox>
                                <span id="Span5" class="spnRequiredField">*</span>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtStartingchild"
                                    ErrorMessage="<br />Please Enter Starting Child " Display="Dynamic" runat="server"
                                    CssClass="rfvPCG" ValidationGroup="VGBtn">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="Label4" runat="server" Text="No. of code required :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtnofochildcode" runat="server"></asp:TextBox>
                                <span id="Span7" class="spnRequiredField">*</span>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtnofochildcode"
                                    ErrorMessage="<br />Please Enter No Of ChildCode " Display="Dynamic" runat="server"
                                    CssClass="rfvPCG" ValidationGroup="VGBtn">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                &nbsp;
                            </td>
                            <td class="rightField">
                                <asp:Button ID="Button3" runat="server" ValidationGroup="VGBtn" Text="Check Availability"
                                    OnClick="Button3_Click" CssClass="PCGButton" />
                            </td>
                            <td class="rightField">
                                <asp:Button ID="Button4" runat="server" ValidationGroup="VGBtn" Text="Generate"
                                    OnClick="Button4_Click" CssClass="PCGButton" />
                            </td>
                        </tr>
                        <tr>
                       
                            <asp:Label ID="lblMessage" Text="" Height="40%"  runat="server" CssClass="FieldName" ></asp:Label>
                       
                        </tr>
                         
                    </table>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            &nbsp;
        </td>
        <td class="rightField">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" ValidationGroup="Submit"
                OnClick="btnSubmit_Click" />
        </td>
    </tr>
</table>
