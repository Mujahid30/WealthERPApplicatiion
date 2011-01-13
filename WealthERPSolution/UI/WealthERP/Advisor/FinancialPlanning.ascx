<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FinancialPlanning.ascx.cs"
    Inherits="WealthERP.Advisor.FinancialPlanning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<style type="text/css">
    .style1
    {
        width: 42px;
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

            if (document.forms[0].elements[i].type == "radio" && document.forms[0].elements[i].id.indexOf("rbtnQ") > 0) {
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

    function GoalDeactiveConfirm()
    {
     if(document.getElementById("<%= hidGoalCount.ClientID %>")!=null)
     {
     if(document.getElementById("<%= hidGoalCount.ClientID %>").value >0)
     {  
      
       var confirmValue=confirm("All the goals setup for you will be deactivated. Do you wish to continue?");
        
      if(confirmValue)
      {
          return true;         
      }
      else 
       {
          return false;
       }
       
      }
      }
    }

</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="3">
            <hr />
        </td>
    </tr>
   <%-- <tr>
        <td width="150px" align="right">
            <asp:Label ID="lblPickCustomer" runat="server" Text="Pick a customer  : " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPickCustomer" runat="server" Width="142px" AutoPostBack="True"
                OnTextChanged="txtPickCustomer_TextChanged" CssClass="txtField" AutoComplete="Off"></asp:TextBox>
            <ajaxtoolkit:autocompleteextender id="txtParentCustomer_autoCompleteExtender" runat="server"
                targetcontrolid="txtPickCustomer" servicemethod="GetCustomerName" servicepath="~/CustomerPortfolio/AutoComplete.asmx"
                minimumprefixlength="1" enablecaching="False" completionsetcount="5" completioninterval="100"
                completionlistcssclass="AutoCompleteExtender_CompletionList" completionlistitemcssclass="AutoCompleteExtender_CompletionListItem"
                completionlisthighlighteditemcssclass="AutoCompleteExtender_HighlightedItem"
                usecontextkey="True" onclientitemselected="GetCustomerId" delimitercharacters=""
                enabled="True" />
        </td>
        <td>
            <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <hr />
        </td>
    </tr>--%>
</table>
<table width="100%"  >
    <tr>
        <td>
            <ajaxtoolkit:tabcontainer id="tabRiskProfilingAndAssetAllocation" runat="server"
                activetabindex="0" width="100%" style="visibility: visible;">
                <ajaxToolkit:TabPanel ID="tabRiskProfiling" runat="server" HeaderText="Risk Profiling"
                    Visible="true">
                    <HeaderTemplate>
                        Risk Profiling
                    </HeaderTemplate>
                    <ContentTemplate>
                    <table runat="server" id="tblPickOptions" style="text-align: left;" width="100%">
                        <tr align="left" runat="server">
                            <td runat="server" style="float: left">
                            <br />
                                <asp:Label ID="lblPickWhat" runat="server" CssClass="HeaderTextSmall" style="float: right" Text="Do you wish to: "></asp:Label>
                            </td>
                            <td style="float: left; vertical-align: middle" runat="server">
                            <br />
                            <asp:RadioButton ID="rbtnPickRiskclass"  AutoPostBack="True" runat="server" 
                                    CssClass="txtField" GroupName="RiskProfile" Text="Pick Risk Class"
                                    oncheckedchanged="rbtnPickRiskclass_CheckedChanged" />
                            <asp:RadioButton ID="rbtnAnsQuestions" GroupName="RiskProfile" AutoPostBack="True" runat="server" 
                                    CssClass="txtField" Text="Answers the Questions" 
                                    oncheckedchanged="rbtnAnsQuestions_CheckedChanged" />
                                    <br />
                             </td>
                        </tr>
                        
                    </table>
                    <table runat="server" id="tblPickRiskClass">
                        <tr runat="server">
                            <td runat="server">
                            <br />
                                <asp:Label ID="lblPickRiskPlass" runat="server" Text="Pick Risk Class" CssClass="HeaderTextSmall"></asp:Label>
                        
                            </td>
                            <td runat="server">
                            <br />
                                <asp:DropDownList ID="ddlPickRiskClass" runat="server" CssClass="cmbField" 
                                    style="vertical-align: middle">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                        <table width="100%">
                            <tr runat="server" id="trRiskProfiler">
                                <td runat="server">
                                <br />
                                    <asp:Label ID="lblRiskProfiler" Text="Risk Profiler Questionnaire" runat="server"
                                        CssClass="HeaderTextSmall"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblRiskScore" runat="server" style="background-color:#D3D3D3; border: thin solid #FF0000"
                                        visible="False" width="100%">
                                        <tr id="Tr1" runat="server">
                                            <td id="Td1" runat="server">
                                                <strong>Risk Score:</strong><asp:Label ID="lblRScore" runat="server" CssClass="FieldName"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td id="Td2" runat="server">
                                                <strong>Risk Class:</strong><asp:Label ID="lblRClass" runat="server" CssClass="FieldName"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td id="Td3" runat="server">
                                                <strong>Risk Profile Date:</strong><asp:Label ID="lblRiskProfileDate" runat="server"
                                                    CssClass="FieldName" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trRiskProfilingParagraph" runat="server">
                               
                                  <td width="100%" colspan="6" runat="server">                    
                                                                
                                    <asp:Label ID="lblRiskProfilingParagraph" runat="server" 
                                          CssClass="GridViewCmbField" style="white-space:inherit"></asp:Label>
                                               
                                <br />
                               </td> 
                                
                            </tr>
                            
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                        <td>
                                        <div runat="server" id="divQuestionAnswers">
                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                            </div>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnSubmitRisk" runat="server" CssClass="PCGButton" OnClick="btnSubmitRisk_Click"
                                                    Text="Submit" OnClientClick="return optionvalidation()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnSubmitForPickRiskclass" runat="server" CssClass="PCGButton"
                                                    Text="Submit" onclick="btnSubmitForPickRiskclass_Click"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tabAssetAllocation" runat="server" HeaderText="Asset Allocation"
                    Visible="true">
                    <HeaderTemplate>
                        Asset Allocation
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                        <tr style="float: left">
                        <td>
                            <asp:Label ID="lblApprovedByCustomerOn" runat="server" Text="Approved By Customer On : "
                            CssClass="FieldName"></asp:Label>                                
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtApprovedByCustomerOn" runat="server" CssClass="Field"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calApprovedByCustomerOn" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" TargetControlID="txtApprovedByCustomerOn">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtApprovedByCustomerOn_WaterMarkText"
                            runat="server" Enabled="True" TargetControlID="txtApprovedByCustomerOn" WatermarkText="dd/mm/yyyy">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                         </td>
                        </tr>
                        </table>
                        <table width="100%">
                        <tr>
                            <td>
                            <br />
                            <br />
                            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" 
                                        Text="Recomonded Asset Allocation Chart"></asp:Label>
                            </td>
                            <td>
                            <br />
                            <br />
                            <asp:Label ID="Label5" runat="server" 
                                        CssClass="HeaderTextSmall" Text="Current Asset Allocation Chart"></asp:Label>
                            </td>
                        </tr>
                            <tr>
                                <td>
                                    <asp:Chart ID="cActualAsset" runat="server" Height="200px" Width="400px">
                                        <Series>
                                            <asp:Series ChartArea="caActualAsset" Name="sActualAsset">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="caActualAsset">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                                
                                <td ID="trCurrentAssetAllocation" runat="server" 
                                    style="padding: 0px; float: left">
                                   
                                    <asp:Chart ID="ChartCurrentAsset" runat="server" Height="200px" Width="400px">
                                        <Series>
                                            <asp:Series ChartArea="caActualAsset" Name="sActualAsset">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="caActualAsset">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                            </tr>
                                <tr ID="trCustomerAssetText" runat="server">
                                <td runat="server" colspan="5" width="100%">
                                <asp:Label ID="lblCustomerParagraph" runat="server" CssClass="Field" 
                                        BackColor="Transparent"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                            <td>
                                                            
                                <asp:Button ID="btnSave" runat="server" CssClass="PCGButton" 
                                    OnClick="btnSave_Click" OnClientClick="return optionvalidation()" Text="Save" 
                                    Width="62px" />
                            </td>
                                <td class="style1">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAge" runat="server" CssClass="FieldName" 
                                        Text="Based on Customer profile Age is "></asp:Label>
                                    <asp:Label ID="lblAgeResult" runat="server" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblRiskClass" runat="server" CssClass="FieldName" 
                                        Text="Based on  Risk Assessment Customer Risk class is "></asp:Label>
                                    <asp:Label ID="lblRClassRs" runat="server" CssClass="FieldName"></asp:Label>
                                    <asp:Label ID="lblRiskScore" runat="server" CssClass="FieldName" 
                                        Text="  &amp;  Risk Score is  "></asp:Label>
                                    <asp:Label ID="lblRscoreAA" runat="server" CssClass="FieldName"></asp:Label>
                                </td>                           
                               
                                <td></td>
                                 <td>
                                 </td>
                                 <td>
                                 </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblChartErrorDisplay" runat="server" 
                                        Text="No Age to display chart. Fill Date of Birth!" Visible="False"></asp:Label>                                
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxtoolkit:tabcontainer>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hidGoalCount" runat="server" />
        </td>
    </tr>
</table>
