<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineCustomerOrderandTransactionBook.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.OnlineCustomerOrderandTransactionBook" %>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/bootstrap.js" type="text/javascript"></script>

<link href="../Base/CSS/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="scriptmanager" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
<style>
    .ft_sort
    {
        background: #999999 none repeat scroll 0 0;
        border: 0 none;
        border-radius: 12px;
        box-shadow: 0 1.5px 2px #bfbfbf inset;
        color: #ffffff;
        cursor: pointer;
        display: block;
        font-size: 11.5px;
        font-style: normal;
        padding: 3px 12px;
        width: 60px;
    }
    .ft_sort:hover
    {
        background: #565656;
        color: #ffffff;
    }
    .divs
    {
        background-color: #EEEEEE;
        margin-bottom: .5%;
        margin-top: .5%;
        margin-left: .2%;
        margin-right: .1%;
        border: solid 1.5px #EEEEEE;
    }
    .divs:hover
    {
        border: solid 1.5px #00CEFF;
        cursor: pointer;
    }
    .dottedBottom
    {
        border-bottom-style: inset;
        border-bottom-width: thin;
        margin-bottom: 1%;
        border-collapse: collapse;
        border-spacing: 10px;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                        <asp:Label ID="lblHeader" runat="server"></asp:Label>
                        </td>
                        <td style="float: right; margin-right: 50px">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="upMFOrderBook" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <div id="demo" class="row" style="margin-left: 5%; margin-right: 5%; padding-top: 1%;
            height: 20%">
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-md-3">
                    AMC:
                    <asp:DropDownList ID="ddlAMC" runat="server" CssClass="form-control input-sm" Width="100%"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlAMC_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ID="rfvAMC" runat="server" ControlToValidate="ddlAMC" InitialValue="0" ErrorMessage="Please Select AMC" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-4">
                    Scheme:
                    <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control input-sm"
                        class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Transaction Type:
                    <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control input-sm"
                        class="form-control">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                        <asp:ListItem Text="New Purchase" Value="BUY"></asp:ListItem>
                        <asp:ListItem Text="Additional Purchase" Value="ABY"></asp:ListItem>
                        <asp:ListItem Text="Redeem" Value="SEL"></asp:ListItem>
                    </asp:DropDownList>
                    <%--  <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvAmc" runat="server" CssClass="rfvPCG" ErrorMessage="Please Select an AMC"
                Display="Dynamic" ControlToValidate="ddlAction" InitialValue="0" ValidationGroup="btnViewSIP">Select Transaction Type</asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-1" style="margin-top: 1.8%">
                    <asp:Button ID="btnViewSIP" runat="server" CssClass="btn btn-primary btn-primary"
                        Text="Go" ValidationGroup="btnViewSIP" OnClick="btnViewOrder_Click" />
                </div>
                <div class="col-md-1" style="margin-top: 1.8%">
                    <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                        Height="25px" Width="29px"></asp:ImageButton>
                </div>
            </div>
        </div>
        <div id="Div1" class="row" style="margin-left: 5%; margin-right: 5%; background-color: #2480C7;"
            visible="false" runat="server">
            <telerik:RadGrid ID="gvOrderBook" runat="server" GridLines="None" AllowPaging="True"
                PageSize="5" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                HorizontalAlign="NotSet" CellPadding="15" OnNeedDataSource="gvOrderBook_OnNeedDataSource"
                OnItemDataBound="gvOrderBook_ItemDataBound">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="CO_OrderId" AllowCustomSorting="true">
                    <Columns>
                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <%--  <div class="col-md-2">
                            <asp:Button ID="btnAMC" runat="server" CssClass="ft_sort" Text="AMC" /></div>
                        <div class="col-md-2">
                            <asp:Button ID="Button1" runat="server" CssClass="ft_sort" Text="Scheme" /></div>--%>
                                <%--<div class="col-md-4">
                            <asp:Button ID="Button2" runat="server" CssClass="ft_sort" Text="Order Status" Width="100px" /></div>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12 divs">
                                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 fk-font-7" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Scheme Name:</b> </font>
                                        <%# Eval("PASP_SchemePlanName")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Category:</b></font>
                                        <%# Eval("PAIC_AssetInstrumentCategoryName")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Order No.:</b></font>
                                        <%# Eval("CO_OrderId")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Request Date:</b></font>
                                        <%# Eval("CO_OrderDate")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Order Type:</b></font>
                                        <%# Eval("WMTT_TransactionType")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Dividend Type:</b></font>
                                        <%# Eval("CMFOD_DividendOption")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Amount:</b></font>
                                        <%# Eval("CMFOD_Amount")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Units:</b></font>
                                        <%# Eval("CMFOD_Units")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Actioned NAV:</b></font>
                                        <%# Eval("CMFT_Price")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Redeem All:</b></font>
                                        <%# Eval("CMFOD_IsAllUnits")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Order Status:</b></font>
                                        <%# Eval("XS_Status")%>
                                    </div>
                                   
                                    <div id="DivBSE" runat="server" class="col-xs-9 col-sm-9 col-md-9 col-lg-9 fk-font-9" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Exchange Order No.:</b></font>
                                        <%# Eval("BMOERD_BSEOrderId")%>
                                    </div>
                                    <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9 fk-font-9" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Reject Remark:</b></font>
                                        <%# Eval("COS_Reason")%>
                                    </div>
                                    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2 fk-font-2" style="margin-bottom: 1.5px;">
                                        <asp:Button ID="btnDetails" runat="server" class="ft_sort btn-sm btn-info" Text="Transaction Details"
                                            OnClick="btnDetails_OnClick" Width="120px"></asp:Button>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="100%">
                                        <telerik:RadGrid ID="gvChildDetails" runat="server" AllowPaging="false" AllowSorting="True"
                                            AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false"
                                            AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                                            CellPadding="15" Visible="false">
                                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                                Width="100%" ShowHeader="false">
                                                <Columns>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle />
                                                        <ItemTemplate>
                                                            <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12">
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Customer Name:</b></font>
                                                                    <%# Eval("Name")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Folio Number:</b> </font>
                                                                    <%# Eval("CMFA_FolioNum")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Actioned NAV:</b></font>
                                                                    <%# Eval("CMFT_Price")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Transaction Date:</b></font>
                                                                    <%# Eval("CMFT_TransactionDate")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Amount (Rs):</b></font>
                                                                    <%# Eval("CMFT_Amount")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Units:</b></font>
                                                                    <%# Eval("CMFT_Units")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Dividend Type:</b></font>
                                                                    <%# Eval("CMFOD_DividendOption")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Maturity Date:</b></font>
                                                                    <%# Eval("CMFT_ELSSMaturityDate")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                                                    visibility: hidden">
                                                                    <font color="#565656"><b>Fund Name:</b></font>
                                                                    <%# Eval("PA_AMCName")%>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableAlternatingItems="false">
                </ClientSettings>
                <PagerStyle Mode="NextPrevAndNumeric" />
            </telerik:RadGrid>
        </div>

        <script>
            $(document).ready(function() {
                $(".btn-primary").click(function() {
                    $(".collapse").collapse('toggle');
                });

            });
        </script>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
</asp:UpdatePanel>
