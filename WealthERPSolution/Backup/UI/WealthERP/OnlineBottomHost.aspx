<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineBottomHost.aspx.cs" Inherits="WealthERP.OnlineBottomHost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >



<head runat="server">
    <title></title>
   
<script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>
</head>
 <script>
     function calcIFrameHeight(ifrm_id) {
         try {
             var leftframe_height = parent.document.getElementById('bottomframe').contentWindow.document.body.scrollHeight;
             var the_height = leftframe_height;
            
             if (the_height < 150)
             { the_height = 150; }
                 var newHeight = the_height;
                 if (parent.document.getElementById('bottomframe').height != newHeight)
                     parent.document.getElementById('bottomframe').height = newHeight;

         }
         catch (e) { }
     }
    </script>
<body >
    <form id="form1" runat="server" >
    <div>
    <asp:Panel ID="bottompanel" runat="server"  >
            <asp:PlaceHolder ID="phBottom" EnableViewState="true" runat="server" ></asp:PlaceHolder>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
