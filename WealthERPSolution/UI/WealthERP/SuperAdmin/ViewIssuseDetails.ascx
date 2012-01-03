<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewIssuseDetails.ascx.cs" Inherits="WealthERP.SuperAdmin.ViewIssuseDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>


<table width="1012px">
    <tr>
    <td class="HeaderCell">
        <asp:Label ID="lblPriceMonitoring" CssClass="HeaderTextBig" runat="server" Text="CSIssue Tracker"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
    <hr />
    </td>
    </tr>
    </table>
    
 <table class="TableBackground" width="100%">
 <tr align="center">
     <td>
        <table>
            <tr>
                 <td>
                    <asp:TextBox runat="server" ID="txtSearch"  CssClass="txtField"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="PCGButton" 
                        onclick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </td>
 </tr>
    <tr>
        <td>   
                   <telerik:RadGrid ID="gvCSIssueTracker" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true" 
                    AllowAutomaticInserts="false">
                   
                    <MasterTableView DataKeyNames="CSI_id,XMLCSS_Name,XMLCSL_Name" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                      <Columns>                         
                        <telerik:GridTemplateColumn HeaderText="Issue Code" AllowFiltering="false">
                            <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCSIssue" runat="server" CssClass="cmbField" Text='<%# Eval("CSI_Code") %>' OnClick="lnkCSIssue_Click">
                                        </asp:LinkButton>
                                </ItemTemplate>                         
                         </telerik:GridTemplateColumn>
                         
                        <telerik:GridBoundColumn DataField="XMLCSL_Name" AllowFiltering="false"  HeaderText="Active Level" UniqueName="ActiveLevel" >
                            <ItemStyle  Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />                            
                        </telerik:GridBoundColumn>
                         
                         <telerik:GridBoundColumn  DataField="A_OrgName" AllowFiltering="false" HeaderText="Org Name" UniqueName="OrgName">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         
                         <telerik:GridBoundColumn  DataField="CSI_CustomerDescription"  AllowFiltering="false" HeaderText="Description" UniqueName="Description">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="true" VerticalAlign="Top" />
                        </telerik:GridBoundColumn> 
                         
                        <telerik:GridBoundColumn  DataField="CSI_ReportedDate" HeaderText="Reported Date" UniqueName="ReportedDate" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn> 
                        
                        <telerik:GridBoundColumn  DataField="UR_RoleName" AllowFiltering="false"  HeaderText="Role Name" UniqueName="RoleName">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn> 
                        
                        <telerik:GridBoundColumn  DataField="WTN_TreeNodeText" AllowFiltering="false" HeaderText="TreeNodeText" UniqueName="TreeNodeText">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn> 
                         
                        <telerik:GridBoundColumn  DataField="XMLCSP_Name" AllowFiltering="false" HeaderText="Priority" UniqueName="Priority">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn  DataField="XMLCST_Name" AllowFiltering="false" HeaderText="Issue Type" UniqueName="IssueType">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn> 
                        
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="CSILA_Version" HeaderText="Release no" UniqueName="ReleaseVersion">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn> 
                        
                        <telerik:GridBoundColumn  HeaderText="Closed Date" DataField="CSI_ResolvedDate" UniqueName="ResolvedDate" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn> 
                        
                        <telerik:GridBoundColumn  HeaderText="status" AllowFiltering="false" DataField="XMLCSS_Name" UniqueName="status">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
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
