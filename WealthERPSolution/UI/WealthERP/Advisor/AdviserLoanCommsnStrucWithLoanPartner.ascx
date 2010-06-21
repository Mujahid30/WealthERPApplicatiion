<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserLoanCommsnStrucWithLoanPartner.ascx.cs" Inherits="WealthERP.Advisor.AdviserLoanCommsnStrucWithLoanPartner" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Loan Structure with Partner"></asp:Label>
            <hr />
        </td>
    </tr>



    <tr>
        <td>
            <asp:ScriptManager ID="AdvLnCommnLnPrtLnScripManager" runat="server">
            </asp:ScriptManager>
            <asp:GridView ID="gvLnCommsnStrucLnPtr" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ShowFooter="True" CssClass="GridViewStyle" AllowPaging="True"
                DataKeyNames="ALPC_Id" OnRowDataBound="gvLnCommsnStrucLnPtr_RowDataBound" 
                onrowdeleting="gvRowDeleting" 
                OnPageIndexChanging="gvLnCommsnStrucLnPtr_PageIndexChanging" PageSize="2">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" />
                <EmptyDataTemplate>
                    No Data Found.
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Loan Partner">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlLoanPartner" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAddLoanPartner" runat="server" CssClass="cmbField" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAddLoanType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Loan Type">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlLoanType" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAddLoanType" runat="server" CssClass="cmbField" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAddLoanType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Scheme Name">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlSchemeName" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAddSchemeName" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Commission Fee">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCommissionFee" runat="server" Text='<%# Bind("CommissionFee") %>' CssClass="txtField" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddCommissionFee" CssClass="txtField" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lower limit of range">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSlabLowerLimit" runat="server" Text='<%# Bind("SlabLowerLimit") %>' CssClass="txtField"/>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSlabLowerLimit" CssClass="txtField" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upper limit of range">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSlabUpperLimit" runat="server" Text='<%# Bind("SlabUpperLimit") %>' CssClass="txtField"/>
                              <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtSlabUpperLimit"
                                ErrorMessage="Lower Limit should be less than the Upper Limit"
                                Type="Double" Operator="GreaterThan" ControlToCompare="txtSlabLowerLimit" CssClass="cvPCG"
                                ValidationGroup="btnSubmit" Display="Dynamic"></asp:CompareValidator>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSlabUpperLimit" CssClass="txtField" runat="server" />
                              <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtAddSlabUpperLimit"
                                ErrorMessage="Lower Limit should be less than the Upper Limit"
                                Type="Double" Operator="GreaterThan" ControlToCompare="txtAddSlabLowerLimit" CssClass="cvPCG"
                                ValidationGroup="btnSubmit" Display="Dynamic"></asp:CompareValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtStartDate" runat="server" Text='<%#  Eval("StartDate", "{0:dd-MM-yyyy}") %>' CssClass="txtField"/>
                            <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="txtStartDate_TextBoxWatermarkExtender" runat="server"
                                TargetControlID="txtStartDate" WatermarkText="dd/mm/yyyy">
                            </cc1:TextBoxWatermarkExtender>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddStartDate" CssClass="txtField" runat="server" />
                            <cc1:CalendarExtender ID="txtAddStartDate_CalendarExtender" runat="server" TargetControlID="txtAddStartDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="txtAddStartDate_TextBoxWatermarkExtender" runat="server"
                                TargetControlID="txtAddStartDate" WatermarkText="dd/mm/yyyy">
                            </cc1:TextBoxWatermarkExtender>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:dd-MM-yyyy}") %>' CssClass="txtField"/>
                            <cc1:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="txtEndDate_TextBoxWatermarkExtender" runat="server"
                                TargetControlID="txtEndDate" WatermarkText="dd/mm/yyyy">
                            </cc1:TextBoxWatermarkExtender>
                              <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtEndDate"
                                ErrorMessage="End Date should be greater than Start Date" Type="Date" Operator="GreaterThanEqual"
                                ControlToCompare="txtStartDate" CssClass="cvPCG" ValidationGroup="btnSubmit"
                                Display="Dynamic">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:CompareValidator>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEndDate"  runat="server" CssClass="txtField"/>
                            <cc1:CalendarExtender ID="txtAddEndDate_CalendarExtender" runat="server" TargetControlID="txtAddEndDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="txtAddEndDate_TextBoxWatermarkExtender" runat="server"
                                TargetControlID="txtAddEndDate" WatermarkText="dd/mm/yyyy">
                            </cc1:TextBoxWatermarkExtender>
                              <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtAddEndDate"
                                ErrorMessage="End Date should be greater than Start Date" Type="Date" Operator="GreaterThanEqual"
                                ControlToCompare="txtAddStartDate" CssClass="cvPCG" ValidationGroup="btnSubmit"
                                Display="Dynamic">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:CompareValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True">
                    <ControlStyle CssClass="Error" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id= "trValueInserted" runat="server" visible="false" >
        <td>
            <asp:Label ID="lblValueInserted" Text="New Values Inserted Successfully" runat="server" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr id= "trValuesUpdated" runat="server" visible="false" >
        <td>
            <asp:Label ID="lblValuesUpdated" Text="Changes Updated Successfully" runat="server" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr id= "trInsertError" runat="server" visible="false" >
        <td>
            <asp:Label ID="lblInsertError" Text="There was an error during insertion." runat="server" CssClass="Error"></asp:Label>
        </td>
    </tr>
    <tr id= "trUpdateError" runat="server" visible="false" >
        <td>
            <asp:Label ID="Label1" Text="There was an error during updation of one of the rows." runat="server" CssClass="Error"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="PCGButton" OnClick="btnSave_Click" />
            <asp:Label ID = "lblNoOfRows" runat="server" Visible ="false"></asp:Label>
        </td>
    </tr>
    
</table>
