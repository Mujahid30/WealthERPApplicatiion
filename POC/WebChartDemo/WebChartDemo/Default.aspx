<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebChartDemo._Default" %>

<%@ Register assembly="WebChart" namespace="WebChart" tagprefix="Web" %>

<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 203px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table><tr>
    <td>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        </td>
    <td> 
        &nbsp;</td>
    <td class="style1">
        <asp:Chart ID="MyChart" runat="server" Height="399px" Width="660px">
            <series>
                <asp:Series Name="Series1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
        </td>
    </tr></table>
       
    
    </div>
    </form>
</body>
</html>
