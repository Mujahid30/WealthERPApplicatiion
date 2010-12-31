<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FPSectional.ascx.cs" Inherits="WealthERP.Reports.FPSectional" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    
</asp:ScriptManager>
<table border="0" width="100%">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextSmall" Text="Financial Planning Reports"></asp:Label>
            <hr />
        </td>
        
    </tr>
    
    
    <tr>
    <td>
     <asp:CheckBox ID="chkCover_page" runat="server" CssClass="cmbField" Text="Cover page" />
    </td>
    <td>
     <asp:CheckBox ID="chkGoalProfile" runat="server" CssClass="cmbField" Text="Goal Profiling" />      
    </td>
    </tr>
    
      
    <tr>
    <td>
     <asp:CheckBox ID="chkRM_Messgae" runat="server" CssClass="cmbField" Text="RM Messgae" />
    </td>
    <td>
    <asp:CheckBox ID="chkIncome_Expense" runat="server" CssClass="cmbField" Text="Income and Expense Summary" />
    </td>
    </tr>
    
      
    <tr>
    <td>
     <asp:CheckBox ID="chkImage" runat="server" CssClass="cmbField" Text="Image" /> 
    </td>
    <td>
    <asp:CheckBox ID="chkCash_Flows" runat="server" CssClass="cmbField" Text="Cash Flows" />
    </td>
    </tr>
    
      
    <tr>
    <td>
     <%-- <asp:CheckBox ID="Content" runat="server" CssClass="cmbField" Text="Table of Content" /> --%>
    </td>
    <td>
    <asp:CheckBox ID="chkNetWorthSummary" runat="server" CssClass="cmbField" Text="Net Worth Summary" />
    </td>
    </tr>
    
      
    <tr>
    <td>
     <asp:CheckBox ID="chkFPIntroduction" runat="server" CssClass="cmbField" Text="FP Introduction" />
    </td>
    <td>
     <asp:CheckBox ID="chkRiskProfile" runat="server" CssClass="cmbField" Text=" Risk profile & Portfolio allocation" />
    </td>
    </tr>
   
      
    <tr>
    <td>
     <asp:CheckBox ID="chkProfileSummary" runat="server" CssClass="cmbField" Text="Profile Summary" />    
    </td>
    <td>
    <asp:CheckBox ID="chkInsurance" runat="server" CssClass="cmbField" Text="Insurance Details" />    
    </td>
    </tr>
    
    
    
      
    <tr>
    <td>
    <asp:CheckBox ID="chkFinancialHealth" runat="server" CssClass="cmbField" Text="Financial Health" /> 
    </td>
    <td>
    <asp:CheckBox ID="chkCurrentObservation" runat="server" CssClass="cmbField" Text="Current Situation and Observations" />    
    </td>
    </tr>  
    
    
    <tr>
    <td>
     <asp:CheckBox ID="chkKeyAssumptions" runat="server" CssClass="cmbField" Text="Key Assumptions" />  
    </td>
    <td>
    <asp:CheckBox ID="chkDisclaimer" runat="server" CssClass="cmbField" Text="Disclaimer" />  
    </td>
    </tr>   
    
    <tr>
   
    </tr>
    <tr>
    <td>
    </td>
    <td>
    <asp:Button ID="btnViewReport" runat="server" Text="View Report" 
     PostBackUrl="~/Reports/Display.aspx?mail=0" CssClass="PCGMediumButton" />
    </td>
    </tr>
 </table>