<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MFSIPDetails.aspx.cs" Inherits="WealthERP.CustomerPortfolio.MFSIPDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript">
 function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server" />
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptTransactionView" runat="server"></asp:ScriptManager>

    <div>
    
        <table style="width:50%;" align="center">
            <tr>
                <td class="leftField">
                    <asp:Label ID="FieldName" runat="server" Text="SIP Details" 
                        CssClass="HeaderTextSmall"></asp:Label>
                </td>
            </tr>
            <tr>
                <td  class="leftField">
                    <asp:Label ID="Label2" runat="server" Text="Start Date" CssClass="FieldName"></asp:Label>
                </td>
                <td  class="rightField">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtField"></asp:TextBox>
                      <span id="Span4" class="spnRequiredField">*</span>
                       <cc2:calendarextender id="cetxtStartDate" runat="server" targetcontrolid="txtStartDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" >
            </cc2:calendarextender>
            <cc2:textboxwatermarkextender id="tetxtStartDate" runat="server"
                targetcontrolid="txtStartDate" watermarktext="dd/mm/yyyy" >
            </cc2:textboxwatermarkextender>
                </td>
            </tr>
            <tr>
                <td class="leftField" >
                    <asp:Label ID="Label3" runat="server" Text="Period" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPeriod" runat="server" CssClass="txtField"></asp:TextBox>  
                       <span id="Span3" class="spnRequiredField">*</span>
                       
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label4" runat="server" Text="End Date" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtField"></asp:TextBox>
                      <span id="Span2" class="spnRequiredField">*</span>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label5" runat="server" Text="Frequency" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtFrequency" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span1" class="spnRequiredField">*</span>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label6" runat="server" Text="Amount" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span5" class="spnRequiredField">*</span>
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="2">
                    <asp:Label ID="Label7" runat="server" CssClass="FieldName" 
                        Text="Note: All the SWPs from future will be created by the System Automatically"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="2">
                    <asp:Label ID="Label8" runat="server" CssClass="FieldName" 
                        Text="Please Enter Details for First Transaction only"></asp:Label>
                </td>
            </tr>
           <tr>
        <td colspan="6" class="tdRequiredText">
            <asp:Label id="lbl" CssClass="lblRequiredText" 
              Text="Fields marked with a ' * ' are compulsory" runat="server"></asp:label>
        </td>
    </tr>
            <tr>
                <td align="center" class="style1" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" 
                        Text="Submit" onclick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
