<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FPOfflineForm.ascx.cs" Inherits="WealthERP.Reports.FPOfflineForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
    
</asp:ScriptManager>
<script type="text/javascript" language="javascript">

    function CustomerValidate(type) {
        if (type == 'pdf') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=2";
        } else {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=0";
        }

      
        return true;

    }
</script>
 
   <table width="100%">
       <tr>
        <td>
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="FP Offline Form"></asp:Label>
            <hr />
        </td>
    </tr>
   <tr>
   <td><p style="color: #055187;font-family: Arial;font-size: small;font-weight: bold;" >Click the button to See the FP Offline form</p></td>
   </tr>      
   <tr>
       <%--<td style="width:50%">
      <asp:Button ID="btnViewInPDF" runat="server" Text="View Form" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton" />--%>
    </td>
   </tr>
   
   <tr>
    
    <td style="width:50%">
    <asp:Button ID="btnViewReport" runat="server" Text="View & Email" OnClientClick="return CustomerValidate('View')"
     PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="PCGMediumButton" />&nbsp;&nbsp;
     <asp:Button ID="btnViewInPDF" runat="server" Text="Export To PDF" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton" />
    </td>
   
    </tr>
   
   </table>