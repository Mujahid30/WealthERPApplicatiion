<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WealthERP.Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html; charset=ISO-8859-1">

    <script src="Scripts/jquery.js"></script>

    <style>
        body
        {
            background-color: #EBEFF9;
        }
        .error
        {
            color: Red;
            font-weight: bold;
            font-size: 12px;
        }
        .success
        {
            color: Green;
            font-weight: bold;
            font-size: 12px;
        }
        input
        {
            border: 1px solid #ccc;
            color: #333333;
            font-size: 12px;
            margin-top: 2px;
            padding: 3px;
            width: 200px;
        }
        .left-td
        {
            text-align: right;
            width: 52%;
            padding-left:100px;
            color:#16518A;
            
        }
        .right-td
        {
           
            text-align: left;
        }
        .spnRequiredField
        {
            color: #FF0033;
            font-size: x-small;
        }
    </style>

    <script>
        function checkLoginId() {
            $("#hidValid").val("0");
            if ($("#txtLogin").val() == "") {
                $("#spnLoginStatus").html("");
                return;
            }
            $("#spnLoginStatus").html("<img src='Images/loader.gif' />");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "Register.aspx/CheckLoginIdAvailability",
                data: "{ 'loginId': '" + $("#txtLogin").val() + "' }",
                error: function(xhr, status, error) {
                    alert("Sorry. Something went wrong!");
                },
                success: function(msg) {

                    if (msg.d) {
                        $("#hidValid").val("1");
                        $("#spnLoginStatus").removeClass();
                        $("#spnLoginStatus").addClass("success");
                        $("#spnLoginStatus").html("available");
                    }
                    else {
                        $("#hidValid").val("0");
                        $("#spnLoginStatus").removeClass();
                        $("#spnLoginStatus").addClass("error");
                        $("#spnLoginStatus").html("Not available");
                    }
                }

            });



        }
        function isValid() {
            if (document.getElementById('hidValid').value == '1') {
                Page_ClientValidate();
                return Page_IsValid;
            }
            else {
                Page_ClientValidate();
                return false;
            }
        }
    </script>

    <title>WealthERP Register</title>
</head>
<body style="text-align: center;">
    <form id="myform" runat="server">
    <table width="90%" border="0" style="margin-left: 20px; text-align: center;" cellspacing="15" >
        <tr>
            <td align="center" colspan="2">
            </td>
        </tr>
        <tr>
            <td class="HeaderCell" colspan="2">
                <h3 style="color: #942627;">
                    WealthERP Registration</h3>
            </td>
        </tr>
                 <tr>
            <td class="left-td">
               WealthERP Login Id:
            </td>
            <td class="right-td">
                <asp:TextBox ID="txtLogin" runat="server" onchange="checkLoginId()"></asp:TextBox>
                <span id="Span6" class="spnRequiredField">*</span> <span id="spnLoginStatus"></span>
                <input type="hidden" id="btnCheck1" name="btnCheck1" value="check22222" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="<br/>Please enter Login Id"
                    ControlToValidate="txtLogin" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="left-td">
                Email Id:
            </td>
            <td class="right-td">
                <asp:TextBox ID="txtEmail" runat="server" ToolTip="tool tip goes here"></asp:TextBox><span
                    id="Span5" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<br/>Please enter Email Id "
                    ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<br/>Please enter a valid Email Id"
                    Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txtEmail" ></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="left-td">
                Organization Name:
            </td>
            <td class="right-td">
                <asp:TextBox ID="txtOrganization" runat="server"></asp:TextBox><span id="Span4" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<br/>Please enter Organization Name"
                    ControlToValidate="txtOrganization" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="left-td">
                Contact Person:
            </td>
            <td class="right-td">
                <asp:TextBox ID="txtContactPerson" runat="server"></asp:TextBox><span id="Span1"
                    class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<br/>Please enter Contact Person"
                    ControlToValidate="txtContactPerson" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="left-td">
                City:
            </td>
            <td class="right-td">
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox><span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<br/>Please enter city"
                    ControlToValidate="txtCity" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="left-td">
                Mobile:
            </td>
            <td class="right-td">
                <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox><span id="Span3" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="<br/>Please enter Mobile"
                    ControlToValidate="txtMobile" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<br/>Please enter a valid Mobile Number"
                    Display="Dynamic" ValidationExpression="\d{10}" ControlToValidate="txtMobile" ></asp:RegularExpressionValidator>
            </td>
        </tr>

       
        <tr>
            <td colspan="2" align="center" style="padding-top: 10px">
                <asp:Button ID="btnSubmit" runat="server" Text="Register" Style="background-image: url(Images/submit.png);
                    width: 98px; height: 28px; color: White;" OnClick="btnSubmit_Click" OnClientClick="return isValid()" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="hidValid" />
    </form>
</body>
</html>
