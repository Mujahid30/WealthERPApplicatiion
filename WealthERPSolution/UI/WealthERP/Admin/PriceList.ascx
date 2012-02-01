<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PriceList.ascx.cs" Inherits="WealthERP.Admin.PriceList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<script language="javascript" type="text/javascript">
    function checkLastDate(sender, args) {

        var fromDateString = document.getElementById('ctrl_PriceList_txtFromDate').value;
        var fromDate = changeDate(fromDateString);
        var toDateString = document.getElementById('ctrl_PriceList_txtToDate').value;
        var toDate = changeDate(toDateString);
        var todayDate = new Date();

        if (Date.parse(toDate) < Date.parse(fromDate)) {
            //sender._selectedDate = todayDate;
            //sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            sender._textbox.set_Value('dd/mm/yyyy');
            alert("Warning! - ToDate cannot be less than the FromDate");
        }
    }
</script>--%>


<%-- <script type="text/javascript">
     //override the onload event handler to change the picker after the page is loaded
     Sys.Application.add_load(setCalendarTable);

     function setCalendarTable() {

         var picker = $find("<%= RadDatePicker1.ClientID %>");
         var calendar = picker.get_calendar();
         var fastNavigation = calendar._getFastNavigation();

         $clearHandlers(picker.get_popupButton());
         picker.get_popupButton().href = "javascript:void(0);";
         $addHandler(picker.get_popupButton(), "click", function() {
             var textbox = picker.get_textBox();
             //adjust where to show the popup table 
             var x, y;
             var adjustElement = textbox;
             if (textbox.style.display == "none")
                 adjustElement = picker.get_popupImage();

             var pos = picker.getElementPosition(adjustElement);
             x = pos.x;
             y = pos.y + adjustElement.offsetHeight;

             var e = {
                 clientX: x,
                 clientY: y - document.documentElement.scrollTop
             };
             //synchronize the input date if set with the picker one
             var date = picker.get_selectedDate();
             if (date) {
                 calendar.get_focusedDate()[0] = date.getFullYear();
                 calendar.get_focusedDate()[1] = date.getMonth() + 1;
             }

             $get(calendar._titleID).onclick(e);

             return false;
         });

         fastNavigation.OnOK =
                    function() {
                        var date = new Date(fastNavigation.Year, fastNavigation.Month, 1);
                        picker.get_dateInput().set_selectedDate(date);
                        fastNavigation.Popup.Hide();
                    }


         fastNavigation.OnToday =
                    function() {
                        var date = new Date();
                        picker.get_dateInput().set_selectedDate(date);
                        fastNavigation.Popup.Hide();
                    }
     }   
        </script>--%>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<div>
    <div>
        <%--<asp:ScriptManager ID="UploadScripManager" runat="server">
        </asp:ScriptManager>--%>
        
<table style="width: 100%">
    <tr>
        <td class="HeaderCell">
            <%--<label id="lblheader" class="HeaderTextBig" title="Upload Screen">
               MF Data Query</label>--%>
               <asp:Label ID="lblheader" runat="server" Class="HeaderTextBig"></asp:Label>
               
        </td>
    </tr>
</table>

<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" MultiPageID="FactsheetMultiPage" SelectedIndex="0">
    <Tabs>        
        <telerik:RadTab runat="server" Text="NAV" Value="Price" TabIndex="0" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server"  Text="Scheme Comparison" Value="Scheme_Comparison" TabIndex="1" Visible="true">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Factsheet" Value="Factsheet" TabIndex="2" >
        </telerik:RadTab>        
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
    EnableEmbeddedSkins="false">
</telerik:RadAjaxLoadingPanel>
 

<telerik:RadMultiPage ID="FactsheetMultiPage" EnableViewState="true"  runat="server" SelectedIndex="0">
    <telerik:RadPageView ID="RadPageView1" runat="server">
        
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Panel ID="pnlPrice" runat="server">
     <div id="MainDiv" runat="server">
        <table width="70%">
            <tr>
                <%--<td class="leftField">
                    <label id="Label1" class="FieldName" title=" Asset Group">
                        Asset Group:</label>
                </td>--%>
                <%--<td class="rightField">
                    <asp:DropDownList CssClass="cmbField" ID="ddlAssetGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetGroup_OnSelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Equity" Value="Equity"></asp:ListItem>
                        <asp:ListItem Text="MF" Value="MF"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvddlAssetGroup" runat="server" ControlToValidate="ddlAssetGroup"
                ErrorMessage="Please Select  Asset" Operator="NotEqual" ValueToCompare="0"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                </td>--%>
                <td align="right">
                    <asp:Label ID="lblIllegal" runat="server" CssClass="Error" Text="" />
                </td>
            </tr>
          
            
       
            <tr>
                <td align="right">
                 <asp:RadioButton ID="rbtnCurrent" runat="server" AutoPostBack="true" 
                        CssClass="cmbField" GroupName="Snapshot" 
                        OnCheckedChanged="rbtnCurrent_CheckedChanged" Text="Latest" />
                   <%-- <asp:RadioButton ID="rbtnHistorical" Text="Historical" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" CssClass="cmbField" OnCheckedChanged="rbtnHistorical_CheckedChanged" />--%>
                </td>
                <td align="left">
                <asp:RadioButton ID="rbtnHistorical" runat="server" AutoPostBack="true" 
                       CssClass="cmbField" GroupName="Snapshot" 
                       OnCheckedChanged="rbtnHistorical_CheckedChanged" Text="Historical" />
                </td>
                </tr>
               <tr> <td></td>  <td align="left">
                
                
                   <%-- <asp:RadioButton ID="rbtnCurrent" Text="Latest" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" OnCheckedChanged="rbtnCurrent_CheckedChanged" CssClass="cmbField" />--%>
                </td>
               
               
            </tr>
              <tr id="trFromDate" runat="server">
                <td align="right">
                    <label id="lblFromDate" runat="server" class="FieldName" title="FromDate">
                        FromDate:</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender 
                        ID="FrmDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                     <asp:CompareValidator ID="cvChkFutureDate" runat="server" 
                        ControlToValidate="txtFromDate" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="Date Can't be in future" Operator="LessThanEqual" Type="Date" 
                        ValidationGroup="vgbtnSubmit">
                  </asp:CompareValidator>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtFromDate" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="please enter from date" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>
                  
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtFromDate" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="Please Enter valid Date" 
                        ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
       
                    <%--<asp:RequiredFieldValidator ID="frmdatevalid" runat="server" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr id="trToDate" runat="server">
                <td align="right">
                    <label id="lblToDate" runat="server" class="FieldName" title="ToDate">
                        ToDate:</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox><ajaxToolkit:CalendarExtender 
                        ID="TDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>
                    <asp:CompareValidator ID="compDateValidator" runat="server" 
                        ControlToValidate="txtToDate" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="Date Can't be in future" Operator="LessThanEqual" Type="Date" 
                        ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                        runat="server" ControlToValidate="txtToDate" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="please enter to date" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>
                    <br />
                      
               <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                        ControlToValidate="txtToDate" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="Please Enter valid Date" 
                        ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
          <asp:CompareValidator ID="cvCompareDate" runat="server" ControlToCompare="txtFromDate" 
                        ControlToValidate="txtToDate" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="ToDate should be greater than FromDate" 
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="vgbtnSubmit">
                 </asp:CompareValidator>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            
            <tr id="trSelectMutualFund" runat="server">
                <td align="right">
                    <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" 
                        Text="Select AMC Code:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSelectMutualFund" runat="server" AutoPostBack="true" 
                        CssClass="cmbField" 
                        OnSelectedIndexChanged="ddlSelectMutualFund_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" 
                        ControlToValidate="ddlSelectMutualFund" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="Please Select AMC Code" Operator="NotEqual" 
                        ValidationGroup="vgbtnSubmit" ValueToCompare="Select AMC Code"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trNavCategory" runat="server">
            <td align="right" class="leftField">
             <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" 
                    Text="Category:"></asp:Label> 
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNAVCategory" runat="server" AutoPostBack="true" 
                                    CssClass="cmbField" 
                                    OnSelectedIndexChanged="ddlNAVCategory_OnSelectedIndexChanged">                                
                                </asp:DropDownList>
                            </td>
            </tr>
                <%--<tr id="trNavSubCategory" runat="server">
                <td align="right">
                    <asp:Label ID="lblNAVSubCategory" runat="server" CssClass="FieldName" 
                        Text="Sub Category:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNAVSubCategory" runat="server" AutoPostBack="true" 
                        CssClass="cmbField" 
                        OnSelectedIndexChanged="ddlNAVSubCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>--%>
             <tr id="trSelectSchemeNAV" runat="server">
                <td align="right">
                    <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" 
                        Text="Select Scheme Name:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSelectSchemeNAV" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ControlToValidate="ddlSelectSchemeNAV" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="Please Select Scheme" Operator="NotEqual" 
                        ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trbtnSubmit" runat="server">
            <td></td>
             <td>
             <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" 
                     OnClick="OnClick_Submit" Text="Submit" ValidationGroup="vgbtnSubmit" />
            </td>
            </tr>
        </table>
    </div>
    <div id="DivMF" runat="server" style="display: none">
        <table style="width: 100%">
            <tr id="trMfPagecount" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblMFCurrentPage" runat="server" class="Field"></asp:Label>
                    <asp:Label ID="lblMFTotalRows" runat="server" class="Field"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    &#160;
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr id="trgrMfView" runat="server">
                <td>
                    <asp:GridView ID="gvMFRecord" runat="server" AutoGenerateColumns="False" 
                        CssClass="GridViewStyle" Font-Size="Small" ShowFooter="true">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <%--<asp:BoundField DataField="SchemePlanName" HeaderText="Scheme Plan Name" SortExpression="PASP_SchemePlanName" />--%>
                            <asp:BoundField DataField="SchemePlanCode" HeaderText="Scheme Plan Code" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblName" runat="server" align="center" Text="Scheme Plan Name"></asp:Label>
                                    <br />
                                   <%-- <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />--%>
                                </HeaderTemplate>
                            
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeName" runat="server" 
                                        Text='<%# Eval("SchemePlanName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            
                            </asp:TemplateField>
                            <asp:BoundField DataField="NetAssetValue" HeaderText="Net AssetValue" />
                            <asp:BoundField DataField="RepurchasePrice" HeaderText="Repurchase Price" />
                            <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" />
                            <asp:BoundField DataField="PostDate" HeaderText="NAV Date" />
                            <%--<asp:BoundField DataField="Date" HeaderText="Date" />--%>
                        </Columns>
                    
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivEquity" runat="server" style="display: none">
        <table style="width: 100%">
            <tr>
                <td id="trPageCount" runat="server" class="leftField">
                    <asp:Label ID="lblCurrentPage" runat="server" class="Field"></asp:Label>
                    <asp:Label ID="lblTotalRows" runat="server" class="Field"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr id="trgvEquityView" runat="server">
                <td>
                    <asp:GridView ID="gvEquityRecord" runat="server" AutoGenerateColumns="False" 
                        CssClass="GridViewStyle" Font-Size="Small" ShowFooter="true">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblEquityName" runat="server" align="center" Text="Company Name"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtCompanySearch" runat="server" CssClass="txtField" 
                                        onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />
                                </HeaderTemplate>
                            
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" 
                                        Text='<%# Eval("CompanyName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="PEM_CompanyName" />--%>
                            <asp:BoundField DataField="Exchange" HeaderText="Exchange" />
                            <asp:BoundField DataField="Series" HeaderText="Series" />
                            <asp:BoundField DataField="OpenPrice" HeaderText="Open Price" />
                            <asp:BoundField DataField="HighPrice" HeaderText="High Price" />
                            <asp:BoundField DataField="LowPrice" HeaderText="Low Price" />
                            <asp:BoundField DataField="ClosePrice" HeaderText="Close Price" />
                            <asp:BoundField DataField="LastPrice" HeaderText="Last Price" />
                            <asp:BoundField DataField="PreviousClose" HeaderText="Previous Close" />
                            <asp:BoundField DataField="TotalTradeQuantity" 
                                HeaderText="Total Trade Quantity" />
                            <asp:BoundField DataField="TotalTradeValue" HeaderText="Total Trade Value" />
                            <asp:BoundField DataField="NoOfTrades" HeaderText="No Of Trades" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                        </Columns>
                    
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivPager" runat="server" style="display: none">
        <table style="width: 100%">
            <tr id="trPager" runat="server">
                <td>
                    <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                </td>
            </tr>
        </table>
    </div>
     </asp:Panel>
        
&nbsp;&nbsp;&nbsp;
    </telerik:RadPageView>
    
    <telerik:RadPageView ID="RadPageView3" runat="server">
        
&nbsp;&nbsp;&nbsp;
        <asp:Panel ID="pnlSchemeComparison" runat="server">
        <table class="TableBackground" width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblFundPerformance" runat="server" CssClass="HeaderText" 
                        Text="Fund Performance"></asp:Label>
                </td>
            </tr>                
            <tr>
                <td>
                    <table>
                         <tr>
                            <td class="leftField">
                                <asp:Label ID="lblReturn" runat="server" CssClass="FieldName" Text="Return:">                                
                                </asp:Label> 
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlReturn" runat="server" CssClass="cmbField">
                                <asp:ListItem Text="Choose return Period" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1 Week" Value="1"></asp:ListItem>
                                <asp:ListItem Text="1 Month" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                <asp:ListItem Text="6 Months" Value="4"></asp:ListItem>
                                <asp:ListItem Text="1 Year" Value="5"></asp:ListItem>
                                <asp:ListItem Text="2 Years" Value="6"></asp:ListItem>
                                <asp:ListItem Text="3 Years" Value="7"></asp:ListItem>
                                <asp:ListItem Text="5 Years" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Since Launch" Value="9"></asp:ListItem> 
                                </asp:DropDownList>
                                
                                <span id="Span3" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlReturn_CompareValidator" runat="server" 
                                    ControlToValidate="ddlReturn" CssClass="cvPCG" 
                                    ErrorMessage="Please select a Return" Operator="NotEqual" 
                                    ValidationGroup="btnGo" ValueToCompare="0">
                                </asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Condition:">
                                </asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlCondition" runat="server" CssClass="cmbField">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>                                
                                <asp:ListItem Text="Above 30%" Value="&gt;30"></asp:ListItem>
                                <asp:ListItem Text="Above 20%" Value="&gt;20"></asp:ListItem>
                                <asp:ListItem Text="Above 10%" Value="&gt;10"></asp:ListItem> 
                                <asp:ListItem Text="Above 5%" Value="&gt;5"></asp:ListItem>
                                <asp:ListItem Text="Gainer(+ve return)" Value="&gt;0"></asp:ListItem>
                                <asp:ListItem Text="Loser(-ve return)" Value="&lt;0"></asp:ListItem>
                                <asp:ListItem Text="Less than 5%" Value="&lt;-5"></asp:ListItem>
                                <asp:ListItem Text="Less than 10%" Value="&lt;-10"></asp:ListItem>                                 
                                <asp:ListItem Text="Less than 20%" Value="&lt;-20"></asp:ListItem>
                                <asp:ListItem Text="Less than 30%" Value="&lt;-30"></asp:ListItem>                                                                       
                                </asp:DropDownList>
                                
                                <span id="Span2" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlCondition_CompareValidator" runat="server" 
                                    ControlToValidate="ddlCondition" CssClass="cvPCG" 
                                    ErrorMessage="Please select a Condition" Operator="NotEqual" 
                                    ValidationGroup="btnGo" ValueToCompare="0">
                                </asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblSelectAMC" runat="server" CssClass="FieldName" Text="AMC:">
                                </asp:Label>                    
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSelectAMC" runat="server" AutoPostBack="true" 
                                    CssClass="cmbField">
                                </asp:DropDownList> 
                                <span id="Span4" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlAMC_CompareValidator" runat="server" 
                                    ControlToValidate="ddlSelectAMC" CssClass="cvPCG" 
                                    ErrorMessage="Please select an AMC" Operator="NotEqual" ValidationGroup="btnGo" 
                                    ValueToCompare="Select AMC Code">
                                </asp:CompareValidator>
                            </td>
                        </tr>                       
                       <%-- <tr>
                                                       
                        </tr>--%>
                       <%-- <tr>
                            
                        </tr>--%>
                        <%--<tr>
                            <td class="leftField">
                                <asp:Label ID="lblSelectScheme" runat="server" CssClass="FieldName" Text="Scheme:">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="cmbField" ID="ddlSelectScheme" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" 
                                    Text="Category:">
                                </asp:Label> 
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" 
                                    CssClass="cmbField" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                                 </asp:DropDownList>
                                <%--<span id="Span1" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlCategory_CompareValidator" runat="server"
                                    ControlToValidate="ddlCategory" ErrorMessage="Please select a Category"
                                    Operator="NotEqual" ValueToCompare="0" CssClass="cvPCG" ValidationGroup="btnGo">
                                </asp:CompareValidator>--%>
                            </td>
                           <%-- <div id="divSubCategory" runat="server" visible="false">
                            <td class="leftField">
                                <asp:Label ID="lblSubCategory" runat="server" CssClass="FieldName" 
                                    Text="SubCategory:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField">
                                </asp:DropDownList>                                
                            </td>
                            </div>--%>
                            <td></td>
                            <td>
                                <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" 
                                    OnClick="OnClick_btnGo" Text="Go" ValidationGroup="btnGo" />
                            </td>
                        </tr>
                     <%--   <tr>
                            
                        </tr>--%>
                                               
                        </table>
                </td>                
            </tr>
        </table>
        <%--<table  style="width: 100%;">
            <tr>
                <td>
                    <table id="ErrorMessage" align="center" runat="server" visible="false">
                        <tr>
                            <td>
                                <div class="failure-msg" align="center">
                                    No Records found ...
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
        <%--<table>
            <tr>
                <td>--%>
                <div 
            style="overflow-x:auto;overflow-y:hidden;width:100%;padding: 0 0 20px 0">

                   <telerik:RadGrid ID="gvMFFundPerformance" runat="server" 
                        AllowAutomaticInserts="false" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" EnableEmbeddedSkins="false" OnItemCommand="RadGrid1_ItemCommand"
                        GridLines="None" PageSize="10" ShowFooter="true" ShowStatusBar="True" 
                        Skin="Telerik" Width="100%">
                    <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
                    <mastertableview allowmulticolumnsorting="true" autogeneratecolumns="false" CommandItemDisplay="Top"
                        width="99%">
                        <%-- <ExportSettings HideStructureColumns="true" />--%>
                        <CommandItemTemplate>
                            <table class="rcCommandTable" width="100%">
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text=" " ToolTip="Export To CSV" CssClass="rgExpCSV" CommandName="ExportToCSV" />
                                    <asp:Button ID="Button2" runat="server" Text=" " ToolTip="Export To Excel" CssClass="rgExpXLS" CommandName="ExportToExcel" />
                                </td>                                
                            </table>
                        </CommandItemTemplate>
                        
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" />
                        <Columns>
                        <telerik:GridBoundColumn DataField="SchemeName" HeaderText="Scheme Name" 
                                UniqueName="SchemeName">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <%--<telerik:GridBoundColumn  DataField="AUM"  HeaderText="AUM" UniqueName="AUM" >
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        
                        <telerik:GridBoundColumn DataField="LaunchDate" DataFormatString="{0:d}"
                                HeaderText="Launch Date" UniqueName="LaunchDate">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="ClosingDate" DataFormatString="{0:d}" 
                                HeaderText="As On Date" UniqueName="ClosingDate">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="NAV" DataFormatString="{0:0.0000}" 
                                HeaderText="Current NAV" UniqueName="NAV">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="OneYearHighNAV" DataFormatString="{0:0.0000}" 
                                HeaderText="52 Weeks Highest NAV" UniqueName="OneYearHighNAV">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="OneYearLowNAV" DataFormatString="{0:0.0000}" 
                                HeaderText="52 Weeks Lowest NAV" UniqueName="OneYearLowNAV">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <%--<telerik:GridBoundColumn  DataField="YTD"  HeaderText="YTD" UniqueName="YTD" >
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        
                        <telerik:GridBoundColumn DataField="OneWeekReturn" DataFormatString="{0:0.00}" 
                                HeaderText="1 Week Return(%)" UniqueName="OneWeekReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="OneMonthReturn" DataFormatString="{0:0.00}" 
                                HeaderText="1 Month Return(%)" UniqueName="OneMonthReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="ThreeMonthReturn" DataFormatString="{0:0.00}" 
                                HeaderText="3 Months Return(%)" UniqueName="ThreeMonthReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="SixMonthReturn" DataFormatString="{0:0.00}" 
                                HeaderText="6 Months Return(%)" UniqueName="SixMonthReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="OneYearReturn" DataFormatString="{0:0.00}" 
                                HeaderText="1 Year Return(%)" UniqueName="OneYearReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="TwoYearReturn" DataFormatString="{0:0.00}" 
                                HeaderText="2 Years Return(%)" UniqueName="TwoYearReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="ThreeYearReturn" DataFormatString="{0:0.00}" 
                                HeaderText="3 Years Return(%)" UniqueName="ThreeYearReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="FiveYearReturn" DataFormatString="{0:0.00}" 
                                HeaderText="5 Years Return(%)" UniqueName="FiveYearReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="InceptionReturn" DataFormatString="{0:0.00}" 
                                HeaderText="Inception Ret." UniqueName="InceptionReturn">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="PE" DataFormatString="{0:0.00}" HeaderText="PE" 
                                UniqueName="PE">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="PB" DataFormatString="{0:0.00}" HeaderText="PB"
                                UniqueName="PB">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                       <%-- <telerik:GridBoundColumn  DataField="Cash"  HeaderText="Cash %" UniqueName="Cash" >
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        
                        <telerik:GridBoundColumn DataField="Sharpe" DataFormatString="{0:0.00}" 
                                HeaderText="Sharpe" UniqueName="Sharpe">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="SD" DataFormatString="{0:0.00}" HeaderText="SD" 
                                UniqueName="SD">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        
                       <%-- <telerik:GridBoundColumn  DataField="Top5Holdings"  HeaderText="Top 5 Holdings" UniqueName="Top5Holdings" >
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                                       
                    </Columns>
                    
                    </mastertableview>
                    <clientsettings>
                        <%--<Scrolling AllowScroll="false" UseStaticHeaders="True" SaveScrollPosition="true">
                        </Scrolling>--%>
                        <selecting allowrowselect="True" enabledragtoselectrows="True" />                           
                        <%-- <Resizing AllowColumnResize="True"></Resizing>--%>
                    </clientsettings>
                    </telerik:RadGrid>
                </div>
                <%--</td>
            </tr>
        </table>--%></asp:Panel>
        
&nbsp;&nbsp;&nbsp;
    </telerik:RadPageView>
        
    <telerik:RadPageView ID="RadPageView2" runat="server">
        
&nbsp;&nbsp;&nbsp;
        <asp:Panel ID="pnlFactSheet" runat="server">
        <table class="TableBackground" width="100%">
            
            <tr>
            <td>
            <table align="Left" width="80%">
            <tr>
              <td colspan="4"></td>
              </tr>
              
              <tr>
              <td colspan="4"></td>
              </tr>
                               
             <tr>
                <td align="right" valign="top" width="15%">
                    <asp:Label ID="lblAmcCode" runat="server" CssClass="FieldName" Text="AMC:"></asp:Label>
                </td>
                <td width="25%">
                    <asp:DropDownList ID="ddlAmcCode" runat="server" AutoPostBack="true" 
                        CssClass="cmbField" onselectedindexchanged="ddlAmcCode_SelectedIndexChanged" 
                        style="width:350px;">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="ddlAmcCode" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="&lt;br /&gt;Please Select AMC Code" Operator="NotEqual" 
                        ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                </td>
                <td align="right" valign="top" width="15%">
                    <asp:Label ID="lblSchemeList" runat="server" CssClass="FieldName" 
                        Text="Scheme:"></asp:Label>
                </td>
                <td width="25%">
                    <asp:DropDownList ID="ddlSchemeList" runat="server" CssClass="cmbField" 
                        style="width:350px;">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToValidate="ddlSchemeList" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="&lt;br /&gt;Please Select Scheme" Operator="NotEqual" 
                        ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                  </td>
            </tr> 
                               
                <tr>
                    <td class="leftField" colspan="2" width="40%">
                        <asp:Label ID="lblYear" runat="server" CssClass="FieldName" 
                            Text="Month &amp; Year:">
                        </asp:Label>
                    
                      <%--  <telerik:RadDatePicker Skin="Telerik" ID="RadDatePicker1" runat="server">
                            <DateInput Skin="Telerik" DateFormat="">
                            </DateInput>
                            <Calendar>
                                <FastNavigationSettings TodayButtonCaption="current date" />
                            </Calendar>
                        </telerik:RadDatePicker>--%>
                     <%--<asp:TextBox ID="txtFactSheetDate" runat="server" Width="120px"  CssClass="Field"></asp:TextBox>
                     <span class="spnRequiredField">*</span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="FieldName" ErrorMessage="Invalid Date"
                                        ControlToValidate="txtFactSheetDate" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="txtFactSheetDate_CalendarExtender" runat="server" TargetControlID="txtFactSheetDate"
                                        Format="dd/MM/yyyy" Enabled="True"></cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtFactSheetDate_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txtFactSheetDate" WatermarkText="dd/mm/yyyy" Enabled="True"></cc1:TextBoxWatermarkExtender>
                                    <asp:RequiredFieldValidator ID="rfv_txtFactSheetDate" ControlToValidate="txtFactSheetDate"
                ValidationGroup="MFSubmit" ErrorMessage="<br />Please Enter a Date" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:DropDownList ID="ddYear" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:DropDownList ID="ddMonth" runat="server" CssClass="cmbField">
            </asp:DropDownList>
                    </td>
                  
                    <td></td>
                </tr>
                <tr>
                      <td align="left" align="left" width="20%">
                        <asp:Button ID="btnViewFactsheet" runat="server" CssClass="PCGMediumButton" 
                              onclick="btnViewFactsheet_Click" Text="View Factsheet" 
                              ValidationGroup="MFSubmit" />
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                <td colspan="4"></td>
                </tr>
                 <tr>
                <td colspan="4"></td>
                </tr>
            </table>
            </td>
            </tr>  
              
                
               <tr>
               <td colspan="2">
               <table id="tblFactSheet" runat="server" align="Left" width="80%">
            
            <tr>
            <td colspan="3">
            </td>
            </tr>
            
                <tr>
                <td width="30%">
                <asp:Label ID="lblFactSheetHeading" runat="server" CssClass="HeaderTextSmall" 
                        Text="FactSheet for "></asp:Label>
                <asp:Label ID="lblGetScheme" runat="server" CssClass="HeaderTextSmall" Text=" "></asp:Label>
                </td>
                <td width="10%"></td>
                <td align="left" width="30%">
                <asp:Label ID="lblMonth" runat="server" CssClass="HeaderTextSmall" Text="Month: "></asp:Label>
                <asp:Label ID="lblGetMonth" runat="server" CssClass="HeaderTextSmall" Text=" "></asp:Label>
                </td>
                </tr>
                <tr><td colspan="3">
                    <hr /></td></tr>
            
            <tr>
            <td colspan="3"></td>
            </tr>
            
            <tr>
            <td width="30%">
            <asp:Label ID="lblObjective" runat="server" CssClass="HeaderTextSmall" 
                    Text="Fund Objective"></asp:Label>
            </td>
            <td width="10%"></td>
            <td width="30%">
            <asp:Label ID="lblInvestInfo" runat="server" CssClass="HeaderTextSmall" 
                    Text="Investment Information"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td valign="top" width="30%">
            <table id="tblFundObject" runat="server" width="100%">
            <tr id="tr3" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            </table>
            
            <table id="tblFactFundObject" runat="server" border="1" cellspacing="0" 
                    width="100%">
            <tr>
            <td>
             <asp:Label ID="lblObjPara" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            </table>
            
            </td>
            <td width="10%"></td>
            <td valign="top" width="30%">
            <table id="tblInvInformation" runat="server" width="100%">
            <tr id="tr4" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            </table>
            
            <table id="tblFactInvInformation" runat="server" border="1" cellspacing="0" 
                    width="100%">
            <tr>
            <td>
            <asp:Label ID="lblSchemeType" runat="server" CssClass="FieldName" 
                    Text="Scheme Type:"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblgetSchemeType" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
              
            <tr>
            <td>
            <asp:Label ID="lblLaunchDate" runat="server" CssClass="FieldName" 
                    Text="Launch date:"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblgetlunchDate" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            <asp:Label ID="lblFundMgr" runat="server" CssClass="FieldName" 
                    Text="Fund Manager: "></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblGetFundMgr" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
              
            <tr>
            <td>
            <asp:Label ID="lblBenchMark" runat="server" CssClass="FieldName" Text="Benchmark:"></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblgetBenchMark" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
              
              
            </table>
            </td>
            </tr>
            
            <tr>
            <td colspan="3"></td>
            </tr>
            
            <tr>
            <td width="30%">
            <asp:Label ID="lblFHDet" runat="server" CssClass="HeaderTextSmall" 
                    Text="Fund House Details"></asp:Label>
            </td>
            <td width="10%"></td>
            <td width="30%">
            <asp:Label ID="lblFundstr" runat="server" CssClass="HeaderTextSmall" 
                    Text="Fund Structure"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td valign="top" width="30%">
              <table id="tblFundHouseDetails" runat="server" width="100%">
            <tr id="tr5" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            </table>
            
            <table id="tblFactFundHouseDetails" runat="server" border="1" cellspacing="0" 
                    width="100%">
            <tr>
            <td>
            <asp:Label ID="lblAMC" runat="server" CssClass="FieldName" Text="AMC : "></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblgetAMC" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            <asp:Label ID="lblAddress" runat="server" CssClass="FieldName" Text="Addres: "></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblgetAddress" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            <asp:Label ID="lblWebsite" runat="server" CssClass="FieldName" Text="Website: "></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblgetWebsite" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            </table>
            </td>
            <td width="10%"></td>
            <td valign="top" width="30%">
              <table id="tblFundStructure" runat="server" width="100%">
            <tr id="tr6" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            </table>
            
            <table id="tblFactFundStructure" runat="server" border="1" cellspacing="0" 
                    width="100%">
            <tr>
            <td>
            <asp:Label ID="lblPERatio" runat="server" CssClass="FieldName" Text="P/E Ratio:"></asp:Label>
            </td>
            <td align="right">
            <asp:Label ID="lblgetPERatio" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            <asp:Label ID="lblPBRatio" runat="server" CssClass="FieldName" Text="P/B Ratio:"></asp:Label>
            </td>
            <td align="right">
            <asp:Label ID="lblgetPBRatio" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            <asp:Label ID="lblAvgMkt" runat="server" CssClass="FieldName" 
                    Text="Avg Market Cap(Rs):"></asp:Label>
            </td>
            <td align="right">
            <asp:Label ID="lblgetAvgMkt" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            
            <tr>
            <td colspan="3"></td>
            </tr>
            
            <tr>
            <td width="30%">
            <asp:Label ID="lblFinacialDetail" runat="server" CssClass="HeaderTextSmall" 
                    Text="Financial Details"></asp:Label>
            </td>
            <td width="10%"></td>
            <td valign="top" width="30%">
            <asp:Label ID="lblPerformance" runat="server" CssClass="HeaderTextSmall" 
                    Text="Scheme Performance"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td valign="top" width="30%">
              <table id="tblFinancialDetails" runat="server" width="100%">
            <tr id="tr7" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            </table>
            
            <table id="tblFactFinancialDetails" runat="server" border="1" cellspacing="0" 
                    width="100%">
            <tr>
            <td width="50%">
            <asp:Label ID="lblAUM" runat="server" CssClass="FieldName" Text="AUM"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblgetAUM" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lblNAV" runat="server" CssClass="FieldName" Text="NAV"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblgetNAV" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lblMinInvestment" runat="server" CssClass="FieldName" 
                    Text="Min Investment"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblgetMinInvestment" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lblNAV52high" runat="server" CssClass="FieldName" 
                    Text="NAV(52 week high)"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblgetNAV52high" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lblNAV52Low" runat="server" CssClass="FieldName" 
                    Text="NAV (52 week Low)"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblgetNAV52Low" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            </table>
            </td>
            <td width="10%"></td>
            <td valign="top" width="30%">
            <table id="tblmsgSchemePerformance" runat="server" width="100%">
            <tr id="tr1" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            </table>
            
            <table id="tblFactSchemePerformance" runat="server" border="1" cellspacing="0" 
                    width="100%">
            
             <tr>
            <td align="center" width="50%">
            <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName" Text="Period"></asp:Label>
            </td>
            <td align="center" width="50%">
            <asp:Label ID="lblReturns" runat="server" CssClass="FieldName" Text="Returns"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lbl3Month" runat="server" CssClass="FieldName" Text="3Months"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblGet3Month" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lbl6Month" runat="server" CssClass="FieldName" Text="6Months"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblGet6Month" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lbl1year" runat="server" CssClass="FieldName" Text="1Year"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblGet1year" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lbl3Years" runat="server" CssClass="FieldName" Text="3Years"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblGet3Years" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lbl5Years" runat="server" CssClass="FieldName" Text="5Years"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblGet5Years" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td width="50%">
            <asp:Label ID="lblSinceInception" runat="server" CssClass="FieldName" 
                    Text="Since Inception"></asp:Label>
            </td>
            <td align="right" width="50%">
            <asp:Label ID="lblGetSinceInception" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            </table>
            </td>
            </tr>
            
            <tr>
            <td colspan="3"></td>
            </tr>
            
            <tr>
            <td width="30%">
            <asp:Label ID="lblgetFinacialDetail" runat="server" CssClass="HeaderTextSmall" 
                    Text="Volatality Measures"></asp:Label>
            </td>
            <td colspan="2"></td>
            </tr>
            <tr>
            <td valign="top" width="30%">
              <table id="tblVolatality" runat="server" width="100%">
            <tr id="tr8" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            </table>
            
            
            <table id="tblFactVolatality" runat="server" border="1" cellspacing="0" 
                    width="100%">
            <tr>
            <td>
            <asp:Label ID="lblFama" runat="server" CssClass="FieldName" Text="Fama"></asp:Label>
            </td>
            <td align="right">
            <asp:Label ID="lblgetFama" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblstdDev" runat="server" CssClass="FieldName" Text="Std Deviation"></asp:Label>
            </td>
            <td align="right">
            <asp:Label ID="lblgetstdDev" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            <asp:Label ID="lblBeta" runat="server" CssClass="FieldName" Text="Beta"></asp:Label>
            </td>
            <td align="right">
            <asp:Label ID="lblgetBeta" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            <td>
            <asp:Label ID="lblSharpe" runat="server" CssClass="FieldName" Text="Sharpe"></asp:Label>
            </td>
            <td align="right">
            <asp:Label ID="lblgetSharpe" runat="server" CssClass="FieldName" Text=""></asp:Label>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            
            <tr>
            <td colspan="3"></td>
            </tr>
            
            <tr>
            <td width="30%">
            <asp:Label ID="lblCompany" runat="server" CssClass="HeaderTextSmall" 
                    Text="Top 10 Companies"></asp:Label>
            </td>
            <td width="10%"></td>
            <td width="30%">
            <asp:Label ID="lblSector" runat="server" CssClass="HeaderTextSmall" 
                    Text="Top 10 Sector wise holdings"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td colspan="3"></td>
            </tr>
            <tr>
            <td colspan="3"></td>
            </tr>
            
            <tr>
            <td valign="top" width="30%">
            <table width="100%">
            <tr id="trTop10Company" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvTop10Companies" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                        HeaderStyle-Width="90%" ShowFooter="true">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle " />
                        <Columns>
                            <asp:BoundField DataField="PEM_CompanyName" 
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" 
                                HeaderText="Name" />
                            <asp:BoundField DataField="PASPO_HoldPercentage" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" 
                                HeaderStyle-Width="20%" HeaderText="%" />

                        </Columns>
                    
                    </asp:GridView>
                </td>
                </tr>
            </table>
            </td>
            <td width="10%"></td>
            <td valign="top" width="30%">
            <table width="100%">
            <tr id="trSector" runat="server">
            <td align="center" style="background-color: White; color: Black;">
            <asp:Label ID="lblmsgAbsRetn" runat="server" CssClass="FieldName" 
                    Text="No Records Found!"></asp:Label>
            </td>
            </tr>
            <tr>
            <td>
            <asp:GridView ID="gvSectorWiseHolding" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                    HeaderStyle-Width="90%" ShowFooter="true">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" 
                    VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                   <asp:BoundField DataField="Sector" HeaderStyle-HorizontalAlign="Center" 
                        HeaderStyle-Width="20%" HeaderText="Name" />
                   <asp:BoundField DataField="HoldPercen" DataFormatString="{0:n2}" 
                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" HeaderText="%" 
                        ItemStyle-HorizontalAlign="Center" />
                </Columns>
                

</asp:GridView>
</td></tr>

</table>
</td>
</tr>
<tr><td colspan="3"></td></tr>


            </table>
               </td>
               </tr> 
            </table>
            <table>
                <tr>
                    <td>
                       <%-- <asp:UpdatePanel ID="updatePanel" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>--%>
                                <%--<asp:GridView ID="gvCustomers" runat="server" AllowPaging="true" AllowSorting="true"
                                    PageSize="20" Width="95%">
                                    <AlternatingRowStyle BackColor="aliceBlue" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:GridView>--%>                                                             
                           <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
            </table>
     </asp:Panel>
        
&nbsp;&nbsp;&nbsp;
    </telerik:RadPageView>
    
    
</telerik:RadMultiPage>
        
        
        
        
        
        
        
       
    </div>
    
    <table width="100%">
        </table>
    <asp:HiddenField ID="hdnFromDate" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnMFCount" runat="server" />
    <asp:HiddenField ID="hdnAssetGroup" runat="server" />
    <asp:HiddenField ID="hdnEquityCount" runat="server" />
    <asp:HiddenField ID="hdnSchemeSearch" runat="server" />
    <asp:HiddenField ID="hdnCurrentPage" runat="server" />
    <asp:HiddenField ID="hdnCompanySearch" runat="server" />
    <asp:HiddenField ID="hdnSort" runat="server" />
    
    <asp:HiddenField ID="hdnAmcCode" runat="server" />
    <asp:HiddenField ID="hdnSubCategory" runat="server" />
    <asp:HiddenField ID="hdnCondition" runat="server" />
    <asp:HiddenField ID="hdnReturnPeriod" runat="server" />
    <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" BorderStyle="None"
        BackColor="Transparent" />
          <asp:HiddenField ID="hdnassetType" runat="server" />
</div>
