<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserControlDelegate._Default" %>

<%@ Register src="UserControl/UCHeader.ascx" tagname="UCHeader" tagprefix="uc1" %>
<%@ Register src="UserControl/ucFooter.ascx" tagname="ucFooter" tagprefix="uc2" %>
<%@ Register src="UserControl/ucSideMenu.ascx" tagname="ucSideMenu" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 149px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table style="width:100%;">
        <tr align = center >
            <td colspan="2" align = center  >
                
                <uc1:UCHeader ID="UCHeader1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                
                <uc3:ucSideMenu ID="ucSideMenu1" runat="server" />
                
            </td>
            <td>
                <asp:Label ID="lblUcPath" runat="server" Text="Label" Visible="False"></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:PlaceHolder ID="placeholder" runat="server"></asp:PlaceHolder>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
         <tr>
            <td colspan="2" style="text-align: center">
                
                <uc2:ucFooter ID="ucFooter1" runat="server" />
                
             </td>
        </tr>
    </table>
    </form>
    </body>
</html>
