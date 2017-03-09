﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageRepository.ascx.cs"
    Inherits="WealthERP.Admin.ManageRepository" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%"><tr><td align="right"><asp:LinkButton ID="lnkButton" runat="server" Text="Back" CssClass="LinkButtons" OnClick="lnkButton_Click"></asp:LinkButton></td></tr></table>
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="rmpManageRepository" SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Manage Repository" onclick="HideStatusMsg()"
            Value="ManageRepository" TabIndex="0" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="View Repository" onclick="HideStatusMsg()" Value="ViewRepository"
            TabIndex="1" Selected="True">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="rmpManageRepository" EnableViewState="true" runat="server"
    SelectedIndex="0">
    <telerik:RadPageView ID="RadPageView2" runat="server">
    <table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">Manage Repository</td>
        <td  align="right" style="padding-bottom:2px;">
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>
        <%--<table width="100%" class="TableBackground">
            <tr>
                <td class="HeaderCell">
                    <asp:Label ID="lblMRTitle" runat="server" CssClass="HeaderTextBig" Text="Manage Repository"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>--%>
        <table class="TableBackground" width="100%">
            <tr id="trContentMR">
                <td>
                    <table width="100%">
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblCategory" Text="Category:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlRCategory" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                                <span id="Span1" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlRCategory_CompareValidator" runat="server" ControlToValidate="ddlRCategory"
                                    ErrorMessage="Please select a Repository Category" Operator="NotEqual" ValueToCompare="Select a Category"
                                    CssClass="cvPCG" ValidationGroup="btnAdd" Display="Dynamic">
                                </asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblHeadingText" Text="Heading Text:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox CssClass="txtField" ID="txtHeadingText" runat="server" Width="300px"
                                    MaxLength="40"></asp:TextBox>
                                <span id="span2" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvHeadingText" runat="server"
                                    ControlToValidate="txtHeadingText" ErrorMessage="<br/>Enter Heading Text" ValidationGroup="btnAdd"
                                    CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblUploadDataType" Text="Upload Data Type:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlUploadDataType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlUploadDataType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Select a Type" Value="Select a Type" />
                                    <asp:ListItem Text="File" Value="F" />
                                    <asp:ListItem Text="Link" Value="L" />
                                </asp:DropDownList>
                                <span id="span3" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlUploadDataType_CompareValidator" runat="server" ControlToValidate="ddlUploadDataType"
                                    ErrorMessage="Please select an upload data type" Operator="NotEqual" ValueToCompare="Select a Type"
                                    CssClass="cvPCG" ValidationGroup="btnAdd" Display="Dynamic">
                                </asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblDescription" Text="Description:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox CssClass="txtField" ID="txtDescription" MaxLength="500" runat="server"
                                    TextMode="MultiLine" Rows="3" Style="width: 300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trOutsideLink" runat="server" visible="false">
                            <td class="leftField">
                                <asp:Label ID="lblOutsideLink" Text="Outside Link:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td colspan="3" class="rightField">
                                <asp:TextBox CssClass="txtField" ID="txtOutsideLink" runat="server" Width="300px"
                                    MaxLength="200"></asp:TextBox>
                                <span id="span4" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvOutsideLink" runat="server"
                                    ControlToValidate="txtOutsideLink" ErrorMessage="<br/>Enter a Link" ValidationGroup="btnAdd"
                                    CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trUpload" runat="server" visible="false">
                            <td class="leftField">
                                <asp:Label ID="lblUpload" Text="Upload:" runat="server" CssClass="FieldName"></asp:Label>
                            </td>
                            <td colspan="3" class="rightField" style="vertical-align: middle">
                                <span style="font-size: xx-small">(Allowed extensions are: .doc,.xls,.pdf,.docx,.xlsx)</span>
                                <telerik:RadUpload ID="radUploadRepoItem" runat="server" ControlObjectsVisibility="None"
                                    AllowedFileExtensions=".doc,.xls,.pdf,.docx,.xlsx" Skin="Telerik" EnableEmbeddedSkins="false">
                                </telerik:RadUpload>
                                <asp:CustomValidator ID="Customvalidator1" runat="server" Display="Dynamic" ValidationGroup="btnAdd"
                                    ClientValidationFunction="validateradUploadRepoItem">
                                    <span class="cvPCG">Invalid extensions</span>
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr id="trUploadedFileName" runat="server" visible="false">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblUploadedFile" runat="server" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3" class="SubmitCell">
                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" CssClass="PCGButton"
                                    ValidationGroup="btnAdd" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ManageRepository_btnAdd', 'S');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ManageRepository_btnAdd', 'S');" />
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
            <tr>
            <td>
            <asp:Label ID="lblNote" runat="server" CssClass="FieldName" Text="Note:- Please keep file name unique.</br>    Earlier file with same name will be overridden"></asp:Label>
            </td></tr>
        </table>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView1" runat="server">
<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">View Repository</td>
        <td  align="right" style="padding-bottom:2px;">
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>
        <%--<table width="100%" class="TableBackground">
            <tr>
                <td class="HeaderCell">
                    <asp:Label ID="lblVRTitle" runat="server" CssClass="HeaderTextBig" Text="View Repository"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>--%>
        <table class="TableBackground" width="100%">
            <tr id="trContentVR" runat="server">
                <td>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgRepositoryList" runat="server" Width="860px" Height="250px"
                                    PageSize="6" AllowPaging="True" GridLines="None" AutoGenerateColumns="false"
                                    Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                    EnableViewState="true" OnNeedDataSource="rgRepositoryList_NeedDataSource" OnItemDataBound="rgRepositoryList_ItemDataBound"
                                    AllowFilteringByColumn="true">
                                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                    <MasterTableView DataKeyNames="AR_RepositoryId,AR_Filename,AR_IsFile" ShowFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn1" Groupable="False" AllowFiltering="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="hdrCheckBox" runat="server" OnCheckedChanged="hdrCheckBox_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkbxRow" runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                                        ForeColor="White" />
                                                </FooterTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="HeadingText" HeaderText="Heading Text" AllowFiltering="false"
                                                ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnFileName" runat="server" CssClass="CmbField" OnClick="lnkBtnFileNameClientListGrid_Click"
                                                        Text='<%# Eval("AR_HeadingText") %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="Description" HeaderText="Description" DataField="AR_Description"
                                                AllowFiltering="false" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="ARC_RepositoryCategory"
                                                AllowFiltering="true" CurrentFilterFunction="Contains" ItemStyle-Wrap="false"
                                                HeaderStyle-Wrap="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="Path" HeaderText="File/Link" AllowFiltering="false"
                                                ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl" Text='<%# Boolean.Parse(DataBinder.Eval(Container.DataItem, "AR_IsFile").ToString())? DataBinder.Eval(Container.DataItem, "AR_Filename").ToString():DataBinder.Eval(Container.DataItem, "AR_Link").ToString() %>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="Date" HeaderText="Uploaded On" DataField="AR_CreatedOn"
                                                DataFormatString="{0:D}" AllowFiltering="false" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trNoRecords" runat="server">
                <td align="center">
                    <div id="divNoRecords" runat="server" class="failure-msg">
                        <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </telerik:RadPageView>
</telerik:RadMultiPage>

<script type="text/javascript">
    function validateradUploadRepoItem(source, arguments) {
        arguments.IsValid = $find('<%= radUploadRepoItem.ClientID %>').validateExtensions();
        return false;
    }
</script>

