<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUp.aspx.cs" Inherits="WealthERP.PopUp" %>

<%@ Register Src="~/Customer/AddBankDetails.ascx" TagName="AddBankDetails" TagPrefix="Master" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script language="javascript" type="text/javascript">
function somefunction() {
        isClose = true;
    }
    function doUnload() {
        if (!isClose) {
            window.opener.Reprocess();
        }
    }


    function ClosePopUp() {
        isClose = false;
        doUnload();
        window.close();
    }
</script>
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body  onbeforeunload="doUnload()">
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder ID="phLoadControl" runat="server"></asp:PlaceHolder>
   
    </div>
    </form>
</body>
</html>

