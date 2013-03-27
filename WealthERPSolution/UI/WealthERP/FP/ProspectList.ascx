<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProspectList.ascx.cs"
    Inherits="WealthERP.FP.ProspectList" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>

<script type="text/javascript">
    function onToolBarClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "DeleteSelected") {
            args.set_cancel(!confirm('Delete all selected customers?'));
        }
    }
</script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Customer Networth MIS
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnNetworthMIS" runat="server" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                Height="20px" Width="25px" OnClick="btnNetworthMIS_Click" Visible="false"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Customer/Prospect MIS"></asp:Label>
<hr />--%>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgNoRecords" runat="server" class="failure-msg" align="center" visible="false">
                No Records Found
            </div>
        </td>
    </tr>
</table>
<table id="tblAdviserBM" runat="server" width="80%">
    <tr>
        <td align="right" style="width: 5%">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
        </td>
        <td style="width: 15%" align="left">
            <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right" style="width: 5%">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
        </td>
        <td style="width: 15%" align="left">
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true"
                Style="vertical-align: middle">
            </asp:DropDownList>
        </td>
        <td align="right" style="width:10%">
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" OnClick="btnGo_Click" />
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
        </td>
    </tr>
</table>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="98%" EnableHistory="True"
    HorizontalAlign="NotSet" LoadingPanelID="PorspectListLoading">
    <telerik:RadGrid ID="gvCustomerProspectlist" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="95%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="Networth MIS" OnNeedDataSource="gvCustomerProspectlist_OnNeedDataSource">
        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" DataKeyNames="C_CustomerId">
            <CommandItemSettings ExportToPdfText="Export to Pdf" />
            <Columns>
                <telerik:GridTemplateColumn DataField="Name" AllowFiltering="true" UniqueName="Name"
                    HeaderText="Customer" SortExpression="Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderTemplate>
                        <asp:Label ID="lblNAme" runat="server" Text=""></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <%-- <asp:LinkButton ID="lnkbtnGvProspectListName" Style="float: left" runat="server"
                            OnClick="lnkbtnGvProspectListName_Click" Text='<%# Eval("Name").ToString() %>' ></asp:LinkButton>--%>
                              <asp:Label ID="lnkbtnGvProspectListName" runat="server"  Text='<%# Eval("Name").ToString() %>' ></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" 
                    UniqueName="Name">
                    <ItemStyle Width="" HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn DataField="IsProspect" HeaderText="Is Prospect" SortExpression="IsProspect"
                    UniqueName="IsProspect" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn DataField="C_Email" HeaderText="Email" SortExpression="C_Email"
                     UniqueName="C_Email">
                 <HeaderStyle Width="150Px"></HeaderStyle>
                 <ItemStyle Width="150Px" HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <%-- <telerik:GridBoundColumn DataField="C_Mobile1" HeaderText="Mobile" SortExpression="C_Mobile1"
                    UniqueName="C_Mobile1">
                     <HeaderStyle Width="150Px"></HeaderStyle>
                     <ItemStyle Width="150Px" HorizontalAlign="Right" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <%-- <telerik:GridBoundColumn DataField="Address" HeaderText="Address" SortExpression="Address"
                    UniqueName="Address"  >
                    <HeaderStyle Width="150Px"></HeaderStyle>
                    <ItemStyle Width="150Px" HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn DataField="Asset" HeaderText="Asset" SortExpression="Asset"
                    UniqueName="Asset" ShowFilterIcon="false" AllowFiltering="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum">
                    <HeaderStyle></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Liabilities" AllowFiltering="false" HeaderText="Liabilities"
                    SortExpression="Liabilities" UniqueName="Liabilities" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" DataFormatString="{0:N0}"
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                    <HeaderStyle></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Networth" AllowFiltering="false" HeaderText="Networth"
                    SortExpression="Networth" UniqueName="Networth" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum">
                    <HeaderStyle></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <%--<Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" FrozenColumnsCount="1">
            </Scrolling>--%>
            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
            <ClientEvents />
        </ClientSettings>
    </telerik:RadGrid>
</telerik:RadAjaxPanel>
<table class="TableBackground" width="100%">
    <tr align="center">
        <td align="center" width="100%">
            <asp:Label ID="lblErrorMsg" runat="server" CssClass="failure-msg" Visible="false"
                Width="60%">
            </asp:Label>
        </td>
    </tr>
</table>
<table width="100%" style="text-align: left">
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNote" runat="server" CssClass="HeaderTextSmall" Text="Note: Data on the screen can be updated by clicking on finance profile synchronization button."></asp:Label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnAll" runat="server" />
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<%--    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:wealtherp %>"
        SelectCommand="SELECT [C_DOB], [C_Email], [C_FirstName], [C_CustomerId],[C_Mobile1],[C_Adr1Line1]+','+[C_Adr1Line2]+','+[C_Adr1Line3] AS Address FROM [Customer] WHERE (([C_IsProspect] = @C_IsProspect) AND [AR_RMId]=@AR_RMId)">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="C_IsProspect" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="C_IsFPClient" Type="Int32" />   
             <asp:Parameter Name="AR_RMId" Type="Int32" />           
        </SelectParameters>
    </asp:SqlDataSource>--%>
<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:wealtherp %>"
        SelectCommand="SELECT A.C_CustomerId, A.C_FirstName+' '+A.C_MiddleName+' '+A.C_LastName AS Name
,A.C_Email
,A.C_Mobile1
,ISNULL(A.C_Adr1Line1,'')+','+ISNULL(A.C_Adr1Line2,'')+','+ISNULL(A.C_Adr1Line3,'')+','+ISNULL(A.C_Adr1City,'')+','+ISNULL(A.C_Adr1State,'')+','+ISNULL(A.C_Adr1Country,'')+','+ (CASE WHEN A.C_Adr1PinCode = 0 then '' else CONVERT(varchar(10),A.C_Adr1PinCode) end) AS Address
,ISNULL(B.CFPS_Asset,0) AS Asset
,ISNULL(B.CFPS_Liabilities,0) AS Liabilities
, ISNULL(B.CFPS_Asset,0)-ISNULL(B.CFPS_Liabilities,0) AS Networth 
FROM dbo.Customer A
        LEFT OUTER JOIN dbo.CustomerFPSummary B
         ON A.C_CustomerId=B.C_CustomerId LEFT OUTER JOIN CustomerAssociates AS ca ON A.C_CustomerId = ca.C_CustomerId WHERE ([C_IsProspect] = @C_IsProspect AND [AR_RMId]=@AR_RMId AND ca.XR_RelationshipCode='SELF')">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="C_IsProspect" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="C_IsFPClient" Type="Int32" />   
             <asp:Parameter Name="AR_RMId" Type="Int32" />           
        </SelectParameters>
    </asp:SqlDataSource>--%>
<%--    <asp:SqlDataSource ID="SqlDataSourceCustomerRelation" runat="server" ConnectionString="<%$ ConnectionStrings:wealtherp %>"
        SelectCommand="SP_GetCustomerRelation" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>