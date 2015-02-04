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
                            Trade Business Date
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
<table>
    <tr>
        <td>
            <asp:Label ID="Lblselyear" runat="server" Text="Select year:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="Ddlyears" runat="server" CssClass="cmbField" AutoPostBack="true">
                <%--<asp:ListItem Text="Select" Value="Select" Selected="false" />--%>
                <asp:ListItem Text="2013" Value="2013" />
                <%--<asp:ListItem Text="Bussiness Day" Value="1"/>--%>
                <asp:ListItem Text="2014" Value="2014"  />
                <asp:ListItem Text="2015" Value="2015" Selected="True"/>
                <asp:ListItem Text="2016" Value="2016" />
                <asp:ListItem Text="2017" Value="2017" />
            </asp:DropDownList>
        </td>
        <td>
            <asp:Label ID="Lblholidays" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="Ddlholiday" runat="server" CssClass="cmbField" AutoPostBack="true">
                <Items>
                    <asp:ListItem Text="All" Value="2" />
                    <%--<asp:ListItem Text="Bussiness Day" Value="1"/>--%>
                    <asp:ListItem Text="All Business Day" Value="3" />
                    <asp:ListItem Text="Holidays" Value="1" />
                    <asp:ListItem Text="Weekend" Value="0" />
                </Items>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="Button1" Text="GO" runat="server" CssClass="PCGLongButton" OnClick="btngo_Click" />
        </td>
    </tr>
</table>
<table cellspacing="0" cellpadding="1" width="100%">
    <tr>
        <td>
            <telerik:RadGrid ID="gvTradeBusinessDate" runat="server" EnableEmbeddedSkins="false"
                AllowFilteringByColumn="false" AutoGenerateColumns="false" ShowStatusBar="true"
                AllowAutomaticDeletes="true" ShowFooter="true" AllowPaging="true" AllowSorting="true"
                GridLines="both" AllowAutomaticInserts="false" Skin="Telerik" OnNeedDataSource="gvTradeBusinessDate_NeedDataSource"
                OnItemCommand="gvTradeBusinessDateDetails_ItemCommand" OnItemDataBound="gvTradeBusinessDate_ItemDataBound"
                Width="100%">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                </ExportSettings>
                <MasterTableView EditMode="PopUp" CommandItemDisplay="none" CommandItemSettings-ShowRefreshButton="false"
                    AllowMultiColumnSorting="True" AutoGenerateColumns="false" DataKeyNames="WTBD_DayName,WTBD_Id,WTBD_HolidayName"
                    PageSize="20">
                    <Columns>
                        <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="UPdate"
                            EditText="Update" CancelText="Cancel" UpdateText="OK">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridTemplateColumn HeaderText="Select" UniqueName="check" AllowFiltering="false"
                            HeaderStyle-Width="50px">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="WTBD_Id" HeaderText="Application"
                            DataField="WTBD_Id" SortExpression="WTBD_Id" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_DayName" HeaderText="Application Date"
                            DataField="WTBD_DayName" SortExpression="WTBD_DayName" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains" DataFormatString="{0:d}" AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_Day" HeaderText="Application Day" DataField="WTBD_Day"
                            SortExpression="WTBD_Day" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_DayName1" HeaderText="Application Execution Date"
                            DataField="WTBD_DayName1" SortExpression="WTBD_DayName1" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains" DataFormatString="{0:d}" AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_ExecutionDay" HeaderText="Application Execution Day"
                            DataField="WTBD_ExecutionDay" SortExpression="WTBD_ExecutionDay" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_HolidayName" HeaderText="Holiday Name"
                            DataField="WTBD_HolidayName" SortExpression="WTBD_HolidayName" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="WTBD_IsHoliday" HeaderText="Is Holiday"
                            DataField="WTBD_IsHoliday" SortExpression="WTBD_IsHoliday" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WTBD_IsWeekend" HeaderText="Is Weekend" DataField="WTBD_IsWeekend"
                            SortExpression="WTBD_IsWeekend" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="true">
                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30px" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridButtonColumn ButtonType="LinkButton" Text="Delete" ConfirmText="Do you want to delete the mapping?"
                            CommandName="Delete" UniqueName="DeleteCommandColumn" HeaderStyle-Width="50px">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" Wrap="false" />
                        </telerik:GridButtonColumn>--%>
                    </Columns>
                    <EditFormSettings EditFormType="Template" FormTableStyle-Width="100px">
                        <FormTemplate>
                            <table cellspacing="2" cellpadding="2" width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Label runat="server" Text="Date :" ID="lblDate"></asp:Label>
                                        <td>
                                            <telerik:RadDatePicker ID="RadDatePicker1" CssClass="txtField" runat="server" Culture="English (United States)"
                                                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                                                DbSelectedDate='<%# Bind("WTBD_DayName")%>'>
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
                                    <td align="right">
                                        <asp:Label runat="server" Text="Execution Date :" ID="lb1ExecutionDate"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtExecutionDate" CssClass="txtField" runat="server" Culture="English (United States)"
                                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                                            DbSelectedDate='<%# Bind("WTBD_DayName1")%>'>
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
                                    <td>
                                        <asp:Label runat="server" Text="Holiday Name :" ID="lblHolidayname" CssClass="textField"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHolidaysName" runat="server" Text='<%# Bind("WTBD_HolidayName")%>'></asp:TextBox>
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
                <%--  <ClientSettings EnablePostBackOnRowClick="True">
                </ClientSettings>--%>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Button ID="Btnmarkholiday" Text="Mark as holiday" runat="server" CssClass="PCGLongButton"
                OnClick="Btnmarkholiday_Onclick" ValidationGroup="Btnmarkholiday" />
        </td>
        <td>
            <asp:Button ID="Btncreatecalander" Text="Create New Calendar Entry" runat="server"
                CssClass="PCGLongButton" OnClick="Btncreatecalander_OnClick" ValidationGroup="Btncreatecal" />
        </td>
        <td id="createcalanders" runat="server">
            <td class="leftLabel">
                <asp:Label ID="lb1Type" runat="server" Text="Select year:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightData">
                <asp:DropDownList ID="ddlyear" runat="server" CssClass="cmbLongField" ValidationGroup="Btncreatecal"
                    Width="215px">
                    <asp:ListItem Selected="True" Value="0">Create New Business Calendar</asp:ListItem>
                    <asp:ListItem>2013</asp:ListItem>
                    <asp:ListItem>2014</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                    ErrorMessage="Please Select year" Display="Dynamic" ControlToValidate="ddlyear"
                    ValidationGroup="Btncreatecal" InitialValue="0">Please Select year
                </asp:RequiredFieldValidator>
            </td>
            <td align="right">
                <asp:Button ID="Btncreatecal" Text="GO" runat="server" CssClass="PCGLongButton" OnClick="Btncreatecal_OnClick"
                    ValidationGroup="Btncreatecal" />
            </td>
        </td>
    </tr>
</table>
<telerik:RadWindow ID="radwindowPopup" runat="server" VisibleOnPageLoad="false" Height="30%"
    Width="350px" Modal="true" BackColor="#4B4726" VisibleStatusbar="false" Behaviors="None"
    Title="Add Holidays" Left="200" Top="200">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Lblcmt" runat="server" Text="Write Holidays Name"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="Texcmt" runat="server"></asp:TextBox>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="Texcmt"
                        ErrorMessage="<br />Please Enter holiday name" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Btnmarkholid" runat="server" Text="Mark as Holiday" OnClick="btnOk_Click"
                        CssClass="PCGLongButton" ValidationGroup="Submit" />
                </td>
                <td>
                    <asp:Button ID="Butncancle" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        CssClass="PCGButton" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>
<asp:HiddenField ID="hdnyear" runat="server" />
<asp:HiddenField ID="hdnholiday" runat="server" />
