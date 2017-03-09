<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerDataAlertManagement.ascx.cs"
    Inherits="WealthERP.Alerts.CustomerDataAlertManagement" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<%--<script language="javascript" type="text/javascript">
    function ddlSchemeDrop_Change(dropdown) {

        //var index = dropdown.selectedIndex;
        var ddl = dropdown.value;
        //var text = dropdown.options[index].text;
        UseCallBackDropDown(ddl, "");
    }

    function PopulateCurrentValue(value, context) {
        document.getElementById('ctrl_CustomerConditionalAlertManage_txtCurrentValue').value = value;
    }
</script>--%>
<table style="width: 100%;" cssclass="TableBackground">
    <tr>
        <td class="rightField">
            <asp:Label ID="lblHeader" runat="server" Text="Occurrence Setup" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="rightField">
            <asp:Label ID="lblCustomerName" runat="server" CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
</table>
<br />
<br />
<table style="width: 100%;" cssclass="TableBackground">
    <tr>
        <td>
            &nbsp;<asp:Button ID="btnAddNewConditionalEvent" runat="server" Text="Add New" OnClick="btnAddNewConditionalEvent_Click" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblMessage" runat="server" CssClass="HeaderTextSmall">You have not Subscribed for any Event. Click &#39;Add New&#39; to Subscribe.</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:GridView ID="gvConditionalAlerts" runat="server" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" DataKeyNames="EventSetupID" AllowSorting="True"
                HorizontalAlign="Center" BackImageUrl="~/CSS/Images/PCGGridViewHeaderGlass2.jpg"
                ShowFooter="True" EnableViewState="true">
                <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <%--OnRowEditing="gvConditionalAlerts_RowEditing" OnRowCommand="gvConditionalAlerts_RowCommand"
                OnRowUpdating="gvConditionalAlerts_RowUpdating" OnRowCancelingEdit="gvConditionalAlerts_RowCancelingEdit"
                OnRowDataBound="gvConditionalAlerts_RowDataBound"--%>
                <RowStyle Font-Size="Small" CssClass="RowStyle" HorizontalAlign="Center" />
                <EditRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" CssClass="SelectedRowStyle"
                    HorizontalAlign="Center" />
                <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle Font-Bold="True" ForeColor="White" Font-Size="Small" HorizontalAlign="Center"
                    CssClass="HeaderStyle" />
                <AlternatingRowStyle BackColor="White" Font-Size="Small" HorizontalAlign="Center" />
                <Columns>
                    <%--Check Boxes--%>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnDeleteSelected" CssClass="FieldName" OnClick="btnDeleteSelected_Click"
                                runat="server" Text="Delete" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%--Alert--%>
                    <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAlert" runat="server" Text='<%# Bind("Details") %>' Enabled="false"
                                Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAlert" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <%--Scheme--%>
                    <%--<asp:TemplateField HeaderText="Scheme" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtScheme" runat="server" Text='<%# Bind("Scheme") %>' Enabled="false" Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblScheme" runat="server" Text='<%# Bind("Scheme") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>--%>
                    <%--Current Value--%>
                    <asp:TemplateField HeaderText="Current Value" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCurrValue" runat="server" Text='<%# Bind("CurrentValue") %>'
                                Enabled="false" Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCurrValue" runat="server" Text='<%# Bind("CurrentValue") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <%--Condition--%>
                    <asp:TemplateField HeaderText="Condition" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlCondition" runat="server">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnCondition" runat="server" Value='<%# Bind("Condition") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCondition" runat="server" Text='<%# Bind("Condition") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <%--Preset Value--%>
                    <asp:TemplateField HeaderText="Preset Value" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPresetValue" runat="server" Text='<%# Bind("PresetValue") %>'
                                Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPresetValue" runat="server" Text='<%# Bind("PresetValue") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <%--Message--%>
                    <asp:TemplateField HeaderText="Message" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMessage" runat="server" Text='<%# Bind("Message") %>' Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMessage" runat="server" Text='<%# Bind("Message") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <%--Edit Field--%>
                    <%--<asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update"></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <br />
        </td>
    </tr>
    </table>
    <table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
    </table>
    <table width="100%">
    <tr>
        <td align="center">
            &nbsp;
        </td>
    </tr>
    <tr id="trAddNewEvent" runat="server">
        <td class="rightField">
            <label id="lblAddEvent" class="HeaderTextSmall">
                Add New Event</label>
            <hr />
        </td>
    </tr>
    <tr id="trAddNewCondition" runat="server" visible="false">
        <td align="center">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblSchemeEntry" runat="server" CssClass="FieldName" Text="Identifier"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" Text="Current Value"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCondition" runat="server" CssClass="FieldName" Text="Preset Condition"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPresetValue" runat="server" CssClass="FieldName" Text="Preset Value"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCondition" runat="server" CssClass="cmbField">
                            <asp:ListItem>&gt;</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                            <asp:ListItem>&gt;=</asp:ListItem>
                            <asp:ListItem>&lt;=</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPresetValue" runat="server" CssClass="txtField"></asp:TextBox>
                        <%--<span id="Span4" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPresetValue"
                            ValidationGroup="btnSaveConditionAlert" ErrorMessage="<br />Please give a Preset Value"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Please enter only Numeric Value"
                            ValidationGroup="btnSaveConditionAlert" Type="Double"ControlToValidate="txtPresetValue"
                            Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr id="trConditionSave" runat="server" visible="false">
        <td align="center">
            <asp:Button ID="btnSaveConditionAlert" runat="server" Text="Subscribe" class="ButtonField"
                OnClick="btnSaveConditionAlert_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="SchemeName ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
