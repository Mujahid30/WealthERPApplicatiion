<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedSystematicTransactionStaging.ascx.cs" Inherits="WealthERP.Uploads.RejectedSystematicTransactionStaging" %>
<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>--%>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

   <script src="Scripts/jquery-1.2.6.js" type="text/javascript"></script>
    <script src="Scripts/webtoolkit.jscrollable.js" type="text/javascript"></script>
    <script src="Scripts/webtoolkit.scrollabletable.js" type="text/javascript"></script> 
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Systematic Transaction Staging"></asp:Label>
<br />
<hr />
<br /><br /><br /><br /><br />

<script type="text/javascript">
    function pageLoad() {
        InitDialogs();
        Loading(false);
    }

    function UpdateImg(ctrl, imgsrc) {
        var img = document.getElementById(ctrl);
        img.src = imgsrc;
    }

    // sets up all of the YUI dialog boxes
    function InitDialogs() {
        DialogBox_Loading = new YAHOO.widget.Panel("waitBox",
	{ fixedcenter: true, modal: true, visible: false,
	    width: "230px", close: false, draggable: true
	});
        DialogBox_Loading.setHeader("Processing, please wait...");
        DialogBox_Loading.setBody('<div style="text-align:center;"><img src="/Images/Wait.gif" id="Image1" /></div>');
        DialogBox_Loading.render(document.body);
    }
    function Loading(b) {
        if (b == true) {
            DialogBox_Loading.show();
        }
        else {
            DialogBox_Loading.hide();
        }
    }
</script>
<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvSIPReject.Rows.Count %>');
        var gvControl = document.getElementById('<%= gvSIPReject.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
    </script>
    
    <table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessComplete" runat="server" class="success-msg" align="center"
                visible="false">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
</table>
 <table width="100%">
    <tr>
        <td align="center">
            <div id="msgDelete" runat="server" class="success-msg" align="center"
                visible="false">
                Records has been deleted successfully 
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessincomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Reprocess Failed!
            </div>
        </td>
    </tr>
</table>

<table style="width: 100%" class="TableBackground">
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back To Upload Log"
                OnClick="lnkBtnBackToUploadLog_Click"></asp:LinkButton>
       &nbsp;&nbsp;&nbsp;
         <asp:LinkButton runat="server" ID="lnkBtnBackToUploadGrid" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
</table>

   
  <%--      
    <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" Width="98%" EnableHistory="True"
    HorizontalAlign="NotSet" LoadingPanelID="RejectedSIPLoading">

<telerik:RadGrid ID="gvSIPReject" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="True" AllowPaging="True" HeaderStyle-Wrap="true" HeaderStyle-VerticalAlign="Top" 
        ShowStatusBar="True" ShowFooter="true" Width="100%"
        Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" 
        
        AllowAutomaticInserts="false">
        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false">
           
            <Columns>
            
             <telerik:GridTemplateColumn UniqueName="RejectCode" HeaderStyle-HorizontalAlign="Center" AllowFiltering="true"  DataField="RejectCode" HeaderText="Reject Reason" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkBx" runat="server" CssClass="CmbField" Text='<%# Eval("WRR_RejectReasonDescription").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>    
                        <asp:DropDownList CssClass="CmbField" ID="DropDownList1" runat="server"></asp:DropDownList>    
                    </EditItemTemplate>  
                </telerik:GridTemplateColumn>
                
          <telerik:GridTemplateColumn UniqueName="RejectCode" HeaderStyle-HorizontalAlign="Center" AllowFiltering="true"  DataField="RejectCode" HeaderText="Reject Reason" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblRejectCode" runat="server" CssClass="CmbField" Text='<%# Eval("WRR_RejectReasonDescription").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>    
                        <asp:DropDownList CssClass="CmbField" ID="ddlRejectCode" runat="server"></asp:DropDownList>    
                    </EditItemTemplate>  
                </telerik:GridTemplateColumn>
              
                
                
             <%--     <custom:MyCustomFilteringColumn DataField="Reject" FilterControlWidth="180px" HeaderText="Reject Reason">
                        <headerstyle width="25%" />
                        <itemtemplate>
                       <asp:Label ID="lblRejectCode" runat="server" CssClass="CmbField" Text='<%# Eval("WRR_RejectReasonDescription").ToString() %>'></asp:Label>
                        </itemtemplate>
                    </custom:MyCustomFilteringColumn>--%>
            <%--      <telerik:GridTemplateColumn UniqueName="ProcessId" HeaderStyle-HorizontalAlign="Center" AllowFiltering="true" HeaderStyle-Width="120px" DataField="SIPProcessId" HeaderText="ProcessId" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblProcessId" runat="server" CssClass="CmbField" Text='<%# Eval("WUPL_ProcessId").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ADUL_FileName" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"  DataField="File" HeaderText="File Name" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblADUL_FileName" runat="server" CssClass="CmbField" Text='<%# Eval("ADUL_FileName").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="SystematicType" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" DataField="SIPType" HeaderText="Source Type" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblSystematicType" runat="server" CssClass="CmbField" Text='<%# Eval("SystematicType").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="CMFSCS_InvName" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"  DataField="CMFSCS_InvName" HeaderText="Investor Name" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_InvName" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_InvName").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="CMFSCS_FolioNum" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" HeaderStyle-Width="90px" DataField="Cash" HeaderText="Folio Number" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_FolioNum" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_FolioNum").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="CMFSCS_PRODUCT" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" HeaderStyle-Width="90px" DataField="PRODUCT" HeaderText="Scheme Code" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_PRODUCT" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_PRODUCT").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="CMFSCS_SchemeName" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"  DataField="SchemeName" HeaderText="Scheme Plan Name" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_SchemeName" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_SchemeName").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                    <telerik:GridTemplateColumn UniqueName="CMFSCS_SystematicCode" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="120px"  DataField="SystematicCode" HeaderText="Systematic Type" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_SystematicCode" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_SystematicCode").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                    <telerik:GridTemplateColumn UniqueName="CMFSCS_StartDate" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px"  DataField="StartDate" HeaderText="Start Date" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_StartDate" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_StartDate").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                   <telerik:GridTemplateColumn UniqueName="CMFSCS_ToDate" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px"  DataField="ToDate" HeaderText="End Date" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_ToDate" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_ToDate").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                   <telerik:GridTemplateColumn UniqueName="CMFSCS_SystematicDate" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" DataField="SystematicDate" HeaderText="Systematic Date" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_SystematicDate" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_SystematicDate").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                   <telerik:GridTemplateColumn UniqueName="XF_Frequency" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="70px" DataField="XFFrequency" HeaderText="Frequency" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblXF_Frequency" runat="server" CssClass="CmbField" Text='<%# Eval("XF_Frequency").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
           
                 <telerik:GridTemplateColumn UniqueName="CMFSCS_Amount" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="70px" DataField="Amount" HeaderText="Amount" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_Amount" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_Amount").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="CMFSCS_TARGET_SCH" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" DataField="TARGET_SCH" HeaderText="Target Scheme" >
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblCMFSCS_TARGET_SCH" runat="server" CssClass="CmbField" Text='<%# Eval("CMFSCS_TARGET_SCH").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
           
              
            </Columns>
                
        </MasterTableView>
        <HeaderStyle Width="140px" VerticalAlign="Top" Wrap="false" />
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" EnableVirtualScrollPaging="false" SaveScrollPosition="true" FrozenColumnsCount="1">                
            </Scrolling>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
     </telerik:RadAjaxPanel>
          
   --%>
   
   
   <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td>
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="gvSIPReject" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ShowFooter="true" CssClass="GridViewStyle" AllowSorting="true" DataKeyNames="CMFSCS_ID,WUPL_ProcessId,CMFSCS_FolioNum"
                    >
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <HeaderTemplate>
                            <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                            <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                                <asp:DropDownList ID="ddlRejectReason" CssClass="cmbLongField" AutoPostBack="true"
                                    runat="server"  OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRejectReasonHeader"  runat="server" Text='<%# Eval("WRR_RejectReasonDescription").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrProcessId" runat="server" Text="Process Id"></asp:Label>
                                <asp:DropDownList ID="ddlProcessId" AutoPostBack="true" CssClass="cmbField" runat="server"
                                 OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged"   >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("WUPL_ProcessId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrFileName" runat="server" Text="File Name"></asp:Label>
                                <asp:DropDownList ID="ddlFileName" AutoPostBack="true" CssClass="cmbLongField" runat="server"
                                 OnSelectedIndexChanged="ddlFileName_SelectedIndexChanged"   >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("ADUL_FileName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrSourceType" runat="server" Text="Source Type"></asp:Label>
                            <%--    <asp:DropDownList ID="ddlSourceType" AutoPostBack="true" CssClass="cmbField" runat="server"
                                   >
                                </asp:DropDownList>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSourceType" runat="server" Text='<%# Eval("SystematicType").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblInvName" runat="server" Text="Investor Name"></asp:Label><br />
                                <asp:DropDownList ID="ddlInvName" AutoPostBack="true" CssClass="cmbLongField" runat="server"
                                 OnSelectedIndexChanged="ddlInvName_SelectedIndexChanged"    >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblInvNameData" runat="server" Text='<%# Bind("CMFSCS_InvName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrFolioNumber" runat="server" Text="Folio Number"></asp:Label>
                                <asp:DropDownList ID="ddlFolioNumber" AutoPostBack="true" CssClass="cmbField" runat="server"
                                 OnSelectedIndexChanged="ddlFolioNumber_SelectedIndexChanged"  >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFolioNumber" runat="server" Text='<%# Bind("CMFSCS_FolioNum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <asp:BoundField DataField="Scheme" HeaderText="Scheme" DataFormatString="{0:f4}"
                            ItemStyle-Wrap="false">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>--%>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrSchemeName" runat="server" Text="Scheme Name"></asp:Label>
                                <asp:DropDownList ID="ddlSchemeName" AutoPostBack="true" CssClass="cmbLongField"
                                    runat="server" OnSelectedIndexChanged="ddlSchemeName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSchemeName" runat="server" Text='<%# Bind("CMFSCS_SchemeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrTransactionType" runat="server" Text="Transaction Type"></asp:Label>
                                <asp:DropDownList ID="ddlTransactionType" AutoPostBack="true" CssClass="cmbField"
                                    runat="server" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionType" runat="server" Text='<%# Bind("CMFSCS_SystematicCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
            
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("CMFSCS_StartDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblToDate" runat="server" Text="End Date"></asp:Label>
                               
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblToDate" runat="server" Text='<%# Bind("CMFSCS_ToDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSystematicDate" runat="server" Text="Systematic Date"></asp:Label>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSystematicDate" runat="server" Text='<%# Bind("CMFSCS_SystematicDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblFrequency" runat="server" Text="Frequency"></asp:Label>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFrequency" runat="server" Text='<%# Bind("XF_Frequency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                               <%-- <asp:DropDownList ID="ddlTransactionType" AutoPostBack="true" CssClass="cmbField"
                                    runat="server" >
                                </asp:DropDownList>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("CMFSCS_Amount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblTARGET_SCH" runat="server" Text="Target Scheme"></asp:Label>
                               
                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTARGET_SCH" runat="server" Text='<%# Bind("CMFSCS_TARGET_SCH") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <%--<asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrTransactionType" runat="server" Text="Transaction Type"></asp:Label>
                                <asp:DropDownList ID="ddlTransactionType" AutoPostBack="true" CssClass="cmbField"
                                    runat="server" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionType" runat="server" Text='<%# Bind("XF_Frequency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
   
</table>
</asp:Panel>

<table width="100%">
 <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnReprocess"  runat="server" Text="Reprocess" OnClick="btnReprocess_Click"
                CssClass="PCGLongButton" OnClientClick="Loading(true);" />
           
                <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
               OnClick="btnDelete_Click"   />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyMsg" class="FieldName">
                There are no records to be displayed!</label>
        </td>
    </tr>
    <tr id="trErrorMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Message" runat="server">
            </asp:Label>
        </td>
    </tr>
    </table>
<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnInvNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTransactionTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFileNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />