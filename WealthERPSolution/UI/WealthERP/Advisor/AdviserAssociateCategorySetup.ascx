<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserAssociateCategorySetup.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserAssociateCategorySetup" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Associate Category"></asp:Label>
            <hr />
        </td>
    </tr>
</table>


<table style="width: 100%;" class="TableBackground">
    <tr id="trAssignNumber" visible="false" runat="server">
        <td class="rightField">
            <asp:Label ID="lblNoOfCat" CssClass="FieldName" runat="server" Text="No of Categories:"></asp:Label>
            <asp:TextBox ID="txtNoOfCat" CssClass="txtField" runat="server"></asp:TextBox>
            &nbsp; &nbsp;
            <asp:Button ID="BtnNoOfCat" CssClass="PCGButton" Text="Submit" runat="server" OnClick="BtnNoOfCat_Click" />
        </td>
    </tr>
    <tr id="trMeaageDefault" runat="server" visible="false" class="Message">
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="Default values have been generated as the Names. You may edit the same according to your choice"
                CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr id="trUnboundedgrid" visible="false" runat="server">
        <td>
            <asp:GridView ID="gvAssocCatSetUp" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" ShowFooter="true" OnRowDataBound="gvAssocCatSetUp_RowDataBound">
                <FooterStyle CssClass="FieldName" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                    <asp:TemplateField HeaderText="Category Code">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="txtField"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category Description">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="txtField"></asp:TextBox>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" CssClass="PCGLongButton" OnClick="ButtonAdd_Click"
                                Text="Add New Row" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trBoundedgrid" visible="false" runat="server">
        <td>
            <asp:GridView ID="gvAssocCatSetUpBounded" runat="server" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" ShowFooter="True" OnRowDataBound="gvAssocCatSetUp_RowDataBound"
                DataKeyNames="AssociateCategoryId" OnRowDeleting="DeleteAssocCategory">
                <FooterStyle CssClass="FieldName" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                    <asp:TemplateField HeaderText="Category Code">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AssociateCategoryCode") %>'
                                CssClass="txtField"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category Description">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AssociateCategoryName") %>'
                                CssClass="txtField"></asp:TextBox>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" CssClass="PCGLongButton" OnClick="ButtonAdd_Click"
                                Text="Add New Row" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AssociateCategoryId" HeaderText="AssociateCategoryId"
                        Visible="false" />
                    <asp:CommandField ShowDeleteButton="True">
                        <ControlStyle CssClass="Error" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSave" CssClass="PCGButton" Text="Save" runat="server" OnClick="btnSave_Click" />
        </td>
    </tr>
    <tr id="trError" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Error" runat="server"></asp:Label>
        </td>
    </tr>
</table>
