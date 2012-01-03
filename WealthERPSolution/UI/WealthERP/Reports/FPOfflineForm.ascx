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
        } else if (type == 'doc') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=4";
        }
        else {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=0";
        }

      
        return true;

    }
</script>
 
   <table width="100%">
       <tr>
        <td width="25%">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="FP Offline Form"></asp:Label>
        </td>
        <td align="right">
        <asp:Button ID="btnViewReport" runat="server" Text="View & Email" OnClientClick="return CustomerValidate('View')"
     PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="PCGMediumButton"  />&nbsp;&nbsp;
     <asp:Button ID="btnViewInPDF" runat="server" Text="Export To PDF" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton"  />&nbsp;&nbsp;
     <asp:Button ID="btnViewInDOC" runat="server" Text="Export To Doc"  CssClass="PCGMediumButton" OnClientClick="return CustomerValidate('doc')"
     PostBackUrl="~/Reports/Display.aspx?mail=4"  />
        </td>
    </tr>
    <tr>
    <td colspan="2"><hr /></td>
    </tr>
   <tr>
   <td colspan="2"><p style="color: #055187;font-family: Arial;font-size: small;font-weight: bold;" >Click the buttons to See the FP Offline form in different format</p></td>
   </tr>      
   <tr>
       <%--<td style="width:50%">
      <asp:Button ID="btnViewInPDF" runat="server" Text="View Form" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton" />
    </td>--%>
   </tr>
   
<%--   <tr>
    
    <td style="width:50%">
    <asp:Button ID="btnViewReport" runat="server" Text="View & Email" OnClientClick="return CustomerValidate('View')"
     PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="PCGMediumButton" />&nbsp;&nbsp;
     <asp:Button ID="btnViewInPDF" runat="server" Text="Export To PDF" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGMediumButton" />
    </td>
   
    </tr>--%>
<%--    <tr>
    <td style="width:50%">
   <asp:DropDownList ID="ddlBtnSelect" runat="server" CssClass="cmbField" AutoPostBack="true"
            onselectedindexchanged="ddlBtnSelect_SelectedIndexChanged">
   <asp:ListItem Value="ViewReport" Text="View & Email" Selected="True"></asp:ListItem>
   <asp:ListItem Value="ViewInPdf" Text="View In PDF"></asp:ListItem>
   <asp:ListItem Value="ViewInDoc" Text="View In Doc"></asp:ListItem>
   </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  
   <asp:Button ID="btnViewReport" runat="server" Text="Go" OnClientClick="return CustomerValidate('View')"
     PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="PCGButton"  />&nbsp;&nbsp;
     <asp:Button ID="btnViewInPDF" runat="server" Text="Go" OnClientClick="return CustomerValidate('pdf')"
     PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PCGButton"  />&nbsp;&nbsp;
     <asp:Button ID="btnViewInDOC" runat="server" Text="Go"  CssClass="PCGButton" OnClientClick="return CustomerValidate('doc')"
     PostBackUrl="~/Reports/Display.aspx?mail=4"  />
    </td>
    </tr>--%>
   </table>