<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetMapping.ascx.cs" Inherits="WealthERP.Research.AssetMapping" %>

 <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
<telerik:RadMultiPage ID="AssetMappingId" 
    EnableViewState="true" runat="server" SelectedIndex="0">
<telerik:RadPageView ID="RadPageView1" runat="server">
        <asp:Panel ID="pnlAssetMapping" runat="server">
        <br />
      <table width="100%" class="TableBackground">
        <tr>
                <td class="HeaderCell">
                    <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Asset Mapping"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>  
        
      <table style="width:75%">
      <tr>
        <td>
            <asp:Label ID="lblAssetMapping" runat="server" CssClass="FieldName" Text="Asset classification to financial product mapping"></asp:Label>
            
        </td>
      </tr>
      <tr>
        <td></td>
      </tr>
        <tr>        
        <td>
        <div id="divScroll" runat="server" style="height:450px; overflow:scroll">
        <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid" GridLines="None"
        AllowSorting="True" AutoGenerateColumns="False" Skin="Telerik"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="False"
        OnItemCommand="RadGrid1_ItemCommand" AllowAutomaticUpdates="False" HorizontalAlign="NotSet"> 
        <MasterTableView CommandItemDisplay="None" >
        
            <columns>                           
                <telerik:GridBoundColumn UniqueName="Name" HeaderText="Name" DataField="Name" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Equity" HeaderText="Equity (%)" DataField="Equity" >
                </telerik:GridBoundColumn>    
                <telerik:GridBoundColumn UniqueName="Debt" HeaderText="Debt (%)" DataField="Debt" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Cash" HeaderText="Cash (%)" DataField="Cash" >
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn UniqueName="Alternate" HeaderText="Alternate (%)" DataField="Alternate" >
                </telerik:GridBoundColumn>          
            </Columns>           
        </MasterTableView>
        <ClientSettings>
            <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
        </ClientSettings> 
    </telerik:RadGrid>  
        </div>     
    </td>
        </tr>
        </table>   
      </asp:Panel>
   
    </telerik:RadPageView>
 </telerik:RadMultiPage>