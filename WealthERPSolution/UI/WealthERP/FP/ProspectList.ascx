<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProspectList.ascx.cs"
    Inherits="WealthERP.FP.ProspectList"  %>
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

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Prospect List"></asp:Label>
<hr />
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgNoRecords" runat="server" class="failure-msg" align="center" visible="false">
                No Records Found
            </div>
        </td>
    </tr>
</table>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="80%" EnableHistory="True"
    HorizontalAlign="NotSet" LoadingPanelID="PorspectListLoading">
<telerik:RadGrid ID="gvCustomerProspectlist" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="True" AllowPaging="True" 
        ShowStatusBar="True" ShowFooter="true" 
        Skin="Telerik" EnableEmbeddedSkins="false" 
        AllowFilteringByColumn="True" 
        AllowAutomaticInserts="false">
        <MasterTableView AllowMultiColumnSorting="True" Width="100%" AutoGenerateColumns="false"
            DataKeyNames="C_CustomerId" >
            <CommandItemSettings ExportToPdfText="Export to Pdf" />
            <Columns>
               
                <telerik:GridTemplateColumn UniqueName="Name" HeaderText="Name" >
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderTemplate>
                       <asp:Label ID="lblNAme" runat="server" Text="Name"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnGvProspectListName" style="float: left" runat="server" OnClick="lnkbtnGvProspectListName_Click"
                        Text='<%# Eval("Name").ToString() %>'></asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
               
                <%--<telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" 
                    UniqueName="Name">
                    <ItemStyle Width="" HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridBoundColumn DataField="IsProspect" HeaderText="Is Prospect" SortExpression="IsProspect" 
                    UniqueName="IsProspect">
                    <ItemStyle Width="" HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
              
                <telerik:GridBoundColumn DataField="C_Email" HeaderText="Email" SortExpression="C_Email"
                     UniqueName="C_Email">
                 <ItemStyle Width="" HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn DataField="C_Mobile1" HeaderText="Mobile" SortExpression="C_Mobile1"
                    UniqueName="C_Mobile1">
                     <ItemStyle Width="" HorizontalAlign="Right" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                                
                 <telerik:GridBoundColumn DataField="Address" HeaderText="Address" SortExpression="Address"
                    UniqueName="Address"  >
                    <ItemStyle Width="" HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridTemplateColumn UniqueName="Asset" HeaderText="Asset" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblAsset" runat="server" CssClass="CmbField" Text='<%# Eval("Asset").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    
                    <FooterTemplate>
                        <asp:Label ID="lblTotalAssets" runat="server" CssClass="CmbField" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                
                 <telerik:GridBoundColumn DataField="Liabilities" HeaderText="Liabilities" SortExpression="Liabilities"
                    UniqueName="Liabilities">
                     <ItemStyle Width="" HorizontalAlign="Right" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn DataField="Networth" HeaderText="Networth" SortExpression="Networth"
                    UniqueName="Networth">
                     <ItemStyle Width="" HorizontalAlign="Right" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                
            </Columns>
                
        </MasterTableView>
        
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
    </telerik:RadAjaxPanel>
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