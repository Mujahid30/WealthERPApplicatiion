<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionStructureRuleGrid.ascx.cs" Inherits="WealthERP.CommisionManagement.CommissionStructureRuleGrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
</style>


<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>
    <table width="100%">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                Commission Structure Grid
                            </td>
                            <td align=right>
                                <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExportSummary_OnClick"
                                    Height="25px" Width="25px"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
                
        <table width="100%">
            <tr id="trStepOneHeading" runat="server">
                <td class="tdSectionHeading">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">                        
                        <div class="fltlft">                           
                            <asp:Label ID="Label2" runat="server" Text="Filter Section"></asp:Label>
                        </div>                       
                    </div>
                </td>
            </tr>
        </table>
        
        <table width="100%">
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblProduct" runat="server" Text="Product:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddProduct" runat="server" CssClass="cmbField" 
                        onselectedindexchanged="ddProduct_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvProduct" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select a product" Display="Dynamic" ControlToValidate="ddProduct"
                        InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblIssuer" runat="server" Text="Issuer :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddIssuer" runat="server" CssClass="cmbField" AutoPostBack = "false"></asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvIssuer" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select an issuer" Display="Dynamic" ControlToValidate="ddIssuer"
                        InitialValue="Select" ValidationGroup="vgBtnSubmitStage2"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select a category" Display="Dynamic" ControlToValidate="ddProduct"
                        InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddSubCategory" runat="server" CssClass="cmbField" AutoPostBack="false" 
                        Enabled="False">
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddStatus" runat="server" CssClass="cmbField" AutoPostBack="false">
                        <asp:ListItem Value="All">All</asp:ListItem>
                        <asp:ListItem Value="Active">Active</asp:ListItem>
                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                    </asp:DropDownList>
                </td>           
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    <asp:Button ID="btnGo" runat="server" Text="Go" onclick="btnGo_Click" 
                        CssClass="PCGButton" />
                </td>
            </tr>
        </table>
        
        <table id="tblCommissionStructureRule" runat="server" width="100%">
            <tr id="trStepTwoHeading" runat="server">
                <td class="tdSectionHeading">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="fltlft" style="width: 200px;">
                            <asp:Label ID="lblStage" runat="server" Text="Structure Details"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="gvCommMgmt" AllowSorting="false" runat="server" AllowAutomaticInserts="false"
                                        EnableLoadOnDemand="True" AllowPaging="True" AutoGenerateColumns="False"
                                        EnableEmbeddedSkins="false" GridLines="none" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                                        EnableViewState="true" ShowStatusBar="true" Skin="Telerik" 
                                        onpageindexchanged="gvCommMgmt_PageIndexChanged" 
                                        OnNeedDataSource="gvCommMgmt_OnNeedDataSource" 
                                        onitemdatabound="gvCommMgmt_ItemDataBound">
                                        
                                        <HeaderContextMenu EnableEmbeddedSkins="False"></HeaderContextMenu>

                                        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="CommissionStructureRule"></ExportSettings>
                                        <PagerStyle AlwaysVisible="True" />
                                        <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" 
                                            AutoGenerateColumns="false" Width="100%">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" 
                                                    HeaderStyle-Width="100px" UniqueName="Action">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddAction" runat="server" OnSelectedIndexChanged="ddAction_OnSelectedIndexChanged" 
                                                            AutoPostBack="true" CssClass="cmbField" EnableEmbeddedSkins="false" Width="80px" >
                                                            <%--<Items>
                                                                <asp:ListItem Selected="true" Text="Select" Value="Select" />
                                                                <asp:ListItem Text="View Details" Value="viewDetails" />
                                                                <asp:ListItem Text="View Mapped Schemes" Value="viewMappedSchemes" />
                                                            </Items>--%>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Name" HeaderStyle-Width="160px" 
                                                    HeaderText="Name" UniqueName="cmRuleName">
                                                    <%--<HeaderStyle Width="100px" />--%>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="160px" 
                                                        Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Product" HeaderStyle-Width="100px" 
                                                    HeaderText="Product" UniqueName="cmProdType">
                                                    <%--<HeaderStyle Width="100px" />--%>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                        Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Issuer" HeaderStyle-Width="200px" 
                                                    HeaderText="Issuer" UniqueName="cmIssuer">
                                                    <%--<HeaderStyle Width="200px" />--%>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="200px" 
                                                        Wrap="true" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Category" HeaderStyle-Width="100px" 
                                                    HeaderText="Category" ShowFilterIcon="false" UniqueName="cmCategory">
                                                    <%--<HeaderStyle Width="100px" />--%>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                        Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="SubCategory" 
                                                    HeaderStyle-Width="150px" HeaderText="SubCategory" ShowFilterIcon="false" 
                                                    UniqueName="cmSubCategory">
                                                    <%--<HeaderStyle Width="150px" />--%>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" 
                                                        Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridDateTimeColumn DataField="ValidFrom" 
                                                    DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" 
                                                    HeaderText="Valid From" SortExpression="ValidFrom" UniqueName="cmValidFrom">
                                                    <%--<HeaderStyle Width="100px" />--%>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                        Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridDateTimeColumn DataField="ValidTill" 
                                                    DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" 
                                                    HeaderText="Valid Till" UniqueName="cmValidTill">
                                                    <%--<HeaderStyle Width="100px" />--%>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                        Wrap="false" />
                                                </telerik:GridDateTimeColumn>
                                            </Columns>
                                            <PagerStyle AlwaysVisible="True" />
                                            <%--<HeaderStyle Width="100px" />--%>
                                        </MasterTableView>                                        
                                        <ClientSettings>
                                            <%--<Scrolling AllowScroll="true" UseStaticHeaders="True" ScrollHeight="200px"></Scrolling>--%>
                                            <ClientEvents OnGridCreated="GridCreated" />
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                            <Resizing AllowColumnResize="true" />                                          
                                        </ClientSettings>
                                        <FilterMenu EnableEmbeddedSkins="False"></FilterMenu>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
    </Triggers>
</asp:UpdatePanel>