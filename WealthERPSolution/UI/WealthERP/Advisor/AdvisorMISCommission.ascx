<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorMISCommission.ascx.cs" Inherits="WealthERP.Advisor.AdvisorMISCommission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    
</asp:ScriptManager>
<script type="text/javascript" language="javascript">
    
    function DisplayDates(type) {

       
        if (type == 'DATE_RANGE') {

            document.getElementById("tblRange").style.display = 'block';
            document.getElementById("tblPeriod").style.display = 'none';
           
            }

            else if (type == 'PERIOD') {

            document.getElementById("tblRange").style.display = 'none';
            document.getElementById("tblPeriod").style.display = 'block';
           
           
           
        }   


        
        document.getElementById("<%= hidDateType.ClientID %>").value = type;
    };

//********************************Date Validation************************************************
    function validation() {
    
        var dateType = document.getElementById("<%= hidDateType.ClientID  %>").value
        if (dateType == 'DATE_RANGE') {

            dateVal = document.getElementById("<%= txtFromDate.ClientID  %>").value;
            if (dateVal == null || dateVal == "" || dateVal == 'dd/mm/yyyy') {
                alert("Please select from date")
                return false;
            }
            toDate = document.getElementById("<%= txtToDate.ClientID  %>").value;
            if (toDate == null || toDate == "" || toDate == 'dd/mm/yyyy') {
                alert("Please select to date")
                return false;
            }
            if (isFutureDate(toDate) == true) {
                alert("To date cannot be greater than current date.")
                return false;
            }
        }
        else if (dateType == 'PERIOD') {

            dateVal = document.getElementById("<%= ddlPeriod.ClientID  %>").selectedIndex;
            if (dateVal < 1) {
                alert("Please select a period")
                return false;
            }
        }
    }
    //**********************************************************
    
    function isFutureDate(dateToCheck) {
        var currentDate = '<%= DateTime.Now.ToString("yyyyMMdd") %>'
        var yyyymmdddateToCheck = dateToCheck.substr(6, 4) + dateToCheck.substr(3, 2) + dateToCheck.substr(0, 2)
        if (currentDate < yyyymmdddateToCheck) {
            return true;
        }
        else
            return false;
    }
</script>
<table width="100%">
     <tr>
        <td colspan="6">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="MF MIS Commission"></asp:Label>
            <hr />
        </td>
  </tr>
  
   
<tr>
    
    <td colspan="6">
<table><tr id="tr1" runat="server">
<td id="Td3" runat="server">
    <asp:Label ID="Label1" runat="server" Text="MIS Type:"  CssClass="FieldName"></asp:Label>
</td>
<td id="Td5" runat="server">
     <asp:DropDownList ID="ddlMISType" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Folio Wise" Value="Folio Wise"></asp:ListItem>
        <asp:ListItem Text="AMC Wise" Value="AMC Wise"></asp:ListItem>
        <asp:ListItem Text="Transaction Type Wise" Value="Transaction_Wise"></asp:ListItem>
        <asp:ListItem Text="Category Wise" Value="Category Wise"></asp:ListItem>
    </asp:DropDownList>
</td>
 <td class="style1">
   <asp:RadioButton ID="rbtnPickDate" Class="cmbField" Checked="True" runat="server" Text="Pick a Date" GroupName="Date" onclick="DisplayDates('DATE_RANGE')" />
  </td>
  <td>
  <asp:RadioButton ID="rbtnPickPeriod" Class="cmbField" runat="server" Text="Pick a Period" GroupName="Date" onclick="DisplayDates('PERIOD')"/>
 </td>
</tr>



</table></td>

</tr>

<tr>

     <td colspan="6">
     <%--
                        <table id="tblPickDate" border="0">
                            <tr>
                                <td class="style1">
                                   <asp:RadioButton ID="rbtnPickDate" Class="cmbField" Checked="True" runat="server" Text="Pick a Date" GroupName="Date" onclick="DisplayDates('DATE_RANGE')" />
                                </td>
                                <td>
                                   <asp:RadioButton ID="rbtnPickPeriod" Class="cmbField" runat="server" Text="Pick a Period" GroupName="Date" onclick="DisplayDates('PERIOD')"/>
                                </td>
                            </tr>
                        </table>--%>
                        
      
                        <table id="tblRange">
                            <tr>
                               <td valign="middle" align="left">
                               
                                <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName">
                                </asp:Label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server" TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy" Enabled="True">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtFromDate" CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" runat="server" ValidationGroup="btnGo">
                                </asp:RequiredFieldValidator>--%>
                               </td>
                               <td valign="middle" align="left">
                                <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server" TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy" Enabled="True">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtToDate" CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                                   runat="server" ValidationGroup="btnGo">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date" ype="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                                        CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo">
                                </asp:CompareValidator>--%>
                                </td>
                            </tr>
                        </table>
                        
                        
                        <table id="tblPeriod" style="display:none">
                            <tr>
                                <td valign="top">
                                   <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period: </asp:Label>
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField"></asp:DropDownList>
                                    <span id="Span4" class="spnRequiredField">*</span>
                                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod" CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select a Period" Operator="NotEqual"
                                        ValidationGroup="btnGo" ValueToCompare="Select a Period">
                                    </asp:CompareValidator>--%>
                                </td>
                            </tr>
                        </table>
                        
                        <asp:Button ID="btnView" runat="server" CssClass="PCGButton"  Text="Go" onclick="btnView_Click" OnClientClick="return validation()"/>
</td>


</tr>
    
    <tr>
     <td class="leftField" colspan="5" align="right"  width="60%">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
     </td>
      <td>
      </td>
    </tr>   
   
    <tr>
    <td colspan="5" align="center"  width="60%">
      <asp:GridView ID="gvCommissionMIS" width="100%" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                            HorizontalAlign="Center" ShowFooter="True">
                            <FooterStyle CssClass="FooterStyle"  />                            
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                             <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <%--   <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkGoalOutput" runat="server" CssClass="GridViewCmbField" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                --%>
                                
                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                <asp:Label ID="lblHeaderText" runat="server" CssClass="HeaderStyle" Font-Bold="true" 
                                            ForeColor="White" Text=""> </asp:Label>
                                </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMISCom" runat="server" CssClass="CmbField"  Text='<%#Eval("MISType") %>'>
                                        </asp:Label>
                                    </ItemTemplate> 
                                     <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="Total" >
                                        </asp:Label>                            
                                    </FooterTemplate>                                 
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Brokerage Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBrokAmt" runat="server" CssClass="CmbField" Text='<%#Eval("BrokerageAmt") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lblTotalValue" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="Rs." >
                                        </asp:Label>                            
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                            </Columns>                            
                           
                        </asp:GridView>
    </td>
    <td>
    </td>
    </tr>
           
    <tr id="trPager" runat="server">
        <td colspan="5" align="center"  width="60%">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
        <td>
        </td>
       
    </tr>
  
    
    

</table>


<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
   
    <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
      
    </div>
    </td>
    </tr>
 </table>


<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnMISType" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />

<script type="text/javascript">

    if (document.getElementById("<%= rbtnPickDate.ClientID %>").checked) {
        document.getElementById("tblRange").style.display = 'block';
        document.getElementById("tblPeriod").style.display = 'none';
    }
    else if (document.getElementById("<%= rbtnPickPeriod.ClientID %>").checked) {
    document.getElementById("tblRange").style.display = 'none';
    document.getElementById("tblPeriod").style.display = 'block';
           
    }
   </script>