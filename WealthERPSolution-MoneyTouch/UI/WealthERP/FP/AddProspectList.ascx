﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProspectList.ascx.cs"
    Inherits="WealthERP.FP.AddProspectList" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All"
    Skin="Outlook" />
<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Add Prospect"></asp:Label>
<hr />
<telerik:RadToolBar ID="aplToolBar" runat="server" OnButtonClick="aplToolBar_ButtonClick"
    Skin="Outlook" EnableShadows="true" EnableRoundedCorners="true" Width="100%"
    Visible="false">
    <Items>
        <telerik:RadToolBarButton runat="server" Text="Back" Value="Back" ImageUrl="/Images/Telerik/BackButton.gif"
            ImagePosition="Left" ToolTip="Back">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton runat="server" Text="Edit" Value="Edit" ImageUrl="~/Images/Telerik/EditButton.gif"
            ImagePosition="Left" ToolTip="Edit">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record created Successfully
            </div>
        </td>
    </tr>
</table>
<div style="float: left; width: 100%;" id="Div1" runat="server">
<asp:Label ID="Label1" runat="server" CssClass="HeaderText" Text="Self"></asp:Label>
<hr />
                <table width="100%">
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
                            <asp:TextBox ID="txtLastName" runat="server" Text=""  />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth : " CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadDatePicker ID="dpDOB" runat="server" Culture="English (United States)"
                                Skin="Outlook" ShowAnimation-Type="Fade"  
                                MinDate="1900-01-01">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                    Skin="Outlook">
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblEmailId" runat="server" Text="Email Id : " CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmail" runat="server" Text="" />
                            <span id="Span2" class="spnRequiredField">*</span>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPickBranch" runat="server" Text="Branch Name : " CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadComboBox ID="ddlPickBranch" runat="server" ExpandAnimation-Type="Linear"
                                ShowToggleImage="True" EmptyMessage="Pick a Branch here" Skin="Outlook">
                                <ExpandAnimation Type="InExpo"></ExpandAnimation>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlPickBranch"
                                ErrorMessage="Please pick a branch" CssClass="validator" />
                        </td>
                    </tr>
                </table>
           
</div>
<telerik:RadInputManager ID="RadInputManager1" runat="server" Skin="Outlook">
    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" Validation-IsRequired="true"
        ErrorMessage="Is Required">
        <TargetControls>
            <telerik:TargetInput ControlID="txtFirstName" />
            <telerik:TargetInput ControlID="txtEmail" />
            <telerik:TargetInput ControlID="txtChildFirstName" />
            <telerik:TargetInput ControlID="txtGridEmailId" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:TextBoxSetting>
    <telerik:DateInputSetting BehaviorID="DateInputBehavior1" Validation-IsRequired="true"
        DateFormat="MM/dd/yyyy">
        <TargetControls>
            <telerik:TargetInput ControlID="dpDOB" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:DateInputSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
        <TargetControls>
            <telerik:TargetInput ControlID="txtEmail" />
            <telerik:TargetInput ControlID="txtGridEmailId" />
        </TargetControls>
    </telerik:RegExpTextBoxSetting> 
</telerik:RadInputManager>
<telerik:RadAjaxLoadingPanel ID="FamilyMemberDetailsLoading" runat="server" Skin="Outlook">
</telerik:RadAjaxLoadingPanel>
<table width="100%">
    <tr>
        <td>
            <div style="float: left; width: 100%;" id="Div2" runat="server">
               <asp:Label ID="Label2" runat="server" CssClass="HeaderText" Text="Family Member Details"></asp:Label>
<hr />
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <div id="msgNochildCustomer" runat="server" class="failure-msg" align="center" visible="false">
                                            There is no Child Customers for this Customer
                                            <br />
                                            (or)<br />
                                            This Customer itself might be a child Customer</div>
                                    </td>
                                </tr>
                            </table>
                            <telerik:RadAjaxPanel ID="ChildCustomerGridPanel" runat="server" Width="100%" HorizontalAlign="Center"
                                LoadingPanelID="FamilyMemberDetailsLoading" EnablePageHeadUpdate="False" >
                                <telerik:RadGrid ID="RadGrid1" runat="server" Width="96%" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="13" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    ShowStatusBar="True" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                                    Skin="Outlook" OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound">
                                    <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" />
                                    <MasterTableView DataKeyNames="C_CustomerId" AllowMultiColumnSorting="True" Width="100%"
                                        CommandItemDisplay="Top" AutoGenerateColumns="false" EditMode="InPlace">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                                                CancelText="Cancel" ButtonType="ImageButton">
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
                                                DataFormatString="{0:MM/dd/yyyy}" EditDataFormatString="MMMM dd, yyyy" >
                                                <ItemStyle Width="120px" />
                                            </telerik:GridDateTimeColumn>                                            
                                            <telerik:GridTemplateColumn HeaderText="Email-Id" SortExpression="Email-Id" UniqueName="EmailId"
                                                HeaderStyle-HorizontalAlign="Center" EditFormColumnIndex="1">
                                                <HeaderStyle Width="80px" />
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGridEmailId" Text='<%# Eval("EmailId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtGridEmailId" Text='<%# Bind("EmailId") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" />
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
                                <asp:SqlDataSource ID="SqlDataSourceCustomerRelation" runat="server" ConnectionString="<%$ ConnectionStrings:wealtherp %>"
                                    SelectCommand="SP_GetCustomerRelation" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            </telerik:RadAjaxPanel>
                       
            </div>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            &nbsp;<asp:Button ID="btnSubmitAddDetails" runat="server" Text="Add Finance Details" OnClick="btnSubmitAddDetails_Click" />
        </td>
    </tr>
</table>
