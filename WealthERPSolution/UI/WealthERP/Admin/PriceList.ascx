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


 <script type="text/javascript">
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
        </script>

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
        <telerik:RadTab runat="server" Text="Factsheet" Value="Factsheet" TabIndex="1" Visible="false">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Scheme Comparison" Value="Scheme_Comparison" TabIndex="2" >
        </telerik:RadTab>        
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
    EnableEmbeddedSkins="false">
</telerik:RadAjaxLoadingPanel>
 

<telerik:RadMultiPage ID="FactsheetMultiPage" EnableViewState="true"  runat="server" SelectedIndex="0">
    <telerik:RadPageView ID="RadPageView1" runat="server">
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
                    <asp:Label ID="lblIllegal" Text="" runat="server" CssClass="Error" />
                </td>
            </tr>
          
            
       
            <tr><td></td>
                <td align="left">
                 <asp:RadioButton ID="rbtnCurrent" Text="Latest" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" OnCheckedChanged="rbtnCurrent_CheckedChanged" CssClass="cmbField" />
                   <%-- <asp:RadioButton ID="rbtnHistorical" Text="Historical" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" CssClass="cmbField" OnCheckedChanged="rbtnHistorical_CheckedChanged" />--%>
                </td></tr>
               <tr> <td></td>  <td align="left">
                
                <asp:RadioButton ID="rbtnHistorical" Text="Historical" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" CssClass="cmbField" OnCheckedChanged="rbtnHistorical_CheckedChanged" />
                   <%-- <asp:RadioButton ID="rbtnCurrent" Text="Latest" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" OnCheckedChanged="rbtnCurrent_CheckedChanged" CssClass="cmbField" />--%>
                </td>
               
               
            </tr>
              <tr id="trFromDate" runat="server">
                <td align="right">
                    <label id="lblFromDate" class="FieldName" runat="server" title="FromDate">
                        FromDate:</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox><cc1:CalendarExtender
                        ID="FrmDate" TargetControlID="txtFromDate" runat="server" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                     <asp:CompareValidator id="cvChkFutureDate" runat="server" Type="Date"
                   ControlToValidate="txtFromDate"
                   Operator="LessThanEqual" CssClass="cvPCG"
                   ErrorMessage="Date Can't be in future"
                   Display="Dynamic" ValidationGroup="vgbtnSubmit">
                  </asp:CompareValidator>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromDate" ErrorMessage="please enter from date" ValidationGroup="vgbtnSubmit"  CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
                  
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" runat="server" CssClass="cvPCG" ErrorMessage="Please Enter valid Date"
                                        ControlToValidate="txtFromDate" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
       
                    <%--<asp:RequiredFieldValidator ID="frmdatevalid" runat="server" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr id="trToDate" runat="server">
                <td align="right">
                    <label id="lblToDate" class="FieldName" runat="server" title="ToDate">
                        ToDate:</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtToDate" CssClass="txtField" runat="server"></asp:TextBox><cc1:CalendarExtender
                        ID="TDate" TargetControlID="txtToDate" runat="server" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator id="compDateValidator" ValidationGroup="vgbtnSubmit" Display="Dynamic"
                        ControlToValidate="txtToDate" Operator="LessThanEqual" Type="Date" CssClass="cvPCG"
                        runat="server" ErrorMessage="Date Can't be in future" ></asp:CompareValidator>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate" ErrorMessage="please enter to date" ValidationGroup="vgbtnSubmit" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator><br />
                      
               <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" runat="server" CssClass="cvPCG" ErrorMessage="Please Enter valid Date"
                                        ControlToValidate="txtToDate" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
          <asp:CompareValidator id="cvCompareDate" runat="server"
                   ControlToValidate="txtToDate" ControlToCompare="txtFromDate"
                   Operator="GreaterThanEqual"
                   Type="Date" CssClass="cvPCG" ValidationGroup="vgbtnSubmit"
                   ErrorMessage="ToDate should be greater than FromDate"
                   Display="Dynamic">
                 </asp:CompareValidator>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            
            <tr runat="server" id="trSelectMutualFund">
                <td align="right">
                    <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Select AMC Code:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSelectMutualFund" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectMutualFund_OnSelectedIndexChanged" >
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlSelectMutualFund"
                ErrorMessage="Please Select AMC Code" Operator="NotEqual" ValueToCompare="Select AMC Code"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                </td>
            </tr>
             <tr runat="server" id="trSelectSchemeNAV">
                <td align="right">
                    <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Select Scheme Name:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSelectSchemeNAV" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                     <%--<asp:CompareValidator ID="cvddlSelectSchemeNAV" runat="server" ControlToValidate="ddlSelectSchemeNAV"
                ErrorMessage="Please Select Scheme Name" Operator="NotEqual" ValueToCompare="Select Scheme Name"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>--%>
                </td>
            </tr></table>
            <table><tr id="trbtnSubmit" runat="server">
             <td>
             <asp:Button ID="btnSubmit" Text="Submit" CssClass="PCGButton" ValidationGroup="vgbtnSubmit" runat="server" OnClick="OnClick_Submit" />
            </td>
            </tr>
        </table>
    </div>
    <div id="DivMF" runat="server" style="display: none">
        <table style="width: 100%">
            <tr id="trMfPagecount" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblMFCurrentPage" class="Field" runat="server"></asp:Label>
                    <asp:Label ID="lblMFTotalRows" class="Field" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr runat="server" id="trgrMfView">
                <td>
                    <asp:GridView ID="gvMFRecord" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                        ShowFooter="true" Font-Size="Small">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <%--<asp:BoundField DataField="SchemePlanName" HeaderText="Scheme Plan Name" SortExpression="PASP_SchemePlanName" />--%>
                            <asp:BoundField DataField="SchemePlanCode" HeaderText="Scheme Plan Code" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblName" align="center" runat="server" Text="Scheme Plan Name"></asp:Label>
                                    <br />
                                   <%-- <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />--%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeName" runat="server" Text='<%# Eval("SchemePlanName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NetAssetValue" HeaderText="Net AssetValue" />
                            <asp:BoundField DataField="RepurchasePrice" HeaderText="Repurchase Price" />
                            <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" />
                            <asp:BoundField DataField="PostDate" HeaderText="Post Date" />
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
                <td class="leftField" runat="server" id="trPageCount">
                    <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                    <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr  runat="server" id="trgvEquityView">
                <td>
                    <asp:GridView ID="gvEquityRecord" runat="server" CssClass="GridViewStyle" ShowFooter="true"
                        AutoGenerateColumns="False" Font-Size="Small">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblEquityName" align="center" runat="server" Text="Company Name"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtCompanySearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName").ToString() %>'></asp:Label>
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
                            <asp:BoundField DataField="TotalTradeQuantity" HeaderText="Total Trade Quantity" />
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
    </telerik:RadPageView>
        
    <telerik:RadPageView ID="RadPageView2" runat="server">
    <asp:Panel ID="pnlFactSheet" runat="server">
        <table class="TableBackground" width="100%">
                               
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblYear" runat="server" CssClass="FieldName" Text="Month & Year:">
                        </asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker Skin="Telerik" ID="RadDatePicker1" runat="server">
                            <DateInput Skin="Telerik" DateFormat="MMMM yyyy">
                            </DateInput>
                            <Calendar>
                                <FastNavigationSettings TodayButtonCaption="current date" />
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnViewFactsheet" Text="View Factsheet" CssClass="PCGMediumButton" runat="server" OnClick="OnClick_btnViewFactsheet" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%--<ajaxToolkit:UpdatePanelAnimationExtender ID="aeFactsheet"
                          runat="server" TargetControlID="updatePanel">
                             <Animations>
                                <OnUpdating>
                                    <Parallel duration="0">
                                        <ScriptAction Script="onUpdating();" />  
                                        <EnableAction AnimationTarget="btnViewFactsheet" Enabled="false" />
                                        <FadeOut minimumOpacity=".5" />
                                    </Parallel>
                                </OnUpdating>
                                <OnUpdated>
                                    <Parallel duration="0">
                                        <ScriptAction Script="onUpdated();" />  
                                        <EnableAction AnimationTarget="btnViewFactsheet" Enabled="true" />
                                        <FadeOut minimumOpacity=".5" />
                                    </Parallel>
                                </OnUpdated>
                            </Animations>
                        </ajaxToolkit:UpdatePanelAnimationExtender>--%>
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
    </telerik:RadPageView>
    
    <telerik:RadPageView ID="RadPageView3" runat="server">
    <asp:Panel ID="pnlSchemeComparison" runat="server">
        <table class="TableBackground" width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblFundPerformance" CssClass="HeaderText" Text="Fund Performance" runat="server"></asp:Label>
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
                                <asp:DropDownList ID="ddlReturn" runat="server" CssClass="cmbField" AutoPostBack="true" >
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
                                    ControlToValidate="ddlReturn" ErrorMessage="Please select a Return"
                                    Operator="NotEqual" ValueToCompare="0" CssClass="cvPCG" ValidationGroup="btnGo">
                                </asp:CompareValidator>
                            </td>
                        </tr>                       
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Condition:">
                                </asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlCondition" runat="server" CssClass="cmbField" 
                                 AutoPostBack="true">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Less than 30%" Value="<30" ></asp:ListItem>
                                <asp:ListItem Text="Less than 20%" Value="<20" ></asp:ListItem>
                                <asp:ListItem Text="Less than 10%" Value="<10" ></asp:ListItem>             
                                <asp:ListItem Text="Less than 5%" Value="<5" ></asp:ListItem>                                                             
                                <asp:ListItem Text="Loser(-ve return)" Value="<0" ></asp:ListItem>
                                <asp:ListItem Text="Gainer(+ve return)" Value=">0" ></asp:ListItem>
                                <asp:ListItem Text="Above 5%" Value=">5" ></asp:ListItem>  
                                <asp:ListItem Text="Above 10%" Value=">10" ></asp:ListItem> 
                                <asp:ListItem Text="Above 20%" Value=">20" ></asp:ListItem>
                                <asp:ListItem Text="Above 30%" Value=">30" ></asp:ListItem>       
                                </asp:DropDownList>
                                
                                <span id="Span2" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlCondition_CompareValidator" runat="server"
                                    ControlToValidate="ddlCondition" ErrorMessage="Please select a Condition"
                                    Operator="NotEqual" ValueToCompare="0" CssClass="cvPCG" ValidationGroup="btnGo">
                                </asp:CompareValidator>
                            </td>                           
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblSelectAMC" runat="server" CssClass="FieldName" Text="AMC:">
                                </asp:Label>                    
                            </td>
                            <td>
                                <asp:DropDownList CssClass="cmbField" ID="ddlSelectAMC" runat="server" AutoPostBack="true">
                                </asp:DropDownList> 
                                <span id="Span4" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlAMC_CompareValidator" runat="server"
                                    ControlToValidate="ddlSelectAMC" ErrorMessage="Please select an AMC"
                                    Operator="NotEqual" ValueToCompare="Select AMC Code" CssClass="cvPCG" ValidationGroup="btnGo">
                                </asp:CompareValidator>
                            </td>
                        </tr>
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
                                <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:">
                                </asp:Label> 
                            </td>
                            <td>
                                <asp:DropDownList CssClass="cmbField" ID="ddlCategory" runat="server" 
                                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" AutoPostBack="true">
                                 <asp:ListItem Text="Select Category" Value="0"></asp:ListItem>
                                <asp:ListItem Text="commodity" Value="MFCO"></asp:ListItem>
                                <asp:ListItem Text="Debt" Value="MFDT"></asp:ListItem>
                                <asp:ListItem Text="Equity" Value="MFEQ"></asp:ListItem>
                                <asp:ListItem Text="Hybrid" Value="MFHY"></asp:ListItem>
                                </asp:DropDownList>
                                <span id="Span1" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlCategory_CompareValidator" runat="server"
                                    ControlToValidate="ddlCategory" ErrorMessage="Please select a Category"
                                    Operator="NotEqual" ValueToCompare="0" CssClass="cvPCG" ValidationGroup="btnGo">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblSubCategory" CssClass="FieldName" Text="SubCategory:" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="cmbField" ID="ddlSubCategory" runat="server">
                                </asp:DropDownList>
                                <%--<span id="Span2" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlSubCategory_CompareValidator" runat="server"
                                    ControlToValidate="ddlCategory" ErrorMessage="Please select a SubCategory"
                                    Operator="NotEqual" ValueToCompare="0" CssClass="cvPCG" ValidationGroup="btnGo">
                                </asp:CompareValidator>--%>
                            </td>
                        </tr>
                                               
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnGo" Text="Go" CssClass="PCGButton" runat="server" OnClick="OnClick_btnViewFactsheet" ValidationGroup="btnGo"/>
                            </td>
                        </tr>
                    </table>
                </td>                
            </tr>
        </table>
        <table>
            <tr>
                <td>
                   <telerik:RadGrid  ID="gvMFFundPerformance" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="15" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="false" 
                    AllowAutomaticInserts="false">
                    <PagerStyle Mode="NumericPages"></PagerStyle>
                    <MasterTableView Width="100%">
                <Columns>
                <telerik:GridBoundColumn  DataField="SchemeName"  HeaderText="Scheme Name" 
                    UniqueName="SchemeName" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <%--<telerik:GridBoundColumn  DataField="AUM"  HeaderText="AUM" UniqueName="AUM" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridBoundColumn  DataField="LaunchDate"  HeaderText="Launch Date" UniqueName="LaunchDate" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="NAV"  HeaderText="Current NAV" UniqueName="NAV" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="OneYearHighNAV"  HeaderText="52 Weeks Highest NAV" UniqueName="OneYearHighNAV" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="OneYearLowNAV"  HeaderText="52 Weeks Lowest NAV" UniqueName="OneYearLowNAV" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <%--<telerik:GridBoundColumn  DataField="YTD"  HeaderText="YTD" UniqueName="YTD" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridBoundColumn  DataField="OneWeekReturn"  HeaderText="1 Week Return(%)" UniqueName="OneWeekReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="OneMonthReturn"  HeaderText="1 Month Return(%)" UniqueName="OneMonthReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="ThreeMonthReturn"  HeaderText="3 Months Return(%)" UniqueName="ThreeMonthReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="SixMonthReturn"  HeaderText="6 Months Return(%)" UniqueName="SixMonthReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="OneYearReturn"  HeaderText="1 Year Return(%)" UniqueName="OneYearReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="TwoYearReturn"  HeaderText="2 Years Return(%)" UniqueName="TwoYearReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="ThreeYearReturn"  HeaderText="3 Years Return(%)" UniqueName="ThreeYearReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="FiveYearReturn"  HeaderText="5 Years Return(%)" UniqueName="FiveYearReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="InceptionReturn"  HeaderText="Inception Ret." UniqueName="InceptionReturn" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="PE"  HeaderText="PE" UniqueName="PE" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="PB"  HeaderText="PB" UniqueName="PB" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
               <%-- <telerik:GridBoundColumn  DataField="Cash"  HeaderText="Cash %" UniqueName="Cash" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridBoundColumn  DataField="Sharpe"  HeaderText="Sharpe" UniqueName="Sharpe" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn  DataField="SD"  HeaderText="SD" UniqueName="SD" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
               <%-- <telerik:GridBoundColumn  DataField="Top5Holdings"  HeaderText="Top 5 Holdings" UniqueName="Top5Holdings" >
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                               
            </Columns>
            </MasterTableView>
            <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                    </Scrolling>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />                           
                   <%-- <Resizing AllowColumnResize="True"></Resizing>--%>
            </ClientSettings>
        </telerik:RadGrid>

                   
                </td>
            </tr>
        </table>
    </asp:Panel>
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
    <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" BorderStyle="None"
        BackColor="Transparent" />
          <asp:HiddenField ID="hdnassetType" runat="server" />
</div>
