<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TradeBusinessDate.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.TradeBusinessDate" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            TradeBusinessDate
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnTradeBusinessDate" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="true" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnTradeBusinessDate_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table cellspacing="0" cellpadding="1" width="70%">
    <tr>
        <td>
            <telerik:RadGrid ID="gvTradeBusinessDate" runat="server" EnableEmbeddedSkins="false"
                AllowFilteringByColumn="true" AutoGenerateColumns="false" ShowStatusBar="true"
                AllowAutomaticDeletes="true" ShowFooter="true" AllowPaging="true" AllowSorting="true"
                GridLines="both" AllowAutomaticInserts="false" Skin="Telerik" OnNeedDataSource="gvTradeBusinessDate_NeedDataSource"
                OnItemCommand="gvTradeBusinessDateDetails_ItemCommand" Width="100%">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                </ExportSettings>
                <MasterTableView EditMode="PopUp" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
                    CommandItemSettings-AddNewRecordText="Add TradeBusinessDate" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" DataKeyNames="WTBD_Id">
                    <Columns>
                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderStyle-Width="50px">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" Wrap="false" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridTemplateColumn HeaderText="Checking" UniqueName="check" AllowFiltering="false">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_DayName" HeaderText="Date" DataField="WTBD_DayName"
                            SortExpression="WTBD_DayName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            DataFormatString="{0:d}" AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_DayName1" HeaderText="ExecutionDate" DataField="WTBD_DayName1"
                            SortExpression="WTBD_DayName1" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            DataFormatString="{0:d}" AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_IsHoliday" HeaderText="IsHoliday" DataField="WTBD_IsHoliday"
                            SortExpression="WTBD_IsHoliday" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_IsWeekend" HeaderText="IsWeekend" DataField="WTBD_IsWeekend"
                            SortExpression="WTBD_IsWeekend" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Delete" ConfirmText="Do you want to delete the mapping?"
                            CommandName="Delete" UniqueName="DeleteCommandColumn" HeaderStyle-Width="50px">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" Wrap="false" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template" FormTableStyle-Width="100px">
                        <FormTemplate>
                            <table cellspacing="2" cellpadding="2" width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:Label runat="server" Text="Date :" ID="lblDate"></asp:Label>
                                        <td align="right">
                                            <telerik:RadDatePicker ID="RadDatePicker1" CssClass="txtField" runat="server" Culture="English (United States)"
                                                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                                <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                                                </Calendar>
                                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                <DateInput ID="DateInput1" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" runat="server">
                                                </DateInput>
                                            </telerik:RadDatePicker>
                                        </td>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label runat="server" Text="Execution Date :" ID="lb1ExecutionDate"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <telerik:RadDatePicker ID="txtExecutionDate" CssClass="txtField" runat="server" Culture="English (United States)"
                                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                            <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                                            </Calendar>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                            <DateInput ID="DateInput2" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" runat="server">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:RadioButton ID="rbtnIsHoliday" Class="cmbField" runat="server" AutoPostBack="true"
                                            Text="IsHoliday" />
                                    </td>
                                    <td align="right">
                                        <asp:RadioButton ID="rbtnIsWeekened" Class="cmbField" runat="server" AutoPostBack="true"
                                            Text="IsWeekened" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                            CommandName="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lb1Type" runat="server" Text="Type  : " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlyear" runat="server" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem>2013</asp:ListItem>
                <asp:ListItem>2014</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select year" Display="Dynamic" ControlToValidate="ddlyear"
                ValidationGroup="Btncreatecal">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Button ID="Btncreatecal" Text="createcal" runat="server" CssClass="PCGLongButton"
                OnClick="Btncreatecal_OnClick" ValidationGroup="Btncreatecal" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="Btnmarkholiday" Text="markholiday" runat="server" CssClass="PCGLongButton"
                OnClick="Btnmarkholiday_Onclick" ValidationGroup="Btnmarkholiday" />
        </td>
    </tr>
</table>
