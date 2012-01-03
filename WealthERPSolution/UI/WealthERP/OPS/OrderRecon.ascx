<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderRecon.ascx.cs" Inherits="WealthERP.OPS.OrderRecon" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    
</asp:ScriptManager>

<%--<script type="text/javascript" language="javascript">

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
</script>--%>

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
<td class="style1" colspan="2">
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date"  />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date"  />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
</td>
</tr>
                        </table>
                        
      
                        <table id="tblRange" runat="server" >
    <tr id="trRange" visible="false" runat="server">
        <td align="left" valign="top">
            <asp:Label ID="lblFromDate" runat="server" width="50" CssClass="FieldName">From:</asp:Label>
            </td>
            <td valign="top">
            <asp:TextBox ID="txtFromDate" runat="server" style="vertical-align: middle" Width="150" CssClass="txtField"></asp:TextBox>
            <span id="spnFrom" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td valign="top" align="left">
            <asp:Label ID="lblToDate" runat="server" width="50" CssClass="FieldName">To:</asp:Label>
            </td>
            <td valign="top">
            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField" Width="150"> 
            </asp:TextBox>
            <span id="spnTo" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>

    </tr>

     <tr id="trPeriod" visible="false" runat="server">
        <td>
            <asp:Label ID="lblPeriod" runat="server" Width="50"  CssClass="FieldName">Period:</asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="ddlPeriod" runat="server" Width="150"  AutoPostBack="true" CssClass="cmbField" >
            </asp:DropDownList>
            <span id="spnPeriod" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvPeriod" runat="server" ErrorMessage="<br />Please select a Period"
            ValidationGroup="MFSubmit" ControlToValidate="ddlPeriod" Operator="NotEqual"
            CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    </table>
                        
                        
</td>


</tr>

 
  <tr>
   <td align="left" colspan="2">
  <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: "  CssClass="FieldName"></asp:Label>
        <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField">
        <%--<asp:ListItem Text="Executed" Value="Executed" ></asp:ListItem>
        <asp:ListItem Text="In Process" Value="In Process" ></asp:ListItem>
        <asp:ListItem Text="Pending" Value="Pending" ></asp:ListItem>
        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
        <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>--%>
        </asp:DropDownList>
        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToValidate="ddlOrderStatus" CssClass="cvPCG" Display="Dynamic" 
                        ErrorMessage="<br />Please Select Status" Operator="NotEqual" 
                        ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
  </td>
<td colspan="4"></td>

  </tr>
<tr>
    <td>
     <asp:Label ID="lblOrderType" runat="server" Text="Order Type: "  CssClass="FieldName"></asp:Label>
   
        <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField">
        <asp:ListItem Text="Immediate" Value="Y" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Future" Value="N"></asp:ListItem>
        </asp:DropDownList>
   
  </td>
</tr>

<%--<tr>
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
</tr>--%>
<tr>
  <td colspan="6">
   <asp:Button ID="btnGo" runat="server" CssClass="PCGButton"  Text="GO"  ValidationGroup="MFSubmit" CausesValidation="true"
          onclick="btnGo_Click" />
  </td>
  </tr>
<tr>
<td colspan="6"></td>
</tr>  
  <tr>
  <td colspan="6">
  <asp:GridView ID="gvOrderRecon" runat="server" DataKeyNames="CMOT_MFOrderId" AutoGenerateColumns="False" OnRowDataBound="gvOrderRecon_RowDataBound"
                   CellPadding="4" CssClass="GridViewStyle" 
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
                                <asp:Label ID="lblCustNameHeader" runat="server" Text='<%# Eval("Customer_Name").ToString() %>'></asp:Label>
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
                                <asp:Label ID="lblOrderNoHeader" runat="server" Text='<%# Eval("CMOT_OrderNumber").ToString() %>'></asp:Label>
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
                                <asp:Label ID="lbltransactionHeader" runat="server" Text='<%# Eval("CMFT_TransactionNumber").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                      
                        <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lbltransaction1" runat="server" Text="Order Date"></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbltransactionHeader1" runat="server" Text='<%#Eval("CMOT_OrderDate").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                        
                         <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:Label ID="lblOrderTypeHeader" runat="server" Text="Order Type" ></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrderType" runat="server" Text='<%#Eval("CMOT_IsImmediate").ToString() %>'> </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status"></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrderStatusHeader" runat="server" Text='<%#Eval("XS_Status").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField> 
                        
                     <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblPendingReject" runat="server" Text="Pending/Reject Reason"></asp:Label>                  
                                </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPendingRejectHeader" runat="server" Text='<%#Eval("XSR_StatusReason").ToString() %>'></asp:Label>
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
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
    <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    </div>
    </td>
    </tr>
 </table>
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnOrderType" runat="server" />
<asp:HiddenField ID="hdnOrderStatus" runat="server" />
