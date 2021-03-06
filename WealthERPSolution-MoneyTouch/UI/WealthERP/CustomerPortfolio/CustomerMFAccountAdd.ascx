﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFAccountAdd.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMFAccountAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
</script>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Add MF Folio"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table>
    <tr>
        <td colspan="6" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name : "></asp:Label>
        </td>
        <td colspan="4" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFolioNum0" runat="server" CssClass="FieldName" Text="AMC Code :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlProductAmc" runat="server" CssClass="cmbLongField">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvProductAmc" runat="server" ControlToValidate="ddlProductAmc"
                ErrorMessage="Please select an AMC Code" Operator="NotEqual" ValueToCompare="Select an AMC Code"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFolioNum" runat="server" CssClass="FieldName" Text="Folio Number :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFolioNumber" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvFolioNumber" ControlToValidate="txtFolioNumber"
                ErrorMessage="Please enter a Folio Number" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblJointHolding" runat="server" CssClass="FieldName" Text="Joint Holding :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                Text="Yes"  AutoPostBack="true" 
                oncheckedchanged="rbtnYes_CheckedChanged1" />
            <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                Text="No"  AutoPostBack="true" oncheckedchanged="rbtnNo_CheckedChanged" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField" 
                >
            </asp:DropDownList>
            <%--<span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlModeOfHolding"
                ErrorMessage="Please select a Mode of Holding" Operator="NotEqual" ValueToCompare="Select Mode of Holding"
                CssClass="cvPCG"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAccountStartingDate" runat="server" CssClass="FieldName" Text="Account Starting Date :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAccountDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtAccountDate_CalendarExtender" runat="server" TargetControlID="txtAccountDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtAccountDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtAccountDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <%--<span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccountDate"
                ErrorMessage="Please select an Account Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr id="trJointHolders" runat="server" visible="false">
        <td colspan="2">
            <asp:Label ID="lblJointHolders" runat="server" CssClass="HeaderTextSmall" Text="Joint Holders"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trJointHoldersGrid" runat="server">
        <td colspan="2" align="center">
            <asp:GridView ID="gvJointHoldersList" runat="server" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="AssociationId" 
                CssClass="GridViewStyle">
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
                    <asp:BoundField DataField="Name" HeaderText="Name"  />
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship"  />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr id="trNominees" runat="server" visible="false">
        <td colspan="2">
            <asp:Label ID="lblNominees" runat="server" CssClass="HeaderTextSmall" Text="Nominees"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trNomineesGrid" runat="server">
        <td colspan="2" align="center">
            <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyNames="MemberCustomerId, AssociationId"  CssClass="GridViewStyle">
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
                    <asp:BoundField DataField="Name" HeaderText="Name"  />
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship"  />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    
    
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    
    
    <tr id="trJoint2Header" runat="server">
     <td colspan="2">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Joint Holders"></asp:Label>
            <hr />
     </td>
    </tr>
    <tr id="trJoint2" runat="server">
      
     <td align="center" colspan="2">
            <asp:GridView ID="gvJoint2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyNames="AssociateId" AllowSorting="True" CssClass="GridViewStyle" OnRowDataBound="gvJoint2_RowDataBound">
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
                    <asp:BoundField DataField="Stat" HeaderText="Stat" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    
    <tr id="trNominee2Header" runat="server" align="left">
    <td colspan="2">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Nominees"></asp:Label>
            <hr />
        </td>
    </tr>
    
    <tr id="trNominee2" runat="server">
        
        <td colspan="2" align="center">
            <asp:GridView ID="gvNominee2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyNames="AssociateId" AllowSorting="True" CssClass="GridViewStyle" OnRowDataBound="gvNominee2_RowDataBound">
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
                    <asp:BoundField DataField="Stat" HeaderText="Stat" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerMFAccountAdd_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerMFAccountAdd_btnSubmit', 'S');"
                Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerMFAccountAdd_btnUpdate', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerMFAccountAdd_btnUpdate', 'S');"
                Text="Update" OnClick="btnUpdate_Click" />
        </td>
    </tr>
</table>
