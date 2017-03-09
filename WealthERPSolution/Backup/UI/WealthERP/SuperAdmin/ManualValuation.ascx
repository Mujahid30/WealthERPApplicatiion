<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManualValuation.ascx.cs" Inherits="WealthERP.SuperAdmin.ManualValuation" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
        .module1
        {
            background-color: #dff3ff;
            border: 1px solid #c6e1f2;
        }
    </style>
    
    <script type="text/javascript">

                function changeEndDate(sender, eventArgs) {
                    try
                        {
                            var grd_Cb = document.getElementById("<%= gvAdviserList.ClientID %>");
                            document.getElementById("<%= gvAdviserList.ClientID %>").style.display = 'none';
                        }

                     catch(err){}

                 }

                 function checkAllBoxes() {

                     //get total number of rows in the gridview and do whatever
                     //you want with it..just grabbing it just cause
                     var totalChkBoxes = parseInt('<%= gvAdviserList.Rows.Count %>');
                     var gvControl = document.getElementById('<%= gvAdviserList.ClientID %>');

                     //this is the checkbox in the item template...this has to be the same name as the ID of it
                     var gvChkBoxControl = "chkBx";

                     //this is the checkbox in the header template
                     var mainChkBox = document.getElementById("chkBoxAll");

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
<%--<tr>
<td>
 <asp:Image ID="imgGoalImage" ImageAlign="Left" runat="server" ImageUrl="~/Images/Telerik/EditButton.gif" />
</td>
</tr>--%>
     <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Manual Valuation"></asp:Label>
            <hr />
        </td>
     </tr> 
     <tr>
       
        
        <td>
         <asp:RadioButton ID="rbtnMF" runat="server" AutoPostBack="True" 
                CssClass="cmbField" Text="Mutual Fund" GroupName="ManualValuation" 
                oncheckedchanged="rbtnMF_CheckedChanged" />
           
        </td>
        
        <td>
            <asp:RadioButton ID="rbtnEquity" runat="server" AutoPostBack="True" 
                CssClass="cmbField" Text="Equity" GroupName="ManualValuation" 
                oncheckedchanged="rbtnEquity_CheckedChanged" />
        </td>
       
    </tr>    
    <tr id="trEquity" runat="server">
        <td>
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Trade Date"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddTradeYear" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddTradeMonth" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMonth_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlTradeDate" runat="server" CssClass="cmbField" 
                AutoPostBack="True" onselectedindexchanged="ddlTradeDate_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trMF" runat="server">
        <td align="right">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Trade Date:"></asp:Label>
        </td>
        <td id="tdTradeDate" runat="server">
                <telerik:RadDatePicker ID="txtTradeDate" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                    <ClientEvents OnDateSelected="changeEndDate" />
                </telerik:RadDatePicker>
                <div id="dvTradeDate" runat="server" class="dvInLine">
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtTradeDate"
                        ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtTradeDate" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td>
    <asp:Button ID="btnAdviserList" runat="server" Text="Go" CssClass="PCGButton" 
            onclick="btnAdviserList_Click" Visible="true" />
    </td>
        <%--<td>
            <asp:DropDownList ID="ddTradeMFYear" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMFYear_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddTradeMFMonth" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddTradeMFMonth_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlTradeMFDate" runat="server" CssClass="cmbField" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlTradeMFDate_SelectedIndexChanged">
            </asp:DropDownList>
        </td>--%>
    </tr>
    
      
</table>

<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Height="275px" ScrollBars="Horizontal">
<table width="100%" class="TableBackground">
   
    <tr>
    <td>
    
    <asp:GridView ID="gvAdviserList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" AllowSorting="True" HorizontalAlign="Center" ShowHeader="true" ShowFooter="true" DataKeyNames="AdviserId">
                <FooterStyle CssClass="FooterStyle" />
                <PagerSettings Visible="False" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>  
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkBoxAll"  name="vehicle" value="Bike" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AdviserId" HeaderText="AdviserId" />
                    <asp:BoundField DataField="OrgName" HeaderText="Organisation" />
                    <asp:BoundField DataField="ValuationStatus" HeaderText="Valuation Status" />
                </Columns>
            </asp:GridView>
    
            
    </td>
    </tr>
      
   
 </table>
 </asp:Panel>
 <table>
   <tr>
    <td>
    <asp:Button ID="btnRunValuation" runat="server" Text="Run Valuation" CssClass="PCGLongButton" 
    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerUpload_btn_Upload','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerUpload_btn_Upload','L');"
            onclick="btnRunValuation_Click" Visible="false" />
    </td>
   </tr>    
 </table>
    <asp:HiddenField ID="hfIsCurrentValuation" runat="server" />

 

 
   

