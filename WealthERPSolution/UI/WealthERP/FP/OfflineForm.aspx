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
            <td>
            <img alt="AdvisorLogo" src="ByPass.aspx" /> 
             <%--<img alt="Advisor Logo"  src="../Images/werplogo.jpg" height="50" />--%>
             <%--<asp:Image ID="Img1" runat="server" AlternateText="Advisor Logo" Height="50" />--%>
            </td>
           
            <td colspan="2">
                <strong><u>Financial Planning Form </u></strong>
            </td>
        </tr>
        <tr>
          <td colspan="5"></td>  
        </tr>
        <tr><td colspan="5"><p>Dear Customer,</p></td></tr>
        <tr>
        <td colspan="5" align="justify">
        <p>Thank you for agreeing to taking some time off and completing your Wealth Financial Plan Questionnaire which will help you obtain a 360-degree view of your investments. It is our endeavor to assist you in making the right investment choices and guiding you to a secure and promising financial future.</p>
        <p>Financial planning is a critical exercise in ensuring long-term financial security. A financial plan is a road map to help you achieve your life’s financial goals.During the financial planning process you analyze what your financial needs and goals are. Your response will help us prepare a Financial Plan which will help you achieve all your financial and life event goals. Answering the questionnaire will not take more than 10 minutes of your time. Please try to provide an accurate estimate of your current assets and investments. Information provided by you will be kept strictly confidential. Thank you for sitting aside ten minutes for answering the questionnaire. We can assure you it will be time well spent and will enable you acheive life long financial freedom. We look forward to a long association with you.</p>
        </td>
        </tr>
    <%--    <tr>
        <td colspan="5" align="justify">
        <p>Financial planning is a critical exercise in ensuring long-term financial security. A financial plan is a road map to help you achieve your life’s financial goals.During the financial planning process you analyze what your financial needs and goals are. Your response will help us prepare a Financial Plan which will help you achieve all your financial and life event goals. Answering the questionnaire will not take more than 10 minutes of your time. Please try to provide an accurate estimate of your current assets and investments. Information provided by you will be kept strictly confidential. Thank you for sitting aside ten minutes for answering the questionnaire. We can assure you it will be time well spent and will enable you acheive life long financial freedom. We look forward to a long association with you.</p>
        </td>
        </tr>--%>
        <%--<tr><td colspan="5"><p>Thanking you,</p></td></tr>--%>
        <tr>
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                Self 
                
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td align="left" class="FieldName">
                First Name:
            </td>
            <td>
                <input id="txtFirstName" type="text" name="firstname" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;" />
            </td>
            <td align="left" class="FieldName">
                DOB:
            </td>
            <td>
                <input id="Text1" type="text" name="firstname" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="FieldName" align="left">
                Middle Name:
            </td>
            <td>
                <input id="Text2" type="text" name="firstname" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;"/>
            </td>
            <td class="FieldName" align="left">
                Email Id:
            </td>
            <td>
                <input id="Text4" type="text" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="FieldName" align="left">
                Last Name:
            </td>
            <td>
                <input id="Text3" type="text" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;" />
            </td>
                        <td class="FieldName" align="left">
                Gender:
            </td>
            <td>
                <input id="Text5" type="text" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;"/>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="FieldName" align="left">
                Address1:
            </td>
            <td>
                <input id="Text6" type="text" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;"/>
            </td>
                        <td class="FieldName" align="left">
                Address2:
            </td>
            <td>
                <input id="Text7" type="text" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;"/>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="FieldName" align="left">
                Address3:
            </td>
            <td>
                <input id="Text8" type="text" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;"/>
            </td>
            <td colspan="3">
            </td>
        </tr>
          <tr>
            <td colspan="5">
            </td>
        </tr>
        <!--********** Family *************-->
        <tr>
            <td colspan="5">
            </td>
        </tr>
         <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                Family Members Details
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="100%">
                    <tr>
                        <td class="FieldName" align="center">
                        </td>
                        <td class="FieldName" align="center">
                            Member1
                        </td>
                        <td class="FieldName" align="center">
                            Member2
                        </td>
                        <td class="FieldName" align="center">
                            Member3
                        </td>
                        <td class="FieldName" align="center">
                            Member4
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 12%;">
                            FirstName
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 12%;">
                            MiddleName
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 12%;">
                            LastName
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 12%;">
                            Relationship
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 12%;">
                            Date Of Birth
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 12%;">
                            Email Id
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 12%;">
                            Children Education cost
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 12%;">
                            Children Marriage cost
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
                        </td>
                        <td style="width: 22%;">
                            &nbsp;
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
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
          <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
            <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
            <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <!--**************** Investment***********-->
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                Investment Details
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td style="width: 100%;" colspan="5">
                <table border="none" width="100%">
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Direct Equity:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Gold:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            MF Equity:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Collectibles:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            MF Debt:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Cash & Savings:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            MF Hybrid -Equity:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Structured Product:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            MF Hybrid -Debt:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Commodities:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Fixed Income:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Private Equity:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Govt Savings:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            PMS:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Pension & Gratuities:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Others:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Property:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td colspan="3">
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
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                Expense Details
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td style="width: 100%;" colspan="5">
                <table border="none" width="100%">
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Food:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Entertainment-Holidays:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Rent:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Personal wear:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Utilities:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Insurance:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Health-Personal care:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Domestic Help:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Conveyance:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Other:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <!--*************Income Details*********-->
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                Income Details
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td style="width: 100%;" colspan="5">
                <table border="none" width="100%">
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Salary:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Capital Gains:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Rental Property
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Agricultural income:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Business & Profession:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
                        </td>
                        <td align="left" class="FieldName" style="width: 25%;">
                            Other Sources:
                        </td>
                        <td style="width: 25%;">
                            &nbsp;
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
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                Liabilities Details
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="100%">
                    <tr>
                        <td align="center" class="FieldName">
                        </td>
                        <td align="center" class="FieldName">
                            Loan Outstanding
                        </td>
                        <td align="center" class="FieldName">
                            Tenure(in months)
                        </td>
                        <td align="center" class="FieldName">
                            EMI
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;">
                            Home Loan
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
                            Auto Loan
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
                            Personal Loan
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
                            Education Loan
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
                            Other Loan
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
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                Life Insurance Details
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="100%">
                    <tr>
                        <td align="center" class="FieldName">
                        </td>
                        <td align="center" class="FieldName">
                            Sum Assured
                        </td>
                        <td align="center" class="FieldName">
                            Premium
                        </td>
                        <td align="center" class="FieldName">
                            Maturity Date
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
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                General Insurance Details
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="left" style="width: 100%;">
                <table border="1" width="100%">
                    <tr>
                        <td align="center" class="FieldName">
                        </td>
                        <td align="center" class="FieldName">
                            Sum Assured
                        </td>
                        <td align="center" class="FieldName">
                            Premium
                        </td>
                        <td align="center" class="FieldName">
                            Maturity Date
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldName" align="left" style="width: 25%;" style="border-bottom: 1px Important; ">
                            Health Insurance Cover
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
                        <td class="FieldName" align="left" style="width: 25%;" style="border-bottom: 1px Important; ">
                            Property inusrance cover
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
                        <td class="FieldName" align="left" style="width: 25%;border-bottom: 1px Important;">
                            Personal accident
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
            <td colspan="6">
            </td>
        </tr>
        <tr>
            <td align="left" class="FieldName">
                Risk Class:
            </td>
            <td colspan="4" class="FieldName">
                <input type="checkbox" id="chk1" />Conservative &nbsp;<input type="checkbox" id="Checkbox1" />Moderate
                &nbsp;<input type="checkbox" id="Checkbox2" />Aggressive
            </td>
        </tr>
          <tr>
            <td colspan="5" align="left" class="header" style="border-bottom: 1px Important; background-color:#7DA5E0; ">
                Goal
            </td>
               <tr>
            <td class="FieldName" align="left">
                Buying house (year):
            </td>
            <td>
                <input id="Text9" type="text" class="txtField" style="border-bottom: solid 1px; border-top: solid 1px; border-left:solid 1px; border-right: solid 1px;"/>
            </td>
            <td colspan="3">
            </td>
        </tr>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br /><br /><br /><br />
    <br /><br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br /><br /><br /><br />
    <br /><br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br /><br /><br /><br />
    <br /><br />
    <br />
    <br />
    <br />
    <table width="100%">
        <tr>
             <td>
            <img alt="AdvisorLogo" src="ByPass.aspx" /> 
            </td>
            <td colspan="5" align="Left" font-family="verdana" font-size="18px">
                <strong><u>Financial Plan Questionnaire </u></strong>
            </td>
        </tr>
    <tr>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </tr>
    </table>
</body>
</form>  
</html>



