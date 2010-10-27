<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlHost.aspx.cs" Inherits="WealthERP.ControlHost" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>

   <%--<link href="../CSS/ControlsStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
    <script language="javascript" type="text/javascript"> 
        function showpopup() {
            window.open('Advisor/PopUp.aspx', 'CustomPopUp', 'width=400, height=500, menubar=no, resizable=yes');
        }
        function showpop() {
            window.open('CustomerPortfolio/TestMaster.aspx', 'CustomPopUp', 'width=500, height=500, menubar=no, resizable=yes');
        }
        function closepopup() {
            window.opener = self;
            window.close();

        }
        function showProrate() {
            window.open('CustomerPortfolio/EquityProrate.aspx', 'CustomPopUp', 'width=400, height=500, menubar=no, resizable=yes');
        }
        function showSIPDetails() {
          
            window.open('CustomerPortfolio/MFSIPDetails.aspx', 'CustomPopUp', 'width=500, height=450, menubar=no, resizable=yes');
        }
        function showSWPDetails() {
           
            window.open('CustomerPortfolio/MFSWPDetails.aspx', 'CustomPopUp', 'width=400, height=500, menubar=no, resizable=yes');
        }
        function showSTPDetails() {
            window.open('CustomerPortfolio/MFSTPDetails.aspx', 'CustomPopUp', 'width=400, height=500, menubar=no, resizable=yes');
        }
        function ShowMoneyBackEpisode() {
            window.open('CustomerPortfolio/MoneyBackEpisodeAdd.aspx', 'CustomePopUp', 'width=500,height=500,menubar=no,resizable=yes');
        }
        function showPanel() {
            $find("collapsed")._doOpen();
        }
        function HidePanel() {
            $find("collapsed")._doClose();

        }
        function showUploadRules() {
            window.open('Uploads/WerpUploadRules.aspx', 'CustomPopUp', 'width=400, height=500, menubar=no, resizable=yes, scrollbars=yes');
        }
    </script>

</head>
<body class="TableBackground" style="overflow-y:hidden;">
    <form id="form1" runat="server">
    <div style="vertical-align:top">
        <asp:Panel ID="mainpanel" runat="server" >
          <%--  <asp:Literal ID="ltBreadCrumbs" runat="server">
            </asp:Literal>--%>
            <asp:PlaceHolder ID="PlaceHolder1" EnableViewState="true" runat="server"></asp:PlaceHolder>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
