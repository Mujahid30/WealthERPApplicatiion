<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserLoginTrack.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserLoginTrack" %>
<asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                           Login History
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<table width="100%">
    <tr>
        <td>
        </td>
    </tr>
</table>--%>
<table width="80%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr>
        <td>
            <asp:Label ID="lblSelectCategory" runat="server" Text="Select User :" CssClass="FieldName"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="cmbField"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                <asp:ListItem Value="All">All </asp:ListItem>
                <asp:ListItem Value="customer">Customer</asp:ListItem>
                <asp:ListItem Value="advisor">Staff</asp:ListItem>
                <asp:ListItem Value="associates">Associates</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    <%-- <ClientEvents OnLoad="onLoadRadTimePicker1"> </ClientEvents>--%>
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td align="right">
            <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
        </td>
        <td align="left" valign="middle" colspan="2">
            <telerik:RadDatePicker ID="txtTo" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    <%--  <ClientEvents OnLoad="onLoadRadTimePicker2"> </ClientEvents>--%>
                </DateInput>
            </telerik:RadDatePicker>
        </td>
       <td>
            <asp:CompareValidator ID="dateCompareValidator" runat="server" ControlToValidate="txtTo"
                ControlToCompare="txtFrom" Operator="GreaterThan" Type="Date" Font-Size="Small" ErrorMessage="Error">
            </asp:CompareValidator>
        </td>
        <td colspan="6" >
            <asp:Button ID="btnGo" runat="server" Text="Go" ValidationGroup="btnGo" CssClass="PCGButton"
                OnClick="btnGo_Click" />
        </td>         
    </tr>
</table>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>
        <div>
        <table cellspacing="0" cellpadding="3" width="100%">
                
                <tr>
                    <td>
                        <telerik:RadGrid ID="gvLoginTrack" AllowSorting="true" runat="server" AllowAutomaticInserts="false"
                            EnableLoadOnDemand="true" AllowFilteringByColumn="true" AllowPaging="True" AutoGenerateColumns="False"
                            EnableViewState="true" EnableEmbeddedSkins="false" GridLines="None" PageSize="10"
                            ShowFooter="true" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik" OnNeedDataSource="gvLoginTrack_NeedDataSource">
                            <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                            <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                FileName="AdviserLoginTrack" Excel-Format="ExcelML">
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                AllowFilteringByColumn="true">
                                <Columns>
                                 <telerik:GridBoundColumn DataField="LT_CreatedOn" HeaderStyle-Width="100px" AllowFiltering="false" SortExpression="true"
                                        DataFormatString="{0:dd/MM/yyyy HH:mm:ss.FFFF}" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        HeaderText="Date and Time" UniqueName="LoginDateTime">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Wrap="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LT_LoginId" HeaderText="ID" HeaderStyle-Width="100px"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="LoginId" AllowFiltering="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="U_FirstName" HeaderStyle-Width="100px" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" HeaderText="Name" UniqueName="Name" AllowFiltering="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>                                   
                                    <telerik:GridBoundColumn DataField="U_UserType" HeaderStyle-Width="100px" AllowFiltering="false"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="UserType" UniqueName="U_UserType">
                                      <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Wrap="true" />
                                     </telerik:GridBoundColumn>
                             </Columns>
                            </MasterTableView>
                            <ClientSettings>
                           <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                           </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
  </ContentTemplate>
</asp:UpdatePanel>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnTodate" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
