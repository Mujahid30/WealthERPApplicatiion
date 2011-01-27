<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfflineForm.aspx.cs" Inherits="WealthERP.FP.OfflineForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
        <style type="text/css">
        .FieldName
        {
            font-family: Verdana,Tahoma;
            font-weight: bold;
            font-size: 10px;
            color: #16518A;
            vertical-align: middle;
        }
        .txtField
        {
            font-family: Verdana,Tahoma;
            font-weight: normal;
            font-size: x-small;
            color: #16518A;
            width: 200px;
            height: 17px;
         }
        .header
        {
            font-family: Verdana,Tahoma;
            font-weight: bold;
            font-size: 16px;
            color: black;
            background-color: #ff99ff;
        }
        .style1
        {
            height: 23px;
        }
        .txtField1
        {
            font-family: Verdana,Tahoma;
            font-weight: bold;
            font-size: 11px;
            color: #16518A;
        }
        .txtField2
        {
            font-family: Verdana,Tahoma;
            font-weight: normal;
            font-size: 11px;
            color: #16518A;
        }
        .txtField3
        {
            font-family: Verdana,Tahoma;
            font-weight: bold;
            font-size: 12px;
            color: #16518A;
        }
    </style>
    <script id="myScript" language="javascript" type="text/javascript">
    function message(score, rclass) {
        alert(score + rclass);
        alert("Risk Score:" + score + "\n" + "Risk Class:" + rclass);
    }
    function optionvalidation() {
        
        var totalQuestions = <%= totalquestion %>;
        var maximumOptions = <%= optioncount %>;
        
        var isOptionSelected = true;
        var QuestionArray = new Array();
        var OptionArray = new Array();
        var questiontracker = 0;
        var optiontracker = 0;
        var notAnswered = new Array(totalQuestions);
        var notAnsweredDisplay = "";
        


        var optionsArr = new Array(totalQuestions) //later bring this number from Server side
        for (i = 1; i <= totalQuestions; i++)
            optionsArr[i] = new Array(maximumOptions)

        for (var i = 0; i < document.forms[0].elements.length; i++) {

            if (document.forms[0].elements[i].type == "checkbox" && document.forms[0].elements[i].id.indexOf("rbtnQ") > 0) {
                var optionId = document.forms[0].elements[i].id;
                var optionValue = optionId.substring(optionId.indexOf("rbtnQ") + 5)
                var answer = optionValue.split('A')
               
                optionsArr[answer[0]][answer[1]] = document.forms[0].elements[i].checked;

            }
        }


        for (i = 1; i <= totalQuestions; i++) {
            notAnswered[i] = false;
            var isSelected = false;
            for (j = 1; j < maximumOptions; j++) {
                if (optionsArr[i][j] == true) {
                    notAnswered[i] = true;
                    break;
                }
            }
        }

        for (i = 1; i <= totalQuestions; i++) {
            if (notAnswered[i] == false)
                notAnsweredDisplay += i + ",";
        }
        if (notAnsweredDisplay != "")
        {
            notAnsweredDisplay = notAnsweredDisplay.substr(0, notAnsweredDisplay.length - 1)
            alert("Please select answer for question(s) " + notAnsweredDisplay)
            return false;
       }
       return  GoalDeactiveConfirm();

    }

   
    }

</script>
</head>
<body>
  <form runat="server">
   <table width="100%" >
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="3" width="50%" align="left" style="font-family:Arial;font-size:14px;font-weight:bold;">
                <asp:Label ID="lblOrgName" runat="server" Text=""></asp:Label> 
            </td>
            
             <td colspan="2" width="50%" align="right">
             <img alt="AdvisorLogo" src="ByPass.aspx" /> 
            </td>
         </tr>
         <tr><td colspan="5"></td></tr>   
        <tr>
        <td colspan="5" align="center" style=" font-family: Verdana,Tahoma;font-weight: bold;font-size: 16px;color: black; width:100%;">
                <strong><u>Investor's Need Analysis </u></strong>
            </td>
          </tr>
          
          <tr>
          <td><br /></td>
          </tr>
            <tr>
          <td colspan="5"></td>  
           </tr> 
         <tr>
        <td class="FieldName" align="left" >Date :</td>
        <td>
        <table cellspacing="0" border="1">
        <tr>
        <td style="color:#E8FFFF;">&nbsp;d&nbsp;</td><td style="color:#E8FFFF;">&nbsp;d&nbsp;</td><td style="color:#E8FFFF;">&nbsp;m&nbsp;</td><td style="color:#E8FFFF;">&nbsp;m&nbsp;</td><td style="color:#E8FFFF;">&nbsp;y&nbsp;</td><td style="color:#E8FFFF;">&nbsp;y&nbsp;</td><td style="color:#E8FFFF;">&nbsp;y&nbsp;</td><td style="color:#E8FFFF;">&nbsp;y&nbsp;</td>
        </tr>
        </table>
        </td>
        <td colspan="3"></td>
        </tr>
         <tr>
         <td colspan="5"></td>
         </tr>
         <tr>
         <td class="FieldName" align="left">Name:</td>
         <td colspan="4" class="FieldName">
                <input type="checkbox" id="Checkbox3" />Mr. &nbsp;<input type="checkbox" id="Checkbox4" />Mrs.
                &nbsp;<input type="checkbox" id="Checkbox5" />Ms. &nbsp;<input type="checkbox" id="Checkbox6" />Dr.
            </td>
          </tr>
         <tr>
          <td></td>  
        <td colspan="4">
        <table width="100%" cellspacing="0" border="1">
        <tr>
        <td style="color:#E8FFFF;">&nbsp;f</td><td style="color:#E8FFFF;">&nbsp;i</td><td style="color:#E8FFFF;">&nbsp;r</td><td style="color:#E8FFFF;">&nbsp;s</td><td style="color:#E8FFFF;">&nbsp;t</td><td style="color:#E8FFFF;">&nbsp;n</td>
        <td style="color:#E8FFFF;">&nbsp;a</td><td style="color:#E8FFFF;">m</td><td style="color:#E8FFFF;">&nbsp;e</td><td>&nbsp;&nbsp;&nbsp;</td><td style="color:#E8FFFF;">m</td><td style="color:#E8FFFF;">&nbsp;i</td>
        <td style="color:#E8FFFF;">&nbsp;d</td><td style="color:#E8FFFF;">&nbsp;d</td><td style="color:#E8FFFF;">&nbsp;l</td><td style="color:#E8FFFF;">&nbsp;e</td><td style="color:#E8FFFF;">&nbsp;n</td><td style="color:#E8FFFF;">&nbsp;a</td>
        <td style="color:#E8FFFF;">m</td><td style="color:#E8FFFF;">&nbsp;e</td><td>&nbsp;&nbsp;&nbsp;</td><td style="color:#E8FFFF;">&nbsp;l</td><td style="color:#E8FFFF;">&nbsp;a</td><td style="color:#E8FFFF;">&nbsp;s</td>
        <td style="color:#E8FFFF;">&nbsp;t</td><td style="color:#E8FFFF;">&nbsp;n</td><td style="color:#E8FFFF;">&nbsp;a</td><td style="color:#E8FFFF;">m</td><td style="color:#E8FFFF;">&nbsp;e</td><td>&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        </tr> 
        <tr>
        <td colspan="5"></td>
        </tr>
         <tr>
         <td colspan="5" align="justify" style="font-size:medium;font-family:Verdana,Tahoma;">
         <p>The purpose of this investment profile form is for us to better understand your financial means, investment experience and financial goals.</p>
         <p>Your input will help us identify your investment profile, assist you in making right investment choices and guiding you to a secure and promising financial future.</p>
         <p>Please try to provide an accurate information as it will impact the results. We assure you it will be time well spent which will enable you achieve long financial freedom.</p>
         </td>
         </tr>
         <tr><td colspan="5"></td></tr>
         
        <tr>
            <td colspan="5">
                        
            <table width="100%"  cellspacing="0">
            <tr>
            <td colspan="5" align="center"><img alt="Personal Details" src="../Images/PersonalDetails.png"/></td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
        <td class="FieldName" align="left">Address1:</td>
        <td colspan="4">
        <table width="100%" cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td class="FieldName" align="left">Address2:</td>
        <td colspan="4">
        <table width="100%" cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td class="FieldName" align="left">Address3:</td>
        <td colspan="4">
        <table width="100%" cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td class="FieldName" align="left">City:</td>
        <td colspan="3">
        <table width="100%" cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        <td colspan="3"><span>&nbsp;</span></td>
        </tr>
        <tr>
        <td class="FieldName" align="left">PinCode :</td>
        <td><table cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        <td colspan="3"><span>&nbsp;</span></td>
        </tr>
        <tr>
        <td class="FieldName" align="left">State :</td>
        <td colspan="3">
        <table width="100%" cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        <td colspan="3"><span>&nbsp;</span></td>
        </tr>
        <tr>
        <td class="FieldName" align="left">Country:</td>
        <td colspan="3">
        <table width="100%" cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        <td colspan="3"><span>&nbsp;</span></td>
        </tr>
        <tr>
        <td class="FieldName" align="left">Mobile No. :</td>
         <td><table cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        <td colspan="3"><span>&nbsp;</span></td>
        </tr>
        <tr>
        <td class="FieldName" align="left">Email ID:</td>
        <td colspan="4">
        <table width="100%" cellspacing="0" border="1">
        <tr>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        <td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td class="FieldName" align="left">Date Of Birth:</td>
        <td>
        <table cellspacing="0" border="1">
        <tr>
        <td style="color:#E8FFFF;">&nbsp;d&nbsp;</td><td style="color:#E8FFFF;">&nbsp;d&nbsp;</td><td style="color:#E8FFFF;">&nbsp;m&nbsp;</td><td style="color:#E8FFFF;">&nbsp;m&nbsp;</td><td style="color:#E8FFFF;">&nbsp;y&nbsp;</td><td style="color:#E8FFFF;">&nbsp;y&nbsp;</td><td style="color:#E8FFFF;">&nbsp;y&nbsp;</td><td style="color:#E8FFFF;">&nbsp;y&nbsp;</td>
        </tr>
        </table>
        </td>
        <td colspan="3"></td>
        </tr>
        
        
        <!--********** Family *************-->
        <tr>
            <td colspan="5">
            </td>
        </tr>
        
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="1" cellspacing="0">
            <tr>
            <td colspan="5" align="Left" class="header" style="font-size:small; border-bottom: 1px Important; background-color:#7DA5E0; "> Family Details</td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="100%" cellspacing="0">
                    <tr>
                        <td class="FieldName" align="center">
                        Sr no.
                        </td>
                        <td class="FieldName" align="center">
                            Name
                        </td>
                        <td class="FieldName" align="center">
                            Relationship
                        </td>
                        <td class="FieldName" align="center">
                            DOB
                        </td>
                        <td class="FieldName" align="center">
                           Email
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 20%;">
                           
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 20%;">
                            
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 20%;">
                           
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 20%;">
                            
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width:20%;">
                            
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                    </tr>
                   </table>
            </td>
        </tr>
        
        <!--*************************Goal*************************-->
        
        <tr>
        <td colspan="5"></td>
        </tr>
        <tr>
        <td colspan="3" align="left" style="font-family:Arial;font-size:14px;">
        <asp:Label ID="lblRMNameFamily" runat="server" Text=""></asp:Label>
        </td>
        <td colspan="2" align="right" style="font-family:Arial;font-size:14px; "><asp:Label ID="lblDate1" runat="server" Text=""></asp:Label></td>
        </tr>
       <tr>
       <td>     
      <br />
       <br />
       <br />
       <br />
      
       <br /></td>
    </tr>
    <tr>
    
            <td colspan="3" width="50%" align="left" style="font-family:Arial;font-size:14px;font-weight:bold;">
                <asp:Label ID="lblOrgnameGoal" runat="server" Text=""></asp:Label> 
            </td>
           
             <td colspan="2" align="right" width="50%">
             <img alt="AdvisorLogo" src="ByPass.aspx" /> 
            </td>
         </tr>
         <tr><td colspan="5"></td></tr>
         <tr>
          <td colspan="5" align="center" style=" font-family: Verdana,Tahoma;font-weight: bold;font-size: 16px;color: black; width:60%;">
                <strong><u>Investor's Need Analysis </u></strong>
            </td>
         </tr> 
      <tr><td><br /></td></tr>   
         <tr>
        <td colspan="5"></td>
        </tr>
         <tr>
        <td colspan="5"></td>
        </tr>
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="0" cellspacing="0">
            <tr>
            <td colspan="5" align="center"><img alt="Goals" src="../Images/Goal.png" /></td>
            </tr>
            </table>
             </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="80%" cellspacing="0">
                    <tr>
                        <td align="center" class="FieldName">
                        Description
                        </td>
                        <td align="center" class="FieldName">
                            Cost today
                        </td>
                        <td align="center" class="FieldName">
                            Goal Year
                        </td>
                        <td align="center" class="FieldName">
                            Priority
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Buy home
                        </td>
                        <td style="width: 25%;">
                            &nbsp
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            First child education
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Second child education
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            First child marriage
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Second child marriage
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Retirement
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Other
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td colspan="5"></td>
        </tr>
        <!--**************** Investment***********-->
        <tr>
            <td colspan="5" align="left" style="font-size:medium;font-family:Verdana,Tahoma;">
            <p>Note: Priority 1 being the highest.</p>
            </td>
        </tr>
         <tr>
         <td colspan="5"></td>
         </tr> 
         <tr><td><br /><br /><br /><br /></td></tr>     
        
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="0" cellspacing="0">
            <tr>
            <td colspan="5" align="center"><img alt="Investment Details" src="../Images/Investment.png" /></td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td style="width: 100%;" colspan="5">
                <table border="none" width="100%" style="border-bottom:none;border-left:none;border-right:none;border-top:none;">
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Direct Equity
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Gold
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%; border:none;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            MF Equity
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Collectibles
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%; border:none;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            MF Debt
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Cash & Savings
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%; border:none; ">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            MF Hybrid -Equity
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Structured Product
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%; border:none; ">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            MF Hybrid -Debt
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Commodities
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Fixed Income
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Private Equity
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Govt Savings
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            PMS
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Pension & Gratuities
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20;">
                            Others
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Property
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td colspan="3" style="border:none;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!--*************Expense Details*********-->
        <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
        <td colspan="3" align="left" style="font-family:Arial;font-size:14px;">
        <asp:Label ID="lblRmNameExpanse" runat="server" Text=""></asp:Label>
        </td>
        <td colspan="2" align="right" style="font-family:Arial;font-size:14px; "><asp:Label ID="lblDate2" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr><td><br /><br /><br /><br /><br /><br /></td></tr>
        <tr>
            <td colspan="3" align="left" width="50%" style="font-family:Arial;font-size:14px;font-weight:bold;">
                <asp:Label ID="lblOrgname1" runat="server" Text=""></asp:Label> 
            </td>
           
             <td width="50%" colspan="2" align="right">
             <img alt="AdvisorLogo" src="ByPass.aspx" /> 
            </td>
         </tr>
         <tr><td colspan="5"></td></tr>
         <tr>
          <td colspan="5" align="center" style=" font-family: Verdana,Tahoma;font-weight: bold;font-size: 16px;color: black; width:60%;">
                <strong><u>Investor's Need Analysis </u></strong>
            </td>
         </tr>
         <tr><td><br /><br /></td></tr>
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="0" cellspacing="0">
            <tr>
            <td colspan="5" align="center"> <img alt="Expense Details" src="../Images/Expense.png"/></td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td style="width:100%;" colspan="5">
                <table border="1" width="100%" style="border-bottom:none;border-left:none;border-right:none;border-top:none;" >
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Food
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Entertainment-Holidays
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Rent
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Personal wear
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Utilities
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Insurance
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Health-Personal care
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Domestic Help
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Conveyance
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Other
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="font-size:medium;font-family:Verdana,Tahoma;" >
            <p>Note: Please enter monthly.</p>
            </td>
        </tr>


        <!--*************Income Details*********-->
        <tr>
            <td colspan="5">
            </td>
        </tr>
        
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="0" cellspacing="0">
            <tr>
            <td colspan="5" align="center"><img alt="Income Details" src="../Images/income.png" /></td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td style="width: 100%;" colspan="5">
                <table border="none" width="100%" style="border-bottom:none;border-left:none;border-right:none;border-top:none;">
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Salary
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Capital Gains
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Rental Property
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Agricultural income
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Business & Profession
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 20%;">
                            Other Sources
                        </td>
                        <td style="width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 20%;border:none; ">
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!--**************Liabilities Details**************-->
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="font-size:medium;font-family:@Arial Unicode MS;" >
            <p>Note: Please enter monthly.</p>
            </td>
        </tr>
        <tr>
        <td colspan="5"></td>
        </tr>
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="0" cellspacing="0">
            <tr>
            <td colspan="5" align="center"><img  alt="Liabilities Details" src="../Images/liabilities.png" /></td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="60%" cellspacing="0">
                    <tr>
                        <td align="center" class="FieldName">
                        </td>
                        <td align="center" class="FieldName">
                            Loan Outstanding
                        </td>
                        <td align="center" class="FieldName">
                            EMI(Annual)
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Home Loan
                        </td>
                        
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Auto Loan
                        </td>
                       
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Personal Loan
                        </td>
                       
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Education Loan
                        </td>
                       
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Other Loan
                        </td>
                        
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr><td><br /></td></tr>
        <tr>
        <td colspan="3" align="left" style="font-family:Arial;font-size:14px;">
        <asp:Label ID="lblRMRisk" runat="server" Text=""></asp:Label>
        </td>
        <td colspan="2" align="right" style="font-family:Arial;font-size:14px;"><asp:Label ID="lblDate3" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr><td><br /><br /><br /><br /></td></tr>
       <tr>
            <td colspan="3" align="left" width="50%" style="font-family:Arial;font-size:14px;font-weight:bold;">
                <asp:Label ID="lblOrgRisk" runat="server" Text=""></asp:Label> 
            </td>
            
             <td colspan="2" align="right" width="50%">
             <img alt="AdvisorLogo" src="ByPass.aspx" /> 
            </td>
         </tr>
         <tr><td colspan="5"></td></tr>
         <tr>
         <td colspan="5" align="center" style=" font-family: Verdana,Tahoma;font-weight: bold;font-size: 16px;color: black; width:60%;">
                <strong><u>Investor's Need Analysis </u></strong>
            </td>
         </tr>
         <tr><td><br /><br /></td></tr>
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="0" cellspacing="0">
            <tr>
            <td colspan="5" align="center" ><img alt="Risk Protection" src="../Images/RiskProtection.png" /></td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="1" cellspacing="0">
            <tr>
            <td colspan="5" align="Left" class="header" style=" font-size:small; border-bottom: 1px Important; background-color:#7DA5E0; "> Life Insurance Details</td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="80%" cellspacing="0">
                    <tr>
                        <td align="center" class="FieldName">
                        </td>
                        <td align="center" class="FieldName">
                            Sum Assured
                        </td>
                        <td align="center" class="FieldName">
                            Premium(Annual)
                        </td>
                        <td align="center" class="FieldName">
                            Surrender/Market Value
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Term
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Endowment
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Whole life
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Money back
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            ULIP
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Other
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        
        <tr>
            <td colspan="5">
                        
            <table width="100%" border="1" cellspacing="0">
            <tr>
            <td colspan="5" align="Left" class="header" style=" font-size:small; border-bottom: 1px Important; background-color:#7DA5E0; "> General Insurance Details</td>
            </tr>
            </table>
             </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="60%" cellspacing="0">
                    <tr>
                        <td align="center" class="FieldName">
                        </td>
                        <td align="center" class="FieldName">
                            Sum Assured
                        </td>
                        <td align="center" class="FieldName">
                            Premium(Annual)
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Health Insurance Cover
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Property inusrance cover
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Personal accident
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                       
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Others
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                       
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="6">
            </td>
        </tr>
        <tr><td><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /></td></tr>
        <tr>
        <td colspan="3" align="left" style="font-family:Arial;font-size:14px;">
        <asp:Label ID="lblRMRiskPro" runat="server" Text=""></asp:Label>
        </td>
        <td colspan="2" align="right" style="font-family:Arial;font-size:14px; "><asp:Label ID="lblDate4" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br /><br />
     
    <table width="100%">
        <tr>
            <td colspan="3" align="left"width="50%" style="font-family:Arial;font-size:14px;font-weight:bold;"><asp:Label ID="lblOrgQues" runat="server" Text=""></asp:Label></td>
            
             <td colspan="2" align="right" width="50%">
            <img alt="AdvisorLogo" src="ByPass.aspx" /> 
            </td>
        </tr>
        <tr><td colspan="5"></td></tr>
        <tr>
        <td width="100%" align="center" colspan="5">
            <img alt="Financial Plan Questionnaire" src="../Images/Questionnaire.png" />
            </td>
        </tr>
        <tr><td><br /><br /></td></tr>
    <tr>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </tr>
    </table>
    
    <table width="100%">
    <tr><td><br /><br /><br /><br /></td></tr>
    <tr>
  
        <td width="50%" align="left" style="font-family:Arial;font-size:14px;">
        <asp:Label ID="lblRMName" runat="server" Text=""></asp:Label>
        </td>
     
        <td width="50%" align="right" style="font-family:Arial;font-size:14px; "><asp:Label ID="lblDate5" runat="server" Text=""></asp:Label></td>
  
    </tr>
    </table>
     
    </form>
</body>
  
</html>



