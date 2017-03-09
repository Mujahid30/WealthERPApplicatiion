<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MFOnlineOrder.aspx.cs"
    Inherits="WealthERP.OnlineOrder.MFOnlineOrder" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                     <rsweb:reportviewer id="rptViewer" runat="server" width="100%" height="800px">
                     </rsweb:reportviewer> 
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>