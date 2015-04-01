<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfflineCustomerMerge.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.OfflineCustomerMerge" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

<script type="text/javascript">
    var popUp;
    function PopUpShowing(sender, eventArgs) {
        popUp = eventArgs.get_popUp();
        var gridWidth = sender.get_element().offsetWidth;
        var gridHeight = sender.get_element().offsetHeight;
        var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
        var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
        popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
        popUp.style.top = ((gridHeight - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
    } 
</script>

<table width="100%"  >
    <tr>
        <td>
            <telerik:RadWindow ID="radPopUpCustomer" runat="server" VisibleOnPageLoad="false"
                Height="200%" Width="700px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
                Behaviors="Resize, Close, Move" Title="Auto Match Criteria" RestrictionZoneID="radWindowZone"
                OnClientShow="setCustomPosition" Top="10" Left="20">
                <ContentTemplate>
                    <asp:Panel ID="pnlCustomerList1" runat="server" class="Landscape" Height="80%" ScrollBars="Both"
                        Width="100%">
                        <telerik:RadGrid ID="RgPopUpCustomer" runat="server" AllowAutomaticInserts="false"
                            AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False"
                            EnableEmbeddedSkins="false" GridLines="None" OnNeedDataSource="RgPopUpCustomer_OnNeedDataSource"
                            PageSize="10" ShowFooter="true" ShowStatusBar="True" Skin="Telerik">
                            <%--  <ExportSettings ExportOnlyData="true" FileName="OrderMIS" HideStructureColumns="true">
        </ExportSettings>--%>
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None"
                                DataKeyNames="C_CustomerId,C_Mobile1,C_DOB,C_Email,DeleteCustomerId">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToCsvButton="false"
                                    ShowExportToExcelButton="false" ShowExportToWordButton="false" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        DataField="C_CustomerId" HeaderStyle-Width="100px" HeaderText="Customer Id" ShowFilterIcon="false"
                                        SortExpression="C_CustomerId" UniqueName="C_CustomerId" Visible="false">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="30px" ItemStyle-Width="10px"
                                        Visible="true">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkManualMerge" runat="server" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        DataField="CustomerName" HeaderStyle-Width="100px" HeaderText="Customer Name"
                                        ShowFilterIcon="false" SortExpression="CustomerName" UniqueName="CustomerName">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        DataField="C_PANNum" FilterControlWidth="50px" HeaderStyle-Width="100px" HeaderText="PAN"
                                        ShowFilterIcon="false" SortExpression="C_PANNum" UniqueName="C_PANNum">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        DataField="C_DOB" FilterControlWidth="60px" HeaderStyle-Width="100px" HeaderText="DOB"
                                        ShowFilterIcon="false" SortExpression="C_DOB" UniqueName="C_DOB">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        DataField="C_Mobile1" HeaderStyle-Width="100px" HeaderText="Mobile" ShowFilterIcon="false"
                                        SortExpression="C_Mobile1" UniqueName="C_Mobile1">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        DataField="C_Email" HeaderStyle-Width="100px" HeaderText="Email" ShowFilterIcon="false"
                                        SortExpression="C_Email" UniqueName="C_Email">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        DataField="DeleteCustomerId" HeaderStyle-Width="100px" HeaderText="DeleteCustomerId"
                                        ShowFilterIcon="false" SortExpression="DeleteCustomerId" UniqueName="DeleteCustomerId" Visible="false">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Resizing AllowColumnResize="true" />
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </asp:Panel>
                    <asp:Button ID="Button1" runat="server" Text="Merge" CssClass="PCGMediumButton" OnClick="btnManualMerge_Click"
                        Visible="true" />
                </ContentTemplate>
            </telerik:RadWindow>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="6">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Customer Merge
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblSelect" runat="server" CssClass="FieldName" Text="Find :"></asp:Label>
            <%--  </td>
        <td  >--%>
            <asp:DropDownList ID="ddlTheme" runat="server" AutoPostBack="true" CssClass="cmbField"
                Width="250px">
                <asp:ListItem Value="BlackAndWhite">Customers with no pan :</asp:ListItem>
            </asp:DropDownList>
            <%--   </td>
        
        <td>--%>
            <telerik:RadListBox ID="chkbldepart" runat="server" AutoPostBack="true" CheckBoxes="true">
                <Items>
                    <telerik:RadListBoxItem Text="DOB" Value="DOB" />
                    <telerik:RadListBoxItem Text="Email" Value="Email" />
                    <telerik:RadListBoxItem Text="Mobile" Value="Mobile" />
                </Items>
            </telerik:RadListBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGO" runat="server" CssClass="PCGButton" OnClick="btnGO_Click"
                Text="Go" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr id="trMessage" runat="server">
        <td colspan="6">
            <table cellspacing="0" class="tblMessage">
                <tr>
                    <td align="center">
                        <div id="div4" align="center">
                        </div>
                        <div style="clear: both">
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" class="tblMessage">
    <tr>
        <td align="center">
            <div id="divMessage" align="center">
            </div>
            <div style="clear: both">
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlCustomerList" runat="server" class="Landscape" Height="80%" ScrollBars="Horizontal"
    Visible="false" Width="100%">
    <telerik:RadGrid ID="gvCustomer" runat="server" AllowAutomaticInserts="false" AllowFilteringByColumn="true"
        AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" EnableEmbeddedSkins="false"
        GridLines="None" OnItemCommand="gvCustomer_OnItemCommand" OnNeedDataSource="gvCustomer_OnNeedDataSource"
        PageSize="10" ShowFooter="true" ShowStatusBar="True" Skin="Telerik">
        <ExportSettings ExportOnlyData="true" FileName="OrderMIS" HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None"
            DataKeyNames="C_CustomerId,C_Mobile1,C_DOB,C_Email,MatchCustomerId">
            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToCsvButton="false"
                ShowExportToExcelButton="false" ShowExportToWordButton="false" ShowRefreshButton="false" />
            <Columns>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    DataField="C_CustomerId" HeaderStyle-Width="100px" HeaderText="Customer Id" ShowFilterIcon="false"
                    SortExpression="C_CustomerId" UniqueName="C_CustomerId" Visible="false">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="30px" ItemStyle-Width="10px"
                    Visible="true">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkdummypan" runat="server" AutoPostBack="true" OnCheckedChanged="chkdummypan_OnCheckedChanged" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    DataField="CustomerName" HeaderStyle-Width="100px" HeaderText="Customer Name"
                    ShowFilterIcon="false" SortExpression="CustomerName" UniqueName="CustomerName">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="CMFA_FolioNum" HeaderStyle-Width="100px" HeaderText="Folio" ShowFilterIcon="false"
                    SortExpression="CMFA_FolioNum" UniqueName="CMFA_FolioNum" Visible="false">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="C_PANNum" FilterControlWidth="50px" HeaderStyle-Width="100px" HeaderText="PAN"
                    ShowFilterIcon="false" SortExpression="C_PANNum" UniqueName="C_PANNum">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="C_DOB" FilterControlWidth="60px" HeaderStyle-Width="100px" HeaderText="DOB"
                    ShowFilterIcon="false" SortExpression="C_DOB" UniqueName="C_DOB">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="C_Mobile1" HeaderStyle-Width="100px" HeaderText="Mobile" ShowFilterIcon="false"
                    SortExpression="C_Mobile1" UniqueName="C_Mobile1">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="C_Email" HeaderStyle-Width="100px" HeaderText="Email" ShowFilterIcon="false"
                    SortExpression="C_Email" UniqueName="C_Email">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="C_Adr1Line1" HeaderStyle-Width="100px" HeaderText="PAN" ShowFilterIcon="false"
                    SortExpression="C_Adr1Line1" UniqueName="C_Adr1Line1" Visible="false">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="MatchCount" FilterControlWidth="50px" HeaderStyle-Width="100px" HeaderText="MatchCount"
                    ShowFilterIcon="false" SortExpression="MatchCount" UniqueName="MatchCount">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="MatchCriteria" FilterControlWidth="50px" HeaderStyle-Width="100px"
                    HeaderText="MatchCriteria" ShowFilterIcon="false" SortExpression="MatchCriteria"
                    UniqueName="MatchCriteria">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="MatchPan" FilterControlWidth="50px" HeaderStyle-Width="100px" HeaderText="MatchPAN"
                    ShowFilterIcon="false" SortExpression="MatchPan" UniqueName="MatchPan">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="MatchDOB" FilterControlWidth="60px" HeaderStyle-Width="100px" HeaderText="MatchDOB"
                    ShowFilterIcon="false" SortExpression="MatchDOB" UniqueName="MatchDOB">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="MatchMobile" HeaderStyle-Width="100px" HeaderText="MatchMobile" ShowFilterIcon="false"
                    SortExpression="MatchMobile" UniqueName="MatchMobile">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    DataField="MatchEmail" HeaderStyle-Width="100px" HeaderText="MatchEmail" ShowFilterIcon="false"
                    SortExpression="MatchEmail" UniqueName="MatchEmail">
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="50px" HeaderText="CustomerId"
                    UniqueName="MatchCustomerId" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCustomerId" runat="server" Text="">
                        </asp:TextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="50px" HeaderText="Manual"
                    Visible="true">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSelectClient" runat="server" OnClick="lnkSelectClient_Click"
                            Text="Manual Merge">
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ButtonType="PushButton" CommandName="Delete" ConfirmText="Do you want to match"
                    HeaderStyle-Width="100px" Text="Match" UniqueName="Match" Visible="false">
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:Button ID="btnBulkMerge" runat="server" Text="Bulk Merge" CssClass="PCGMediumButton"
        OnClick="btnBulkMerge_Click" Visible="true" />
</asp:Panel>
<asp:HiddenField ID="hdnPan" runat="server" />
<asp:HiddenField ID="hdnDOB" runat="server" />
<asp:HiddenField ID="hdnEMAIL" runat="server" />
<asp:HiddenField ID="hdnMoblile" runat="server" />
