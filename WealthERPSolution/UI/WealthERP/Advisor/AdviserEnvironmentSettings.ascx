<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserEnvironmentSettings.ascx.cs" Inherits="WealthERP.Advisor.AdviserEnvironmentSettings" %>

<script type="text/javascript">

    function alertingForOpsEnabeling() {

        var chkCheckedOrNot = document.getElementById("<%=chkIsOpsEnable.ClientID%>").checked;
        if (chkCheckedOrNot == true) {
            var bool = window.confirm('By Enabling Ops Role All Operational Permissions will be \n Removed From Admin Login & Given to Ops Login.\n\n Click OK to Add a Staff with Ops Role If you have not added staff  \n who is having Ops role.\n\n Click Cancel If you have already added a staff with Ops role \n and to proceed further...\n');
            if (bool) {
                document.getElementById("ctrl_AdviserEnvironmentSettings_hdnEnableOpsMsg").value = 1;
                document.getElementById("ctrl_AdviserEnvironmentSettings_hiddenbtnOpsEnable").click();

                return false;
            }
            else {
                document.getElementById("ctrl_AdviserEnvironmentSettings_hdnEnableOpsMsg").value = 0;
                return true;
            }
        }
        else {

            var bool = window.confirm('By Disabling Ops Role Your Ops Staff will be Inactive.');
            if (bool) {
                document.getElementById("ctrl_AdviserEnvironmentSettings_hdnDisableOpsMsg").value = 1;
                document.getElementById("ctrl_AdviserEnvironmentSettings_hiddenbtnOpsDisable").click();

                return false;
            }
            else {
                document.getElementById("ctrl_AdviserEnvironmentSettings_hdnDisableOpsMsg").value = 0;
                return true;
            }
        
        
        }
    }

    function alertingForAddDefaultIP() {
        var chkCheckedOrNot = document.getElementById("<%=chkIsIPEnable.ClientID%>").checked;
        if (chkCheckedOrNot == true) {
            alert("By Enabling this your IP Login Security has been activated.!! \n\n You current system IP has been added to your IP list..");
        }
        else {
            alert("By Disabling this you will loose your IP Login Security..!!");
        }
    }

</script>

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderTextBig" colspan="2">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Advisor Environment Settings"></asp:Label>
            <hr />
        </td>
    </tr>
    
    </table>
    
    <table>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblNoteIP" runat="server" CssClass="txtField" Text="Note: Please check if you have static IP. Feature is useful with static IP only.">
            </asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="lblisIPEnable" runat="server" CssClass="FieldName" Text="IP Enable :"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="chkIsIPEnable" onclick="alertingForAddDefaultIP()" style="vertical-align: middle" CssClass="FieldName" runat="server" Text="Is IP Enable ?" AutoPostBack="false" />
        </td>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="lblisOpsEnable" runat="server" CssClass="FieldName" Text="Ops Enable :"></asp:Label>
        </td>
        <td>
                <asp:CheckBox ID="chkIsOpsEnable" runat="server" onclick="return alertingForOpsEnabeling()" style="vertical-align: middle" CssClass="FieldName" Text="Is Ops Enable ?" />
        </td>
    </tr>
    
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    
     
    
    
    <tr>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditAdvisorProfile_btnSubmit', 'S');"
                
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditAdvisorProfile_btnSubmit', 'S');" 
                onclick="btnSubmit_Click" />
        </td>
    </tr>
    
</table>

<asp:Button ID="hiddenbtnOpsEnable" runat="server" 
    BorderStyle="None" BackColor="Transparent" onclick="hiddenbtnOpsEnable_Click" />
    
<%--<asp:Button ID="hiddenbtnOpsDisable" runat="server" BorderStyle="None" BackColor="Transparent"
    onclick="hiddenbtnOpsDisable_Click" /> --%>   
    
<asp:HiddenField ID="hdnEnableOpsMsg" runat="server" />
<asp:HiddenField ID="hdnDisableOpsMsg" runat="server" />
