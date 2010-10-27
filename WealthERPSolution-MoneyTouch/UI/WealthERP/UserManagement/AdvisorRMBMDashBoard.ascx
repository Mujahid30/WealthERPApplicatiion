<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorRMBMDashBoard.ascx.cs" Inherits="WealthERP.UserManagement.AdvisorRMBMDashBoard" %>
   <html>
    <head>
    <meta http-equiv="pragma" content="no-cache" />
    </head>
    <body>
    <table width="100%">
        <tr>
            <td align="right">
             <asp:HyperLink ID="hlReleases" runat="server" NavigateUrl="~/Releases.html" Target="_blank" CssClass="LinkButtons">Release Bulletin</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
            
            </td>
        </tr>
    </table>
       
    </body>
</html>
                <asp:Label ID="lbl" runat="server" 
    Text="Please add Your Branch Details...."></asp:Label>
<asp:LinkButton ID="lnkAdd" runat="server" onclick="lnkAdd_Click">Add</asp:LinkButton>
