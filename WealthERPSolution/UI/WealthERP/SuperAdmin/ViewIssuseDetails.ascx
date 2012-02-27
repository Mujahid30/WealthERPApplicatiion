<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewIssuseDetails.ascx.cs" Inherits="WealthERP.SuperAdmin.ViewIssuseDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">  
</telerik:RadScriptManager>



<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<style type="text/css">
/*CollapsiblePanel*/
.ContainerPanel
{    
	width:200px;
	border:2px;
	border-color:#FFFFFF;	
	border-style:double double double double;
}
.collapsePanelHeader
{
	width:200px;
	height:20px;
	background-image: url(images/bg-menu-main.png);
	background-repeat:repeat-x;
	color:#FFF;
	font-weight:bold;
}



.HeaderContent
{ 
    float:left;
	padding-left:5px;
    font-family: Verdana,Tahoma;
    font-weight: bold;
    font-size: small;
    color: #FFFFFF;	
}
.Content
{
	
}
.ArrowExpand
{
    vertical-align:middle;
	background-image: url(images/expand_blue.jpg);
	width:13px;
	height:13px;
	float:right;
	margin-top:3px;
	margin-right:5px;
}
.ArrowExpand:hover
{
	cursor:hand;
}
.ArrowClose
{
    vertical-align:middle;
	background-image: url(images/collapse_blue.jpg);
	width:13px;
	height:13px;
	float:right;
	margin-top:3px;
	margin-right:5px;
}
.ArrowClose:hover
{
	cursor:hand;
}
</style>
          <script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript">
            $(document).ready(function() {
                $("DIV.ContainerPanel > DIV.collapsePanelHeader > DIV.ArrowExpand").toggle(
                function() {
                    $(this).parent().next("div.Content").show("slow");
                    $(this).attr("class", "ArrowClose");
                },
                function() {
                    $(this).parent().next("div.Content").hide("slow");
                    $(this).attr("class", "ArrowExpand");
                });


            });            
        </script>

<script type="text/javascript">
    $(document).ready(function() {
        $('#ctrl_EquityReports_btnView').bubbletip($('#div1'), { deltaDirection: 'left' });
        $('#ctrl_EquityReports_btnViewInPDF').bubbletip($('#div2'), { deltaDirection: 'left' });
        $('#ctrl_EquityReports_btnViewInDOC').bubbletip($('#div3'), { deltaDirection: 'left' });
    });
</script>

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
        
   
   <%--<div> 
        <div id="ContainerPanel" class="ContainerPanel">
        <div id="header" class="collapsePanelHeader"> 
            <div id="dvHeaderText" class="HeaderContent">More Export Options</div>
            <div id="dvArrow" class="ArrowExpand"></div>
        </div>
        <div id="dvContent" class="Content" style="display:none">
            <asp:CheckBox ID="chkExportAll" Class="FieldName" Text="Export all pages" runat="server">
            </asp:CheckBox>    
                       
            <br />
            <br />
            <asp:Button ID="btnViewInExcel" CssClass="ExcelButton" runat="server"></asp:Button>&nbsp&nbsp&nbsp
            <div id="div1" style="display: none;">
                <p class="tip">
                    Click here to view equity report in Excel format.
                </p>
            </div>
            <asp:Button ID="btnViewInWord" CssClass="DOCButton" runat="server"></asp:Button>&nbsp&nbsp&nbsp
            <div id="div2" style="display: none;">
                <p class="tip">
                    Click here to view equity report in Word format.
                </p>
            </div>
            <asp:Button ID="btnViewInPdf" CssClass="PDFButton" runat="server"></asp:Button>&nbsp&nbsp&nbsp
            <div id="div3" style="display: none;">
                <p class="tip">
                    Click here to view equity report in Pdf format.
                </p>
            </div>
            <asp:Button ID="btnViewInCSV" CssClass="CSVButton" runat="server"></asp:Button>  
            <div id="div4" style="display: none;">
                <p class="tip">
                    Click here to view equity report in CSV format.
                </p>
            </div>          
            <br />
            <br />
        </div>
    </div>          
    
    </div>--%>
   
    
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
        <asp:Button runat="server" Text="Export filtered data to Excel" CssClass="PCGLongLongButton" OnClick="btnExportFilteredData_OnClick" ID="btnExportFilteredData" />
    </td>
 </tr>
    <tr>
        <td>   
                   <telerik:RadGrid ID="gvCSIssueTracker" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true" 
                    AllowAutomaticInserts="false" OnNeedDataSource="gvCSIssueTracker_OnNeedDataSource">
                    <ExportSettings ExportOnlyData="true" HideStructureColumns="true"></ExportSettings>
                    <MasterTableView DataKeyNames="CSI_id,XMLCSS_Name,XMLCSL_Name" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top">
                     <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                    ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
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
                         
                         
                         
                        <telerik:GridDateTimeColumn  DataField="CSI_ReportedDate" SortExpression="CSI_ReportedDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" AllowFiltering="true" HeaderText="Reported Date" UniqueName="ReportedDate" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                            <FilterTemplate>
                                <telerik:RadDatePicker ID="calFilter" runat="server"></telerik:RadDatePicker>
                            </FilterTemplate>
                        </telerik:GridDateTimeColumn> 
                        
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
                        
                        <telerik:GridDateTimeColumn  HeaderText="Closed Date" DataField="CSI_ResolvedDate" UniqueName="ResolvedDate" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                            <FilterTemplate>
                                <telerik:RadDatePicker ID="resolveDateFilter" AutoPostBack="true" runat="server"></telerik:RadDatePicker>
                            </FilterTemplate>
                        </telerik:GridDateTimeColumn> 
                        
                        <telerik:GridBoundColumn  HeaderText="status" SortExpression="XMLCSS_Name" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" AllowFiltering="true" DataField="XMLCSS_Name" UniqueName="status">
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
