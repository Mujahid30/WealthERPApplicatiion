<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResearchDashboard.ascx.cs" Inherits="WealthERP.Research.ResearchDashboard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager> 
<table class="TableBackground" style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="lblAttatchScheme" runat="server" CssClass="HeaderTextBig" Text="Research Dashboard"></asp:Label>            
            <hr />
        </td>
    </tr>
          
</table>

<table id="tableGrid" runat="server" class="TableBackground" width="75%">
<tr>
    <td>
     <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None" AllowPaging="True" 
    PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false" OnItemDataBound="RadGrid1_ItemDataBound"
    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="XAMP_ModelPortfolioCode">
        <MasterTableView DataKeyNames="XAMP_ModelPortfolioCode">
            <Columns>                
                <telerik:GridHyperLinkColumn  UniqueName="XAMP_ModelPortfolioName" 
                HeaderText="Model Portfolio" DataTextField="XAMP_ModelPortfolioName">
                <ItemStyle Wrap="True" Font-Underline="true"/>
                </telerik:GridHyperLinkColumn>

                <%--<telerik:GridBoundColumn UniqueName="XAMP_ModelPortfolioName" HeaderText="Variant" DataField="XAMP_ModelPortfolioName">
                
                </telerik:GridBoundColumn>--%>
                                
                <telerik:GridBoundColumn UniqueName="XAMP_ROR" HeaderText="ROR(%)" DataField="XAMP_ROR">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="XAMP_RiskPercentage" HeaderText="Risk(%)" DataField="XAMP_RiskPercentage">
                </telerik:GridBoundColumn>               
               
            </Columns>            
        </MasterTableView>
        <ClientSettings>
            <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
        </ClientSettings>
    </telerik:RadGrid>     
    </td>    
    </tr>

</table>
  