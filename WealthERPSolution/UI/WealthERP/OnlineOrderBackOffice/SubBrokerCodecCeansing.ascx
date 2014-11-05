<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubBrokerCodecCeansing.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.SubBrokerCodecCeansing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script type="text/javascript">
    $(window).bind('load', function() {
        var headerChk = $(".chkHeader input");
        var itemChk = $(".chkItem input");
        headerChk.bind("click", function() {
            itemChk.each(function() { this.checked = headerChk[0].checked; })
        });
        itemChk.bind("click", function() { if ($(this).checked == false) headerChk[0].checked = false; });
    });
</script>

<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            SubBroker Code Cleansing
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td align="right">
            <asp:Label ID="lblRTA" runat="server" Text="Select RTA:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRTA" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlRTA_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span27" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvddlRTA" runat="server" ErrorMessage="Please Select RTA"
                CssClass="rfvPCG" ControlToValidate="ddlRTA" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblAMC" runat="server" CssClass="FieldName" Text="AMC:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlAMC_OnSelectedIndexChanged"
                AutoPostBack="true" Width="340px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblScheme" runat="server" Text="SchemePlan:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlScheme" CssClass="cmbField" runat="server" Width="330px">
            </asp:DropDownList>
        </td>
        <td align="right">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblSubbrokerCode" runat="server" Text="SubBroker Code:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSubbrokerCode" runat="server" CssClass="cmbField" Width="340px">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="With Identified SubBroker Code" Value="1"></asp:ListItem>
                <asp:ListItem Text="With UnIdentified SubBroker Code" Value="2"></asp:ListItem>
                <asp:ListItem Text="WithOut SubBrokerCode" Value="3"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select SubBroker Code"
                CssClass="rfvPCG" ControlToValidate="ddlSubbrokerCode" ValidationGroup="btnGo"
                Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;&nbsp;
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" OnClick="Go_OnClick"
                ValidationGroup="btnGo" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlgvSubBrokerCleansing" runat="server" ScrollBars="Horizontal" Height="100%"
    Width="100%" Visible="false">
    <table width="100%">
        <tr id="trSubBrokerCleansing" runat="server">
            <td>
                <telerik:RadGrid ID="gvSubBrokerCleansing" runat="server" AutoGenerateColumns="false"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="gvSubBrokerCleansing_NeedDataSource">
                    <MasterTableView DataKeyNames="CMFT_MFTransId,CMFT_SubBrokerCode" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="20px" UniqueName="chkBoxColumn">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" CssClass="chkHeader" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkItem" runat="server" CssClass="chkItem"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Name" SortExpression="Name" UniqueName="Name"
                                AllowFiltering="true" HeaderText="Customer Name" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_PANNum" SortExpression="C_PANNum" UniqueName="C_PANNum"
                                AllowFiltering="true" HeaderText="PAN No" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFA_FolioNum" SortExpression="CMFA_FolioNum"
                                UniqueName="CMFA_FolioNum" AllowFiltering="true" HeaderText="Folio Number" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="CMFT_SubBrokerCode" SortExpression="CMFT_SubBrokerCode"
                                UniqueName="CMFT_SubBrokerCode" AllowFiltering="true" HeaderText="SubBrokerCode"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSubBrokerCode" runat="server" Text='<%#Eval("CMFT_SubBrokerCode") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="newSubBrokerCode" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="AssociatesName" SortExpression="AssociatesName"
                                UniqueName="AssociatesName" AllowFiltering="true" HeaderText="Associates Name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemTemplate>
                                    <%#Eval("AssociatesName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Apply" CssClass="PCGButton" OnClick="btnUpdate_Update" />
                                </FooterTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName"
                                UniqueName="PASP_SchemePlanName" AllowFiltering="true" HeaderText="Scheme Name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                FilterControlWidth="180px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_Amount" SortExpression="CMFT_Amount" UniqueName="CMFT_Amount"
                                AllowFiltering="true" HeaderText="Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_Units" SortExpression="CMFT_Units" UniqueName="CMFT_Units"
                                AllowFiltering="true" HeaderText="Unit" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WMTT_TransactionClassificationName" SortExpression="WMTT_TransactionClassificationName"
                                UniqueName="WMTT_TransactionClassificationName" AllowFiltering="true" HeaderText="Transaction Type"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_TransactionDate" SortExpression="CMFT_TransactionDate"
                                UniqueName="CMFT_TransactionDate" AllowFiltering="true" HeaderText="Transaction Date"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ADUL_ProcessId" SortExpression="ADUL_ProcessId"
                                UniqueName="ADUL_ProcessId" AllowFiltering="true" HeaderText="ProcessId" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<table>
    <tr>
        <td>
            <asp:Button ID="btnUpdateSubBroker" runat="server" CssClass="PCGButton" Text="Update" OnClick="btnUpdateSubBroker_OnClick" Visible="false"/>
        </td>
    </tr>
</table>
