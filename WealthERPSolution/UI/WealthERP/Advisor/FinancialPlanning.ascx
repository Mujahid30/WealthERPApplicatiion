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
<table width="100%">
    <tr>
        <td>
            <ajaxtoolkit:tabcontainer id="tabRiskProfilingAndAssetAllocation" runat="server"
                activetabindex="1" width="100%" style="visibility: visible">
                <ajaxToolkit:TabPanel ID="tabRiskProfiling" runat="server" HeaderText="Risk Profiling"
                    Visible="true">
                    <HeaderTemplate>
                        Risk Profiling
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
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
                               
                                  <td width="100%" colspan="6">                    
                                                                
                                    <asp:Label ID="lblRiskProfilingParagraph" runat="server" CssClass="GridViewCmbField" style="white-space:inherit" Text="">
                                    </asp:Label>
                                               
                                <br />
                               </td> 
                                
                            </tr>
                            
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnSubmitRisk" runat="server" CssClass="PCGButton" OnClick="btnSubmitRisk_Click"
                                                    Text="Submit" OnClientClick="return optionvalidation()" />
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
                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblAge" runat="server" CssClass="FieldName" Text="Based on Customer profile Age is "></asp:Label>
                                    <asp:Label ID="lblAgeResult" runat="server" CssClass="FieldName"></asp:Label>
                                </td>                           
                                <td align="right" colspan="2">
                                    <asp:Label ID="lblApprovedByCustomerOn" runat="server" Text="Approved By Customer On : "
                                        CssClass="FieldName"></asp:Label>                                
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApprovedByCustomerOn" runat="server" CssClass="Field"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calApprovedByCustomerOn" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" TargetControlID="txtApprovedByCustomerOn">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtApprovedByCustomerOn_WaterMarkText"
                                        runat="server" Enabled="True" TargetControlID="txtApprovedByCustomerOn" WatermarkText="dd/mm/yyyy">
                                    </ajaxToolkit:TextBoxWatermarkExtender>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblRiskClass" runat="server" CssClass="FieldName" Text="Based on  Risk Assessment Customer Risk class is "></asp:Label>                                
                                    <asp:Label ID="lblRClassRs" runat="server" CssClass="FieldName"></asp:Label><asp:Label
                                        ID="lblRiskScore" runat="server" Text="  &  Risk Score is  " CssClass="FieldName"></asp:Label>
                                    <asp:Label ID="lblRscoreAA" runat="server" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblChartErrorDisplay" runat="server" Text="No Age to display chart. Fill Date of Birth!"
                                        Visible="False"></asp:Label>
                                </td>
                                <td>
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
                                    <asp:Chart ID="cActualAsset" runat="server" Height="165px" Width="214px">
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
                                <td colspan="2">
                                    <table id="tblRecommended" runat="server">
                                        <tr id="Tr2" runat="server">
                                            <td id="Td4" runat="server" align="center" colspan="2">
                                                <asp:Label ID="lblRecommended" runat="server" CssClass="HeaderTextSmaller" Text="Recommended Asset Allocation"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="Tr3" runat="server">
                                            <td id="Td5" runat="server" align="right">
                                                <asp:Label ID="lblRecommendedEquity" runat="server" CssClass="FieldName" Text="Equity:"></asp:Label>
                                            </td>
                                            <td id="Td6" runat="server">
                                                <asp:TextBox ID="txtRecommendedEquity" runat="server" CssClass="Field" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr4" runat="server">
                                            <td id="Td7" runat="server" align="right">
                                                <asp:Label ID="lblRecommendedDebt" runat="server" CssClass="FieldName" Text="Debt:"></asp:Label>
                                            </td>
                                            <td id="Td8" runat="server">
                                                <asp:TextBox ID="txtRecommendedDebt" runat="server" CssClass="Field" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr5" runat="server">
                                            <td id="Td9" runat="server" align="right">
                                                <asp:Label ID="lblRecommendedCash" runat="server" CssClass="FieldName" Text="Cash:"></asp:Label>
                                            </td>
                                            <td id="Td10" runat="server">
                                                <asp:TextBox ID="txtRecommendedCash" runat="server" CssClass="Field" 
                                                    ReadOnly="True"></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            
                                <tr id="trCurrentAssetAllocation" runat="server">
                                <td>
                                    <asp:Chart ID="ChartCurrentAsset" runat="server" Height="165px" Width="214px">
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
                                <td colspan="2">
                                    <table id="tblCurrent" runat="server">
                                        <tr id="Tr6" runat="server">
                                            <td id="Td11" runat="server" align="center" colspan="2">
                                                <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmaller" Text="Current Asset Allocation"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="Tr7" runat="server">
                                            <td id="Td12" runat="server" align="right">
                                                <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Equity:"></asp:Label>
                                            </td>
                                            <td id="Td13" runat="server">
                                                <asp:TextBox ID="txtCurrentEquity" runat="server" CssClass="Field" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr8" runat="server">
                                            <td id="Td14" runat="server" align="right">
                                                <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Debt:"></asp:Label>
                                            </td>
                                            <td id="Td15" runat="server">
                                                <asp:TextBox ID="txtCurrentDebt" runat="server" CssClass="Field" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr9" runat="server">
                                            <td id="Td16" runat="server" align="right">
                                                <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Cash:"></asp:Label>
                                            </td>
                                            <td id="Td17" runat="server">
                                                <asp:TextBox ID="txtCurrentCash" runat="server" CssClass="Field" 
                                                    ReadOnly="True"></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            
                            <tr id="trCustomerAssetText" runat="server">
                            <td colspan="6" width="100%">
                                                            
                             <asp:Label ID="lblCustomerParagraph" runat="server" BackColor="Transparent" CssClass="Field"></asp:Label>
                            </td>
                            </tr>
                            
                            
                            
                            
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="PCGButton"
                                        Width="62px" OnClientClick="return optionvalidation()" />
                                </td>
                                <td class="style1">
                                </td>
                                <td>
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
