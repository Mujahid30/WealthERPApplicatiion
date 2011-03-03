<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyDatabaseApp._Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
</head>
<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"/>
    <div>
    <table>
     <tr>
    <td colspan="2">
    <asp:CheckBox ID="chkAuthenticationType" runat="server" Checked="true" 
            Text="SQL Authentication" AutoPostBack="true"
            oncheckedchanged="chkAuthenticationType_CheckedChanged" />
    </td>
    </tr>
    
    <tr>
    <td><asp:Label ID="lblUserName" runat="server" Text="UserName : "></asp:Label></td>
    <td><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblPassword" runat="server" Text="Password : "></asp:Label></td>
    <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
    </tr> 
    <tr>
    <td><asp:Label ID="lblServer" runat="server" Text="Server Name : "></asp:Label></td>
    <td><asp:TextBox ID="txtServer" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblDatabase" runat="server" Text="Database : "></asp:Label></td>
    <td><asp:TextBox ID="txtDatabase" runat="server"></asp:TextBox></td>
    </tr> 
   <tr>    
    <td colspan="2">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" />
    </td>
    </tr> 
    </table>
    <table>
    <tr>          
    <td>
    <asp:Label ID="lblTableName" runat="server" Text="Table : " Enabled="false"></asp:Label>
    </td>
    <td>
     <telerik:RadComboBox ID="cmbTable" runat="server" Enabled="false" ItemsPerRequest="10" EnableVirtualScrolling="true" >
        </telerik:RadComboBox>
    </td>
    </tr>
    <tr>
    <td>
    <asp:Button ID="btnGo" runat="server" Text="Go" onclick="btnGo_Click" Enabled="false" />
    </td>
    </tr>
    </table>
    <table width="100%">
    <tr>
    <td><telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" 
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
            GridLines="None" ShowGroupPanel="True" Skin="Vista"  >
<MasterTableView >
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
</MasterTableView>
        <ClientSettings AllowColumnsReorder="True"  
            ReorderColumnsOnClient="True">
            <Scrolling UseStaticHeaders="True"/>
        </ClientSettings>
        </telerik:RadGrid>        
        
    </td>
    </tr>
        
    </table>
    
    </div>
    </form>
</body>
</html>
