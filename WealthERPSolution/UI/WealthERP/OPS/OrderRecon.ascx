<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderRecon.ascx.cs" Inherits="WealthERP.OPS.OrderRecon" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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

    function CheckedOnlyOneRadioButton(spanChk) {
        var IsChecked = spanChk.checked;
        var CurrentRdbID = spanChk.id;
        var Chk = spanChk;
        Parent = document.getElementById("<%=gvOrderRecon.ClientID%>");
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {
              if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                if (items[i].checked) {
                    items[i].checked = false;

                }
            }
        }
    }
</script>

<table width="100%">
     <tr>
        <td colspan="6">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="Order Recon"></asp:Label>
            <hr />
        </td>
  </tr>
<tr>
<td colspan="6">
     
                        <table id="tblPickDate" border="0">
                            <tr>
                                <td class="style1">
                                   <asp:RadioButton ID="rbtnPickDate" Class="cmbField" Checked="True" runat="server" Text="Pick a Date" GroupName="Date" onclick="DisplayDates('DATE_RANGE')" />
                                </td>
                                <td>
                                   <asp:RadioButton ID="rbtnPickPeriod" Class="cmbField" runat="server" Text="Pick a Period" GroupName="Date" onclick="DisplayDates('PERIOD')"/>
                                </td>
                            </tr>
                        </table>
                        
      
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
                                </td>
                               <td valign="middle" align="left">
                                <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server" TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy" Enabled="True">
                                </ajaxToolkit:TextBoxWatermarkExtender>
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
                                  </td>
                            </tr>
                        </table>
                        
                        
</td>


</tr>

 
  <tr>
   <td align="left" colspan="2">
  <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: "  CssClass="FieldName"></asp:Label>
        <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Executed" Value="Executed" ></asp:ListItem>
        <asp:ListItem Text="In Process" Value="In Process" ></asp:ListItem>
        <asp:ListItem Text="Pending" Value="Pending" ></asp:ListItem>
        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
        <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
        </asp:DropDownList>
  </td>
<td colspan="4"></td>

  </tr>
<tr>
    <td>
     <asp:Label ID="lblOrderType" runat="server" Text="Order Type: "  CssClass="FieldName"></asp:Label>
   
        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Immediate" Value="Immediate" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Future" Value="Future"></asp:ListItem>
        </asp:DropDownList>
   
  </td>
</tr>

<tr>
 <td>
  <asp:Label ID="lblOrderDate" runat="server" Text="Order Date: "  CssClass="FieldName"></asp:Label>

  <asp:TextBox ID="txtOrderDate" runat="server" CssClass="txtField"></asp:TextBox>
           <cc1:CalendarExtender ID="txtOrderDate_CalendarExtender" runat="server" TargetControlID="txtOrderDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtOrderDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtOrderDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
  </td>
</tr>
<tr>
  <td colspan="6">
   <asp:Button ID="btnGo" runat="server" CssClass="PCGButton"  Text="GO" 
          onclick="btnGo_Click" />
  </td>
  </tr>
<tr>
<td colspan="6"></td>
</tr>  
  <tr>
  <td colspan="6">
  <asp:GridView ID="gvOrderRecon" runat="server" AutoGenerateColumns="False" 
                   CellPadding="4" CssClass="GridViewStyle" DataKeyNames="Id"
                    ShowFooter="true" ShowHeader="true" width="100%" >
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" 
                        VerticalAlign="Top" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                  <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                       <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblCustName" runat="server" Text="Customer"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="GridViewTxtField" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustNameHeader" runat="server" Text='<%# Eval("Customer").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblOrderNo" runat="server" Text="Order Number"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="GridViewTxtField" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrderNoHeader" runat="server" Text='<%# Eval("OrderNumber").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lbltransaction" runat="server" Text="Transaction No."></asp:Label>
                                <br />
                                <asp:TextBox ID="txtTransaction" runat="server" CssClass="GridViewTxtField" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbltransactionHeader" runat="server" Text='<%# Eval("TransactionNumber").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                      
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lbltransaction1" runat="server" Text="Order Date"></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbltransactionHeader1" runat="server" Text='<%#Eval("Orderdate").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                        
                         <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:Label ID="lblOrderType" runat="server" Text="Order Type" ></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrderTypeHeader" runat="server" Text='<%#Eval("OrderType").ToString() %>'> </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status"></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrderStatusHeader" runat="server" Text='<%#Eval("OrderStatus").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                        
                     <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblPendingReject" runat="server" Text="Pending/Reject Reason"></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPendingRejectHeader" runat="server" Text='<%#Eval("Pending/Reject").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                        
                      <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblManualMatch" runat="server" Text="Manual Match"></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                            <asp:RadioButton  ID="rbtnMatch" runat="server" Checked="false" onclick="javascript:CheckedOnlyOneRadioButton(this);" />
                           </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>                          
                    </Columns>
                </asp:GridView>
                </td>
  </tr>
  <tr>
  <tr>
  <td colspan="6">
  &nbsp;
  </td>
  </tr>
  <td colspan="6"  align="left">
  <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton"  Text="Submit" 
          onclick="btnSubmit_Click" />
  </td>
  </tr>
</table>
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
