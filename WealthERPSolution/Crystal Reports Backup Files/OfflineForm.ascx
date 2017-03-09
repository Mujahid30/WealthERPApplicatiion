<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfflineForm.aspx.cs" Inherits="WealthERP.Reports.OfflineForm" %>

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
            window.document.forms[0].action = "/Reports/Display.aspx?mail=3";
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
   <td>Click the button to See the Offline form</td>
   </tr>      
   <tr>
       <td style="width:50%">
      <asp:Button ID="btnViewInPDF" runat="server" Text="Export To PDF" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton" />
    </td>
   </tr>
   </table>

