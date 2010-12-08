<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="RMAMCSchemewiseMIS.ascx.cs"
    Inherits="WealthERP.Advisor.RMAMCSchemewiseMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Reference VirtualPath="~/Default.aspx" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">



    function checkdate(sender, args) {

        //create a new date var and set it to the
        //value of the senders selected date
        var selectedDate = new Date();
        selectedDate = sender._selectedDate;
        //create a date var and set it's value to today
        var todayDate = new Date();
        var mssge = "";

        if (selectedDate > todayDate) {

            //set the senders selected date to today
            sender._selectedDate = todayDate;
            //set the textbox assigned to the cal-ex to today
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            //alert the user what we just did and why
            alert("Warning! - Date Cannot be in the future. Date value is reset to the current date");
        }
    }


    function DownloadScript() {
        if (document.getElementById('<%= gvMFMIS.ClientID %>') == null) {
            alert("No records to export");
            return false;
        }
        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click();

    }

    function setPageType(pageType) {
        document.getElementById('<%= hdnDownloadPageType.ClientID %>').value = pageType;

    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
   

</script>

<style type="text/css">
    </style>
<table style="width: 100%; margin: 0px; padding: 0px;" cellpadding="1" cellspacing="1"">
    <tr>
        <td class="HeaderTextBig" colspan="3">
            <asp:Label ID="lblMfMIS" runat="server" CssClass="HeaderTextBig" Text="MF MIS"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table style="width: 62%; margin: 0px; padding: 0px;" cellpadding="1" cellspacing="1">
    <tr>
        <td align="right">
            <asp:Label ID="lblMISType" runat="server" CssClass="FieldName">MIS Type:</asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlMISType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMISType_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Value="AMCWiseAUM">AMC Wise AUM</asp:ListItem>
                <asp:ListItem Value="AMCSchemeWiseAUM" Selected="True">Scheme Wise AUM</asp:ListItem>
                <asp:ListItem Value="FolioWiseAUM">Folio Wise AUM</asp:ListItem>
                <asp:ListItem Value="TurnOverSummary">Turn Over Summary</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <span id="spnBranch" runat="server">
                <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                AutoPostBack="true">
                 <%--<asp:ListItem Value="1086" Text="All"></asp:ListItem>
                 <asp:ListItem Value="1145" Text="AJAY SINGH"></asp:ListItem>
                 <asp:ListItem Value="1058" Text="INVESTPRO FINANCIAL  SERV"></asp:ListItem> --%>
            </asp:DropDownList>
            </span>
        </td>
    </tr>
    <tr id="trRange" runat="server">
        <td align="right" valign="top">
            <asp:Label ID="lblDate" runat="server" CssClass="FieldName">As on Date:</asp:Label>
        </td>
        <td valign="top">
            <asp:TextBox ID="txtDate" runat="server" CssClass="txtField" Height="16px" Width="145px"></asp:TextBox>
            <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtDate"
                OnClientDateSelectionChanged="checkdate" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right" valign="top">
            <span id="spnRM" runat="server">
                <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" 
                AutoPostBack="true" onselectedindexchanged="ddlRM_SelectedIndexChanged">
            </asp:DropDownList>
            </span>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" ValidationGroup="btnGo" CssClass="PCGButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMAMCSchemewiseMIS_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMAMCSchemewiseMIS_btnGo', 'S');"
                OnClick="btnGo_Click" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
<table style="width: 100%; margin: 0px; padding: 0px;" cellpadding="0" cellspacing="0">
    <tr id="trModalPopup" runat="server">
        <td>
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();"
                PopupDragHandleControlID="Panel1" X="280" Y="35">
            </cc1:ModalPopupExtender>
        </td>
    </tr>
    <tr id="trExportPopup" runat="server">
        <td>
            <asp:Panel ID="Panel1" runat="server" CssClass="ExortPanelpopup">
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <input id="rbCurrent" runat="server" name="Radio" onclick="setPageType('single')"
                                type="radio" />
                        </td>
                        <td align="left">
                            <label for="rbCurrent" style="color: Black; font-family: Verdana; font-size: 8pt;
                                text-decoration: none">
                                Current Page</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <input id="rbAll" runat="server" name="Radio" onclick="setPageType('multiple')" type="radio" />
                        </td>
                        <td align="left">
                            <label for="rbAll" style="color: Black; font-family: Verdana; font-size: 8pt; text-decoration: none">
                                All Pages</label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                        </td>
                        <td align="left">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none"
                OnClick="btnExportExcel_Click" Height="31px" Width="30px" />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td colspan="3">
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found."></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <table style="width: 100%; border: none; margin: 0px; padding: 0px;" cellpadding="0"
                cellspacing="0">
                <tr>
                    <td>
                        <asp:ImageButton ID="imgBtnExport" ImageUrl="../App_Themes/Maroon/Images/Export_Excel.png"
                            runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                        <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="upnl" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvMFMIS" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CellPadding="4" CssClass="GridViewStyle" ShowFooter="True" DataKeyNames="SchemePlanCode"
                        OnSelectedIndexChanged="gvMFMIS_SelectedIndexChanged">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Details" />
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAMC" runat="server" Text="AMC"></asp:Label>
                                    <asp:TextBox ID="txtAMCSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMAMCSchemewiseMIS_btnSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAMCHeader" runat="server" Text='<%# Eval("AMC").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label>
                                    <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMAMCSchemewiseMIS_btnSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeHeader" runat="server" Text='<%# Eval("Scheme").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCategoryHeader" runat="server" Text='<%# Eval("Category").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MarketPrice" HeaderText="Curr NAV" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Wrap="false">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Wrap="false">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AUM" HeaderText="AUM" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Wrap="false">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Percentage" HeaderText="AUM %" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Wrap="false">
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
     <tr id="ValuationNotDoneErrorMsg" align="center" style="width: 100%" runat="server">
                    <td align="center" style="width: 100%">
                        <div class="failure-msg" style="text-align:center" align="center">
                            Valuation not done for this adviser....
                        </div>
                    </td>
                </tr>
</table>
<%--<table>
    <tr>
        <td>
            <asp:ImageButton ID="imgBtnExport" ImageUrl="../App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                OnClick="imgBtnExport_Click" Height="25px" Width="25px" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();"
                PopupDragHandleControlID="Panel1" X="280" Y="55">
            </cc1:ModalPopupExtender>
           
            
        </td>
    </tr>
    <tr id="Tr1" runat="server">
        <td>
        <asp:Panel ID="Panel1" runat="server" CssClass="ExortPanelpopup">
                 <br />               
                <table width="100%">
                <tr>
                <td>
                &nbsp;&nbsp;&nbsp;
                </td>
                <td align="right">
                <input id="rbtnSin" runat="server" name="Radio" onclick="setPageType('single')" type="radio" />                
                </td>
                <td align="left">
                 <label for="rbtnSin" style="color:Black;font-family:Verdana;font-size:8pt;text-decoration:none">
                    Current Page</label>
                </td>
                </tr>
                <tr>
                <td>
                &nbsp;&nbsp;&nbsp;
                </td>
                <td align="right">
                 <input id="Radio1" runat="server" name="Radio" onclick="setPageType('multiple')"
                    type="radio" />                  
                 </td>
                <td align="left">
                  <label for="Radio1" style="color:Black;font-family:Verdana;font-size:8pt;text-decoration:none">
                    All Pages</label>
                 </td>
                </tr>
                </table>
              
                <table width="100%">
                 <tr>
                <td align="right">
                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" />
                </td>
                <td align="left">
                  <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" />
                </td>
                </tr>
                </table>
                
         </asp:Panel>

        <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none"
                OnClick="btnExportExcel_Click" Height="31px" Width="35px" />
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblMISType" runat="server" CssClass="FieldName">&nbsp;&nbsp;&nbsp;MIS Type:</asp:Label>
                        <asp:DropDownList ID="ddlMISType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMISType_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Value="AMCWiseAUM">AMC Wise AUM</asp:ListItem>
                            <asp:ListItem Value="AMCSchemeWiseAUM" Selected="True">Scheme Wise AUM</asp:ListItem>
                            <asp:ListItem Value="FolioWiseAUM">Folio Wise AUM</asp:ListItem>
                            <asp:ListItem Value="TurnOverSummary">Turn Over Summary</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <span id="spnBranch" runat="server">
                <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </span>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr id="trRange" runat="server">
                    <td>
                        <asp:Label ID="lblDate" runat="server" CssClass="FieldName">As on Date:</asp:Label>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtDate"
                            OnClientDateSelectionChanged="checkdate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtDate" WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnGo">
                        </asp:RequiredFieldValidator><br />
                    </td>
                    <td valign="top">
                        <span id="spnRM" runat="server">&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
                <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField">
                </asp:DropDownList>
            </span>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPortfolio" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Portfolio:"
                            CssClass="FieldName" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" Visible="false">
                        </asp:DropDownList>
                        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnGo" runat="server" Text="Go" ValidationGroup="btnGo" CssClass="PCGButton"
                            onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AdviserMFMIS_btnGo', 'S');"
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AdviserMFMIS_btnGo', 'S');"
                            OnClick="btnGo_Click" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr id="trMessage" runat="server" visible="false">
                    <td colspan="3">
                        <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                        <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        <asp:UpdatePanel ID="upnl" runat="server">
         <ContentTemplate>
            <asp:GridView ID="gvMFMIS" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" ShowFooter="True" DataKeyNames="SchemePlanCode"
                OnSelectedIndexChanged="gvMFMIS_SelectedIndexChanged"> 
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Details" />
                    <asp:TemplateField HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblAMC" runat="server" Text="AMC"></asp:Label>
                            <asp:TextBox ID="txtAMCSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMAMCSchemewiseMIS_btnSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAMCHeader" runat="server" Text='<%# Eval("AMC").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label>
                            <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMAMCSchemewiseMIS_btnSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSchemeHeader" runat="server" Text='<%# Eval("Scheme").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                            <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">                                
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryHeader" runat="server" Text='<%# Eval("Category").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                   
                    <asp:BoundField DataField="MarketPrice" HeaderText="Curr NAV" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Units" HeaderText="Units" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="AUM" HeaderText="AUM" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Percentage" HeaderText="AUM %" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
         </ContentTemplate>
      </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
</table>--%>
<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:Button ID="btnSearch" runat="server" Text="" BorderStyle="None" BackColor="Transparent"
    OnClick="btnSearch_Click" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="ADUL_StartTime DESC" />
<asp:HiddenField ID="hdnAMCSearchVal" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeSearchVal" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCategoryFilter" runat="server" Visible="false" />
<asp:HiddenField ID="ValuationDate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />

<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnXWise" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnValuationDate" runat="server" Visible="false" />