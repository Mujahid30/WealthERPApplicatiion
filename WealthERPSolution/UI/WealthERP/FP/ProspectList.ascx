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
<telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid1_NeedDataSource"
        ShowStatusBar="True" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
        Skin="Telerik" EnableEmbeddedSkins="false" OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound"
        AllowFilteringByColumn="True" DataSourceID="SqlDataSource1" 
        AllowAutomaticInserts="false">
        <MasterTableView AllowMultiColumnSorting="True" Width="100%" AutoGenerateColumns="false"
            DataSourceID="SqlDataSource1" DataKeyNames="C_CustomerId">
            <CommandItemSettings ExportToPdfText="Export to Pdf" />
            <Columns>
                <telerik:GridTemplateColumn UniqueName="comboProspectSelect" 
                    AllowFiltering="false">
                    <ItemTemplate>
                        <telerik:RadComboBox Skin="Telerik" EnableEmbeddedSkins="false" ID="ddlProspectList" AutoPostBack="true" runat="server" HighlightTemplatedItems="true"
                            OnSelectedIndexChanged="ddlProspectList_OnSelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="-Select-" Value="-Select-" />
                                <%--<telerik:RadComboBoxItem ImageUrl="../Images/Telerik/FP/DashBoard.png" Text="FPDashboard"
                                    Value="FPDashBoard" />--%>
                                <telerik:RadComboBoxItem ImageUrl="/Images/Telerik/FP/DashBoard.png" Text="Financial Profile"
                                    Value="FinancialPlanning" />
                                <telerik:RadComboBoxItem ImageUrl="/Images/Telerik/View.gif" Text="View Profile"
                                    Value="ViewProfile" />
                            </Items>
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" 
                    UniqueName="Name">
                    <ItemStyle Width="" HorizontalAlign="left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
               
               <%-- <telerik:GridDateTimeColumn UniqueName="DOB" PickerType="DatePicker" HeaderText="Date of Birth"
                    HeaderStyle-HorizontalAlign="Center" DataField="C_DOB" FooterText="DateTimeColumn footer"
                    DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd MMMM, yyyy">
                    <ItemStyle Width="120px" />
                </telerik:GridDateTimeColumn>--%>
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
                 <telerik:GridBoundColumn DataField="Asset" HeaderText="Asset" SortExpression="Asset"
                     UniqueName="Asset">
                     <ItemStyle Width="" HorizontalAlign="Right" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
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
<%--    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:wealtherp %>"
        SelectCommand="SELECT [C_DOB], [C_Email], [C_FirstName], [C_CustomerId],[C_Mobile1],[C_Adr1Line1]+','+[C_Adr1Line2]+','+[C_Adr1Line3] AS Address FROM [Customer] WHERE (([C_IsProspect] = @C_IsProspect) AND [AR_RMId]=@AR_RMId)">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="C_IsProspect" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="C_IsFPClient" Type="Int32" />   
             <asp:Parameter Name="AR_RMId" Type="Int32" />           
        </SelectParameters>
    </asp:SqlDataSource>--%>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:wealtherp %>"
        SelectCommand="SELECT A.C_CustomerId, A.C_FirstName+' '+A.C_MiddleName+' '+A.C_LastName AS Name
,A.C_Email
,A.C_Mobile1
,ISNULL(A.C_Adr1Line1,'')+','+ISNULL(A.C_Adr1Line2,'')+','+ISNULL(A.C_Adr1Line3,'')+','+ISNULL(A.C_Adr1City,'')+','+ISNULL(A.C_Adr1State,'')+','+ISNULL(A.C_Adr1Country,'')+','+ (CASE WHEN A.C_Adr1PinCode = 0 then '' else CONVERT(varchar(10),A.C_Adr1PinCode) end) AS Address
,ISNULL(B.CFPS_Asset,0) AS Asset
,ISNULL(B.CFPS_Liabilities,0) AS Liabilities
, ISNULL(B.CFPS_Asset,0)-ISNULL(B.CFPS_Liabilities,0) AS Networth 
FROM dbo.Customer A
        LEFT OUTER JOIN dbo.CustomerFPSummary B
         ON A.C_CustomerId=B.C_CustomerId WHERE ([C_IsProspect] = @C_IsProspect AND [AR_RMId]=@AR_RMId)">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="C_IsProspect" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="C_IsFPClient" Type="Int32" />   
             <asp:Parameter Name="AR_RMId" Type="Int32" />           
        </SelectParameters>
    </asp:SqlDataSource>
    <%--    <asp:SqlDataSource ID="SqlDataSourceCustomerRelation" runat="server" ConnectionString="<%$ ConnectionStrings:wealtherp %>"
        SelectCommand="SP_GetCustomerRelation" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
</telerik:RadAjaxPanel>
