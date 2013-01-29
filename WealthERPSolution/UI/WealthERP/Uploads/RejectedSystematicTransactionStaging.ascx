<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedSystematicTransactionStaging.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedSystematicTransactionStaging" %>
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
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Systematic Transaction Staging
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="LinkButton1" CssClass="LinkButtons" Text="Back To Upload Log"
                                OnClick="lnkBtnBackToUploadLog_Click"></asp:LinkButton>
                        </td>
                        <td>
                         <asp:LinkButton ID="LinkInputRejects" runat="server" Text="View Input Rejects" Visible="false"
                             CssClass="LinkButtons" OnClick="LinkInputRejects_Click"></asp:LinkButton>
                             </td>
                         <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Systematic Transaction Staging"></asp:Label>--%>

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

<div id="divConditional" runat="server">
    <table class="TableBackground" cellspacing="0" cellpadding="2">
        <tr>
            <%--  <td id="tdLblAdviser" runat="server" align="right">
            <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
        </td>
       <td id="tdDdlAdviser" runat="server" align="left">
     <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlAdviser_OnSelectedIndexChanged" ></asp:DropDownList>
     </td>--%>
            <td id="tdlblRejectReason" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Select reject reason :" ID="lblRejectReason"></asp:Label>
            </td>
            <td id="tdDDLRejectReason" runat="server">
                <asp:DropDownList CssClass="cmbField" ID="ddlRejectReason" runat="server">
                </asp:DropDownList>
            </td>
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromSIP" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server">
                <telerik:RadDatePicker ID="txtFromSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="dvTransactionDate" runat="server" class="dvInLine">
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtSIPDate" ControlToValidate="txtFromSIP" ErrorMessage="<br />Please select a From Date"
                        CssClass="cvPCG" Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtFromSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtToSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="Div1" runat="server" class="dvInLine">
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToSIP"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtToSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToSIP"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtFromSIP" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
                </asp:CompareValidator>
            </td>
            <td id="tdBtnViewRejetcs" runat="server">
                <asp:Button ID="btnViewSIP" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewSIP_Click" />
            </td>
        </tr>
    </table>
</div>
<%--<table style="width: 100%" class="TableBackground">
    <tr>
        <td>
            <%--var totalChkBoxes = parseInt('<%= gvSIPReject.Rows.Count %>');--%>
            <%-- <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back To Upload Log"
                OnClick="lnkBtnBackToUploadLog_Click"></asp:LinkButton>
       &nbsp;&nbsp;&nbsp;--%>
           <%-- <asp:LinkButton runat="server" ID="lnkBtnBackToUploadGrid" Visible="false" CssClass="LinkButtons"
                Text="Back" OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="LinkInputRejects" runat="server" Text="View Input Rejects" Visible="false"
                CssClass="LinkButtons" OnClick="LinkInputRejects_Click"></asp:LinkButton>
        </td>
    </tr>
</table>--%>
<table width="100%" cellspacing="" cellpadding="2">
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
            <div id="msgDelete" runat="server" class="success-msg" align="center" visible="false">
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
<asp:Panel ID="Panel3" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
 <table width="100%" cellspacing="0" cellpadding="2">
        <%--<tr>
            <%--<td>
                <asp:LinkButton runat="server" ID="lnkViewInputRejects" Text="View Input Rejects"
                    CssClass="LinkButtons" OnClick="lnkViewInputRejects_OnClick"></asp:LinkButton>
            </td>--%>
        <%-- </tr>--%>
        <tr>
            <td>
                <telerik:RadGrid ID="gvSIPReject" runat="server" GridLines="None" AutoGenerateColumns="False"
                    AllowFiltering="true" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                    Skin="Telerik" OnItemDataBound="gvSIPReject_ItemDataBound" EnableEmbeddedSkins="false"
                    Width="80%" AllowFilteringByColumn="true" AllowAutomaticInserts="false" EnableViewState="true"
                    ShowFooter="true" OnNeedDataSource="gvSIPReject_NeedDataSource"
                    OnPreRender="gvSIPReject_PreRender">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView TableLayout="Auto" DataKeyNames="CMFSCS_ID,WUPL_ProcessId,CMFSCS_FolioNum"
                        AllowFilteringByColumn="true" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                                HeaderStyle-Width="50px">
                                <HeaderTemplate>
                                    <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                    <%--<asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("WERPTransactionId").ToString()%>' />--%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="WRR_RejectReasonDescription" AllowFiltering="true"
                                HeaderText="RejectReason" HeaderStyle-Width="300px" UniqueName="WRR_RejectReasonCode"
                                SortExpression="RejectReason" AutoPostBackOnFilter="false" ShowFilterIcon="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxRR" Width="290px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents1_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("WRR_RejectReasonCode").CurrentFilterValue %>'
                                        runat="server">
                                        <%--OnPreRender="rcbContinents_PreRender"--%>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                        <script type="text/javascript">
                                            function InvesterNameIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                //////                                                    sender.value = args.get_item().get_value();
                                                tableView.filter("WRR_RejectReasonCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WUPL_ProcessId" AllowFiltering="true" HeaderText="ProcessId"
                                HeaderStyle-Width="60px" UniqueName="WUPL_ProcessId" SortExpression="WUPL_ProcessId"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="8px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ADUL_FileName" HeaderText="File Name" HeaderStyle-Width="60px"
                                UniqueName="ADUL_FileName" SortExpression="ADUL_FileName" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SystematicType" AllowFiltering="false" HeaderText="Source Type"
                                HeaderStyle-Width="75px" UniqueName="SystematicType" SortExpression="SystematicType"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_InvName" AllowFiltering="true" HeaderText="Investor Name"
                                HeaderStyle-Width="85px" UniqueName="CMFSCS_InvName" SortExpression="CMFSCS_InvName"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_FolioNum" HeaderText="Folio No." AllowFiltering="true"
                                HeaderStyle-Width="80px" UniqueName="CMFSCS_FolioNum" SortExpression="CMFSCS_FolioNum"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                <%--<FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxFolioN" Width="90px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFolioNumber_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents5_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("CMFSCS_FolioNum").CurrentFilterValue %>'
                                        runat="server">
                                        <%--OnPreRender="rcbContinents_PreRender"--%>
                                <%-- <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock5" runat="server">

                                        <script type="text/javascript">
                                            function FolioIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("CMFSCS_FolioNum", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>--%>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_SchemeName" ShowFilterIcon="false" FilterControlWidth="250px"
                                AllowFiltering="true" HeaderStyle-Width="250px" HeaderText="Scheme" UniqueName="CMFSCS_SchemeName"
                                CurrentFilterFunction="Contains" SortExpression="CMFSCS_SchemeName" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <%-- <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxSN" Width="290px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSchemeName_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents6_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("CMFSCS_SchemeName").CurrentFilterValue %>'
                                        runat="server">
                                        <%--OnPreRender="rcbContinents_PreRender"--%>
                                <%-- <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock6" runat="server">

                                        <script type="text/javascript">
                                            function SCNIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("CMFSCS_SchemeName", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>--%>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_SystematicCode" AllowFiltering="true"
                                HeaderText="Transaction Type" HeaderStyle-Width="100px" UniqueName="CMFSCS_SystematicCode"
                                SortExpression="TransactionType" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxTT" Width="80px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents7_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("CMFSCS_SystematicCode").CurrentFilterValue %>'
                                        runat="server">
                                        <%--OnPreRender="rcbContinents_PreRender"--%>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock7" runat="server">

                                        <script type="text/javascript">
                                            function TransactionIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("CMFSCS_SystematicCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_StartDate" HeaderText="Start Date" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSCS_StartDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSCS_StartDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_ToDate" HeaderText="End Date" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSCS_ToDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSCS_ToDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField=" CMFSCS_SystematicDate" HeaderText="Systematic Date"
                                AllowFiltering="false" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression=" CMFSCS_SystematicDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSCS_SystematicDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XF_Frequency" HeaderText="Frequency" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="XF_Frequency"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="XF_Frequency" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" DataField="CMFSCS_Amount"
                                AllowFiltering="false" HeaderStyle-Width="75px" HeaderText="Amount" UniqueName="CMFSCS_Amount"
                                Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_TARGET_SCH" HeaderText="Target Scheme"
                                AllowFiltering="false" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSCS_TARGET_SCH"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSCS_TARGET_SCH" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="false" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<%--  <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
                    <table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:GridView ID="gvSIPReject" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ShowFooter="true" CssClass="GridViewStyle" AllowSorting="true" DataKeyNames="CMFSCS_ID,WUPL_ProcessId,CMFSCS_FolioNum">
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
                                                        runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRejectReasonHeader" runat="server" Text='<%# Eval("WRR_RejectReasonDescription").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHdrProcessId" runat="server" Text="Process Id"></asp:Label>
                                                    <asp:DropDownList ID="ddlProcessId" AutoPostBack="true" CssClass="cmbField" runat="server"
                                                        OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged">
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
                                                        OnSelectedIndexChanged="ddlFileName_SelectedIndexChanged">
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
<%--   </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSourceType" runat="server" Text='<%# Eval("SystematicType").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblInvName" runat="server" Text="Investor Name"></asp:Label><br />
                                                    <asp:DropDownList ID="ddlInvName" AutoPostBack="true" CssClass="cmbLongField" runat="server"
                                                        OnSelectedIndexChanged="ddlInvName_SelectedIndexChanged">
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
                                                        OnSelectedIndexChanged="ddlFolioNumber_SelectedIndexChanged">
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
<%-- <asp:TemplateField>
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
                                                        runat="server" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
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
                                                    <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>--%>
<%-- <asp:DropDownList ID="ddlTransactionType" AutoPostBack="true" CssClass="cmbField"
                                    runat="server" >
                                </asp:DropDownList>--%>
<%--  </HeaderTemplate>
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
                                            </asp:TemplateField>--%>
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
<%-- </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>--%>
<table width="100%">
    <div id="DivAction" runat="server" visible="false">
        <tr id="trReprocess" runat="server">
            <td class="SubmitCell">
                <asp:Button ID="btnReprocess" runat="server" Text="Reprocess" OnClick="btnReprocess_Click"
                    CssClass="PCGLongButton" OnClientClick="Loading(true);" />
                <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                    OnClick="btnDelete_Click" />
            </td>
        </tr>
    </div>
  <tr id="trMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyMsg" class="FieldName" visible="false">
                There are no records to be displayed!</label>
        </td>
    </tr>
   <%-- <tr id="trErrorMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Message" runat="server">
            </asp:Label>
        </td>
    </tr>--%>
</table>
<%--<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>--%>
<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnInvNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTransactionTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFileNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
